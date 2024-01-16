Public Class Login
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If UnameTb.Text = "" Or passwordTb.Text = "" Then
            MsgBox("Enter UserName and Password")
        ElseIf UnameTb.Text = "Uday" And passwordTb.Text = "123" Then
            Me.Hide()
            Dim Obj = New Appointments
            Obj.Show()
        Else
            MsgBox("Wrong UserName and Password")
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        UnameTb.Text = ""
        passwordTb.Text = ""
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class