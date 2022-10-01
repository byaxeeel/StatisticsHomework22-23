Imports System.Runtime.CompilerServices

Public Class Form1
    Dim rnd As New Random
    Dim pari As Boolean = True


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For i = 0 To 10
            PictureBox1.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
            Button1.Text = 10 - i
            Threading.Thread.Sleep(1000)
            Refresh()
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If pari Then
            PictureBox1.Location = New Point(400, 162)
            pari = False

        Else
            PictureBox1.Location = New Point(39, 162)
            pari = True

        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub button2Hover(sender As Object, e As EventArgs) Handles Button2.MouseHover
        Button1.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
        Button2.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
        Button3.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
    End Sub
End Class
