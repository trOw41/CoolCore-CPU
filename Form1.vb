Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports Timer = System.Windows.Forms.Timer
Imports System.Globalization

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
        ' Map Temperature Boxes to core indices
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
        refreshTimer.Start()
        If MaxTemp2 IsNot Nothing Then MaxTempBoxes.Add(2, MaxTemp2)
        If MaxTemp3 IsNot Nothing Then MaxTempBoxes.Add(3, MaxTemp3)
    End Sub
    Private Sub RecordTemperaturesInBackground(cancellationToken As CancellationToken)
        Dim intervalMs As Integer = 2000 ' Messintervall von 2 Sekunden (anpassbar)

        Do While Not cancellationToken.IsCancellationRequested
            Try
                ' Sicherstellen, dass die Hardware aktualisiert wird
                cpu?.Update() ' Verwenden Sie das vorhandene cpu-Objekt

                Dim currentCoreTemps As New Dictionary(Of String, Single)()
                ' Iterieren Sie über die tatsächlich gefundenen coreTemperatures-Sensoren
                For Each sensor As ISensor In coreTemperatures
                    If sensor.Value.HasValue Then
                        currentCoreTemps.Add(sensor.Name, sensor.Value.Value)
                    End If
                Next

                If currentCoreTemps.Any() Then
                    Dim newEntry As New CoreTempData With {
                    .Timestamp = DateTime.Now,
                    .CoreTemperatures = currentCoreTemps
                }
                    SyncLock backgroundTempMeasurements ' Thread-sicherer Zugriff auf die Liste
                        backgroundTempMeasurements.Add(newEntry)
                    End SyncLock
                End If

                Task.Delay(intervalMs, cancellationToken).Wait() ' Auf das nächste Intervall warten

            Catch ex As OperationCanceledException
                ' Task wurde abgebrochen, ist normal
                Debug.WriteLine("Temperature monitoring task cancelled.")
                Exit Do
            Catch ex As Exception
                Debug.WriteLine($"Error during background temperature recording: {ex.Message}")
                ' Hier könnte man eine Fehlerbehandlung implementieren, z.B. eine Statusmeldung in der UI
                ' Me.Invoke(Sub() LblStatusMessage.Text = $"Error in background: {ex.Message}")
                Exit Do ' Schleife bei schwerwiegendem Fehler beenden
            End Try
        Loop
    End Sub

    Private Function SaveTemperatureDataToCsv(data As List(Of CoreTempData))
        ' Den Pfad des aktuellen Programms ermitteln
        Dim programDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim logDirectory As String = Path.Combine(programDirectory, "TemperatureLogs")

        ' Sicherstellen, dass das Verzeichnis existiert
        If Not Directory.Exists(logDirectory) Then
            Directory.CreateDirectory(logDirectory)
        End If

        ' Dateiname generieren (z.B. CoolCore_Temp_Log_YYYYMMDD_HHMMSS.csv)
        Dim fileName As String = $"CoolCore_Temp_Log_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
        Dim filePath As String = Path.Combine(logDirectory, fileName)

        Try
            Using writer As New StreamWriter(filePath, False, Encoding.UTF8)
                ' Header schreiben
                Dim coreNames As New SortedSet(Of String)() ' Für sortierte Kernnamen
                For Each entry In data
                    For Each kvp In entry.CoreTemperatures
                        coreNames.Add(kvp.Key) ' Hier werden die Namen wie "Core #0", "Core #1" gesammelt
                    Next
                Next
                writer.Write("Timestamp")
                For Each coreName In coreNames
                    ' HIER wird der Header für JEDEN KERN geschrieben
                    writer.Write($",{coreName} (°C)")
                Next
                writer.WriteLine()

                ' Daten schreiben
                For Each entry In data
                    writer.Write(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                    For Each coreName In coreNames
                        Dim temp As Single = 0
                        If entry.CoreTemperatures.TryGetValue(coreName, temp) Then
                            ' HIER wird der Wert für JEDEN KERN geschrieben
                            ' WICHTIG: Verwenden Sie CultureInfo.InvariantCulture für den Punkt als Dezimaltrennzeichen
                            writer.Write($",{temp.ToString("F1", CultureInfo.InvariantCulture)}")
                        Else
                            writer.Write(",N/A") ' N/A, wenn der Sensor für diesen Zeitpunkt nicht verfügbar war
                        End If
                    Next
                    writer.WriteLine()
                Next
            End Using
            Me.Invoke(Sub()
                          LblStatusMessage.Text = $"Temperature data saved to {filePath}"
                          LblStatusMessage.ForeColor = Color.Blue
                      End Sub)
            Return filePath ' Dateipfad zurückgeben
        Catch ex As Exception
            Me.Invoke(Sub()
                          LblStatusMessage.Text = $"Error saving data: {ex.Message}"
                          LblStatusMessage.ForeColor = Color.Red
                      End Sub)
            Return Nothing ' Im Fehlerfall Nothing zurückgeben
        End Try
        ' Rückgabe des Dateipfads für die direkte Nutzung im Chart-Fenster
        Return filePath
    End Function

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblStatusMessage.Text = "Ready to read system information."
        LblStatusMessage.ForeColor = Color.Black
        ClearCpuDisplayControls()

        ' Initialize the computer object with enabled hardware monitoring
        If computer Is Nothing Then
            computer = New Computer() ' Create a new Computer object if it doesn't exist
        End If
        computer.Hardware.Clear() ' Clear any existing hardware to ensure fresh data
        computer.Open(True) ' Open the computer object to access hardware
        computer.IsCpuEnabled = True ' Enable CPU monitoring
        computer.IsGpuEnabled = True ' Enable GPU monitoring if needed


        LblStatusMessage.Text = "Real-time monitoring started. Static info saved."
        LblStatusMessage.ForeColor = Color.Firebrick
        InitializePerCoreCounters()
        InitializeCoreTemperatureSensors()
        ' Read and display system information asynchronously
        Await ReadAndDisplaySystemInfoAsync()
        refreshTimer.Start()
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
            'MessageBox.Show($"Fehler beim Initialisieren der Temperatursensoren: {ex.Message}", "Initialisierungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Fehler beim Initialisieren der Temperatursensoren: {ex.Message}")
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

            If cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = Global.OpenHardwareMonitor.Hardware.SensorType.Power AndAlso s.Name.Contains("Package Power")) IsNot Nothing AndAlso cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = Global.OpenHardwareMonitor.Hardware.SensorType.Power AndAlso s.Name.Contains("Package Power")).Value.HasValue Then
                ' Update a TextBox like TDPBox with the current power draw
                PowerBox.Text = $"{cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = Global.OpenHardwareMonitor.Hardware.SensorType.Power AndAlso s.Name.Contains("Package Power")).Value.Value:F1}W"
            Else
                PowerBox.Text = "N/A"
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
                          PowerBox.Text = "N/A" ' Placeholder, will be updated later
                      End Sub)
            '#--------------------------------------------------------------------------------------------------------------------'
            ' Try to get more detailed info from OpenHardwareMonitor's CPU object
            If cpu IsNot Nothing Then
                ' Display identifier for more info
                ModelBox.Text &= $" ({cpu.Identifier})" ' Append to existing model name or use a new box
                ' Search for specific sensors for power/voltage if they exist
                Dim packagePowerSensor = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Power AndAlso s.Name.Contains("Package"))
                If packagePowerSensor IsNot Nothing AndAlso packagePowerSensor.Value.HasValue Then
                    PowerBox.Text = $"{packagePowerSensor.Value.Value:F1}W" ' Display current package power
                End If

                Dim coreVoltageSensor = cpu.Sensors.FirstOrDefault(Function(s) s.SensorType = SensorType.Voltage AndAlso s.Name.Contains("Core"))
                If coreVoltageSensor IsNot Nothing AndAlso coreVoltageSensor.Value.HasValue Then
                    PowerBox2.Text = $"{coreVoltageSensor.Value.Value:F3}V"
                Else
                    PowerBox2.Text = "N/A"
                End If

                ' Revision might be in cpu.Version or cpu.Identifier
                RevisionBox.Text = cpu.Identifier.ToString.LastIndexOf("/"c) ' Or try parsing cpu.Identifier
                CPUIDBox.Text = "N/A" ' Not directly exposed
                LitBox.Text = "N/A" ' Not directly exposed
            Else
                TDPBox.Text = "N/A"
                VidBox.Text = "N/A"
                RevisionBox.Text = "N/A"
                CPUIDBox.Text = "N/A"
                LitBox.Text = "N/A"
            End If

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
            BtnToggleMonitoring.Text = "Stop Temp. Monitoring"
            ' LblStatusMessage.Text = "Background temperature monitoring started..." 
            ' LblStatusMessage.ForeColor = Color.Orange

            backgroundTempMeasurements.Clear() ' Vorherige Daten löschen
            cts = New CancellationTokenSource()
            loadingForm = New Form3()
            AddHandler loadingForm.StopRequested, AddressOf LoadingForm_StopRequested
            loadingForm.Show()

            monitoringTask = Task.Run(Sub() RecordTemperaturesInBackground(cts.Token))

            refreshTimer.Stop()

        Else
            Call StopMonitoringProcess()
        End If
    End Sub
    Private Function StartMonitoringAsync() As Task
        If Not isMonitoringActive Then
            ' Monitoring starten
            isMonitoringActive = True
            BtnToggleMonitoring.Text = "Stop Temp. Monitoring"
            LblStatusMessage.Text = "Background temperature monitoring started..."
            LblStatusMessage.ForeColor = Color.Orange

            backgroundTempMeasurements.Clear() ' Vorherige Daten löschen
            cts = New CancellationTokenSource()
            monitoringTask = Task.Run(Sub() RecordTemperaturesInBackground(cts.Token))

            ' Optional: Deaktivieren Sie den normalen RefreshTimer, um Konflikte zu vermeiden
            refreshTimer.Stop()
            'Me.Invoke(Sub() ClearCpuDisplayControls()) ' Optional: UI leeren während der Hintergrundmessung

        Else


        End If

        Return Task.CompletedTask
    End Function

    Private Async Function StopmonitoringAsync() As Task
        ' Diagramm anzeigen und Daten speichern
        If backgroundTempMeasurements.Any() Then
            ' Daten in CSV speichern
            SaveTemperatureDataToCsv(backgroundTempMeasurements)

            ' Neues Fenster mit Diagramm anzeigen
            Dim chartForm As New Form2(backgroundTempMeasurements)
            chartForm.Show()
        Else
            MessageBox.Show("No temperature data recorded.", "Monitoring Stopped", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' Optional: Aktivieren Sie den normalen RefreshTimer wieder
        refreshTimer.Start()
        LblStatusMessage.Text = "Real-time monitoring started. Static info saved."
        LblStatusMessage.ForeColor = Color.Green
        Await ReadAndDisplaySystemInfoAsync()
        InitializePerCoreCounters()
        InitializeCoreTemperatureSensors()
    End Function
    Private Async Sub StopMonitoringProcess()
        If Not isMonitoringActive Then Exit Sub ' Nur stoppen, wenn es aktiv ist

        isMonitoringActive = False
        BtnToggleMonitoring.Text = "Start Temp. Monitoring"
        LblStatusMessage.Text = "Background temperature monitoring stopped. Preparing chart..."
        LblStatusMessage.ForeColor = Color.DarkOrange

        ' Ladeformular schließen, falls es offen ist
        If loadingForm IsNot Nothing AndAlso Not loadingForm.IsDisposed Then
            loadingForm.Close()
            loadingForm = Nothing ' Referenz löschen
        End If

        cts?.Cancel() ' Hintergrund-Task abbrechen
        Try
            If monitoringTask IsNot Nothing Then
                Await monitoringTask ' Warten, bis der Task beendet ist
            End If
        Catch ex As OperationCanceledException
            ' Dies ist normal, wenn der Task abgebrochen wird.
        Catch ex As Exception
            Debug.WriteLine($"Error awaiting monitoring task: {ex.Message}")
        End Try

        ' Diagramm anzeigen und Daten speichern
        If backgroundTempMeasurements.Any() Then
            ' Daten in CSV speichern und Dateipfad erhalten
            Dim savedFilePath As String = SaveTemperatureDataToCsv(backgroundTempMeasurements)

            If Not String.IsNullOrEmpty(savedFilePath) Then
                ' Neues Fenster mit Diagramm anzeigen und den Dateipfad übergeben
                ' Form2 muss dafür einen Konstruktor bekommen, der einen Dateipfad akzeptiert
                Dim chartForm As New Form2(savedFilePath) ' <-- Änderung hier
                chartForm.Show()
            Else
                MessageBox.Show("Failed to save temperature data.", "Monitoring Stopped", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("No temperature data recorded.", "Monitoring Stopped", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' Optional: Aktivieren Sie den normalen RefreshTimer wieder
        refreshTimer.Start()
        Me.Invoke(Sub() ReadAndDisplaySystemInfoAsync().Wait()) ' Optional: UI sofort aktualisieren
        Await ReadAndDisplaySystemInfoAsync() ' Async aufrufen
        InitializePerCoreCounters()
        InitializeCoreTemperatureSensors()

        LblStatusMessage.Text = "Real-time monitoring started. Static info saved."
        LblStatusMessage.ForeColor = Color.Green
    End Sub

    Private Sub LoadingForm_StopRequested(sender As Object, e As EventArgs)
        ' Dies wird aufgerufen, wenn der Benutzer auf den Stop-Button in Form3 klickt
        Call StopMonitoringProcess()
    End Sub

    Private Sub LoadArchivedMeasurementsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadArchivedMeasurementsToolStripMenuItem.Click
        Dim programDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim logDirectory As String = Path.Combine(programDirectory, "TemperatureLogs")

        If Not Directory.Exists(logDirectory) Then
            MessageBox.Show("Keine archivierten Messungen gefunden. Der Ordner 'TemperatureLogs' existiert nicht.", "Archiv leer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Instanz von Form4 erstellen und den Archivordner übergeben
        Using archiveSelectionForm As New Form4(logDirectory)
            ' Form4 als Dialog anzeigen
            Dim dialogResult As DialogResult = archiveSelectionForm.ShowDialog(Me) ' 'Me' setzt Form1 als Parent

            If dialogResult = DialogResult.OK Then
                ' Wenn der Benutzer auf "Auswählen" geklickt hat und eine Datei ausgewählt wurde
                Dim selectedFilePath As String = archiveSelectionForm.SelectedFilePath

                If Not String.IsNullOrEmpty(selectedFilePath) Then
                    Try
                        ' Neues Fenster mit Diagramm anzeigen und den Dateipfad übergeben
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
                ' Der Benutzer hat auf "Abbrechen" geklickt
                LblStatusMessage.Text = "Auswahl abgebrochen."
                LblStatusMessage.ForeColor = Color.Gray
            End If
        End Using
    End Sub
End Class