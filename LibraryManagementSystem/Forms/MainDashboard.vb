' ====================================================================
' Main Dashboard Form
' Main interface after login - shows navigation to all features
' Demonstrates SEQUENTIAL and SELECTION structures
' ====================================================================

Public Class MainDashboard
    ' Form Load Event
    Private Sub MainDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set form properties
        Me.Text = "Library Management System - Dashboard"
        Me.WindowState = FormWindowState.Maximized
        Me.IsMdiContainer = False

        ' Display welcome message
        lblWelcome.Text = "Welcome, " & DataStore.CurrentUser.FullName & " (" & DataStore.CurrentUser.Role & ")"

        ' Update statistics
        UpdateDashboardStatistics()

        ' SELECTION: Show/hide admin features based on user role
        If DataStore.CurrentUser.Role = "Admin" Then
            btnManageUsers.Visible = True
        Else
            btnManageUsers.Visible = False
        End If
    End Sub

    ' SEQUENTIAL & ITERATION: Update dashboard statistics
    Private Sub UpdateDashboardStatistics()
        ' Count total books
        Dim totalBooks As Integer = 0
        For Each book As Book In DataStore.Books
            totalBooks += book.Quantity
        Next
        lblTotalBooks.Text = "Total Books: " & totalBooks.ToString()

        ' Count available books
        Dim availableBooks As Integer = 0
        For Each book As Book In DataStore.Books
            availableBooks += book.AvailableQuantity
        Next
        lblAvailableBooks.Text = "Available: " & availableBooks.ToString()

        ' Count total members
        lblTotalMembers.Text = "Total Members: " & DataStore.Members.Count.ToString()

        ' Count active transactions
        Dim activeTransactions As Integer = 0
        For Each trans As Transaction In DataStore.Transactions
            If trans.Status = "Issued" Then
                activeTransactions += 1
            End If
        Next
        lblActiveTransactions.Text = "Active Loans: " & activeTransactions.ToString()

        ' Count overdue books
        Dim overdueCount As Integer = DataStore.GetOverdueTransactions().Count
        lblOverdue.Text = "Overdue Books: " & overdueCount.ToString()

        ' SELECTION: Change color if there are overdue books
        If overdueCount > 0 Then
            lblOverdue.ForeColor = Color.Red
        Else
            lblOverdue.ForeColor = Color.Green
        End If
    End Sub

    ' ====================================================================
    ' NAVIGATION BUTTON EVENTS - SELECTION structure
    ' ====================================================================

    ' View All Books
    Private Sub btnViewBooks_Click(sender As Object, e As EventArgs) Handles btnViewBooks.Click
        Dim viewBooksForm As New ViewBooksForm()
        viewBooksForm.ShowDialog()
        UpdateDashboardStatistics() ' Refresh stats
    End Sub

    ' Add New Book
    Private Sub btnAddBook_Click(sender As Object, e As EventArgs) Handles btnAddBook.Click
        Dim addBookForm As New AddBookForm()
        addBookForm.ShowDialog()
        UpdateDashboardStatistics() ' Refresh stats
    End Sub

    ' View All Members
    Private Sub btnViewMembers_Click(sender As Object, e As EventArgs) Handles btnViewMembers.Click
        Dim viewMembersForm As New ViewMembersForm()
        viewMembersForm.ShowDialog()
        UpdateDashboardStatistics() ' Refresh stats
    End Sub

    ' Add New Member
    Private Sub btnAddMember_Click(sender As Object, e As EventArgs) Handles btnAddMember.Click
        Dim addMemberForm As New AddMemberForm()
        addMemberForm.ShowDialog()
        UpdateDashboardStatistics() ' Refresh stats
    End Sub

    ' Issue Book
    Private Sub btnIssueBook_Click(sender As Object, e As EventArgs) Handles btnIssueBook.Click
        Dim issueBookForm As New IssueBookForm()
        issueBookForm.ShowDialog()
        UpdateDashboardStatistics() ' Refresh stats
    End Sub

    ' Return Book
    Private Sub btnReturnBook_Click(sender As Object, e As EventArgs) Handles btnReturnBook.Click
        Dim returnBookForm As New ReturnBookForm()
        returnBookForm.ShowDialog()
        UpdateDashboardStatistics() ' Refresh stats
    End Sub

    ' View Transaction History
    Private Sub btnViewTransactions_Click(sender As Object, e As EventArgs) Handles btnViewTransactions.Click
        Dim transactionHistoryForm As New TransactionHistoryForm()
        transactionHistoryForm.ShowDialog()
    End Sub

    ' View Overdue Report
    Private Sub btnOverdueReport_Click(sender As Object, e As EventArgs) Handles btnOverdueReport.Click
        Dim overdueReportForm As New OverdueReportForm()
        overdueReportForm.ShowDialog()
    End Sub

    ' Search Books
    Private Sub btnSearchBooks_Click(sender As Object, e As EventArgs) Handles btnSearchBooks.Click
        Dim searchForm As New SearchBooksForm()
        searchForm.ShowDialog()
    End Sub

    ' Manage Users (Admin only)
    Private Sub btnManageUsers_Click(sender As Object, e As EventArgs) Handles btnManageUsers.Click
        ' SELECTION: Check if user is admin
        If DataStore.CurrentUser.IsAdmin() Then
            MessageHelper.ShowInfo("User Management feature - Coming soon!" & vbCrLf &
                                 "This would allow adding/editing system users.")
        Else
            MessageHelper.ShowError("Access Denied! Admin privileges required.")
        End If
    End Sub

    ' Refresh Dashboard
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        UpdateDashboardStatistics()
        MessageHelper.ShowSuccess("Dashboard refreshed!")
    End Sub

    ' Logout
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' SELECTION: Confirm logout
        If MessageHelper.ShowConfirmation("Are you sure you want to logout?") Then
            DataStore.CurrentUser = Nothing
            Me.Close()
        End If
    End Sub

    ' Form Closing Event
    Private Sub MainDashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Return to login form
        Dim loginForm As New LoginForm()
        loginForm.Show()
    End Sub
End Class
