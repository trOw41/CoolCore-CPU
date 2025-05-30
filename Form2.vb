Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Drawing
Imports System.Linq
Imports System.Collections.Generic
Imports System.Windows.Forms ' Wichtig für Panel und Chart controls

Public Class Form2
    Private temperatureData As List(Of CoreTempData) ' Beachten Sie, dass CoreTempData jetzt global verfügbar ist,
    ' da wir es als Public Structure in Form1.vb hinzugefügt haben.

    Public Sub New(data As List(Of CoreTempData))
        InitializeComponent()
        temperatureData = data
        Me.Text = "CPU Temperature History"
        Me.Width = 800
        Me.Height = 600

        ' Diagramm initialisieren (sicherstellen, dass Chart1 im Designer platziert wurde)
        If Chart1 IsNot Nothing Then
            InitializeChart()
            LoadChartData()
        End If

        ' Farblegende zeichnen
        AddHandler PanelColorLegend.Paint, AddressOf PanelColorLegend_Paint
    End Sub

    Private Sub InitializeChart()
        Chart1.Series.Clear()
        Chart1.Titles.Clear()
        Chart1.ChartAreas.Clear()

        Chart1.Titles.Add("Core Temperature Distribution")
        Chart1.ChartAreas.Add("ChartArea1") ' Standard ChartArea
        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Temperature (°C)"
        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Frequency / Count"

        ' Automatische Skalierung für Y-Achse
        Chart1.ChartAreas("ChartArea1").AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount
        Chart1.ChartAreas("ChartArea1").AxisY.LabelAutoFitStyle = LabelAutoFitStyles.StaggeredLabels Or LabelAutoFitStyles.LabelsAngleStep90

        Dim series As New Series("Temperatures")
        series.ChartType = SeriesChartType.Bar
        series.IsValueShownAsLabel = True ' Werte auf den Balken anzeigen
        series.Font = New Font("Arial", 8, FontStyle.Bold) ' Kleinere Schrift für Labels
        Chart1.Series.Add(series)

        ' Verbesserte Tooltips
        Chart1.Series("Temperatures").ToolTip = "Temp: #VALX{F1}°C, Count: #VALY"
    End Sub

    Private Sub LoadChartData()
        If temperatureData Is Nothing OrElse Not temperatureData.Any() Then
            Exit Sub
        End If

        ' Alle gemessenen Temperaturen sammeln und abflachen (von allen Kernen)
        Dim allTemperatures As New List(Of Single)()
        For Each entry In temperatureData
            For Each kvp In entry.CoreTemperatures
                allTemperatures.Add(kvp.Value)
            Next
        Next

        If Not allTemperatures.Any() Then
            Exit Sub
        End If

        ' Histogramm-Daten erstellen
        Dim minOverallTemp As Integer = CInt(Math.Floor(allTemperatures.Min()))
        Dim maxOverallTemp As Integer = CInt(Math.Ceiling(allTemperatures.Max()))
        Dim binSize As Integer = 2 ' Bins von 2°C
        If (maxOverallTemp - minOverallTemp) < binSize AndAlso (maxOverallTemp - minOverallTemp) > 0 Then binSize = 1 ' Mindestens 1 Grad bei kleiner Spanne
        If (maxOverallTemp - minOverallTemp) = 0 Then maxOverallTemp += 1 ' Falls nur ein Wert gemessen wird, um Division durch Null zu vermeiden

        Dim temperatureBins As New SortedDictionary(Of Integer, Integer)() ' Key: Bin-Start, Value: Count

        ' Bins initialisieren
        For temp As Integer = minOverallTemp To maxOverallTemp + binSize Step binSize ' +binSize, um den letzten Bin sicher abzudecken
            temperatureBins.Add(temp, 0)
        Next

        ' Temperaturen den Bins zuordnen
        For Each temp In allTemperatures
            Dim binStart As Integer = CInt(Math.Floor(temp / binSize)) * binSize
            ' Sicherstellen, dass binStart innerhalb der vorbereiteten Bins liegt oder angepasst wird
            If binStart < minOverallTemp Then binStart = minOverallTemp
            If binStart > maxOverallTemp Then binStart = CInt(Math.Floor(maxOverallTemp / binSize)) * binSize

            If temperatureBins.ContainsKey(binStart) Then
                temperatureBins(binStart) += 1
            Else
                ' Dies sollte bei korrekter Initialisierung der Bins nicht passieren, aber zur Sicherheit
                temperatureBins.Add(binStart, 1)
            End If
        Next

        ' Daten zum Diagramm hinzufügen und einfärben
        Chart1.Series("Temperatures").Points.Clear()
        For Each kvp In temperatureBins.OrderBy(Function(x) x.Key) ' Nach Temperatur sortieren
            Dim tempValue As Single = kvp.Key
            Dim count As Integer = kvp.Value

            ' Nur Bins mit Werten oder relevanten Bereichen anzeigen
            If count > 0 OrElse (tempValue >= minOverallTemp AndAlso tempValue <= maxOverallTemp) Then
                Dim dataPoint As DataPoint = New DataPoint()
                dataPoint.SetValueXY($"{tempValue}°C - {tempValue + binSize}°C", count) ' X-Achse als Bereich
                dataPoint.Label = $"{count}"
                dataPoint.Color = GetTemperatureColor(tempValue, minOverallTemp, maxOverallTemp)
                Chart1.Series("Temperatures").Points.Add(dataPoint)
            End If
        Next

        ' Achsen anpassen
        Chart1.ChartAreas("ChartArea1").RecalculateAxesScale()
        If temperatureBins.Any() Then
            Chart1.ChartAreas("ChartArea1").AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Angle = -45 ' Winkel für X-Achsen-Labels
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.IsStaggered = True ' Gestaffelte Labels

            ' Umbenennung der X-Achse von numerisch zu Kategorien
            Chart1.Series("Temperatures").XValueType = ChartValueType.String
        End If
    End Sub

    Private Function GetTemperatureColor(temp As Single, minOverallTemp As Single, maxOverallTemp As Single) As Color
        ' Einfache lineare Farbskala von Blau nach Rot über Grün
        If maxOverallTemp <= minOverallTemp Then Return Color.Gray ' Vermeidet Division durch Null

        Dim normalizedTemp As Single = (temp - minOverallTemp) / (maxOverallTemp - minOverallTemp)
        normalizedTemp = Math.Max(0, Math.Min(1, normalizedTemp)) ' Sicherstellen, dass es zwischen 0 und 1 liegt

        Dim blue As Integer
        Dim red As Integer
        Dim green As Integer
        If normalizedTemp < 0.5 Then
            ' Von Blau zu Grün (0.0 bis 0.5)
            blue = CInt(255 * (1 - normalizedTemp * 2)) ' Von 255 (Blau) nach 0
            green = CInt(255 * (normalizedTemp * 2))   ' Von 0 nach 255 (Grün)
            red = 0
        Else
            ' Von Grün zu Rot (0.5 bis 1.0)
            red = CInt(255 * (normalizedTemp - 0.5) * 2)   ' Von 0 nach 255 (Rot)
            green = CInt(255 * (1 - (normalizedTemp - 0.5) * 2)) ' Von 255 (Grün) nach 0
            blue = 0
        End If

        Return Color.FromArgb(red, green, 0)
    End Function

    Private Sub PanelColorLegend_Paint(sender As Object, e As PaintEventArgs) Handles PanelColorLegend.Paint
        Dim g As Graphics = e.Graphics
        Dim panelWidth As Integer = PanelColorLegend.Width
        Dim panelHeight As Integer = PanelColorLegend.Height
        Dim numSteps As Integer = 100 ' Für einen glatten Farbverlauf

        Dim minOverallTemp As Single = 0 ' Standardwerte
        Dim maxOverallTemp As Single = 100

        ' Wenn tatsächlich Daten vorhanden sind, die Min/Max-Temperatur verwenden
        If temperatureData IsNot Nothing AndAlso temperatureData.Any() Then
            Dim allTemps = temperatureData.SelectMany(Function(x) x.CoreTemperatures.Values).ToList()
            If allTemps.Any() Then
                minOverallTemp = CInt(Math.Floor(allTemps.Min()))
                maxOverallTemp = CInt(Math.Ceiling(allTemps.Max()))
                If maxOverallTemp = minOverallTemp Then maxOverallTemp = minOverallTemp + 10 ' Vermeidet Division durch Null/problematische Skala
            End If
        End If

        For i As Integer = 0 To numSteps - 1
            Dim temp As Single = minOverallTemp + (maxOverallTemp - minOverallTemp) * (i / (numSteps - 1))
            Dim color As Color = GetTemperatureColor(temp, minOverallTemp, maxOverallTemp)
            Using brush As New SolidBrush(color)
                ' Zeichne einen kleinen Streifen für jeden Schritt des Farbverlaufs
                g.FillRectangle(brush,
                                CInt(i * panelWidth / numSteps), _            ' <-- Hier umwandeln zu Integer
                                0,
                                CInt((i + 1) * panelWidth / numSteps + 1), _  ' <-- Hier umwandeln zu Integer
                                panelHeight)
            End Using
        Next

        ' Temperaturwerte an der Legende hinzufügen
        Using font As New Font("Arial", 8, FontStyle.Bold)
            Using brush As New SolidBrush(Color.Black)
                Dim p As New Pen(Color.Black, 1)

                ' Min-Temperatur
                g.DrawString($"{minOverallTemp}°C", font, brush, 0, 0)
                g.DrawLine(p, 0, panelHeight - 1, 0, 0)

                ' Max-Temperatur
                Dim textSize As SizeF = g.MeasureString($"{maxOverallTemp}°C", font)
                g.DrawString($"{maxOverallTemp}°C", font, brush, panelWidth - textSize.Width, 0)
                g.DrawLine(p, panelWidth - 1, panelHeight - 1, panelWidth - 1, 0)

                ' Eine oder zwei Zwischenpunkte hinzufügen (optional)
                Dim midTemp1 As Single = minOverallTemp + (maxOverallTemp - minOverallTemp) / 3
                Dim midTemp2 As Single = minOverallTemp + 2 * (maxOverallTemp - minOverallTemp) / 3

                Dim midX1 As Single = panelWidth / 3
                Dim midX2 As Single = 2 * panelWidth / 3

                Dim midTextSize1 As SizeF = g.MeasureString($"{midTemp1:F0}°C", font)
                Dim midTextSize2 As SizeF = g.MeasureString($"{midTemp2:F0}°C", font)

                g.DrawString($"{midTemp1:F0}°C", font, brush, midX1 - midTextSize1.Width / 2, 0)
                g.DrawString($"{midTemp2:F0}°C", font, brush, midX2 - midTextSize2.Width / 2, 0)
            End Using
        End Using
    End Sub

    ' Optional: Wenn sich die Größe des Formulars ändert, die Legende neu zeichnen
    Private Sub Form2_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        PanelColorLegend.Invalidate()
    End Sub

End Class