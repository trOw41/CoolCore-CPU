Imports System.Configuration
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Web.UI.DataVisualization.Charting
Imports System.Windows.Forms
Imports Google.Protobuf.WellKnownTypes
Imports Newtonsoft.Json
Imports Org.BouncyCastle.Asn1.Cms

Public Class ReadLog
    Private logDir As String = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory & "log")
    Private LogFilePath As String = ""
    Private Sub ReadLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.View = View.Details
        ListView1.GridLines = True
        ListView1.FullRowSelect = True
        LogFilePath = Settings.LogFilePath
    End Sub

    Public Sub LoadLogEntries(logEntries As List(Of LogEntry), headerLine As String)
        Debug.WriteLine("LogEntries count: " & logEntries.Count)
        Debug.WriteLine("Header: " & headerLine)
        ListView1.Items.Clear()
        ListView1.Columns.Clear()
        Dim headers() As String = headerLine.Split(";"c)
        For Each header As String In headers
            ListView1.Columns.Add(header.Trim())
        Next
        For Each entry As LogEntry In logEntries
            Dim item As New ListViewItem(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"))
            item.SubItems.Add(entry.Core)
            item.SubItems.Add(entry.CurrentTemp.ToString("F1"))
            item.SubItems.Add(entry.CurrentTemp.ToString("F1"))
            item.SubItems.Add(entry.MinTemp.ToString("F1"))
            item.SubItems.Add(entry.MaxTemp.ToString("F1"))

            ListView1.Items.Add(item)
        Next

        For Each col As ColumnHeader In ListView1.Columns
            col.Width = -2
        Next
        If Form1 IsNot Nothing AndAlso Form1.IsHandleCreated Then
            Form1.Invoke(Sub()
                             Form1.StartLog()
                         End Sub)
        End If
    End Sub
    Private Sub ErstelleTempReportToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If ListView1.Items.Count = 0 Then
            MessageBox.Show("Die Liste ist leer. Bitte laden Sie zuerst eine Log-Datei.", "Keine Daten", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim reportDataList As New List(Of LogEntry)()
            For Each item As ListViewItem In ListView1.Items
                Dim timestamp As DateTime
                Dim cpuName As String = item.SubItems(0).Text
                Dim core As String = item.SubItems(1).Text
                Dim currentTemp As Single
                Dim minTemp As Single
                Dim maxTemp As Single

                If DateTime.TryParseExact(item.Text, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, timestamp) AndAlso
                   Single.TryParse(item.SubItems(2).Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, currentTemp) AndAlso
                   Single.TryParse(item.SubItems(3).Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, minTemp) AndAlso
                   Single.TryParse(item.SubItems(4).Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, maxTemp) Then

                    reportDataList.Add(New LogEntry With {
                        .Timestamp = timestamp,
                        .Core = core,
                        .CurrentTemp = currentTemp,
                        .MinTemp = minTemp,
                        .MaxTemp = maxTemp
                    })
                End If
            Next
            Dim jsonData As String = JsonConvert.SerializeObject(reportDataList, Formatting.None)
            Dim templatePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemperatureReportTemplate.html")
            If Not File.Exists(templatePath) Then
                MessageBox.Show("Die HTML-Vorlage (TemperatureReportTemplate.html) wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            Dim htmlTemplate As String = File.ReadAllText(templatePath)
            Dim finalHtml As String = htmlTemplate.Replace("{{LOG_DATA_PLACEHOLDER}}", jsonData)

            Using saveFileDialog As New SaveFileDialog()
                saveFileDialog.Filter = "HTML-Datei (*.html)|*.html|Alle Dateien (*.*)|*.*"
                saveFileDialog.Title = "Temperatur-Bericht speichern"
                saveFileDialog.FileName = "Temperatur-Bericht.html"
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    ' 6. Den Bericht in die ausgewählte Datei schreiben
                    File.WriteAllText(saveFileDialog.FileName, finalHtml, System.Text.Encoding.UTF8)

                    ' 7. Den Bericht im Browser öffnen
                    Process.Start(saveFileDialog.FileName)
                    If Form1 IsNot Nothing AndAlso Form1.IsHandleCreated Then
                        Form1.Invoke(Sub()
                                         Form1.StartLog()
                                     End Sub)
                    End If
                    MessageBox.Show("Der Temperatur-Bericht wurde erfolgreich erstellt.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub


End Class

