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
        Me.lblThemeSetting = New System.Windows.Forms.Label()
        Me.chkDarkTheme = New System.Windows.Forms.CheckBox()
        Me.chkStandardTheme = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.LogSizeBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LogStartStopBox = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.BootBox = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.InfoButton = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblThemeSetting
        '
        Me.lblThemeSetting.AutoSize = True
        Me.lblThemeSetting.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblThemeSetting.Location = New System.Drawing.Point(4, 7)
        Me.lblThemeSetting.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblThemeSetting.Name = "lblThemeSetting"
        Me.lblThemeSetting.Size = New System.Drawing.Size(49, 18)
        Me.lblThemeSetting.TabIndex = 0
        Me.lblThemeSetting.Text = "Theme:"
        '
        'chkDarkTheme
        '
        Me.chkDarkTheme.AutoSize = True
        Me.chkDarkTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkDarkTheme.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDarkTheme.Location = New System.Drawing.Point(126, 9)
        Me.chkDarkTheme.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDarkTheme.Name = "chkDarkTheme"
        Me.chkDarkTheme.Size = New System.Drawing.Size(79, 17)
        Me.chkDarkTheme.TabIndex = 1
        Me.chkDarkTheme.Text = "DarkTheme"
        Me.chkDarkTheme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkDarkTheme.UseVisualStyleBackColor = True
        '
        'chkStandardTheme
        '
        Me.chkStandardTheme.AutoSize = True
        Me.chkStandardTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkStandardTheme.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStandardTheme.Location = New System.Drawing.Point(263, 8)
        Me.chkStandardTheme.Margin = New System.Windows.Forms.Padding(4)
        Me.chkStandardTheme.Name = "chkStandardTheme"
        Me.chkStandardTheme.Size = New System.Drawing.Size(104, 17)
        Me.chkStandardTheme.TabIndex = 2
        Me.chkStandardTheme.Text = "Standard Theme"
        Me.chkStandardTheme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStandardTheme.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSave.Location = New System.Drawing.Point(312, 274)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(86, 25)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Speichern"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnCancel.Location = New System.Drawing.Point(4, 274)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(92, 25)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Abbr."
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(169, 18)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Temp. Monitor & CPU Streß:"
        Me.Label1.UseMnemonic = False
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BackColor = System.Drawing.Color.Azure
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"30", "45", "60", "120"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(341, 33)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(57, 64)
        Me.CheckedListBox1.TabIndex = 6
        '
        'LogSizeBox
        '
        Me.LogSizeBox.FormattingEnabled = True
        Me.LogSizeBox.Items.AddRange(New Object() {"5", "10", "50", "100", "200", "500", "1000"})
        Me.LogSizeBox.Location = New System.Drawing.Point(126, 8)
        Me.LogSizeBox.Margin = New System.Windows.Forms.Padding(4)
        Me.LogSizeBox.Name = "LogSizeBox"
        Me.LogSizeBox.Size = New System.Drawing.Size(49, 21)
        Me.LogSizeBox.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label4.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Assertive
        Me.Label4.Location = New System.Drawing.Point(4, 34)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(382, 22)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "10 KB entsprechen ca. 2 min  - 100KB = 10min --> 1000 KB (Max) ca. 100min"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.UseCompatibleTextRendering = True
        Me.Label4.UseMnemonic = False
        '
        'LogStartStopBox
        '
        Me.LogStartStopBox.AutoSize = True
        Me.LogStartStopBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LogStartStopBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogStartStopBox.Location = New System.Drawing.Point(262, 9)
        Me.LogStartStopBox.Margin = New System.Windows.Forms.Padding(4)
        Me.LogStartStopBox.Name = "LogStartStopBox"
        Me.LogStartStopBox.Size = New System.Drawing.Size(121, 17)
        Me.LogStartStopBox.TabIndex = 3
        Me.LogStartStopBox.Text = "Stop Backround Log"
        Me.LogStartStopBox.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 8)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Log Start & Größe:"
        Me.Label3.UseMnemonic = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(176, 9)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 17)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "KB"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label20.UseMnemonic = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkStandardTheme)
        Me.Panel2.Controls.Add(Me.chkDarkTheme)
        Me.Panel2.Controls.Add(Me.lblThemeSetting)
        Me.Panel2.Location = New System.Drawing.Point(-1, 131)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(421, 35)
        Me.Panel2.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoEllipsis = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(162, 80)
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
        Me.Label6.Location = New System.Drawing.Point(229, 56)
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
        Me.Label14.Location = New System.Drawing.Point(92, 55)
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
        Me.Label15.Location = New System.Drawing.Point(145, 56)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 13)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Rechenoperationen"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Bahnschrift Light SemiCondensed", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(265, 56)
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
        Me.Label17.Location = New System.Drawing.Point(136, 41)
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
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.LogStartStopBox)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.LogSizeBox)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Location = New System.Drawing.Point(5, 103)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(393, 165)
        Me.Panel3.TabIndex = 13
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.BootBox)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Location = New System.Drawing.Point(-1, 68)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(393, 64)
        Me.Panel4.TabIndex = 14
        '
        'BootBox
        '
        Me.BootBox.AutoSize = True
        Me.BootBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BootBox.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!)
        Me.BootBox.Location = New System.Drawing.Point(126, 2)
        Me.BootBox.Name = "BootBox"
        Me.BootBox.Size = New System.Drawing.Size(90, 17)
        Me.BootBox.TabIndex = 8
        Me.BootBox.Text = "Start on Boot"
        Me.BootBox.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 0)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 18)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "System Start:"
        Me.Label9.UseMnemonic = False
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label18.Font = New System.Drawing.Font("Bahnschrift SemiLight", 8.25!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label18.Location = New System.Drawing.Point(4, 25)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(388, 33)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "Hier können die Einstellungen für einen Start von CoolCore bei System-boot vorgen" &
    "ommen werden."
        Me.Label18.UseCompatibleTextRendering = True
        Me.Label18.UseMnemonic = False
        '
        'InfoButton
        '
        Me.InfoButton.BackgroundImage = Global.CoolCore.My.Resources.Resources._009_system
        Me.InfoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.InfoButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InfoButton.FlatAppearance.BorderSize = 2
        Me.InfoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.InfoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.InfoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InfoButton.Location = New System.Drawing.Point(10, 33)
        Me.InfoButton.Name = "InfoButton"
        Me.InfoButton.Size = New System.Drawing.Size(75, 59)
        Me.InfoButton.TabIndex = 14
        Me.InfoButton.UseVisualStyleBackColor = True
        '
        'OptionsForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(403, 303)
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
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblThemeSetting As Label
    Friend WithEvents chkDarkTheme As CheckBox
    Friend WithEvents chkStandardTheme As CheckBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents LogSizeBox As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LogStartStopBox As CheckBox
    Friend WithEvents Panel2 As Panel
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
End Class
