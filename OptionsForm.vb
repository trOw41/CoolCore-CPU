' OptionsForm.vb
Public Class OptionsForm

    ' Event to notify Form1 about theme change
    Public Event ThemeChanged As EventHandler(Of String)

    Private Sub OptionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Lade die aktuelle Theme-Einstellung
        Select Case My.Settings.ApplicationTheme
            Case "Dark"
                chkDarkTheme.Checked = True
                chkStandardTheme.Checked = False
            Case "Standard"
                chkDarkTheme.Checked = False
                chkStandardTheme.Checked = True
            Case Else ' Standardmäßig auf Standard setzen, falls noch nicht gesetzt
                chkDarkTheme.Checked = False
                chkStandardTheme.Checked = True
                My.Settings.ApplicationTheme = "Standard"
        End Select
    End Sub

    Private Sub ChkDarkTheme_CheckedChanged(sender As Object, e As EventArgs) Handles chkDarkTheme.CheckedChanged
        If chkDarkTheme.Checked Then
            chkStandardTheme.Checked = False
        End If
    End Sub

    Private Sub ChkStandardTheme_CheckedChanged(sender As Object, e As EventArgs) Handles chkStandardTheme.CheckedChanged
        If chkStandardTheme.Checked Then
            chkDarkTheme.Checked = False
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If chkDarkTheme.Checked Then
            My.Settings.ApplicationTheme = "Dark"
        ElseIf chkStandardTheme.Checked Then
            My.Settings.ApplicationTheme = "Standard"
        Else
            ' Falls keine Auswahl getroffen, Standard wählen
            My.Settings.ApplicationTheme = "Standard"
        End If

        My.Settings.Save() ' Speichere die Einstellungen

        ' Benachrichtige über die Änderung (falls Form1 darauf lauschen möchte)
        RaiseEvent ThemeChanged(Me, My.Settings.ApplicationTheme)

        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close() ' Form schließen ohne Änderungen zu speichern
    End Sub

End Class