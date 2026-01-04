' ============================================
' View Members Form - Library Management System
' Displays all members with search functionality
' ============================================
Imports System.IO

Public Class ViewMembers
    ' Form Load Event
    Private Sub ViewMembers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup DataGridView
        SetupDataGridView()

        ' Load members
        LoadMembers()
    End Sub

    ' Setup DataGridView
    Private Sub SetupDataGridView()
        ' Clear columns
        dgvMembers.Columns.Clear()

        ' Add columns
        dgvMembers.Columns.Add("MemberID", "Member ID")
        dgvMembers.Columns.Add("Name", "Full Name")
        dgvMembers.Columns.Add("Email", "Email")
        dgvMembers.Columns.Add("Phone", "Phone")
        dgvMembers.Columns.Add("Type", "Member Type")
        dgvMembers.Columns.Add("Date", "Registration Date")

        ' Set column widths
        dgvMembers.Columns(0).Width = 100
        dgvMembers.Columns(1).Width = 200
        dgvMembers.Columns(2).Width = 200
        dgvMembers.Columns(3).Width = 120
        dgvMembers.Columns(4).Width = 120
        dgvMembers.Columns(5).Width = 130

        ' Set properties
        dgvMembers.ReadOnly = True
        dgvMembers.AllowUserToAddRows = False
        dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    ' Load members from file
    Private Sub LoadMembers(Optional searchText As String = "")
        Try
            ' Clear existing rows
            dgvMembers.Rows.Clear()

            ' Check if file exists
            If Not File.Exists("members.txt") Then
                lblTotal.Text = "Total Members: 0"
                Return
            End If

            ' Read all lines
            Dim lines() As String = File.ReadAllLines("members.txt")
            Dim count As Integer = 0

            ' Add each member to DataGridView
            For Each line As String In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    Dim parts() As String = line.Split("|"c)

                    If parts.Length >= 6 Then
                        ' Apply search filter if provided
                        If String.IsNullOrWhiteSpace(searchText) OrElse
                           parts(0).ToLower().Contains(searchText.ToLower()) OrElse
                           parts(1).ToLower().Contains(searchText.ToLower()) OrElse
                           parts(2).ToLower().Contains(searchText.ToLower()) OrElse
                           parts(4).ToLower().Contains(searchText.ToLower()) Then

                            dgvMembers.Rows.Add(parts(0), parts(1), parts(2), parts(3), parts(4), parts(5))
                            count += 1
                        End If
                    End If
                End If
            Next

            ' Update total count
            lblTotal.Text = "Total Members: " & count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error loading members: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Search Button
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadMembers(txtSearch.Text.Trim())
    End Sub

    ' Refresh Button
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadMembers()
    End Sub

    ' Delete Button
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            ' Check if a row is selected
            If dgvMembers.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a member to delete", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get selected member ID
            Dim memberID As String = dgvMembers.SelectedRows(0).Cells(0).Value.ToString()

            ' Confirm deletion
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this member?" & vbNewLine & "Member ID: " & memberID, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Delete member
                DeleteMember(memberID)

                ' Refresh list
                LoadMembers()

                MessageBox.Show("Member deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting member: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Delete member from file
    Private Sub DeleteMember(memberID As String)
        Try
            If File.Exists("members.txt") Then
                Dim lines() As String = File.ReadAllLines("members.txt")
                Dim newLines As New List(Of String)

                For Each line As String In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split("|"c)
                        If parts(0) <> memberID Then
                            newLines.Add(line)
                        End If
                    End If
                Next

                File.WriteAllLines("members.txt", newLines.ToArray())
            End If

        Catch ex As Exception
            Throw New Exception("Failed to delete member: " & ex.Message)
        End Try
    End Sub

    ' Close Button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Search TextBox - Enter key
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSearch_Click(sender, e)
        End If
    End Sub
End Class
