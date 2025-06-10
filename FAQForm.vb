' FAQForm.vb
Imports System.Drawing
Imports System.Runtime.Versioning
Imports System.Windows.Forms

Public Class FAQForm

    Private Sub FAQForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateFAQsTreeView()
        ApplyCurrentTheme()
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        If TrvFAQ.Nodes.Count > 0 AndAlso TrvFAQ.Nodes(0).Nodes.Count > 0 Then
            TrvFAQ.SelectedNode = TrvFAQ.Nodes(0).Nodes(0)
            If TrvFAQ.SelectedNode.Tag IsNot Nothing Then
                TxtAnswer.Text = TrvFAQ.SelectedNode.Tag.ToString()
            End If
        End If

    End Sub

    Private Sub PopulateFAQsTreeView()

        TrvFAQ.Nodes.Clear()

        Dim generalNode As New TreeNode("Allgemeine Informationen zu CoolCore")
        generalNode.Nodes.Add(CreateFAQNode(
            "Was ist CoolCore?",
            "CoolCore ist ein Systeminformations- und Überwachungstool, das Ihnen detaillierte Einblicke in die Leistung und den Zustand Ihres Computers bietet."))
        generalNode.Nodes.Add(CreateFAQNode(
            "Werden persönliche Daten gesammelt oder geteilt?",
            "Nein, CoolCore ist ein lokales Tool und sammelt oder teilt keine persönlichen Daten. Alle Informationen werden nur auf Ihrem eigenen Computer gespeichert."))
        TrvFAQ.Nodes.Add(generalNode)

        ' Systeminformationen & Updates
        Dim systemInfoNode As New TreeNode("Systeminformationen & Aktualisierungen")
        systemInfoNode.Nodes.Add(CreateFAQNode(
            "Wie aktualisiere ich die angezeigten Systeminformationen?",
            "Die Systeminformationen werden in regelmäßigen Intervallen automatisch aktualisiert. Sie können die Aktualisierungsrate in den 'Optionen' anpassen, sobald diese Funktion verfügbar ist."))
        systemInfoNode.Nodes.Add(CreateFAQNode(
            "Warum werden einige Werte als 'N/A' angezeigt?",
            "Manche Informationen können möglicherweise nicht von Ihrem System abgefragt werden (z.B. wenn der WMI-Dienst nicht läuft, fehlende Sensoren oder Berechtigungsprobleme)."))
        systemInfoNode.Nodes.Add(CreateFAQNode(
            "Kann ich die Systeminformationen exportieren?",
            "Ja, Sie können einen detaillierten Bericht über die Schaltfläche 'Export' als HTML-Datei speichern."))
        systemInfoNode.Nodes.Add(CreateFAQNode(
            "Wo werden die Systeminformationen gespeichert?",
            "CoolCore speichert die Systeminformationen in einer lokalen SQLite-Datenbankdatei namens 'SystemInfo.sqlite', die im Anwendungsverzeichnis liegt."))
        TrvFAQ.Nodes.Add(systemInfoNode)

        ' Temperatur & Sensoren
        Dim tempSensorNode As New TreeNode("Temperaturen & Sensoren")
        tempSensorNode.Nodes.Add(CreateFAQNode(
            "Wie werden CPU-Temperaturen ermittelt?",
            "CoolCore nutzt die OpenHardwareMonitorLib, um CPU-Kerntemperaturen von Hardware-Sensoren auszulesen. Stellen Sie sicher, dass diese Bibliothek korrekt integriert ist."))
        tempSensorNode.Nodes.Add(CreateFAQNode(
            "Was bedeuten die Farbcodes bei den Temperaturen (Grün, Gelb, Rot)?",
            "Grün bedeutet normale Temperatur. Gelb weist auf erhöhte Temperaturen hin, die überwacht werden sollten. Rot signalisiert kritische Temperaturen, die sofortige Aufmerksamkeit erfordern."))
        tempSensorNode.Nodes.Add(CreateFAQNode(
            "Was ist der Unterschied zwischen 'MinTemp', 'MaxTemp' und 'CoreTemp'?",
            "MinTemp' zeigt die niedrigste gemessene Temperatur eines Sensors an, 'MaxTemp' die höchste und 'CoreTemp' die aktuelle Temperatur jedes CPU-Kerns."))
        tempSensorNode.Nodes.Add(CreateFAQNode(
            "Kann CoolCore auch GPU-Temperaturen anzeigen?",
            "Ja, wenn Ihre Hardware und die OpenHardwareMonitorLib dies unterstützen, können GPU-Temperaturen erfasst und angezeigt werden."))
        TrvFAQ.Nodes.Add(tempSensorNode)

        ' Leistung & Systemstatus
        Dim performanceNode As New TreeNode("Leistung & Systemstatus")
        performanceNode.Nodes.Add(CreateFAQNode(
            "Warum ist die CPU-Auslastung manchmal hoch, obwohl keine Anwendung läuft?",
            "Hintergrundprozesse und Systemdienste können eine gewisse CPU-Auslastung verursachen. Überprüfen Sie den Task-Manager für weitere Details."))
        performanceNode.Nodes.Add(CreateFAQNode(
            "Was bedeutet 'Uptime'?",
            "'Uptime' gibt an, wie lange Ihr System seit dem letzten Neustart aktiv ist."))
        performanceNode.Nodes.Add(CreateFAQNode(
            "Wie kann ich das Erscheinungsbild (Theme) der Anwendung ändern?",
            "Sie können das Erscheinungsbild zwischen 'Dunkel' und 'Standard' über das Menü 'Einstellungen' -> 'Optionen' ändern und speichern."))
        performanceNode.Nodes.Add(CreateFAQNode(
            "Wie kann ich die CPU-Stresstest-Funktion nutzen?",
            "Die CPU-Stresstest-Funktion ist derzeit in Entwicklung. Sie wird es Ihnen ermöglichen, die Leistung Ihrer CPU unter Last zu testen und zu überwachen."))
        performanceNode.Nodes.Add(CreateFAQNode(
            "Wie kann ich die Anwendung beenden?",
            "Sie können CoolCore über das Menü 'Datei' -> 'Beenden' oder durch Klicken auf das Schließen-Symbol in der oberen rechten Ecke des Fensters beenden."))
        performanceNode.Nodes.Add(CreateFAQNode(
            "Was bedeutet'Logging'?",
            "'Logging' ermöglicht es CoolCore, die CPU Temperatur im hintergrund aufzuzeichnen (logging) um diese dann später wenn gewünscht als Ereigniss Tabell zu jedem Core auf der CPU anzuzeigen und bei Bedarf zu exportieren."))
        TrvFAQ.Nodes.Add(performanceNode)

        generalNode.Expand()
        systemInfoNode.Expand()
        tempSensorNode.Expand()
        performanceNode.Expand()
    End Sub

    Private Function CreateFAQNode(question As String, answer As String) As TreeNode
        Dim node As New TreeNode(question) With {
            .Tag = answer
        }
        Return node
    End Function

    Private Sub TrvFAQ_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TrvFAQ.AfterSelect
        Dim selectedNode As TreeNode = e.Node
        If selectedNode IsNot Nothing AndAlso selectedNode.Tag IsNot Nothing AndAlso selectedNode.Nodes.Count = 0 Then
            TxtAnswer.Text = selectedNode.Tag.ToString()
        Else
            TxtAnswer.Text = String.Empty
        End If

        ' Bildauswahl anhand des Knotentexts (Frage oder Kategorie)
        Select Case selectedNode.Text
            Case "Was ist CoolCore?"
                PictureBox1.Image = PictureLoad("1")
            Case "Systeminformationen & Aktualisierungen"
                PictureBox1.Image = PictureLoad("2")
            Case "Temperaturen & Sensoren"
                PictureBox1.Image = PictureLoad("3")
            Case "Leistung & Systemstatus"
                PictureBox1.Image = PictureLoad("4")
            Case "CPU-Stresstest"
                PictureBox1.Image = PictureLoad("5")
            Case "Logging"
                PictureBox1.Image = PictureLoad("6")
            Case "Einstellungen"
                PictureBox1.Image = PictureLoad("7")
            Case "Über CoolCore"
                PictureBox1.Image = PictureLoad("8")
            Case Else
                PictureBox1.Image = Nothing
        End Select
    End Sub


    Private Sub ApplyCurrentTheme()
        Dim theme As String = My.Settings.ApplicationTheme
        Select Case theme
            Case "Dark"
                Me.BackColor = Color.FromArgb(45, 45, 48)
                Me.ForeColor = Color.White
                LblTitel.ForeColor = Color.White
                TrvFAQ.BackColor = Color.FromArgb(60, 60, 63)
                TrvFAQ.ForeColor = Color.White
                TxtAnswer.BackColor = Color.FromArgb(70, 70, 73)
                TxtAnswer.ForeColor = Color.White
            Case "Standard"
                Me.BackColor = SystemColors.ControlLightLight
                Me.ForeColor = SystemColors.ControlText
                LblTitel.ForeColor = SystemColors.ControlText
                TrvFAQ.BackColor = SystemColors.Window
                TrvFAQ.ForeColor = SystemColors.WindowText
                TxtAnswer.BackColor = SystemColors.ControlLightLight
                TxtAnswer.ForeColor = SystemColors.WindowText
        End Select
    End Sub

    ' Gibt das gewünschte Bild anhand einer Bildnummer als String zurück.
    Private Function PictureLoad(picKey As String) As Image
        Select Case picKey
            Case "1" : Return My.Resources._1
            Case "2" : Return My.Resources._2
            Case "3" : Return My.Resources._3
            Case "4" : Return My.Resources._4
            Case "5" : Return My.Resources._5
            Case "6" : Return My.Resources._6
            Case "7" : Return My.Resources._7
            Case "8" : Return My.Resources._9
            Case Else : Return Nothing
        End Select
    End Function

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class