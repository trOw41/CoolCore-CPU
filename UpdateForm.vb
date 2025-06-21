

Imports System.IO
Imports System.Net

Public Class UpdateForm
    Private WithEvents UpdateClient As WebClient
    Private ReadOnly stopwatch As New Stopwatch()

    Private Async Sub UpdateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0
        ProgressLabel.Text = "Warte auf Download..."
        TimeLabel.Text = "Geschwindigkeit: 0.00 MB/s"
        Dim setupPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CoolCoreSetup.exe")
        Try
            UpdateClient = New WebClient()
            AddHandler UpdateClient.DownloadProgressChanged, AddressOf UpdateClient_DownloadProgressChanged
            stopwatch.Start()
            Await UpdateClient.DownloadFileTaskAsync(New Uri("https://cool-core.de.cool/updates/cool-core/CoolCoreSetup.exe"), setupPath)

            ProgressLabel.Text = "Download abgeschlossen. Starte Setup..."
            ProgressBar1.Value = 100
            TimeLabel.Text = "Geschwindigkeit: - MB/s"
            stopwatch.Stop()
            If File.Exists(setupPath) Then
                MessageBox.Show("Update wird installiert. Anwendung wird beendet...")
                Try
                    Dim psi As New ProcessStartInfo() With {
                        .FileName = setupPath,
                        .UseShellExecute = True,
                        .WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                    }
                    Process.Start(psi)
                Catch ex As Exception
                    MessageBox.Show("Fehler beim Starten des Setups: " & ex.Message)
                End Try
                Application.Exit()
            Else
                MessageBox.Show("Error: File not found!")
            End If

        Catch ex As Exception
            MessageBox.Show("Fehler beim Herunterladen der Datei: " & ex.Message)
        Finally
            If UpdateClient IsNot Nothing Then
                RemoveHandler UpdateClient.DownloadProgressChanged, AddressOf UpdateClient_DownloadProgressChanged
                UpdateClient.Dispose()
                UpdateClient = Nothing
            End If
            Me.Close()
        End Try
        Enabled = True
    End Sub

    Private Sub UpdateClient_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        ProgressBar1.Value = e.ProgressPercentage
        Dim receivedMB = e.BytesReceived / 1024.0 / 1024.0
        Dim totalMB = If(e.TotalBytesToReceive > 0, e.TotalBytesToReceive / 1024.0 / 1024.0, 0)
        ProgressLabel.Text = $"Heruntergeladen: {receivedMB:F2} MB von {totalMB:F2} MB ({e.ProgressPercentage}%)"
        If stopwatch.IsRunning AndAlso stopwatch.Elapsed.TotalSeconds > 0 Then
            Dim downloadSpeedBytesPerSecond = e.BytesReceived / stopwatch.Elapsed.TotalSeconds
            Dim downloadSpeedMBPerSecond = downloadSpeedBytesPerSecond / 1024.0 / 1024.0
            TimeLabel.Text = $"Geschwindigkeit: {downloadSpeedMBPerSecond:F2} MB/s"
        Else
            TimeLabel.Text = "Geschwindigkeit: Berechne..."
        End If
    End Sub

End Class