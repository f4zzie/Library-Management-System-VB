' ============================================
' Add Members Form - Library Management System
' Allows adding new library members
' ============================================
Imports System.IO

Public Class AddMembers
    ' Form Load Event
    Private Sub AddMembers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Generate next Member ID
        txtMemberID.Text = GenerateNextMemberID()
        txtMemberID.ReadOnly = True

        ' Set date picker to today
        dtpRegistration.Value = DateTime.Now
    End Sub

    ' Generate next Member ID
    Private Function GenerateNextMemberID() As String
        Try
            If File.Exists("members.txt") Then
                Dim lines() As String = File.ReadAllLines("members.txt")
                If lines.Length > 0 Then
                    Dim lastLine As String = lines(lines.Length - 1)
                    Dim parts() As String = lastLine.Split("|"c)
                    If parts.Length > 0 Then
                        Dim lastID As Integer = Integer.Parse(parts(0).Replace("MEM", ""))
                        Return "MEM" & (lastID + 1).ToString("D4")
                    End If
                End If
            End If
            Return "MEM0001"
        Catch ex As Exception
            Return "MEM0001"
        End Try
    End Function

    ' Add Member Button
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ' Input Validation
            If Not ValidateInputs() Then
                Return
            End If

            ' Create member record (now includes Student ID, Gender, and Address)
            Dim memberRecord As String = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                txtMemberID.Text.Trim(),
                txtName.Text.Trim(),
                txtStudentID.Text.Trim(),
                cmbGender.Text.Trim(),
                txtAddress.Text.Trim().Replace(vbCrLf, " ").Replace(vbLf, " "),
                txtEmail.Text.Trim(),
                txtPhone.Text.Trim(),
                cmbMemberType.Text.Trim(),
                dtpRegistration.Value.ToString("yyyy-MM-dd"))

            ' Save to file
            File.AppendAllText("members.txt", memberRecord & Environment.NewLine)

            ' Success message
            MessageBox.Show("Member added successfully!" & vbNewLine & "Member ID: " & txtMemberID.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear form
            ClearForm()

        Catch ex As Exception
            ' Error handling
            MessageBox.Show("Error adding member: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Validate all inputs
    Private Function ValidateInputs() As Boolean
        ' Validate Name
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("Please enter the member name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        ' Validate Student ID
        If String.IsNullOrWhiteSpace(txtStudentID.Text) Then
            MessageBox.Show("Please enter the student/registration ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtStudentID.Focus()
            Return False
        End If

        ' Validate Gender
        If String.IsNullOrWhiteSpace(cmbGender.Text) Then
            MessageBox.Show("Please select a gender", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbGender.Focus()
            Return False
        End If

        ' Validate Address
        If String.IsNullOrWhiteSpace(txtAddress.Text) Then
            MessageBox.Show("Please enter the address", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAddress.Focus()
            Return False
        End If

        ' Validate Email
        If String.IsNullOrWhiteSpace(txtEmail.Text) Then
            MessageBox.Show("Please enter the email address", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmail.Focus()
            Return False
        End If

        ' Validate Email format
        If Not IsValidEmail(txtEmail.Text) Then
            MessageBox.Show("Please enter a valid email address", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmail.Focus()
            Return False
        End If

        ' Validate Phone
        If String.IsNullOrWhiteSpace(txtPhone.Text) Then
            MessageBox.Show("Please enter the phone number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPhone.Focus()
            Return False
        End If

        ' Validate Phone format (numeric only)
        If Not IsNumeric(txtPhone.Text) Then
            MessageBox.Show("Phone number must contain only numbers", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPhone.Focus()
            Return False
        End If

        ' Validate Member Type
        If String.IsNullOrWhiteSpace(cmbMemberType.Text) Then
            MessageBox.Show("Please select a member type", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbMemberType.Focus()
            Return False
        End If

        Return True
    End Function

    ' Validate Email format
    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr = New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    ' Clear Form
    Private Sub ClearForm()
        txtName.Clear()
        txtStudentID.Clear()
        cmbGender.SelectedIndex = -1
        txtAddress.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        cmbMemberType.SelectedIndex = -1
        dtpRegistration.Value = DateTime.Now
        txtMemberID.Text = GenerateNextMemberID()
        txtName.Focus()
    End Sub

    ' Clear Button
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    ' Close Button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Phone TextBox - KeyPress (Only allow numbers)
    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Please enter numbers only", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class
