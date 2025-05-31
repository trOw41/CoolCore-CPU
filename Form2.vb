Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form2
    Private temperatureData As List(Of CoreTempData) = New List(Of CoreTempData)()
    Private sourceFilePath As String

    Public Sub New(data As List(Of CoreTempData))
        InitializeComponent()
        temperatureData = data
        Me.Text = "CPU Temperature History (Live Data)"
        SetupFormAndChart()
    End Sub

    Public Sub New(filePath As String)
        InitializeComponent()
        sourceFilePath = filePath
        Me.Text = $"CPU Temperature History: {Path.GetFileName(filePath)}"
        SetupFormAndChart()
        LoadDataFromCsv(filePath)
    End Sub

    Private Sub SetupFormAndChart()
        Me.Width = 800
        Me.Height = 600

        If Chart1 IsNot Nothing Then
            InitializeChart()

        End If

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

    Private Sub LoadDataFromCsv(filePath As String)
        temperatureData.Clear()
        Debug.WriteLine($"Attempting to load CSV from: {filePath}")

        If Not File.Exists(filePath) Then
            MessageBox.Show($"Die Datei wurde nicht gefunden: {filePath}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine("File not found.")
            Exit Sub
        End If

        Try
            Using reader As New StreamReader(filePath, Encoding.UTF8)
                Dim headerLine As String = reader.ReadLine()
                If String.IsNullOrEmpty(headerLine) Then
                    Debug.WriteLine("CSV header is empty or null.")
                    Exit Sub
                End If
                Debug.WriteLine($"CSV Header: {headerLine}")

                Dim headers() As String = headerLine.Split(","c).Select(Function(s) s.Trim()).ToArray()
                Dim coreHeaderIndices As New Dictionary(Of String, Integer)()
                For i As Integer = 1 To headers.Length - 1
                    Dim header As String = headers(i)
                    If header.EndsWith(" (°C)", StringComparison.OrdinalIgnoreCase) Then

                        Dim coreName As String = header.Replace(" (°C)", "").Trim()
                        coreHeaderIndices.Add(coreName, i)
                        Debug.WriteLine($"Found core header: {coreName} at index {i}")
                    Else
                        Debug.WriteLine($"Skipping unknown header: {header}")
                    End If
                Next

                If Not coreHeaderIndices.Any() Then
                    Debug.WriteLine("No valid core temperature headers found in CSV. Expected format like 'Core #0 (°C)'.")
                    MessageBox.Show("Keine Temperaturspalten im CSV gefunden. Erwartetes Format ist 'Core #X (°C)'.", "CSV-Formatfehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If


                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    If String.IsNullOrEmpty(line) Then Continue While

                    Debug.WriteLine($"Processing line: {line}")
                    Dim parts() As String = line.Split(","c).Select(Function(s) s.Trim()).ToArray()

                    If parts.Length > 0 Then
                        Dim timestamp As DateTime
                        If DateTime.TryParse(parts(0), timestamp) Then
                            Dim coreTemps As New Dictionary(Of String, Single)()
                            For Each kvp In coreHeaderIndices
                                If parts.Length > kvp.Value Then
                                    Dim tempString As String = parts(kvp.Value)
                                    Dim tempValue As Single

                                    If Single.TryParse(tempString, NumberStyles.Any, CultureInfo.InvariantCulture, tempValue) Then
                                        coreTemps.Add(kvp.Key, tempValue)
                                        Debug.WriteLine($"  Parsed core {kvp.Key}: {tempValue}°C")
                                    ElseIf tempString.Equals("N/A", StringComparison.OrdinalIgnoreCase) Then
                                        Debug.WriteLine($"  Core {kvp.Key} is N/A - skipping this value.")

                                    Else
                                        Debug.WriteLine($"  Failed to parse temperature for core {kvp.Key}: '{parts(kvp.Value)}'")
                                    End If
                                End If
                            Next
                            If coreTemps.Any() Then
                                temperatureData.Add(New CoreTempData() With {
                                    .Timestamp = timestamp,
                                    .CoreTemperatures = coreTemps
                                })
                                'Debug.WriteLine($"Added entry for {timestamp} with {coreTemps.Count} temperatures. Total entries: {temperatureData.Count}")
                            Else
                                Debug.WriteLine($"No valid temperatures found for timestamp: {timestamp}.")
                            End If
                        Else
                            Debug.WriteLine($"Failed to parse timestamp: '{parts(0)}'")
                        End If
                    Else
                        Debug.WriteLine("Line is empty after splitting or malformed.")
                    End If
                End While
            End Using

            Debug.WriteLine($"Finished reading CSV. Total data points collected: {temperatureData.Count}")
            If temperatureData.Any() Then
                LoadChartData()
            Else
                Debug.WriteLine("No data points in temperatureData list after parsing. Chart will be empty.")
                Chart1.Series("Temperatures").Points.Clear()
            End If


        Catch ex As Exception
            MessageBox.Show($"Fehler beim Lesen der CSV-Datei: {ex.Message}{Environment.NewLine}Datei: {filePath}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine($"Exception during CSV loading: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

    Private Sub LoadChartData()
        Debug.WriteLine($"LoadChartData called. Data points available in internal list: {temperatureData.Count}")
        If temperatureData Is Nothing OrElse Not temperatureData.Any() Then
            Chart1.Series("Temperatures").Points.Clear()
            Debug.WriteLine("No data in temperatureData list, chart cleared.")
            Exit Sub
        End If
        Chart1.Series("Temperatures").Points.Clear()
        Dim allTemperatures As New List(Of Single)()
        For Each entry In temperatureData
            For Each kvp In entry.CoreTemperatures
                allTemperatures.Add(kvp.Value)
            Next
        Next
        Debug.WriteLine($"Total individual temperature values gathered for chart: {allTemperatures.Count}")

        If Not allTemperatures.Any() Then
            Debug.WriteLine("No individual temperature values collected. Chart will be empty.")
            Chart1.Series("Temperatures").Points.Clear()
            Exit Sub
        End If
        Dim minOverallTemp As Single = CInt(Math.Floor(allTemperatures.Min()))
        Dim maxOverallTemp As Single = CInt(Math.Ceiling(allTemperatures.Max()))
        Dim binSize As Integer = 2 ' Bins von 2°C
        If (maxOverallTemp - minOverallTemp) < binSize AndAlso (maxOverallTemp - minOverallTemp) > 0 Then binSize = 1 ' Mindestens 1 Grad bei kleiner Spanne
        If (maxOverallTemp - minOverallTemp) = 0 Then maxOverallTemp += 1

        Dim temperatureBins As New SortedDictionary(Of Integer, Integer)()
        For temp As Integer = CInt(minOverallTemp) To CInt(maxOverallTemp + binSize) Step binSize ' +
            temperatureBins.Add(temp, 0)
        Next
        For Each temp In allTemperatures
            Dim binStart As Integer = CInt(Math.Floor(temp / binSize)) * binSize

            If binStart < CInt(minOverallTemp) Then binStart = CInt(minOverallTemp)

            If binStart > CInt(maxOverallTemp) Then binStart = CInt(Math.Floor(maxOverallTemp / binSize)) * binSize

            If temperatureBins.ContainsKey(binStart) Then
                temperatureBins(binStart) += 1
            Else

                temperatureBins.Add(binStart, 1)
            End If
        Next
        For Each kvp In temperatureBins.OrderBy(Function(x) x.Key)
            Dim tempValue As Single = kvp.Key
            Dim count As Integer = kvp.Value
            If count > 0 Then
                Dim dataPoint As DataPoint = New DataPoint()
                dataPoint.SetValueXY($"{tempValue}°C - {tempValue + binSize}°C", count)
                dataPoint.Label = $"{count}"
                dataPoint.Color = GetTemperatureColor(tempValue, minOverallTemp, maxOverallTemp)
                Chart1.Series("Temperatures").Points.Add(dataPoint)
            End If
        Next
        Chart1.ChartAreas("ChartArea1").RecalculateAxesScale()
        If temperatureBins.Any() Then
            Chart1.ChartAreas("ChartArea1").AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Angle = -45
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.IsStaggered = True
            Chart1.Series("Temperatures").XValueType = ChartValueType.String
        End If
        Chart1.Invalidate()
        PanelColorLegend.Invalidate()
        Debug.WriteLine("Chart data loaded and drawn.")
    End Sub

    Private Function GetTemperatureColor(temp As Single, minOverallTemp As Single, maxOverallTemp As Single) As Color
        ' Einfache lineare Farbskala von Blau nach Rot über Grün
        If maxOverallTemp <= minOverallTemp Then Return Color.Gray ' Vermeidet Division durch Null

        Dim normalizedTemp As Single = (temp - minOverallTemp) / (maxOverallTemp - minOverallTemp)
        normalizedTemp = Math.Max(0, Math.Min(1, normalizedTemp)) ' Sicherstellen, dass es zwischen 0 und 1 liegt

        Dim red As Integer = 0
        Dim green As Integer = 0
        Dim blue As Integer = 0

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

        Return Color.FromArgb(red, green, blue)
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

End Class