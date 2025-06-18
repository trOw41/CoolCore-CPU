<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportCPUInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.LoadArchivedMeasurementsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CpuInfoMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.IntelCPUDBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.AmdCPUDBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.FAQToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SupportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataSet1 = New System.Data.DataSet()
        Me.DataTable1 = New System.Data.DataTable()
        Me.DataColumn1 = New System.Data.DataColumn()
        Me.DataColumn2 = New System.Data.DataColumn()
        Me.LblStatusMessage = New System.Windows.Forms.Label()
        Me.Standard = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PicBox2 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.BtnToggleMonitor1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CoreTemp3 = New System.Windows.Forms.TextBox()
        Me.CoreTemp2 = New System.Windows.Forms.TextBox()
        Me.CoreTemp1 = New System.Windows.Forms.TextBox()
        Me.TjMax = New System.Windows.Forms.Label()
        Me.Power = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MaxTemplbl = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.MinTemplbl = New System.Windows.Forms.Label()
        Me.Core0 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Core2 = New System.Windows.Forms.Label()
        Me.Core3 = New System.Windows.Forms.Label()
        Me.CoreTemp = New System.Windows.Forms.TextBox()
        Me.LoadBox2 = New System.Windows.Forms.TextBox()
        Me.MaxTemp2 = New System.Windows.Forms.TextBox()
        Me.LoadBox1 = New System.Windows.Forms.TextBox()
        Me.MaxTemp1 = New System.Windows.Forms.TextBox()
        Me.MinTemp2 = New System.Windows.Forms.TextBox()
        Me.MinTemp1 = New System.Windows.Forms.TextBox()
        Me.PowerBox2 = New System.Windows.Forms.TextBox()
        Me.LoadBox = New System.Windows.Forms.TextBox()
        Me.MaxTemp3 = New System.Windows.Forms.TextBox()
        Me.MaxTemp = New System.Windows.Forms.TextBox()
        Me.LoadBox3 = New System.Windows.Forms.TextBox()
        Me.VBox4 = New System.Windows.Forms.TextBox()
        Me.VBox3 = New System.Windows.Forms.TextBox()
        Me.VBox2 = New System.Windows.Forms.TextBox()
        Me.Vbox1 = New System.Windows.Forms.TextBox()
        Me.MinTemp = New System.Windows.Forms.TextBox()
        Me.MinTemp3 = New System.Windows.Forms.TextBox()
        Me.TJBox = New System.Windows.Forms.TextBox()
        Me.PowerBox = New System.Windows.Forms.TextBox()
        Me.Lithography = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Threads = New System.Windows.Forms.Label()
        Me.AllCores = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TDP = New System.Windows.Forms.Label()
        Me.Revision = New System.Windows.Forms.Label()
        Me.VID = New System.Windows.Forms.Label()
        Me.Frequency = New System.Windows.Forms.Label()
        Me.Platform = New System.Windows.Forms.Label()
        Me.FanBox = New System.Windows.Forms.TextBox()
        Me.TDPBox = New System.Windows.Forms.TextBox()
        Me.LithographyBox = New System.Windows.Forms.TextBox()
        Me.SockBox = New System.Windows.Forms.TextBox()
        Me.CPUIDBox = New System.Windows.Forms.TextBox()
        Me.VidBox = New System.Windows.Forms.TextBox()
        Me.FrequencyBox2 = New System.Windows.Forms.TextBox()
        Me.FrequencyBox = New System.Windows.Forms.TextBox()
        Me.ThreadBox = New System.Windows.Forms.TextBox()
        Me.CoresBox = New System.Windows.Forms.TextBox()
        Me.PlatformBox = New System.Windows.Forms.TextBox()
        Me.ModelBox = New System.Windows.Forms.TextBox()
        Me.Model = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Loadlbl = New System.Windows.Forms.Label()
        Me.GCList = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Standard.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PicBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.GCLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.MdiWindowListItem = Me.OptionsToolStripMenuItem
        Me.MenuStrip1.Name = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem})
        Me.FileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CloseToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._014_close
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        resources.ApplyResources(Me.CloseToolStripMenuItem, "CloseToolStripMenuItem")
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.ToolStripSeparator2, Me.ExportCPUInfoToolStripMenuItem})
        Me.OptionsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        resources.ApplyResources(Me.OptionsToolStripMenuItem, "OptionsToolStripMenuItem")
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._038_system_1
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        resources.ApplyResources(Me.SettingsToolStripMenuItem, "SettingsToolStripMenuItem")
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'ExportCPUInfoToolStripMenuItem
        '
        Me.ExportCPUInfoToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._036_folder
        Me.ExportCPUInfoToolStripMenuItem.Name = "ExportCPUInfoToolStripMenuItem"
        resources.ApplyResources(Me.ExportCPUInfoToolStripMenuItem, "ExportCPUInfoToolStripMenuItem")
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.BackColor = System.Drawing.SystemColors.HighlightText
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogMenuItem, Me.ToolStripSeparator1, Me.LoadArchivedMeasurementsToolStripMenuItem, Me.ToolStripSeparator5, Me.CpuInfoMenu, Me.ToolStripSeparator6})
        Me.ToolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        resources.ApplyResources(Me.ToolsToolStripMenuItem, "ToolsToolStripMenuItem")
        '
        'LogMenuItem
        '
        Me.LogMenuItem.AutoToolTip = True
        Me.LogMenuItem.CheckOnClick = True
        Me.LogMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportLogToolStripMenuItem})
        Me.LogMenuItem.Image = Global.CoolCore.My.Resources.Resources._004_computer_science
        Me.LogMenuItem.Name = "LogMenuItem"
        resources.ApplyResources(Me.LogMenuItem, "LogMenuItem")
        '
        'ExportLogToolStripMenuItem
        '
        Me.ExportLogToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._030_ram_2
        Me.ExportLogToolStripMenuItem.Name = "ExportLogToolStripMenuItem"
        resources.ApplyResources(Me.ExportLogToolStripMenuItem, "ExportLogToolStripMenuItem")
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'LoadArchivedMeasurementsToolStripMenuItem
        '
        resources.ApplyResources(Me.LoadArchivedMeasurementsToolStripMenuItem, "LoadArchivedMeasurementsToolStripMenuItem")
        Me.LoadArchivedMeasurementsToolStripMenuItem.Name = "LoadArchivedMeasurementsToolStripMenuItem"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        '
        'CpuInfoMenu
        '
        Me.CpuInfoMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IntelCPUDBToolStripMenuItem, Me.ToolStripSeparator7, Me.AmdCPUDBToolStripMenuItem})
        Me.CpuInfoMenu.Image = Global.CoolCore.My.Resources.Resources._023_cpu
        Me.CpuInfoMenu.Name = "CpuInfoMenu"
        resources.ApplyResources(Me.CpuInfoMenu, "CpuInfoMenu")
        '
        'IntelCPUDBToolStripMenuItem
        '
        Me.IntelCPUDBToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources.IntelLogo
        Me.IntelCPUDBToolStripMenuItem.Name = "IntelCPUDBToolStripMenuItem"
        resources.ApplyResources(Me.IntelCPUDBToolStripMenuItem, "IntelCPUDBToolStripMenuItem")
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        resources.ApplyResources(Me.ToolStripSeparator7, "ToolStripSeparator7")
        '
        'AmdCPUDBToolStripMenuItem
        '
        Me.AmdCPUDBToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources.AMDLogo_Dark
        Me.AmdCPUDBToolStripMenuItem.Name = "AmdCPUDBToolStripMenuItem"
        resources.ApplyResources(Me.AmdCPUDBToolStripMenuItem, "AmdCPUDBToolStripMenuItem")
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        resources.ApplyResources(Me.ToolStripSeparator6, "ToolStripSeparator6")
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InfoMenuItem, Me.ToolStripSeparator3, Me.FAQToolStripMenuItem, Me.ToolStripSeparator4, Me.SupportToolStripMenuItem})
        Me.HelpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'InfoMenuItem
        '
        Me.InfoMenuItem.Image = Global.CoolCore.My.Resources.Resources._021_about
        Me.InfoMenuItem.Name = "InfoMenuItem"
        resources.ApplyResources(Me.InfoMenuItem, "InfoMenuItem")
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'FAQToolStripMenuItem
        '
        Me.FAQToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._032_faq_3
        Me.FAQToolStripMenuItem.Name = "FAQToolStripMenuItem"
        resources.ApplyResources(Me.FAQToolStripMenuItem, "FAQToolStripMenuItem")
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'SupportToolStripMenuItem
        '
        Me.SupportToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._031_faq_2
        Me.SupportToolStripMenuItem.Name = "SupportToolStripMenuItem"
        resources.ApplyResources(Me.SupportToolStripMenuItem, "SupportToolStripMenuItem")
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        Me.DataSet1.Tables.AddRange(New System.Data.DataTable() {Me.DataTable1})
        '
        'DataTable1
        '
        Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2})
        Me.DataTable1.TableName = "StringTable"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "Name"
        '
        'DataColumn2
        '
        Me.DataColumn2.ColumnName = "Wert"
        '
        'LblStatusMessage
        '
        resources.ApplyResources(Me.LblStatusMessage, "LblStatusMessage")
        Me.LblStatusMessage.Name = "LblStatusMessage"
        '
        'Standard
        '
        Me.Standard.Controls.Add(Me.TabPage1)
        Me.Standard.Controls.Add(Me.TabPage3)
        Me.Standard.Controls.Add(Me.TabPage2)
        resources.ApplyResources(Me.Standard, "Standard")
        Me.Standard.HotTrack = True
        Me.Standard.Multiline = True
        Me.Standard.Name = "Standard"
        Me.Standard.SelectedIndex = 0
        Me.Standard.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Name = "TabPage1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Controls.Add(Me.PicBox2)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Lithography)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Threads)
        Me.Panel1.Controls.Add(Me.AllCores)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.TDP)
        Me.Panel1.Controls.Add(Me.Revision)
        Me.Panel1.Controls.Add(Me.VID)
        Me.Panel1.Controls.Add(Me.Frequency)
        Me.Panel1.Controls.Add(Me.Platform)
        Me.Panel1.Controls.Add(Me.FanBox)
        Me.Panel1.Controls.Add(Me.TDPBox)
        Me.Panel1.Controls.Add(Me.LithographyBox)
        Me.Panel1.Controls.Add(Me.SockBox)
        Me.Panel1.Controls.Add(Me.CPUIDBox)
        Me.Panel1.Controls.Add(Me.VidBox)
        Me.Panel1.Controls.Add(Me.FrequencyBox2)
        Me.Panel1.Controls.Add(Me.FrequencyBox)
        Me.Panel1.Controls.Add(Me.ThreadBox)
        Me.Panel1.Controls.Add(Me.CoresBox)
        Me.Panel1.Controls.Add(Me.PlatformBox)
        Me.Panel1.Controls.Add(Me.ModelBox)
        Me.Panel1.Controls.Add(Me.Model)
        Me.Panel1.Name = "Panel1"
        '
        'PicBox2
        '
        Me.PicBox2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.PicBox2, "PicBox2")
        Me.PicBox2.Name = "PicBox2"
        Me.PicBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoEllipsis = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Name = "Label7"
        Me.Label7.UseCompatibleTextRendering = True
        Me.Label7.UseMnemonic = False
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Name = "Label3"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.CoreTemp3)
        Me.Panel2.Controls.Add(Me.CoreTemp2)
        Me.Panel2.Controls.Add(Me.CoreTemp1)
        Me.Panel2.Controls.Add(Me.TjMax)
        Me.Panel2.Controls.Add(Me.Power)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.MaxTemplbl)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.MinTemplbl)
        Me.Panel2.Controls.Add(Me.Core0)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Core2)
        Me.Panel2.Controls.Add(Me.Core3)
        Me.Panel2.Controls.Add(Me.CoreTemp)
        Me.Panel2.Controls.Add(Me.LoadBox2)
        Me.Panel2.Controls.Add(Me.MaxTemp2)
        Me.Panel2.Controls.Add(Me.LoadBox1)
        Me.Panel2.Controls.Add(Me.MaxTemp1)
        Me.Panel2.Controls.Add(Me.MinTemp2)
        Me.Panel2.Controls.Add(Me.MinTemp1)
        Me.Panel2.Controls.Add(Me.PowerBox2)
        Me.Panel2.Controls.Add(Me.LoadBox)
        Me.Panel2.Controls.Add(Me.MaxTemp3)
        Me.Panel2.Controls.Add(Me.MaxTemp)
        Me.Panel2.Controls.Add(Me.LoadBox3)
        Me.Panel2.Controls.Add(Me.VBox4)
        Me.Panel2.Controls.Add(Me.VBox3)
        Me.Panel2.Controls.Add(Me.VBox2)
        Me.Panel2.Controls.Add(Me.Vbox1)
        Me.Panel2.Controls.Add(Me.MinTemp)
        Me.Panel2.Controls.Add(Me.MinTemp3)
        Me.Panel2.Controls.Add(Me.TJBox)
        Me.Panel2.Controls.Add(Me.PowerBox)
        Me.Panel2.Name = "Panel2"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.BtnToggleMonitor1)
        Me.Panel3.Controls.Add(Me.Label1)
        resources.ApplyResources(Me.Panel3, "Panel3")
        Me.Panel3.Name = "Panel3"
        '
        'BtnToggleMonitor1
        '
        Me.BtnToggleMonitor1.BackgroundImage = Global.CoolCore.My.Resources.Resources.fan1
        resources.ApplyResources(Me.BtnToggleMonitor1, "BtnToggleMonitor1")
        Me.BtnToggleMonitor1.Name = "BtnToggleMonitor1"
        '
        'Label1
        '
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'CoreTemp3
        '
        Me.CoreTemp3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CoreTemp3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoreTemp3, "CoreTemp3")
        Me.CoreTemp3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoreTemp3.Name = "CoreTemp3"
        Me.CoreTemp3.ReadOnly = True
        '
        'CoreTemp2
        '
        Me.CoreTemp2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CoreTemp2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoreTemp2, "CoreTemp2")
        Me.CoreTemp2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoreTemp2.Name = "CoreTemp2"
        Me.CoreTemp2.ReadOnly = True
        '
        'CoreTemp1
        '
        Me.CoreTemp1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CoreTemp1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoreTemp1, "CoreTemp1")
        Me.CoreTemp1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoreTemp1.Name = "CoreTemp1"
        Me.CoreTemp1.ReadOnly = True
        '
        'TjMax
        '
        resources.ApplyResources(Me.TjMax, "TjMax")
        Me.TjMax.Name = "TjMax"
        '
        'Power
        '
        resources.ApplyResources(Me.Power, "Power")
        Me.Power.Name = "Power"
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'MaxTemplbl
        '
        resources.ApplyResources(Me.MaxTemplbl, "MaxTemplbl")
        Me.MaxTemplbl.Name = "MaxTemplbl"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'MinTemplbl
        '
        resources.ApplyResources(Me.MinTemplbl, "MinTemplbl")
        Me.MinTemplbl.Name = "MinTemplbl"
        '
        'Core0
        '
        Me.Core0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Core0, "Core0")
        Me.Core0.Name = "Core0"
        '
        'Label9
        '
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'Core2
        '
        Me.Core2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Core2, "Core2")
        Me.Core2.Name = "Core2"
        '
        'Core3
        '
        Me.Core3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Core3, "Core3")
        Me.Core3.Name = "Core3"
        '
        'CoreTemp
        '
        Me.CoreTemp.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CoreTemp.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoreTemp, "CoreTemp")
        Me.CoreTemp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoreTemp.Name = "CoreTemp"
        Me.CoreTemp.ReadOnly = True
        '
        'LoadBox2
        '
        Me.LoadBox2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.LoadBox2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LoadBox2, "LoadBox2")
        Me.LoadBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LoadBox2.Name = "LoadBox2"
        Me.LoadBox2.ReadOnly = True
        '
        'MaxTemp2
        '
        Me.MaxTemp2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.MaxTemp2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MaxTemp2, "MaxTemp2")
        Me.MaxTemp2.ForeColor = System.Drawing.Color.OrangeRed
        Me.MaxTemp2.Name = "MaxTemp2"
        Me.MaxTemp2.ReadOnly = True
        '
        'LoadBox1
        '
        Me.LoadBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.LoadBox1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LoadBox1, "LoadBox1")
        Me.LoadBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LoadBox1.Name = "LoadBox1"
        Me.LoadBox1.ReadOnly = True
        '
        'MaxTemp1
        '
        Me.MaxTemp1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.MaxTemp1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MaxTemp1, "MaxTemp1")
        Me.MaxTemp1.ForeColor = System.Drawing.Color.OrangeRed
        Me.MaxTemp1.Name = "MaxTemp1"
        Me.MaxTemp1.ReadOnly = True
        '
        'MinTemp2
        '
        Me.MinTemp2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.MinTemp2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MinTemp2, "MinTemp2")
        Me.MinTemp2.ForeColor = System.Drawing.Color.DodgerBlue
        Me.MinTemp2.Name = "MinTemp2"
        Me.MinTemp2.ReadOnly = True
        '
        'MinTemp1
        '
        Me.MinTemp1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.MinTemp1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MinTemp1, "MinTemp1")
        Me.MinTemp1.ForeColor = System.Drawing.Color.DodgerBlue
        Me.MinTemp1.Name = "MinTemp1"
        Me.MinTemp1.ReadOnly = True
        '
        'PowerBox2
        '
        Me.PowerBox2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PowerBox2.Cursor = System.Windows.Forms.Cursors.IBeam
        resources.ApplyResources(Me.PowerBox2, "PowerBox2")
        Me.PowerBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PowerBox2.Name = "PowerBox2"
        Me.PowerBox2.ReadOnly = True
        '
        'LoadBox
        '
        Me.LoadBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.LoadBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LoadBox, "LoadBox")
        Me.LoadBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LoadBox.HideSelection = False
        Me.LoadBox.Name = "LoadBox"
        Me.LoadBox.ReadOnly = True
        '
        'MaxTemp3
        '
        Me.MaxTemp3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.MaxTemp3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MaxTemp3, "MaxTemp3")
        Me.MaxTemp3.ForeColor = System.Drawing.Color.OrangeRed
        Me.MaxTemp3.Name = "MaxTemp3"
        Me.MaxTemp3.ReadOnly = True
        '
        'MaxTemp
        '
        Me.MaxTemp.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.MaxTemp.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MaxTemp, "MaxTemp")
        Me.MaxTemp.ForeColor = System.Drawing.Color.OrangeRed
        Me.MaxTemp.Name = "MaxTemp"
        Me.MaxTemp.ReadOnly = True
        '
        'LoadBox3
        '
        Me.LoadBox3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.LoadBox3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LoadBox3, "LoadBox3")
        Me.LoadBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LoadBox3.Name = "LoadBox3"
        Me.LoadBox3.ReadOnly = True
        '
        'VBox4
        '
        Me.VBox4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.VBox4.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.VBox4, "VBox4")
        Me.VBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VBox4.Name = "VBox4"
        Me.VBox4.ReadOnly = True
        '
        'VBox3
        '
        Me.VBox3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.VBox3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.VBox3, "VBox3")
        Me.VBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VBox3.Name = "VBox3"
        Me.VBox3.ReadOnly = True
        '
        'VBox2
        '
        Me.VBox2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.VBox2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.VBox2, "VBox2")
        Me.VBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VBox2.Name = "VBox2"
        Me.VBox2.ReadOnly = True
        '
        'Vbox1
        '
        Me.Vbox1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Vbox1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Vbox1, "Vbox1")
        Me.Vbox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Vbox1.Name = "Vbox1"
        Me.Vbox1.ReadOnly = True
        '
        'MinTemp
        '
        Me.MinTemp.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.MinTemp.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MinTemp, "MinTemp")
        Me.MinTemp.ForeColor = System.Drawing.Color.DodgerBlue
        Me.MinTemp.Name = "MinTemp"
        Me.MinTemp.ReadOnly = True
        '
        'MinTemp3
        '
        Me.MinTemp3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.MinTemp3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MinTemp3, "MinTemp3")
        Me.MinTemp3.ForeColor = System.Drawing.Color.DodgerBlue
        Me.MinTemp3.Name = "MinTemp3"
        Me.MinTemp3.ReadOnly = True
        '
        'TJBox
        '
        Me.TJBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TJBox.Cursor = System.Windows.Forms.Cursors.IBeam
        resources.ApplyResources(Me.TJBox, "TJBox")
        Me.TJBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TJBox.Name = "TJBox"
        Me.TJBox.ReadOnly = True
        '
        'PowerBox
        '
        Me.PowerBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PowerBox.Cursor = System.Windows.Forms.Cursors.IBeam
        resources.ApplyResources(Me.PowerBox, "PowerBox")
        Me.PowerBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PowerBox.Name = "PowerBox"
        Me.PowerBox.ReadOnly = True
        '
        'Lithography
        '
        Me.Lithography.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Lithography, "Lithography")
        Me.Lithography.Name = "Lithography"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Name = "Label10"
        '
        'Threads
        '
        Me.Threads.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Threads, "Threads")
        Me.Threads.Name = "Threads"
        '
        'AllCores
        '
        Me.AllCores.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.AllCores, "AllCores")
        Me.AllCores.Name = "AllCores"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'TDP
        '
        Me.TDP.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.TDP, "TDP")
        Me.TDP.Name = "TDP"
        '
        'Revision
        '
        Me.Revision.BackColor = System.Drawing.Color.Transparent
        Me.Revision.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Revision, "Revision")
        Me.Revision.Name = "Revision"
        '
        'VID
        '
        Me.VID.BackColor = System.Drawing.Color.Transparent
        Me.VID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.VID, "VID")
        Me.VID.Name = "VID"
        '
        'Frequency
        '
        Me.Frequency.BackColor = System.Drawing.Color.Transparent
        Me.Frequency.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Frequency, "Frequency")
        Me.Frequency.Name = "Frequency"
        '
        'Platform
        '
        Me.Platform.BackColor = System.Drawing.Color.Transparent
        Me.Platform.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Platform, "Platform")
        Me.Platform.Name = "Platform"
        '
        'FanBox
        '
        Me.FanBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.FanBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.FanBox, "FanBox")
        Me.FanBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FanBox.Name = "FanBox"
        Me.FanBox.ReadOnly = True
        '
        'TDPBox
        '
        Me.TDPBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TDPBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.TDPBox, "TDPBox")
        Me.TDPBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TDPBox.Name = "TDPBox"
        Me.TDPBox.ReadOnly = True
        '
        'LithographyBox
        '
        Me.LithographyBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.LithographyBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LithographyBox, "LithographyBox")
        Me.LithographyBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LithographyBox.Name = "LithographyBox"
        Me.LithographyBox.ReadOnly = True
        '
        'SockBox
        '
        Me.SockBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.SockBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.SockBox, "SockBox")
        Me.SockBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SockBox.Name = "SockBox"
        Me.SockBox.ReadOnly = True
        '
        'CPUIDBox
        '
        Me.CPUIDBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CPUIDBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CPUIDBox, "CPUIDBox")
        Me.CPUIDBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CPUIDBox.Name = "CPUIDBox"
        Me.CPUIDBox.ReadOnly = True
        '
        'VidBox
        '
        Me.VidBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.VidBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.VidBox, "VidBox")
        Me.VidBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VidBox.Name = "VidBox"
        Me.VidBox.ReadOnly = True
        '
        'FrequencyBox2
        '
        Me.FrequencyBox2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.FrequencyBox2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.FrequencyBox2, "FrequencyBox2")
        Me.FrequencyBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FrequencyBox2.Name = "FrequencyBox2"
        Me.FrequencyBox2.ReadOnly = True
        '
        'FrequencyBox
        '
        Me.FrequencyBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.FrequencyBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.FrequencyBox, "FrequencyBox")
        Me.FrequencyBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FrequencyBox.Name = "FrequencyBox"
        Me.FrequencyBox.ReadOnly = True
        '
        'ThreadBox
        '
        Me.ThreadBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ThreadBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.ThreadBox, "ThreadBox")
        Me.ThreadBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ThreadBox.Name = "ThreadBox"
        Me.ThreadBox.ReadOnly = True
        '
        'CoresBox
        '
        Me.CoresBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CoresBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoresBox, "CoresBox")
        Me.CoresBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoresBox.Name = "CoresBox"
        Me.CoresBox.ReadOnly = True
        '
        'PlatformBox
        '
        Me.PlatformBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PlatformBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.PlatformBox, "PlatformBox")
        Me.PlatformBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PlatformBox.Name = "PlatformBox"
        Me.PlatformBox.ReadOnly = True
        '
        'ModelBox
        '
        Me.ModelBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ModelBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.ModelBox, "ModelBox")
        Me.ModelBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ModelBox.Name = "ModelBox"
        Me.ModelBox.ReadOnly = True
        '
        'Model
        '
        resources.ApplyResources(Me.Model, "Model")
        Me.Model.BackColor = System.Drawing.Color.Transparent
        Me.Model.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Model.Name = "Model"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Panel4)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        resources.ApplyResources(Me.Panel4, "Panel4")
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
        Me.Panel4.Name = "Panel4"
        '
        'Loadlbl
        '
        resources.ApplyResources(Me.Loadlbl, "Loadlbl")
        Me.Loadlbl.Name = "Loadlbl"
        '
        'GCList
        '
        Me.GCList.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.GCList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.GCList, "GCList")
        Me.GCList.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GCList.FullRowSelect = True
        Me.GCList.GridLines = True
        Me.GCList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.GCList.HideSelection = False
        Me.GCList.MultiSelect = False
        Me.GCList.Name = "GCList"
        Me.GCList.ShowItemToolTips = True
        Me.GCList.SmallImageList = Me.ImageList1
        Me.GCList.UseCompatibleStateImageBehavior = False
        Me.GCList.View = System.Windows.Forms.View.SmallIcon
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "025-gpu.png")
        Me.ImageList1.Images.SetKeyName(1, "001-app.png")
        Me.ImageList1.Images.SetKeyName(2, "028-ram.png")
        Me.ImageList1.Images.SetKeyName(3, "003-coding.png")
        Me.ImageList1.Images.SetKeyName(4, "047-circuit.png")
        Me.ImageList1.Images.SetKeyName(5, "006-laptop.png")
        Me.ImageList1.Images.SetKeyName(6, "021-domain-servers.png")
        Me.ImageList1.Images.SetKeyName(7, "026-graphics-card.png")
        Me.ImageList1.Images.SetKeyName(8, "027-gpu-1.png")
        Me.ImageList1.Images.SetKeyName(9, "038-system-1.png")
        '
        'GCTempBox
        '
        Me.GCTempBox.AutoEllipsis = True
        Me.GCTempBox.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.GCTempBox, "GCTempBox")
        Me.GCTempBox.Name = "GCTempBox"
        '
        'GCNameBox
        '
        resources.ApplyResources(Me.GCNameBox, "GCNameBox")
        Me.GCNameBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GCNameBox.Name = "GCNameBox"
        '
        'GCLogo
        '
        Me.GCLogo.BackColor = System.Drawing.Color.Transparent
        Me.GCLogo.Image = Global.CoolCore.My.Resources.Resources.Nvidia_Logo_wine
        resources.ApplyResources(Me.GCLogo, "GCLogo")
        Me.GCLogo.Name = "GCLogo"
        Me.GCLogo.TabStop = False
        '
        'GCClockBox
        '
        resources.ApplyResources(Me.GCClockBox, "GCClockBox")
        Me.GCClockBox.Name = "GCClockBox"
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label11.Name = "Label11"
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label15.Name = "Label15"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label14.Name = "Label14"
        '
        'GCTempLabel
        '
        resources.ApplyResources(Me.GCTempLabel, "GCTempLabel")
        Me.GCTempLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.GCTempLabel.Name = "GCTempLabel"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.Color.AntiqueWhite
        Me.Label2.Name = "Label2"
        '
        'TabPage2
        '
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Controls.Add(Me.SystemViewList)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SystemViewList
        '
        Me.SystemViewList.BackColor = System.Drawing.SystemColors.Window
        Me.SystemViewList.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.SystemViewList, "SystemViewList")
        Me.SystemViewList.FullRowSelect = True
        Me.SystemViewList.GridLines = True
        Me.SystemViewList.HideSelection = False
        Me.SystemViewList.LargeImageList = Me.ImageList1
        Me.SystemViewList.MultiSelect = False
        Me.SystemViewList.Name = "SystemViewList"
        Me.SystemViewList.ShowItemToolTips = True
        Me.SystemViewList.SmallImageList = Me.ImageList1
        Me.SystemViewList.StateImageList = Me.ImageList1
        Me.SystemViewList.UseCompatibleStateImageBehavior = False
        Me.SystemViewList.View = System.Windows.Forms.View.Details
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'Form1
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Standard)
        Me.Controls.Add(Me.LblStatusMessage)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Standard.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PicBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.GCLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LogMenuItem As ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataSet1 As DataSet
    Friend WithEvents DataTable1 As DataTable
    Friend WithEvents DataColumn1 As DataColumn
    Friend WithEvents DataColumn2 As DataColumn
    Friend WithEvents LblStatusMessage As Label
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InfoMenuItem As ToolStripMenuItem
    Friend WithEvents LoadArchivedMeasurementsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportCPUInfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FAQToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SupportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents CpuInfoMenu As ToolStripMenuItem
    Friend WithEvents IntelCPUDBToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents AmdCPUDBToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents Standard As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PicBox2 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents BtnToggleMonitor1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents CoreTemp3 As TextBox
    Friend WithEvents CoreTemp2 As TextBox
    Friend WithEvents CoreTemp1 As TextBox
    Friend WithEvents TjMax As Label
    Friend WithEvents Power As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents MaxTemplbl As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents MinTemplbl As Label
    Friend WithEvents Core0 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Core2 As Label
    Friend WithEvents Core3 As Label
    Friend WithEvents CoreTemp As TextBox
    Friend WithEvents LoadBox2 As TextBox
    Friend WithEvents MaxTemp2 As TextBox
    Friend WithEvents LoadBox1 As TextBox
    Friend WithEvents MaxTemp1 As TextBox
    Friend WithEvents MinTemp2 As TextBox
    Friend WithEvents MinTemp1 As TextBox
    Friend WithEvents PowerBox2 As TextBox
    Friend WithEvents LoadBox As TextBox
    Friend WithEvents MaxTemp3 As TextBox
    Friend WithEvents MaxTemp As TextBox
    Friend WithEvents LoadBox3 As TextBox
    Friend WithEvents VBox4 As TextBox
    Friend WithEvents VBox3 As TextBox
    Friend WithEvents VBox2 As TextBox
    Friend WithEvents Vbox1 As TextBox
    Friend WithEvents MinTemp As TextBox
    Friend WithEvents MinTemp3 As TextBox
    Friend WithEvents TJBox As TextBox
    Friend WithEvents PowerBox As TextBox
    Friend WithEvents Lithography As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Threads As Label
    Friend WithEvents AllCores As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TDP As Label
    Friend WithEvents Revision As Label
    Friend WithEvents VID As Label
    Friend WithEvents Frequency As Label
    Friend WithEvents Platform As Label
    Friend WithEvents FanBox As TextBox
    Friend WithEvents TDPBox As TextBox
    Friend WithEvents LithographyBox As TextBox
    Friend WithEvents SockBox As TextBox
    Friend WithEvents CPUIDBox As TextBox
    Friend WithEvents VidBox As TextBox
    Friend WithEvents FrequencyBox2 As TextBox
    Friend WithEvents FrequencyBox As TextBox
    Friend WithEvents ThreadBox As TextBox
    Friend WithEvents CoresBox As TextBox
    Friend WithEvents PlatformBox As TextBox
    Friend WithEvents ModelBox As TextBox
    Friend WithEvents Model As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents SystemViewList As ListView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GCList As ListView
    Friend WithEvents GCNameBox As Label
    Friend WithEvents GCLogo As PictureBox
    Friend WithEvents GCTempLabel As Label
    Friend WithEvents GCTempBox As Label
    Friend WithEvents GCClockBox As Label
    Friend WithEvents Loadlbl As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label15 As Label
End Class
