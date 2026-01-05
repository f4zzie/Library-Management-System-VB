' ============================================
' Issue Books Form - Library Management System
' Allows issuing books to members
' ============================================
Imports System.IO

Public Class IssueBooks
    ' Form Load Event
    Private Sub IssueBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Generate next Issue ID
        txtIssueID.Text = GenerateNextIssueID()
        txtIssueID.ReadOnly = True

        ' Set issue date to today
        dtpIssueDate.Value = DateTime.Now

        ' Calculate and set due date (14 days from today)
        dtpDueDate.Value = DateTime.Now.AddDays(14)

        ' Load available books into dropdown
        LoadAvailableBooks()
    End Sub

    ' Load Available Books into Dropdown
    Private Sub LoadAvailableBooks()
        Try
            cmbBookSelect.Items.Clear()

            If File.Exists("books.txt") Then
                Dim lines() As String = File.ReadAllLines("books.txt")
                For Each line As String In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split("|"c)
                        ' Only show books with available copies (field 8 > 0)
                        If parts.Length >= 11 AndAlso Integer.Parse(parts(8)) > 0 Then
                            ' Format: "BK0001 - Book Title (Available: 5)"
                            Dim displayText As String = String.Format("{0} - {1} (Available: {2})",
                                parts(0), parts(1), parts(8))
                            cmbBookSelect.Items.Add(displayText)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading books: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Book Selection Changed
    Private Sub cmbBookSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBookSelect.SelectedIndexChanged
        Try
            If cmbBookSelect.SelectedIndex >= 0 Then
                Dim selectedText As String = cmbBookSelect.SelectedItem.ToString()
                ' Extract Book ID from "BK0001 - Title (Available: 5)"
                Dim bookID As String = selectedText.Split("-"c)(0).Trim()

                ' Get full book info
                Dim bookInfo As String = GetBookInfo(bookID)
                If Not String.IsNullOrEmpty(bookInfo) Then
                    Dim parts() As String = bookInfo.Split("|"c)
                    If parts.Length >= 11 Then
                        txtBookID.Text = parts(0)  ' Store Book ID for later use
                        txtBookTitle.Text = parts(1)
                        txtAuthor.Text = parts(2)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting book: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Generate next Issue ID
    Private Function GenerateNextIssueID() As String
        Try
            If File.Exists("issued_books.txt") Then
                Dim lines() As String = File.ReadAllLines("issued_books.txt")
                Dim activeIssues = lines.Where(Function(l) Not l.Contains("|Returned|")).ToArray()
                If activeIssues.Length > 0 Then
                    Dim lastLine As String = lines(lines.Length - 1)
                    Dim parts() As String = lastLine.Split("|"c)
                    If parts.Length > 0 Then
                        Dim lastID As Integer = Integer.Parse(parts(0).Replace("ISS", ""))
                        Return "ISS" & (lastID + 1).ToString("D4")
                    End If
                End If
            End If
            Return "ISS0001"
        Catch ex As Exception
            Return "ISS0001"
        End Try
    End Function



    ' Verify Member Button
    Private Sub btnVerifyMember_Click(sender As Object, e As EventArgs) Handles btnVerifyMember.Click
        Try
            If String.IsNullOrWhiteSpace(txtMemberID.Text) Then
                MessageBox.Show("Please enter a Member ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim memberInfo As String = GetMemberInfo(txtMemberID.Text.Trim())

            If Not String.IsNullOrEmpty(memberInfo) Then
                Dim parts() As String = memberInfo.Split("|"c)
                txtMemberName.Text = parts(1)
                MessageBox.Show("Member verified successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Member ID not found in the database", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtMemberName.Clear()
            End If

        Catch ex As Exception
            MessageBox.Show("Error verifying member: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Get Book Info
    Private Function GetBookInfo(bookID As String) As String
        Try
            If File.Exists("books.txt") Then
                Dim lines() As String = File.ReadAllLines("books.txt")
                For Each line As String In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split("|"c)
                        If parts(0) = bookID Then
                            Return line
                        End If
                    End If
                Next
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' Get Member Info
    Private Function GetMemberInfo(memberID As String) As String
        Try
            If File.Exists("members.txt") Then
                Dim lines() As String = File.ReadAllLines("members.txt")
                For Each line As String In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split("|"c)
                        If parts(0) = memberID Then
                            Return line
                        End If
                    End If
                Next
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' Issue Book Button
    Private Sub btnIssue_Click(sender As Object, e As EventArgs) Handles btnIssue.Click
        Try
            ' Validation
            If Not ValidateInputs() Then
                Return
            End If

            ' Create issue record
            Dim issueRecord As String = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|Active",
                txtIssueID.Text.Trim(),
                txtBookID.Text.Trim(),
                txtMemberID.Text.Trim(),
                dtpIssueDate.Value.ToString("yyyy-MM-dd"),
                dtpDueDate.Value.ToString("yyyy-MM-dd"),
                txtBookTitle.Text.Trim())

            ' Save to file
            File.AppendAllText("issued_books.txt", issueRecord & Environment.NewLine)

            ' Update book quantity
            UpdateBookQuantity(txtBookID.Text.Trim(), -1)

            ' Success message
            MessageBox.Show("Book issued successfully!" & vbNewLine & "Issue ID: " & txtIssueID.Text & vbNewLine & "Due Date: " & dtpDueDate.Value.ToString("yyyy-MM-dd"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear form
            ClearForm()

        Catch ex As Exception
            MessageBox.Show("Error issuing book: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Validate Inputs
    Private Function ValidateInputs() As Boolean
        If cmbBookSelect.SelectedIndex < 0 Then
            MessageBox.Show("Please select a book", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtBookTitle.Text) Then
            MessageBox.Show("Book information not loaded", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtMemberID.Text) Then
            MessageBox.Show("Please enter and verify a Member ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtMemberName.Text) Then
            MessageBox.Show("Please verify the member first", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    ' Update Book Quantity (AvailableCopies in new 11-field format)
    Private Sub UpdateBookQuantity(bookID As String, change As Integer)
        Try
            If File.Exists("books.txt") Then
                Dim lines() As String = File.ReadAllLines("books.txt")
                Dim newLines As New List(Of String)

                For Each line As String In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split("|"c)
                        If parts(0) = bookID AndAlso parts.Length >= 11 Then
                            ' Update AvailableCopies (field 8)
                            Dim newAvailable As Integer = Integer.Parse(parts(8)) + change
                            ' Format: BookID|Title|Author|ISBN|Category|Publisher|PublishYear|TotalCopies|AvailableCopies|ShelfLocation|AddedDate
                            Dim newLine As String = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                                parts(0), parts(1), parts(2), parts(3), parts(4), parts(5), parts(6), parts(7), newAvailable, parts(9), parts(10))
                            newLines.Add(newLine)
                        Else
                            newLines.Add(line)
                        End If
                    End If
                Next

                File.WriteAllLines("books.txt", newLines.ToArray())
            End If

        Catch ex As Exception
            Throw New Exception("Failed to update book quantity: " & ex.Message)
        End Try
    End Sub

    ' Clear Form
    Private Sub ClearForm()
        cmbBookSelect.SelectedIndex = -1
        txtBookID.Clear()
        txtBookTitle.Clear()
        txtAuthor.Clear()
        txtMemberID.Clear()
        txtMemberName.Clear()
        dtpIssueDate.Value = DateTime.Now
        dtpDueDate.Value = DateTime.Now.AddDays(14)
        txtIssueID.Text = GenerateNextIssueID()
        LoadAvailableBooks()  ' Refresh book list
        cmbBookSelect.Focus()
    End Sub

    ' Clear Button
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    ' Close Button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
