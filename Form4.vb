Imports System.IO ' Für Directory und File Operationen
Imports System.Linq ' Für OrderByDescending

Public Class Form4
    ' Diese Public-Property wird das Ergebnis der Auswahl speichern
    Public Property SelectedFilePath As String = Nothing

    Private logDirectoryPath As String

    Public Sub New(archiveFolderPath As String)
        InitializeComponent()
        logDirectoryPath = archiveFolderPath
        Me.Text = "Archivierte Messungen auswählen"
        Me.ControlBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent

        LblInstructions.Text = "Wählen Sie eine archivierte Temperaturmessung aus:"
        LblInstructions.Location = New Point(10, 10)
        LblInstructions.AutoSize = True

        ListBoxArchives.Location = New Point(10, LblInstructions.Bottom + 10)
        ListBoxArchives.Size = New Size(Me.ClientSize.Width - 20, Me.ClientSize.Height - LblInstructions.Bottom - 80) ' Angepasste Größe
        ListBoxArchives.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        BtnSelect.Text = "Auswählen"
        BtnSelect.Location = New Point(Me.ClientSize.Width - BtnSelect.Width - 10, Me.ClientSize.Height - BtnSelect.Height - 10)
        BtnSelect.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right

        BtnCancel.Text = "Abbrechen"
        BtnCancel.Location = New Point(BtnSelect.Left - BtnCancel.Width - 10, Me.ClientSize.Height - BtnCancel.Height - 10)
        BtnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right

        ' Event Handler hinzufügen
        AddHandler BtnSelect.Click, AddressOf BtnSelect_Click
        AddHandler BtnCancel.Click, AddressOf BtnCancel_Click
        AddHandler ListBoxArchives.DoubleClick, AddressOf ListBoxArchives_DoubleClick ' Doppelklick zum Auswählen
        LoadArchivedMeasurements()
        LoadArchiveFiles()
    End Sub

    Private Sub LoadArchiveFiles()
        ListBoxArchives.Items.Clear()
        If Not Directory.Exists(logDirectoryPath) Then
            MessageBox.Show($"Der Archivordner '{logDirectoryPath}' wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            ' Nur Dateien laden, die dem Muster CoolCore_Temp_Log_*.csv entsprechen
            Dim files = Directory.GetFiles(logDirectoryPath, "CoolCore_Temp_Log_*.csv") _
                                 .OrderByDescending(Function(f) File.GetCreationTime(f)) ' Neueste zuerst

            If files.Any() Then
                For Each filePath As String In files
                    ListBoxArchives.Items.Add(Path.GetFileName(filePath)) ' Nur den Dateinamen anzeigen
                Next
            Else
                ListBoxArchives.Items.Add("Keine archivierten Messungen gefunden.")
                ListBoxArchives.Enabled = False ' Deaktivieren, wenn keine Dateien
                BtnSelect.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show($"Fehler beim Laden der Archivdateien: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ListBoxArchives.Enabled = False
            BtnSelect.Enabled = False
        End Try
    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        If ListBoxArchives.SelectedItem IsNot Nothing AndAlso ListBoxArchives.Enabled Then
            SelectedFilePath = Path.Combine(logDirectoryPath, ListBoxArchives.SelectedItem.ToString())
            Me.DialogResult = DialogResult.OK ' Dialogergebnis auf OK setzen
            'Me.Close() ' Formular schließen
        Else
            MessageBox.Show("Bitte wählen Sie eine Messung aus der Liste aus.", "Auswahl erforderlich", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel ' Dialogergebnis auf Cancel setzen
        Me.Close() ' Formular schließen
    End Sub

    Private Sub ListBoxArchives_DoubleClick(sender As Object, e As EventArgs) Handles ListBoxArchives.DoubleClick
        ' Doppelklick auf ein Element ist wie "Auswählen"
        BtnSelect_Click(sender, e)
    End Sub

    ' Optional: Wenn sich die Größe des Formulars ändert, die Controls neu positionieren
    Private Sub Form4_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ListBoxArchives.Size = New Size(Me.ClientSize.Width - 20, Me.ClientSize.Height - LblInstructions.Bottom - 80)
        BtnSelect.Location = New Point(Me.ClientSize.Width - BtnSelect.Width - 10, Me.ClientSize.Height - BtnSelect.Height - 10)
        BtnCancel.Location = New Point(BtnSelect.Left - BtnCancel.Width - 10, Me.ClientSize.Height - BtnCancel.Height - 10)
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        Dim confirmResult As DialogResult
        confirmResult = MessageBox.Show("ALLE archivierten Messungen löschen? Dies kann NICHT rückgängig gemacht werden.", "Archiv löschen bestätigen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)

        If confirmResult = DialogResult.Yes Then
            Try
                If Directory.Exists(logDirectoryPath) Then
                    Dim csvFiles() As String = Directory.GetFiles(logDirectoryPath, "*.csv")
                    Dim deletedCount As Integer = 0

                    For Each file As String In csvFiles
                        System.IO.File.Delete(file)
                        deletedCount += 1
                    Next

                    MessageBox.Show($"{deletedCount} archivierte Messungen erfolgreich gelöscht.", "Löschvorgang abgeschlossen", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadArchivedMeasurements()
                    SelectedFilePath = Nothing
                    Me.Close()
                Else
                    MessageBox.Show("Das Archivverzeichnis wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Löschen der archivierten Messungen: {ex.Message}", "Löschfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub LoadArchivedMeasurements()
        ListBoxArchives.Items.Clear()
        Try
            If Directory.Exists(logDirectoryPath) Then
                Dim files As String() = Directory.GetFiles(logDirectoryPath, "*.csv")
                Dim sortedFiles = files.Select(Function(f) New FileInfo(f)).OrderByDescending(Function(fi) fi.CreationTime).ToList()

                For Each fileInfo In sortedFiles
                    ListBoxArchives.Items.Add(fileInfo.Name)
                Next

                Select Case ListBoxArchives.Items.Count
                    Case 0
                        ListBoxArchives.Items.Add("Keine archivierten Messungen gefunden.")
                        ListBoxArchives.Enabled = False ' Deaktiviert die Auswahl
                    Case Else
                        ListBoxArchives.Enabled = True
                End Select
            Else
                ListBoxArchives.Items.Add("Archivverzeichnis nicht gefunden.")
                ListBoxArchives.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Laden der Archivdaten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ListBoxArchives.Items.Clear()
            ListBoxArchives.Items.Add("Fehler beim Laden der Daten.")
            ListBoxArchives.Enabled = False
        End Try
    End Sub
End Class