' SystemInfoData.vb
' In a separate file like SystemInfoData.vb or at the top of your Form1.vb
Public Class SystemInfoData
    Public Property Id As Integer
    Public Property Timestamp As DateTime
    Public Property OSSystem As String
    Public Property SystemType As String
    Public Property ComputerName As String
    Public Property UserName As String
    Public Property DomainName As String
    Public Property ProcessorCount As Integer
    Public Property TotalPhysicalMemory As String
    Public Property AvailablePhysicalMemory As String
    Public Property HostName As String
    Public Property IPAddresses As String
    Public Property SystemDirectory As String
    Public Property ProgramDirectory As String
    Public Property NetworkAdapterNames As String
    Public Property NetworkAdapterMacAddresses As String
    Public Property BIOSVersion As String
    Public Property ProcessorInformation As String
    Public Property GraphicsCardInformation As String

    ' New properties for CPU details
    Public Property CpuName As String
    Public Property NumberOfCores As Integer
    Public Property NumberOfLogicalProcessors As Integer
    Public Property CurrentClockSpeedMHz As Integer
    Public Property Architecture As String
End Class