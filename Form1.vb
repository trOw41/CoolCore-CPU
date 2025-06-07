Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports Timer = System.Windows.Forms.Timer
Imports System.Globalization
Imports System.Reflection
Imports Microsoft.VisualBasic.Logging
Imports OpenHardwareMonitor.Hardware
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices

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
    Private logDir As String = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory & "log")
    Private ReadOnly LogFilePath As String = Path.Combine(logDir, "CoolCore_TemperatureLog1.txt")
    Private temperatureLogWriter As StreamWriter
    Private isLoggingActive As Boolean = False
    Private allParsedLogEntries As Object
    Private LogSize As Long = Settings.MAX_LOG_SIZE_KB
    Public Sub New()
        InitializeComponent()
        systemInfoRepository = New SystemInfoRepository()
        cpuLoadCounter = New PerformanceCounter("Processor", "% Processor Time", "_Total")
        refreshTimer = New Timer With {
            .Interval = 1000
            }
        If Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log")) = False Then
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log"))
            File.OpenWrite(Path.Combine(logDir, "CoolCore_TemperatureLog1.txt")).Close()
            MessageBox.Show("Log-Verzeichnis wurde erstellt." & logDir, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
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

        StopCpuStressTest()
        cancellationTokenSource = New CancellationTokenSource()
        Dim cancellationToken = cancellationTokenSource.Token
        Dim processorCount As Integer = Environment.ProcessorCount
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
            cancellationTokenSource.Cancel()
            Try
                Task.WaitAll(stressTasks.ToArray(), 5000)
            Catch ex As AggregateException
                For Each innerEx In ex.InnerExceptions
                    Debug.WriteLine($"Fehler beim Beenden der Stress-Task: {innerEx.Message}")
                Next
            Catch ex As Exception
                Debug.WriteLine($"Fehler beim Warten auf Stress-Tasks: {ex.Message}")
            End Try
            cancellationTokenSource.Dispose() '
            cancellationTokenSource = Nothing
        End If
        stressTasks.Clear()
        Me.Invoke(Sub()
                      LblStatusMessage.Text = "CPU-Stresstest beendet."
                      'LblStatusMessage.ForeColor = Color.Green
                  End Sub)
    End Sub

    Private Sub RecordTemperaturesInBackground(cancellationToken As CancellationToken)
        Dim intervalMs As Integer = 1000

        Do While Not cancellationToken.IsCancellationRequested
            Try
                cpu?.Update()
                Dim currentCoreTemps As New Dictionary(Of String, Single)()
                For Each sensor As ISensor In coreTemperatures
                    If sensor.Name.StartsWith("CPU Core #", StringComparison.OrdinalIgnoreCase) AndAlso sensor.Value.HasValue Then
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
                          'LblStatusMessage.ForeColor = Color.Blue
                      End Sub)
            Return filePath
        Catch ex As Exception
            Me.Invoke(Sub()
                          LblStatusMessage.Text = $"Error saving data: {ex.Message}"
                          'LblStatusMessage.ForeColor = Color.Red
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
        LblStatusMessage.ForeColor = Color.Black
        Me.Text = "CoolCore - Monitoring Tool" & " - " & My.Application.Info.Version.ToString(4)
        ApplyTheme(My.Settings.ApplicationTheme)
        InitializePerCoreCounters()
        InitializeCoreTemperatureSensors()
        'InitializeOpenHardwareMonitor()
        InitializeVoltageSensors()
        Using ReadAndDisplaySystemInfoAsync()
            refreshTimer.Start()
        End Using

        StartStopLog()

    End Sub

    Public Sub StartStopLog()
        Dim Start As Boolean = Settings.LogStartStop
        If Start = True Then
            Me.Invoke(Sub()
                          StartLog()
                          CheckAndManageLogFile()
                      End Sub)
        ElseIf Start = False Then
            If isLoggingActive Then
                StopLog()
                LblStatusMessage.Text = "Logging stopped."
            End If
        End If
    End Sub
    Public Sub UpdateLogSize()
        Dim logSizeKB As Integer = My.Settings.MAX_LOG_SIZE_KB
        LogSize = logSizeKB
        LblStatusMessage.Text = $"Max. Loggröße: {logSizeKB} KB"
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
        If isLoggingActive AndAlso temperatureLogWriter IsNot Nothing Then
            Try
                temperatureLogWriter.WriteLine($"--- CoolCore Temperatur-Log beendet: {DateTime.Now} (Anwendung geschlossen) ---")
                temperatureLogWriter.Close()
                temperatureLogWriter.Dispose()
                temperatureLogWriter = Nothing
                Debug.WriteLine("Temperatur-Logging beim Beenden der Anwendung beendet.")
            Catch ex As Exception
                Debug.WriteLine($"Fehler beim Beenden des Loggings während des Form-Schließens: {ex.Message}")
            End Try
        End If
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
            If computer Is Nothing Then
                computer = New Computer()
                computer.Open(True)
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
                    If sensor.Name.StartsWith("CPU Core #", StringComparison.OrdinalIgnoreCase) Then
                        Dim namePart As String = sensor.Name.Replace("CPU Core #", "").Trim()
                        Dim coreNum As Integer
                        If Integer.TryParse(namePart.Split(" "c)(0), coreNum) Then

                            If coreNum <= VoltBoxes.Count Then
                                cpuVoltageSensorMap.Add(coreNum, sensor)

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


                If MinTempBoxes.ContainsKey(coreIndex) Then
                    MinTempBoxes(coreIndex).Text = $"{sensor.Min:F1}°C"
                    MinTempBoxes(coreIndex).ForeColor = Color.Green
                End If

                If MaxTempBoxes.ContainsKey(coreIndex) Then
                    MaxTempBoxes(coreIndex).Text = $"{sensor.Max:F1}°C"
                    MaxTempBoxes(coreIndex).ForeColor = Color.OrangeRed
                End If

                If CoreTempBoxes.ContainsKey(coreIndex) Then
                    CoreTempBoxes(coreIndex).Text = $"{sensor.Value:F1}°C"
                    CoreTempBoxes(coreIndex).ForeColor = GetTemperatureColor(sensor.Value)
                End If
                Me.Invoke(Sub()
                              If MinTempBoxes.ContainsKey(coreIndex) Then
                                  MinTempBoxes(coreIndex).Text = $"{sensor.Min:F1}°C"
                                  MinTempBoxes(coreIndex).ForeColor = Color.Green
                              End If
                              If MaxTempBoxes.ContainsKey(coreIndex) Then
                                  MaxTempBoxes(coreIndex).Text = $"{sensor.Max:F1}°C"
                                  MaxTempBoxes(coreIndex).ForeColor = GetTemperatureColor(sensor.Max)
                              End If
                              If CoreTempBoxes.ContainsKey(coreIndex) Then
                                  CoreTempBoxes(coreIndex).Text = $"{sensor.Value:F1}°C"
                                  CoreTempBoxes(coreIndex).ForeColor = GetTemperatureColor(sensor.Value)
                              End If
                          End Sub)
                If isLoggingActive AndAlso temperatureLogWriter IsNot Nothing Then
                    Try
                        ' Daten in CSV-Format schreiben
                        temperatureLogWriter.WriteLine(
                            $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")};" &
                            $"Core {coreIndex};" &
                            $"{sensor.Min:F1};" &
                            $"{sensor.Max:F1};" &
                            $"{sensor.Value:F1}")

                        temperatureLogWriter.Flush()
                    Catch ex As Exception
                        Debug.WriteLine($"Fehler beim Schreiben ins Temperatur-Log: {ex.Message}")

                    End Try
                End If
            Next
            If monitoringStopwatch.Elapsed.TotalSeconds >= My.Settings.MonitorTime Then
                StopMonitoringProcess()
                StopCpuStressTest()
                'Exit Sub
            End If
            CheckAndManageLogFile()
        Catch ex As Exception
            For Each kvp In LoadBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In CoreTempBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In MinTempBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In MaxTempBoxes : kvp.Value.Text = "Error" : Next
        End Try
    End Sub

    Private Function ReadAndDisplaySystemInfoAsync() As Task
        LblStatusMessage.Text = "Reading system information and saving to database..."
        'LblStatusMessage.ForeColor = Color.CadetBlue
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
            Me.Invoke(Sub()
                          LblStatusMessage.Text = "System information successfully read and saved to database!"
                          'LblStatusMessage.ForeColor = Color.Green
                      End Sub)

        Catch ex As Exception
            Me.Invoke(Sub()
                          LblStatusMessage.Text = "Error: " & ex.Message
                          'LblStatusMessage.ForeColor = Color.Red
                      End Sub)
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return Task.CompletedTask
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
            monitoringTask = Task.Run(Sub() RecordTemperaturesInBackground(cts.Token))

            If monitoringTimer Is Nothing Then
                monitoringTimer = New Timer With {
                    .Interval = 1000
                    }
                AddHandler monitoringTimer.Tick, AddressOf MonitoringTimer_Tick
            End If
            If monitoringForm Is Nothing OrElse monitoringForm.IsDisposed Then
                monitoringForm = New Form3()
                AddHandler monitoringForm.StopRequested, AddressOf MonitoringForm_StopRequested
            End If
            monitoringForm.Show()
            monitoringStopwatch.Restart()
            monitoringTimer.Start()
            StartCpuStressTest()
            LblStatusMessage.Text = "Background temperature monitoring started. CPU stress test running..."
        Else

        End If
    End Sub
    Private Sub MonitoringForm_StopRequested(sender As Object, e As EventArgs)
        StopCpuStressTest()
        StopMonitoringProcess()
    End Sub

    Private Async Sub StopMonitoringProcess()
        If Not isMonitoringActive Then Exit Sub
        isMonitoringActive = False
        LblStatusMessage.Text = "Background temperature monitoring stopped. Preparing chart..."
        'LblStatusMessage.ForeColor = Color.DarkOrange
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
        'LblStatusMessage.ForeColor = Color.Green
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
                        'LblStatusMessage.ForeColor = Color.DarkGreen
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
                'LblStatusMessage.ForeColor = Color.Gray
            End If
        End Using
    End Sub

    Private Sub InfoMenuItem_Click(sender As Object, e As EventArgs) Handles InfoMenuItem.Click
        AboutBox1.ShowDialog(Me)
    End Sub


    Private Sub ExportCPUInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportCPUInfoToolStripMenuItem.Click
        Dim currentSystemInfo As SystemInfoData = systemInfoRepository.GetCurrentSystemInfo()
        If currentSystemInfo Is Nothing Then
            Exit Sub
        End If
        systemInfoRepository.SaveSystemInfo(currentSystemInfo)
        Dim htmlContent As String = GenerateSystemInfoHtml(currentSystemInfo)
        Using saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "HTML-Datei (*.html)|*.html|Alle Dateien (*.*)|*.*"
            saveFileDialog.Title = "Systeminformationen exportieren"
            saveFileDialog.FileName = $"SystemInfo_{DateTime.Now:yyyyMMdd_HHmmss}.html"
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    File.WriteAllText(saveFileDialog.FileName, htmlContent, System.Text.Encoding.UTF8)
                    MessageBox.Show("Systeminformationen erfolgreich exportiert.", "Export abgeschlossen", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"Fehler beim Speichern der HTML-Datei: {ex.Message}", "Exportfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub
    Private Function GenerateSystemInfoHtml(data As SystemInfoData) As String
        Dim htmlBuilder As New System.Text.StringBuilder()
        Try
            htmlBuilder.AppendLine("<!DOCTYPE html>")
            htmlBuilder.AppendLine("<html lang='de'>")
            htmlBuilder.AppendLine("<head>")
            htmlBuilder.AppendLine("<meta charset='UTF-8'>")
            htmlBuilder.AppendLine("<meta name='viewport' content='width=device-width, initial-scale=1.0'>")
            htmlBuilder.AppendLine("<title>CPU Info Bericht</title>")
            htmlBuilder.AppendLine("<style>")
            htmlBuilder.AppendLine("  body { font-family: Arial, sans-serif; margin: 20px; background-color: #f4f4f4; color: #333; }")
            htmlBuilder.AppendLine("  .container { background-color: #fff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0,0,0,0.1); max-width: 900px; margin: auto; }")
            htmlBuilder.AppendLine("  h1 { color: #0056b3; text-align: center; }")
            htmlBuilder.AppendLine("  h2 { color: #007bff; border-bottom: 1px solid #eee; padding-bottom: 5px; margin-top: 20px; }")
            htmlBuilder.AppendLine("  table { width: 100%; border-collapse: collapse; margin-top: 10px; }")
            htmlBuilder.AppendLine("  th, td { padding: 8px; border: 1px solid #ddd; text-align: left; vertical-align: top; }")
            htmlBuilder.AppendLine("  th { background-color: #f2f2f2; font-weight: bold; width: 30%; }")
            htmlBuilder.AppendLine("  .note { margin-top: 30px; font-size: 0.9em; color: #777; text-align: center; }")
            htmlBuilder.AppendLine("  .value-list { list-style-type: disc; margin: 0; padding-left: 20px; }")
            htmlBuilder.AppendLine("  .value-list li { margin-bottom: 3px; }")
            htmlBuilder.AppendLine("</style>")
            htmlBuilder.AppendLine("</head>")
            htmlBuilder.AppendLine("<body>")
            htmlBuilder.AppendLine("  <div class='container'>")
            htmlBuilder.AppendLine("    <h1>Systeminformationen Bericht</h1>")
            htmlBuilder.AppendLine($"    <p class='note'>Bericht erstellt am: {data.Timestamp:dd.MM.yyyy HH:mm:ss}</p>")

            htmlBuilder.AppendLine("    <h2>Host Info</h2>")
            htmlBuilder.AppendLine("    <table>")
            'htmlBuilder.AppendLine($"      <tr><th>Computername</th><td>{data.ComputerName}</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>Hostname</th><td>{data.HostName}</td></tr>")
            'htmlBuilder.AppendLine($"      <tr><th>Benutzername</th><td>{data.UserName}</td></tr>")
            'htmlBuilder.AppendLine($"      <tr><th>Domain</th><td>{data.DomainName}</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>Betriebssystem</th><td>{data.OSSystem} ({data.Architecture})</td></tr>")
            'htmlBuilder.AppendLine($"      <tr><th>Systemtyp</th><td>{data.SystemType}</td></tr>")
            'htmlBuilder.AppendLine($"      <tr><th>Systemverzeichnis</th><td>{data.SystemDirectory}</td></tr>")
            'htmlBuilder.AppendLine($"      <tr><th>Programmverzeichnis</th><td>{data.ProgramDirectory}</td></tr>")
            htmlBuilder.AppendLine("    </table>")

            htmlBuilder.AppendLine("    <h2>CPU Informationen</h2>")
            htmlBuilder.AppendLine("    <table>")
            htmlBuilder.AppendLine($"      <tr><th>CPU Name</th><td>{data.CpuName}</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>Anzahl Kerne</th><td>{data.NumberOfCores}</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>Anzahl logischer Prozessoren</th><td>{data.NumberOfLogicalProcessors}</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>Prozessorzähler (Threads)</th><td>{data.ProcessorCount}</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>Aktuelle Taktrate</th><td>{data.CurrentClockSpeedMHz} MHz</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>Prozessorinformationen</th><td>{data.ProcessorInformation}</td></tr>")
            'htmlBuilder.AppendLine($"      <tr><th>Gesamter physischer Speicher</th><td>{(data.TotalPhysicalMemory / (1024.0 * 1024 * 1024)):F2} GB</td></tr>")
            'htmlBuilder.AppendLine($"      <tr><th>Verfügbarer physischer Speicher</th><td>{(data.AvailablePhysicalMemory / (1024.0 * 1024 * 1024)):F2} GB</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>Grafikkarten Informationen</th><td>{data.GraphicsCardInformation}</td></tr>")
            htmlBuilder.AppendLine($"      <tr><th>BIOS Version</th><td>{data.BIOSVersion}</td></tr>")
            htmlBuilder.AppendLine("    </table>")

            htmlBuilder.AppendLine("    <h2>Netzwerk Informationen</h2>")
            htmlBuilder.AppendLine("    <table>")
            htmlBuilder.AppendLine("      <tr><th>IP-Adressen</th><td>")
            If Not String.IsNullOrEmpty(data.IPAddresses) Then
                htmlBuilder.AppendLine("        <ul class='value-list'>")
                For Each ipAddress As String In data.IPAddresses.Split(";"c)
                    htmlBuilder.AppendLine($"          <li>{ipAddress.Trim()}</li>")
                Next
                htmlBuilder.AppendLine("        </ul>")
            Else
                htmlBuilder.AppendLine("        N/A")
            End If
            htmlBuilder.AppendLine("      </td></tr>")

            htmlBuilder.AppendLine("      <tr><th>Netzwerkadapter Namen</th><td>")
            If Not String.IsNullOrEmpty(data.NetworkAdapterNames) Then
                htmlBuilder.AppendLine("        <ul class='value-list'>")
                For Each adapterName As String In data.NetworkAdapterNames.Split(";"c)
                    htmlBuilder.AppendLine($"          <li>{adapterName.Trim()}</li>")
                Next
                htmlBuilder.AppendLine("        </ul>")
            Else
                htmlBuilder.AppendLine("        N/A")
            End If
            htmlBuilder.AppendLine("      </td></tr>")

            htmlBuilder.AppendLine("      <tr><th>MAC-Adressen</th><td>")
            If Not String.IsNullOrEmpty(data.NetworkAdapterMacAddresses) Then
                htmlBuilder.AppendLine("        <ul class='value-list'>")
                For Each macAddress As String In data.NetworkAdapterMacAddresses.Split(";"c)
                    htmlBuilder.AppendLine($"          <li>{macAddress.Trim()}</li>")
                Next
                htmlBuilder.AppendLine("        </ul>")
            Else
                htmlBuilder.AppendLine("        N/A")
            End If
            htmlBuilder.AppendLine("      </td></tr>")
            htmlBuilder.AppendLine("    </table>")


            htmlBuilder.AppendLine("    <p class='note'>Bericht erstellt von CoolCore Tool.</p>")
            htmlBuilder.AppendLine("  </div>")
            htmlBuilder.AppendLine("</body>")
            htmlBuilder.AppendLine("</html>")

            Return htmlBuilder.ToString()
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Generieren des HTML-Berichts: {ex.Message}", "HTML-Generierungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"HTML Generation Error: {ex.Message}")
            Return "<h1>Fehler beim Generieren der Systeminformationen.</h1>"
        End Try
    End Function

    Private Sub ApplyTheme(theme As String)
        Select Case theme
            Case "Dark"
                ' Apply Dark Theme
                ' Apply Dark Theme
                Me.BackColor = ColorTranslator.FromHtml("#282C34")
                Me.ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next

                ' Apply to menu strip if you have one
                If Me.MainMenuStrip IsNot Nothing Then
                    Me.MainMenuStrip.BackColor = Color.FromArgb(50, 50, 53)
                    Me.MainMenuStrip.ForeColor = Color.White
                    For Each item As ToolStripItem In Me.MainMenuStrip.Items
                        ApplyThemeToToolStripItem(item, theme)
                    Next
                End If
                PictureBox1.Image = My.Resources._024_cpu
                Me.Icon = My.Resources._024_cpu_1
            Case "Standard"
                ' Apply Standard/Light Theme
                Me.BackColor = ColorTranslator.FromHtml("#F0F0F0") ' Sehr helles Grau
                Me.ForeColor = ColorTranslator.FromHtml("#333333") ' Dunkles Grau
                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next

                If Me.MainMenuStrip IsNot Nothing Then
                    Me.MainMenuStrip.BackColor = SystemColors.Control
                    Me.MainMenuStrip.ForeColor = SystemColors.ControlText
                    For Each item As ToolStripItem In Me.MainMenuStrip.Items
                        ApplyThemeToToolStripItem(item, theme)
                    Next
                End If
                PictureBox1.Image = My.Resources._023_cpu
                Me.Icon = My.Resources._023_cpu_1
        End Select
    End Sub
    Private Sub ApplyThemeToControl(ctrl As Control, theme As String)
        Select Case theme
            Case "Dark"
                If TypeOf ctrl Is Button Then
                    CType(ctrl, Button).BackColor = ColorTranslator.FromHtml("#3B4048")
                    CType(ctrl, Button).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                    CType(ctrl, Button).FlatStyle = FlatStyle.Flat
                    CType(ctrl, Button).FlatAppearance.BorderColor = ColorTranslator.FromHtml("#4A5059")
                    CType(ctrl, Button).FlatAppearance.BorderSize = 1
                    ' Optional: Add MouseHover/MouseLeave events for hover effect
                ElseIf TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).BackColor = ColorTranslator.FromHtml("#3B4048")
                    CType(ctrl, TextBox).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                    CType(ctrl, TextBox).BorderStyle = BorderStyle.FixedSingle
                ElseIf TypeOf ctrl Is Label Then
                    CType(ctrl, Label).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                ElseIf TypeOf ctrl Is CheckBox Then
                    CType(ctrl, CheckBox).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                ElseIf TypeOf ctrl Is GroupBox Then
                    CType(ctrl, GroupBox).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                    CType(ctrl, GroupBox).BackColor = ColorTranslator.FromHtml("#282C34")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                ElseIf TypeOf ctrl Is Panel Then
                    CType(ctrl, Panel).BackColor = ColorTranslator.FromHtml("#282C34")
                    CType(ctrl, Panel).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                End If
            Case "Standard"
                If TypeOf ctrl Is Button Then
                    CType(ctrl, Button).BackColor = ColorTranslator.FromHtml("#E1E1E1")
                    CType(ctrl, Button).ForeColor = ColorTranslator.FromHtml("#333333")
                    CType(ctrl, Button).FlatStyle = FlatStyle.Flat
                    CType(ctrl, Button).FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CCCCCC")
                    CType(ctrl, Button).FlatAppearance.BorderSize = 1
                    ' Optional: Add MouseHover/MouseLeave events for hover effect
                ElseIf TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).BackColor = Color.White
                    CType(ctrl, TextBox).ForeColor = ColorTranslator.FromHtml("#333333")
                    CType(ctrl, TextBox).BorderStyle = BorderStyle.FixedSingle
                ElseIf TypeOf ctrl Is Label Then
                    CType(ctrl, Label).ForeColor = ColorTranslator.FromHtml("#333333")
                ElseIf TypeOf ctrl Is CheckBox Then
                    CType(ctrl, CheckBox).ForeColor = ColorTranslator.FromHtml("#333333")
                ElseIf TypeOf ctrl Is GroupBox Then
                    CType(ctrl, GroupBox).ForeColor = ColorTranslator.FromHtml("#333333")
                    CType(ctrl, GroupBox).BackColor = ColorTranslator.FromHtml("#F0F0F0") ' Hintergrund wie Form
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                ElseIf TypeOf ctrl Is Panel Then
                    CType(ctrl, Panel).BackColor = ColorTranslator.FromHtml("#F0F0F0") ' Hintergrund wie Form
                    CType(ctrl, Panel).ForeColor = ColorTranslator.FromHtml("#333333")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                End If
        End Select
    End Sub

    Private Sub ApplyThemeToToolStripItem(item As ToolStripItem, theme As String)
        Select Case theme
            Case "Dark"
                item.BackColor = Color.DarkGray
                item.ForeColor = SystemColors.WindowText
                If TypeOf item Is ToolStripDropDownItem Then
                    Dim dropDownItem As ToolStripDropDownItem = CType(item, ToolStripDropDownItem)
                    For Each subItem As ToolStripItem In dropDownItem.DropDownItems
                        ApplyThemeToToolStripItem(subItem, theme)
                    Next
                End If
            Case "Standard"
                item.BackColor = SystemColors.Control
                item.ForeColor = SystemColors.ControlText
                If TypeOf item Is ToolStripDropDownItem Then
                    Dim dropDownItem As ToolStripDropDownItem = CType(item, ToolStripDropDownItem)
                    For Each subItem As ToolStripItem In dropDownItem.DropDownItems
                        ApplyThemeToToolStripItem(subItem, theme)
                    Next
                End If
        End Select
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Using optionsForm As New OptionsForm()
            AddHandler optionsForm.ThemeChanged, AddressOf OptionsForm_ThemeChanged
            If optionsForm.ShowDialog() = DialogResult.OK Then

            End If
        End Using
    End Sub
    Private Sub OptionsForm_ThemeChanged(sender As Object, newTheme As String)
        ApplyTheme(newTheme)
    End Sub

    Private Function GetTemperatureColor(temperature As Single) As Color
        Const YELLOW_THRESHOLD As Single = 55.0F
        Const ORANGE_THRESHOLD As Single = 70.0F
        Const RED_THRESHOLD As Single = 90.0F
        Try
            If temperature <= YELLOW_THRESHOLD Then
                Return Color.Green
            ElseIf temperature > YELLOW_THRESHOLD Then
                Return Color.Orange
            ElseIf temperature > ORANGE_THRESHOLD Then
                Return Color.OrangeRed
            ElseIf temperature > RED_THRESHOLD Then
                Return Color.Red
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error in GetTemperatureColor: {ex.Message}")
            Return Color.Black
        End Try
    End Function

    Private Sub FAQToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FAQToolStripMenuItem.Click
        FAQForm.Show()
    End Sub

    Private Sub SupportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupportToolStripMenuItem.Click
        Try
            Dim supportPagePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Support\support.html")

            If File.Exists(supportPagePath) Then
                Process.Start(supportPagePath)
            Else
                MessageBox.Show("Die Supportseite konnte nicht gefunden werden: " & supportPagePath, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Öffnen der Supportseite: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub StartLog()
        Try
            If temperatureLogWriter IsNot Nothing Then
                temperatureLogWriter.Close()
                temperatureLogWriter.Dispose()
                temperatureLogWriter = Nothing
            End If
            temperatureLogWriter = New StreamWriter(LogFilePath, append:=True)
            temperatureLogWriter.WriteLine($"--- CoolCore Temperatur-Log gestartet: {DateTime.Now} ---")
            temperatureLogWriter.WriteLine("Zeitpunkt ; CPU-Core ; MinTemp ;MaxTemp ; CurrentTemp")
            isLoggingActive = True
            LblStatusMessage.Text = "Temperatur-Logging wurde gestartet. Daten werden in 'CoolCore_TemperatureLog.txt' geschrieben."
            Debug.WriteLine("Temperatur-Logging gestartet.")
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Starten des Loggings: {ex.Message}", "Logging Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Logging Start Error: {ex.Message}")
            isLoggingActive = False
        End Try
    End Sub

    Private Sub StopLog()
        Try
            If temperatureLogWriter IsNot Nothing Then
                temperatureLogWriter.WriteLine($"--- CoolCore Temperatur-Log beendet: {DateTime.Now} ---")
                temperatureLogWriter.Close()
                temperatureLogWriter.Dispose()
                temperatureLogWriter = Nothing
            End If
            isLoggingActive = False
            LblStatusMessage.Text = "Temperatur-Logging wurde beendet."
            Debug.WriteLine("Temperatur-Logging beendet.")
            ExportLogToolStripMenuItem.Enabled = True
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Beenden des Loggings: {ex.Message}", "Logging Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Logging Stop Error: {ex.Message}")
            isLoggingActive = False
        End Try
    End Sub
    Private Structure LogEntry
        Public Property Timestamp As DateTime
        Public Property Core As String
        Public Property MinTemp As Single
        Public Property MaxTemp As Single
        Public Property CurrentTemp As Single
        Public Property CpuName As String
    End Structure
    Private Sub ExportLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportLogToolStripMenuItem.Click
        Dim programDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim logDirectory As String = logDir

        If Not Directory.Exists(logDirectory) Then
            MessageBox.Show("Keine archivierten Messungen gefunden. Der Ordner 'TemperatureLogs' existiert nicht.", "Archiv leer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Using archiveSelectionForm As New Form5(logDirectory)
            Dim dialogResult As DialogResult = archiveSelectionForm.ShowDialog(Me)

            If dialogResult = DialogResult.OK Then
                Dim selectedFilePath As String = archiveSelectionForm.SelectedFilePath

                If Not String.IsNullOrEmpty(selectedFilePath) Then
                    Try
                        ExportLog(selectedFilePath)
                        LblStatusMessage.Text = $"Archivierte Messung '{Path.GetFileName(selectedFilePath)}' geladen."
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
            End If
        End Using
    End Sub

    Private Sub ExportLog(LogFilePath As String)
        If Not File.Exists(LogFilePath) Then
            MessageBox.Show("Die Temperatur-Logdatei wurde nicht gefunden. Bitte starten Sie das Logging zuerst.", "Export Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        StopLog()
        Try
            Dim logLines As List(Of String) = File.ReadAllLines(LogFilePath).ToList()
            Dim parsedLogEntries As New List(Of LogEntry)()
            Dim currentCpuName As String = "Unbekannt"
            Try
                Using searcher As New ManagementObjectSearcher("SELECT Name FROM Win32_Processor")
                    For Each mo As ManagementObject In searcher.Get()
                        currentCpuName = mo("Name")?.ToString()
                        Exit For
                    Next
                End Using
            Catch ex As Exception
                Debug.WriteLine($"Could not get CPU Name for report: {ex.Message}")
                currentCpuName = "Unbekannt"
            End Try
            For Each line As String In logLines

                If line.StartsWith("--- CoolCore Temperatur-Log") OrElse line.StartsWith("Zeitpunkt;CPU-Core") Then
                    Continue For
                End If
                Dim parts() As String = line.Split(";"c)
                If parts.Length = 5 Then
                    Dim timestamp As DateTime
                    Dim core As String = parts(1).Trim()
                    Dim minTemp As Single
                    Dim maxTemp As Single
                    Dim currentTemp As Single
                    If DateTime.TryParseExact(parts(0).Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, timestamp) AndAlso
                   Single.TryParse(parts(2).Trim().Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, minTemp) AndAlso
                   Single.TryParse(parts(3).Trim().Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, maxTemp) AndAlso
                   Single.TryParse(parts(4).Trim().Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, currentTemp) Then
                        parsedLogEntries.Add(New LogEntry With {
                        .Timestamp = timestamp,
                        .Core = core,
                        .MinTemp = minTemp,
                        .MaxTemp = maxTemp,
                        .CurrentTemp = currentTemp,
                        .CpuName = currentCpuName
                    })
                    End If
                End If
            Next
            Dim filteredLogEntries As New List(Of LogEntry)()
            Dim lastFilteredTimestampPerCore As New Dictionary(Of String, DateTime)()
            For Each entry As LogEntry In parsedLogEntries
                Dim coreKey As String = entry.Core
                Dim roundedTimestamp As DateTime = entry.Timestamp
                roundedTimestamp = New DateTime(roundedTimestamp.Year, roundedTimestamp.Month, roundedTimestamp.Day,
                                            roundedTimestamp.Hour, roundedTimestamp.Minute, (roundedTimestamp.Second \ 10) * 10,
                                            roundedTimestamp.Kind)
                If Not lastFilteredTimestampPerCore.ContainsKey(coreKey) OrElse (roundedTimestamp - lastFilteredTimestampPerCore(coreKey)).TotalSeconds >= 10 Then
                    filteredLogEntries.Add(entry)
                    lastFilteredTimestampPerCore(coreKey) = roundedTimestamp
                    Debug.WriteLine($"Added entry to filtered (10s interval): {entry.Timestamp} - {entry.Core}")
                Else
                    Debug.WriteLine($"Skipped entry (already logged this 10s interval for core {coreKey}): {entry.Timestamp} - {entry.Core}")
                End If
            Next
            Debug.WriteLine($"Filtered down to {filteredLogEntries.Count} entries.")
            Dim jsonBuilder As New StringBuilder()
            jsonBuilder.Append("[")
            For i As Integer = 0 To parsedLogEntries.Count - 1
                Dim entry = parsedLogEntries(i)
                jsonBuilder.Append("{")
                jsonBuilder.Append(String.Format("""Timestamp"": ""{0}"",", entry.Timestamp.ToString("o")))
                jsonBuilder.Append(String.Format("""Core"": ""{0}"",", entry.Core))
                jsonBuilder.Append(String.Format("""MinTemp"": {0},", entry.MinTemp.ToString(CultureInfo.InvariantCulture)))
                jsonBuilder.Append(String.Format("""MaxTemp"": {0},", entry.MaxTemp.ToString(CultureInfo.InvariantCulture)))
                jsonBuilder.Append(String.Format("""CurrentTemp"": {0},", entry.CurrentTemp.ToString(CultureInfo.InvariantCulture)))
                jsonBuilder.Append(String.Format("""CpuName"": ""{0}""", entry.CpuName))
                jsonBuilder.Append("}")
                If i < parsedLogEntries.Count - 1 Then
                    jsonBuilder.Append(",")
                End If
            Next
            jsonBuilder.Append("]")
            Dim jsonData As String = jsonBuilder.ToString()
            Dim templatePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemperatureReportTemplate.html")
            If Not File.Exists(templatePath) Then
                MessageBox.Show("Die HTML-Vorlagendatei 'TemperatureReportTemplate.html' wurde nicht gefunden. Bitte stellen Sie sicher, dass sie im Anwendungsverzeichnis liegt.", "Export Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            Dim htmlContent As String = File.ReadAllText(templatePath)
            htmlContent = htmlContent.Replace("{{LOG_DATA_PLACEHOLDER}}", jsonData)
            Dim reportFileName As String = "CoolCore_TemperatureReport.html"
            Dim reportPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, reportFileName)
            File.WriteAllText(reportPath, htmlContent, Encoding.UTF8)
            If File.Exists(reportPath) Then
                Debug.WriteLine($"Temperature report successfully created at: {reportPath}")
                StartLog()
            Else
                Debug.WriteLine("Failed to create temperature report.")
            End If
            Process.Start(reportPath)
            LblStatusMessage.Text = $"Temperaturbericht erfolgreich erstellt und geöffnet: {reportFileName}"
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Exportieren des Temperatur-Logs: {ex.Message}", "Export Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Temperature Log Export Error: {ex.Message}")
        End Try
    End Sub
    Private Sub CheckAndManageLogFile()
        Dim fileInfo As New FileInfo(LogFilePath)
        Dim fileSizeInBytes As Long = fileInfo.Length
        Dim maxSizeBytes As Long = LogSize * 1024
        Dim timestamp As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")
        Try
            If File.Exists(LogFilePath) Then
                LblStatusMessage.Text = $"Überprüfe Log-Datei '{LogFilePath}'..."
                If fileSizeInBytes >= maxSizeBytes Then
                    LblStatusMessage.Text = $"{LogSize}KB ({fileSizeInBytes} Bytes) erreicht. Lösche und erstelle neu..."
                    StopLog()
                    ' File.Copy(LogFilePath, $"CoolCore_TempeLog_{timestamp}.txt", True)
                    Dim archiveDir As String = Path.GetDirectoryName(LogFilePath)
                    Dim archiveFileName As String = $"CoolCore_TempeLog_{timestamp}.txt"
                    Dim archivePath As String = Path.Combine(archiveDir, archiveFileName)
                    Try
                        File.Copy(LogFilePath, archivePath, True)
                        Debug.WriteLine($"Log-Datei erfolgreich archiviert: {archivePath}")
                    Catch ex As Exception
                        MessageBox.Show($"Fehler beim Archivieren der Log-Datei: {ex.Message}", "Archivierungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Debug.WriteLine($"Fehler beim Kopieren der Log-Datei: {ex.Message}")
                    End Try
                    LblStatusMessage.Text = $"Log-Datei '{LogFilePath}' archiviert."
                    File.Delete(LogFilePath)
                    LblStatusMessage.Text = $"Log-Datei '{LogFilePath}' gelöscht."
                    Using writer As New StreamWriter(LogFilePath, True, Encoding.UTF8)
                        writer.WriteLine("--- CoolCore Temperatur-Log ---")
                        writer.WriteLine("Zeitpunkt;CPU-Core;MinTemp;MaxTemp;CurrentTemp")
                    End Using
                    LblStatusMessage.Text = $"Neue leere Log-Datei '{LogFilePath}' mit Header erstellt."
                    If fileSizeInBytes > 0 Then
                        StartLog()
                    End If
                Else
                End If
            Else
                LblStatusMessage.Text = $"Log-Datei '{LogFilePath}' nicht gefunden. Erstelle eine neue..."
                Using writer As New StreamWriter(LogFilePath, False, Encoding.UTF8)
                    writer.WriteLine("--- CoolCore Temperatur-Log ---")
                    writer.WriteLine("Zeitpunkt;CPU-Core;MinTemp;MaxTemp;CurrentTemp")
                End Using
            End If
            If isLoggingActive = False Then
                LblStatusMessage.Text = $"Log: Stop!"
            End If
        Catch ex As Exception
            Debug.WriteLine($"Fehler beim Verwalten der Log-Datei: {ex.Message}")
        End Try
    End Sub

End Class