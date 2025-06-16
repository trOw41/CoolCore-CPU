Imports System.IO

Public Class WelcomeForm
    Private infoText As String = "WilkommenInfo.txt"


    Private Sub WelcomeForm_load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim text As String = File.ReadAllText(infoText)
        TextBox1.Text = text
    End Sub
End Class