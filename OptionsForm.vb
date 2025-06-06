' OptionsForm.vb
Public Class OptionsForm
    Public Event ThemeChanged As EventHandler(Of String)

    Private Sub OptionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case My.Settings.ApplicationTheme
            Case "Dark"
                chkDarkTheme.Checked = True
                chkStandardTheme.Checked = False
            Case "Standard"
                chkDarkTheme.Checked = False
                chkStandardTheme.Checked = True
            Case Else
                chkDarkTheme.Checked = False
                chkStandardTheme.Checked = True
                My.Settings.ApplicationTheme = "Standard"
        End Select
        Label1.Text = "CPU Stresstest Intervall (in Sekunden): " & My.Settings.MonitorTime
    End Sub

    Private Sub ChkDarkTheme_CheckedChanged(sender As Object, e As EventArgs) Handles chkDarkTheme.CheckedChanged
        If chkDarkTheme.Checked = False Then
            chkStandardTheme.Checked = True
            'chkDarkTheme.Checked = True

        ElseIf chkDarkTheme.Checked = True Then
            chkStandardTheme.Checked = False
            'chkDarkTheme.Checked = True
            OptionsForm_ThemeChanged(Me, "Dark")
        End If

    End Sub

    Private Sub ChkStandardTheme_CheckedChanged(sender As Object, e As EventArgs) Handles chkStandardTheme.CheckedChanged
        If chkStandardTheme.Checked = False Then
            chkDarkTheme.Checked = True

        ElseIf chkStandardTheme.Checked = True Then
            chkDarkTheme.Checked = False
            'chkStandardTheme.Checked = True
            OptionsForm_ThemeChanged(Me, "Standard")
        End If

    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If chkDarkTheme.Checked Then
#Const darkmode = True
            My.Settings.ApplicationTheme = "Dark"
        ElseIf chkStandardTheme.Checked Then
#Const darkmode = False
            My.Settings.ApplicationTheme = "Standard"
        Else
#Const darkmode = False
            My.Settings.ApplicationTheme = "Standard"
        End If
        My.Settings.Save()
        RaiseEvent ThemeChanged(Me, My.Settings.ApplicationTheme)
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub CheckedListBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedValueChanged
        If CheckedListBox1.SelectedItem IsNot Nothing Then
            My.Settings.MonitorTime = CheckedListBox1.SelectedItem.ToString()
            Label1.Text = "CPU Stresstest Intervall (in Sekunden): " & My.Settings.MonitorTime
        End If
        For i = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(i, i = CheckedListBox1.SelectedIndex)
        Next
    End Sub

    Private Sub ApplyTheme(theme As String)
        Select Case theme
            Case "Dark"
                Me.BackColor = ColorTranslator.FromHtml("#282C34")
                Me.ForeColor = ColorTranslator.FromHtml("#ABB2BF")
                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next
                Me.Icon = My.Resources._024_cpu_1
            Case "Standard"
                Me.BackColor = ColorTranslator.FromHtml("#F0F0F0")
                Me.ForeColor = ColorTranslator.FromHtml("#333333")
                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next
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
                    CType(ctrl, CheckBox).ForeColor = ColorTranslator.FromHtml("#ABB2BF")
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
                    CType(ctrl, Button).ForeColor = ColorTranslator.FromHtml("#333333")
                    CType(ctrl, Button).FlatStyle = FlatStyle.Flat
                    CType(ctrl, Button).FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CCCCCC")
                    CType(ctrl, Button).FlatAppearance.BorderSize = 1
                ElseIf TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).BackColor = Color.White
                    CType(ctrl, TextBox).ForeColor = ColorTranslator.FromHtml("#333333")
                    CType(ctrl, TextBox).BorderStyle = BorderStyle.FixedSingle
                ElseIf TypeOf ctrl Is Label Then
                    CType(ctrl, Label).ForeColor = ColorTranslator.FromHtml("#333333")
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
                    CType(ctrl, Panel).ForeColor = ColorTranslator.FromHtml("#333333")
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                End If
        End Select
    End Sub

    Private Sub OptionsForm_ThemeChanged(sender As Object, newTheme As String)
        ApplyTheme(newTheme)
    End Sub
End Class