Imports System.Security.Principal
Imports System.Windows.Forms
Imports System.Diagnostics

Module Module1

    ''' <summary>
    ''' Der Haupteinstiegspunkt für die Anwendung.
    ''' </summary>
    <STAThread()> ' Wichtig für Windows Forms-Anwendungen
    Public Sub Main()
        If Not IsAdministrator() Then
            ' Das Programm hat keine Administratorrechte. Versuche, es neu zu starten.
            Dim startInfo As New ProcessStartInfo()
            startInfo.UseShellExecute = True
            startInfo.WorkingDirectory = Environment.CurrentDirectory
            startInfo.FileName = Application.ExecutablePath ' Pfad zur aktuellen ausführbaren Datei
            startInfo.Verb = "runas" ' Fordert die UAC-Eingabeaufforderung (Als Administrator ausführen) an

            Try
                Process.Start(startInfo)
            Catch ex As Exception
                ' Fehlerbehandlung, falls der Neustart fehlschlägt oder vom Benutzer abgebrochen wird.
                MessageBox.Show("Das Programm benötigt Administratorrechte, um ausgeführt zu werden. Bitte starten Sie es manuell als Administrator.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' Beende die aktuelle Instanz der Anwendung, da sie nicht mit Admin-Rechten läuft.
            Application.Exit()
        Else
            ' Wenn das Programm Administratorrechte hat, starte Form1 ganz normal.
            MessageBox.Show("Is Admin")
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New Form1())
        End If
    End Sub

    ''' <summary>
    ''' Überprüft, ob die aktuelle Anwendung mit Administratorrechten ausgeführt wird.
    ''' </summary>
    ''' <returns>True, wenn die Anwendung Administratorrechte hat; ansonsten False.</returns>
    Private Function IsAdministrator() As Boolean
        Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
        Dim principal As New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

End Module