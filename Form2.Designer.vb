
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form2
    Inherits System.Windows.Forms.Form
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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.PanelColorLegend = New System.Windows.Forms.Panel()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.AutoFitMinFontSize = 9
        Legend1.BorderColor = System.Drawing.Color.Black
        Legend1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
        Legend1.Font = New System.Drawing.Font("Bahnschrift Light SemiCondensed", 8.0!)
        Legend1.InterlacedRows = True
        Legend1.IsEquallySpacedItems = True
        Legend1.IsTextAutoFit = False
        Legend1.Name = "Legend1"
        Legend1.TitleBackColor = System.Drawing.Color.Transparent
        Legend1.TitleFont = New System.Drawing.Font("Bahnschrift Condensed", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(2, 12)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Font = New System.Drawing.Font("Bahnschrift", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Series1.Legend = "Legend1"
        Series1.Name = "Temperature"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(798, 493)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'PanelColorLegend
        '
        Me.PanelColorLegend.Location = New System.Drawing.Point(70, 511)
        Me.PanelColorLegend.Name = "PanelColorLegend"
        Me.PanelColorLegend.Size = New System.Drawing.Size(288, 21)
        Me.PanelColorLegend.TabIndex = 1
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 540)
        Me.Controls.Add(Me.PanelColorLegend)
        Me.Controls.Add(Me.Chart1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "Form2"
        Me.Text = "Chart"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents PanelColorLegend As Panel
End Class
