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
        Me.Tabpane = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Loadlbl = New System.Windows.Forms.Label()
        Me.GCList = New System.Windows.Forms.ListView()
        Me.GCTempBox = New System.Windows.Forms.Label()
        Me.GCNameBox = New System.Windows.Forms.Label()
        Me.GCLogo = New System.Windows.Forms.PictureBox()
        Me.GCClockBox = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GCTempLabel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SystemViewList = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tabpane.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.GCLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tabpane
        '
        Me.Tabpane.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.Tabpane.Controls.Add(Me.TabPage1)
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
        'TabPage1
        '
        Me.TabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage1.Controls.Add(Me.Panel4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(387, 421)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Graphics:"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.CoolCore.My.Resources.Resources.border4
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Loadlbl)
        Me.Panel4.Controls.Add(Me.GCList)
        Me.Panel4.Controls.Add(Me.GCTempBox)
        Me.Panel4.Controls.Add(Me.GCNameBox)
        Me.Panel4.Controls.Add(Me.GCLogo)
        Me.Panel4.Controls.Add(Me.GCClockBox)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.GCTempLabel)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(381, 415)
        Me.Panel4.TabIndex = 3
        '
        'Loadlbl
        '
        Me.Loadlbl.BackColor = System.Drawing.Color.Transparent
        Me.Loadlbl.Font = New System.Drawing.Font("Bahnschrift SemiLight", 18.0!)
        Me.Loadlbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Loadlbl.Location = New System.Drawing.Point(155, 79)
        Me.Loadlbl.Name = "Loadlbl"
        Me.Loadlbl.Size = New System.Drawing.Size(81, 39)
        Me.Loadlbl.TabIndex = 12
        Me.Loadlbl.Text = "load"
        Me.Loadlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GCList
        '
        Me.GCList.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.GCList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GCList.ContextMenuStrip = Me.ContextMenuStrip2
        Me.GCList.Font = New System.Drawing.Font("Bahnschrift SemiLight SemiConde", 9.75!)
        Me.GCList.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GCList.FullRowSelect = True
        Me.GCList.GridLines = True
        Me.GCList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.GCList.HideSelection = False
        Me.GCList.Location = New System.Drawing.Point(3, 292)
        Me.GCList.MultiSelect = False
        Me.GCList.Name = "GCList"
        Me.GCList.ShowItemToolTips = True
        Me.GCList.Size = New System.Drawing.Size(372, 112)
        Me.GCList.TabIndex = 5
        Me.GCList.UseCompatibleStateImageBehavior = False
        Me.GCList.View = System.Windows.Forms.View.List
        '
        'GCTempBox
        '
        Me.GCTempBox.AutoEllipsis = True
        Me.GCTempBox.BackColor = System.Drawing.Color.Transparent
        Me.GCTempBox.Font = New System.Drawing.Font("Bahnschrift SemiLight", 11.75!)
        Me.GCTempBox.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GCTempBox.Location = New System.Drawing.Point(87, 45)
        Me.GCTempBox.Name = "GCTempBox"
        Me.GCTempBox.Size = New System.Drawing.Size(65, 22)
        Me.GCTempBox.TabIndex = 11
        Me.GCTempBox.Text = "-------"
        Me.GCTempBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GCNameBox
        '
        Me.GCNameBox.BackColor = System.Drawing.Color.Transparent
        Me.GCNameBox.Font = New System.Drawing.Font("Bahnschrift SemiLight SemiConde", 11.25!)
        Me.GCNameBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GCNameBox.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GCNameBox.Location = New System.Drawing.Point(44, 209)
        Me.GCNameBox.Name = "GCNameBox"
        Me.GCNameBox.Size = New System.Drawing.Size(148, 16)
        Me.GCNameBox.TabIndex = 18
        Me.GCNameBox.Text = "Label15"
        Me.GCNameBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GCLogo
        '
        Me.GCLogo.BackColor = System.Drawing.Color.Transparent
        Me.GCLogo.Image = Global.CoolCore.My.Resources.Resources.Nvidia_Logo_wine
        Me.GCLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GCLogo.Location = New System.Drawing.Point(63, 121)
        Me.GCLogo.Name = "GCLogo"
        Me.GCLogo.Size = New System.Drawing.Size(117, 85)
        Me.GCLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GCLogo.TabIndex = 16
        Me.GCLogo.TabStop = False
        '
        'GCClockBox
        '
        Me.GCClockBox.BackColor = System.Drawing.Color.Transparent
        Me.GCClockBox.Font = New System.Drawing.Font("Bahnschrift SemiLight", 18.0!)
        Me.GCClockBox.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GCClockBox.Location = New System.Drawing.Point(195, 212)
        Me.GCClockBox.Name = "GCClockBox"
        Me.GCClockBox.Size = New System.Drawing.Size(143, 38)
        Me.GCClockBox.TabIndex = 11
        Me.GCClockBox.Text = "-------"
        Me.GCClockBox.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Bahnschrift SemiLight", 15.0!)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(263, 152)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 24)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Clock"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Bahnschrift SemiLight", 15.0!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label15.Location = New System.Drawing.Point(18, 99)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 24)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Vendor"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Bahnschrift SemiLight", 15.0!)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(191, 41)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 24)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "V-Ram"
        '
        'GCTempLabel
        '
        Me.GCTempLabel.AutoSize = True
        Me.GCTempLabel.BackColor = System.Drawing.Color.Transparent
        Me.GCTempLabel.Font = New System.Drawing.Font("Bahnschrift SemiLight", 13.0!)
        Me.GCTempLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.GCTempLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GCTempLabel.Location = New System.Drawing.Point(38, 16)
        Me.GCTempLabel.Name = "GCTempLabel"
        Me.GCTempLabel.Size = New System.Drawing.Size(57, 22)
        Me.GCTempLabel.TabIndex = 13
        Me.GCTempLabel.Text = "Temp."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Bahnschrift SemiCondensed", 12.0!)
        Me.Label2.ForeColor = System.Drawing.Color.AntiqueWhite
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(202, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(173, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Grafik Vendor Information"
        '
        'TabPage2
        '
        Me.TabPage2.BackgroundImage = CType(resources.GetObject("TabPage2.BackgroundImage"), System.Drawing.Image)
        Me.TabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage2.Controls.Add(Me.SystemViewList)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(387, 421)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "System Info:"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SystemViewList
        '
        Me.SystemViewList.BackColor = System.Drawing.SystemColors.Window
        Me.SystemViewList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SystemViewList.ContextMenuStrip = Me.ContextMenuStrip1
        Me.SystemViewList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SystemViewList.Font = New System.Drawing.Font("Bahnschrift SemiLight SemiConde", 10.25!)
        Me.SystemViewList.FullRowSelect = True
        Me.SystemViewList.GridLines = True
        Me.SystemViewList.HideSelection = False
        Me.SystemViewList.Location = New System.Drawing.Point(3, 3)
        Me.SystemViewList.MultiSelect = False
        Me.SystemViewList.Name = "SystemViewList"
        Me.SystemViewList.ShowItemToolTips = True
        Me.SystemViewList.Size = New System.Drawing.Size(381, 415)
        Me.SystemViewList.SmallImageList = Me.ImageList1
        Me.SystemViewList.TabIndex = 0
        Me.SystemViewList.UseCompatibleStateImageBehavior = False
        Me.SystemViewList.View = System.Windows.Forms.View.Details
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
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(121, 26)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.BackColor = System.Drawing.SystemColors.Highlight
        Me.ToolStripMenuItem2.Image = Global.CoolCore.My.Resources.Resources._036_folder
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(120, 22)
        Me.ToolStripMenuItem2.Text = "kopieren"
        '
        'GrafikInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 450)
        Me.Controls.Add(Me.Tabpane)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GrafikInfo"
        Me.Text = "System Info:"
        Me.Tabpane.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.GCLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Tabpane As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Loadlbl As Label
    Friend WithEvents GCList As ListView
    Friend WithEvents GCTempBox As Label
    Friend WithEvents GCNameBox As Label
    Friend WithEvents GCLogo As PictureBox
    Friend WithEvents GCClockBox As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents GCTempLabel As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents SystemViewList As ListView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
End Class
