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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LogSizeBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LogStartStopBox = New System.Windows.Forms.CheckBox()
        Me.LogTimeLabel = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblThemeSetting
        '
        Me.lblThemeSetting.AutoSize = True
        Me.lblThemeSetting.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblThemeSetting.Location = New System.Drawing.Point(12, 18)
        Me.lblThemeSetting.Name = "lblThemeSetting"
        Me.lblThemeSetting.Size = New System.Drawing.Size(132, 18)
        Me.lblThemeSetting.TabIndex = 0
        Me.lblThemeSetting.Text = "Thema-Einstellungen:"
        '
        'chkDarkTheme
        '
        Me.chkDarkTheme.AutoSize = True
        Me.chkDarkTheme.Location = New System.Drawing.Point(15, 48)
        Me.chkDarkTheme.Name = "chkDarkTheme"
        Me.chkDarkTheme.Size = New System.Drawing.Size(82, 17)
        Me.chkDarkTheme.TabIndex = 1
        Me.chkDarkTheme.Text = "DarkTheme"
        Me.chkDarkTheme.UseVisualStyleBackColor = True
        '
        'chkStandardTheme
        '
        Me.chkStandardTheme.AutoSize = True
        Me.chkStandardTheme.Location = New System.Drawing.Point(103, 48)
        Me.chkStandardTheme.Name = "chkStandardTheme"
        Me.chkStandardTheme.Size = New System.Drawing.Size(105, 17)
        Me.chkStandardTheme.TabIndex = 2
        Me.chkStandardTheme.Text = "Standard Theme"
        Me.chkStandardTheme.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.Location = New System.Drawing.Point(265, 324)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Speichern"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(15, 324)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Abbr."
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(195, 18)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Temp. Monitor CPU stress länge:"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BackColor = System.Drawing.Color.Azure
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"30", "45", "60", "120"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(15, 101)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(52, 64)
        Me.CheckedListBox1.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(73, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(267, 64)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Auswahl der Zeit in Sekunden Cpu Stress-Intervall pro Messung."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.UseCompatibleTextRendering = True
        Me.Label2.UseMnemonic = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LogStartStopBox)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LogSizeBox)
        Me.Panel1.Location = New System.Drawing.Point(15, 198)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(325, 88)
        Me.Panel1.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(12, 177)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(189, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Log Start und Größe einstellen:"
        '
        'LogSizeBox
        '
        Me.LogSizeBox.FormattingEnabled = True
        Me.LogSizeBox.Items.AddRange(New Object() {"5", "10", "25", "50", "100"})
        Me.LogSizeBox.Location = New System.Drawing.Point(2, 60)
        Me.LogSizeBox.Name = "LogSizeBox"
        Me.LogSizeBox.Size = New System.Drawing.Size(121, 21)
        Me.LogSizeBox.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Bahnschrift Condensed", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FloralWhite
        Me.Label4.Location = New System.Drawing.Point(-1, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(325, 57)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Log Größe, bitte beachten Sie das eine größere Log-Datei bei älteren System zu ei" &
    "ner längeren lade Zeit führen kann. 10KB sind als Standard auch für ältere Proze" &
    "ssoren kein Problem."
        '
        'LogStartStopBox
        '
        Me.LogStartStopBox.AutoSize = True
        Me.LogStartStopBox.Location = New System.Drawing.Point(194, 62)
        Me.LogStartStopBox.Name = "LogStartStopBox"
        Me.LogStartStopBox.Size = New System.Drawing.Size(112, 17)
        Me.LogStartStopBox.TabIndex = 3
        Me.LogStartStopBox.Text = "Start/Stop logging"
        Me.LogStartStopBox.UseVisualStyleBackColor = True
        '
        'LogTimeLabel
        '
        Me.LogTimeLabel.AutoSize = True
        Me.LogTimeLabel.BackColor = System.Drawing.SystemColors.Control
        Me.LogTimeLabel.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogTimeLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LogTimeLabel.Location = New System.Drawing.Point(207, 177)
        Me.LogTimeLabel.Name = "LogTimeLabel"
        Me.LogTimeLabel.Size = New System.Drawing.Size(14, 18)
        Me.LogTimeLabel.TabIndex = 0
        Me.LogTimeLabel.Text = "-"
        '
        'OptionsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 359)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LogTimeLabel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.chkStandardTheme)
        Me.Controls.Add(Me.chkDarkTheme)
        Me.Controls.Add(Me.lblThemeSetting)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsForm"
        Me.Text = "Settings"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
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
End Class
