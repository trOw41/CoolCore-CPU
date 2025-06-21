' CpuinfoForm.vb
Imports System.Collections.Generic
Imports System.Drawing.Printing

Public Class CpuinfoForm
    Private printDocument As New Printing.PrintDocument()
    Private printPreviewDialog As New PrintPreviewDialog()
    Private cpuInfoToPrint As String
    Public Sub LoadCpuInfo(ByVal cpuData As Dictionary(Of String, String))
        InfoList.View = View.Details
        If InfoList.Columns.Count = 0 Then
            InfoList.Columns.Add("ID", 200)
            InfoList.Columns.Add("Eigenschaft:", 300)
        End If
        InfoList.Items.Clear()

        For Each entry In cpuData
            Dim item As New ListViewItem(entry.Key)
            item.SubItems.Add(entry.Value)
            InfoList.Items.Add(item)
        Next
        InfoList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        InfoList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Private Sub CpuinfoForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cpuInfoToPrint = ""
        If InfoList.Items.Count = 0 Then
            MessageBox.Show("Keine CPU-Informationen zum Drucken vorhanden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        cpuInfoToPrint = "CPU-Informationen:" & Environment.NewLine & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        For i = 0 To InfoList.Items.Count - 1
            Dim item As ListViewItem = InfoList.Items(i)
            cpuInfoToPrint &= item.Text & ": " & item.SubItems(1).Text & Environment.NewLine
        Next
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "" & Environment.NewLine
        cpuInfoToPrint &= "---------------------------------------------------------" & Environment.NewLine
        cpuInfoToPrint &= "Datum: " & DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") & Environment.NewLine & Environment.NewLine
        cpuInfoToPrint &= Environment.NewLine & "Hinweis: Diese Informationen wurden automatisch gesammelt und können je nach System variieren." & Environment.NewLine
        cpuInfoToPrint &= $"{Date.Now.Year} © CoolCore-CPU®" & Environment.NewLine
        AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage
        PrintPreviewDialog.Document = printDocument
        PrintPreviewDialog.ShowDialog()
        RemoveHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        Dim font As New Font("Segoe UI", 11)
        e.Graphics.DrawString(cpuInfoToPrint, font, Brushes.Black, 50, 50)
    End Sub
End Class