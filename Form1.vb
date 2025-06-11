Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Runtime.Versioning
Imports System.Text
Imports System.Threading
Imports HidSharp.Utility
Imports Microsoft.VisualBasic.Logging
Imports Newtonsoft.Json
Imports OpenHardwareMonitor.Hardware
Imports Timer = System.Windows.Forms.Timer
Imports System.Text.RegularExpressions
Imports System.Management
Public Structure CoreTempData
    Public Property Timestamp As DateTime
    Public Property CoreTemperatures As Dictionary(Of String, Single)
End Structure

Public Class Form1
    Private systemInfoRepository As SystemInfoRepository
    Private cpuLoadCounter As PerformanceCounter
    Private refreshTimer As Timer
    Private cpuLoadCounters As New List(Of PerformanceCounter)()
    Private freqCores As New List(Of PerformanceCounter)()
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
    Private ReadOnly FallbackFontFamilyName As String = "Segoe UI"
    Private ReadOnly FallbackFontFamilyNameOld As String = "Microsoft Sans Serif"
    Private Const CPU_SPECS_CSV_FILE As String = "intel-cpus.csv"
    Private foundCpuDetails As New Dictionary(Of String, String)()


    'Programm initialization
    Public Sub New()
        InitializeComponent()
        CheckAndSetSystemFonts()
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
        computer = New Computer() With {
            .IsMotherboardEnabled = True,
            .IsCpuEnabled = True,
            .IsMemoryEnabled = True,
            .IsGpuEnabled = True,
            .IsPsuEnabled = True,
            .IsStorageEnabled = True
        }
        refreshTimer.Start()
        Dim InitBoxes As Task = Task.Run(Function() CeckTempLoadBoxes())
    End Sub
    Private Function CeckTempLoadBoxes() As Task
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
        If MaxTemp2 IsNot Nothing Then MaxTempBoxes.Add(2, MaxTemp2)
        If MaxTemp3 IsNot Nothing Then MaxTempBoxes.Add(3, MaxTemp3)
        If Vbox1 IsNot Nothing Then VoltBoxes.Add(1, Vbox1)
        If VBox2 IsNot Nothing Then VoltBoxes.Add(2, VBox2)
        If VBox3 IsNot Nothing Then VoltBoxes.Add(3, VBox3)
        If VBox4 IsNot Nothing Then VoltBoxes.Add(4, VBox4)
        Return Task.CompletedTask
    End Function

    'Form Logic
    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblStatusMessage.Text = "Ready to read system information."
        LblStatusMessage.ForeColor = Color.Black
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
        ClearCpuDisplayControls()
        Await Task.Run(Sub()
                           InitializePerCoreCounters()
                           InitializeCoreTemperatureSensors()
                           InitializeVoltageSensors()


                       End Sub)
        refreshTimer.Start()
        Dim LogSystem As Task = Task.Run(Sub()
                                             StartStopLog()
                                             UpdateLogSize()
                                             BrandCheck()
                                         End Sub)
        Await LogSystem
        ReadAndDisplaySystemInfoAsync()
        GetCpuSubInfos()
    End Sub

    Private Function BrandCheck() As Task
        Dim sysinfo = systemInfoRepository.GetCurrentSystemInfo.CpuName
        Dim cpuName As String = sysinfo
        Dim cpuNameLower As String = cpuName.ToLowerInvariant()
        If cpuNameLower.Contains("intel") Then
            If cpuNameLower.Contains("intel") AndAlso Settings.ApplicationTheme = "Standard" Then
                PicBox2.Image = Resources.IntelLogo
                Settings.CpuLogoName = "intel"
            ElseIf cpuNameLower.Contains("intel") AndAlso Settings.ApplicationTheme = "Dark" Then
                PicBox2.Image = Resources.IntelLogo_white
            End If
        ElseIf cpuNameLower.Contains("amd") Then
            If cpuNameLower.Contains("amd") AndAlso Settings.ApplicationTheme = "Standard" Then
                PicBox2.Image = Resources.AMDLogo
                Settings.CpuLogoName = "amd"
            ElseIf cpuNameLower.Contains("amd") AndAlso Settings.ApplicationTheme = "Dark" Then
                PicBox2.Image = Resources.AMDLogo_Dark
            End If
        Else
            PicBox2.Image = Nothing
        End If
        Return Task.CompletedTask
    End Function
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

    'Sensor initialization
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
                                      VoltBoxes(boxIndex).Text = $"{sensor.Value.Value:F3}V"
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
                                      VoltBoxes(1).Text = $"{genericVcoreSensor.Value.Value:F3}V (VCore)"
                                  End Sub)
                    End If
                End If
            End If

            For Each kvp In VoltBoxes
                If Me.InvokeRequired Then
                    Me.Invoke(Sub()
                                  If String.IsNullOrEmpty(kvp.Value.Text) OrElse Not kvp.Value.Text.EndsWith("V") Then
                                      If Not (kvp.Value.Text.Contains("(VCore)") AndAlso Not String.IsNullOrEmpty(kvp.Value.Text)) Then
                                          kvp.Value.Text = "N/A"
                                      End If
                                  End If
                              End Sub)
                Else
                    If String.IsNullOrEmpty(kvp.Value.Text) OrElse Not kvp.Value.Text.EndsWith("V") Then
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

    'Timer Progress
    Private Sub RefreshTimer_Tick(sender As Object, e As EventArgs)
        If cpu Is Nothing Then
            InitializeCoreTemperatureSensors()
            InitializePerCoreCounters()
            InitializeVoltageSensors()
        End If
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

                Me.Invoke(Sub()
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
                        temperatureLogWriter.WriteLine(
                            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss};" &
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
            Me.Invoke(Sub()
                          CheckAndManageLogFile()
                          FrequencyBox2.Text = $"{Math.Round(UpdateCpuFrequencyDisplay(), 1)} MHz"
                      End Sub)
        Catch ex As Exception
            For Each kvp In LoadBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In CoreTempBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In MinTempBoxes : kvp.Value.Text = "Error" : Next
            For Each kvp In MaxTempBoxes : kvp.Value.Text = "Error" : Next
        End Try
    End Sub

    Private Function UpdateCpuFrequencyDisplay()
        Try
            If cpu Is Nothing Then
                Return FrequencyBox2.Text = "N/A"
            End If
            cpu.Update()
            Dim coreClocks As New List(Of Single)()
            Dim packageClock As ISensor = Nothing
            For Each sensor In cpu.Sensors
                If sensor.SensorType = SensorType.Clock AndAlso sensor.Value.HasValue Then
                    If sensor.Name.StartsWith("CPU Core #") OrElse sensor.Name.StartsWith("Core #") Then
                        coreClocks.Add(sensor.Value.Value)
                    ElseIf sensor.Name.Contains("CPU Package") OrElse sensor.Name.Contains("Core Max") OrElse sensor.Name.Contains("CPU Bus") Then
                        If sensor.Name.Contains("CPU Package") OrElse sensor.Name.Contains("Core Max") Then
                            packageClock = sensor
                        End If
                    End If
                End If
            Next
            If packageClock IsNot Nothing Then
                Return packageClock.Value.Value
            ElseIf coreClocks.Count > 0 Then
                Dim averageCoreClock As Single = coreClocks.Average()
                Dim maxCoreClock As Single = coreClocks.Max()
                'maxCoreClock
                Return averageCoreClock
            Else
                Dim currentClockSpeedWMI As Integer = 0
                Using searcher As New ManagementObjectSearcher("SELECT CurrentClockSpeed FROM Win32_Processor")
                    For Each queryObj As ManagementObject In searcher.Get()
                        Return currentClockSpeedWMI = If(queryObj("CurrentClockSpeed"), 0)
                        Exit For
                    Next
                End Using
            End If
        Catch ex As Exception
            If FrequencyBox2 IsNot Nothing Then
                FrequencyBox2.Text = "Fehler"
            End If
            Debug.WriteLine($"UpdateCpuFrequencyDisplay ERROR: {ex.Message}")
        End Try
    End Function

    'Hardware Initialization
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
    Private Sub ReadAndDisplaySystemInfoAsync()
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
            Debug.WriteLine(systemInfo)

            Dim MaxFreq As Double = Math.Round(systemInfo.CurrentClockSpeedMHz / 1000, 2, MidpointRounding.ToEven) ' Convert MHz to GHz for display
            '#--------------------------------------------------------------------------------------------------------------------'
            Dim biosVersion As String = "N/A"
            Dim biosSearcher As New ManagementObjectSearcher("SELECT Version FROM Win32_BIOS")
            For Each obj As ManagementObject In biosSearcher.Get()
                biosVersion = If(obj("Version"), "N/A").ToString()
                Exit For
            Next
            Me.Invoke(Sub()

                          ModelBox.Text = systemInfo.CpuName.Aggregate("", Function(current, nextChar) current & nextChar.ToString().ToUpperInvariant())

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
                                  'PowerBox2.Text = $"{coreVoltageSensor.Value.Value:F3}V"
                                  UpdateVoltageDisplay()
                              Else
                                  PowerBox2.Text = "N/A"
                              End If
                              RevisionBox.Text = cpu.Identifier.ToString.LastIndexOf("/"c)
                              CPUIDBox.Text = "N/A"
                              LithographyBox.Text = "N/A"
                          Else
                              TDPBox.Text = "N/A"
                              'VidBox.Text = "N/A"
                              'RevisionBox.Text = "N/A"
                              'CPUIDBox.Text = "N/A"
                              LithographyBox.Text = "N/A"
                          End If
                          Dim tjmax = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Temperature AndAlso s.Name.Contains("Distance to TjMax"))
                          If tjmax IsNot Nothing AndAlso tjmax.Value.HasValue Then
                              Dim maxtj As Single = 15.0F
                              TJBox.Text = $"{tjmax.Value.Value + maxtj}°C"
                          Else
                              TJBox.Text = "N/A"
                          End If
                          Dim vid = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Clock AndAlso s.Name.Contains("Bus Speed"))
                          If vid IsNot Nothing AndAlso vid.Value.HasValue Then
                              Dim sum As Double = 0.0
                              Dim total As Double = 0.0
                              VidBox.Text = $"{vid.Value.Value:F3} Mhz"
                              sum = systemInfo.CurrentClockSpeedMHz + CInt(vid.Value.Value)
                              FrequencyBox.Text = Math.Round(sum / 1000, 1) & " GHz"
                          Else
                              VidBox.Text = "N/A"
                          End If
                          Dim PowerAllCores = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Power AndAlso s.Name.Contains("Cores"))
                          If PowerAllCores IsNot Nothing AndAlso PowerAllCores.Value.HasValue Then
                              PowerBox2.Text = $"{PowerAllCores.Value.Value:F3}V"
                          Else
                              LithographyBox.Text = "N/A"
                          End If
                          Dim tdp = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Power AndAlso s.Name.Contains("TDP"))
                          If tdp IsNot Nothing AndAlso tdp.Value.HasValue Then
                              TDPBox.Text = $"{tdp.Value.Value:F1} W"
                          Else
                              TDPBox.Text = "N/A"
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

    End Sub
    Private Sub ClearCpuDisplayControls()
        ModelBox.Text = ""
        FrequencyBox.Text = ""
        PlatformBox.Text = ""
        PowerBox2.Text = ""
        TDPBox.Text = ""
        LithographyBox.Text = ""
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

    'CPU Sub Information
    Private Sub GetCpuSubInfos()

        foundCpuDetails.Clear()
        Dim cpuNameFromWMI As String = "N/A"
        Dim processorIdFromWMI As String = "N/A"
        Dim manufacturerFromWMI As String = "N/A"
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT Name, ProcessorId, Manufacturer FROM Win32_Processor")
            For Each queryObj As ManagementObject In searcher.Get()
                cpuNameFromWMI = queryObj("Name")?.ToString()
                processorIdFromWMI = queryObj("ProcessorId")?.ToString()
                manufacturerFromWMI = queryObj("Manufacturer")?.ToString()
                Dim name As String = queryObj("Name")?.ToString()
                Dim family As String = queryObj("Family")?.ToString()
                Dim stepping As String = queryObj("Stepping")?.ToString()
                Dim revision As String = queryObj("revision")?.ToString()
                ' Debug.WriteLine($"Name: {name}, Family: {family}, Stepping: {stepping}, Revision: {revision}")
                ModelBox.Text = name
                CPUIDBox.Text = If(queryObj("processorId"), "N/A").ToString()
                Exit For
            Next
        Catch ex As Exception
            Debug.WriteLine($"Fehler beim Abrufen der CPU-Informationen von WMI: {ex.Message}")
        End Try
        ModelBox.Text = cpuNameFromWMI
        CPUIDBox.Text = processorIdFromWMI
        ' Dim foundCpuDetails As New Dictionary(Of String, String)()

        Dim csvFilePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CPU_SPECS_CSV_FILE)

        If File.Exists(csvFilePath) Then
            Try
                Dim lines As String() = File.ReadAllLines(csvFilePath)
                If lines.Length > 0 Then
                    Dim headers As String() = lines(0).Split(","c).Select(Function(h) h.Trim("""").Trim()).ToArray()
                    Dim nameColumnIndex As Integer = Array.IndexOf(headers, "CpuName")

                    If nameColumnIndex = -1 Then
                        Debug.WriteLine($"CSV Error: Spalte 'CpuName' nicht gefunden in '{CPU_SPECS_CSV_FILE}'")
                        LithographyBox.Text = "CSV Error: CpuName-Spalte fehlt"
                        TDPBox.Text = "CSV Error"
                        Return
                    End If

                    Dim normalizedWmiCpuName As String = NormalizeCpuName(cpuNameFromWMI)
                    Debug.WriteLine($"Normalized WMI CPU Name: '{normalizedWmiCpuName}'")

                    For i As Integer = 1 To lines.Length - 1
                        Dim data As String() = lines(i).Split(","c).Select(Function(d) d.Trim("""").Trim()).ToArray()

                        If data.Length > nameColumnIndex Then
                            Dim csvCpuName As String = data(nameColumnIndex)
                            Dim normalizedCsvCpuName As String = NormalizeCpuName(csvCpuName)
                            Debug.WriteLine($"Comparing: WMI Normalized '{normalizedWmiCpuName}' with CSV Normalized '{normalizedCsvCpuName}' (Original CSV: '{csvCpuName}')")

                            If Not String.IsNullOrWhiteSpace(normalizedWmiCpuName) AndAlso
                               normalizedWmiCpuName.Contains(normalizedCsvCpuName) Then
                                For colIndex As Integer = 0 To headers.Length - 1
                                    If colIndex < data.Length Then
                                        Dim headerName As String = headers(colIndex)
                                        Dim cellValue As String = data(colIndex)
                                        foundCpuDetails.Add(headerName, cellValue)
                                    End If
                                Next
                                If foundCpuDetails.ContainsKey("Lithography") Then
                                    LithographyBox.Text = foundCpuDetails("Lithography")
                                Else
                                    LithographyBox.Text = "N/A (Litho in CSV fehlt)"
                                End If

                                If foundCpuDetails.ContainsKey("TDPMax") Then

                                    TDPBox.Text = foundCpuDetails("TDP")
                                End If

                                Exit For
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                Debug.WriteLine($"Fehler beim Lesen oder Parsen der CSV-Datei '{CPU_SPECS_CSV_FILE}': {ex.Message}")
                LithographyBox.Text = "CSV Lesefehler"
            End Try
        Else
            Debug.WriteLine($"CSV Error: Datei '{CPU_SPECS_CSV_FILE}' nicht gefunden unter '{csvFilePath}'")
            LithographyBox.Text = "CSV nicht gefunden"
        End If

    End Sub

    Private Function NormalizeCpuName(ByVal cpuName As String) As String
        If String.IsNullOrWhiteSpace(cpuName) Then
            Return String.Empty
        End If
        Dim normalizedName As String = cpuName.ToLowerInvariant()
        normalizedName = normalizedName.Replace("(r)", "")
        normalizedName = normalizedName.Replace("®", "")
        normalizedName = normalizedName.Replace("(tm)", "")
        normalizedName = normalizedName.Replace("™", "")
        normalizedName = normalizedName.Replace("cpu @", "")
        normalizedName = normalizedName.Replace("processor", "")
        normalizedName = normalizedName.Replace("ghz", "")
        normalizedName = normalizedName.Replace("k", "")
        normalizedName = normalizedName.Replace("x", "")
        ' normalizedName = Regex.Replace(normalizedName, "\d+(\.\d+)?$", "").Trim()
        normalizedName = System.Text.RegularExpressions.Regex.Replace(normalizedName, "\s+", " ").Trim()
        Return normalizedName
    End Function

    'Test Section initialization
    Private Sub BtnToggleMonitor1_Click(sender As Object, e As EventArgs) Handles BtnToggleMonitor1.Click
        Dim attentionMessage = File.ReadAllText("TestInfo.txt")
        Dim result As DialogResult = MessageBox.Show(Me, attentionMessage, "Wichtiger Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
        If result = DialogResult.OK Then
            TestStart()
        Else
            MessageBox.Show(Me,
                            "Test Vorgang nicht gestartet!",
                            caption:="Info: Test nicht gestartet:",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub TestStart()
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
            monitoringForm.StartAnimation()
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
        If monitoringForm IsNot Nothing AndAlso Not monitoringForm.IsDisposed Then
            monitoringForm.StopAnimation()
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
                chartForm.Show
                ' MessageBox.Show($"Temperature data saved to {savedFilePath}", "Monitoring Stopped", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'ExportLog(savedFilePath)
            Else
                MessageBox.Show("Failed to save temperature data.", "Monitoring Stopped", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("No temperature data recorded.", "Monitoring Stopped", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        ' refreshTimer.Start()
        monitoringTimer.Stop()
        monitoringStopwatch.Stop()
        Me.Invoke(Sub()
                      InitializePerCoreCounters()
                      InitializeCoreTemperatureSensors()
                      ReadAndDisplaySystemInfoAsync()
                  End Sub)
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
                    If sensor.Name.StartsWith("CPU Core #", StringComparison.OrdinalIgnoreCase) And Not sensor.Name.Contains("Distance to TjMax") AndAlso sensor.Value.HasValue Then
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

    'Archive load and archived measurements from the TemperatureLogs directory
    Private Sub LoadArchivedMeasurementsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadArchivedMeasurementsToolStripMenuItem.Click
        Dim programDirectory1 As String = AppDomain.CurrentDomain.BaseDirectory
        Dim logDirectory1 As String = Path.Combine(programDirectory1, "TemperatureLogs")

        If Not Directory.Exists(logDirectory1) Then
            MessageBox.Show("Keine archivierten Messungen gefunden. Der Ordner 'TemperatureLogs' existiert nicht.", "Archiv leer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Using archiveSelectionForm1 As New Form4(logDirectory1)
            Dim dialogResult1 As DialogResult = archiveSelectionForm1.ShowDialog(Me)

            If dialogResult1 = DialogResult.OK Then
                Dim selectedFilePath1 As String = archiveSelectionForm1.SelectedFilePath

                If Not String.IsNullOrEmpty(selectedFilePath1) Then
                    Try
                        Dim chartForm As New Form2(selectedFilePath1)
                        chartForm.Show()
                        LblStatusMessage.Text = $"Archivierte Messung '{Path.GetFileName(selectedFilePath1)}' geladen."
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
            End If
        End Using
    End Sub

    'Export CPU Info Section
    Private Sub ExportCPUInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportCPUInfoToolStripMenuItem.Click
        If foundCpuDetails.Count = 0 Then
            MessageBox.Show("Keine CPU-Informationen zum Exportieren verfügbar. Bitte scannen Sie zuerst die CPU-Details.", "Exportinformation", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim htmlContent As String = GenerateSystemInfoHtml(foundCpuDetails)

        Using saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "HTML-Datei (*.html)|*.html|Alle Dateien (*.*)|*.*"
            saveFileDialog.Title = "CPU-Informationen exportieren"
            saveFileDialog.FileName = $"CPUInfo_{DateTime.Now:yyyyMMdd_HHmmss}.html"
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    File.WriteAllText(saveFileDialog.FileName, htmlContent, System.Text.Encoding.UTF8)
                    MessageBox.Show("CPU-Informationen erfolgreich exportiert.", "Export abgeschlossen", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"Fehler beim Speichern der HTML-Datei: {ex.Message}", "Exportfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Function GenerateSystemInfoHtml(cpuData As Dictionary(Of String, String)) As String
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
            htmlBuilder.AppendLine("</style>")
            htmlBuilder.AppendLine("</head>")
            htmlBuilder.AppendLine("<body>")
            htmlBuilder.AppendLine("  <div class='container'>")
            htmlBuilder.AppendLine("    <h1>CPU Informationen Bericht</h1>")
            ' Hier können Sie allgemeine Informationen hinzufügen, wenn gewünscht
            htmlBuilder.AppendLine($"    <p class='note'>Bericht erstellt am: {DateTime.Now:dd.MM.yyyy HH:mm:ss}</p>")

            htmlBuilder.AppendLine("    <h2>CPU Details</h2>")
            htmlBuilder.AppendLine("    <table>")

            ' Iteriere durch alle Schlüssel-Wert-Paare im Dictionary und füge sie als Tabellenzeilen hinzu
            For Each entry In cpuData.OrderBy(Function(e) e.Key) ' Optional: Nach Eigenschaftsname sortieren
                htmlBuilder.AppendLine($"      <tr><th>{entry.Key}</th><td>{entry.Value}</td></tr>")
            Next

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

    'Theme Section
    Private Sub ApplyTheme(theme As String)
        Select Case theme
            Case "Dark"
                Me.BackColor = ColorTranslator.FromHtml("#282C34")
                Me.ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next

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
                Me.BackColor = ColorTranslator.FromHtml("#F0F0F0")
                Me.ForeColor = ColorTranslator.FromHtml("#333333")
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
                If Settings.CpuLogoName = "intel" Then
                    PicBox2.Image = Resources.IntelLogo_white
                ElseIf Settings.CpuLogoName = "amd" Then
                    PicBox2.Image = Resources.AMDLogo_Dark
                End If
            Case "Standard"
                If TypeOf ctrl Is Button Then
                    CType(ctrl, Button).BackColor = ColorTranslator.FromHtml("#E1E1E1")
                    CType(ctrl, Button).ForeColor = ColorTranslator.FromHtml("#333333")
                    CType(ctrl, Button).FlatStyle = FlatStyle.Flat
                    CType(ctrl, Button).FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CCCCCC")
                    CType(ctrl, Button).FlatAppearance.BorderSize = 1
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
                    CType(ctrl, GroupBox).BackColor = ColorTranslator.FromHtml("#F0F0F0")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                ElseIf TypeOf ctrl Is Panel Then
                    CType(ctrl, Panel).BackColor = ColorTranslator.FromHtml("#F0F0F0")
                    CType(ctrl, Panel).ForeColor = ColorTranslator.FromHtml("#333333")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                End If
                If Settings.CpuLogoName = "intel" Then
                    PicBox2.Image = Resources.IntelLogo
                ElseIf Settings.CpuLogoName = "amd" Then
                    PicBox2.Image = Resources.AMDLogo
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
    Private Sub OptionsForm_ThemeChanged(sender As Object, newTheme As String)
        ApplyTheme(newTheme)
    End Sub

    'Menu Item Click Events
    Private Sub InfoMenuItem_Click(sender As Object, e As EventArgs) Handles InfoMenuItem.Click
        AboutBox1.ShowDialog(Me)
    End Sub
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Using optionsForm As New OptionsForm()
            AddHandler optionsForm.ThemeChanged, AddressOf OptionsForm_ThemeChanged
            optionsForm.ShowDialog(Me)
        End Using
    End Sub
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub
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

    'Log Section
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

    'Helper Section
    Public Function UpdateLogSize() As Task
        Dim logSizeKB As Integer = My.Settings.MAX_LOG_SIZE_KB
        LogSize = logSizeKB
        If LblStatusMessage.InvokeRequired Then
            LblStatusMessage.Invoke(Sub()
                                        LblStatusMessage.Text = $"Max. Loggröße: {logSizeKB} KB"
                                    End Sub)
        Else
            LblStatusMessage.Text = $"Max. Loggröße: {logSizeKB} KB"
        End If
        Return Task.CompletedTask
    End Function
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
                'Debug.WriteLine($"Log: {fileSizeInBytes} Bytes (max: {maxSizeBytes} bytes")
                LblStatusMessage.Text = $"Log-Datei Größe: {Math.Round(fileSizeInBytes / 1024)} KB (Max: {Math.Round(maxSizeBytes / 1024)} KB)"
                If fileSizeInBytes >= maxSizeBytes Then
                    LblStatusMessage.Text = $"{LogSize}KB ({fileSizeInBytes} Bytes) erreicht. Lösche und erstelle neu..."
                    StopLog()
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
    Private Sub CheckAndSetSystemFonts()
        Dim isWindows7OrOlder As Boolean = False
        Dim osVersion As Version = Environment.OSVersion.Version
        ' Windows 7 is version 6.1
        If osVersion.Major < 6 OrElse (osVersion.Major = 6 AndAlso osVersion.Minor <= 1) Then
            isWindows7OrOlder = True
            Debug.WriteLine("Running on Windows 7 or older.")
        Else
            Debug.WriteLine("Running on Windows 8 or newer.")
        End If
        If isWindows7OrOlder Then
            Dim fallbackFontFamily As FontFamily = Nothing
            If FontFamily.Families.Any(Function(f) f.Name = FallbackFontFamilyName) Then
                fallbackFontFamily = New FontFamily(FallbackFontFamilyName)
                Debug.WriteLine($"Fallback font set to: {FallbackFontFamilyName}")
            ElseIf FontFamily.Families.Any(Function(f) f.Name = FallbackFontFamilyNameOld) Then
                fallbackFontFamily = New FontFamily(FallbackFontFamilyNameOld)
                Debug.WriteLine($"Fallback font set to: {FallbackFontFamilyNameOld}")
            Else
                Debug.WriteLine("No preferred fallback font found. Using generic Sans Serif.")
                fallbackFontFamily = FontFamily.GenericSansSerif
            End If
            If fallbackFontFamily IsNot Nothing Then
                Dim programFontsToCheck As New List(Of String) From {
                    "Arial",
                    "Calibri",
                    "Tahoma"
                }
                Dim controlsToProcess As New Queue(Of Control)()
                controlsToProcess.Enqueue(Me)
                While controlsToProcess.Count > 0
                    Dim currentControl As Control = controlsToProcess.Dequeue()
                    If currentControl.Font IsNot Nothing Then
                        Dim currentFontFamilyName As String = currentControl.Font.FontFamily.Name
                        If programFontsToCheck.Contains(currentFontFamilyName) AndAlso
                           Not FontFamily.Families.Any(Function(f) f.Name = currentFontFamilyName) Then
                            Try
                                Dim newFont As New Font(fallbackFontFamily, currentControl.Font.Size, currentControl.Font.Style)
                                currentControl.Font = newFont
                                Debug.WriteLine($"Changed font for control '{currentControl.Name}' to '{fallbackFontFamily.Name}'.")
                            Catch ex As Exception
                                Debug.WriteLine($"Error changing font for control '{currentControl.Name}': {ex.Message}")
                            End Try
                        End If
                    End If
                    For Each childControl As Control In currentControl.Controls
                        controlsToProcess.Enqueue(childControl)
                    Next
                End While
            End If
        End If
    End Sub

    Private Sub CpuInfoMenu_Click(sender As Object, e As EventArgs) Handles CpuInfoMenu.Click
        If foundCpuDetails.Count > 0 Then
            Dim cpuInfoForm As New CpuinfoForm()
            cpuInfoForm.LoadCpuInfo(foundCpuDetails)
            cpuInfoForm.Show()
        Else
            MessageBox.Show("Keine detaillierten CPU-Informationen in der CSV-Datei für Ihre CPU gefunden.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class