Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO

Public Class Patients
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mohit\OneDrive\Documents\ClinicDbVb.mdf;Integrated Security=True;Connect Timeout=30")
    Dim cmd As SqlCommand
    Private Sub Populate()
        Con.Close()
        Dim query = "Select * from PatientTb1"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        PatientsDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub Reset()
        PatNameTb.Text = ""
        PatAddTb.Text = ""
        PatPhoneTb.Text = ""
        AllergieTb.Text = ""
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If PatNameTb.Text = "" Or PatAddTb.Text = "" Or PatPhoneTb.Text = "" Or AllergieTb.Text = "" Or GenCb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Con.Open()
            Dim query = "Insert into PatientTb1 values('" + PatNameTb.Text + "','" + PatPhoneTb.Text + "','" + PatAddTb.Text + "','" + DOBDate.Value.Date.ToString + "','" + GenCb.SelectedItem.ToString + "','" + AllergieTb.Text + "')"
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Patient Saved Successfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub Patients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Populate()
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
        Appointments.Show()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Hide()
        Prescriptions.Show()
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Con.Open()
            Dim query = "Delete from PatientTb1 where PatId = " & key & ""
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Patient Deleted Successfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub
    Dim key = 0
    Private Sub PatientsDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles PatientsDGV.CellMouseClick
        Dim row As DataGridViewRow = PatientsDGV.Rows(e.RowIndex)
        PatNameTb.Text = row.Cells(1).Value.ToString
        PatPhoneTb.Text = row.Cells(2).Value.ToString
        PatAddTb.Text = row.Cells(3).Value.ToString
        DOBDate.Text = row.Cells(4).Value.ToString
        GenCb.SelectedItem = row.Cells(5).Value.ToString
        AllergieTb.Text = row.Cells(6).Value.ToString
        If PatNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        Dim obj = New Treatments
        obj.Show()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Me.Hide()
        Dim obj = New Login
        obj.Show()
    End Sub
End Class