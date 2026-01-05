' ============================================
' Add Books Form - Library Management System
' Allows adding new books to the library
' ============================================
Imports System.IO

Public Class AddBooks
    ' Form Load Event
    Private Sub AddBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Generate next Book ID
        txtBookID.Text = GenerateNextBookID()
        txtBookID.ReadOnly = True
        
        ' Set Added Date to today
        dtpAddedDate.Value = DateTime.Now
    End Sub

    ' Generate next Book ID
    Private Function GenerateNextBookID() As String
        Try
            If File.Exists("books.txt") Then
                Dim lines() As String = File.ReadAllLines("books.txt")
                If lines.Length > 0 Then
                    Dim lastLine As String = lines(lines.Length - 1)
                    Dim parts() As String = lastLine.Split("|"c)
                    If parts.Length > 0 Then
                        Dim lastID As Integer = Integer.Parse(parts(0).Replace("BK", ""))
                        Return "BK" & (lastID + 1).ToString("D4")
                    End If
                End If
            End If
            Return "BK0001"
        Catch ex As Exception
            Return "BK0001"
        End Try
    End Function

    ' Add Book Button
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ' Input Validation
            If Not ValidateInputs() Then
                Return
            End If

            ' Create book record (now with Publisher, PublishYear, TotalCopies, AvailableCopies, ShelfLocation, AddedDate)
            Dim totalCopies As Integer = Integer.Parse(txtTotalCopies.Text.Trim())
            Dim bookRecord As String = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                txtBookID.Text.Trim(),
                txtTitle.Text.Trim(),
                txtAuthor.Text.Trim(),
                txtISBN.Text.Trim(),
                cmbCategory.Text.Trim(),
                txtPublisher.Text.Trim(),
                txtPublishYear.Text.Trim(),
                totalCopies.ToString(),
                totalCopies.ToString(),
                txtShelfLocation.Text.Trim(),
                dtpAddedDate.Value.ToString("yyyy-MM-dd"))

            ' Save to file
            File.AppendAllText("books.txt", bookRecord & Environment.NewLine)

            ' Success message
            MessageBox.Show("Book added successfully!" & vbNewLine & "Book ID: " & txtBookID.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear form
            ClearForm()

        Catch ex As Exception
            ' Error handling
            MessageBox.Show("Error adding book: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Validate all inputs
    Private Function ValidateInputs() As Boolean
        ' Validate Title
        If String.IsNullOrWhiteSpace(txtTitle.Text) Then
            MessageBox.Show("Please enter the book title", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTitle.Focus()
            Return False
        End If

        ' Validate Author
        If String.IsNullOrWhiteSpace(txtAuthor.Text) Then
            MessageBox.Show("Please enter the author name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAuthor.Focus()
            Return False
        End If

        ' Validate ISBN
        If String.IsNullOrWhiteSpace(txtISBN.Text) Then
            MessageBox.Show("Please enter the ISBN", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtISBN.Focus()
            Return False
        End If

        ' Validate ISBN format (numeric only)
        If Not IsNumeric(txtISBN.Text) Then
            MessageBox.Show("ISBN must contain only numbers", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtISBN.Focus()
            Return False
        End If

        ' Validate Publisher
        If String.IsNullOrWhiteSpace(txtPublisher.Text) Then
            MessageBox.Show("Please enter the publisher name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPublisher.Focus()
            Return False
        End If

        ' Validate Publish Year
        If String.IsNullOrWhiteSpace(txtPublishYear.Text) Then
            MessageBox.Show("Please enter the publish year", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPublishYear.Focus()
            Return False
        End If

        ' Validate Publish Year is numeric and reasonable
        Dim year As Integer
        If Not Integer.TryParse(txtPublishYear.Text, year) Or year < 1800 Or year > DateTime.Now.Year + 1 Then
            MessageBox.Show("Please enter a valid year (1800-" & (DateTime.Now.Year + 1).ToString() & ")", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPublishYear.Focus()
            Return False
        End If

        ' Validate Total Copies
        If String.IsNullOrWhiteSpace(txtTotalCopies.Text) Then
            MessageBox.Show("Please enter the total copies", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTotalCopies.Focus()
            Return False
        End If

        ' Validate Total Copies is numeric and positive
        Dim copies As Integer
        If Not Integer.TryParse(txtTotalCopies.Text, copies) Or copies <= 0 Then
            MessageBox.Show("Total copies must be a positive number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTotalCopies.Focus()
            Return False
        End If

        ' Validate Shelf Location
        If String.IsNullOrWhiteSpace(txtShelfLocation.Text) Then
            MessageBox.Show("Please enter the shelf location", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtShelfLocation.Focus()
            Return False
        End If

        ' Validate Category
        If String.IsNullOrWhiteSpace(cmbCategory.Text) Then
            MessageBox.Show("Please select a category", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbCategory.Focus()
            Return False
        End If

        Return True
    End Function

    ' Clear Form
    Private Sub ClearForm()
        txtTitle.Clear()
        txtAuthor.Clear()
        txtISBN.Clear()
        txtPublisher.Clear()
        txtPublishYear.Clear()
        txtTotalCopies.Clear()
        txtShelfLocation.Clear()
        cmbCategory.SelectedIndex = -1
        dtpAddedDate.Value = DateTime.Now
        txtBookID.Text = GenerateNextBookID()
        txtTitle.Focus()
    End Sub

    ' Clear Button
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    ' Close Button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' TotalCopies TextBox - KeyPress (Only allow numbers)
    Private Sub txtTotalCopies_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalCopies.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Please enter numbers only", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' PublishYear TextBox - KeyPress (Only allow numbers)
    Private Sub txtPublishYear_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPublishYear.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Please enter numbers only", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' ISBN TextBox - KeyPress (Only allow numbers)
    Private Sub txtISBN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtISBN.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("ISBN must contain numbers only", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class
