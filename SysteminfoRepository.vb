' SystemInfoRepository.vb
Imports System.Data.SqlClient ' Für Microsoft SQL Server
Imports System.Diagnostics ' Für Debug.WriteLine
Imports System.Management ' Für WMI-Abfragen
Imports System.Net.NetworkInformation ' Für Netzwerkadapter-Informationen
Imports System.Net ' Für Host Name/IP-Adressen
Imports System.Linq ' Für LINQ-Erweiterungsmethoden wie .Join

Public Class SystemInfoRepository

    ' !! WICHTIG !!
    ' Ersetzen Sie dies durch Ihre tatsächliche SQL Server Verbindungszeichenfolge.
    ' Beispiele:
    ' Lokaler SQL Server Express/LocalDB: "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SystemInfoDB;Integrated Security=True;"
    ' Standard SQL Server Instanz: "Data Source=YOUR_SERVER_NAME;Initial Catalog=YourDatabaseName;Integrated Security=True;"
    ' Mit SQL Server Authentifizierung: "Data Source=YOUR_SERVER_NAME;Initial Catalog=YourDatabaseName;User ID=YourUser;Password=YourPassword;"
    Private Const CONNECTION_STRING As String = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SystemInfoDB;Integrated Security=True;"

    Public Sub New()
        InitializeDatabase()
    End Sub

    ''' <summary>
    ''' Initialisiert die Datenbank, indem die Tabelle 'SystemInfoReadings' erstellt wird, falls sie noch nicht existiert.
    ''' </summary>
    Private Sub InitializeDatabase()
        Try
            Using connection As New SqlConnection(CONNECTION_STRING)
                connection.Open()

                Dim createTableSql As String = "
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SystemInfoReadings')
                BEGIN
                    CREATE TABLE SystemInfoReadings (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Timestamp DATETIME NOT NULL,
                        OSSystem NVARCHAR(255),
                        SystemType NVARCHAR(255),
                        ComputerName NVARCHAR(255),
                        UserName NVARCHAR(255),
                        DomainName NVARCHAR(255),
                        ProcessorCount INT,
                        TotalPhysicalMemory BIGINT, -- ULong in VB.NET passt zu BIGINT in SQL
                        AvailablePhysicalMemory BIGINT,
                        HostName NVARCHAR(255),
                        IPAddresses NVARCHAR(MAX), -- NVARCHAR(MAX) für potenziell lange Listen
                        SystemDirectory NVARCHAR(MAX),
                        ProgramDirectory NVARCHAR(MAX),
                        NetworkAdapterNames NVARCHAR(MAX),
                        NetworkAdapterMacAddresses NVARCHAR(MAX),
                        BIOSVersion NVARCHAR(255),
                        ProcessorInformation NVARCHAR(MAX),
                        GraphicsCardInformation NVARCHAR(MAX),
                        CpuName NVARCHAR(255),
                        NumberOfCores INT,
                        NumberOfLogicalProcessors INT,
                        CurrentClockSpeedMHz INT,
                        Architecture NVARCHAR(50)
                    );
                END"

                Using command As New SqlCommand(createTableSql, connection)
                    command.ExecuteNonQuery()
                    Debug.WriteLine("SystemInfoReadings table checked/created successfully.")
                End Using
            End Using
        Catch ex As SqlException
            ' Spezifische SQL-Fehler behandeln (z.B. Berechtigungsprobleme)
            MessageBox.Show($"Datenbankinitialisierungsfehler: {ex.Message}", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"SQL Datenbankinitialisierungsfehler: {ex.Message}")
        Catch ex As Exception
            ' Alle anderen allgemeinen Ausnahmen abfangen
            MessageBox.Show($"Ein unerwarteter Fehler bei der Datenbankinitialisierung ist aufgetreten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Allgemeiner Datenbankinitialisierungsfehler: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Speichert ein SystemInfoData-Objekt in der SQL Server-Datenbank.
    ''' </summary>
    ''' <param name="data">Das zu speichernde SystemInfoData-Objekt.</param>
    ''' <returns>True, wenn das Speichern erfolgreich war, False sonst.</returns>
    Public Function SaveSystemInfo(data As SystemInfoData) As Boolean
        Try
            Using connection As New SqlConnection(CONNECTION_STRING)
                connection.Open()

                Dim insertSql As String = "
                INSERT INTO SystemInfoReadings (
                    Timestamp, OSSystem, SystemType, ComputerName, UserName, DomainName,
                    ProcessorCount, TotalPhysicalMemory, AvailablePhysicalMemory, HostName,
                    IPAddresses, SystemDirectory, ProgramDirectory, NetworkAdapterNames,
                    NetworkAdapterMacAddresses, BIOSVersion, ProcessorInformation, GraphicsCardInformation,
                    CpuName, NumberOfCores, NumberOfLogicalProcessors, CurrentClockSpeedMHz, Architecture
                ) VALUES (
                    @Timestamp, @OSSystem, @SystemType, @ComputerName, @UserName, @DomainName,
                    @ProcessorCount, @TotalPhysicalMemory, @AvailablePhysicalMemory, @HostName,
                    @IPAddresses, @SystemDirectory, @ProgramDirectory, @NetworkAdapterNames,
                    @NetworkAdapterMacAddresses, @BIOSVersion, @ProcessorInformation, @GraphicsCardInformation,
                    @CpuName, @NumberOfCores, @NumberOfLogicalProcessors, @CurrentClockSpeedMHz, @Architecture
                )"

                Using command As New SqlCommand(insertSql, connection)
                    command.Parameters.AddWithValue("@Timestamp", data.Timestamp)
                    command.Parameters.AddWithValue("@OSSystem", data.OSSystem)
                    command.Parameters.AddWithValue("@SystemType", data.SystemType)
                    command.Parameters.AddWithValue("@ComputerName", data.ComputerName)
                    command.Parameters.AddWithValue("@UserName", data.UserName)
                    command.Parameters.AddWithValue("@DomainName", data.DomainName)
                    command.Parameters.AddWithValue("@ProcessorCount", data.ProcessorCount)
                    command.Parameters.AddWithValue("@TotalPhysicalMemory", data.TotalPhysicalMemory)
                    command.Parameters.AddWithValue("@AvailablePhysicalMemory", data.AvailablePhysicalMemory)
                    command.Parameters.AddWithValue("@HostName", data.HostName)
                    command.Parameters.AddWithValue("@IPAddresses", data.IPAddresses)
                    command.Parameters.AddWithValue("@SystemDirectory", data.SystemDirectory)
                    command.Parameters.AddWithValue("@ProgramDirectory", data.ProgramDirectory)
                    command.Parameters.AddWithValue("@NetworkAdapterNames", data.NetworkAdapterNames)
                    command.Parameters.AddWithValue("@NetworkAdapterMacAddresses", data.NetworkAdapterMacAddresses)
                    command.Parameters.AddWithValue("@BIOSVersion", data.BIOSVersion)
                    command.Parameters.AddWithValue("@ProcessorInformation", data.ProcessorInformation)
                    command.Parameters.AddWithValue("@GraphicsCardInformation", data.GraphicsCardInformation)
                    command.Parameters.AddWithValue("@CpuName", data.CpuName)
                    command.Parameters.AddWithValue("@NumberOfCores", data.NumberOfCores)
                    command.Parameters.AddWithValue("@NumberOfLogicalProcessors", data.NumberOfLogicalProcessors)
                    command.Parameters.AddWithValue("@CurrentClockSpeedMHz", data.CurrentClockSpeedMHz)
                    command.Parameters.AddWithValue("@Architecture", data.Architecture)

                    command.ExecuteNonQuery()
                    Debug.WriteLine("Systeminformationen erfolgreich gespeichert.")
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Speichern der Systeminformationen: {ex.Message}", "Datenbank Speicherfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"System Info Speicherfehler: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Ruft alle SystemInfoData-Datensätze aus der SQL Server-Datenbank ab.
    ''' </summary>
    ''' <returns>Eine Liste von SystemInfoData-Objekten.</returns>
    Public Function GetAllSystemInfoReadings() As List(Of SystemInfoData)
        Dim readings As New List(Of SystemInfoData)()
        Try
            Using connection As New SqlConnection(CONNECTION_STRING)
                connection.Open()
                Dim selectSql As String = "SELECT * FROM SystemInfoReadings ORDER BY Timestamp DESC" ' Neueste zuerst

                Using command As New SqlCommand(selectSql, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim data As New SystemInfoData()
                            data.Timestamp = reader.GetDateTime(reader.GetOrdinal("Timestamp"))
                            data.OSSystem = reader.GetString(reader.GetOrdinal("OSSystem"))
                            data.SystemType = reader.GetString(reader.GetOrdinal("SystemType"))
                            data.ComputerName = reader.GetString(reader.GetOrdinal("ComputerName"))
                            data.UserName = reader.GetString(reader.GetOrdinal("UserName"))
                            data.DomainName = reader.GetString(reader.GetOrdinal("DomainName"))
                            data.ProcessorCount = reader.GetInt32(reader.GetOrdinal("ProcessorCount"))
                            ' Verwenden Sie Convert.ToUInt64 für BIGINT-Spalten, da GetUInt64() nicht immer verfügbar ist
                            data.TotalPhysicalMemory = Convert.ToUInt64(reader.GetValue(reader.GetOrdinal("TotalPhysicalMemory")))
                            data.AvailablePhysicalMemory = Convert.ToUInt64(reader.GetValue(reader.GetOrdinal("AvailablePhysicalMemory")))
                            data.HostName = reader.GetString(reader.GetOrdinal("HostName"))
                            data.IPAddresses = reader.GetString(reader.GetOrdinal("IPAddresses"))
                            data.SystemDirectory = reader.GetString(reader.GetOrdinal("SystemDirectory"))
                            data.ProgramDirectory = reader.GetString(reader.GetOrdinal("ProgramDirectory"))
                            data.NetworkAdapterNames = reader.GetString(reader.GetOrdinal("NetworkAdapterNames"))
                            data.NetworkAdapterMacAddresses = reader.GetString(reader.GetOrdinal("NetworkAdapterMacAddresses"))
                            data.BIOSVersion = reader.GetString(reader.GetOrdinal("BIOSVersion"))
                            data.ProcessorInformation = reader.GetString(reader.GetOrdinal("ProcessorInformation"))
                            data.GraphicsCardInformation = reader.GetString(reader.GetOrdinal("GraphicsCardInformation"))
                            data.CpuName = reader.GetString(reader.GetOrdinal("CpuName"))
                            data.NumberOfCores = reader.GetInt32(reader.GetOrdinal("NumberOfCores"))
                            data.NumberOfLogicalProcessors = reader.GetInt32(reader.GetOrdinal("NumberOfLogicalProcessors"))
                            data.CurrentClockSpeedMHz = reader.GetInt32(reader.GetOrdinal("CurrentClockSpeedMHz"))
                            data.Architecture = reader.GetString(reader.GetOrdinal("Architecture"))

                            readings.Add(data)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Abrufen der Systeminformationen aus der Datenbank: {ex.Message}", "Datenbank Abruffehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"SQL Abruffehler: {ex.Message}")
        End Try
        Return readings
    End Function

    ''' <summary>
    ''' Sammelt aktuelle Systeminformationen über WMI und andere .NET-APIs.
    ''' </summary>
    ''' <returns>Ein SystemInfoData-Objekt mit aktuellen Systemdetails, oder Nothing, wenn ein Fehler auftritt.</returns>
    Public Function FetchLiveSystemInfo() As SystemInfoData
        Dim data As New SystemInfoData()
        Try
            data.Timestamp = DateTime.Now
            data.HostName = Dns.GetHostName() ' Hostname abrufen
            data.ProgramDirectory = AppDomain.CurrentDomain.BaseDirectory ' Programmverzeichnis

            ' OS-Informationen (Caption, OSArchitecture, SystemDirectory, TotalPhysicalMemory, FreePhysicalMemory)
            Using searcher As New ManagementObjectSearcher("SELECT Caption, OSArchitecture, SystemDirectory, TotalPhysicalMemory, FreePhysicalMemory FROM Win32_OperatingSystem")
                For Each mo As ManagementObject In searcher.Get()
                    data.OSSystem = mo("Caption")?.ToString()
                    data.Architecture = mo("OSArchitecture")?.ToString()
                    data.SystemDirectory = mo("SystemDirectory")?.ToString()
                    If mo("TotalPhysicalMemory") IsNot Nothing Then ULong.TryParse(mo("TotalPhysicalMemory").ToString(), data.TotalPhysicalMemory)
                    If mo("FreePhysicalMemory") IsNot Nothing Then ULong.TryParse(mo("FreePhysicalMemory").ToString(), data.AvailablePhysicalMemory)
                    Exit For
                Next
            End Using

            ' Computer System Informationen (Model, Manufacturer, UserName, Domain)
            Using searcher As New ManagementObjectSearcher("SELECT Model, Manufacturer, UserName, Domain FROM Win32_ComputerSystem")
                For Each mo As ManagementObject In searcher.Get()
                    data.SystemType = mo("Model")?.ToString()  ' Modell als Systemtyp
                    data.ComputerName = mo("Name")?.ToString()  ' Computername
                    data.UserName = mo("UserName")?.ToString()
                    data.DomainName = mo("Domain")?.ToString()
                    Exit For
                Next
            End Using

            ' Prozessor-Informationen (Name, NumberOfCores, NumberOfLogicalProcessors, MaxClockSpeed, Manufacturer, CurrentClockSpeed, Description)
            Using searcher As New ManagementObjectSearcher("SELECT Name, NumberOfCores, NumberOfLogicalProcessors, MaxClockSpeed, Manufacturer, CurrentClockSpeed, Description FROM Win32_Processor")
                For Each mo As ManagementObject In searcher.Get()
                    data.CpuName = mo("Name")?.ToString()
                    If mo("NumberOfCores") IsNot Nothing Then Integer.TryParse(mo("NumberOfCores").ToString(), data.NumberOfCores)
                    If mo("NumberOfLogicalProcessors") IsNot Nothing Then Integer.TryParse(mo("NumberOfLogicalProcessors").ToString(), data.NumberOfLogicalProcessors)
                    If mo("CurrentClockSpeed") IsNot Nothing Then Integer.TryParse(mo("CurrentClockSpeed").ToString(), data.CurrentClockSpeedMHz) ' In MHz
                    data.ProcessorInformation = mo("Description")?.ToString()  ' Allgemeine Beschreibung
                    Exit For
                Next
            End Using
            data.ProcessorCount = Environment.ProcessorCount ' Verwendung von Environment.ProcessorCount für logische Prozessoren

            ' Netzwerkadapter-Informationen
            Dim ipAddressesList As New List(Of String)()
            Dim networkAdapterNamesList As New List(Of String)()
            Dim macAddressesList As New List(Of String)()

            For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                If ni.OperationalStatus = OperationalStatus.Up AndAlso ni.NetworkInterfaceType <> NetworkInterfaceType.Loopback Then
                    networkAdapterNamesList.Add(ni.Name)
                    If ni.GetPhysicalAddress().ToString() <> String.Empty Then
                        macAddressesList.Add(BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes()).Replace("-", ":"))
                    End If

                    For Each ip In ni.GetIPProperties().UnicastAddresses
                        If ip.Address.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then ' Nur IPv4
                            ipAddressesList.Add(ip.Address.ToString())
                        End If
                    Next
                End If
            Next
            data.IPAddresses = String.Join(";", ipAddressesList)
            data.NetworkAdapterNames = String.Join(";", networkAdapterNamesList)
            data.NetworkAdapterMacAddresses = String.Join(";", macAddressesList)

            ' BIOS-Informationen
            Using searcher As New ManagementObjectSearcher("SELECT SMBIOSBIOSVersion FROM Win32_BIOS")
                For Each mo As ManagementObject In searcher.Get()
                    data.BIOSVersion = mo("SMBIOSBIOSVersion")?.ToString()
                    Exit For
                Next
            End Using

            ' Grafikkarten-Informationen
            Dim gpuInfoList As New List(Of String)()
            Using searcher As New ManagementObjectSearcher("SELECT Name, AdapterRAM FROM Win32_VideoController")
                For Each mo As ManagementObject In searcher.Get()
                    Dim gpuName As String = mo("Name")?.ToString()
                    Dim adapterRAM As ULong = 0
                    If mo("AdapterRAM") IsNot Nothing Then ULong.TryParse(mo("AdapterRAM").ToString(), adapterRAM)
                    gpuInfoList.Add($"{gpuName} ({(adapterRAM / (1024.0 * 1024 * 1024)):F2} GB)")
                Next
            End Using
            data.GraphicsCardInformation = String.Join(";", gpuInfoList)

        Catch ex As Exception
            MessageBox.Show($"Fehler beim Abrufen aktueller Systeminformationen: {ex.Message}", "System Info Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Abruffehler der Live-Systeminformationen: {ex.Message}")
            Return Nothing ' Bei Fehler Nothing zurückgeben
        End Try
        Return data
    End Function

End Class