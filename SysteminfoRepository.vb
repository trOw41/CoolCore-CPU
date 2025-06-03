' SystemInfoRepository.vb
Imports System.Data.SQLite
Imports System.Diagnostics
Imports System.Management
Imports System.Net.NetworkInformation
Imports System.Net
Imports System.Linq
Imports System.IO

Public Class SystemInfoRepository
    Private Const DB_FILE_NAME As String = "SystemInfo.sqlite"
    Private ReadOnly CONNECTION_STRING As String = $"Data Source={DB_FILE_NAME};Version=3;"

    Public Sub New()
        If Not File.Exists(DB_FILE_NAME) Then
            SQLiteConnection.CreateFile(DB_FILE_NAME)
        End If
        InitializeDatabase()
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub InitializeDatabase()
        Try
            Using connection As New SQLiteConnection(CONNECTION_STRING)
                connection.Open()

                Dim createTableSql As String =
                    "CREATE TABLE IF NOT EXISTS SystemInfoReadings (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
                        HostName TEXT,
                        IPAddress TEXT,
                        OperatingSystem TEXT,
                        CPUName TEXT,
                        NumberOfLogicalProcessors INTEGER,
                        TotalPhysicalMemoryGB REAL,
                        AvailablePhysicalMemoryGB REAL,
                        BIOSVersion TEXT,
                        GraphicsCardInformation TEXT,
                        NetworkAdapterMacAddresses TEXT,
                        CpuLoad REAL,
                        CPUCoreTemperatures TEXT,
                        GPUCoreTemperatures TEXT,
                        DiskSpaceInformation TEXT,
                        Uptime TEXT,
                        ProcessesRunning INTEGER,
                        ServicesRunning INTEGER,
                        NetworkTrafficSentMB REAL,
                        NetworkTrafficReceivedMB REAL,
                        InstalledSoftware TEXT,
                        AntivirusStatus TEXT,
                        FirewallStatus TEXT
                    );"

                Using command As New SQLiteCommand(createTableSql, connection)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SQLiteException
            MessageBox.Show($"Datenbankinitialisierungsfehler (SQLite): {ex.Message}", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"SQLite Database Initialization Error: {ex.Message}")
        Catch ex As Exception
            MessageBox.Show($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "Allgemeiner Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"General Error during DB Init: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Speichert die aktuellen Systeminformationen in der Datenbank.
    ''' </summary>
    ''' <param name="data">Die zu speichernden Systeminformationen.</param>
    Public Sub SaveSystemInfo(data As SystemInfoData)
        Try
            Using connection As New SQLiteConnection(CONNECTION_STRING)
                connection.Open()

                Dim insertSql As String =
                    "INSERT INTO SystemInfoReadings (
                        Timestamp, HostName, IPAddress, OperatingSystem, CPUName, NumberOfLogicalProcessors,
                        TotalPhysicalMemoryGB, AvailablePhysicalMemoryGB, BIOSVersion, GraphicsCardInformation,
                        NetworkAdapterMacAddresses, CpuLoad, CPUCoreTemperatures, GPUCoreTemperatures,
                        DiskSpaceInformation, Uptime, ProcessesRunning, ServicesRunning,
                        NetworkTrafficSentMB, NetworkTrafficReceivedMB, InstalledSoftware, AntivirusStatus, FirewallStatus
                    ) VALUES (
                        @Timestamp, @HostName, @IPAddress, @OperatingSystem, @CPUName, @NumberOfLogicalProcessors,
                        @TotalPhysicalMemoryGB, @AvailablePhysicalMemoryGB, @BIOSVersion, @GraphicsCardInformation,
                        @NetworkAdapterMacAddresses, @CpuLoad, @CPUCoreTemperatures, @GPUCoreTemperatures,
                        @DiskSpaceInformation, @Uptime, @ProcessesRunning, @ServicesRunning,
                        @NetworkTrafficSentMB, @NetworkTrafficReceivedMB, @InstalledSoftware, @AntivirusStatus, @FirewallStatus
                    );"

                Using command As New SQLiteCommand(insertSql, connection)

                    command.Parameters.AddWithValue("@Timestamp", data.Timestamp)
                    command.Parameters.AddWithValue("@HostName", data.HostName)
                    command.Parameters.AddWithValue("@IPAddress", data.IPAddress)
                    command.Parameters.AddWithValue("@OperatingSystem", data.OperatingSystem)
                    command.Parameters.AddWithValue("@CPUName", data.CpuName)
                    command.Parameters.AddWithValue("@NumberOfLogicalProcessors", data.NumberOfLogicalProcessors)
                    command.Parameters.AddWithValue("@TotalPhysicalMemoryGB", data.TotalPhysicalMemoryGB)
                    command.Parameters.AddWithValue("@AvailablePhysicalMemoryGB", data.AvailablePhysicalMemoryGB)
                    command.Parameters.AddWithValue("@BIOSVersion", data.BIOSVersion)
                    command.Parameters.AddWithValue("@GraphicsCardInformation", data.GraphicsCardInformation)
                    command.Parameters.AddWithValue("@NetworkAdapterMacAddresses", data.NetworkAdapterMacAddresses)
                    command.Parameters.AddWithValue("@CpuLoad", data.CpuLoad)
                    command.Parameters.AddWithValue("@CPUCoreTemperatures", data.CPUCoreTemperatures)
                    command.Parameters.AddWithValue("@GPUCoreTemperatures", data.GPUCoreTemperatures)
                    command.Parameters.AddWithValue("@DiskSpaceInformation", data.DiskSpaceInformation)
                    command.Parameters.AddWithValue("@Uptime", data.Uptime)
                    command.Parameters.AddWithValue("@ProcessesRunning", data.ProcessesRunning)
                    command.Parameters.AddWithValue("@ServicesRunning", data.ServicesRunning)
                    command.Parameters.AddWithValue("@NetworkTrafficSentMB", data.NetworkTrafficSentMB)
                    command.Parameters.AddWithValue("@NetworkTrafficReceivedMB", data.NetworkTrafficReceivedMB)
                    command.Parameters.AddWithValue("@InstalledSoftware", data.InstalledSoftware)
                    command.Parameters.AddWithValue("@AntivirusStatus", data.AntivirusStatus)
                    command.Parameters.AddWithValue("@FirewallStatus", data.FirewallStatus)

                    command.ExecuteNonQuery()
                End Using
            End Using
            Debug.WriteLine("Systeminformationen erfolgreich in SQLite gespeichert.")
        Catch ex As SQLiteException
            MessageBox.Show($"Fehler beim Speichern der Systeminformationen (SQLite): {ex.Message}", "Speicherfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"SQLite Save Error: {ex.Message}")
        Catch ex As Exception
            MessageBox.Show($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "Allgemeiner Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"General Error during Save: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Ruft die letzten gespeicherten Systeminformationen aus der Datenbank ab.
    ''' </summary>
    ''' <returns>Die zuletzt gespeicherten Systeminformationen oder Nothing, wenn keine vorhanden sind.</returns>
    Public Function GetLastSystemInfo() As SystemInfoData
        Dim data As New SystemInfoData()
        Try
            Using connection As New SQLiteConnection(CONNECTION_STRING)
                connection.Open()

                Dim selectSql As String = "SELECT * FROM SystemInfoReadings ORDER BY Timestamp DESC LIMIT 1;"

                Using command As New SQLiteCommand(selectSql, connection)
                    Using reader As SQLiteDataReader = command.ExecuteReader()
                        If reader.Read() Then

                            data.Timestamp = If(reader("Timestamp") Is DBNull.Value, DateTime.MinValue, CDate(reader("Timestamp")))
                            data.HostName = If(reader("HostName") Is DBNull.Value, Nothing, CStr(reader("HostName")))
                            data.IPAddress = If(reader("IPAddress") Is DBNull.Value, Nothing, CStr(reader("IPAddress")))
                            data.OperatingSystem = If(reader("OperatingSystem") Is DBNull.Value, Nothing, CStr(reader("OperatingSystem")))
                            data.CpuName = If(reader("CPUName") Is DBNull.Value, Nothing, CStr(reader("CPUName")))
                            data.NumberOfLogicalProcessors = If(reader("NumberOfLogicalProcessors") Is DBNull.Value, 0, CInt(reader("NumberOfLogicalProcessors")))
                            data.TotalPhysicalMemoryGB = If(reader("TotalPhysicalMemoryGB") Is DBNull.Value, 0.0, CSng(reader("TotalPhysicalMemoryGB")))
                            data.AvailablePhysicalMemoryGB = If(reader("AvailablePhysicalMemoryGB") Is DBNull.Value, 0.0, CSng(reader("AvailablePhysicalMemoryGB")))
                            data.BIOSVersion = If(reader("BIOSVersion") Is DBNull.Value, Nothing, CStr(reader("BIOSVersion")))
                            data.GraphicsCardInformation = If(reader("GraphicsCardInformation") Is DBNull.Value, Nothing, CStr(reader("GraphicsCardInformation")))
                            data.NetworkAdapterMacAddresses = If(reader("NetworkAdapterMacAddresses") Is DBNull.Value, Nothing, CStr(reader("NetworkAdapterMacAddresses")))
                            data.CpuLoad = If(reader("CpuLoad") Is DBNull.Value, 0.0, CSng(reader("CpuLoad")))
                            data.CPUCoreTemperatures = If(reader("CPUCoreTemperatures") Is DBNull.Value, Nothing, CStr(reader("CPUCoreTemperatures")))
                            data.GPUCoreTemperatures = If(reader("GPUCoreTemperatures") Is DBNull.Value, Nothing, CStr(reader("GPUCoreTemperatures")))
                            data.DiskSpaceInformation = If(reader("DiskSpaceInformation") Is DBNull.Value, Nothing, CStr(reader("DiskSpaceInformation")))
                            data.Uptime = If(reader("Uptime") Is DBNull.Value, Nothing, CStr(reader("Uptime")))
                            data.ProcessesRunning = If(reader("ProcessesRunning") Is DBNull.Value, 0, CInt(reader("ProcessesRunning")))
                            data.ServicesRunning = If(reader("ServicesRunning") Is DBNull.Value, 0, CInt(reader("ServicesRunning")))
                            data.NetworkTrafficSentMB = If(reader("NetworkTrafficSentMB") Is DBNull.Value, 0.0, CSng(reader("NetworkTrafficSentMB")))
                            data.NetworkTrafficReceivedMB = If(reader("NetworkTrafficReceivedMB") Is DBNull.Value, 0.0, CSng(reader("NetworkTrafficReceivedMB")))
                            data.InstalledSoftware = If(reader("InstalledSoftware") Is DBNull.Value, Nothing, CStr(reader("InstalledSoftware")))
                            data.AntivirusStatus = If(reader("AntivirusStatus") Is DBNull.Value, Nothing, CStr(reader("AntivirusStatus")))
                            data.FirewallStatus = If(reader("FirewallStatus") Is DBNull.Value, Nothing, CStr(reader("FirewallStatus")))
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using
            End Using
        Catch ex As SQLiteException
            MessageBox.Show($"Fehler beim Abrufen der letzten Systeminformationen (SQLite): {ex.Message}", "Abruffehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"SQLite GetLast Error: {ex.Message}")
            Return Nothing
        Catch ex As Exception
            MessageBox.Show($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "Allgemeiner Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"General Error during GetLast: {ex.Message}")
            Return Nothing
        End Try
        Return data
    End Function

    ''' <summary>
    ''' </summary>
    ''' <returns>Ein SystemInfoData-Objekt mit den aktuellen Informationen.</returns>
    Public Function GetCurrentSystemInfo() As SystemInfoData
        Dim data As New SystemInfoData()
        Try
            data.Timestamp = DateTime.Now
            data.HostName = Dns.GetHostName()
            Dim ipAddresses As IPHostEntry = Dns.GetHostEntry(data.HostName)
            Dim ipv4Address As Net.IPAddress = ipAddresses.AddressList.FirstOrDefault(Function(ip) ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork)
            If ipv4Address IsNot Nothing Then
                data.IPAddress = ipv4Address.ToString()
            Else
                data.IPAddress = "N/A"
            End If

            Using searcher As New ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem")
                For Each mo As ManagementObject In searcher.Get()
                    data.OperatingSystem = mo("Caption")?.ToString()
                    Exit For
                Next
            End Using

            Using searcher As New ManagementObjectSearcher("SELECT Name, NumberOfLogicalProcessors FROM Win32_Processor")
                For Each mo As ManagementObject In searcher.Get()
                    data.CpuName = mo("Name")?.ToString()
                    If mo("NumberOfLogicalProcessors") IsNot Nothing Then
                        data.NumberOfLogicalProcessors = CInt(mo("NumberOfLogicalProcessors"))
                    End If
                    Exit For
                Next
            End Using

            Using searcher As New ManagementObjectSearcher("SELECT SMBIOSBIOSVersion FROM Win32_BIOS")
                For Each mo As ManagementObject In searcher.Get()
                    data.BIOSVersion = mo("SMBIOSBIOSVersion")?.ToString()
                    Exit For
                Next
            End Using


            Using searcher As New ManagementObjectSearcher("SELECT Name FROM Win32_VideoController")
                Dim graphicsInfo As New List(Of String)()
                For Each mo As ManagementObject In searcher.Get()
                    If mo("Name") IsNot Nothing Then
                        graphicsInfo.Add(mo("Name").ToString())
                    End If
                Next
                data.GraphicsCardInformation = String.Join(", ", graphicsInfo)
            End Using


            Dim macAddresses As New List(Of String)()
            For Each nic As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                If nic.OperationalStatus = OperationalStatus.Up Then
                    Dim macAddress As String = String.Join(":", nic.GetPhysicalAddress().GetAddressBytes().Select(Function(b) b.ToString("X2")))
                    macAddresses.Add(macAddress)
                End If
            Next
            data.NetworkAdapterMacAddresses = String.Join(", ", macAddresses)

            If String.IsNullOrEmpty(data.NetworkAdapterMacAddresses) Then
                data.NetworkAdapterMacAddresses = "Keine aktiven Netzwerkadapter gefunden."
            End If

            Dim networkSent As Double = 0
            Dim networkReceived As Double = 0
            Try
                Dim networkCounters As New List(Of PerformanceCounter)()
                For Each nic In NetworkInterface.GetAllNetworkInterfaces()
                    If nic.OperationalStatus = OperationalStatus.Up AndAlso nic.NetworkInterfaceType <> NetworkInterfaceType.Loopback Then
                        networkCounters.Add(New PerformanceCounter("Network Interface", "Bytes Sent/sec", nic.Description))
                        networkCounters.Add(New PerformanceCounter("Network Interface", "Bytes Received/sec", nic.Description))
                    End If
                Next

                For Each pc In networkCounters
                    pc.NextValue()
                Next
                System.Threading.Thread.Sleep(1000)

                For Each pc In networkCounters
                    If pc.CounterName = "Bytes Sent/sec" Then
                        networkSent += pc.NextValue()
                    ElseIf pc.CounterName = "Bytes Received/sec" Then
                        networkReceived += pc.NextValue()
                    End If
                Next
            Catch ex As Exception
                Debug.WriteLine($"Fehler beim Abrufen des Netzwerkverkehrs: {ex.Message}")
            End Try
            data.NetworkTrafficSentMB = networkSent / (1024.0 * 1024)
            data.NetworkTrafficReceivedMB = networkReceived / (1024.0 * 1024)

        Catch ex As Exception
            MessageBox.Show($"Fehler beim Abrufen aktueller Systeminformationen: {ex.Message}", "System Info Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Abruffehler der Live-Systeminformationen: {ex.Message}")
            Return Nothing
        End Try
        Return data
    End Function

End Class

