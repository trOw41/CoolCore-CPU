<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsForm))
        Me.lblThemeSetting = New System.Windows.Forms.Label()
        Me.chkDarkTheme = New System.Windows.Forms.CheckBox()
        Me.chkStandardTheme = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LogStartStopBox = New System.Windows.Forms.CheckBox()
        Me.LogSizeBox = New System.Windows.Forms.ComboBox()
        Me.LogTimeLabel = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblThemeSetting
        '
        Me.lblThemeSetting.AutoSize = True
        Me.lblThemeSetting.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblThemeSetting.Location = New System.Drawing.Point(3, 5)
        Me.lblThemeSetting.Name = "lblThemeSetting"
        Me.lblThemeSetting.Size = New System.Drawing.Size(132, 18)
        Me.lblThemeSetting.TabIndex = 0
        Me.lblThemeSetting.Text = "Thema-Einstellungen:"
        '
        'chkDarkTheme
        '
        Me.chkDarkTheme.AutoSize = True
        Me.chkDarkTheme.Location = New System.Drawing.Point(174, 9)
        Me.chkDarkTheme.Name = "chkDarkTheme"
        Me.chkDarkTheme.Size = New System.Drawing.Size(82, 17)
        Me.chkDarkTheme.TabIndex = 1
        Me.chkDarkTheme.Text = "DarkTheme"
        Me.chkDarkTheme.UseVisualStyleBackColor = True
        '
        'chkStandardTheme
        '
        Me.chkStandardTheme.AutoSize = True
        Me.chkStandardTheme.Location = New System.Drawing.Point(276, 9)
        Me.chkStandardTheme.Name = "chkStandardTheme"
        Me.chkStandardTheme.Size = New System.Drawing.Size(105, 17)
        Me.chkStandardTheme.TabIndex = 2
        Me.chkStandardTheme.Text = "Standard Theme"
        Me.chkStandardTheme.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.Location = New System.Drawing.Point(326, 437)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Speichern"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(5, 437)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Abbr."
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(93, 40)
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
        Me.CheckedListBox1.Location = New System.Drawing.Point(356, 47)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(45, 64)
        Me.CheckedListBox1.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(396, 187)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = resources.GetString("Label2.Text")
        Me.Label2.UseCompatibleTextRendering = True
        Me.Label2.UseMnemonic = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LogStartStopBox)
        Me.Panel1.Controls.Add(Me.LogSizeBox)
        Me.Panel1.Controls.Add(Me.LogTimeLabel)
        Me.Panel1.Location = New System.Drawing.Point(5, 343)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(396, 83)
        Me.Panel1.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(6, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(385, 49)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Log Größe, bitte beachten Sie das eine größere Log-Datei bei älteren System zu ei" &
    "ner längeren lade Zeit führen kann. 10KB sind als Standard auch für ältere Proze" &
    "ssoren kein Problem."
        '
        'LogStartStopBox
        '
        Me.LogStartStopBox.AutoSize = True
        Me.LogStartStopBox.Location = New System.Drawing.Point(279, 61)
        Me.LogStartStopBox.Name = "LogStartStopBox"
        Me.LogStartStopBox.Size = New System.Drawing.Size(112, 17)
        Me.LogStartStopBox.TabIndex = 3
        Me.LogStartStopBox.Text = "Start/Stop logging"
        Me.LogStartStopBox.UseVisualStyleBackColor = True
        '
        'LogSizeBox
        '
        Me.LogSizeBox.FormattingEnabled = True
        Me.LogSizeBox.Items.AddRange(New Object() {"5", "10", "25", "50", "100"})
        Me.LogSizeBox.Location = New System.Drawing.Point(4, 57)
        Me.LogSizeBox.Name = "LogSizeBox"
        Me.LogSizeBox.Size = New System.Drawing.Size(121, 21)
        Me.LogSizeBox.TabIndex = 1
        '
        'LogTimeLabel
        '
        Me.LogTimeLabel.AutoSize = True
        Me.LogTimeLabel.BackColor = System.Drawing.Color.Transparent
        Me.LogTimeLabel.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogTimeLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LogTimeLabel.Location = New System.Drawing.Point(193, 61)
        Me.LogTimeLabel.Name = "LogTimeLabel"
        Me.LogTimeLabel.Size = New System.Drawing.Size(12, 14)
        Me.LogTimeLabel.TabIndex = 0
        Me.LogTimeLabel.Text = "-"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(9, 322)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Log Start & Größe:"
        Me.Label3.UseMnemonic = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkStandardTheme)
        Me.Panel2.Controls.Add(Me.chkDarkTheme)
        Me.Panel2.Controls.Add(Me.lblThemeSetting)
        Me.Panel2.Location = New System.Drawing.Point(5, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(396, 36)
        Me.Panel2.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoEllipsis = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(169, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(181, 16)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "╨ ┈ ┈ ┈Intervall in Sekunden┈ ┈ ┈ ▶"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label5.UseMnemonic = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(358, 309)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(39, 41)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(29, 59)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(51, 55)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 11
        Me.PictureBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(169, 51)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(12, 40)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label7.UseMnemonic = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(78, 90)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 16)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label6.UseMnemonic = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(168, 105)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 14)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "▼"
        Me.Label8.UseMnemonic = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(9, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(28, 14)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "┣ ┈ ┈"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(9, 105)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 14)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "┇"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(9, 77)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(12, 14)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "┇"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(9, 59)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(12, 14)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "┇"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(9, 42)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(84, 42)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "┣ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ╖" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                        ║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                        ╚" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(92, 69)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(79, 16)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "═ ═ ═ ═ ═ ═ ═ ▶"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.UseMnemonic = False
        '
        'OptionsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 467)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents LogSizeBox As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LogStartStopBox As CheckBox
    Friend WithEvents LogTimeLabel As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
End Class
