' OptionsForm.vb
Imports System.IO
Imports System.Xml.Serialization

Public Class OptionsForm
    Public Event ThemeChanged As EventHandler(Of String)
    Private _updateCheckBoxInitialState As CheckState
    Private _StartMessageBoxCheckBoxInitialState As CheckState
    Private _isInitializing As Boolean = False
    Dim documentsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    Dim settingsPath As String = Path.Combine(documentsPath, "CoolCore")

    Private Sub OptionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Label1.Text = $"Temp. Monitor & CPU Streß: {Settings().MonitorTime} s"
        For i = 0 To LogSizeBox.Items.Count - 1
            LogSizeBox.Items(i) = Settings.MAX_LOG_SIZE_KB
            If Settings.MAX_LOG_SIZE_KB = LogSizeBox.Items(i).ToString() Then
                LogSizeBox.SelectedIndex = i
                Exit For
            End If
        Next
        LogStartStopBox.Checked = Settings.LogStartStop
        updateCheckBox.Checked = Settings.UpdateCheck
        BootBox.Checked = Settings.BootUp
        LogPanelCheckBox.Checked = Settings.LogPanel
        StartMessageBox.Checked = Settings.AllwaysShow
        updateCheckBox.Checked = Settings.UpdateCheck
        _updateCheckBoxInitialState = updateCheckBox.CheckState
        _isInitializing = True
        LogNowCheckBox.Checked = Not Settings.LogNormal
        Dim monitorTime = Settings.MonitorTime
        If monitorTime > 0 Then
            For i = 0 To CheckedListBox1.Items.Count - 1
                If CheckedListBox1.Items(i).ToString() = monitorTime.ToString() Then
                    CheckedListBox1.SetItemChecked(i, True)
                Else
                    CheckedListBox1.SetItemChecked(i, False)
                End If
            Next
        Else
            monitorTime = 30
            For i = 0 To CheckedListBox1.Items.Count - 1
                If CheckedListBox1.Items(i).ToString() = monitorTime.ToString() Then
                    CheckedListBox1.SetItemChecked(i, True)
                Else
                    CheckedListBox1.SetItemChecked(i, False)
                End If
            Next
        End If

    End Sub
    Private Sub UpdateSettingsFromUI()
        Settings.MonitorTime = CheckedListBox1.SelectedItem?.ToString()
        Settings.MAX_LOG_SIZE_KB = LogSizeBox.SelectedItem?.ToString()
        Settings.LogStartStop = LogStartStopBox.Checked
        Settings.BootUp = BootBox.Checked
        Settings.LogPanel = LogPanelCheckBox.Checked
        Settings.AllwaysShow = StartMessageBox.Checked
        Settings.UpdateCheck = updateCheckBox.Checked
        Settings.LogNormal = Not LogNowCheckBox.Checked

    End Sub
    Private Sub SaveSettingsToXml()
        Dim getsettings As New AppSettingsXML With {
        .MonitorTime = Settings.MonitorTime,
        .MAX_LOG_SIZE_KB = Settings.MAX_LOG_SIZE_KB,
        .LogStartStop = Settings.LogStartStop,
        .CpuLogoName = Settings.CpuLogoName,
        .BootUp = Settings.BootUp,
        .Autostart = Settings.Autostart,
        .InfoMessage = Settings.InfoMessage,
        .MashineID = Settings.MashineID,
        .IsCpuSubInfoLoaded = Settings.IsCpuSubInfoLoaded,
        .FirstStart = Settings.FirstStart,
        .UpdateCheck = Settings.UpdateCheck,
        .AllwaysShow = Settings.AllwaysShow,
        .CName = Settings.CName,
        .LogPanel = Settings.LogPanel,
        .LogNormal = Settings.LogNormal,
        .Ops = Settings.ops
        }
        Dim documentsPath As String = settingsPath
        Dim xmlPath As String = Path.Combine(documentsPath, "CoolCoreSettings.xml")
        Using fs As New FileStream(xmlPath, FileMode.Create)
            Dim serializer As New XmlSerializer(GetType(AppSettingsXML))
            serializer.Serialize(fs, getsettings)
        End Using
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        UpdateSettingsFromUI()
        SaveSettingsToXml()
        Settings().Save()
        Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub CheckedListBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedValueChanged
        If Settings.InfoMessage = False Then
            InfoDialog.ShowDialog(Me)
        End If
        If CheckedListBox1.SelectedItem IsNot Nothing Then
            Settings.MonitorTime = CheckedListBox1.SelectedItem.ToString()
            'Label1.Text = "CPU Stresstest Intervall: " & Settings().MonitorTime
        End If
        For i = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(i, i = CheckedListBox1.SelectedIndex)
        Next
    End Sub

    Private Sub ApplyThemeToControl(ctrl As Control, theme As String)
        If theme = "Standard" Then
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
        End If
    End Sub

    Private Sub LogSizeBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LogSizeBox.SelectedIndexChanged

        If LogSizeBox.SelectedItem IsNot Nothing Then
            Settings.MAX_LOG_SIZE_KB = LogSizeBox.SelectedItem.ToString()
        End If
        If Form1 IsNot Nothing AndAlso Form1.IsHandleCreated Then
            Form1.Invoke(Sub()
                             Form1.UpdateLogSize()
                         End Sub)
        End If

    End Sub

    Private Sub LogStartStopBox_CheckedChanged(sender As Object, e As EventArgs) Handles LogStartStopBox.CheckedChanged
        If LogStartStopBox.Checked = False Then
            Settings.LogStartStop = False
            Settings.Save()
            Form1.Invoke(Sub()
                             Form1.StartStopLog()
                         End Sub)
        ElseIf LogStartStopBox.Checked = True Then
            Settings.LogStartStop = True
            Settings.Save()
            If Form1 Is Nothing AndAlso Form1.IsHandleCreated Then
                Form1.Invoke(Sub()
                                 Form1.StartStopLog()
                             End Sub)
            End If
        End If
    End Sub

    Private Sub LogPanelCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles LogPanelCheckBox.CheckedChanged
        Try
            If LogPanelCheckBox.Checked = True Then
                Settings.LogPanel = True
                Settings.Save()
                Form1.LblStatusMessage.Visible = True
            ElseIf LogPanelCheckBox.Checked = False Then
                Settings.LogPanel = False
                Settings.Save()
                Form1.LblStatusMessage.Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show($"Error Prozess kan nicht gestartet werden: {ex.Message}")
            LogPanelCheckBox.Checked = Not LogPanelCheckBox.Checked ' Reset the checkbox state
        End Try
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
            MessageBox.Show($"Error Prozess kan nicht gestartet werden: {ex.Message}")
            BootBox.Checked = Not BootBox.Checked ' Reset the checkbox state
        End Try
    End Sub


    Private Sub UpdateCheckBox_CheckStateChanged(sender As Object, e As EventArgs) Handles updateCheckBox.CheckStateChanged
        If updateCheckBox.CheckState = _updateCheckBoxInitialState Then
            ' Keine Änderung, keine MessageBox anzeigen
            Return
        End If

        If updateCheckBox.CheckState = CheckState.Checked Then
            Settings.UpdateCheck = True
            'MessageBox.Show("Update Check wurde aktiviert. Sie werden benachrichtigt, wenn eine neue Version verfügbar ist.", "Update Check aktiviert", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Settings.UpdateCheck = False
            MessageBox.Show("Update Check wurde deaktiviert. Sie werden keine Benachrichtigungen über neue Versionen erhalten.", "Update Check deaktiviert", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        _updateCheckBoxInitialState = updateCheckBox.CheckState
    End Sub

    Private Sub StartMessageBox_CheckedChanged(sender As Object, e As EventArgs)
        If StartMessageBox.CheckState = True Then
            Settings.AllwaysShow = True
            Settings.Save()
            MessageBox.Show("Die Startnachricht wird immer angezeigt.", "Info:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Settings.AllwaysShow = False
            If _isInitializing Then
                _isInitializing = False
                Return
            End If
            Settings.Save()
            MessageBox.Show("Die Startnachricht wird nicht mehr angezeigt.", "Info:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub InfoButton_Click(sender As Object, e As EventArgs) Handles InfoButton.Click
        InfoDialog.ShowDialog(Me)
    End Sub

    Private Sub LogNowCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles LogNowCheckBox.CheckedChanged
        If _isInitializing Then
            _isInitializing = False
            Return
        End If
        If Form1 Is Nothing OrElse Not Form1.IsHandleCreated Then
            MessageBox.Show("Form1 is not initialized or handle is not created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        ' Update the log state based on the checkbox
        If LogNowCheckBox.CheckState = CheckState.Unchecked AndAlso Settings.LogNormal = True Then
            Settings.Save()
            If Form1 IsNot Nothing AndAlso Form1.IsHandleCreated Then
                Form1.Invoke(Sub()
                                 Form1.CheckLogMeta(False)
                             End Sub)
            End If
        ElseIf LogNowCheckBox.CheckState = CheckState.Checked Then
            Settings.LogNormal = False
            Settings.Save()
            If Form1 IsNot Nothing AndAlso Form1.IsHandleCreated Then
                Form1.Invoke(Sub()
                                 Form1.CheckLogMeta(True)
                             End Sub)
            End If
        End If
        UpdateSettingsFromUI()
        SaveSettingsToXml()
        Settings().Save()
    End Sub
End Class