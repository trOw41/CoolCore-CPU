Imports System.Windows.Forms
Imports System.Globalization
Imports System.Net
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Security.AccessControl
Imports System.Resources
Public Class Form3
    Public Event StopRequested As EventHandler
    Private frameCounter As Integer = 0
    Private _fanAngle As Single = 0.0F
    Private Const _fanRotationSpeed As Single = 10.0F
    Private _heatEffectOffset As Integer = 0
    Private _pulseDirection As Integer = 1
    Private _currentScale As Single = 2.0F ' Aktueller Skalierungsfaktor für Pulsieren
    Private Const _maxScale As Single = 1.2F ' Maximale Größe (110%)
    Private Const _minScale As Single = 0.8F ' Minimale Größe (90%)
    Private Const _scaleSpeed As Single = 0.01F
    Private _dataFlowOffset As Integer = 0 ' Offset für den Datenfluss-Effekt
    Private Const _dataFlowSpeed As Integer = 2
    Private _cpuImage As Image
    Public Sub New()
        InitializeComponent()
        Me.Text = "CPU Monitoring"
        Me.ControlBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        ProgressBar1.Style = ProgressBarStyle.Blocks

        LblLoadingText.Text = "Monitoring CPU Temperatur:"
        LblLoadingText.AutoSize = True
        If TimeLabel IsNot Nothing Then
            TimeLabel.Text = "Wird geladen.."
            TimeLabel.AutoSize = True
        End If
        If PnlCpuFanAnimation IsNot Nothing Then
            _cpuImage = My.Resources.temp2
            PnlCpuFanAnimation.BackColor = SystemColors.Control
            PnlCpuFanAnimation.BorderStyle = BorderStyle.None
            AddHandler PnlCpuFanAnimation.Paint, AddressOf PnlCpuFanAnimation_Paint
        End If

        If AnimationTimer IsNot Nothing Then
            AnimationTimer.Interval = Settings.MonitorTime
            AddHandler AnimationTimer.Tick, AddressOf AnimationTimer_Tick
        End If

    End Sub

    Private Sub Form3_load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = ColorTranslator.FromHtml("#F0F0F0")
        Me.ForeColor = ColorTranslator.FromHtml("#333333")
        _cpuImage = My.Resources.tools_and_utensils
    End Sub

    Public Sub UpdateElapsedTime(elapsedTime As TimeSpan)
        Dim monitorTime As Double = My.Settings.MonitorTime
        Dim tickerMax As Integer = monitorTime
        Dim ticker As Integer = elapsedTime.TotalSeconds
        ProgressBar1.Maximum = tickerMax

        If Me.InvokeRequired Then
            Me.Invoke(Sub() UpdateElapsedTime(elapsedTime))
        Else
            If TimeLabel IsNot Nothing Then
                TimeLabel.Text = $"Dauer: {elapsedTime:hh\:mm\:ss}"
                'LblLoadingText.Text = $"Monitoring CPU Temperatur noch: {Math.Round(monitorTime - elapsedTime.TotalSeconds)}s"
                Me.Invoke(Sub()
                              ProgressBar1.Value = ticker
                              If ticker > 0 Then
                                  LblLoadingText.Text = "ermittel Sensoren.."
                              End If
                              If ticker > 4 Then
                                  LblLoadingText.Text = "Starte CPU überladung (Stress).."
                              End If
                              If ticker > 9 Then
                                  LblLoadingText.Text = $"ermittel Temperatur Werte bitte warten {Math.Round(monitorTime - elapsedTime.TotalSeconds)}s"
                              End If
                              If monitorTime - ticker < 7 Then
                                  LblLoadingText.Text = "beende Stress Ladung.."
                              End If
                              If monitorTime - ticker < 4 Then
                                  LblLoadingText.Text = "schreibe Werte in die Tabelle.."
                              End If
                              If ticker = monitorTime Then
                                  LblLoadingText.Text = "Beende Stress-Test"
                              End If
                          End Sub)
            End If
        End If
    End Sub



    Private Sub AnimationTimer_Tick(sender As Object, e As EventArgs)
        _currentScale += _pulseDirection * _scaleSpeed
        If _currentScale > _maxScale OrElse _currentScale < _minScale Then
            _pulseDirection *= -1
        End If

        _dataFlowOffset = (_dataFlowOffset + _dataFlowSpeed) Mod 400

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
        g.SmoothingMode = SmoothingMode.HighSpeed
        g.InterpolationMode = InterpolationMode.HighQualityBicubic

        Dim panelWidth As Integer = PnlCpuFanAnimation.Width
        Dim panelHeight As Integer = PnlCpuFanAnimation.Height
        Dim centerX As Integer = panelWidth / 2
        Dim centerY As Integer = panelHeight / 2

        If _cpuImage IsNot Nothing Then

            Dim scaledWidth As Integer = CInt(_cpuImage.Width * _currentScale)
            Dim scaledHeight As Integer = CInt(_cpuImage.Height * _currentScale)
            Dim maxDimension As Integer = Math.Min(panelWidth, panelHeight) * 9 \ 10
            If scaledWidth > maxDimension Then
                scaledWidth = maxDimension
                scaledHeight = CInt(_cpuImage.Height * (scaledWidth / _cpuImage.Width))
            End If
            If scaledHeight > maxDimension Then
                scaledHeight = maxDimension
                scaledWidth = CInt(_cpuImage.Width * (scaledHeight / _cpuImage.Height))
            End If
            Dim drawX As Integer = centerX - (scaledWidth \ 2)
            Dim drawY As Integer = centerY - (scaledHeight \ 2)
            g.DrawImage(_cpuImage, drawX, drawY, scaledWidth, scaledHeight)
        End If

        Using dataPen As New Pen(Color.FromArgb(210, Color.OrangeRed), 3)
            Dim radius As Integer = CInt(Math.Min(panelWidth, panelHeight) * 0.45)

            For i As Integer = 0 To 10
                Dim angle As Double = (i * 45 + _dataFlowOffset) * (Math.PI / 180.0)
                Dim angleEnd As Double = (i * 45 + _dataFlowOffset + 20) * (Math.PI / 180.0)

                Dim startX As Single = centerX + CInt(radius * Math.Cos(angle))
                Dim startY As Single = centerY + CInt(radius * Math.Sin(angle))

                Dim endX As Single = centerX + CInt((radius + 20) * Math.Cos(angleEnd))
                Dim endY As Single = centerY + CInt((radius + 20) * Math.Sin(angleEnd))

                dataPen.Color = Color.FromArgb(Math.Max(50, 200 - (_dataFlowOffset Mod 100)), Color.IndianRed)
                g.DrawLine(dataPen, startX, startY, endX, endY)

                Dim pointRadius As Integer = CInt(radius + (_dataFlowOffset Mod 20) - 10)
                Dim pointX As Single = centerX + CInt(pointRadius * Math.Cos(angle + _dataFlowOffset * 0.05))
                Dim pointY As Single = centerY + CInt(pointRadius * Math.Sin(angle + _dataFlowOffset * 0.05))
                g.FillEllipse(Brushes.GreenYellow, pointX - 2, pointY - 2, 4, 4)
            Next
        End Using

        Using borderPen As New Pen(Color.Gray, 1)
            g.DrawRectangle(borderPen, 0, 0, panelWidth - 1, panelHeight - 1)
        End Using
    End Sub

End Class