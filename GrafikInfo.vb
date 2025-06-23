Imports System.IO
Imports OpenHardwareMonitor.Hardware.Gpu
Imports BasicComputerInfo = Microsoft.VisualBasic.Devices.ComputerInfo
Imports System.Diagnostics
Imports System.Management

Public Class GrafikInfo
    Private Computer As Computer
    Private OhmComputer As New Computer()
    Dim documentsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    Dim settingsPath As String = Path.Combine(documentsPath, "CoolCore")
    Private printDocument As New Printing.PrintDocument()
    Private printPreviewDialog As New PrintPreviewDialog()
    Private cpuInfoToPrint As String
    Private systemInfoPrint As String

    Private Async Sub GrafikInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Grafik Info"
        TabPage1.Text = "Grafik Info:"
        TabPage2.Text = "System Info:"
        Tabpane.BackColor = ColorTranslator.FromHtml("#F0F0F0")
        Tabpane.ForeColor = SystemColors.WindowText
        SystemViewList.View = View.Details
        SystemViewList.Columns.Add("Category", 150)
        SystemViewList.Columns.Add("Information", 300)
        SystemViewList.Items.Clear()
        If Computer Is Nothing Then
            Computer = New Computer()
            Computer.Open(True)
        End If
        OhmComputer.IsCpuEnabled = True
        OhmComputer.IsGpuEnabled = True
        OhmComputer.IsMemoryEnabled = True
        OhmComputer.IsPsuEnabled = True
        OhmComputer.IsStorageEnabled = True
        OhmComputer.IsMotherboardEnabled = True
        OhmComputer.IsNetworkEnabled = True
        OhmComputer.IsControllerEnabled = True
        Computer = New Computer() With {
            .IsMotherboardEnabled = True,
            .IsCpuEnabled = True,
            .IsMemoryEnabled = True,
            .IsGpuEnabled = True,
            .IsPsuEnabled = True,
            .IsStorageEnabled = True
        }
        Dim systemInfoTask As Task = Task.Run(Function() Collect_AllSystemInfo())
        Await Task.Run(Sub()
                           ReadGraphicCardInfo()
                           GetGraphicInfo()

                       End Sub)
    End Sub

    Private Function GetGraphicInfo() As Task
        Try
            'GPU Monitoring
            Dim gpu = Enumerable.FirstOrDefault(CType(Computer.Hardware, IEnumerable(Of IHardware)), CType(Function(h) h.HardwareType = Global.OpenHardwareMonitor.Hardware.HardwareType.GpuNvidia OrElse h.HardwareType = Global.OpenHardwareMonitor.Hardware.HardwareType.GpuAmd OrElse h.HardwareType = Global.OpenHardwareMonitor.Hardware.HardwareType.GpuIntel, Func(Of IHardware, Boolean)))
            If gpu Is Nothing Then
                Debug.WriteLine("Keine GPU gefunden!")
            End If
            gpu?.Update()
            Dim gpuSensors = gpu?.Sensors.Where(Function(s) s.SensorType = SensorType.Temperature OrElse s.SensorType = SensorType.Clock OrElse s.SensorType = SensorType.Load).ToList()
            If gpuSensors IsNot Nothing Then
                For Each sensor In gpuSensors
                    Select Case sensor.SensorType
                        Case SensorType.Temperature
                            If sensor.Value.HasValue Then
                                Me.Invoke(Sub()
                                              GCTempBox.Text = $"{sensor.Value.Value:F1}°C"
                                              Dim lastGcLoadBarValue As Integer = -1
                                              If sensor.SensorType = SensorType.Temperature Then
                                                  If sensor.Value.HasValue Then
                                                      Dim newValue As Integer = CInt(sensor.Value.Value)
                                                      If newValue <> lastGcLoadBarValue Then
                                                          Me.Invoke(Sub()
                                                                        GCTempBox.Text = $"{sensor.Value.Value:F1}°C"

                                                                        lastGcLoadBarValue = newValue
                                                                    End Sub)
                                                      End If
                                                  End If
                                              End If
                                          End Sub)
                            End If
                        Case SensorType.Clock
                            If sensor.Value.HasValue Then
                                Me.Invoke(Sub() GCClockBox.Text = $"{sensor.Value.Value:F1}MHz")
                            End If
                        Case SensorType.Load

                            If sensor.Value.HasValue Then
                                Dim loadValue As Integer = CInt(sensor.Value.Value)
                                Me.Invoke(Sub()
                                              Loadlbl.Text = $"{loadValue}%"
                                          End Sub)
                            End If
                    End Select
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving GPU information: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Task.CompletedTask
    End Function
    Private Function ReadGraphicCardInfo() As Task
        Dim UseWmiForGpu = True
        Dim gpuInfo As String = "N/A"
        Dim boardGpuInfo As String = "N/A"
        Try
            If UseWmiForGpu Then
                Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_VideoController")
                Dim icon = ImageList1
                GCList.LargeImageList = ImageList1
                GCList.SmallImageList = ImageList1
                GCList.View = View.SmallIcon
                GCList.Items.Clear()
                For i = 0 To GCList.Items.Count - 1
                    GCList.Items(i).ImageIndex = i
                Next
                ' Hinweis: Die Images müssen vorher in ImageList1 hinzugefügt worden sein (z.B. im Designer oder per Code).
                For Each queryObj As ManagementObject In searcher.Get()
                    boardGpuInfo = If(queryObj("Name"), "N/A").ToString()
                    Me.Invoke(Sub()
                                  GCNameBox.Text = boardGpuInfo
                                  SystemViewList.Items.Add($"On Board Graphic: {boardGpuInfo}").ImageIndex = 1
                                  ' Detaillierte Infos in GCViewList eintragen
                                  GCList.Items.Clear()
                                  GCList.Items.Add($"Name: {If(queryObj("Name"), "N/A")}")
                                  GCList.Items.Add($"Hersteller: {If(queryObj("AdapterCompatibility"), "N/A")}")
                                  Dim vramSize = If(queryObj("AdapterRAM"), 0L)
                                  GCList.Items.Add($"VRAM: {FormatBytes(vramSize)}")
                                  GCList.Items.Add($"Treiberversion: {If(queryObj("DriverVersion"), "N/A")}")
                                  GCList.Items.Add($"Status: {If(queryObj("Status"), "N/A")}")
                                  GCList.Items.Add($"Geräte-ID: {If(queryObj("PNPDeviceID"), "N/A")}")
                                  GCList.Items.Add($"Video-Prozessor: {If(queryObj("VideoProcessor"), "N/A")}")
                                  GCList.Items.Add($"Auflösung: {If(queryObj("CurrentHorizontalResolution"), "N/A")} x {If(queryObj("CurrentVerticalResolution"), "N/A")}")
                                  GCList.Items.Add($"Farb-Tiefe: {If(queryObj("CurrentBitsPerPixel"), "N/A")} Bit")
                                  GCList.Items.Add($"Aktualisierungsrate: {If(queryObj("CurrentRefreshRate"), "N/A")} Hz")
                              End Sub)
                Next

            End If

            Dim gpu = Computer.Hardware.FirstOrDefault(Function(h) h.HardwareType = HardwareType.GpuNvidia OrElse h.HardwareType = HardwareType.GpuAmd OrElse h.HardwareType = HardwareType.GpuIntel)
            If gpu IsNot Nothing Then
                gpu.Update()
                gpuInfo = gpu.Name
                Me.Invoke(Sub()
                              'GCNameBox.Text = gpu.Name
                          End Sub)
            End If
            Dim gcMemmory As Double = 0.0
            If gpu IsNot Nothing AndAlso gpu.HardwareType = HardwareType.GpuNvidia Then
                GCLogo.Image = Resources.Nvidia_Logo_wine
            ElseIf gpu IsNot Nothing AndAlso gpu.HardwareType = HardwareType.GpuAmd Then
                GCLogo.Image = Resources.atiamdlogo
            ElseIf gpu Is Nothing Then
                gpuInfo = "No GPU found"
                GCNameBox.Text = gpuInfo
            End If
            Dim gpuSensors = gpu?.Sensors.Where(Function(s) s.SensorType = SensorType.Temperature OrElse s.SensorType = SensorType.Clock OrElse s.SensorType = SensorType.Load).ToList()
            If gpuSensors IsNot Nothing Then
                For Each sensor In gpuSensors
                    Select Case sensor.SensorType
                        Case SensorType.Temperature
                            If sensor.Value.HasValue Then
                                Me.Invoke(Sub() GCTempBox.Text = $"{sensor.Value.Value:F1}°C")
                            End If
                        Case SensorType.Clock
                            If sensor.Value.HasValue Then
                                Me.Invoke(Sub() GCClockBox.Text = $"{sensor.Value.Value:F1} MHz")
                            End If
                        Case SensorType.Load
                            If sensor.Value.HasValue Then

                                Me.Invoke(Sub()
                                              Loadlbl.Text = $"{sensor.Value.Value}%"
                                          End Sub)
                            End If
                        Case SensorType.Fan
                            If sensor.Value.HasValue Then
                                Debug.WriteLine($"{sensor.Value.Value:F0} RPM")
                            End If
                    End Select
                Next
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error reading GPU info: {ex.Message}")
            MessageBox.Show("An error occurred while reading GPU information: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ' Fallback für On-Board-Grafik
        If String.IsNullOrEmpty(boardGpuInfo) OrElse boardGpuInfo = "N/A" Then
            boardGpuInfo = "On Board Graphic: " & gpuInfo
            Me.Invoke(Sub() GCNameBox.Text = boardGpuInfo)
            SystemViewList.Items.Add(boardGpuInfo).ImageIndex = 1
        End If
        ' Fallback für GPU-Info
        If String.IsNullOrEmpty(gpuInfo) OrElse gpuInfo = "N/A" Then
            gpuInfo = "No GPU found"
            Me.Invoke(Sub() GCNameBox.Text = gpuInfo)
            SystemViewList.Items.Add(gpuInfo).ImageIndex = 1
        End If
        ' Fallback für VRAM-Größe
        If String.IsNullOrEmpty(boardGpuInfo) OrElse boardGpuInfo = "N/A" Then
            boardGpuInfo = "On Board Graphic: " & gpuInfo
            Me.Invoke(Sub() GCNameBox.Text = boardGpuInfo)
            SystemViewList.Items.Add(boardGpuInfo).ImageIndex = 1
        End If
        Return Task.CompletedTask
    End Function

    Public Function Collect_AllSystemInfo()
        Try
            SystemViewList.Items.Clear() ' Leeren der ListView vor dem Hinzufügen neuer Einträge
            ' --- Allgemeine Systeminformationen (mit ComputerInfo via Alias) ---
            Dim basicComputerInfo As New BasicComputerInfo() ' Hier verwenden wir den Alias "BasicComputerInfo"
            AddListViewItem("OS Vollständiger Name", basicComputerInfo.OSFullName)
            AddListViewItem("OS Plattform", basicComputerInfo.OSPlatform)
            AddListViewItem("OS Version", basicComputerInfo.OSVersion)
            AddListViewItem("OS Service Pack", basicComputerInfo.InstalledUICulture.ThreeLetterISOLanguageName)
            AddListViewItem("Verfügbarer Phys. Speicher", FormatBytes(basicComputerInfo.AvailablePhysicalMemory))
            AddListViewItem("Gesamter Phys. Speicher", FormatBytes(basicComputerInfo.TotalPhysicalMemory))
            AddListViewItem("Verfügbarer Virt. Speicher", FormatBytes(basicComputerInfo.AvailableVirtualMemory))
            AddListViewItem("Gesamter Virt. Speicher", FormatBytes(basicComputerInfo.TotalVirtualMemory))
            AddListViewItem("Computer Name", Environment.MachineName)
            AddListViewItem("Benutzername", Environment.UserName)
            AddListViewItem("Prozessoranzahl", Environment.ProcessorCount.ToString())
            AddListViewItem("Systemverzeichnis", Environment.SystemDirectory)
            AddListViewItem("Aktuelles Verzeichnis", Environment.CurrentDirectory)
            AddListViewItem("CLR Version", Environment.Version.ToString())
            AddListViewItem("Ist 64-bit OS", Environment.Is64BitOperatingSystem.ToString())
            AddListViewItem("Ist 64-bit Prozess", Environment.Is64BitProcess.ToString())

            For Each hardware As IHardware In OhmComputer.Hardware
                AddListViewItem("Hardware", hardware.Name & " (" & hardware.HardwareType.ToString() & ")")

                If hardware.SubHardware.Count > 0 Then
                    For Each subHardware As IHardware In hardware.SubHardware
                        AddListViewItem("  Sub-Hardware", subHardware.Name & " (" & subHardware.HardwareType.ToString() & ")")
                        For Each sensor As ISensor In subHardware.Sensors
                            Dim sensorValue As String = If(sensor.Value.HasValue, sensor.Value.Value.ToString("0.0") & GetSensorUnit(sensor.SensorType), "N/A")
                            AddListViewItem("    " & sensor.Name, sensorValue)
                        Next
                    Next
                End If

            Next

            AddListViewItem("", "--- Festplatteninformationen ---") ' Trennlinie
            Dim searcherHD As New ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType = 3") ' DriveType 3 für lokale Festplatte
            For Each queryObj As ManagementObject In searcherHD.Get()
                AddListViewItem("Laufwerk (" & queryObj("DeviceID").ToString() & ")", queryObj("VolumeName") & " (" & queryObj("FileSystem") & ")")
                AddListViewItem("  Gesamtspeicher", FormatBytes(Convert.ToUInt64(queryObj("Size"))))
                AddListViewItem("  Freier Speicher", FormatBytes(Convert.ToUInt64(queryObj("FreeSpace"))))
            Next

            ' --- Prozessorinformationen (mit WMI) ---
            AddListViewItem("", "--- Prozessorinformationen ---") ' Trennlinie
            Dim searcherCPU As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
            For Each queryObj As ManagementObject In searcherCPU.Get()
                AddListViewItem("Prozessor Name", queryObj("Name").ToString())
                AddListViewItem("  Hersteller", queryObj("Manufacturer").ToString())
                AddListViewItem("  Anzahl der Kerne", queryObj("NumberOfCores").ToString())
                AddListViewItem("  Anzahl logischer Prozessoren", queryObj("NumberOfLogicalProcessors").ToString())
                AddListViewItem("  Architektur", GetProcessorArchitecture(Convert.ToUInt16(queryObj("Architecture"))))
            Next

            ' --- Netzwerkadapterinformationen (mit WMI) ---
            AddListViewItem("", "--- Netzwerkadapterinformationen ---") ' Trennlinie
            Dim searcherNet As New ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = TRUE")
            For Each queryObj As ManagementObject In searcherNet.Get()
                AddListViewItem("Adapter Name", queryObj("Description").ToString())
                If queryObj("IPAddress") IsNot Nothing Then
                    Dim ipAddresses As String() = DirectCast(queryObj("IPAddress"), String())
                    For Each ip As String In ipAddresses
                        AddListViewItem("  IP Adresse", ip)
                    Next
                End If
                If queryObj("MACAddress") IsNot Nothing Then
                    AddListViewItem("  MAC Adresse", queryObj("MACAddress").ToString())
                End If
                If queryObj("DNSHostName") IsNot Nothing Then
                    AddListViewItem("  DNS Host Name", queryObj("DNSHostName").ToString())
                End If
            Next
            Return True
        Catch ex As Exception
            MessageBox.Show("Ein Fehler beim Sammeln der Systeminformationen ist aufgetreten: " & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Fehler Collect System Data: {ex.Message}")
            Return False
        End Try
    End Function
    Private Sub AddListViewItem(category As String, info As String)
        If SystemViewList.InvokeRequired Then
            SystemViewList.Invoke(Sub() AddListViewItem(category, info))
        Else
            Dim item As New ListViewItem(category) With {
                .UseItemStyleForSubItems = False,
                .ImageIndex = If(String.IsNullOrEmpty(info), 0, 0)
                }
            item.SubItems.Add(info)
            SystemViewList.Items.Add(item)
        End If
    End Sub
    Private Function FormatBytes(bytes As ULong) As String
        Const KB As ULong = 1024
        Const MB As ULong = KB * 1024
        Const GB As ULong = MB * 1024

        If bytes >= GB Then
            Return (bytes / GB).ToString("0.00") & " GB"
        ElseIf bytes >= MB Then
            Return (bytes / MB).ToString("0.00") & " MB"
        ElseIf bytes >= KB Then
            Return (bytes / KB).ToString("0.00") & " KB"
        Else
            Return bytes.ToString() & " Bytes"
        End If
    End Function

    Private Function GetProcessorArchitecture(architectureCode As UShort) As String
        Select Case architectureCode
            Case 0
                Return "Intel x86"
            Case 1
                Return "MIPS"
            Case 2
                Return "Alpha"
            Case 3
                Return "PowerPC"
            Case 5
                Return "ARM"
            Case 6
                Return "Itanium-based Alpha"
            Case 9
                Return "x64 (AMD64 und Intel EM64T)"
            Case 12
                Return "ARM64"
            Case Else
                Return "Unbekannt"
        End Select
    End Function
    Private Sub Tabpane_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim tabIndex As Integer = Tabpane.SelectedIndex
        Dim tabPage As TabPage = Tabpane.SelectedTab
        If tabIndex = 1 Then
            'TabPage2.BackgroundImage = _backgroundImage2
            tabPage.BackgroundImageLayout = ImageLayout.Stretch
            'tabPage.BackgroundImage = _backgroundImage2

        ElseIf tabIndex = 2 Then
            tabPage.BackgroundImageLayout = ImageLayout.Stretch
            'tabPage.BackgroundImage = _backgroundImage3
            'Collect_AllSystemInfo()
        End If
    End Sub
    Private Sub ContextMenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
        If e.ClickedItem IsNot Nothing AndAlso SystemViewList.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = SystemViewList.SelectedItems(0)
            Dim itemText As String = selectedItem.SubItems(1).Text
            If e.ClickedItem.Text = "kopieren" Then
                ContextMenuStrip1.Close()
                Clipboard.SetText(itemText)
                MessageBox.Show(Me, $"{itemText}", "kopiert")
            End If
        End If
    End Sub
    Private Sub ContextMenuStrip2_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
        If e.ClickedItem IsNot Nothing AndAlso GCList.SelectedItems.Count > 0 Then
            Dim selectedItem2 As ListViewItem = GCList.SelectedItems(0)
            Dim itemText2 As String = selectedItem2.SubItems(0).Text
            If e.ClickedItem.Text = "kopieren" Then
                ContextMenuStrip2.Close()
                Clipboard.SetText(itemText2)
                MessageBox.Show(Me, $"{itemText2}", "kopiert")
            End If
        End If
    End Sub

    Private Function GetSensorUnit(sensorType As SensorType) As String
        Select Case sensorType
            Case SensorType.Temperature
                Return " °C"
            Case SensorType.Fan
                Return " RPM"
            Case SensorType.Load
                Return " %"
            Case SensorType.Clock
                Return " MHz"
            Case SensorType.Power
                Return " W"
            Case SensorType.Data
                Return " GB" ' Oder Bytes/MB/KB je nach Kontext, hier als Beispiel GB
            Case SensorType.Voltage
                Return " V"
            Case Else
                Return ""
        End Select
    End Function

End Class