Imports System.Data.SqlClient
Imports System.Security.Cryptography

Public Class Prescriptions
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mohit\OneDrive\Documents\ClinicDbVb.mdf;Integrated Security=True;Connect Timeout=30")
    Dim cmd As SqlCommand
    Private Sub FillPatients()
        Con.Open()
        Dim sql = "select * from AppointmentTb1"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tb1 As New DataTable()
        adapter.Fill(Tb1)
        PatCb.DataSource = Tb1
        PatCb.DisplayMember = "ApPat"
        PatCb.ValueMember = "ApPat"
        Con.Close()
    End Sub
    Private Sub Populate()
        Con.Open()
        Dim query = "Select * from PrescriptionTb1"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        PrescriptionDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub

    Private Sub Reset()
        PatCb.SelectedIndex = -1
        TreatNameTb.Text = ""
        QtyTb.Text = ""
        MedicineTb.Text = ""
        CostTb.Text = ""
    End Sub
    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Me.Hide()
        Patients.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
        Appointments.Show()
    End Sub

    Private Sub Label8_Click_1(sender As Object, e As EventArgs) Handles Label8.Click
        Me.Hide()
        Treatments.Show()
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If TreatNameTb.Text = "" Or QtyTb.Text = "" Or MedicineTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.Open()
            Dim query = "Insert into PrescriptionTb1 values('" + PatCb.SelectedValue.ToString + "','" + TreatNameTb.Text + "','" + CostTb.Text + "','" + MedicineTb.Text + "'," + QtyTb.Text + ")"
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Prescription Saved Successfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub Prescriptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillPatients()
        Populate()
    End Sub
    Private Sub FetchData()
        Con.Open()
        Dim Query = "Select * from AppointmentTb1 where ApPat='" + PatCb.SelectedValue.ToString + "'"
        Dim cmd As New SqlCommand(Query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            TreatNameTb.Text = reader(2).ToString

        End While
        Con.Close()
        Con.Open()
        Dim Query1 = "Select * from TreatmentTb1 where TrName='" + TreatNameTb.Text + "'"
        Dim cmd1 As New SqlCommand(Query1, Con)
        Dim dt1 As New DataTable
        Dim reader1 As SqlDataReader
        reader1 = cmd1.ExecuteReader()
        While reader1.Read
            CostTb.Text = reader1(2).ToString
        End While
        Con.Close()
    End Sub
    Private Sub FetchCost()

    End Sub
    Private Sub PatCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles PatCb.SelectionChangeCommitted
        FetchData()
        '  FetchCost()
    End Sub

    Private Sub TreatNameTb_TextChanged(sender As Object, e As EventArgs) Handles TreatNameTb.TextChanged

    End Sub

    Private Sub ResetsBtn_Click(sender As Object, e As EventArgs) Handles ResetsBtn.Click
        Reset()
    End Sub
    Dim key = 0
    Private Sub PrescriptionDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles PrescriptionDGV.CellContentClick
        Dim row As DataGridViewRow = PrescriptionDGV.Rows(e.RowIndex)
        PatCb.SelectedValue = row.Cells(1).Value.ToString
        TreatNameTb.Text = row.Cells(2).Value.ToString
        CostTb.Text = row.Cells(3).Value.ToString
        MedicineTb.Text = row.Cells(4).Value.ToString
        QtyTb.Text = row.Cells(5).Value.ToString

        If TreatNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub


    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetsBtn.Click
        Populate()
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Con.Open()
            Dim query = "Delete from PrescriptionTb1 where PId = " & key & ""
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Prescription Deleted Successfully")
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