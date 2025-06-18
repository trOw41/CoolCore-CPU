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
        Me.LblLoadingText = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TimeLabel = New System.Windows.Forms.Label()
        Me.PnlCpuFanAnimation = New System.Windows.Forms.Panel()
        Me.AnimationTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'LblLoadingText
        '
        Me.LblLoadingText.AutoEllipsis = True
        Me.LblLoadingText.BackColor = System.Drawing.Color.Transparent
        Me.LblLoadingText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LblLoadingText.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLoadingText.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblLoadingText.Location = New System.Drawing.Point(0, 27)
        Me.LblLoadingText.Name = "LblLoadingText"
        Me.LblLoadingText.Size = New System.Drawing.Size(325, 31)
        Me.LblLoadingText.TabIndex = 1
        Me.LblLoadingText.Text = "Bitte warten.."
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(4, 170)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(473, 18)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 2
        '
        'TimeLabel
        '
        Me.TimeLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TimeLabel.BackColor = System.Drawing.Color.Transparent
        Me.TimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TimeLabel.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimeLabel.Location = New System.Drawing.Point(1, 138)
        Me.TimeLabel.Name = "TimeLabel"
        Me.TimeLabel.Size = New System.Drawing.Size(330, 19)
        Me.TimeLabel.TabIndex = 3
        Me.TimeLabel.Text = "Label1"
        '
        'PnlCpuFanAnimation
        '
        Me.PnlCpuFanAnimation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCpuFanAnimation.BackColor = System.Drawing.Color.Transparent
        Me.PnlCpuFanAnimation.Location = New System.Drawing.Point(324, 14)
        Me.PnlCpuFanAnimation.Name = "PnlCpuFanAnimation"
        Me.PnlCpuFanAnimation.Padding = New System.Windows.Forms.Padding(3)
        Me.PnlCpuFanAnimation.Size = New System.Drawing.Size(147, 145)
        Me.PnlCpuFanAnimation.TabIndex = 4
        '
        'AnimationTimer
        '
        Me.AnimationTimer.Interval = 30
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(483, 192)
        Me.Controls.Add(Me.PnlCpuFanAnimation)
        Me.Controls.Add(Me.TimeLabel)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.LblLoadingText)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form3"
        Me.ShowInTaskbar = False
        Me.Text = "Form3"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblLoadingText As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents TimeLabel As Label
    Friend WithEvents PnlCpuFanAnimation As Panel
    Friend WithEvents AnimationTimer As Timer
End Class
