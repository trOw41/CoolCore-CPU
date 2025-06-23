Imports System.IO
Imports OpenHardwareMonitor.Hardware.Gpu
Imports BasicComputerInfo = Microsoft.VisualBasic.Devices.ComputerInfo
Imports System.Diagnostics
Imports System.Management

Public Class GrafikInfo
    ' Dies ist ein Beispiel, der genaue Name hängt von Ihrem Menüeintrag ab

    Private Function GrafikInfo_LoadAsync(sender As Object, e As EventArgs) As Task Handles MyBase.Load
        Me.Text = "System Info"

        Tabpane.TabPages.Clear()
        Tabpane.TabPages.Add(TabPage2) ' Make sure TabPage2 is added to the Tabpane
        TabPage2.Text = "System Info:"
        Tabpane.BackColor = ColorTranslator.FromHtml("#F0F0F0")
        Tabpane.ForeColor = SystemColors.WindowText
        SystemViewList.View = View.Details
        SystemViewList.Columns.Clear()
        SystemViewList.Columns.Add("Category", 150)
        SystemViewList.Columns.Add("Information", 300)
        SystemViewList.Items.Clear()
        Collect_AllSystemInfo()
        Return Task.CompletedTask

    End Function

    Public Function Collect_AllSystemInfo() As Boolean
        Try
            'SystemViewList.Items.Clear() ' Leeren der ListView vor dem Hinzufügen neuer Einträge
            ' --- Allgemeine Systeminformationen (mit ComputerInfo via Alias) ---
            Dim basicComputerInfo As New Microsoft.VisualBasic.Devices.ComputerInfo() ' Fully qualify BasicComputerInfo
            AddListViewItem("OS Vollständiger Name", basicComputerInfo.OSFullName)
            AddListViewItem("OS Plattform", basicComputerInfo.OSPlatform)
            AddListViewItem("OS Version", basicComputerInfo.OSVersion)
            ' Corrected: basicComputerInfo.OSServicePack is the correct property for service pack
            'AddListViewItem("OS Service Pack", basicComputerInfo.OSServicePack)
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

            AddListViewItem("", "--- Festplatteninformationen ---") ' Trennlinie
            Dim searcherHD As New Management.ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType = 3") ' DriveType 3 für lokale Festplatte
            For Each queryObj As Management.ManagementObject In searcherHD.Get()
                AddListViewItem("Laufwerk (" & queryObj("DeviceID").ToString() & ")", queryObj("VolumeName") & " (" & queryObj("FileSystem") & ")")
                AddListViewItem("  Gesamtspeicher", FormatBytes(Convert.ToUInt64(queryObj("Size"))))
                AddListViewItem("  Freier Speicher", FormatBytes(Convert.ToUInt64(queryObj("FreeSpace"))))
            Next

            ' --- Prozessorinformationen (mit WMI) ---
            AddListViewItem("", "--- Prozessorinformationen ---") ' Trennlinie
            Dim searcherCPU As New Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor")
            For Each queryObj As Management.ManagementObject In searcherCPU.Get()
                AddListViewItem("Prozessor Name", queryObj("Name").ToString())
                AddListViewItem("  Hersteller", queryObj("Manufacturer").ToString())
                AddListViewItem("  Anzahl der Kerne", queryObj("NumberOfCores").ToString())
                AddListViewItem("  Anzahl logischer Prozessoren", queryObj("NumberOfLogicalProcessors").ToString())
                AddListViewItem("  Architektur", GetProcessorArchitecture(Convert.ToUInt16(queryObj("Architecture"))))
            Next

            ' --- Netzwerkadapterinformationen (mit WMI) ---
            AddListViewItem("", "--- Netzwerkadapterinformationen ---") ' Trennlinie
            Dim searcherNet As New Management.ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = TRUE")
            For Each queryObj As Management.ManagementObject In searcherNet.Get()
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
        ' Since Collect_AllSystemInfo is now called from the UI thread (GrafikInfo_LoadAsync),
        ' the InvokeRequired check is unnecessary and causes the items not to be added.
        ' We can directly add the item.
        Dim item As New ListViewItem(category)
        item.SubItems.Add(info)
        SystemViewList.Items.Add(item)
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

    ' You will likely need to define the SensorType enum if it's not already defined
    Public Enum SensorType
        Temperature
        Fan
        Load
        Clock
        Power
        Data
        Voltage
    End Enum

    Private Sub PerformCopyAction()
        If SystemViewList.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = SystemViewList.SelectedItems(0)
            Dim itemText As String = selectedItem.SubItems(1).Text
            ContextMenuStrip1.Close()
            Clipboard.SetText(itemText)
            MessageBox.Show(Me, $"{itemText}", "kopiert")
        End If
    End Sub

    ' This is your event handler, which now just calls the shared method
    Private Sub ContextMenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked
        If e.ClickedItem IsNot Nothing Then ' Removed SelectedItems.Count > 0 from here, as PerformCopyAction handles it.
            If e.ClickedItem.Text = "kopieren" Then
                PerformCopyAction()
            End If
        End If
    End Sub
End Class