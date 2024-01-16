Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class Appointments
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mohit\OneDrive\Documents\ClinicDbVb.mdf;Integrated Security=True;Connect Timeout=30")
    Dim cmd As SqlCommand
    Private Sub FillPatients()
        Con.Open()
        Dim sql = "select * from PatientTb1"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tb1 As New DataTable()
        adapter.Fill(Tb1)
        PatCb.DataSource = Tb1
        PatCb.DisplayMember = "PatName"
        PatCb.ValueMember = "PatName"
        Con.Close()
    End Sub
    Private Sub FillTreatment()
        Con.Open()
        Dim sql = "select * from TreatmentTb1"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tb1 As New DataTable()
        adapter.Fill(Tb1)
        TreatCb.DataSource = Tb1
        TreatCb.DisplayMember = "TrName"
        TreatCb.ValueMember = "TrName"
        Con.Close()
    End Sub

    Private Sub Appointments_Load(Sender As Object, e As EventArgs) Handles MyBase.Load
        FillPatients()
        FillTreatment()
        Populate()
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
        Patients.Show()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        Treatments.Show()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Hide()
        Prescriptions.Show()
    End Sub
    Private Sub Populate()
        Con.Close()
        Dim query = "Select * from AppointmentTb1"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        AppointmentDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub Reset()
        PatCb.SelectedIndex = -1
        TreatCb.SelectedIndex = -1
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If TreatCb.SelectedIndex = -1 Or PatCb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Con.Open()
            Dim query = "Insert into AppointmentTb1 values('" + PatCb.SelectedValue.ToString + "','" + TreatCb.SelectedValue.ToString + "','" + ApDate.Text + "','" + ApTime.Text + "')"
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Appointment Saved Successfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Con.Open()
            Dim query = "Delete from AppointmentTb1 where ApId = " & key & ""
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Appointment Deleted Successfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub
    Dim key = 0
    Private Sub AppointmentDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles AppointmentDGV.CellContentClick
        Dim row As DataGridViewRow = AppointmentDGV.Rows(e.RowIndex)
        PatCb.SelectedValue = row.Cells(1).Value.ToString
        TreatCb.SelectedValue = row.Cells(2).Value.ToString
        ApDate.Text = row.Cells(3).Value.ToString
        ApTime.Text = row.Cells(4).Value.ToString

        If PatCb.SelectedIndex = -1 Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Me.Hide()
        Dim obj = New Login
        obj.Show()
    End Sub
End Class