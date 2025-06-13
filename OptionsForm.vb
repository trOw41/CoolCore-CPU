' OptionsForm.vb
Imports System.Windows.Forms

Public Class OptionsForm
    Public Event ThemeChanged As EventHandler(Of String)

    Private Sub OptionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case Settings.ApplicationTheme
            Case "Dark"
                chkDarkTheme.Checked = True
                chkStandardTheme.Checked = False
            Case "Standard"
                chkDarkTheme.Checked = False
                chkStandardTheme.Checked = True
            Case Else
                chkDarkTheme.Checked = False
                chkStandardTheme.Checked = True
                Settings.ApplicationTheme = "Standard"
        End Select
        Label1.Text = "CPU Stresstest Intervall (in Sekunden): " & My.Settings.MonitorTime
        For i = 0 To LogSizeBox.Items.Count - 1
            Dim items = i
            LogSizeBox.Items(i) = Settings.MAX_LOG_SIZE_KB
            If My.Settings.MAX_LOG_SIZE_KB = LogSizeBox.Items(i).ToString() Then
                LogSizeBox.SelectedIndex = i
                Exit For
            End If
        Next
        LogStartStopBox.Checked = Settings.LogStartStop
        BootBox.Checked = Settings.BootUp
        Dim monitorTime = Settings.MonitorTime
        If monitorTime > 0 Then
            For i = 0 To CheckedListBox1.Items.Count - 1
                If CheckedListBox1.Items(i).ToString() = monitorTime.ToString() Then
                    CheckedListBox1.SetItemChecked(i, True)
                Else
                    CheckedListBox1.SetItemChecked(i, False)
                End If
            Next
        End If
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
        If Settings.InfoMessage = False Then
            InfoDialog.ShowDialog(Me)
        End If
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

    Private Sub LogSizeBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LogSizeBox.SelectedIndexChanged

        If LogSizeBox.SelectedItem IsNot Nothing Then
            My.Settings.MAX_LOG_SIZE_KB = LogSizeBox.SelectedItem.ToString()
        End If
        If Form1 IsNot Nothing AndAlso Form1.IsHandleCreated Then
            Form1.Invoke(Sub()
                             Form1.UpdateLogSize()
                         End Sub)
        End If

    End Sub

    Private Sub LogStartStopBox_CheckedChanged(sender As Object, e As EventArgs) Handles LogStartStopBox.CheckedChanged
        If LogStartStopBox.Checked = False Then
            My.Settings.LogStartStop = False
            Form1.Invoke(Sub()
                             Form1.StartStopLog()
                         End Sub)
        ElseIf LogStartStopBox.Checked = True Then
            My.Settings.LogStartStop = True
            If Form1 IsNot Nothing AndAlso Form1.IsHandleCreated Then
                Form1.Invoke(Sub()
                                 Form1.StartStopLog()
                             End Sub)
            End If
        End If
    End Sub

    Public Sub BootBox_CheckedChanged(sender As Object, e As EventArgs) Handles BootBox.CheckedChanged
        Try
            If BootBox.Checked = True Then
                Settings.BootUp = True
                If Settings.Autostart = False Then
                    Using Process.Start("setreg.bat")
                        Settings.Autostart = True
                        MessageBox.Show("Autostart wurde aktiviert. CoolCore wird mit dem nächsten System Start ausgeführt.", "Autostart aktiviert", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                Else
                End If
            ElseIf BootBox.Checked = False Then
                Settings.BootUp = False
                If Settings.Autostart = True Then
                    Using Process.Start("rmreg2.bat")
                        Settings.Autostart = False
                        MessageBox.Show("Autostart wurde deaktiviert. CoolCore wird nicht mehr automatisch gestartet.", "Autostart deaktiviert", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End If
            End If
        Catch ex As Exception
            MessageBox.Show($"Error Prozess kan nicht gestzartet werden: {ex.Message}")
            BootBox.Checked = Not BootBox.Checked ' Reset the checkbox state
        End Try
    End Sub

End Class