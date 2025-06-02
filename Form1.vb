Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports Timer = System.Windows.Forms.Timer
Imports System.Globalization
Imports System.Drawing.Text
Imports Azure
Imports System.CodeDom.Compiler
Imports System.IdentityModel.Metadata

Public Structure CoreTempData
    Public Property Timestamp As DateTime
    Public Property CoreTemperatures As Dictionary(Of String, Single)
End Structure

Public Class Form1
    Private systemInfoRepository As SystemInfoRepository
    Private cpuLoadCounter As PerformanceCounter
    Private refreshTimer As Timer
    Private cpuLoadCounters As New List(Of PerformanceCounter)()
    Private ReadOnly LoadBoxes As New Dictionary(Of Integer, TextBox)()
    Private ReadOnly MinTempBoxes As New Dictionary(Of Integer, TextBox)()
    Private ReadOnly MaxTempBoxes As New Dictionary(Of Integer, TextBox)()
    Private computer As Computer
    Private cpu As IHardware
    Private coreTemperatures As New List(Of ISensor)()
    Private loadingForm As Form3
    Private isMonitoringActive As Boolean = False
    Private backgroundTempMeasurements As New List(Of CoreTempData)()
    Private monitoringTask As Task
    Private cts As CancellationTokenSource
    Private ReadOnly CoreTempBoxes As New Dictionary(Of Integer, TextBox)()
    Private ReadOnly coreIndex As Integer
    Private monitoringStopwatch As New Stopwatch()
    Private monitoringTimer As Timer
    Private monitoringForm As Form3
    Private ReadOnly VoltBoxes As New Dictionary(Of Integer, TextBox)
    Private cpuVoltageSensorMap As New Dictionary(Of Integer, ISensor)
    Private genericVcoreSensor As ISensor = Nothing
    Private stressTasks As New List(Of Task)()
    Private cancellationTokenSource As CancellationTokenSource

    Public Sub New()
        InitializeComponent()
        systemInfoRepository = New SystemInfoRepository()
        cpuLoadCounter = New PerformanceCounter("Processor", "% Processor Time", "_Total")
        refreshTimer = New Timer With {
            .Interval = 1000
            }
        AddHandler refreshTimer.Tick, AddressOf RefreshTimer_Tick
        If LoadBox IsNot Nothing Then LoadBoxes.Add(0, LoadBox)
        If LoadBox1 IsNot Nothing Then LoadBoxes.Add(1, LoadBox1)
        If LoadBox2 IsNot Nothing Then LoadBoxes.Add(2, LoadBox2)
        If LoadBox3 IsNot Nothing Then LoadBoxes.Add(3, LoadBox3)

        If CoreTemp IsNot Nothing Then CoreTempBoxes.Add(0, CoreTemp)
        If CoreTemp1 IsNot Nothing Then CoreTempBoxes.Add(1, CoreTemp1)
        If CoreTemp2 IsNot Nothing Then CoreTempBoxes.Add(2, CoreTemp2)
        If CoreTemp3 IsNot Nothing Then CoreTempBoxes.Add(3, CoreTemp3)

        If MinTemp IsNot Nothing Then MinTempBoxes.Add(0, MinTemp)
        If MinTemp1 IsNot Nothing Then MinTempBoxes.Add(1, MinTemp1)
        If MinTemp2 IsNot Nothing Then MinTempBoxes.Add(2, MinTemp2)
        If MinTemp3 IsNot Nothing Then MinTempBoxes.Add(3, MinTemp3)

        If MaxTemp IsNot Nothing Then MaxTempBoxes.Add(0, MaxTemp)
        If MaxTemp1 IsNot Nothing Then MaxTempBoxes.Add(1, MaxTemp1)
        'AddHandler BtnToggleMonitoring.Click, AddressOf BtnToggleMonitoring_Click

        If MaxTemp2 IsNot Nothing Then MaxTempBoxes.Add(2, MaxTemp2)
        If MaxTemp3 IsNot Nothing Then MaxTempBoxes.Add(3, MaxTemp3)

        If Vbox1 IsNot Nothing Then VoltBoxes.Add(1, Vbox1)
        If VBox2 IsNot Nothing Then VoltBoxes.Add(2, VBox2)
        If VBox3 IsNot Nothing Then VoltBoxes.Add(3, VBox3)
        If VBox4 IsNot Nothing Then VoltBoxes.Add(4, VBox4)
        refreshTimer.Start()
    End Sub

    Private Sub StartCpuStressTest()
        ' Stoppt und bereinigt eventuell noch laufende frühere Stress-Tasks
        StopCpuStressTest()

        cancellationTokenSource = New CancellationTokenSource()
        Dim cancellationToken = cancellationTokenSource.Token

        Dim processorCount As Integer = Environment.ProcessorCount ' Ermittelt die Anzahl der logischen Prozessoren (Kerne/Threads)

        ' Startet für jeden logischen Prozessor eine eigene Task
        For i As Integer = 0 To processorCount - 1
            Dim cpuStressTask = Task.Run(Sub()
                                             While Not cancellationToken.IsCancellationRequested

                                                 Dim result As Double = 1.0
                                                 For j As Integer = 0 To 1000000
                                                     result = Math.Sqrt(result + Math.Sin(j) * Math.Cos(j))
                                                 Next
                                             End While
                                         End Sub, cancellationToken)
            stressTasks.Add(cpuStressTask)
        Next

        Me.Invoke(Sub()
                      LblStatusMessage.Text = "CPU-Stresstest läuft..."
                      LblStatusMessage.ForeColor = Color.DarkOrange
                  End Sub)
    End Sub

    Private Sub StopCpuStressTest()
        If cancellationTokenSource IsNot Nothing Then
            cancellationTokenSource.Cancel() ' Signalisiert allen Tasks, dass sie beendet werden sollen
            Try
                ' Warten Sie bis zu 5 Sekunden, bis alle Tasks beendet sind
                Task.WaitAll(stressTasks.ToArray(), 5000)
            Catch ex As AggregateException
                ' Behandelt Ausnahmen, die während des Wartens auf die Tasks auftreten könnten
                For Each innerEx In ex.InnerExceptions
                    Debug.WriteLine($"Fehler beim Beenden der Stress-Task: {innerEx.Message}")
                Next
            Catch ex As Exception
                Debug.WriteLine($"Fehler beim Warten auf Stress-Tasks: {ex.Message}")
            End Try
            cancellationTokenSource.Dispose() ' Gibt das Cancellation Token Source Objekt frei
            cancellationTokenSource = Nothing
        End If
        stressTasks.Clear() ' Leert die Liste der Tasks

        ' Optional: Aktualisieren Sie einen Status-Label in der Benutzeroberfläche
        Me.Invoke(Sub()
                      LblStatusMessage.Text = "CPU-Stresstest beendet."
                      LblStatusMessage.ForeColor = Color.Green
                  End Sub)
    End Sub

    Private Sub RecordTemperaturesInBackground(cancellationToken As CancellationToken)
        Dim intervalMs As Integer = 1000
        Do While Not cancellationToken.IsCancellationRequested
            Try
                cpu?.Update()
                Dim currentCoreTemps As New Dictionary(Of String, Single)()
                For Each sensor As ISensor In coreTemperatures
                    If sensor.Value.HasValue Then
                        currentCoreTemps.Add(sensor.Name, sensor.Value.Value)
                    End If
                Next
                If currentCoreTemps.Any() Then
                    SyncLock backgroundTempMeasurements
                        Dim newEntry As New CoreTempData With {
                        .Timestamp = Date.Now,
                        .CoreTemperatures = currentCoreTemps}
                        backgroundTempMeasurements.Add(newEntry)
                    End SyncLock
                End If
                Task.Delay(intervalMs, cancellationToken).Wait()
            Catch ex As OperationCanceledException

                Exit Do
            Catch ex As Exception
                Exit Do
            End Try
        Loop
    End Sub

    Private Function SaveTemperatureDataToCsv(data As List(Of CoreTempData))
        Dim programDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim logDirectory As String = Path.Combine(programDirectory, "TemperatureLogs")
        If Not Directory.Exists(logDirectory) Then
            Directory.CreateDirectory(logDirectory)
        End If
        Dim fileName As String = $"CoolCore_Temp_Log_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
        Dim filePath As String = Path.Combine(logDirectory, fileName)

        Try
            Using writer As New StreamWriter(filePath, False, Encoding.UTF8)
                Dim coreNames As New SortedSet(Of String)()
                For Each entry In data
                    For Each kvp In entry.CoreTemperatures
                        coreNames.Add(kvp.Key)
                    Next
                Next
                writer.Write("Timestamp")
                For Each coreName In coreNames
                    writer.Write($",{coreName} (°C)")
                Next
                writer.WriteLine()
                For Each entry In data
                    writer.Write(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                    For Each coreName In coreNames
                        Dim temp As Single = 0
                        If entry.CoreTemperatures.TryGetValue(coreName, temp) Then
                            writer.Write($",{temp.ToString("F1", CultureInfo.InvariantCulture)}")
                        Else
                            writer.Write(",N/A")
                        End If
                    Next
                    writer.WriteLine()
                Next
            End Using
            Me.Invoke(Sub()
                          LblStatusMessage.Text = $"Temperature data saved to {filePath}"
                          LblStatusMessage.ForeColor = Color.Blue
                      End Sub)
            Return filePath
        Catch ex As Exception
            Me.Invoke(Sub()
                          LblStatusMessage.Text = $"Error saving data: {ex.Message}"
                          LblStatusMessage.ForeColor = Color.Red
                      End Sub)
            Return Nothing
        End Try
        Return filePath
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblStatusMessage.Text = "Ready to read system information."
        LblStatusMessage.ForeColor = Color.Black
        ClearCpuDisplayControls()
        If computer Is Nothing Then
            computer = New Computer()
        End If
        computer.Hardware.Clear()
        computer.Open(True)
        computer.IsCpuEnabled = True
        computer.IsGpuEnabled = True
        LblStatusMessage.Text = "Real-time monitoring started. Static info saved."
        LblStatusMessage.ForeColor = Color.Firebrick
        InitializePerCoreCounters()
        InitializeCoreTemperatureSensors()
        'InitializeOpenHardwareMonitor()
        InitializeVoltageSensors()
        Using ReadAndDisplaySystemInfoAsync()
            refreshTimer.Start()
        End Using
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        refreshTimer.Stop()
        refreshTimer.Dispose()
        If monitoringTimer IsNot Nothing Then
            monitoringTimer.Dispose()
            monitoringTimer = Nothing
        End If
        If monitoringForm IsNot Nothing Then
            monitoringForm.Dispose()
            monitoringForm = Nothing
        End If
        For Each counter In cpuLoadCounters
            counter.Dispose()
        Next
        cpuLoadCounters.Clear()
    End Sub


    Private Sub InitializePerCoreCounters()
        Try

            Dim numberOfLogicalProcessors As Integer = 0
            Dim searcher As New ManagementObjectSearcher("SELECT NumberOfLogicalProcessors FROM Win32_Processor")
            For Each queryObj As ManagementObject In searcher.Get()
                numberOfLogicalProcessors = CInt(If(queryObj("NumberOfLogicalProcessors"), 0))
                Exit For
            Next

            If numberOfLogicalProcessors > 0 Then

                For Each counter In cpuLoadCounters
                    counter.Dispose()
                Next
                cpuLoadCounters.Clear()
                For i As Integer = 0 To Math.Min(numberOfLogicalProcessors - 1, LoadBoxes.Count - 1)
                    Dim instanceName As String = i.ToString()
                    Dim counter As New PerformanceCounter("Processor", "% Processor Time", instanceName)
                    cpuLoadCounters.Add(counter)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show($"Error initializing per-core counters: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Error initializing per-core counters: {ex.Message}")
        End Try
    End Sub

    Private Sub InitializeCoreTemperatureSensors()
        Try
            ' CPU-Hardwareobjekt finden
            If computer Is Nothing Then
                computer = New Computer()
                computer.Open(True) ' Open the computer object to access hardware
            End If
            cpu = computer.Hardware.FirstOrDefault(Function(h) h.HardwareType = HardwareType.Cpu)
            Dim numberOfLogicalProcessors As Integer = 0
            Dim searcher As New ManagementObjectSearcher("SELECT NumberOfLogicalProcessors FROM Win32_Processor")
            For Each queryObj As ManagementObject In searcher.Get()
                numberOfLogicalProcessors = CInt(If(queryObj("NumberOfLogicalProcessors"), 0))
                Exit For
            Next
            If numberOfLogicalProcessors > 0 Then
                If cpu IsNot Nothing Then
                    Debug.WriteLine($"CPU-Hardware erkannt: {cpu.Name}")
                    coreTemperatures.Clear()
                    For Each sensor As ISensor In cpu.Sensors
                        Debug.WriteLine($"  Gefundener Sensor: {sensor.Name}, Typ: {sensor.SensorType}, Wert: {sensor.Value}")
                        If sensor.SensorType = SensorType.Temperature Then
                            coreTemperatures.Add(sensor)
                            Dim coreIndex As Integer = numberOfLogicalProcessors - 1
                            If coreIndex > 0 Then
                                If Not CoreTempBoxes.ContainsKey(coreIndex) Then
                                    Dim tempBox As New TextBox With {
                                        .Name = $"CoreTemp{coreIndex}",
                                        .Text = $"{sensor.Value:F1}°C",
                                        .Location = New Point(10, 30 + coreIndex * 30),
                                        .Size = New Size(100, 20)
                                    }
                                    CoreTempBoxes.Add(coreIndex, tempBox)
                                    Me.Controls.Add(tempBox)
                                End If
                            End If

                            Debug.WriteLine($"Core-Temperatursensor gefunden: {sensor.Name}, Wert: {sensor.Value}")
                        End If
                    Next
                    coreTemperatures = coreTemperatures.OrderBy(Function(s) s.Name).ToList()

                    'MessageBox.Show($"Anzahl gefundener Core-Temperatursensoren nach Sortierung: {coreTemperatures.Count}")
                Else

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub InitializeVoltageSensors()
        cpuVoltageSensorMap.Clear()
        genericVcoreSensor = Nothing
        If cpu IsNot Nothing Then
            For Each sensor In cpu.Sensors
                If sensor.SensorType = SensorType.Voltage Then
                    ' Versuchen Sie, einen Core-Index aus dem Sensornamen zu parsen
                    ' Z.B. "CPU Core #0", "CPU Core #1"
                    If sensor.Name.StartsWith("CPU Core #", StringComparison.OrdinalIgnoreCase) Then
                        Dim namePart As String = sensor.Name.Replace("CPU Core #", "").Trim()
                        Dim coreNum As Integer
                        If Integer.TryParse(namePart.Split(" "c)(0), coreNum) Then
                            ' Wir mappen Core #0 auf VBox1, Core #1 auf VBox2, usw.
                            If coreNum <= VoltBoxes.Count Then
                                cpuVoltageSensorMap.Add(coreNum, sensor)
                                MessageBox.Show("Core Voltage Index: " & coreNum)
                            End If
                        End If
                    ElseIf sensor.Name.Equals("CPU VCore", StringComparison.OrdinalIgnoreCase) OrElse
                       sensor.Name.Equals("CPU VID", StringComparison.OrdinalIgnoreCase) Then
                        genericVcoreSensor = sensor
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub UpdateVoltageDisplay()
        Try
            If cpu IsNot Nothing Then
                cpu.Update()
                For Each kvp In cpuVoltageSensorMap
                    Dim boxIndex As Integer = kvp.Key
                    Dim sensor As ISensor = kvp.Value

                    If sensor.Value.HasValue AndAlso VoltBoxes.ContainsKey(boxIndex) Then
                        Me.Invoke(Sub()
                                      VoltBoxes(boxIndex).Text = $"{sensor.Value.Value:F3} V"
                                  End Sub)
                    ElseIf VoltBoxes.ContainsKey(boxIndex) Then
                        Me.Invoke(Sub()
                                      VoltBoxes(boxIndex).Text = "N/A"
                                  End Sub)
                    End If
                Next

                If genericVcoreSensor IsNot Nothing AndAlso genericVcoreSensor.Value.HasValue Then
                    If Not cpuVoltageSensorMap.ContainsKey(1) AndAlso VoltBoxes.ContainsKey(1) Then
                        Me.Invoke(Sub()
                                      VoltBoxes(1).Text = $"{genericVcoreSensor.Value.Value:F3} V (VCore)"
                                  End Sub)
                    End If
                End If
            End If

            For Each kvp In VoltBoxes
                If Me.InvokeRequired Then
                    Me.Invoke(Sub()
                                  If String.IsNullOrEmpty(kvp.Value.Text) OrElse Not kvp.Value.Text.EndsWith(" V") Then
                                      If Not (kvp.Value.Text.Contains("(VCore)") AndAlso Not String.IsNullOrEmpty(kvp.Value.Text)) Then
                                          kvp.Value.Text = "N/A"
                                      End If
                                  End If
                              End Sub)
                Else
                    If String.IsNullOrEmpty(kvp.Value.Text) OrElse Not kvp.Value.Text.EndsWith(" V") Then
                        If Not (kvp.Value.Text.Contains("(VCore)") AndAlso Not String.IsNullOrEmpty(kvp.Value.Text)) Then
                            kvp.Value.Text = "N/A"
                        End If
                    End If
                End If
            Next

        Catch ex As Exception
            Debug.WriteLine($"Error updating voltage display: {ex.Message}")
            For Each kvp In VoltBoxes
                If Me.InvokeRequired Then
                    Me.Invoke(Sub() kvp.Value.Text = "Error")
                Else
                    kvp.Value.Text = "Error"
                End If
            Next
        End Try
    End Sub

    Private Sub RefreshTimer_Tick(sender As Object, e As EventArgs)
        Try
            cpu?.Update()
            For i As Integer = 0 To cpuLoadCounters.Count - 1
                Dim currentCoreLoad As Single = cpuLoadCounters(i).NextValue()
                If LoadBoxes.ContainsKey(i) Then
                    LoadBoxes(i).Text = $"{currentCoreLoad:F2}%"
                End If
            Next

            Dim packagePowerSensor = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Power AndAlso s.Name.Contains("Package"))
            If packagePowerSensor IsNot Nothing AndAlso packagePowerSensor.Value.HasValue Then
                PowerBox.Text = $"{packagePowerSensor.Value.Value:F1}W"
            End If
            For i As Integer = 0 To cpuLoadCounters.Count - 1
                Dim sensor As ISensor = coreTemperatures(i)
                Dim coreIndex As Integer = i

                If CoreTempBoxes.ContainsKey(coreIndex) Then
                    CoreTempBoxes(coreIndex).Text = If(sensor.Value.HasValue, $"{sensor.Value.Value:F1}°C", "N/A")

                End If
                If MinTempBoxes.ContainsKey(coreIndex) Then
                    MinTempBoxes(coreIndex).Text = If(sensor.Min.HasValue, $"{sensor.Min.Value:F1}°C", "N/A") ' Minimum observed
                End If
                If MaxTempBoxes.ContainsKey(coreIndex) Then
                    MaxTempBoxes(coreIndex).Text = If(sensor.Max.HasValue, $"{sensor.Max.Value:F1}°C", "N/A") ' Maximum observed
                End If

            Next
            If monitoringStopwatch.Elapsed.TotalSeconds >= 30 Then
                StopMonitoringProcess()
                StopCpuStressTest()
                Exit Sub
            End If
        Catch ex As Exception
            For Each kvp In LoadBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In CoreTempBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In MinTempBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In MaxTempBoxes : kvp.Value.Text = "Error" : Next
        End Try
    End Sub

    Private Async Function ReadAndDisplaySystemInfoAsync() As Task
        LblStatusMessage.Text = "Reading system information and saving to database..."
        LblStatusMessage.ForeColor = Color.CadetBlue
        Dim systemInfo As New SystemInfoData With {
            .Timestamp = DateTime.Now
        }
        ClearCpuDisplayControls()
        Try

            systemInfo.OSSystem = Environment.OSVersion.VersionString
            systemInfo.SystemType = Environment.Is64BitOperatingSystem.ToString() & "-bit"
            systemInfo.ComputerName = Environment.MachineName
            systemInfo.UserName = Environment.UserName
            systemInfo.DomainName = Environment.UserDomainName
            systemInfo.ProcessorCount = Environment.ProcessorCount
            systemInfo.SystemDirectory = Environment.SystemDirectory
            systemInfo.ProgramDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            systemInfo.HostName = Dns.GetHostName()
            '#--------------------------------------------------------------------------------------------------------------------'
            Dim cpuName As String = "N/A"
            Dim numberOfCores As Integer = 0
            Dim numberOfLogicalProcessors As Integer = 0
            Dim currentClockSpeed As Integer = 0 ' MHz
            Dim architecture As String = "N/A"
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
            For Each queryObj As ManagementObject In searcher.Get()
                cpuName = If(queryObj("Name"), "N/A").ToString()
                numberOfCores = If(queryObj("NumberOfCores"), 0)
                numberOfLogicalProcessors = If(queryObj("NumberOfLogicalProcessors"), 0)
                currentClockSpeed = If(queryObj("CurrentClockSpeed"), 0)
                Dim archValue As Integer = If(queryObj("Architecture"), -1)
                Select Case archValue
                    Case 0 : architecture = "X86"
                    Case 1 : architecture = "MIPS"
                    Case 2 : architecture = "Alpha"
                    Case 3 : architecture = "PowerPC"
                    Case 5 : architecture = "ARM"
                    Case 6 : architecture = "ia64"
                    Case 9 : architecture = "X64"
                    Case Else : architecture = "Unknown"
                End Select
                Exit For
            Next
            '#--------------------------------------------------------------------------------------------------------------------'
            systemInfo.CpuName = cpuName
            systemInfo.NumberOfCores = numberOfCores
            systemInfo.NumberOfLogicalProcessors = numberOfLogicalProcessors
            systemInfo.CurrentClockSpeedMHz = currentClockSpeed
            systemInfo.Architecture = architecture
            '#--------------------------------------------------------------------------------------------------------------------'
            Dim biosVersion As String = "N/A"
            Dim biosSearcher As New ManagementObjectSearcher("SELECT Version FROM Win32_BIOS")
            For Each obj As ManagementObject In biosSearcher.Get()
                biosVersion = If(obj("Version"), "N/A").ToString()
                Exit For
            Next
            Me.Invoke(Sub()

                          ModelBox.Text = systemInfo.CpuName.Aggregate("", Function(current, nextChar) current & nextChar.ToString().ToUpperInvariant())
                          FrequencyBox.Text = systemInfo.CurrentClockSpeedMHz.ToString() & " MHz"
                          PlatformBox.Text = systemInfo.Architecture
                          CoresBox.Text = systemInfo.NumberOfCores.ToString()
                          ThreadBox.Text = systemInfo.NumberOfLogicalProcessors.ToString()
                          PowerBox.Text = "N/A"

                          '#--------------------------------------------------------------------------------------------------------------------'
                          If cpu IsNot Nothing Then

                              ModelBox.Text &= $" ({cpu.Identifier})"
                              Dim packagePowerSensor = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Power AndAlso s.Name.Contains("Package"))
                              If packagePowerSensor IsNot Nothing AndAlso packagePowerSensor.Value.HasValue Then
                                  PowerBox.Text = $"{packagePowerSensor.Value.Value:F1}W"
                              End If

                              Dim coreVoltageSensor = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Voltage AndAlso s.Name.Contains("Core"))
                              If coreVoltageSensor IsNot Nothing AndAlso coreVoltageSensor.Value.HasValue Then
                                  PowerBox2.Text = $"{coreVoltageSensor.Value.Value:F3}V"
                                  UpdateVoltageDisplay()
                              Else
                                  PowerBox2.Text = "N/A"
                              End If
                              RevisionBox.Text = cpu.Identifier.ToString.LastIndexOf("/"c)
                              CPUIDBox.Text = "N/A"
                              LitBox.Text = "N/A"
                          Else
                              TDPBox.Text = "N/A"
                              VidBox.Text = "N/A"
                              RevisionBox.Text = "N/A"
                              CPUIDBox.Text = "N/A"
                              LitBox.Text = "N/A"
                          End If
                      End Sub)
            '#--------------------------------------------------------------------------------------------------------------------'
            Dim expoItem As New SysteminfoRepository
            Await expoItem.SaveSystemInfoAsync(systemInfo)
            Me.Invoke(Sub()
                          LblStatusMessage.Text = "System information successfully read and saved to database!"
                          LblStatusMessage.ForeColor = Color.Green
                      End Sub)

        Catch ex As Exception
            Me.Invoke(Sub()
                          LblStatusMessage.Text = "Error: " & ex.Message
                          LblStatusMessage.ForeColor = Color.Red
                      End Sub)
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    '#--------------------------------------------------------------------------------------------------------------------'
    Private Sub ClearCpuDisplayControls()
        ModelBox.Text = ""
        FrequencyBox.Text = ""
        PlatformBox.Text = ""
        PowerBox2.Text = ""
        TDPBox.Text = ""
        LitBox.Text = ""
        RevisionBox.Text = ""
        CPUIDBox.Text = ""
        VidBox.Text = ""
        For Each kvp In LoadBoxes
            kvp.Value.Text = ""
        Next
        For Each kvp In CoreTempBoxes : kvp.Value.Text = "" : Next
        For Each kvp In MinTempBoxes : kvp.Value.Text = "" : Next
        For Each kvp In MaxTempBoxes : kvp.Value.Text = "" : Next
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub BtnToggleMonitor1_Click(sender As Object, e As EventArgs) Handles BtnToggleMonitor1.Click
        If Not isMonitoringActive Then
            isMonitoringActive = True
            backgroundTempMeasurements.Clear()
            cts = New CancellationTokenSource()
            'loadingForm = New Form3()
            'AddHandler loadingForm.StopRequested, AddressOf LoadingForm_StopRequested
            'loadingForm.Show()
            monitoringTask = Task.Run(Sub() RecordTemperaturesInBackground(cts.Token))
            ' refreshTimer.Stop()
            If monitoringTimer Is Nothing Then
                monitoringTimer = New Timer With {
                    .Interval = 1000 ' Jede Sekunde aktualisieren
                    }
                AddHandler monitoringTimer.Tick, AddressOf MonitoringTimer_Tick
            End If
            If monitoringForm Is Nothing OrElse monitoringForm.IsDisposed Then
                monitoringForm = New Form3()
                AddHandler monitoringForm.StopRequested, AddressOf MonitoringForm_StopRequested
            End If
            monitoringForm.Show()
            monitoringStopwatch.Restart() ' Startet oder setzt die Stopwatch zurück
            monitoringTimer.Start() ' Startet den Timer
            StartCpuStressTest()
        Else
            Call StopMonitoringProcess()
        End If
    End Sub
    Private Sub MonitoringForm_StopRequested(sender As Object, e As EventArgs)
        StopCpuStressTest()
        StopMonitoringProcess()
    End Sub

    Private Async Sub StopMonitoringProcess()
        If Not isMonitoringActive Then Exit Sub
        isMonitoringActive = False
        InfoMenuItem.Text = "Start Temp. Monitoring"
        LblStatusMessage.Text = "Background temperature monitoring stopped. Preparing chart..."
        LblStatusMessage.ForeColor = Color.DarkOrange
        If monitoringForm IsNot Nothing AndAlso Not monitoringForm.IsDisposed Then
            monitoringForm.Close()
            monitoringForm = Nothing
        End If
        cts?.Cancel()
        Try
            If monitoringTask IsNot Nothing Then
                Await monitoringTask
            End If
        Catch ex As OperationCanceledException
        Catch ex As Exception
            Debug.WriteLine($"Error awaiting monitoring task: {ex.Message}")
        End Try
        If backgroundTempMeasurements.Any() Then
            Dim savedFilePath As String = SaveTemperatureDataToCsv(backgroundTempMeasurements)
            If Not String.IsNullOrEmpty(savedFilePath) Then
                Dim chartForm As New Form2(savedFilePath)
                chartForm.Show()
            Else
                MessageBox.Show("Failed to save temperature data.", "Monitoring Stopped", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("No temperature data recorded.", "Monitoring Stopped", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        ' refreshTimer.Start()
        monitoringTimer.Stop()
        monitoringStopwatch.Stop()
        Me.Invoke(Sub() ReadAndDisplaySystemInfoAsync().Wait())
        Await ReadAndDisplaySystemInfoAsync()
        InitializePerCoreCounters()
        InitializeCoreTemperatureSensors()

        LblStatusMessage.Text = "Real-time monitoring started. Static info saved."
        LblStatusMessage.ForeColor = Color.Green
    End Sub

    Private Sub MonitoringTimer_Tick(sender As Object, e As EventArgs)
        If monitoringForm IsNot Nothing AndAlso Not monitoringForm.IsDisposed Then
            monitoringForm.UpdateElapsedTime(monitoringStopwatch.Elapsed)

        End If
    End Sub

    Private Sub LoadingForm_StopRequested(sender As Object, e As EventArgs)
        Call StopMonitoringProcess()
    End Sub

    Private Sub LoadArchivedMeasurementsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadArchivedMeasurementsToolStripMenuItem.Click
        Dim programDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim logDirectory As String = Path.Combine(programDirectory, "TemperatureLogs")

        If Not Directory.Exists(logDirectory) Then
            MessageBox.Show("Keine archivierten Messungen gefunden. Der Ordner 'TemperatureLogs' existiert nicht.", "Archiv leer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Using archiveSelectionForm As New Form4(logDirectory)
            Dim dialogResult As DialogResult = archiveSelectionForm.ShowDialog(Me)

            If dialogResult = DialogResult.OK Then
                Dim selectedFilePath As String = archiveSelectionForm.SelectedFilePath

                If Not String.IsNullOrEmpty(selectedFilePath) Then
                    Try
                        Dim chartForm As New Form2(selectedFilePath)
                        chartForm.Show()
                        LblStatusMessage.Text = $"Archivierte Messung '{Path.GetFileName(selectedFilePath)}' geladen."
                        LblStatusMessage.ForeColor = Color.DarkGreen
                    Catch ex As Exception
                        MessageBox.Show($"Fehler beim Laden der Messung: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        LblStatusMessage.Text = $"Fehler beim Laden der Messung: {ex.Message}"
                        LblStatusMessage.ForeColor = Color.Red
                    End Try
                Else
                    MessageBox.Show("Keine Datei ausgewählt.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                LblStatusMessage.Text = "Auswahl abgebrochen."
                LblStatusMessage.ForeColor = Color.Gray
            End If
        End Using
    End Sub

    Private Sub InfoMenuItem_Click(sender As Object, e As EventArgs) Handles InfoMenuItem.Click
        AboutBox1.ShowDialog(Me)
    End Sub


    Private Sub ExportCPUInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportCPUInfoToolStripMenuItem.Click

        Try
            Me.Invoke(Sub()
                          LblStatusMessage.Text = "Retrieving system information for export..."
                          LblStatusMessage.ForeColor = Color.Blue
                      End Sub)
            Dim exportItem As New SysteminfoRepository
            Dim latestReadings As List(Of SystemInfoData) = exportItem.GetLastSystemInfoReadingsAsync(1).Result

            If latestReadings Is Nothing OrElse latestReadings.Count = 0 Then
                Me.Invoke(Sub()
                              LblStatusMessage.Text = "No system information found to export."
                              LblStatusMessage.ForeColor = Color.OrangeRed
                          End Sub)
                MessageBox.Show("No system information found to export.", "Export Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim systemInfo As SystemInfoData = latestReadings(0)
            Dim htmlContent As String = GenerateSystemInfoHtml(systemInfo)

            Using saveFileDialog As New SaveFileDialog()
                saveFileDialog.Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*"
                saveFileDialog.Title = "Save System Information Report"
                saveFileDialog.FileName = $"SystemInfoReport_{DateTime.Now:yyyyMMdd_HHmmss}.html"

                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    Dim filePath As String = saveFileDialog.FileName
                    File.OpenWrite(Path.GetFullPath(htmlContent))

                    Me.Invoke(Sub()
                                  LblStatusMessage.Text = $"System information successfully exported to: {filePath}"
                                  LblStatusMessage.ForeColor = Color.Green
                              End Sub)
                    MessageBox.Show($"System information successfully exported to:{Environment.NewLine}{filePath}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Me.Invoke(Sub()
                                  LblStatusMessage.Text = "System information export cancelled."
                                  LblStatusMessage.ForeColor = Color.Gray
                              End Sub)
                End If
            End Using

        Catch ex As Exception
            Me.Invoke(Sub()
                          LblStatusMessage.Text = "Error during export: " & ex.Message
                          LblStatusMessage.ForeColor = Color.Red
                      End Sub)
            MessageBox.Show("An error occurred during export: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GenerateSystemInfoHtml(data As SystemInfoData) As String
        Dim htmlBuilder As New StringBuilder()

        htmlBuilder.AppendLine("<!DOCTYPE html>")
        htmlBuilder.AppendLine("<html lang='en'>")
        htmlBuilder.AppendLine("<head>")
        htmlBuilder.AppendLine("    <meta charset='UTF-8'>")
        htmlBuilder.AppendLine("    <meta name='viewport' content='width=device-width, initial-scale=1.0'>")
        htmlBuilder.AppendLine("    <title>System Information Report</title>")
        htmlBuilder.AppendLine("    <style>")
        htmlBuilder.AppendLine("        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 20px; background-color: #f4f7f6; color: #333; }")
        htmlBuilder.AppendLine("        .container { max-width: 900px; margin: 0 auto; background-color: #ffffff; padding: 30px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); }")
        htmlBuilder.AppendLine("        h1 { color: #2c3e50; text-align: center; margin-bottom: 30px; border-bottom: 2px solid #e0e0e0; padding-bottom: 10px; }")
        htmlBuilder.AppendLine("        h2 { color: #34495e; border-bottom: 1px solid #e0e0e0; padding-bottom: 5px; margin-top: 25px; margin-bottom: 15px; }")
        htmlBuilder.AppendLine("        table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }")
        htmlBuilder.AppendLine("        th, td { border: 1px solid #ddd; padding: 10px; text-align: left; }")
        htmlBuilder.AppendLine("        th { background-color: #f2f2f2; color: #555; width: 30%; }")
        htmlBuilder.AppendLine("        td { background-color: #fff; word-break: break-word; }") ' Added word-break
        htmlBuilder.AppendLine("        .note { font-size: 0.9em; color: #777; margin-top: 30px; text-align: center; }")
        htmlBuilder.AppendLine("        .value-list { list-style-type: none; padding: 0; margin: 0; }")
        htmlBuilder.AppendLine("        .value-list li { margin-bottom: 5px; }")
        htmlBuilder.AppendLine("    </style>")
        htmlBuilder.AppendLine("</head>")
        htmlBuilder.AppendLine("<body>")
        htmlBuilder.AppendLine("    <div class='container'>")
        htmlBuilder.AppendLine("        <h1>System Information Report</h1>")
        htmlBuilder.AppendLine("        <p><strong>Report Generated:</strong> " & DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") & "</p>")
        htmlBuilder.AppendLine("        <p><strong>Data Timestamp:</strong> " & data.Timestamp.ToString("dd-MM-yyyy HH:mm:ss") & "</p>")

        ' System Information
        htmlBuilder.AppendLine("        <h2>General System Information</h2>")
        htmlBuilder.AppendLine("        <table>")
        htmlBuilder.AppendLine("            <tr><th>Operating System</th><td>" & If(String.IsNullOrEmpty(data.OSSystem), "N/A", data.OSSystem) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>System Type</th><td>" & If(String.IsNullOrEmpty(data.SystemType), "N/A", data.SystemType) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Computer Name</th><td>" & If(String.IsNullOrEmpty(data.ComputerName), "N/A", data.ComputerName) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>User Name</th><td>" & If(String.IsNullOrEmpty(data.UserName), "N/A", data.UserName) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Domain Name</th><td>" & If(String.IsNullOrEmpty(data.DomainName), "N/A", data.DomainName) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Host Name</th><td>" & If(String.IsNullOrEmpty(data.HostName), "N/A", data.HostName) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>System Directory</th><td>" & If(String.IsNullOrEmpty(data.SystemDirectory), "N/A", data.SystemDirectory) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Program Files Directory</th><td>" & If(String.IsNullOrEmpty(data.ProgramDirectory), "N/A", data.ProgramDirectory) & "</td></tr>")
        htmlBuilder.AppendLine("        </table>")

        ' CPU Information
        htmlBuilder.AppendLine("        <h2>Processor (CPU) Information</h2>")
        htmlBuilder.AppendLine("        <table>")
        htmlBuilder.AppendLine("            <tr><th>CPU Name</th><td>" & If(String.IsNullOrEmpty(data.CpuName), "N/A", data.CpuName) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Processor Information</th><td>" & If(String.IsNullOrEmpty(data.ProcessorInformation), "N/A", data.ProcessorInformation) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Total Processors</th><td>" & data.ProcessorCount.ToString() & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Number of Cores</th><td>" & data.NumberOfCores.ToString() & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Logical Processors</th><td>" & data.NumberOfLogicalProcessors.ToString() & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Current Clock Speed</th><td>" & If(data.CurrentClockSpeedMHz > 0, data.CurrentClockSpeedMHz.ToString() & " MHz", "N/A") & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Architecture</th><td>" & If(String.IsNullOrEmpty(data.Architecture), "N/A", data.Architecture) & "</td></tr>")
        htmlBuilder.AppendLine("        </table>")

        ' Memory Information
        htmlBuilder.AppendLine("        <h2>Memory (RAM) Information</h2>")
        htmlBuilder.AppendLine("        <table>")
        htmlBuilder.AppendLine("            <tr><th>Total Physical Memory</th><td>" & If(String.IsNullOrEmpty(data.TotalPhysicalMemory), "N/A", data.TotalPhysicalMemory) & "</td></tr>")
        htmlBuilder.AppendLine("            <tr><th>Available Physical Memory</th><td>" & If(String.IsNullOrEmpty(data.AvailablePhysicalMemory), "N/A", data.AvailablePhysicalMemory) & "</td></tr>")
        htmlBuilder.AppendLine("        </table>")

        ' Graphics Card Information
        htmlBuilder.AppendLine("        <h2>Graphics Card Information</h2>")
        htmlBuilder.AppendLine("        <table>")
        htmlBuilder.AppendLine("            <tr><th>Graphics Card(s)</th><td>" & If(String.IsNullOrEmpty(data.GraphicsCardInformation), "N/A", data.GraphicsCardInformation.Replace(";", "<br>")) & "</td></tr>")
        htmlBuilder.AppendLine("        </table>")

        ' Network Information
        htmlBuilder.AppendLine("        <h2>Network Information</h2>")
        htmlBuilder.AppendLine("        <table>")
        htmlBuilder.AppendLine("            <tr><th>IP Addresses</th><td>")
        If Not String.IsNullOrEmpty(data.IPAddresses) Then
            htmlBuilder.AppendLine("                <ul class='value-list'>")
            For Each ip As String In data.IPAddresses.Split(";"c)
                htmlBuilder.AppendLine($"                    <li>{ip.Trim()}</li>")
            Next
            htmlBuilder.AppendLine("                </ul>")
        Else
            htmlBuilder.AppendLine("                N/A")
        End If
        htmlBuilder.AppendLine("            </td></tr>")

        htmlBuilder.AppendLine("            <tr><th>Network Adapters</th><td>")
        If Not String.IsNullOrEmpty(data.NetworkAdapterNames) Then
            htmlBuilder.AppendLine("                <ul class='value-list'>")
            For Each adapterName As String In data.NetworkAdapterNames.Split(";"c)
                htmlBuilder.AppendLine($"                    <li>{adapterName.Trim()}</li>")
            Next
            htmlBuilder.AppendLine("                </ul>")
        Else
            htmlBuilder.AppendLine("                N/A")
        End If
        htmlBuilder.AppendLine("            </td></tr>")

        htmlBuilder.AppendLine("            <tr><th>MAC Addresses</th><td>")
        If Not String.IsNullOrEmpty(data.NetworkAdapterMacAddresses) Then
            htmlBuilder.AppendLine("                <ul class='value-list'>")
            For Each macAddress As String In data.NetworkAdapterMacAddresses.Split(";"c)
                htmlBuilder.AppendLine($"                    <li>{macAddress.Trim()}</li>")
            Next
            htmlBuilder.AppendLine("                </ul>")
        Else
            htmlBuilder.AppendLine("                N/A")
        End If
        htmlBuilder.AppendLine("            </td></tr>")
        htmlBuilder.AppendLine("        </table>")

        ' BIOS Information
        htmlBuilder.AppendLine("        <h2>BIOS Information</h2>")
        htmlBuilder.AppendLine("        <table>")
        htmlBuilder.AppendLine("            <tr><th>BIOS Version</th><td>" & If(String.IsNullOrEmpty(data.BIOSVersion), "N/A", data.BIOSVersion) & "</td></tr>")
        htmlBuilder.AppendLine("        </table>")


        htmlBuilder.AppendLine("        <p class='note'>Report generated by System Information Tool.</p>")
        htmlBuilder.AppendLine("    </div>")
        htmlBuilder.AppendLine("</body>")
        htmlBuilder.AppendLine("</html>")

        Return htmlBuilder.ToString()
    End Function


End Class