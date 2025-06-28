Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form2
    Private temperatureData As New List(Of CoreTempData)()

    Private sourceFilePath As String
    Public Property PanelColorLegend As Color

    ' This class is required for the CoreTempData type.
    ' Assuming CoreTempData looks something like this:
    Public Class CoreTempData
        Public Property Timestamp As Date
        Public Property CoreTemperatures As Dictionary(Of String, Single)
        ' Add any other properties your actual CoreTempData class might have
    End Class

    Public Sub New(data As List(Of CoreTempData))
        InitializeComponent()
        temperatureData = data
        Text = "CPU Temperature History (Live Data)"
        SetupFormAndChart()
    End Sub

    Public Sub New(filePath As String)
        InitializeComponent()
        sourceFilePath = filePath
        Text = $"CPU Temperature History: {Path.GetFileName(filePath)}"
        SetupFormAndChart()
        LoadDataFromCsv(filePath)
    End Sub

    Private Sub SetupFormAndChart()
        Width = 800
        Height = 600
        If Chart1 IsNot Nothing Then
            InitializeChart()
        End If
    End Sub

    Private Sub InitializeChart()
        If Chart1 Is Nothing Then
            Chart1 = New Chart()
            Controls.Add(Chart1)
            Chart1.Dock = DockStyle.Fill
        End If
        Chart1.Series.Clear()
        Chart1.ChartAreas.Clear()
        Chart1.Legends.Clear()

        Dim chartArea As New ChartArea("MainChartArea")
        Chart1.ChartAreas.Add(chartArea)
        chartArea.AxisX.Title = "Zeit"
        chartArea.AxisX.IntervalType = DateTimeIntervalType.Seconds
        chartArea.AxisX.LabelStyle.Format = "HH:mm:ss"
        chartArea.AxisX.MajorGrid.LineColor = Color.LightGray
        chartArea.AxisX.MinorGrid.LineColor = Color.LightGray
        chartArea.AxisX.MinorGrid.Enabled = True
        chartArea.AxisX.LabelStyle.Angle = -45
        chartArea.AxisX.LabelStyle.IsStaggered = True

        chartArea.AxisY.Title = "Temperatur (°C), Volt"
        chartArea.AxisY.MajorGrid.LineColor = Color.LightGray
        chartArea.AxisY.MinorGrid.LineColor = Color.LightGray
        chartArea.AxisY.MinorGrid.Enabled = True
        chartArea.AxisY.Minimum = 0
        chartArea.AxisY.Maximum = 100

        Dim legend As New Legend("CoreLegend")
        Chart1.Legends.Add(legend)
        legend.Docking = Docking.Bottom
        legend.Alignment = StringAlignment.Center
        legend.IsTextAutoFit = True
        legend.LegendStyle = LegendStyle.Row
        legend.MaximumAutoSize = 80

        ' Chart Titel
        Chart1.Titles.Clear()
        Dim mainTitle As New Title With {
            .Name = "MainTitle",
            .Text = Text,
            .Font = New Font("Bahnschrift", 11, FontStyle.Regular)
        }
        Chart1.Titles.Add(mainTitle)
    End Sub

    Private Sub LoadDataFromCsv(filePath As String)
        temperatureData.Clear()
        If Not File.Exists(filePath) Then
            MessageBox.Show($"Die Datei wurde nicht gefunden: {filePath}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Using reader As New StreamReader(filePath, Encoding.UTF8)
                Dim headerLine As String = reader.ReadLine()
                If String.IsNullOrEmpty(headerLine) Then
                    ' If the file is empty, clear the chart and exit
                    Chart1.Series.Clear() ' <<<--- IMPORTANT FIX
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
                    End If
                Next

                If Not coreHeaderIndices.Any() Then
                    Debug.WriteLine("No valid core temperature headers found in CSV. Expected format like 'Core #0 (°C)'.")
                    MessageBox.Show("Keine Temperaturspalten im CSV gefunden. Erwartetes Format ist 'Core #X (°C)'.", "CSV-Formatfehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Chart1.Series.Clear() ' <<<--- IMPORTANT FIX
                    Exit Sub
                End If

                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    If String.IsNullOrEmpty(line) Then Continue While
                    Dim parts() As String = line.Split(","c).Select(Function(s) s.Trim()).ToArray()

                    If parts.Length > 0 Then
                        Dim timestamp As Date
                        If Date.TryParse(parts(0), timestamp) Then
                            Dim coreTemps As New Dictionary(Of String, Single)()

                            For Each kvp In coreHeaderIndices
                                If parts.Length > kvp.Value Then
                                    Dim tempString As String = parts(kvp.Value)
                                    Dim tempValue As Single
                                    If Single.TryParse(tempString, NumberStyles.Any, CultureInfo.InvariantCulture, tempValue) Then
                                        coreTemps.Add(kvp.Key, tempValue)
                                    ElseIf tempString.Equals("N/A", StringComparison.OrdinalIgnoreCase) Then
                                        ' Ignore N/A values, do not add to dictionary
                                    Else
                                        ' Optional: Handle other parsing errors or log them
                                    End If
                                End If
                            Next

                            If coreTemps.Any() Then
                                temperatureData.Add(New CoreTempData() With {
                                    .Timestamp = timestamp,
                                    .CoreTemperatures = coreTemps
                                })
                            End If
                        End If
                    End If
                End While
            End Using

            If temperatureData.Any() Then
                LoadChartData()
            Else
                ' If no data was loaded, just clear the chart
                Chart1.Series.Clear() ' <<<--- IMPORTANT FIX: Clear ALL series, not a specific named one
            End If

        Catch ex As Exception
            MessageBox.Show($"Fehler beim Lesen der CSV-Datei: {ex.Message}{Environment.NewLine}Datei: {filePath}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadChartData()
        If temperatureData Is Nothing OrElse Not temperatureData.Any() Then
            Chart1.Series.Clear()
            Exit Sub
        End If

        Chart1.Series.Clear() ' Clear existing series before adding new ones

        Dim coreColors As New List(Of Color) From {
            Color.Blue, Color.Red, Color.Green, Color.Purple,
            Color.OrangeRed, Color.DarkCyan, Color.HotPink,
            Color.DarkGray, Color.Indigo, Color.Crimson, Color.Aquamarine, Color.YellowGreen,
            Color.SlateBlue, Color.Tomato, Color.OliveDrab, Color.Salmon, Color.CornflowerBlue, Color.MediumVioletRed, Color.LightSeaGreen,
            Color.SteelBlue, Color.MediumOrchid, Color.DarkKhaki, Color.LightCoral, Color.PaleGreen, Color.MediumSlateBlue, Color.SeaGreen,
            Color.Goldenrod, Color.LightSkyBlue, Color.MediumSpringGreen, Color.DarkSalmon, Color.LightPink, Color.Thistle,
            Color.LimeGreen, Color.CadetBlue, Color.SandyBrown, Color.Plum,
            Color.LightSteelBlue, Color.DarkOliveGreen, Color.Coral, Color.MediumTurquoise, Color.Wheat, Color.LightGoldenrodYellow
        }
        Dim colorIndex As Integer = 0

        Dim allCoreNames As New SortedSet(Of String)()
        For Each entry In temperatureData
            For Each kvp In entry.CoreTemperatures
                allCoreNames.Add(kvp.Key)
            Next
        Next

        Try
            If Not allCoreNames.Any() Then
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Ermitteln der Kernenamen: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        For Each coreName In allCoreNames
            Dim series As New Series(coreName) With {
                .ChartType = SeriesChartType.Line, ' <<<--- CORRECTED CHART TYPE: Use SeriesChartType.Line or .Column
                .XValueType = ChartValueType.DateTime,
                .YValueType = ChartValueType.Auto,
                .BorderWidth = 3, ' Reduced for line chart
                .IsValueShownAsLabel = False, ' Usually better for line charts unless specific points need labels
                .LabelFormat = "{0:F1}°C",
                .ToolTip = "Zeit: #VALX{HH:mm:ss.fff}" & Environment.NewLine &
                             "Kern: #SERIESNAME" & Environment.NewLine &
                             "Temp: #VALY{F1}°C",
                .ShadowColor = Color.FromArgb(128, Color.Black),
                .ShadowOffset = 1,
                .Color = coreColors(colorIndex Mod coreColors.Count)
            }
            Chart1.Series.Add(series)
            colorIndex += 1
        Next

        Dim minChartTemp As Single = Single.MaxValue
        Dim maxChartTemp As Single = Single.MinValue
        Dim firstTimestamp As Date = Date.MaxValue
        Dim lastTimestamp As Date = Date.MinValue

        For Each entry In temperatureData.OrderBy(Function(e) e.Timestamp)
            If entry.Timestamp < firstTimestamp Then firstTimestamp = entry.Timestamp
            If entry.Timestamp > lastTimestamp Then lastTimestamp = entry.Timestamp

            For Each kvp In entry.CoreTemperatures
                Dim coreName As String = kvp.Key
                Dim tempValue As Single = kvp.Value
                If Chart1.Series.Any(Function(s) s.Name.Equals(coreName, StringComparison.OrdinalIgnoreCase)) Then
                    ' Using AddXY directly is more concise
                    Chart1.Series(coreName).Points.AddXY(entry.Timestamp, tempValue)
                    If tempValue < minChartTemp Then minChartTemp = tempValue
                    If tempValue > maxChartTemp Then maxChartTemp = tempValue
                End If
            Next
        Next

        If temperatureData.Any() Then
            Chart1.ChartAreas("MainChartArea").AxisY.Minimum = CInt(Math.Floor(minChartTemp - 5))
            Chart1.ChartAreas("MainChartArea").AxisY.Maximum = CInt(Math.Ceiling(maxChartTemp + 5))

            Chart1.ChartAreas("MainChartArea").AxisX.Minimum = firstTimestamp.ToOADate()
            Chart1.ChartAreas("MainChartArea").AxisX.Maximum = lastTimestamp.ToOADate()

            Dim totalDuration As TimeSpan = lastTimestamp - firstTimestamp
            If totalDuration.TotalSeconds < 60 Then
                Chart1.ChartAreas("MainChartArea").AxisX.IntervalType = DateTimeIntervalType.Seconds
                Chart1.ChartAreas("MainChartArea").AxisX.Interval = 5
            ElseIf totalDuration.TotalMinutes < 30 Then
                Chart1.ChartAreas("MainChartArea").AxisX.IntervalType = DateTimeIntervalType.Minutes
                Chart1.ChartAreas("MainChartArea").AxisX.Interval = 1
            Else
                Chart1.ChartAreas("MainChartArea").AxisX.IntervalType = DateTimeIntervalType.Minutes
                Chart1.ChartAreas("MainChartArea").AxisX.Interval = 5
            End If
        End If
        Chart1.Invalidate()
        'Debug: Debug.WriteLine("Chart data loaded and invalidated.")
    End Sub

    Private Function GetTemperatureColor(temp As Single, minOverallTemp As Single, maxOverallTemp As Single) As Color
        If maxOverallTemp <= minOverallTemp Then Return Color.Gray
        Dim normalizedTemp As Single = (temp - minOverallTemp) / (maxOverallTemp - minOverallTemp)
        normalizedTemp = Math.Max(0, Math.Min(1, normalizedTemp))

        Dim red As Integer
        Dim green As Integer
        Dim blue As Integer

        If normalizedTemp < 0.5 Then
            blue = CInt(255 * (1 - normalizedTemp * 2))
            green = CInt(255 * (normalizedTemp * 2))
            red = 0
        Else
            red = CInt(255 * (normalizedTemp - 0.5) * 2)
            green = CInt(255 * (1 - (normalizedTemp - 0.5) * 2))
            blue = 0
        End If
        Return Color.FromArgb(red, green, blue)
    End Function

    Private Sub PanelColorLegend_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        Dim numSteps As Integer = 100
        Dim minOverallTemp As Single = 0
        Dim maxOverallTemp As Single = 100

        If temperatureData IsNot Nothing AndAlso temperatureData.Any() Then
            Dim allTemps = temperatureData.SelectMany(Function(x) x.CoreTemperatures.Values).ToList()
            If allTemps.Any() Then
                minOverallTemp = CInt(Math.Floor(allTemps.Min()))
                maxOverallTemp = CInt(Math.Ceiling(allTemps.Max()))
                If maxOverallTemp = minOverallTemp Then maxOverallTemp = minOverallTemp + 10
            End If
        End If

        For i As Integer = 0 To numSteps - 1
            Dim temp As Single = minOverallTemp + (maxOverallTemp - minOverallTemp) * (i / (numSteps - 1))
            Dim color As Color = GetTemperatureColor(temp, minOverallTemp, maxOverallTemp)
            Using brush As New SolidBrush(color)
                ' You need to actually draw something here, e.g., a rectangle:
                Dim rectHeight As Integer = e.ClipRectangle.Height / numSteps
                Dim rect As New Rectangle(0, i * rectHeight, e.ClipRectangle.Width, rectHeight)
                g.FillRectangle(brush, rect)
            End Using
        Next
    End Sub

    Private Sub Form2_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Chart1?.Invalidate()
    End Sub

End Class
