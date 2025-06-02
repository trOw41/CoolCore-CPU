Imports System.Windows.Forms

Public Class Form3
    Public Event StopRequested As EventHandler ' Ereignis, das Form1 informiert

    Public Sub New()
        InitializeComponent()
        Me.Text = "CPU Monitoring"
        Me.ControlBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        ProgressBar1.Style = ProgressBarStyle.Marquee
        LblLoadingText.Text = "Monitoring CPU Temperatur:"
        LblLoadingText.AutoSize = True
        LblLoadingText.Location = New Point((Me.ClientSize.Width - LblLoadingText.Width) / 2, 20)
        AddHandler BtnStopMonitoring.Click, AddressOf BtnStopMonitoring_Click

        If TimeLabel IsNot Nothing Then
            TimeLabel.Text = "Wird geladen.."
            TimeLabel.AutoSize = True
            'TimeLabel.Location = New Point((Me.ClientSize.Width - TimeLabel.Width) / 2, LblLoadingText.Bottom + 10)
        End If
    End Sub
    Public Sub UpdateElapsedTime(elapsedTime As TimeSpan)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() UpdateElapsedTime(elapsedTime))
        Else
            If TimeLabel IsNot Nothing Then
                TimeLabel.Text = $"Verstrichene Zeit: {elapsedTime:hh\:mm\:ss}"

                'TimeLabel.Location = New Point((Me.ClientSize.Width - TimeLabel.Width) / 2, LblLoadingText.Bottom + 10)
            End If
        End If
    End Sub

    Private Sub BtnStopMonitoring_Click(sender As Object, e As EventArgs) Handles BtnStopMonitoring.Click
        RaiseEvent StopRequested(Me, EventArgs.Empty)
    End Sub

End Class