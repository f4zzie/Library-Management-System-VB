' ====================================================================
' Student Dashboard Form - MINIMAL VERSION
' Shows basic student information and borrowed books
' Demonstrates SEQUENTIAL, SELECTION, and ITERATION structures
' ====================================================================

Public Class StudentDashboard
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        
        Me.Text = "Student Portal - Library Management System"
        Me.Size = New Size(800, 600)
        Me.StartPosition = FormStartPosition.CenterScreen
        
        ' SEQUENTIAL: Load student information
        LoadStudentInfo()
    End Sub

    ' SEQUENTIAL & SELECTION: Load student information
    Private Sub LoadStudentInfo()
        ' Get current student's member record
        Dim currentStudent As Member = DataStore.GetMemberByID(DataStore.CurrentUser.MemberID)
        
        ' SELECTION: Check if member exists
        If currentStudent IsNot Nothing Then
            ' Display student information
            lblWelcome.Text = "Welcome, " & currentStudent.FullName & "!"
            lblMemberID.Text = "Member ID: " & currentStudent.MemberID.ToString()
            lblEmail.Text = "Email: " & currentStudent.Email
            lblPhone.Text = "Phone: " & currentStudent.PhoneNumber
            lblStatus.Text = "Status: " & currentStudent.Status
            
            ' Load borrowed books
            LoadMyBooks()
            
            ' Calculate fines
            CalculateFines()
        End If
    End Sub

    ' ITERATION: Load borrowed books for this student
    Private Sub LoadMyBooks()
        lvMyBooks.Items.Clear()
        Dim borrowedCount As Integer = 0
        
        ' ITERATION: Loop through all transactions
        For Each trans As Transaction In DataStore.Transactions
            ' SELECTION: Check if transaction belongs to this student and is active
            If trans.MemberID = DataStore.CurrentUser.MemberID And trans.Status = "Issued" Then
                borrowedCount += 1
                
                ' Add to list
                Dim item As New ListViewItem(trans.BookTitle)
                item.SubItems.Add(trans.IssueDate.ToShortDateString())
                item.SubItems.Add(trans.DueDate.ToShortDateString())
                
                ' Calculate days remaining
                Dim daysRemaining As Integer = DateDiff(DateInterval.Day, Date.Today, trans.DueDate)
                
                ' SELECTION: Color code by due date
                If trans.IsOverdue() Then
                    item.ForeColor = Color.Red
                    item.SubItems.Add("OVERDUE!")
                ElseIf daysRemaining <= 3 Then
                    item.ForeColor = Color.Orange
                    item.SubItems.Add(daysRemaining.ToString() & " days left")
                Else
                    item.ForeColor = Color.Green
                    item.SubItems.Add(daysRemaining.ToString() & " days left")
                End If
                
                lvMyBooks.Items.Add(item)
            End If
        Next
        
        lblBorrowedCount.Text = "Currently Borrowed: " & borrowedCount.ToString() & " book(s)"
    End Sub

    ' ITERATION & SELECTION: Calculate total outstanding fines
    Private Sub CalculateFines()
        Dim totalFines As Decimal = 0
        
        ' ITERATION: Loop through transactions
        For Each trans As Transaction In DataStore.Transactions
            ' SELECTION: Check if belongs to student and is overdue
            If trans.MemberID = DataStore.CurrentUser.MemberID And trans.IsOverdue() Then
                totalFines += trans.CalculateFine()
            End If
        Next
        
        lblFines.Text = "Outstanding Fines: $" & totalFines.ToString("F2")
        
        ' SELECTION: Color code fines
        If totalFines > 0 Then
            lblFines.ForeColor = Color.Red
            lblFines.Font = New Font(lblFines.Font, FontStyle.Bold)
        Else
            lblFines.ForeColor = Color.Green
        End If
    End Sub

    ' Refresh button
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadStudentInfo()
        MessageHelper.ShowSuccess("Information refreshed!")
    End Sub

    ' Logout button
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' SELECTION: Confirm logout
        If MessageHelper.ShowConfirmation("Are you sure you want to logout?") Then
            DataStore.CurrentUser = Nothing
            Me.Close()
        End If
    End Sub

    ' Form closing - return to login
    Private Sub StudentDashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim loginForm As New LoginForm()
        loginForm.Show()
    End Sub

    ' Designer - Initialize controls
    Private Sub InitializeComponent()
        Me.lblTitle = New Label()
        Me.lblWelcome = New Label()
        Me.lblMemberID = New Label()
        Me.lblEmail = New Label()
        Me.lblPhone = New Label()
        Me.lblStatus = New Label()
        Me.lblBorrowedCount = New Label()
        Me.lblFines = New Label()
        Me.grpInfo = New GroupBox()
        Me.grpBooks = New GroupBox()
        Me.lvMyBooks = New ListView()
        Me.btnRefresh = New Button()
        Me.btnLogout = New Button()
        Me.SuspendLayout()
        
        ' Title
        Me.lblTitle.Font = New Font("Microsoft Sans Serif", 16.0!, FontStyle.Bold)
        Me.lblTitle.Location = New Point(30, 20)
        Me.lblTitle.Size = New Size(400, 30)
        Me.lblTitle.Text = "Student Portal"
        
        ' Info Group
        Me.grpInfo.Location = New Point(30, 60)
        Me.grpInfo.Size = New Size(730, 150)
        Me.grpInfo.Text = "My Information"
        
        Me.lblWelcome.Font = New Font("Microsoft Sans Serif", 12.0!, FontStyle.Bold)
        Me.lblWelcome.Location = New Point(20, 25)
        Me.lblWelcome.Size = New Size(600, 25)
        Me.lblWelcome.Text = "Welcome, Student!"
        
        Me.lblMemberID.Location = New Point(20, 60)
        Me.lblMemberID.Size = New Size(300, 20)
        Me.lblMemberID.Text = "Member ID: -"
        
        Me.lblEmail.Location = New Point(20, 85)
        Me.lblEmail.Size = New Size(400, 20)
        Me.lblEmail.Text = "Email: -"
        
        Me.lblPhone.Location = New Point(20, 110)
        Me.lblPhone.Size = New Size(300, 20)
        Me.lblPhone.Text = "Phone: -"
        
        Me.lblStatus.Location = New Point(400, 60)
        Me.lblStatus.Size = New Size(200, 20)
        Me.lblStatus.Text = "Status: -"
        
        Me.lblFines.Font = New Font("Microsoft Sans Serif", 11.0!, FontStyle.Bold)
        Me.lblFines.Location = New Point(400, 85)
        Me.lblFines.Size = New Size(300, 25)
        Me.lblFines.Text = "Outstanding Fines: $0.00"
        
        Me.grpInfo.Controls.AddRange(New Control() {
            lblWelcome, lblMemberID, lblEmail, lblPhone, lblStatus, lblFines
        })
        
        ' Books Group
        Me.grpBooks.Location = New Point(30, 230)
        Me.grpBooks.Size = New Size(730, 280)
        Me.grpBooks.Text = "My Borrowed Books"
        
        Me.lblBorrowedCount.Font = New Font("Microsoft Sans Serif", 10.0!, FontStyle.Bold)
        Me.lblBorrowedCount.Location = New Point(20, 25)
        Me.lblBorrowedCount.Size = New Size(300, 20)
        Me.lblBorrowedCount.Text = "Currently Borrowed: 0 book(s)"
        
        Me.lvMyBooks.Location = New Point(20, 55)
        Me.lvMyBooks.Size = New Size(690, 210)
        Me.lvMyBooks.View = View.Details
        Me.lvMyBooks.GridLines = True
        Me.lvMyBooks.FullRowSelect = True
        Me.lvMyBooks.Columns.Add("Book Title", 300)
        Me.lvMyBooks.Columns.Add("Issue Date", 110)
        Me.lvMyBooks.Columns.Add("Due Date", 110)
        Me.lvMyBooks.Columns.Add("Status", 150)
        
        Me.grpBooks.Controls.AddRange(New Control() {lblBorrowedCount, lvMyBooks})
        
        ' Buttons
        Me.btnRefresh.Location = New Point(550, 525)
        Me.btnRefresh.Size = New Size(100, 35)
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Font = New Font("Microsoft Sans Serif", 10.0!)
        
        Me.btnLogout.Location = New Point(660, 525)
        Me.btnLogout.Size = New Size(100, 35)
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.Font = New Font("Microsoft Sans Serif", 10.0!)
        
        ' Form
        Me.ClientSize = New Size(800, 580)
        Me.Controls.AddRange(New Control() {lblTitle, grpInfo, grpBooks, btnRefresh, btnLogout})
        Me.Name = "StudentDashboard"
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.ResumeLayout(False)
    End Sub

    ' Control declarations
    Friend WithEvents lblTitle, lblWelcome, lblMemberID, lblEmail, lblPhone As Label
    Friend WithEvents lblStatus, lblBorrowedCount, lblFines As Label
    Friend WithEvents grpInfo, grpBooks As GroupBox
    Friend WithEvents lvMyBooks As ListView
    Friend WithEvents btnRefresh, btnLogout As Button
End Class
