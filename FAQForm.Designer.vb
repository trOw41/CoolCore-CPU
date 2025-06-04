<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FAQForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FAQForm))
        Me.LblTitel = New System.Windows.Forms.Label()
        Me.TrvFAQ = New System.Windows.Forms.TreeView()
        Me.TxtAnswer = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'LblTitel
        '
        Me.LblTitel.AutoEllipsis = True
        Me.LblTitel.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblTitel.Font = New System.Drawing.Font("Bahnschrift SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitel.Location = New System.Drawing.Point(0, 0)
        Me.LblTitel.Name = "LblTitel"
        Me.LblTitel.Size = New System.Drawing.Size(912, 29)
        Me.LblTitel.TabIndex = 0
        Me.LblTitel.Text = "Häufig gestellte Fragen (FAQ)"
        Me.LblTitel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TrvFAQ
        '
        Me.TrvFAQ.Dock = System.Windows.Forms.DockStyle.Left
        Me.TrvFAQ.Font = New System.Drawing.Font("Bahnschrift SemiLight", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrvFAQ.FullRowSelect = True
        Me.TrvFAQ.Location = New System.Drawing.Point(0, 29)
        Me.TrvFAQ.Name = "TrvFAQ"
        Me.TrvFAQ.ShowNodeToolTips = True
        Me.TrvFAQ.Size = New System.Drawing.Size(323, 484)
        Me.TrvFAQ.TabIndex = 1
        '
        'TxtAnswer
        '
        Me.TxtAnswer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TxtAnswer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAnswer.Dock = System.Windows.Forms.DockStyle.Right
        Me.TxtAnswer.Font = New System.Drawing.Font("Bahnschrift SemiLight", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAnswer.Location = New System.Drawing.Point(329, 29)
        Me.TxtAnswer.Name = "TxtAnswer"
        Me.TxtAnswer.ReadOnly = True
        Me.TxtAnswer.Size = New System.Drawing.Size(583, 484)
        Me.TxtAnswer.TabIndex = 2
        Me.TxtAnswer.Text = ""
        '
        'FAQForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(912, 513)
        Me.Controls.Add(Me.TxtAnswer)
        Me.Controls.Add(Me.TrvFAQ)
        Me.Controls.Add(Me.LblTitel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FAQForm"
        Me.Text = "CoolCore FAQ"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LblTitel As Label
    Friend WithEvents TrvFAQ As TreeView
    Friend WithEvents TxtAnswer As RichTextBox
End Class
