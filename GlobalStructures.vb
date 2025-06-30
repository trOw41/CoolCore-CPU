' SharedTypes.vb

Public Module SharedTypes

    Public Structure LogEntry
        Public Property Timestamp As DateTime
        Public Property CpuName As String
        Public Property Core As String
        Public Property MinTemp As Single
        Public Property MaxTemp As Single
        Public Property CurrentTemp As Single
    End Structure

End Module
