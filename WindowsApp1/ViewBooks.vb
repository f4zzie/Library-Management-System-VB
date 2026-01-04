' ============================================
' View Books Form - Library Management System
' Displays all books with search functionality
' ============================================
Imports System.IO

Public Class ViewBooks
    ' Form Load Event
    Private Sub ViewBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup DataGridView
        SetupDataGridView()

        ' Load books
        LoadBooks()
    End Sub

    ' Setup DataGridView
    Private Sub SetupDataGridView()
        ' Clear columns
        dgvBooks.Columns.Clear()

        ' Add columns
        dgvBooks.Columns.Add("BookID", "Book ID")
        dgvBooks.Columns.Add("Title", "Title")
        dgvBooks.Columns.Add("Author", "Author")
        dgvBooks.Columns.Add("ISBN", "ISBN")
        dgvBooks.Columns.Add("Quantity", "Quantity")
        dgvBooks.Columns.Add("Category", "Category")

        ' Set column widths
        dgvBooks.Columns(0).Width = 100
        dgvBooks.Columns(1).Width = 250
        dgvBooks.Columns(2).Width = 200
        dgvBooks.Columns(3).Width = 120
        dgvBooks.Columns(4).Width = 80
        dgvBooks.Columns(5).Width = 120

        ' Set properties
        dgvBooks.ReadOnly = True
        dgvBooks.AllowUserToAddRows = False
        dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    ' Load books from file
    Private Sub LoadBooks(Optional searchText As String = "")
        Try
            ' Clear existing rows
            dgvBooks.Rows.Clear()

            ' Check if file exists
            If Not File.Exists("books.txt") Then
                lblTotal.Text = "Total Books: 0"
                Return
            End If

            ' Read all lines
            Dim lines() As String = File.ReadAllLines("books.txt")
            Dim count As Integer = 0

            ' Add each book to DataGridView
            For Each line As String In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    Dim parts() As String = line.Split("|"c)

                    If parts.Length >= 6 Then
                        ' Apply search filter if provided
                        If String.IsNullOrWhiteSpace(searchText) OrElse
                           parts(0).ToLower().Contains(searchText.ToLower()) OrElse
                           parts(1).ToLower().Contains(searchText.ToLower()) OrElse
                           parts(2).ToLower().Contains(searchText.ToLower()) OrElse
                           parts(5).ToLower().Contains(searchText.ToLower()) Then

                            dgvBooks.Rows.Add(parts(0), parts(1), parts(2), parts(3), parts(4), parts(5))
                            count += 1
                        End If
                    End If
                End If
            Next

            ' Update total count
            lblTotal.Text = "Total Books: " & count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error loading books: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Search Button
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadBooks(txtSearch.Text.Trim())
    End Sub

    ' Refresh Button
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadBooks()
    End Sub

    ' Delete Button
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            ' Check if a row is selected
            If dgvBooks.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a book to delete", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get selected book ID
            Dim bookID As String = dgvBooks.SelectedRows(0).Cells(0).Value.ToString()

            ' Confirm deletion
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this book?" & vbNewLine & "Book ID: " & bookID, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Delete book
                DeleteBook(bookID)

                ' Refresh list
                LoadBooks()

                MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting book: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Delete book from file
    Private Sub DeleteBook(bookID As String)
        Try
            If File.Exists("books.txt") Then
                Dim lines() As String = File.ReadAllLines("books.txt")
                Dim newLines As New List(Of String)

                For Each line As String In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split("|"c)
                        If parts(0) <> bookID Then
                            newLines.Add(line)
                        End If
                    End If
                Next

                File.WriteAllLines("books.txt", newLines.ToArray())
            End If

        Catch ex As Exception
            Throw New Exception("Failed to delete book: " & ex.Message)
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
