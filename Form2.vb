Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form2
    Private temperatureData As New List(Of CoreTempData)()
    Private sourceFilePath As String
    Public Property PanelColorLegend As Color

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

    End Sub

    Private Sub InitializeChart()
        If Chart1 Is Nothing Then
            Chart1 = New Chart()
            Me.Controls.Add(Chart1)
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

        chartArea.AxisY.Title = "Temperatur (°C)"
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
            .Text = Me.Text,
            .Font = New Font("Bahnschrift", 11, FontStyle.Regular)
        }
        Chart1.Titles.Add(mainTitle)
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

            'Debug.WriteLine($"Finished reading CSV. Total data points collected: {temperatureData.Count}")
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
            Chart1.Series.Clear()
            Debug.WriteLine("No data in temperatureData list, chart cleared.")
            Exit Sub
        End If
        Chart1.Series.Clear()
        Dim coreColors As New List(Of Color) From {
        Color.Blue, Color.Red, Color.Green, Color.Purple,
        Color.Orange, Color.DarkCyan, Color.HotPink, Color.AliceBlue,
        Color.DarkSlateGray, Color.Indigo, Color.Azure, Color.DarkGreen
    }
        Dim colorIndex As Integer = 0
        Dim allCoreNames As New SortedSet(Of String)()
        For Each entry In temperatureData
            For Each kvp In entry.CoreTemperatures
                allCoreNames.Add(kvp.Key)
            Next
        Next

        If Not allCoreNames.Any() Then
            Debug.WriteLine("No core names found in temperatureData. Chart will be empty.")
            Exit Sub
        End If

        For Each coreName In allCoreNames
            Dim series As New Series(coreName) With {
            .ChartType = SeriesChartType.Stock.FastPoint,
            .XValueType = ChartValueType.DateTime,
            .YValueType = ChartValueType.Single,
            .BorderWidth = 2,
            .Color = coreColors(colorIndex Mod coreColors.Count)
        }
            Chart1.Series.Add(series)
            colorIndex += 1
        Next

        ' Daten zu den Serien hinzufügen
        Dim minChartTemp As Single = Single.MaxValue
        Dim maxChartTemp As Single = Single.MinValue
        Dim firstTimestamp As DateTime = DateTime.MaxValue
        Dim lastTimestamp As DateTime = DateTime.MinValue

        For Each entry In temperatureData.OrderBy(Function(e) e.Timestamp) ' Daten nach Zeitstempel sortieren
            If entry.Timestamp < firstTimestamp Then firstTimestamp = entry.Timestamp
            If entry.Timestamp > lastTimestamp Then lastTimestamp = entry.Timestamp

            For Each kvp In entry.CoreTemperatures
                Dim coreName As String = kvp.Key
                Dim tempValue As Single = kvp.Value

                If Chart1.Series.Any(Function(s) s.Name.Equals(coreName, StringComparison.OrdinalIgnoreCase)) Then
                    Dim dataPoint As New DataPoint()
                    dataPoint.SetValueXY(entry.Timestamp, tempValue)

                    dataPoint.ToolTip = $"Zeit: {entry.Timestamp:HH:mm:ss.fff}{Environment.NewLine}" &
                                   $"Kern: {coreName}{Environment.NewLine}" &
                                   $"Temp: {tempValue:F1}°C"

                    Chart1.Series(coreName).Points.Add(dataPoint)

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
        Debug.WriteLine("Chart data loaded and invalidated.")
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
            End Using
        Next
    End Sub


    Private Sub Form2_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Chart1?.Invalidate()
    End Sub

End Class