' SystemInfoData.vb
Imports System.Collections.Generic
Imports System.Management
Imports System.Net.NetworkInformation
Imports System.Net
Imports System.Linq
Imports System.IO

Public Class SystemInfoData
    Public Property Timestamp As DateTime
    Public Property OSSystem As String
    Public Property SystemType As String ' z.B. Desktop, Laptop
    Public Property ComputerName As String
    Public Property UserName As String
    Public Property DomainName As String
    Public Property ProcessorCount As Integer ' Anzahl logischer Prozessoren (Threads)
    Public Property TotalPhysicalMemory As ULong ' In Bytes
    Public Property AvailablePhysicalMemory As ULong ' In Bytes
    Public Property HostName As String
    Public Property IPAddresses As String ' Semikolon-getrennte Liste von IP-Adressen
    Public Property SystemDirectory As String
    Public Property ProgramDirectory As String
    Public Property NetworkAdapterNames As String ' Semikolon-getrennte Liste von Adapternamen
    Public Property NetworkAdapterMacAddresses As String ' Semikolon-getrennte Liste von MAC-Adressen
    Public Property BIOSVersion As String
    Public Property ProcessorInformation As String ' Allgemeine Prozessor-Beschreibung
    Public Property GraphicsCardInformation As String ' Allgemeine Grafikkarten-Beschreibung (kann mehrere GPUs enthalten)
    Public Property CpuName As String
    Public Property NumberOfCores As Integer
    Public Property NumberOfLogicalProcessors As Integer
    Public Property CurrentClockSpeedMHz As Integer
    Public Property Architecture As String ' z.B. "x64", "x86"

    ' Standardkonstruktor zur Initialisierung von Eigenschaften mit Standardwerten
    Public Sub New()
        Timestamp = DateTime.Now
        OSSystem = ""
        SystemType = ""
        ComputerName = ""
        UserName = ""
        DomainName = ""
        ProcessorCount = 0
        TotalPhysicalMemory = 0
        AvailablePhysicalMemory = 0
        HostName = ""
        IPAddresses = ""
        SystemDirectory = ""
        ProgramDirectory = ""
        NetworkAdapterNames = ""
        NetworkAdapterMacAddresses = ""
        BIOSVersion = ""
        ProcessorInformation = ""
        GraphicsCardInformation = ""
        CpuName = ""
        NumberOfCores = 0
        NumberOfLogicalProcessors = 0
        CurrentClockSpeedMHz = 0
        Architecture = ""
    End Sub

End Class