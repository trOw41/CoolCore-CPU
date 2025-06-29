<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OptionsForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsForm))
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.LogSizeBox = New System.Windows.Forms.ComboBox()
        Me.LogStartStopBox = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LogPanelCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.StartMessageBox = New System.Windows.Forms.CheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.updateCheckBox = New System.Windows.Forms.CheckBox()
        Me.BootBox = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.InfoButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LogNowCheckBox = New System.Windows.Forms.CheckBox()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSave.Location = New System.Drawing.Point(313, 367)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(86, 25)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Speichern"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnCancel.Location = New System.Drawing.Point(5, 367)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(92, 25)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Abbr."
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(0, 25)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(403, 21)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Temperatur Test Zeit:"
        Me.Label1.UseMnemonic = False
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BackColor = System.Drawing.Color.Azure
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"30", "45", "60", "120"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(339, 51)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(57, 64)
        Me.CheckedListBox1.TabIndex = 6
        '
        'LogSizeBox
        '
        Me.LogSizeBox.FormattingEnabled = True
        Me.LogSizeBox.Items.AddRange(New Object() {"0", "10", "50", "100", "200", "500"})
        Me.LogSizeBox.Location = New System.Drawing.Point(254, 23)
        Me.LogSizeBox.Margin = New System.Windows.Forms.Padding(4)
        Me.LogSizeBox.Name = "LogSizeBox"
        Me.LogSizeBox.Size = New System.Drawing.Size(77, 21)
        Me.LogSizeBox.TabIndex = 1
        '
        'LogStartStopBox
        '
        Me.LogStartStopBox.AutoSize = True
        Me.LogStartStopBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LogStartStopBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LogStartStopBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogStartStopBox.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LogStartStopBox.Location = New System.Drawing.Point(190, 49)
        Me.LogStartStopBox.Margin = New System.Windows.Forms.Padding(4)
        Me.LogStartStopBox.Name = "LogStartStopBox"
        Me.LogStartStopBox.Size = New System.Drawing.Size(118, 17)
        Me.LogStartStopBox.TabIndex = 3
        Me.LogStartStopBox.Text = "Temperatur Log ein:"
        Me.LogStartStopBox.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LogStartStopBox.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoEllipsis = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label3.Location = New System.Drawing.Point(-1, -1)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(403, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Temperatur Log:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.UseMnemonic = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(334, 24)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(33, 17)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "KB"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label20.UseMnemonic = False
        '
        'Label5
        '
        Me.Label5.AutoEllipsis = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(181, 99)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(150, 12)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Intervall in Sekunden┈ ┈ ▶"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.UseMnemonic = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(229, 79)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 12)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = " ◀┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.UseMnemonic = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(92, 78)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 16)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "◀═ ═ ═ ═ ═ ═ ═ ═ ═ "
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label14.UseMnemonic = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Bahnschrift Light SemiCondensed", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(145, 79)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 13)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Rechenoperationen"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Bahnschrift Light SemiCondensed", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(265, 79)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(70, 13)
        Me.Label16.TabIndex = 12
        Me.Label16.Text = "Operations Zeit"
        '
        'Label17
        '
        Me.Label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label17.Font = New System.Drawing.Font("Bahnschrift Light SemiCondensed", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite
        Me.Label17.Location = New System.Drawing.Point(136, 64)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(136, 13)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "Math. Formel für Überladung"
        Me.Label17.UseCompatibleTextRendering = True
        Me.Label17.UseMnemonic = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.LogPanelCheckBox)
        Me.Panel3.Controls.Add(Me.LogStartStopBox)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.StartMessageBox)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.LogNowCheckBox)
        Me.Panel3.Controls.Add(Me.LogSizeBox)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Location = New System.Drawing.Point(0, 121)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(403, 241)
        Me.Panel3.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoEllipsis = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label9.Location = New System.Drawing.Point(0, 70)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(402, 20)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "System Start:"
        Me.Label9.UseMnemonic = False
        '
        'LogPanelCheckBox
        '
        Me.LogPanelCheckBox.AutoSize = True
        Me.LogPanelCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LogPanelCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LogPanelCheckBox.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!)
        Me.LogPanelCheckBox.Location = New System.Drawing.Point(6, 49)
        Me.LogPanelCheckBox.Name = "LogPanelCheckBox"
        Me.LogPanelCheckBox.Size = New System.Drawing.Size(121, 17)
        Me.LogPanelCheckBox.TabIndex = 16
        Me.LogPanelCheckBox.Text = "Log Panel anzeigen:"
        Me.LogPanelCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LogPanelCheckBox.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label18.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.75!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label18.Location = New System.Drawing.Point(7, 126)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(377, 46)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "Hier können die Einstellungen für einen Start von CoolCore bei System-boot vorgen" &
    "ommen werden. Wenn ""Update check"" aktiviert ist prüft CC bei jedem Start des Pro" &
    "gramms auf updates."
        Me.Label18.UseCompatibleTextRendering = True
        Me.Label18.UseMnemonic = False
        '
        'StartMessageBox
        '
        Me.StartMessageBox.AutoSize = True
        Me.StartMessageBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.StartMessageBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.StartMessageBox.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!)
        Me.StartMessageBox.Location = New System.Drawing.Point(85, 209)
        Me.StartMessageBox.Name = "StartMessageBox"
        Me.StartMessageBox.Size = New System.Drawing.Size(223, 17)
        Me.StartMessageBox.TabIndex = 15
        Me.StartMessageBox.Text = "Willkommen Nachricht immer anzeigen:"
        Me.StartMessageBox.UseCompatibleTextRendering = True
        Me.StartMessageBox.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.updateCheckBox)
        Me.Panel4.Controls.Add(Me.BootBox)
        Me.Panel4.Location = New System.Drawing.Point(0, 87)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(402, 36)
        Me.Panel4.TabIndex = 14
        '
        'updateCheckBox
        '
        Me.updateCheckBox.AutoSize = True
        Me.updateCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.updateCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.updateCheckBox.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!)
        Me.updateCheckBox.Location = New System.Drawing.Point(187, 9)
        Me.updateCheckBox.Name = "updateCheckBox"
        Me.updateCheckBox.Size = New System.Drawing.Size(91, 17)
        Me.updateCheckBox.TabIndex = 8
        Me.updateCheckBox.Text = "Update check:"
        Me.updateCheckBox.UseVisualStyleBackColor = True
        '
        'BootBox
        '
        Me.BootBox.AutoSize = True
        Me.BootBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BootBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BootBox.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!)
        Me.BootBox.Location = New System.Drawing.Point(42, 9)
        Me.BootBox.Name = "BootBox"
        Me.BootBox.Size = New System.Drawing.Size(85, 17)
        Me.BootBox.TabIndex = 8
        Me.BootBox.Text = "Systemstart"
        Me.BootBox.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoEllipsis = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label7.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label7.Location = New System.Drawing.Point(0, 177)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(402, 18)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Weitere Einstellungen:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.UseMnemonic = False
        '
        'InfoButton
        '
        Me.InfoButton.BackgroundImage = Global.CoolCore.My.Resources.Resources._004_computer_science
        Me.InfoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.InfoButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight
        Me.InfoButton.FlatAppearance.BorderSize = 2
        Me.InfoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue
        Me.InfoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.InfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.InfoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InfoButton.Location = New System.Drawing.Point(22, 55)
        Me.InfoButton.Name = "InfoButton"
        Me.InfoButton.Size = New System.Drawing.Size(56, 51)
        Me.InfoButton.TabIndex = 14
        Me.InfoButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(403, 25)
        Me.Panel1.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label2.Location = New System.Drawing.Point(115, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(169, 25)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Einstellungen"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Label2.UseMnemonic = False
        '
        'LogNowCheckBox
        '
        Me.LogNowCheckBox.AutoSize = True
        Me.LogNowCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LogNowCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LogNowCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogNowCheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LogNowCheckBox.Location = New System.Drawing.Point(6, 24)
        Me.LogNowCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.LogNowCheckBox.Name = "LogNowCheckBox"
        Me.LogNowCheckBox.Size = New System.Drawing.Size(209, 17)
        Me.LogNowCheckBox.TabIndex = 3
        Me.LogNowCheckBox.Text = "Nur MAX Temperatur Werte mit loggen:"
        Me.LogNowCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LogNowCheckBox.UseVisualStyleBackColor = True
        '
        'OptionsForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(403, 396)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.InfoButton)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents LogSizeBox As ComboBox
    Friend WithEvents LogStartStopBox As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents BootBox As CheckBox
    Friend WithEvents Label20 As Label
    Friend WithEvents InfoButton As Button
    Friend WithEvents updateCheckBox As CheckBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents StartMessageBox As CheckBox
    Friend WithEvents LogPanelCheckBox As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents LogNowCheckBox As CheckBox
End Class
