<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GrafikInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GrafikInfo))
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SystemViewList = New System.Windows.Forms.ListView()
        Me.Tabpane = New System.Windows.Forms.TabControl()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Tabpane.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(121, 26)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.BackColor = System.Drawing.SystemColors.HotTrack
        Me.ToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripMenuItem1.Image = Global.CoolCore.My.Resources.Resources._036_folder
        Me.ToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(120, 22)
        Me.ToolStripMenuItem1.Text = "kopieren"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "003-list.png")
        '
        'TabPage2
        '
        Me.TabPage2.BackgroundImage = CType(resources.GetObject("TabPage2.BackgroundImage"), System.Drawing.Image)
        Me.TabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage2.Controls.Add(Me.SystemViewList)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(387, 424)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "System Info:"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SystemViewList
        '
        Me.SystemViewList.BackColor = System.Drawing.SystemColors.Window
        Me.SystemViewList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SystemViewList.ContextMenuStrip = Me.ContextMenuStrip1
        Me.SystemViewList.Font = New System.Drawing.Font("Bahnschrift SemiLight SemiConde", 10.25!)
        Me.SystemViewList.FullRowSelect = True
        Me.SystemViewList.GridLines = True
        Me.SystemViewList.HideSelection = False
        Me.SystemViewList.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.SystemViewList.Location = New System.Drawing.Point(3, 3)
        Me.SystemViewList.MultiSelect = False
        Me.SystemViewList.Name = "SystemViewList"
        Me.SystemViewList.ShowItemToolTips = True
        Me.SystemViewList.Size = New System.Drawing.Size(381, 418)
        Me.SystemViewList.SmallImageList = Me.ImageList1
        Me.SystemViewList.TabIndex = 0
        Me.SystemViewList.UseCompatibleStateImageBehavior = False
        Me.SystemViewList.View = System.Windows.Forms.View.Details
        '
        'Tabpane
        '
        Me.Tabpane.Controls.Add(Me.TabPage2)
        Me.Tabpane.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabpane.HotTrack = True
        Me.Tabpane.Location = New System.Drawing.Point(0, 0)
        Me.Tabpane.Multiline = True
        Me.Tabpane.Name = "Tabpane"
        Me.Tabpane.Padding = New System.Drawing.Point(0, 0)
        Me.Tabpane.SelectedIndex = 0
        Me.Tabpane.ShowToolTips = True
        Me.Tabpane.Size = New System.Drawing.Size(395, 450)
        Me.Tabpane.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.Tabpane.TabIndex = 6
        '
        'GrafikInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 450)
        Me.Controls.Add(Me.Tabpane)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "GrafikInfo"
        Me.Text = "System Info:"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.Tabpane.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents SystemViewList As ListView
    Friend WithEvents Tabpane As TabControl
End Class
