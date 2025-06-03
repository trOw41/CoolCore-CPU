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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportCPUInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogOnOffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.LoadArchivedMeasurementsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MaxTemplbl = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.MinTemplbl = New System.Windows.Forms.Label()
        Me.Core0 = New System.Windows.Forms.Label()
        Me.Core1 = New System.Windows.Forms.Label()
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
        Me.Threads = New System.Windows.Forms.Label()
        Me.AllCores = New System.Windows.Forms.Label()
        Me.TDP = New System.Windows.Forms.Label()
        Me.Revision = New System.Windows.Forms.Label()
        Me.VID = New System.Windows.Forms.Label()
        Me.Frequency = New System.Windows.Forms.Label()
        Me.Platform = New System.Windows.Forms.Label()
        Me.TDPBox = New System.Windows.Forms.TextBox()
        Me.LitBox = New System.Windows.Forms.TextBox()
        Me.RevisionBox = New System.Windows.Forms.TextBox()
        Me.CPUIDBox = New System.Windows.Forms.TextBox()
        Me.VidBox = New System.Windows.Forms.TextBox()
        Me.FrequencyBox = New System.Windows.Forms.TextBox()
        Me.ThreadBox = New System.Windows.Forms.TextBox()
        Me.CoresBox = New System.Windows.Forms.TextBox()
        Me.PlatformBox = New System.Windows.Forms.TextBox()
        Me.ModelBox = New System.Windows.Forms.TextBox()
        Me.Model = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataSet1 = New System.Data.DataSet()
        Me.DataTable1 = New System.Data.DataTable()
        Me.DataColumn1 = New System.Data.DataColumn()
        Me.DataColumn2 = New System.Data.DataColumn()
        Me.LblStatusMessage = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem, Me.ToolStripSeparator3})
        Me.FileToolStripMenuItem.ForeColor = System.Drawing.Color.CornflowerBlue
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
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.ToolStripSeparator1, Me.ExportCPUInfoToolStripMenuItem})
        Me.OptionsToolStripMenuItem.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        resources.ApplyResources(Me.OptionsToolStripMenuItem, "OptionsToolStripMenuItem")
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._038_system_1
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        resources.ApplyResources(Me.SettingsToolStripMenuItem, "SettingsToolStripMenuItem")
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'ExportCPUInfoToolStripMenuItem
        '
        Me.ExportCPUInfoToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._036_folder
        Me.ExportCPUInfoToolStripMenuItem.Name = "ExportCPUInfoToolStripMenuItem"
        resources.ApplyResources(Me.ExportCPUInfoToolStripMenuItem, "ExportCPUInfoToolStripMenuItem")
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogOnOffToolStripMenuItem, Me.ToolStripSeparator2, Me.LoadArchivedMeasurementsToolStripMenuItem})
        Me.ToolsToolStripMenuItem.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        resources.ApplyResources(Me.ToolsToolStripMenuItem, "ToolsToolStripMenuItem")
        '
        'LogOnOffToolStripMenuItem
        '
        Me.LogOnOffToolStripMenuItem.Image = Global.CoolCore.My.Resources.Resources._034_signature
        Me.LogOnOffToolStripMenuItem.Name = "LogOnOffToolStripMenuItem"
        resources.ApplyResources(Me.LogOnOffToolStripMenuItem, "LogOnOffToolStripMenuItem")
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'LoadArchivedMeasurementsToolStripMenuItem
        '
        resources.ApplyResources(Me.LoadArchivedMeasurementsToolStripMenuItem, "LoadArchivedMeasurementsToolStripMenuItem")
        Me.LoadArchivedMeasurementsToolStripMenuItem.Name = "LoadArchivedMeasurementsToolStripMenuItem"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InfoMenuItem})
        Me.HelpToolStripMenuItem.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'InfoMenuItem
        '
        Me.InfoMenuItem.Image = Global.CoolCore.My.Resources.Resources._021_about
        Me.InfoMenuItem.Name = "InfoMenuItem"
        resources.ApplyResources(Me.InfoMenuItem, "InfoMenuItem")
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Lithography)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Threads)
        Me.Panel1.Controls.Add(Me.AllCores)
        Me.Panel1.Controls.Add(Me.TDP)
        Me.Panel1.Controls.Add(Me.Revision)
        Me.Panel1.Controls.Add(Me.VID)
        Me.Panel1.Controls.Add(Me.Frequency)
        Me.Panel1.Controls.Add(Me.Platform)
        Me.Panel1.Controls.Add(Me.TDPBox)
        Me.Panel1.Controls.Add(Me.LitBox)
        Me.Panel1.Controls.Add(Me.RevisionBox)
        Me.Panel1.Controls.Add(Me.CPUIDBox)
        Me.Panel1.Controls.Add(Me.VidBox)
        Me.Panel1.Controls.Add(Me.FrequencyBox)
        Me.Panel1.Controls.Add(Me.ThreadBox)
        Me.Panel1.Controls.Add(Me.CoresBox)
        Me.Panel1.Controls.Add(Me.PlatformBox)
        Me.Panel1.Controls.Add(Me.ModelBox)
        Me.Panel1.Controls.Add(Me.Model)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.CoreTemp3)
        Me.Panel2.Controls.Add(Me.CoreTemp2)
        Me.Panel2.Controls.Add(Me.CoreTemp1)
        Me.Panel2.Controls.Add(Me.TjMax)
        Me.Panel2.Controls.Add(Me.Power)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.MaxTemplbl)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.MinTemplbl)
        Me.Panel2.Controls.Add(Me.Core0)
        Me.Panel2.Controls.Add(Me.Core1)
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
        resources.ApplyResources(Me.Panel2, "Panel2")
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
        Me.BtnToggleMonitor1.BackgroundImage = Global.CoolCore.My.Resources.Resources._033_monitor_1
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
        Me.CoreTemp3.BackColor = System.Drawing.Color.AliceBlue
        Me.CoreTemp3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoreTemp3, "CoreTemp3")
        Me.CoreTemp3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoreTemp3.Name = "CoreTemp3"
        Me.CoreTemp3.ReadOnly = True
        '
        'CoreTemp2
        '
        Me.CoreTemp2.BackColor = System.Drawing.Color.AliceBlue
        Me.CoreTemp2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoreTemp2, "CoreTemp2")
        Me.CoreTemp2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoreTemp2.Name = "CoreTemp2"
        Me.CoreTemp2.ReadOnly = True
        '
        'CoreTemp1
        '
        Me.CoreTemp1.BackColor = System.Drawing.Color.AliceBlue
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
        resources.ApplyResources(Me.Core0, "Core0")
        Me.Core0.Name = "Core0"
        '
        'Core1
        '
        resources.ApplyResources(Me.Core1, "Core1")
        Me.Core1.Name = "Core1"
        '
        'Core2
        '
        resources.ApplyResources(Me.Core2, "Core2")
        Me.Core2.Name = "Core2"
        '
        'Core3
        '
        resources.ApplyResources(Me.Core3, "Core3")
        Me.Core3.Name = "Core3"
        '
        'CoreTemp
        '
        Me.CoreTemp.BackColor = System.Drawing.Color.AliceBlue
        Me.CoreTemp.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoreTemp, "CoreTemp")
        Me.CoreTemp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoreTemp.Name = "CoreTemp"
        Me.CoreTemp.ReadOnly = True
        '
        'LoadBox2
        '
        Me.LoadBox2.BackColor = System.Drawing.Color.AliceBlue
        Me.LoadBox2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LoadBox2, "LoadBox2")
        Me.LoadBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LoadBox2.Name = "LoadBox2"
        Me.LoadBox2.ReadOnly = True
        '
        'MaxTemp2
        '
        Me.MaxTemp2.BackColor = System.Drawing.Color.AliceBlue
        Me.MaxTemp2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MaxTemp2, "MaxTemp2")
        Me.MaxTemp2.ForeColor = System.Drawing.Color.OrangeRed
        Me.MaxTemp2.Name = "MaxTemp2"
        Me.MaxTemp2.ReadOnly = True
        '
        'LoadBox1
        '
        Me.LoadBox1.BackColor = System.Drawing.Color.AliceBlue
        Me.LoadBox1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LoadBox1, "LoadBox1")
        Me.LoadBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LoadBox1.Name = "LoadBox1"
        Me.LoadBox1.ReadOnly = True
        '
        'MaxTemp1
        '
        Me.MaxTemp1.BackColor = System.Drawing.Color.AliceBlue
        Me.MaxTemp1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MaxTemp1, "MaxTemp1")
        Me.MaxTemp1.ForeColor = System.Drawing.Color.OrangeRed
        Me.MaxTemp1.Name = "MaxTemp1"
        Me.MaxTemp1.ReadOnly = True
        '
        'MinTemp2
        '
        Me.MinTemp2.BackColor = System.Drawing.Color.AliceBlue
        Me.MinTemp2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MinTemp2, "MinTemp2")
        Me.MinTemp2.ForeColor = System.Drawing.Color.DodgerBlue
        Me.MinTemp2.Name = "MinTemp2"
        Me.MinTemp2.ReadOnly = True
        '
        'MinTemp1
        '
        Me.MinTemp1.BackColor = System.Drawing.Color.AliceBlue
        Me.MinTemp1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MinTemp1, "MinTemp1")
        Me.MinTemp1.ForeColor = System.Drawing.Color.DodgerBlue
        Me.MinTemp1.Name = "MinTemp1"
        Me.MinTemp1.ReadOnly = True
        '
        'PowerBox2
        '
        Me.PowerBox2.BackColor = System.Drawing.Color.AliceBlue
        Me.PowerBox2.Cursor = System.Windows.Forms.Cursors.IBeam
        resources.ApplyResources(Me.PowerBox2, "PowerBox2")
        Me.PowerBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PowerBox2.Name = "PowerBox2"
        Me.PowerBox2.ReadOnly = True
        '
        'LoadBox
        '
        Me.LoadBox.BackColor = System.Drawing.Color.AliceBlue
        Me.LoadBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LoadBox, "LoadBox")
        Me.LoadBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LoadBox.HideSelection = False
        Me.LoadBox.Name = "LoadBox"
        Me.LoadBox.ReadOnly = True
        '
        'MaxTemp3
        '
        Me.MaxTemp3.BackColor = System.Drawing.Color.AliceBlue
        Me.MaxTemp3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MaxTemp3, "MaxTemp3")
        Me.MaxTemp3.ForeColor = System.Drawing.Color.OrangeRed
        Me.MaxTemp3.Name = "MaxTemp3"
        Me.MaxTemp3.ReadOnly = True
        '
        'MaxTemp
        '
        Me.MaxTemp.BackColor = System.Drawing.Color.AliceBlue
        Me.MaxTemp.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MaxTemp, "MaxTemp")
        Me.MaxTemp.ForeColor = System.Drawing.Color.OrangeRed
        Me.MaxTemp.Name = "MaxTemp"
        Me.MaxTemp.ReadOnly = True
        '
        'LoadBox3
        '
        Me.LoadBox3.BackColor = System.Drawing.Color.AliceBlue
        Me.LoadBox3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LoadBox3, "LoadBox3")
        Me.LoadBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LoadBox3.Name = "LoadBox3"
        Me.LoadBox3.ReadOnly = True
        '
        'VBox4
        '
        Me.VBox4.BackColor = System.Drawing.Color.AliceBlue
        Me.VBox4.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.VBox4, "VBox4")
        Me.VBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VBox4.Name = "VBox4"
        Me.VBox4.ReadOnly = True
        '
        'VBox3
        '
        Me.VBox3.BackColor = System.Drawing.Color.AliceBlue
        Me.VBox3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.VBox3, "VBox3")
        Me.VBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VBox3.Name = "VBox3"
        Me.VBox3.ReadOnly = True
        '
        'VBox2
        '
        Me.VBox2.BackColor = System.Drawing.Color.AliceBlue
        Me.VBox2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.VBox2, "VBox2")
        Me.VBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VBox2.Name = "VBox2"
        Me.VBox2.ReadOnly = True
        '
        'Vbox1
        '
        Me.Vbox1.BackColor = System.Drawing.Color.AliceBlue
        Me.Vbox1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Vbox1, "Vbox1")
        Me.Vbox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Vbox1.Name = "Vbox1"
        Me.Vbox1.ReadOnly = True
        '
        'MinTemp
        '
        Me.MinTemp.BackColor = System.Drawing.Color.AliceBlue
        Me.MinTemp.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MinTemp, "MinTemp")
        Me.MinTemp.ForeColor = System.Drawing.Color.DodgerBlue
        Me.MinTemp.Name = "MinTemp"
        Me.MinTemp.ReadOnly = True
        '
        'MinTemp3
        '
        Me.MinTemp3.BackColor = System.Drawing.Color.AliceBlue
        Me.MinTemp3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.MinTemp3, "MinTemp3")
        Me.MinTemp3.ForeColor = System.Drawing.Color.DodgerBlue
        Me.MinTemp3.Name = "MinTemp3"
        Me.MinTemp3.ReadOnly = True
        '
        'TJBox
        '
        Me.TJBox.BackColor = System.Drawing.Color.AliceBlue
        Me.TJBox.Cursor = System.Windows.Forms.Cursors.IBeam
        resources.ApplyResources(Me.TJBox, "TJBox")
        Me.TJBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TJBox.Name = "TJBox"
        '
        'PowerBox
        '
        Me.PowerBox.BackColor = System.Drawing.Color.AliceBlue
        Me.PowerBox.Cursor = System.Windows.Forms.Cursors.IBeam
        resources.ApplyResources(Me.PowerBox, "PowerBox")
        Me.PowerBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PowerBox.Name = "PowerBox"
        Me.PowerBox.ReadOnly = True
        '
        'Lithography
        '
        resources.ApplyResources(Me.Lithography, "Lithography")
        Me.Lithography.Name = "Lithography"
        '
        'Label4
        '
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Threads
        '
        resources.ApplyResources(Me.Threads, "Threads")
        Me.Threads.Name = "Threads"
        '
        'AllCores
        '
        Me.AllCores.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.AllCores, "AllCores")
        Me.AllCores.Name = "AllCores"
        '
        'TDP
        '
        resources.ApplyResources(Me.TDP, "TDP")
        Me.TDP.Name = "TDP"
        '
        'Revision
        '
        Me.Revision.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Revision, "Revision")
        Me.Revision.Name = "Revision"
        '
        'VID
        '
        Me.VID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.VID, "VID")
        Me.VID.Name = "VID"
        '
        'Frequency
        '
        Me.Frequency.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Frequency, "Frequency")
        Me.Frequency.Name = "Frequency"
        '
        'Platform
        '
        Me.Platform.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.Platform, "Platform")
        Me.Platform.Name = "Platform"
        '
        'TDPBox
        '
        Me.TDPBox.BackColor = System.Drawing.Color.AliceBlue
        Me.TDPBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.TDPBox, "TDPBox")
        Me.TDPBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TDPBox.Name = "TDPBox"
        Me.TDPBox.ReadOnly = True
        '
        'LitBox
        '
        Me.LitBox.BackColor = System.Drawing.Color.AliceBlue
        Me.LitBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.LitBox, "LitBox")
        Me.LitBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LitBox.Name = "LitBox"
        Me.LitBox.ReadOnly = True
        '
        'RevisionBox
        '
        Me.RevisionBox.BackColor = System.Drawing.Color.AliceBlue
        Me.RevisionBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.RevisionBox, "RevisionBox")
        Me.RevisionBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.RevisionBox.Name = "RevisionBox"
        Me.RevisionBox.ReadOnly = True
        '
        'CPUIDBox
        '
        Me.CPUIDBox.BackColor = System.Drawing.Color.AliceBlue
        Me.CPUIDBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CPUIDBox, "CPUIDBox")
        Me.CPUIDBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CPUIDBox.Name = "CPUIDBox"
        Me.CPUIDBox.ReadOnly = True
        '
        'VidBox
        '
        Me.VidBox.BackColor = System.Drawing.Color.AliceBlue
        Me.VidBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.VidBox, "VidBox")
        Me.VidBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.VidBox.Name = "VidBox"
        Me.VidBox.ReadOnly = True
        '
        'FrequencyBox
        '
        Me.FrequencyBox.BackColor = System.Drawing.Color.AliceBlue
        Me.FrequencyBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.FrequencyBox, "FrequencyBox")
        Me.FrequencyBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FrequencyBox.Name = "FrequencyBox"
        Me.FrequencyBox.ReadOnly = True
        '
        'ThreadBox
        '
        Me.ThreadBox.BackColor = System.Drawing.Color.AliceBlue
        Me.ThreadBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.ThreadBox, "ThreadBox")
        Me.ThreadBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ThreadBox.Name = "ThreadBox"
        Me.ThreadBox.ReadOnly = True
        '
        'CoresBox
        '
        Me.CoresBox.BackColor = System.Drawing.Color.AliceBlue
        Me.CoresBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CoresBox, "CoresBox")
        Me.CoresBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CoresBox.Name = "CoresBox"
        Me.CoresBox.ReadOnly = True
        '
        'PlatformBox
        '
        Me.PlatformBox.BackColor = System.Drawing.Color.AliceBlue
        Me.PlatformBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.PlatformBox, "PlatformBox")
        Me.PlatformBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PlatformBox.Name = "PlatformBox"
        Me.PlatformBox.ReadOnly = True
        '
        'ModelBox
        '
        Me.ModelBox.BackColor = System.Drawing.Color.AliceBlue
        Me.ModelBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.ModelBox, "ModelBox")
        Me.ModelBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ModelBox.Name = "ModelBox"
        Me.ModelBox.ReadOnly = True
        '
        'Model
        '
        resources.ApplyResources(Me.Model, "Model")
        Me.Model.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Model.Name = "Model"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Label2.Name = "Label2"
        Me.Label2.UseCompatibleTextRendering = True
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
        'Form1
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.Controls.Add(Me.LblStatusMessage)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LogOnOffToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label2 As Label
    Friend WithEvents ModelBox As TextBox
    Friend WithEvents Model As Label
    Friend WithEvents Platform As Label
    Friend WithEvents FrequencyBox As TextBox
    Friend WithEvents PlatformBox As TextBox
    Friend WithEvents VID As Label
    Friend WithEvents Frequency As Label
    Friend WithEvents VidBox As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Revision As Label
    Friend WithEvents RevisionBox As TextBox
    Friend WithEvents CPUIDBox As TextBox
    Friend WithEvents TDPBox As TextBox
    Friend WithEvents LitBox As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Lithography As Label
    Friend WithEvents TDP As Label
    Friend WithEvents CoreTemp2 As TextBox
    Friend WithEvents CoreTemp1 As TextBox
    Friend WithEvents CoreTemp As TextBox
    Friend WithEvents TJBox As TextBox
    Friend WithEvents CoreTemp3 As TextBox
    Friend WithEvents Core0 As Label
    Friend WithEvents Core1 As Label
    Friend WithEvents Core2 As Label
    Friend WithEvents Core3 As Label
    Friend WithEvents MinTemp As TextBox
    Friend WithEvents TjMax As Label
    Friend WithEvents Power As Label
    Friend WithEvents MinTemplbl As Label
    Friend WithEvents MaxTemplbl As Label
    Friend WithEvents MaxTemp2 As TextBox
    Friend WithEvents MaxTemp1 As TextBox
    Friend WithEvents MinTemp2 As TextBox
    Friend WithEvents MinTemp1 As TextBox
    Friend WithEvents MaxTemp As TextBox
    Friend WithEvents LoadBox2 As TextBox
    Friend WithEvents LoadBox1 As TextBox
    Friend WithEvents LoadBox As TextBox
    Friend WithEvents MaxTemp3 As TextBox
    Friend WithEvents MinTemp3 As TextBox
    Friend WithEvents PowerBox As TextBox
    Friend WithEvents LoadBox3 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PowerBox2 As TextBox
    Friend WithEvents DataSet1 As DataSet
    Friend WithEvents DataTable1 As DataTable
    Friend WithEvents DataColumn1 As DataColumn
    Friend WithEvents DataColumn2 As DataColumn
    Friend WithEvents LblStatusMessage As Label
    Friend WithEvents Threads As Label
    Friend WithEvents AllCores As Label
    Friend WithEvents ThreadBox As TextBox
    Friend WithEvents CoresBox As TextBox
    Friend WithEvents ExportCPUInfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InfoMenuItem As ToolStripMenuItem
    Friend WithEvents BtnToggleMonitor1 As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents LoadArchivedMeasurementsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label6 As Label
    Friend WithEvents VBox4 As TextBox
    Friend WithEvents VBox3 As TextBox
    Friend WithEvents VBox2 As TextBox
    Friend WithEvents Vbox1 As TextBox
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
End Class
