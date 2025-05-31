<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form4))
        Me.LblInstructions = New System.Windows.Forms.Label()
        Me.ListBoxArchives = New System.Windows.Forms.ListBox()
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LblInstructions
        '
        Me.LblInstructions.AutoSize = True
        Me.LblInstructions.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.LblInstructions.Font = New System.Drawing.Font("Bahnschrift SemiLight", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInstructions.Location = New System.Drawing.Point(12, 9)
        Me.LblInstructions.Name = "LblInstructions"
        Me.LblInstructions.Size = New System.Drawing.Size(281, 19)
        Me.LblInstructions.TabIndex = 0
        Me.LblInstructions.Text = "Wählen Sie eine archivierte Messung:"
        '
        'ListBoxArchives
        '
        Me.ListBoxArchives.FormattingEnabled = True
        Me.ListBoxArchives.Location = New System.Drawing.Point(12, 49)
        Me.ListBoxArchives.Name = "ListBoxArchives"
        Me.ListBoxArchives.Size = New System.Drawing.Size(360, 173)
        Me.ListBoxArchives.TabIndex = 1
        '
        'BtnSelect
        '
        Me.BtnSelect.Location = New System.Drawing.Point(12, 241)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(75, 23)
        Me.BtnSelect.TabIndex = 2
        Me.BtnSelect.Text = "Button1"
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(297, 241)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 3
        Me.BtnCancel.Text = "Button2"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(384, 271)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnSelect)
        Me.Controls.Add(Me.ListBoxArchives)
        Me.Controls.Add(Me.LblInstructions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form4"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form4"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblInstructions As Label
    Friend WithEvents ListBoxArchives As ListBox
    Friend WithEvents BtnSelect As Button
    Friend WithEvents BtnCancel As Button
End Class
