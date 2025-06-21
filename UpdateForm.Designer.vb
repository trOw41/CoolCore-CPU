<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UpdateForm))
        Me.TimeLabel = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ProgressLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TimeLabel
        '
        Me.TimeLabel.AutoSize = True
        Me.TimeLabel.Font = New System.Drawing.Font("Bahnschrift", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimeLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.TimeLabel.Location = New System.Drawing.Point(12, 38)
        Me.TimeLabel.Name = "TimeLabel"
        Me.TimeLabel.Size = New System.Drawing.Size(39, 14)
        Me.TimeLabel.TabIndex = 0
        Me.TimeLabel.Text = "Dauer"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.GreenYellow
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.ForeColor = System.Drawing.Color.GreenYellow
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 61)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(280, 23)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.TabIndex = 1
        '
        'ProgressLabel
        '
        Me.ProgressLabel.AutoSize = True
        Me.ProgressLabel.Font = New System.Drawing.Font("Bahnschrift", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProgressLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ProgressLabel.Location = New System.Drawing.Point(12, 9)
        Me.ProgressLabel.Name = "ProgressLabel"
        Me.ProgressLabel.Size = New System.Drawing.Size(37, 14)
        Me.ProgressLabel.TabIndex = 0
        Me.ProgressLabel.Text = "Datei:"
        '
        'UpdateForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Highlight
        Me.ClientSize = New System.Drawing.Size(280, 84)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.ProgressLabel)
        Me.Controls.Add(Me.TimeLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "UpdateForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update:"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TimeLabel As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ProgressLabel As Label
End Class
