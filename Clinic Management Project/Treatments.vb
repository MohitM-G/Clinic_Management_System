Imports System.Data.SqlClient
Public Class Treatments
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mohit\OneDrive\Documents\ClinicDbVb.mdf;Integrated Security=True;Connect Timeout=30")
    Dim cmd As SqlCommand
    Private Sub Populate()
        Con.Close()
        Dim query = "Select * from TreatmentTb1"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        TreatmentDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub Reset()
        TrNameTb.Text = ""
        CostTb.Text = ""
        DescriptionTb.Text = ""
        key = 0
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If TrNameTb.Text = "" Or CostTb.Text = "" Or DescriptionTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query = "Insert into TreatmentTb1 values('" + TrNameTb.Text + "'," + CostTb.Text + ",'" + DescriptionTb.Text + "')"
                Dim cmd As New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Treatment Saved Successfully")
                Con.Close()
                Populate()
                Reset()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Hide()
        Prescriptions.Show()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Me.Hide()
        Patients.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
        Appointments.Show()
    End Sub

    Private Sub Treatments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Populate()
    End Sub
    Dim key = 0
    Private Sub TreatmentDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles TreatmentDGV.CellContentClick
        Dim row As DataGridViewRow = TreatmentDGV.Rows(e.RowIndex)
        TrNameTb.Text = row.Cells(1).Value.ToString
        CostTb.Text = row.Cells(2).Value.ToString
        DescriptionTb.Text = row.Cells(3).Value.ToString
        If TrNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Con.Open()
            Dim query = "Delete from TreatmentTb1 where TrId = " & key & ""
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Treatment Deleted Successfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        Dim obj = New Login
        obj.Show()
    End Sub
End Class