Imports System.Drawing.Drawing2D
Imports CoolCore.My
Public Class Form3
    Public Event StopRequested As EventHandler
    Private frameCounter As Integer = 0
    Private _fanAngle As Single = 0.0F
    Private Const _fanRotationSpeed As Single = 5.0F
    Private _heatEffectOffset As Integer = 0
    Private _pulseDirection As Integer = 1
    Private _currentScale As Single = 1.0F
    Private Const _maxScale As Single = 1.2F
    Private Const _minScale As Single = 0.8F
    Private Const _scaleSpeed As Single = 0.01F

    Private _cpuImage As Image
    Private _heatWaveOffset As Integer = 0
    Private Const _heatWaveSpeed As Integer = 5
    Private Const _maxHeatWaveAlpha As Integer = 180
    Private Const heatWaveRadiusStart As Integer = 20

    Public Sub New()
        InitializeComponent()
        ControlBox = False
        FormBorderStyle = FormBorderStyle.FixedDialog
        StartPosition = FormStartPosition.CenterParent
        ProgressBar1.Style = ProgressBarStyle.Blocks

        Text = "CPU Temperatur Messung:"
        If TimeLabel IsNot Nothing Then
            TimeLabel.Text = "Wird geladen.."
            TimeLabel.AutoSize = False
        End If
        If PnlCpuFanAnimation IsNot Nothing Then
            _cpuImage = My.Resources.fan2
            PnlCpuFanAnimation.Size = New Size(200, 200)
            PnlCpuFanAnimation.BackColor = SystemColors.Control
            PnlCpuFanAnimation.BorderStyle = BorderStyle.None
            AddHandler PnlCpuFanAnimation.Paint, AddressOf PnlCpuFanAnimation_Paint
        End If

        If AnimationTimer IsNot Nothing Then
            AnimationTimer.Interval = Settings().MonitorTime
            AddHandler AnimationTimer.Tick, AddressOf AnimationTimer_Tick
        End If

    End Sub

    Private Sub Form3_load(sender As Object, e As EventArgs) Handles MyBase.Load
        BackColor = ColorTranslator.FromHtml("#F0F0F0")
        ForeColor = ColorTranslator.FromHtml("#333333")
        _cpuImage = My.Resources.fan2
    End Sub

    Public Sub UpdateElapsedTime(elapsedTime As TimeSpan)
        Dim monitorTime As Double = My.Settings().MonitorTime
        Dim tickerMax As Integer = CInt(monitorTime)
        Dim ticker As Integer = CInt(elapsedTime.TotalSeconds)

        If tickerMax > 0 Then
            ProgressBar1.Maximum = tickerMax
            ProgressBar1.Value = Math.Min(ticker, tickerMax)
        End If

        If InvokeRequired Then
            Invoke(Sub() UpdateElapsedTime(elapsedTime))
        Else
            If TimeLabel IsNot Nothing Then
                TimeLabel.Text = $"Dauer: {elapsedTime:hh\:mm\:ss}"
                Text = $"Monitoring CPU Temperatur noch: {Math.Round(monitorTime - elapsedTime.TotalSeconds)}s"
                lblResults.Text = $"CPU Temperatur: COre1:{Form1.CoreTemp0.Text} -> Core2:{Form1.CoreTemp1.Text} -> Core3:{Form1.CoreTemp2.Text} -> Core4:{Form1.CoreTemp3.Text}"
                lblResults?.Invalidate()
                Form1.CoreTemp0?.Invalidate()
                lblStatus.Text = $"{Settings().Ops * monitorTime / 4} / Millionen -> Operationen pro Thread / sekunde"
            End If
        End If
    End Sub

    Private Sub AnimationTimer_Tick(sender As Object, e As EventArgs)

        _currentScale += _pulseDirection * _scaleSpeed
        If _currentScale > _maxScale OrElse _currentScale < _minScale Then
            _pulseDirection *= -1
        End If


        _fanAngle += _fanRotationSpeed
        If _fanAngle >= 360.0F Then
            _fanAngle -= 360.0F
        End If

        _heatWaveOffset = (_heatWaveOffset + _heatWaveSpeed) Mod 200

        PnlCpuFanAnimation?.Invalidate()
    End Sub

    Public Sub StartAnimation()
        If AnimationTimer IsNot Nothing Then
            AnimationTimer.Enabled = True
        End If
    End Sub

    Public Sub StopAnimation()
        If AnimationTimer IsNot Nothing Then
            AnimationTimer.Enabled = False
            PnlCpuFanAnimation?.Invalidate()
        End If
    End Sub

    Private Sub PnlCpuFanAnimation_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.InterpolationMode = InterpolationMode.HighQualityBicubic

        Dim panelWidth As Integer = PnlCpuFanAnimation.Width
        Dim panelHeight As Integer = PnlCpuFanAnimation.Height
        Dim centerX As Integer = panelWidth / 2
        Dim centerY As Integer = panelHeight / 2

        Dim cpuTemperature As Integer = 0
        If Form1.CoreTemp0 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(Form1.CoreTemp0.Text) Then

            Integer.TryParse(Form1.CoreTemp0.Text.Replace("°C", "").Trim(), cpuTemperature)
        End If

        If cpuTemperature < 0 Then cpuTemperature = 0

        If _cpuImage IsNot Nothing Then
            Dim scaledWidth As Integer = CInt(_cpuImage.Width * _currentScale)
            Dim scaledHeight As Integer = CInt(_cpuImage.Height * _currentScale)

            Dim maxDimension As Integer = Math.Min(panelWidth, panelHeight) * 9 \ 10
            If scaledWidth > maxDimension Then
                scaledWidth = maxDimension
                scaledHeight = CInt(_cpuImage.Height * (CSng(scaledWidth) / _cpuImage.Width))
            End If
            If scaledHeight > maxDimension Then
                scaledHeight = maxDimension
                scaledWidth = CInt(_cpuImage.Width * (CSng(scaledHeight) / _cpuImage.Height))
            End If

            Dim drawX As Integer = centerX - (scaledWidth \ 2)
            Dim drawY As Integer = centerY - (scaledHeight \ 2)

            g.DrawImage(_cpuImage, drawX, drawY, scaledWidth, scaledHeight)

            Dim temperatureAlpha As Integer = CInt(Math.Min(255, cpuTemperature * 2.55))
            Using heatBrush As New SolidBrush(Color.FromArgb(temperatureAlpha, Color.OrangeRed))
                g.FillEllipse(heatBrush, drawX, drawY, scaledWidth, scaledHeight)
            End Using
        End If

        Dim heatWaveRadiusEnd As Integer = Math.Max(panelWidth, panelHeight) / 2 - 10

        Dim numberOfWaves As Integer = CInt(Math.Min(10, 2 + cpuTemperature / 15))
        Dim waveThickness As Integer = 2
        Dim waveColorStart As Color = Color.FromArgb(100, Color.YellowGreen)
        Dim waveColorEnd As Color = Color.FromArgb(0, Color.Red)

        For i As Integer = 0 To numberOfWaves - 1

            Dim angleOffset As Single = (i * (360.0F / numberOfWaves))

            For j As Integer = 0 To 1
                Dim currentWavePos As Single = (_heatWaveOffset + (j * 100)) Mod 200
                Dim currentAlpha As Integer = CInt(_maxHeatWaveAlpha * (1.0F - CSng(Math.Abs(currentWavePos - 100)) / 100.0F))
                currentAlpha = Math.Max(0, currentAlpha)
                Dim blendFactor As Single = CSng(currentWavePos) / 200.0F
                Dim waveColor As Color = InterpolateColors(waveColorStart, waveColorEnd, blendFactor)
                waveColor = Color.FromArgb(currentAlpha, waveColor.R, waveColor.G, waveColor.B)

                Using heatWavePen As New Pen(waveColor, waveThickness)
                    heatWavePen.StartCap = LineCap.Round
                    heatWavePen.EndCap = LineCap.Round

                    Dim segmentLength As Integer = 30
                    For k As Integer = 0 To 2 '
                        Dim currentRadius As Single = heatWaveRadiusStart + currentWavePos + (k * 20)
                        If currentRadius < heatWaveRadiusEnd Then
                            Dim startAngle As Single = angleOffset + (k * 5)
                            Dim endAngle As Single = startAngle + 15

                            g.DrawArc(heatWavePen,
                                      centerX - currentRadius,
                                      centerY - currentRadius,
                                      currentRadius * 2,
                                      currentRadius * 2,
                                      startAngle,
                                      endAngle - startAngle)
                        End If
                    Next
                End Using
            Next
        Next

        Using fanBrush As New SolidBrush(Color.FromArgb(180, Color.LightBlue))
            For i As Integer = 0 To 5
                g.TranslateTransform(centerX, centerY)
                g.RotateTransform(_fanAngle + (i * 60))

                g.FillPolygon(fanBrush, New Point() {New Point(-10, -50), New Point(10, -50), New Point(20, -10), New Point(-20, -10)})
                g.ResetTransform()
            Next
        End Using

        Using borderPen As New Pen(Color.DimGray, 1)
            g.DrawRectangle(borderPen, 0, 0, panelWidth - 1, panelHeight - 1)
        End Using
    End Sub

    ' Hilfsfunktion zur Interpolation von Farben
    Private Function InterpolateColors(color1 As Color, color2 As Color, factor As Single) As Color
        ' Safeguard: Ensure factor is clamped between 0 and 1
        Dim clampedFactor As Single = Math.Max(0.0F, Math.Min(1.0F, factor))

        ' Berechnungen als Single (Fließkommazahl) durchführen
        Dim alpha As Single = CSng(color1.A) + (CSng(color2.A) - CSng(color1.A)) * clampedFactor
        Dim red As Single = CSng(color1.R) + (CSng(color2.R) - CSng(color1.R)) * clampedFactor
        Dim green As Single = CSng(color1.G) + (CSng(color2.G) - CSng(color1.G)) * clampedFactor
        Dim blue As Single = CSng(color1.B) + (CSng(color2.B) - CSng(color1.B)) * clampedFactor

        ' Ergebnisse in den Bereich 0-255 klemmen und dann in Integer umwandeln
        Dim a As Integer = CInt(Math.Max(0, Math.Min(255, Math.Round(alpha))))
        Dim r As Integer = CInt(Math.Max(0, Math.Min(255, Math.Round(red))))
        Dim g As Integer = CInt(Math.Max(0, Math.Min(255, Math.Round(green))))
        Dim b As Integer = CInt(Math.Max(0, Math.Min(255, Math.Round(blue))))

        Return Color.FromArgb(a, r, g, b)
    End Function

End Class