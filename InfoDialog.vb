Public Class InfoDialog

    Private Sub InfoDialog_load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox1.Checked = Settings.InfoMessage
        InfoBox.Text = InfoText()
    End Sub

    Private Function InfoText()
        Dim v = "Dieses Intervall definiert die Dauer, in der Ihr Prozessor für Messzwecke auf 100% Auslastung betrieben wird. Ziel ist die Ermittlung der 
Stress-Temperatur zur Optimierung der Datenverarbeitung und Durchschnittstemperaturberechnungen in CoolCore.
Stress-Messungen können die Systemstabilität vorübergehend beeinträchtigen (z.B. Überlastung, Einfrieren).
Bei älteren oder defekten Systemen kann es zu einer automatischen Abschaltung bei Erreichen der maximalen Temperatur (TjMax) kommen.
Häufiges oder langes 'Stressen' der CPU kann zudem zu dauerhaften Schäden oder erhöhter Lüfterlautstärke führen.
Die Nutzung erfolgt auf eigene Gefahr; jegliche Gewährleistung ist ausgeschlossen."
        Return v
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Settings.InfoMessage = True
            Settings.Save()

        Else
            Settings.InfoMessage = False
            Settings.Save()

        End If
    End Sub

    Private Sub ApplyTheme(theme As String)
        Select Case theme
            Case "Dark"
                Me.BackColor = ColorTranslator.FromHtml("#282C34")
                Me.ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next
                'PictureBox1.Image = My.Resources._024_cpu
                Me.Icon = My.Resources._024_cpu_1
            Case "Standard"
                ' Apply Standard/Light Theme
                Me.BackColor = ColorTranslator.FromHtml("#F0F0F0")
                Me.ForeColor = SystemColors.WindowText
                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next
                'PictureBox1.Image = My.Resources._023_cpu
                Me.Icon = My.Resources._023_cpu_1
        End Select
    End Sub
    Private Sub ApplyThemeToControl(ctrl As Control, theme As String)
        Select Case theme
            Case "Dark"
                If TypeOf ctrl Is Button Then
                    CType(ctrl, Button).BackColor = ColorTranslator.FromHtml("#3B4048")
                    CType(ctrl, Button).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                    CType(ctrl, Button).FlatStyle = FlatStyle.Flat
                    CType(ctrl, Button).FlatAppearance.BorderColor = ColorTranslator.FromHtml("#4A5059")
                    CType(ctrl, Button).FlatAppearance.BorderSize = 1
                ElseIf TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).BackColor = ColorTranslator.FromHtml("#3B4048")
                    CType(ctrl, TextBox).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                    CType(ctrl, TextBox).BorderStyle = BorderStyle.FixedSingle
                ElseIf TypeOf ctrl Is Label Then
                    CType(ctrl, Label).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                ElseIf TypeOf ctrl Is CheckBox Then
                    CType(ctrl, CheckBox).ForeColor = SystemColors.ControlLightLight
                ElseIf TypeOf ctrl Is GroupBox Then
                    CType(ctrl, GroupBox).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                    CType(ctrl, GroupBox).BackColor = ColorTranslator.FromHtml("#282C34")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                ElseIf TypeOf ctrl Is Panel Then
                    CType(ctrl, Panel).BackColor = ColorTranslator.FromHtml("#282C34")
                    CType(ctrl, Panel).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                End If
            Case "Standard"
                If TypeOf ctrl Is Button Then
                    CType(ctrl, Button).BackColor = ColorTranslator.FromHtml("#E1E1E1")
                    CType(ctrl, Button).ForeColor = SystemColors.WindowText
                    CType(ctrl, Button).FlatStyle = FlatStyle.Flat
                    CType(ctrl, Button).FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CCCCCC")
                    CType(ctrl, Button).FlatAppearance.BorderSize = 1
                ElseIf TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).BackColor = Color.White
                    CType(ctrl, TextBox).ForeColor = SystemColors.WindowText
                    CType(ctrl, TextBox).BorderStyle = BorderStyle.FixedSingle
                ElseIf TypeOf ctrl Is Label Then
                    CType(ctrl, Label).ForeColor = SystemColors.WindowText
                ElseIf TypeOf ctrl Is CheckBox Then
                    CType(ctrl, CheckBox).ForeColor = ColorTranslator.FromHtml("#333333")
                ElseIf TypeOf ctrl Is GroupBox Then
                    CType(ctrl, GroupBox).ForeColor = ColorTranslator.FromHtml("#333333")
                    CType(ctrl, GroupBox).BackColor = ColorTranslator.FromHtml("#F0F0F0")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                ElseIf TypeOf ctrl Is Panel Then
                    CType(ctrl, Panel).BackColor = ColorTranslator.FromHtml("#F0F0F0")
                    CType(ctrl, Panel).ForeColor = SystemColors.WindowText 'ColorTranslator.FromHtml("#333333")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                End If

        End Select
    End Sub

    Private Sub OptionsForm_ThemeChanged(sender As Object, newTheme As String)
        ApplyTheme(newTheme)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Settings.Save()
        Me.Close()
    End Sub
End Class