' ====================================================================
' Search Books Form - Demonstrates ITERATION and SELECTION
' ====================================================================

Public Class SearchBooksForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()

        Me.Text = "Search Books"
        Me.Size = New Size(800, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    ' ITERATION & SELECTION: Search books by keyword
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchTerm As String = txtSearch.Text.Trim()

        If String.IsNullOrEmpty(searchTerm) Then
            MessageHelper.ShowWarning("Please enter search term")
            Return
        End If

        ' Search books
        Dim results As List(Of Book) = DataStore.SearchBooks(searchTerm)

        ' Display results
        lvResults.Items.Clear()
        For Each book As Book In results
            Dim item As New ListViewItem(book.BookID.ToString())
            item.SubItems.Add(book.Title)
            item.SubItems.Add(book.Author)
            item.SubItems.Add(book.Category)
            item.SubItems.Add(book.AvailableQuantity.ToString())
            lvResults.Items.Add(item)
        Next

        lblResults.Text = "Found " & results.Count & " book(s)"
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Designer initialization
    Private Sub InitializeComponent()
        Me.txtSearch = New TextBox()
        Me.btnSearch = New Button()
        Me.lvResults = New ListView()
        Me.lblResults = New Label()
        Me.btnClose = New Button()
        Me.SuspendLayout()

        Me.txtSearch.Location = New Point(20, 20)
        Me.txtSearch.Size = New Size(600, 20)

        Me.btnSearch.Location = New Point(630, 18)
        Me.btnSearch.Size = New Size(100, 25)
        Me.btnSearch.Text = "Search"

        Me.lblResults.Location = New Point(20, 60)
        Me.lblResults.Size = New Size(200, 20)
        Me.lblResults.Text = "Search results will appear below"

        Me.lvResults.Location = New Point(20, 90)
        Me.lvResults.Size = New Size(750, 350)
        Me.lvResults.View = View.Details
        Me.lvResults.GridLines = True
        Me.lvResults.FullRowSelect = True
        Me.lvResults.Columns.Add("ID", 50)
        Me.lvResults.Columns.Add("Title", 250)
        Me.lvResults.Columns.Add("Author", 150)
        Me.lvResults.Columns.Add("Category", 120)
        Me.lvResults.Columns.Add("Available", 80)

        Me.btnClose.Location = New Point(670, 450)
        Me.btnClose.Size = New Size(100, 30)
        Me.btnClose.Text = "Close"

        Me.ClientSize = New Size(800, 500)
        Me.Controls.Add(btnClose)
        Me.Controls.Add(lblResults)
        Me.Controls.Add(lvResults)
        Me.Controls.Add(btnSearch)
        Me.Controls.Add(txtSearch)
        Me.Name = "SearchBooksForm"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch, btnClose As Button
    Friend WithEvents lvResults As ListView
    Friend WithEvents lblResults As Label
End Class
