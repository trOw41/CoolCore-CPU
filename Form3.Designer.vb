<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.PnlCpuFanAnimation = New System.Windows.Forms.Panel()
        Me.TimeLabel = New System.Windows.Forms.Label()
        Me.AnimationTimer = New System.Windows.Forms.Timer(Me.components)
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblResults = New System.Windows.Forms.Label()
        Me.PnlCpuFanAnimation.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 166)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(345, 17)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 2
        '
        'PnlCpuFanAnimation
        '
        Me.PnlCpuFanAnimation.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.PnlCpuFanAnimation.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PnlCpuFanAnimation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PnlCpuFanAnimation.Controls.Add(Me.TimeLabel)
        Me.PnlCpuFanAnimation.Location = New System.Drawing.Point(175, 3)
        Me.PnlCpuFanAnimation.Name = "PnlCpuFanAnimation"
        Me.PnlCpuFanAnimation.Padding = New System.Windows.Forms.Padding(3)
        Me.PnlCpuFanAnimation.Size = New System.Drawing.Size(169, 161)
        Me.PnlCpuFanAnimation.TabIndex = 4
        '
        'TimeLabel
        '
        Me.TimeLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TimeLabel.BackColor = System.Drawing.Color.Transparent
        Me.TimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TimeLabel.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimeLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TimeLabel.Location = New System.Drawing.Point(3, 159)
        Me.TimeLabel.Name = "TimeLabel"
        Me.TimeLabel.Size = New System.Drawing.Size(162, 52)
        Me.TimeLabel.TabIndex = 4
        Me.TimeLabel.Text = "Label1"
        Me.TimeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'AnimationTimer
        '
        Me.AnimationTimer.Interval = 30
        '
        'lblStatus
        '
        Me.lblStatus.AutoEllipsis = True
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(0, 33)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(175, 37)
        Me.lblStatus.TabIndex = 5
        Me.lblStatus.Text = "Bitte warten.."
        '
        'lblResults
        '
        Me.lblResults.AutoEllipsis = True
        Me.lblResults.BackColor = System.Drawing.Color.Transparent
        Me.lblResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblResults.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResults.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblResults.Location = New System.Drawing.Point(0, 75)
        Me.lblResults.Name = "lblResults"
        Me.lblResults.Size = New System.Drawing.Size(175, 91)
        Me.lblResults.TabIndex = 5
        Me.lblResults.Text = "bitte warten.."
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BackgroundImage = Global.CoolCore.My.Resources.Resources.border8
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(345, 183)
        Me.Controls.Add(Me.lblResults)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.PnlCpuFanAnimation)
        Me.Controls.Add(Me.ProgressBar1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form3"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form3"
        Me.TopMost = True
        Me.PnlCpuFanAnimation.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents PnlCpuFanAnimation As Panel
    Friend WithEvents AnimationTimer As Timer
    Friend WithEvents TimeLabel As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblResults As Label
End Class
