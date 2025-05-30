Imports System.Management
Imports System.Net.NetworkInformation
Imports System.Net
Imports System.Linq
Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports OpenHardwareMonitor.Hardware
Imports System.Activities.Statements
Imports HidSharp


Public Class Form1
    Private systemInfoRepository As SystemInfoRepository
    Private cpuLoadCounter As PerformanceCounter
    Private refreshTimer As Timer
    Private cpuLoadCounters As New List(Of PerformanceCounter)()
    Private ReadOnly LoadBoxes As New Dictionary(Of Integer, TextBox)()
    Private ReadOnly MinTempBoxes As New Dictionary(Of Integer, TextBox)()
    Private ReadOnly MaxTempBoxes As New Dictionary(Of Integer, TextBox)()
    Private computer As Computer ' Initialize the computer object with enabled hardware monitoring
    Private cpu As IHardware ' To hold the CPU hardware object
    Private coreTemperatures As New List(Of ISensor)()
    Private ReadOnly CoreTempBoxes As New Dictionary(Of Integer, TextBox)()
    Private ReadOnly coreIndex As Integer

    Public Sub New()
        InitializeComponent()
        systemInfoRepository = New SystemInfoRepository()
        cpuLoadCounter = New PerformanceCounter("Processor", "% Processor Time", "_Total")
        refreshTimer = New Timer()
        refreshTimer.Interval = 1000 ' 1000 milliseconds = 1 second
        AddHandler refreshTimer.Tick, AddressOf RefreshTimer_Tick
        If Not LoadBox Is Nothing Then LoadBoxes.Add(0, LoadBox)
        If Not LoadBox1 Is Nothing Then LoadBoxes.Add(1, LoadBox1)
        If Not LoadBox2 Is Nothing Then LoadBoxes.Add(2, LoadBox2)
        If Not LoadBox3 Is Nothing Then LoadBoxes.Add(3, LoadBox3)
        ' Map Temperature Boxes to core indices
        If Not CoreTemp Is Nothing Then CoreTempBoxes.Add(0, CoreTemp)
        If Not CoreTemp1 Is Nothing Then CoreTempBoxes.Add(1, CoreTemp1)
        If Not CoreTemp2 Is Nothing Then CoreTempBoxes.Add(2, CoreTemp2)
        If Not CoreTemp3 Is Nothing Then CoreTempBoxes.Add(3, CoreTemp3)

        If Not MinTemp Is Nothing Then MinTempBoxes.Add(0, MinTemp)
        If Not MinTemp1 Is Nothing Then MinTempBoxes.Add(1, MinTemp1)
        If Not MinTemp2 Is Nothing Then MinTempBoxes.Add(2, MinTemp2)
        If Not MinTemp3 Is Nothing Then MinTempBoxes.Add(3, MinTemp3)

        If Not MaxTemp Is Nothing Then MaxTempBoxes.Add(0, MaxTemp)
        If Not MaxTemp1 Is Nothing Then MaxTempBoxes.Add(1, MaxTemp1)
        If Not MaxTemp2 Is Nothing Then MaxTempBoxes.Add(2, MaxTemp2)
        If Not MaxTemp3 Is Nothing Then MaxTempBoxes.Add(3, MaxTemp3)
    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblStatusMessage.Text = "Ready to read system information."
        LblStatusMessage.ForeColor = Color.AliceBlue
        ClearCpuDisplayControls()
        'computer.ToString() ' Initialize the computer object
        computer = New Computer()
        computer.Open(True) ' Open the computer object to access hardware
        computer.IsCpuEnabled = True ' Enable CPU monitoring
        Await ReadAndDisplaySystemInfoAsync()
        InitializePerCoreCounters()
        InitializeCoreTemperatureSensors()
        refreshTimer.Start()
        LblStatusMessage.Text = "Real-time monitoring started. Static info saved."
        LblStatusMessage.ForeColor = Color.Green
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        refreshTimer.Stop()
        refreshTimer.Dispose()
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
            Debug.WriteLine($"Error initializing per-core counters: {ex.ToString()}")
        End Try
    End Sub


    Private Sub InitializeCoreTemperatureSensors()
        Try
            ' CPU-Hardwareobjekt finden
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
                        If sensor.SensorType = SensorType.Temperature AndAlso sensor.Name.StartsWith("Core") Then
                            coreTemperatures.Add(sensor)
                        End If
                    Next
                    Debug.WriteLine($"Anzahl gefundener Core-Temperatursensoren: {coreTemperatures.Count}")
                    ' Nach Namen sortieren, damit die Reihenfolge zu den Indizes passt
                    coreTemperatures = coreTemperatures.OrderBy(Function(s) s.Name).ToList()

                    'MessageBox.Show($"Anzahl gefundener Core-Temperatursensoren nach Sortierung: {coreTemperatures.Count}")
                Else
                    Debug.WriteLine("CPU-Hardware wurde von OpenHardwareMonitor nicht gefunden.")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Initialisieren der Temperatursensoren: {ex.Message}", "Initialisierungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Fehler beim Initialisieren der Temperatursensoren: {ex.ToString()}")
        End Try
    End Sub

    Private Sub RefreshTimer_Tick(sender As Object, e As EventArgs)
        Try
            ' Update all hardware (including sensors)
            cpu?.Update()

            ' Update CPU Load for each core
            For i As Integer = 0 To cpuLoadCounters.Count - 1
                Dim currentCoreLoad As Single = cpuLoadCounters(i).NextValue()
                If LoadBoxes.ContainsKey(i) Then
                    LoadBoxes(i).Text = $"{currentCoreLoad:F2}%"
                End If
            Next

            Dim packagePowerSensor = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Power AndAlso s.Name.Contains("Package Power"))
            If packagePowerSensor IsNot Nothing AndAlso packagePowerSensor.Value.HasValue Then
                ' Update a TextBox like TDPBox with the current power draw
                PowerBox.Text = $"{packagePowerSensor.Value.Value:F1}W"
            Else
                PowerBox.Text = "N/A (Power)"
            End If

            ' Update Core Temperatures
            For i As Integer = 0 To coreTemperatures.Count - 1
                Dim sensor As ISensor = coreTemperatures(i)
                Dim coreIndex As Integer = i ' Assuming coreTemperatures list is sorted by core index

                If CoreTempBoxes.ContainsKey(coreIndex) Then
                    CoreTempBoxes(coreIndex).Text = If(sensor.Value.HasValue, $"{sensor.Value.Value:F1}°C", "N/A") ' Current temp
                End If
                If MinTempBoxes.ContainsKey(coreIndex) Then
                    MinTempBoxes(coreIndex).Text = If(sensor.Min.HasValue, $"{sensor.Min.Value:F1}°C", "N/A") ' Minimum observed
                End If
                If MaxTempBoxes.ContainsKey(coreIndex) Then
                    MaxTempBoxes(coreIndex).Text = If(sensor.Max.HasValue, $"{sensor.Max.Value:F1}°C", "N/A") ' Maximum observed
                End If
                If coreTemperatures.Count = 2 Then                         ' Wenn nur 2 Kerne gefunden wurden, fügen Sie einen Dummy-Sensor für den dritten Kern hinzu

                    MaxTemp2.Text = MaxTemp.Text  ' Dummy value for the second core
                    MinTemp2.Text = MinTemp.Text  ' Dummy value for the second core
                    CoreTemp2.Text = CoreTemp.Text ' Dummy value for the third core
                    MaxTemp3.Text = MaxTemp1.Text
                    MinTemp3.Text = MinTemp1.Text ' Dummy value for the third core
                    CoreTemp3.Text = CoreTemp1.Text ' Dummy value for the third core

                End If
            Next

        Catch ex As Exception
            Debug.WriteLine($"Error during refresh: {ex.Message}")
            ' Show error in all relevant boxes if an exception occurs
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
            'Dim load As String = "N/A"
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
            ' --- Retrieve BIOS Information (Example, adjust as needed) ---
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
                          '#--------------------------------------------------------------------------------------------------------------------'
                          ' Try to get more detailed info from OpenHardwareMonitor's CPU object
                          If cpu IsNot Nothing Then
                              ' Display identifier for more info
                              ModelBox.Text &= $" ({cpu.Identifier})" ' Append to existing model name or use a new box
                              ' Search for specific sensors for power/voltage if they exist
                              Dim packagePowerSensor = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Power AndAlso s.Name.Contains("Package"))
                              If packagePowerSensor IsNot Nothing AndAlso packagePowerSensor.Value.HasValue Then
                                  TDPBox.Text = $"{packagePowerSensor.Value.Value:F1}W" ' Display current package power
                              Else
                                  TDPBox.Text = "N/A"
                              End If

                              Dim coreVoltageSensor = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Voltage AndAlso s.Name.Contains("Core"))
                              If coreVoltageSensor IsNot Nothing AndAlso coreVoltageSensor.Value.HasValue Then
                                  VidBox.Text = $"{coreVoltageSensor.Value.Value:F3}V"
                              Else
                                  VidBox.Text = "N/A"
                              End If

                              ' Revision might be in cpu.Version or cpu.Identifier
                              RevisionBox.Text = cpu.Identifier.ToString ' Or try parsing cpu.Identifier
                              CPUIDBox.Text = "N/A" ' Not directly exposed
                              LitBox.Text = "N/A (" ' Not directly exposed
                          Else
                              TDPBox.Text = "N/A"
                              VidBox.Text = "N/A"
                              RevisionBox.Text = "N/A"
                              CPUIDBox.Text = "N/A"
                              LitBox.Text = "N/A"
                          End If
                      End Sub)
            '#--------------------------------------------------------------------------------------------------------------------'
            Await systemInfoRepository.SaveSystemInfoAsync(systemInfo)
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
    ' Helper method to clear display control
    Private Sub ClearCpuDisplayControls()
        ModelBox.Text = ""
        FrequencyBox.Text = ""
        PlatformBox.Text = ""
        TextBox3.Text = ""
        TextBox9.Text = ""
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

    Private Sub PowerBox_TextChanged(sender As Object, e As EventArgs) Handles PowerBox.TextChanged

    End Sub
End Class