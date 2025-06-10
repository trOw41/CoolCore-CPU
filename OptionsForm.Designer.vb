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
        Me.LogSizeBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LogStartStopBox = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LogTimeLabel = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblThemeSetting
        '
        Me.lblThemeSetting.AutoSize = True
        Me.lblThemeSetting.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblThemeSetting.Location = New System.Drawing.Point(4, 6)
        Me.lblThemeSetting.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblThemeSetting.Name = "lblThemeSetting"
        Me.lblThemeSetting.Size = New System.Drawing.Size(190, 25)
        Me.lblThemeSetting.TabIndex = 0
        Me.lblThemeSetting.Text = "Thema-Einstellungen:"
        '
        'chkDarkTheme
        '
        Me.chkDarkTheme.AutoSize = True
        Me.chkDarkTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkDarkTheme.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDarkTheme.Location = New System.Drawing.Point(232, 11)
        Me.chkDarkTheme.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDarkTheme.Name = "chkDarkTheme"
        Me.chkDarkTheme.Size = New System.Drawing.Size(95, 20)
        Me.chkDarkTheme.TabIndex = 1
        Me.chkDarkTheme.Text = "DarkTheme"
        Me.chkDarkTheme.UseVisualStyleBackColor = True
        '
        'chkStandardTheme
        '
        Me.chkStandardTheme.AutoSize = True
        Me.chkStandardTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkStandardTheme.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStandardTheme.Location = New System.Drawing.Point(368, 11)
        Me.chkStandardTheme.Margin = New System.Windows.Forms.Padding(4)
        Me.chkStandardTheme.Name = "chkStandardTheme"
        Me.chkStandardTheme.Size = New System.Drawing.Size(124, 20)
        Me.chkStandardTheme.TabIndex = 2
        Me.chkStandardTheme.Text = "Standard Theme"
        Me.chkStandardTheme.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSave.Location = New System.Drawing.Point(436, 481)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(86, 28)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Speichern"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnCancel.Location = New System.Drawing.Point(11, 483)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(92, 28)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Abbr."
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(124, 49)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Temp. Monitor & CPU Streß:"
        Me.Label1.UseMnemonic = False
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BackColor = System.Drawing.Color.Azure
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"30", "45", "60", "120"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(434, 71)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(59, 64)
        Me.CheckedListBox1.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Gadugi", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(1, Byte), True)
        Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label2.Location = New System.Drawing.Point(7, 4)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(498, 172)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = resources.GetString("Label2.Text")
        Me.Label2.UseCompatibleTextRendering = True
        Me.Label2.UseMnemonic = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.Panel1.Controls.Add(Me.LogSizeBox)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LogStartStopBox)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(11, 343)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(511, 133)
        Me.Panel1.TabIndex = 8
        '
        'LogSizeBox
        '
        Me.LogSizeBox.FormattingEnabled = True
        Me.LogSizeBox.Items.AddRange(New Object() {"5", "10", "25", "50", "100"})
        Me.LogSizeBox.Location = New System.Drawing.Point(8, 92)
        Me.LogSizeBox.Margin = New System.Windows.Forms.Padding(4)
        Me.LogSizeBox.Name = "LogSizeBox"
        Me.LogSizeBox.Size = New System.Drawing.Size(160, 21)
        Me.LogSizeBox.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Gadugi", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.MintCream
        Me.Label4.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Assertive
        Me.Label4.Location = New System.Drawing.Point(4, 34)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(489, 64)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Eine größere Log-Datei kann bei älteren System zu einer längeren " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "lade Zeit führ" &
    "en kann. 10KB sind als Standard auch für ältere Prozessoren kein Problem."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.UseCompatibleTextRendering = True
        Me.Label4.UseMnemonic = False
        '
        'LogStartStopBox
        '
        Me.LogStartStopBox.AutoSize = True
        Me.LogStartStopBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LogStartStopBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogStartStopBox.Location = New System.Drawing.Point(201, 12)
        Me.LogStartStopBox.Margin = New System.Windows.Forms.Padding(4)
        Me.LogStartStopBox.Name = "LogStartStopBox"
        Me.LogStartStopBox.Size = New System.Drawing.Size(109, 17)
        Me.LogStartStopBox.TabIndex = 3
        Me.LogStartStopBox.Text = "Start/Stop logging"
        Me.LogStartStopBox.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(163, 25)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Log Start & Größe:"
        Me.Label3.UseMnemonic = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(67, 78)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(66, 52)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'LogTimeLabel
        '
        Me.LogTimeLabel.AutoSize = True
        Me.LogTimeLabel.BackColor = System.Drawing.Color.Transparent
        Me.LogTimeLabel.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogTimeLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LogTimeLabel.Location = New System.Drawing.Point(129, 129)
        Me.LogTimeLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LogTimeLabel.Name = "LogTimeLabel"
        Me.LogTimeLabel.Size = New System.Drawing.Size(12, 14)
        Me.LogTimeLabel.TabIndex = 0
        Me.LogTimeLabel.Text = "-"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkStandardTheme)
        Me.Panel2.Controls.Add(Me.chkDarkTheme)
        Me.Panel2.Controls.Add(Me.lblThemeSetting)
        Me.Panel2.Location = New System.Drawing.Point(11, -1)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(511, 44)
        Me.Panel2.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoEllipsis = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(242, 111)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(173, 20)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "┈ ┈ ┈Intervall in Sekunden┈ ┈ ┈ ▶"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.UseMnemonic = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(21, 63)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(36, 36)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 11
        Me.PictureBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(225, 63)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(16, 66)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label7.UseMnemonic = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(249, 89)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 12)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = " ◀┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ▶"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.UseMnemonic = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(228, 131)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 17)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "▼"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label8.UseMnemonic = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(12, 109)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
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
        Me.Label11.Location = New System.Drawing.Point(12, 95)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
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
        Me.Label12.Location = New System.Drawing.Point(12, 73)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
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
        Me.Label13.Location = New System.Drawing.Point(12, 52)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(84, 42)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "┣ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ┈ ╖" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                        ║" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                        ╚" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Bahnschrift SemiLight", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(129, 86)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 19)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "◀═ ═ ═ ═ ═ ═ ═ ═ ═ "
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label14.UseMnemonic = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Bahnschrift Light SemiCondensed", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(140, 105)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 13)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Rechenoperationen"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Bahnschrift Light SemiCondensed", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(289, 77)
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
        Me.Label17.Location = New System.Drawing.Point(147, 79)
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
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Location = New System.Drawing.Point(11, 151)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(511, 185)
        Me.Panel3.TabIndex = 13
        '
        'OptionsForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(529, 517)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LogTimeLabel)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(600, 596)
        Me.MinimizeBox = False
        Me.Name = "OptionsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
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
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Panel3 As Panel
End Class
