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
        Me.SuspendLayout()
        '
        'lblThemeSetting
        '
        Me.lblThemeSetting.AutoSize = True
        Me.lblThemeSetting.Location = New System.Drawing.Point(12, 9)
        Me.lblThemeSetting.Name = "lblThemeSetting"
        Me.lblThemeSetting.Size = New System.Drawing.Size(109, 13)
        Me.lblThemeSetting.TabIndex = 0
        Me.lblThemeSetting.Text = "Thema-Einstellungen:"
        '
        'chkDarkTheme
        '
        Me.chkDarkTheme.AutoSize = True
        Me.chkDarkTheme.Location = New System.Drawing.Point(12, 45)
        Me.chkDarkTheme.Name = "chkDarkTheme"
        Me.chkDarkTheme.Size = New System.Drawing.Size(82, 17)
        Me.chkDarkTheme.TabIndex = 1
        Me.chkDarkTheme.Text = "DarkTheme"
        Me.chkDarkTheme.UseVisualStyleBackColor = True
        '
        'chkStandardTheme
        '
        Me.chkStandardTheme.AutoSize = True
        Me.chkStandardTheme.Location = New System.Drawing.Point(173, 45)
        Me.chkStandardTheme.Name = "chkStandardTheme"
        Me.chkStandardTheme.Size = New System.Drawing.Size(105, 17)
        Me.chkStandardTheme.TabIndex = 2
        Me.chkStandardTheme.Text = "Standard Theme"
        Me.chkStandardTheme.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.Location = New System.Drawing.Point(259, 227)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Speichern"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(15, 227)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Abbr."
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'OptionsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 262)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.chkStandardTheme)
        Me.Controls.Add(Me.chkDarkTheme)
        Me.Controls.Add(Me.lblThemeSetting)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsForm"
        Me.Text = "OptionsForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblThemeSetting As Label
    Friend WithEvents chkDarkTheme As CheckBox
    Friend WithEvents chkStandardTheme As CheckBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
End Class
