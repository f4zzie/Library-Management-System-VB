' ====================================================================
' Add Book Form
' Allows user to add a new book to the library
' Demonstrates SEQUENTIAL and SELECTION structures with validation
' ====================================================================

Public Class AddBookForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()

        Me.Text = "Add New Book"
        Me.Size = New Size(500, 550)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
    End Sub

    ' Save Button - SEQUENTIAL and SELECTION structures
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' SELECTION: Validate all input fields
        If Not ValidationHelper.IsNotEmpty(txtISBN.Text) Then
            ValidationHelper.ShowValidationError("ISBN", "ISBN is required")
            txtISBN.Focus()
            Return
        End If

        If Not ValidationHelper.IsValidISBN(txtISBN.Text) Then
            ValidationHelper.ShowValidationError("ISBN", "Invalid ISBN format")
            txtISBN.Focus()
            Return
        End If

        If Not ValidationHelper.IsNotEmpty(txtTitle.Text) Then
            ValidationHelper.ShowValidationError("Title", "Title is required")
            txtTitle.Focus()
            Return
        End If

        If Not ValidationHelper.IsNotEmpty(txtAuthor.Text) Then
            ValidationHelper.ShowValidationError("Author", "Author is required")
            txtAuthor.Focus()
            Return
        End If

        If Not ValidationHelper.IsNotEmpty(txtCategory.Text) Then
            ValidationHelper.ShowValidationError("Category", "Category is required")
            txtCategory.Focus()
            Return
        End If

        If Not ValidationHelper.IsNotEmpty(txtPublisher.Text) Then
            ValidationHelper.ShowValidationError("Publisher", "Publisher is required")
            txtPublisher.Focus()
            Return
        End If

        If Not ValidationHelper.IsValidYear(txtYear.Text) Then
            ValidationHelper.ShowValidationError("Year", "Please enter a valid year (e.g., 2020)")
            txtYear.Focus()
            Return
        End If

        If Not ValidationHelper.IsValidPositiveNumber(txtQuantity.Text) Then
            ValidationHelper.ShowValidationError("Quantity", "Please enter a valid quantity (positive number)")
            txtQuantity.Focus()
            Return
        End If

        ' SEQUENTIAL: Create new book object
        Dim newBook As New Book()
        newBook.ISBN = txtISBN.Text.Trim()
        newBook.Title = txtTitle.Text.Trim()
        newBook.Author = txtAuthor.Text.Trim()
        newBook.Category = txtCategory.Text.Trim()
        newBook.Publisher = txtPublisher.Text.Trim()
        newBook.YearPublished = Integer.Parse(txtYear.Text.Trim())
        newBook.Quantity = Integer.Parse(txtQuantity.Text.Trim())
        newBook.AvailableQuantity = newBook.Quantity

        ' Add to data store
        DataStore.AddBook(newBook)

        MessageHelper.ShowSuccess("Book added successfully!" & vbCrLf & "Book ID: " & newBook.BookID)

        ' Clear form
        ClearForm()
    End Sub

    ' SEQUENTIAL: Clear all input fields
    Private Sub ClearForm()
        txtISBN.Clear()
        txtTitle.Clear()
        txtAuthor.Clear()
        txtCategory.Clear()
        txtPublisher.Clear()
        txtYear.Clear()
        txtQuantity.Clear()
        txtISBN.Focus()
    End Sub

    ' Clear Button
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If MessageHelper.ShowConfirmation("Clear all fields?") Then
            ClearForm()
        End If
    End Sub

    ' Close Button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Designer code
    Private Sub InitializeComponent()
        Me.lblISBN = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.lblPublisher = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.lblQuantity = New System.Windows.Forms.Label()
        Me.txtISBN = New System.Windows.Forms.TextBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.txtAuthor = New System.Windows.Forms.TextBox()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.txtPublisher = New System.Windows.Forms.TextBox()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()

        ' Labels
        Me.lblISBN.AutoSize = True
        Me.lblISBN.Location = New System.Drawing.Point(50, 30)
        Me.lblISBN.Name = "lblISBN"
        Me.lblISBN.Size = New System.Drawing.Size(35, 13)
        Me.lblISBN.Text = "ISBN:"

        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(50, 70)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(30, 13)
        Me.lblTitle.Text = "Title:"

        Me.lblAuthor.AutoSize = True
        Me.lblAuthor.Location = New System.Drawing.Point(50, 110)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(41, 13)
        Me.lblAuthor.Text = "Author:"

        Me.lblCategory.AutoSize = True
        Me.lblCategory.Location = New System.Drawing.Point(50, 150)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(52, 13)
        Me.lblCategory.Text = "Category:"

        Me.lblPublisher.AutoSize = True
        Me.lblPublisher.Location = New System.Drawing.Point(50, 190)
        Me.lblPublisher.Name = "lblPublisher"
        Me.lblPublisher.Size = New System.Drawing.Size(53, 13)
        Me.lblPublisher.Text = "Publisher:"

        Me.lblYear.AutoSize = True
        Me.lblYear.Location = New System.Drawing.Point(50, 230)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(32, 13)
        Me.lblYear.Text = "Year:"

        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.Location = New System.Drawing.Point(50, 270)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(49, 13)
        Me.lblQuantity.Text = "Quantity:"

        ' TextBoxes
        Me.txtISBN.Location = New System.Drawing.Point(150, 27)
        Me.txtISBN.Name = "txtISBN"
        Me.txtISBN.Size = New System.Drawing.Size(300, 20)

        Me.txtTitle.Location = New System.Drawing.Point(150, 67)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(300, 20)

        Me.txtAuthor.Location = New System.Drawing.Point(150, 107)
        Me.txtAuthor.Name = "txtAuthor"
        Me.txtAuthor.Size = New System.Drawing.Size(300, 20)

        Me.txtCategory.Location = New System.Drawing.Point(150, 147)
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(300, 20)

        Me.txtPublisher.Location = New System.Drawing.Point(150, 187)
        Me.txtPublisher.Name = "txtPublisher"
        Me.txtPublisher.Size = New System.Drawing.Size(300, 20)

        Me.txtYear.Location = New System.Drawing.Point(150, 227)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(100, 20)

        Me.txtQuantity.Location = New System.Drawing.Point(150, 267)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(100, 20)

        ' Buttons
        Me.btnSave.Location = New System.Drawing.Point(100, 330)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 30)
        Me.btnSave.Text = "Save"

        Me.btnClear.Location = New System.Drawing.Point(220, 330)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 30)
        Me.btnClear.Text = "Clear"

        Me.btnClose.Location = New System.Drawing.Point(340, 330)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 30)
        Me.btnClose.Text = "Close"

        ' Form
        Me.ClientSize = New System.Drawing.Size(500, 400)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtQuantity)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.txtPublisher)
        Me.Controls.Add(Me.txtCategory)
        Me.Controls.Add(Me.txtAuthor)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.txtISBN)
        Me.Controls.Add(Me.lblQuantity)
        Me.Controls.Add(Me.lblYear)
        Me.Controls.Add(Me.lblPublisher)
        Me.Controls.Add(Me.lblCategory)
        Me.Controls.Add(Me.lblAuthor)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblISBN)
        Me.Name = "AddBookForm"
        Me.Text = "Add New Book"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblISBN, lblTitle, lblAuthor, lblCategory, lblPublisher, lblYear, lblQuantity As Label
    Friend WithEvents txtISBN, txtTitle, txtAuthor, txtCategory, txtPublisher, txtYear, txtQuantity As TextBox
    Friend WithEvents btnSave, btnClear, btnClose As Button
End Class
