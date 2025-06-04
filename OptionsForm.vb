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



    Private Sub ApplyThemeToControl(ctrl As Control, theme As String)
        Select Case theme
            Case "Dark"
                ctrl.BackColor = Color.FromArgb(60, 60, 63)
                ctrl.ForeColor = Color.White
                If TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).BackColor = Color.FromArgb(70, 70, 73)
                    CType(ctrl, TextBox).ForeColor = Color.White
                ElseIf TypeOf ctrl Is Button Then
                    CType(ctrl, Button).BackColor = SystemColors.ControlDarkDark
                    CType(ctrl, Button).ForeColor = SystemColors.WindowText
                ElseIf TypeOf ctrl Is GroupBox Then
                    CType(ctrl, GroupBox).BackColor = Color.SlateGray
                    CType(ctrl, GroupBox).ForeColor = Color.White
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                ElseIf TypeOf ctrl Is Panel Then
                    CType(ctrl, Panel).BackColor = Color.FromArgb(50, 50, 53)
                    CType(ctrl, Panel).ForeColor = Color.White
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                End If
            Case "Standard"
                Me.BackColor = SystemColors.Control
                ctrl.BackColor = SystemColors.Control
                ctrl.ForeColor = SystemColors.ControlText
                If TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).BackColor = SystemColors.Control
                    CType(ctrl, TextBox).ForeColor = SystemColors.WindowText
                ElseIf TypeOf ctrl Is Button Then
                    CType(ctrl, Button).BackColor = SystemColors.Control
                    CType(ctrl, Button).ForeColor = SystemColors.ControlText
                ElseIf TypeOf ctrl Is GroupBox Then
                    CType(ctrl, GroupBox).BackColor = SystemColors.Control
                    CType(ctrl, GroupBox).ForeColor = SystemColors.ControlText
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                ElseIf TypeOf ctrl Is Panel Then
                    CType(ctrl, Panel).BackColor = SystemColors.Control
                    CType(ctrl, Panel).ForeColor = SystemColors.ControlText
                    For Each innerCtrl As Control In ctrl.Controls
                        ApplyThemeToControl(innerCtrl, theme)
                    Next
                End If
        End Select
    End Sub
    Private Sub OptionsForm_ThemeChanged(sender As Object, newTheme As String)
        ApplyTheme(newTheme)
    End Sub
    Private Sub ApplyTheme(theme As String)
        Select Case theme
            Case "Dark"
                ' Apply Dark Theme
                Me.BackColor = Color.FromArgb(45, 45, 48) ' Dark grey background
                Me.ForeColor = Color.White
                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next

            Case "Standard"
                ' Apply Standard/Light Theme
                Me.BackColor = SystemColors.Control ' Default system background
                Me.ForeColor = SystemColors.ControlText ' Default system text color

                For Each ctrl As Control In Me.Controls
                    ApplyThemeToControl(ctrl, theme)
                Next

        End Select
    End Sub
End Class