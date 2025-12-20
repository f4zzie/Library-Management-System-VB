' ====================================================================
' View Books Form
' Displays all books in a list with details
' Demonstrates ITERATION structure to loop through books
' ====================================================================

Public Class ViewBooksForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        
        Me.Text = "View All Books"
        Me.Size = New Size(900, 600)
        Me.StartPosition = FormStartPosition.CenterScreen
        
        ' Load all books into the list
        LoadBooks()
    End Sub

    ' ITERATION: Load all books into ListView
    Private Sub LoadBooks()
        lvBooks.Items.Clear()

        ' ITERATION: Loop through all books
        For Each book As Book In DataStore.Books
            Dim item As New ListViewItem(book.BookID.ToString())
            item.SubItems.Add(book.ISBN)
            item.SubItems.Add(book.Title)
            item.SubItems.Add(book.Author)
            item.SubItems.Add(book.Category)
            item.SubItems.Add(book.Publisher)
            item.SubItems.Add(book.YearPublished.ToString())
            item.SubItems.Add(book.Quantity.ToString())
            item.SubItems.Add(book.AvailableQuantity.ToString())

            ' SELECTION: Color code based on availability
            If book.AvailableQuantity = 0 Then
                item.ForeColor = Color.Red
            ElseIf book.AvailableQuantity < book.Quantity / 2 Then
                item.ForeColor = Color.Orange
            Else
                item.ForeColor = Color.Green
            End If

            lvBooks.Items.Add(item)
        Next

        lblTotal.Text = "Total Books: " & DataStore.Books.Count.ToString()
    End Sub

    ' Edit selected book
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If lvBooks.SelectedItems.Count > 0 Then
            Dim bookID As Integer = Integer.Parse(lvBooks.SelectedItems(0).Text)
            Dim book As Book = DataStore.GetBookByID(bookID)

            If book IsNot Nothing Then
                ' Open edit form (to be implemented)
                MessageHelper.ShowInfo("Edit Book: " & book.Title & vbCrLf & "Edit form to be implemented")
            End If
        Else
            MessageHelper.ShowWarning("Please select a book to edit")
        End If
    End Sub

    ' Delete selected book
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lvBooks.SelectedItems.Count > 0 Then
            If MessageHelper.ShowConfirmation("Are you sure you want to delete this book?") Then
                Dim bookID As Integer = Integer.Parse(lvBooks.SelectedItems(0).Text)
                If DataStore.DeleteBook(bookID) Then
                    MessageHelper.ShowSuccess("Book deleted successfully!")
                    LoadBooks()
                Else
                    MessageHelper.ShowError("Failed to delete book")
                End If
            End If
        Else
            MessageHelper.ShowWarning("Please select a book to delete")
        End If
    End Sub

    ' Refresh list
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadBooks()
        MessageHelper.ShowSuccess("Book list refreshed!")
    End Sub

    ' Close form
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Initialize ListView control
    Private Sub InitializeComponent()
        Me.lvBooks = New System.Windows.Forms.ListView()
        Me.colID = New System.Windows.Forms.ColumnHeader()
        Me.colISBN = New System.Windows.Forms.ColumnHeader()
        Me.colTitle = New System.Windows.Forms.ColumnHeader()
        Me.colAuthor = New System.Windows.Forms.ColumnHeader()
        Me.colCategory = New System.Windows.Forms.ColumnHeader()
        Me.colPublisher = New System.Windows.Forms.ColumnHeader()
        Me.colYear = New System.Windows.Forms.ColumnHeader()
        Me.colQuantity = New System.Windows.Forms.ColumnHeader()
        Me.colAvailable = New System.Windows.Forms.ColumnHeader()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()

        ' lvBooks
        Me.lvBooks.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colISBN, Me.colTitle, Me.colAuthor, Me.colCategory, Me.colPublisher, Me.colYear, Me.colQuantity, Me.colAvailable})
        Me.lvBooks.FullRowSelect = True
        Me.lvBooks.GridLines = True
        Me.lvBooks.Location = New System.Drawing.Point(20, 50)
        Me.lvBooks.Name = "lvBooks"
        Me.lvBooks.Size = New System.Drawing.Size(850, 450)
        Me.lvBooks.TabIndex = 0
        Me.lvBooks.UseCompatibleStateImageBehavior = False
        Me.lvBooks.View = System.Windows.Forms.View.Details

        ' Columns
        Me.colID.Text = "ID"
        Me.colID.Width = 50
        Me.colISBN.Text = "ISBN"
        Me.colISBN.Width = 120
        Me.colTitle.Text = "Title"
        Me.colTitle.Width = 200
        Me.colAuthor.Text = "Author"
        Me.colAuthor.Width = 130
        Me.colCategory.Text = "Category"
        Me.colCategory.Width = 100
        Me.colPublisher.Text = "Publisher"
        Me.colPublisher.Width = 100
        Me.colYear.Text = "Year"
        Me.colYear.Width = 60
        Me.colQuantity.Text = "Qty"
        Me.colQuantity.Width = 45
        Me.colAvailable.Text = "Available"
        Me.colAvailable.Width = 70

        ' lblTotal
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.Location = New System.Drawing.Point(20, 20)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(100, 17)
        Me.lblTotal.TabIndex = 1
        Me.lblTotal.Text = "Total Books: 0"

        ' Buttons
        Me.btnEdit.Location = New System.Drawing.Point(20, 520)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(100, 30)
        Me.btnEdit.TabIndex = 2
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True

        Me.btnDelete.Location = New System.Drawing.Point(130, 520)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 30)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True

        Me.btnRefresh.Location = New System.Drawing.Point(240, 520)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 30)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True

        Me.btnClose.Location = New System.Drawing.Point(770, 520)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 30)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True

        ' Form
        Me.ClientSize = New System.Drawing.Size(900, 570)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lvBooks)
        Me.Name = "ViewBooksForm"
        Me.Text = "View All Books"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lvBooks As ListView
    Friend WithEvents colID As ColumnHeader
    Friend WithEvents colISBN As ColumnHeader
    Friend WithEvents colTitle As ColumnHeader
    Friend WithEvents colAuthor As ColumnHeader
    Friend WithEvents colCategory As ColumnHeader
    Friend WithEvents colPublisher As ColumnHeader
    Friend WithEvents colYear As ColumnHeader
    Friend WithEvents colQuantity As ColumnHeader
    Friend WithEvents colAvailable As ColumnHeader
    Friend WithEvents lblTotal As Label
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnClose As Button
End Class
