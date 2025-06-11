<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CpuinfoForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CpuinfoForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.InfoList = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CPU Info:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'InfoList
        '
        Me.InfoList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InfoList.Font = New System.Drawing.Font("Bahnschrift", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InfoList.FullRowSelect = True
        Me.InfoList.HideSelection = False
        Me.InfoList.Location = New System.Drawing.Point(0, 25)
        Me.InfoList.MultiSelect = False
        Me.InfoList.Name = "InfoList"
        Me.InfoList.ShowItemToolTips = True
        Me.InfoList.Size = New System.Drawing.Size(611, 606)
        Me.InfoList.TabIndex = 1
        Me.InfoList.UseCompatibleStateImageBehavior = False
        Me.InfoList.View = System.Windows.Forms.View.Details
        '
        'CpuinfoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 631)
        Me.Controls.Add(Me.InfoList)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CpuinfoForm"
        Me.Text = "Detail Cpu Info"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents InfoList As ListView
End Class
