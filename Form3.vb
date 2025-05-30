Imports System.Windows.Forms

Public Class Form3
    Public Event StopRequested As EventHandler ' Ereignis, das Form1 informiert

    Public Sub New()
        InitializeComponent()
        Me.Text = "CPU Monitoring"
        Me.ControlBox = False ' Keine Schließen-Box
        Me.FormBorderStyle = FormBorderStyle.FixedDialog ' Nicht in der Größe änderbar
        Me.StartPosition = FormStartPosition.CenterScreen ' In der Mitte des Bildschirms

        ProgressBar1.Style = ProgressBarStyle.Marquee ' Ladekreis-Animation

        ' Positionieren Sie die Controls im Designer oder hier programmatisch
        LblLoadingText.Text = "Bitte warten, Monitoring erfasst die CPU Temperaturen..."
        LblLoadingText.AutoSize = True
        LblLoadingText.Location = New Point((Me.ClientSize.Width - LblLoadingText.Width) / 2, 20)

        ProgressBar1.Location = New Point((Me.ClientSize.Width - ProgressBar1.Width) / 2, LblLoadingText.Bottom + 10)
        ProgressBar1.Size = New Size(200, 20) ' Beispielgröße

        BtnStopMonitoring.Text = "Stop Monitoring"
        BtnStopMonitoring.Location = New Point((Me.ClientSize.Width - BtnStopMonitoring.Width) / 2, ProgressBar1.Bottom + 20)
        AddHandler BtnStopMonitoring.Click, AddressOf BtnStopMonitoring_Click
    End Sub

    Private Sub BtnStopMonitoring_Click(sender As Object, e As EventArgs) Handles BtnStopMonitoring.Click
        RaiseEvent StopRequested(Me, EventArgs.Empty) ' Informiert das Parent-Formular
        ' Das Formular wird von Form1 geschlossen, nicht von hier aus
    End Sub
End Class