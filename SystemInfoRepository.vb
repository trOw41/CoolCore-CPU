Imports System.Data.SQLite ' Ensure you have this NuGet package installed (System.Data.SQLite.Core)
Imports System.IO
Imports System.Diagnostics ' For Debug.WriteLine

Public Class SystemInfoRepository

    Private Const DB_FILE_NAME As String = "SystemInfo.db"
    Private ReadOnly DB_PATH As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DB_FILE_NAME)
    Private ReadOnly CONNECTION_STRING As String = $"Data Source={DB_PATH};Version=3;"

    Public Sub New()
        InitializeDatabase()
    End Sub

    Private Sub InitializeDatabase()
        Try
            If Not File.Exists(DB_PATH) Then
                SQLiteConnection.CreateFile(DB_PATH)
                Console.WriteLine($"Database file '{DB_FILE_NAME}' created.")
            End If

            Using connection As New SQLiteConnection(CONNECTION_STRING)
                connection.Open()

                ' Create table if it doesn't exist (initial creation)
                Dim createTableSql As String = "
                CREATE TABLE IF NOT EXISTS SystemInfoReadings (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Timestamp TEXT NOT NULL,
                    OSSystem TEXT,
                    SystemType TEXT,
                    ComputerName TEXT,
                    UserName TEXT,
                    DomainName TEXT,
                    ProcessorCount INTEGER,
                    TotalPhysicalMemory TEXT,
                    AvailablePhysicalMemory TEXT,
                    HostName TEXT,
                    IPAddresses TEXT,
                    SystemDirectory TEXT,
                    ProgramDirectory TEXT,
                    NetworkAdapterNames TEXT,
                    NetworkAdapterMacAddresses TEXT,
                    BIOSVersion TEXT,
                    ProcessorInformation TEXT,
                    GraphicsCardInformation TEXT,
                    -- New CPU columns
                    CpuName TEXT,
                    NumberOfCores INTEGER,
                    NumberOfLogicalProcessors INTEGER,
                    CurrentClockSpeedMHz INTEGER,
                    Architecture TEXT
                );"

                Using command As New SQLiteCommand(createTableSql, connection)
                    command.ExecuteNonQuery()
                End Using
                Console.WriteLine("Table 'SystemInfoReadings' checked/created.")

                ' Add new columns if they don't exist (for existing databases)
                AddNewColumns(connection)

            End Using
        Catch ex As Exception
            MessageBox.Show($"Error during database initialization: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Database initialization error: {ex.ToString()}")
        End Try
    End Sub

    Private Sub AddNewColumns(connection As SQLiteConnection)
        Dim columnsToAdd As New Dictionary(Of String, String) From {
            {"CpuName", "TEXT"},
            {"NumberOfCores", "INTEGER"},
            {"NumberOfLogicalProcessors", "INTEGER"},
            {"CurrentClockSpeedMHz", "INTEGER"},
            {"Architecture", "TEXT"}
        }

        For Each column In columnsToAdd
            Dim alterTableSql As String = $"ALTER TABLE SystemInfoReadings ADD COLUMN {column.Key} {column.Value};"
            Try
                Using command As New SQLiteCommand(alterTableSql, connection)
                    command.ExecuteNonQuery()
                    Console.WriteLine($"Column '{column.Key}' added to 'SystemInfoReadings'.")
                End Using
            Catch ex As SQLiteException
                ' Ignore if column already exists (SQLITE_ERROR: duplicate column name)
                If Not ex.Message.Contains("duplicate column name") Then
                    Debug.WriteLine($"Error adding column {column.Key}: {ex.Message}")
                End If
            Catch ex As Exception
                Debug.WriteLine($"Generic error adding column {column.Key}: {ex.Message}")
            End Try
        Next
    End Sub


    Public Async Function SaveSystemInfoAsync(ByVal data As SystemInfoData) As Task
        If data Is Nothing Then
            Throw New ArgumentNullException("data", "SystemInfoData object cannot be null.")
        End If

        Try
            Using connection As New SQLiteConnection(CONNECTION_STRING)
                Await connection.OpenAsync()

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
                );"

                Using command As New SQLiteCommand(insertSql, connection)
                    command.Parameters.AddWithValue("@Timestamp", data.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"))
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

                    ' New CPU parameters
                    command.Parameters.AddWithValue("@CpuName", data.CpuName)
                    command.Parameters.AddWithValue("@NumberOfCores", data.NumberOfCores)
                    command.Parameters.AddWithValue("@NumberOfLogicalProcessors", data.NumberOfLogicalProcessors)
                    command.Parameters.AddWithValue("@CurrentClockSpeedMHz", data.CurrentClockSpeedMHz)
                    command.Parameters.AddWithValue("@Architecture", data.Architecture)

                    Await command.ExecuteNonQueryAsync()
                End Using
                Console.WriteLine("System information successfully saved to the database.")
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error saving system information: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Save error in Repository: {ex.ToString()}")
            Throw ' Re-throw the exception to allow calling code to handle it
        End Try
    End Function

    Public Async Function GetLastSystemInfoReadingsAsync(count As Integer) As Task(Of List(Of SystemInfoData))
        Dim readings As New List(Of SystemInfoData)()
        Try
            Using connection As New SQLiteConnection(CONNECTION_STRING)
                Await connection.OpenAsync()
                Dim selectSql As String = $"SELECT * FROM SystemInfoReadings ORDER BY Timestamp DESC LIMIT {count};"
                Using command As New SQLiteCommand(selectSql, connection)
                    Using reader As SQLiteDataReader = CType(Await command.ExecuteReaderAsync(), SQLiteDataReader)
                        While reader.Read()
                            Dim data As New SystemInfoData()
                            data.Id = reader.GetInt32(reader.GetOrdinal("Id"))
                            data.Timestamp = DateTime.Parse(reader.GetString(reader.GetOrdinal("Timestamp")))
                            data.OSSystem = reader.GetString(reader.GetOrdinal("OSSystem"))
                            data.SystemType = reader.GetString(reader.GetOrdinal("SystemType"))
                            data.ComputerName = reader.GetString(reader.GetOrdinal("ComputerName"))
                            data.UserName = reader.GetString(reader.GetOrdinal("UserName"))
                            data.DomainName = reader.GetString(reader.GetOrdinal("DomainName"))
                            data.ProcessorCount = reader.GetInt32(reader.GetOrdinal("ProcessorCount"))
                            data.TotalPhysicalMemory = reader.GetString(reader.GetOrdinal("TotalPhysicalMemory"))
                            data.AvailablePhysicalMemory = reader.GetString(reader.GetOrdinal("AvailablePhysicalMemory"))
                            data.HostName = reader.GetString(reader.GetOrdinal("HostName"))
                            data.IPAddresses = reader.GetString(reader.GetOrdinal("IPAddresses"))
                            data.SystemDirectory = reader.GetString(reader.GetOrdinal("SystemDirectory"))
                            data.ProgramDirectory = reader.GetString(reader.GetOrdinal("ProgramDirectory"))
                            data.NetworkAdapterNames = reader.GetString(reader.GetOrdinal("NetworkAdapterNames"))
                            data.NetworkAdapterMacAddresses = reader.GetString(reader.GetOrdinal("NetworkAdapterMacAddresses"))
                            data.BIOSVersion = reader.GetString(reader.GetOrdinal("BIOSVersion"))
                            data.ProcessorInformation = reader.GetString(reader.GetOrdinal("ProcessorInformation"))
                            data.GraphicsCardInformation = reader.GetString(reader.GetOrdinal("GraphicsCardInformation"))

                            ' Retrieve new CPU properties
                            data.CpuName = If(Not reader.IsDBNull(reader.GetOrdinal("CpuName")), reader.GetString(reader.GetOrdinal("CpuName")), "N/A")
                            data.NumberOfCores = If(Not reader.IsDBNull(reader.GetOrdinal("NumberOfCores")), reader.GetInt32(reader.GetOrdinal("NumberOfCores")), 0)
                            data.NumberOfLogicalProcessors = If(Not reader.IsDBNull(reader.GetOrdinal("NumberOfLogicalProcessors")), reader.GetInt32(reader.GetOrdinal("NumberOfLogicalProcessors")), 0)
                            data.CurrentClockSpeedMHz = If(Not reader.IsDBNull(reader.GetOrdinal("CurrentClockSpeedMHz")), reader.GetInt32(reader.GetOrdinal("CurrentClockSpeedMHz")), 0)
                            data.Architecture = If(Not reader.IsDBNull(reader.GetOrdinal("Architecture")), reader.GetString(reader.GetOrdinal("Architecture")), "N/A")

                            readings.Add(data)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error retrieving system information: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Retrieval error in Repository: {ex.ToString()}")
        End Try
        Return readings
    End Function

End Class