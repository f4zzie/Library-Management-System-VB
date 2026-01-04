' ============================================
' Return Books Form - Library Management System
' Allows returning issued books
' ============================================
Imports System.IO

Public Class ReturnBooks
    ' Form Load Event
    Private Sub ReturnBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set return date to today
        dtpReturnDate.Value = DateTime.Now

        ' Setup DataGridView
        SetupDataGridView()

        ' Load issued books
        LoadIssuedBooks()
    End Sub

    ' Setup DataGridView
    Private Sub SetupDataGridView()
        ' Clear columns
        dgvIssuedBooks.Columns.Clear()

        ' Add columns
        dgvIssuedBooks.Columns.Add("IssueID", "Issue ID")
        dgvIssuedBooks.Columns.Add("BookID", "Book ID")
        dgvIssuedBooks.Columns.Add("MemberID", "Member ID")
        dgvIssuedBooks.Columns.Add("IssueDate", "Issue Date")
        dgvIssuedBooks.Columns.Add("DueDate", "Due Date")
        dgvIssuedBooks.Columns.Add("BookTitle", "Book Title")

        ' Set column widths
        dgvIssuedBooks.Columns(0).Width = 80
        dgvIssuedBooks.Columns(1).Width = 80
        dgvIssuedBooks.Columns(2).Width = 100
        dgvIssuedBooks.Columns(3).Width = 100
        dgvIssuedBooks.Columns(4).Width = 100
        dgvIssuedBooks.Columns(5).Width = 250

        ' Set properties
        dgvIssuedBooks.ReadOnly = True
        dgvIssuedBooks.AllowUserToAddRows = False
        dgvIssuedBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    ' Load issued books from file
    Private Sub LoadIssuedBooks()
        Try
            ' Clear existing rows
            dgvIssuedBooks.Rows.Clear()

            ' Check if file exists
            If Not File.Exists("issued_books.txt") Then
                lblTotal.Text = "Total Issued Books: 0"
                Return
            End If

            ' Read all lines
            Dim lines() As String = File.ReadAllLines("issued_books.txt")
            Dim count As Integer = 0

            ' Add each issued book to DataGridView (only active ones)
            For Each line As String In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    Dim parts() As String = line.Split("|"c)

                    If parts.Length >= 7 And parts(6) = "Active" Then
                        dgvIssuedBooks.Rows.Add(parts(0), parts(1), parts(2), parts(3), parts(4), parts(5))
                        count += 1
                    End If
                End If
            Next

            ' Update total count
            lblTotal.Text = "Total Issued Books: " & count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error loading issued books: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Return Book Button
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Try
            ' Check if a row is selected
            If dgvIssuedBooks.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a book to return", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get selected issue details
            Dim issueID As String = dgvIssuedBooks.SelectedRows(0).Cells(0).Value.ToString()
            Dim bookID As String = dgvIssuedBooks.SelectedRows(0).Cells(1).Value.ToString()
            Dim dueDate As Date = Date.Parse(dgvIssuedBooks.SelectedRows(0).Cells(4).Value.ToString())

            ' Calculate fine if book is returned late
            Dim fine As Decimal = CalculateFine(dueDate, dtpReturnDate.Value)

            ' Show fine information if applicable
            Dim message As String = "Book returned successfully!"
            If fine > 0 Then
                message &= vbNewLine & vbNewLine & "Late Return Fine: $" & fine.ToString("F2") & vbNewLine & "Days Late: " & DateDiff(DateInterval.Day, dueDate, dtpReturnDate.Value).ToString()
            End If

            ' Confirm return
            Dim result As DialogResult = MessageBox.Show(message & vbNewLine & vbNewLine & "Confirm book return?", "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Mark book as returned
                UpdateIssuedBook(issueID, dtpReturnDate.Value, fine)

                ' Update book quantity
                UpdateBookQuantity(bookID, 1)

                ' Refresh list
                LoadIssuedBooks()

                MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error returning book: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Calculate fine for late return
    Private Function CalculateFine(dueDate As Date, returnDate As Date) As Decimal
        Const FINE_PER_DAY As Decimal = 0.5D ' $0.50 per day

        If returnDate > dueDate Then
            Dim daysLate As Integer = DateDiff(DateInterval.Day, dueDate, returnDate)
            Return daysLate * FINE_PER_DAY
        Else
            Return 0
        End If
    End Function

    ' Update issued book record
    Private Sub UpdateIssuedBook(issueID As String, returnDate As Date, fine As Decimal)
        Try
            If File.Exists("issued_books.txt") Then
                Dim lines() As String = File.ReadAllLines("issued_books.txt")
                Dim newLines As New List(Of String)

                For Each line As String In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split("|"c)
                        If parts(0) = issueID Then
                            ' Update status to Returned with return date and fine
                            Dim newLine As String = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|Returned|{6}|{7}",
                                parts(0), parts(1), parts(2), parts(3), parts(4), parts(5),
                                returnDate.ToString("yyyy-MM-dd"), fine.ToString("F2"))
                            newLines.Add(newLine)
                        Else
                            newLines.Add(line)
                        End If
                    End If
                Next

                File.WriteAllLines("issued_books.txt", newLines.ToArray())
            End If

        Catch ex As Exception
            Throw New Exception("Failed to update issued book: " & ex.Message)
        End Try
    End Sub

    ' Update Book Quantity
    Private Sub UpdateBookQuantity(bookID As String, change As Integer)
        Try
            If File.Exists("books.txt") Then
                Dim lines() As String = File.ReadAllLines("books.txt")
                Dim newLines As New List(Of String)

                For Each line As String In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split("|"c)
                        If parts(0) = bookID Then
                            Dim newQuantity As Integer = Integer.Parse(parts(4)) + change
                            Dim newLine As String = String.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                                parts(0), parts(1), parts(2), parts(3), newQuantity, parts(5))
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

    ' Refresh Button
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadIssuedBooks()
    End Sub

    ' Close Button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' DataGridView Selection Changed - Show fine calculation
    Private Sub dgvIssuedBooks_SelectionChanged(sender As Object, e As EventArgs) Handles dgvIssuedBooks.SelectionChanged
        Try
            If dgvIssuedBooks.SelectedRows.Count > 0 Then
                Dim dueDate As Date = Date.Parse(dgvIssuedBooks.SelectedRows(0).Cells(4).Value.ToString())
                Dim fine As Decimal = CalculateFine(dueDate, dtpReturnDate.Value)

                If fine > 0 Then
                    Dim daysLate As Integer = DateDiff(DateInterval.Day, dueDate, dtpReturnDate.Value)
                    lblFineInfo.Text = "Late Return - Fine: $" & fine.ToString("F2") & " (" & daysLate.ToString() & " days late)"
                    lblFineInfo.ForeColor = Color.Red
                Else
                    lblFineInfo.Text = "On Time - No Fine"
                    lblFineInfo.ForeColor = Color.Green
                End If
            Else
                lblFineInfo.Text = ""
            End If

        Catch ex As Exception
            lblFineInfo.Text = ""
        End Try
    End Sub
End Class
