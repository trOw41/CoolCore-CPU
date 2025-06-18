Imports System.IO

Public Class WelcomeForm
    Private infoText As String = Resources.WilkommenInfo
    Private toolTips As ToolTip

    Private Sub WelcomeForm_load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If infoText IsNot Nothing Then
                TextBox1.Text = infoText
            End If
        Catch ex As Exception
            MessageBox.Show($"Error: {infoText} wurde nicht gefunden")
        End Try
#Const ToolTip = off
    End Sub

End Class