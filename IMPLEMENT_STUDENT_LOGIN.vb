' ====================================================================
' QUICK START: Add Student Login Feature
' Copy this code to the appropriate files
' Follow the numbered steps in order
' ====================================================================

' ====================================================================
' STEP 1: Modify User.vb (Models/User.vb)
' Add these properties to the User class:
' ====================================================================

'Public Property UserType As String  ' "Admin", "Librarian", or "Student"
'Public Property MemberID As Integer ' Link student user to member record

' The complete User class should look like this:
'
'Public Class User
'    Public Property UserID As Integer
'    Public Property Username As String
'    Public Property Password As String
'    Public Property FullName As String
'    Public Property Role As String
'    Public Property Email As String
'    Public Property UserType As String   ' NEW
'    Public Property MemberID As Integer  ' NEW
'    
'    ' Existing constructors and methods...
'End Class


' ====================================================================
' STEP 2: Modify DataStore.vb (Data/DataStore.vb)
' Add these lines in InitializeSampleData() after existing Users.Add:
' ====================================================================

' Add student users (linked to existing members)
'Users.Add(New User(3, "alice", "student123", "Alice Brown", "Student", "alice@email.com") With {.MemberID = 1})
'Users.Add(New User(4, "bob", "student123", "Bob Wilson", "Student", "bob@email.com") With {.MemberID = 2})
'Users.Add(New User(5, "carol", "student123", "Carol Davis", "Student", "carol@email.com") With {.MemberID = 3})
'NextUserID = 6


' ====================================================================
' STEP 3: Modify LoginForm.vb (Forms/LoginForm.vb)
' Replace the existing btnLogin_Click with this:
' ====================================================================

'Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
'    Dim username As String = txtUsername.Text.Trim()
'    Dim password As String = txtPassword.Text.Trim()
'
'    If Not ValidationHelper.IsNotEmpty(username) Then
'        MessageHelper.ShowError("Please enter username")
'        txtUsername.Focus()
'        Return
'    End If
'
'    If Not ValidationHelper.IsNotEmpty(password) Then
'        MessageHelper.ShowError("Please enter password")
'        txtPassword.Focus()
'        Return
'    End If
'
'    Dim user As User = DataStore.FindUser(username, password)
'
'    If user IsNot Nothing Then
'        DataStore.CurrentUser = user
'        MessageHelper.ShowSuccess("Welcome, " & user.FullName & "!")
'        Me.Hide()
'
'        ' SELECTION: Route based on user role
'        If user.Role = "Student" Then
'            ' Show Student Dashboard
'            Dim studentDashboard As New StudentDashboard()
'            studentDashboard.ShowDialog()
'        Else
'            ' Show Admin/Librarian Dashboard
'            Dim mainForm As New MainDashboard()
'            mainForm.ShowDialog()
'        End If
'
'        Me.Close()
'    Else
'        MessageHelper.ShowError("Invalid username or password!")
'        txtPassword.Clear()
'        txtPassword.Focus()
'    End If
'End Sub


' ====================================================================
' STEP 4: Create StudentDashboard.vb (Forms/StudentDashboard.vb)
' Create a NEW file and paste this complete code:
' ====================================================================

Public Class StudentDashboard
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        Me.Text = "Student Portal - Library Management System"
        Me.Size = New Size(800, 600)
        Me.StartPosition = FormStartPosition.CenterScreen
        LoadStudentDashboard()
    End Sub

    ' Load student information
    Private Sub LoadStudentDashboard()
        Dim currentStudent As Member = DataStore.GetMemberByID(DataStore.CurrentUser.MemberID)
        
        If currentStudent IsNot Nothing Then
            lblWelcome.Text = "Welcome, " & currentStudent.FullName & "!"
            lblMemberID.Text = "Member ID: " & currentStudent.MemberID.ToString()
            lblEmail.Text = "Email: " & currentStudent.Email
            lblStatus.Text = "Status: " & currentStudent.Status
            
            LoadBorrowedBooksCount()
            LoadOutstandingFines()
        End If
    End Sub

    ' Count borrowed books
    Private Sub LoadBorrowedBooksCount()
        Dim borrowedCount As Integer = 0
        
        For Each trans As Transaction In DataStore.Transactions
            If trans.MemberID = DataStore.CurrentUser.MemberID And trans.Status = "Issued" Then
                borrowedCount += 1
            End If
        Next
        
        lblBorrowedBooks.Text = "Currently Borrowed: " & borrowedCount.ToString()
    End Sub

    ' Calculate fines
    Private Sub LoadOutstandingFines()
        Dim totalFines As Decimal = 0
        
        For Each trans As Transaction In DataStore.Transactions
            If trans.MemberID = DataStore.CurrentUser.MemberID And trans.IsOverdue() Then
                totalFines += trans.CalculateFine()
            End If
        Next
        
        lblFines.Text = "Outstanding Fines: $" & totalFines.ToString("F2")
        
        If totalFines > 0 Then
            lblFines.ForeColor = Color.Red
        Else
            lblFines.ForeColor = Color.Green
        End If
    End Sub

    ' Browse books
    Private Sub btnBrowseBooks_Click(sender As Object, e As EventArgs) Handles btnBrowseBooks.Click
        MessageHelper.ShowInfo("Browse Books feature - Coming soon!" & vbCrLf & 
                             "This will show all available books you can borrow.")
    End Sub

    ' My books
    Private Sub btnMyBooks_Click(sender As Object, e As EventArgs) Handles btnMyBooks.Click
        Dim myBooksForm As New StudentMyBooksForm()
        myBooksForm.ShowDialog()
        LoadStudentDashboard()
    End Sub

    ' History
    Private Sub btnMyHistory_Click(sender As Object, e As EventArgs) Handles btnMyHistory.Click
        MessageHelper.ShowInfo("Transaction History - Coming soon!" & vbCrLf & 
                             "This will show all your past transactions.")
    End Sub

    ' Profile
    Private Sub btnMyProfile_Click(sender As Object, e As EventArgs) Handles btnMyProfile.Click
        MessageHelper.ShowInfo("My Profile - Coming soon!" & vbCrLf & 
                             "This will show your complete profile information.")
    End Sub

    ' Logout
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MessageHelper.ShowConfirmation("Are you sure you want to logout?") Then
            DataStore.CurrentUser = Nothing
            Me.Close()
        End If
    End Sub

    ' Form closing
    Private Sub StudentDashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim loginForm As New LoginForm()
        loginForm.Show()
    End Sub

    ' Designer
    Private Sub InitializeComponent()
        Me.lblWelcome = New Label()
        Me.lblMemberID = New Label()
        Me.lblEmail = New Label()
        Me.lblStatus = New Label()
        Me.lblBorrowedBooks = New Label()
        Me.lblFines = New Label()
        Me.btnBrowseBooks = New Button()
        Me.btnMyBooks = New Button()
        Me.btnMyHistory = New Button()
        Me.btnMyProfile = New Button()
        Me.btnLogout = New Button()
        Me.grpInfo = New GroupBox()
        Me.grpActions = New GroupBox()
        Me.SuspendLayout()
        
        ' Welcome label
        Me.lblWelcome.Font = New Font("Microsoft Sans Serif", 14.0!, FontStyle.Bold)
        Me.lblWelcome.Location = New Point(30, 20)
        Me.lblWelcome.Size = New Size(500, 30)
        Me.lblWelcome.Text = "Welcome, Student!"
        
        ' Info group
        Me.grpInfo.Location = New Point(30, 60)
        Me.grpInfo.Size = New Size(730, 150)
        Me.grpInfo.Text = "My Information"
        
        Me.lblMemberID.Location = New Point(20, 30)
        Me.lblMemberID.Size = New Size(300, 20)
        Me.lblEmail.Location = New Point(20, 60)
        Me.lblEmail.Size = New Size(400, 20)
        Me.lblStatus.Location = New Point(20, 90)
        Me.lblStatus.Size = New Size(200, 20)
        Me.lblBorrowedBooks.Location = New Point(400, 30)
        Me.lblBorrowedBooks.Size = New Size(250, 20)
        Me.lblFines.Font = New Font("Microsoft Sans Serif", 10.0!, FontStyle.Bold)
        Me.lblFines.Location = New Point(400, 60)
        Me.lblFines.Size = New Size(250, 20)
        
        Me.grpInfo.Controls.AddRange(New Control() {
            lblMemberID, lblEmail, lblStatus, lblBorrowedBooks, lblFines
        })
        
        ' Actions group
        Me.grpActions.Location = New Point(30, 230)
        Me.grpActions.Size = New Size(730, 280)
        Me.grpActions.Text = "Student Features"
        
        Me.btnBrowseBooks.Location = New Point(50, 40)
        Me.btnBrowseBooks.Size = New Size(250, 50)
        Me.btnBrowseBooks.Text = "Browse Available Books"
        Me.btnBrowseBooks.Font = New Font("Microsoft Sans Serif", 11.0!)
        
        Me.btnMyBooks.Location = New Point(350, 40)
        Me.btnMyBooks.Size = New Size(250, 50)
        Me.btnMyBooks.Text = "My Borrowed Books"
        Me.btnMyBooks.Font = New Font("Microsoft Sans Serif", 11.0!)
        
        Me.btnMyHistory.Location = New Point(50, 110)
        Me.btnMyHistory.Size = New Size(250, 50)
        Me.btnMyHistory.Text = "My Transaction History"
        Me.btnMyHistory.Font = New Font("Microsoft Sans Serif", 11.0!)
        
        Me.btnMyProfile.Location = New Point(350, 110)
        Me.btnMyProfile.Size = New Size(250, 50)
        Me.btnMyProfile.Text = "My Profile"
        Me.btnMyProfile.Font = New Font("Microsoft Sans Serif", 11.0!)
        
        Me.btnLogout.Location = New Point(250, 200)
        Me.btnLogout.Size = New Size(150, 40)
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.Font = New Font("Microsoft Sans Serif", 10.0!)
        
        Me.grpActions.Controls.AddRange(New Control() {
            btnBrowseBooks, btnMyBooks, btnMyHistory, btnMyProfile, btnLogout
        })
        
        ' Form
        Me.ClientSize = New Size(800, 550)
        Me.Controls.AddRange(New Control() {lblWelcome, grpInfo, grpActions})
        Me.Name = "StudentDashboard"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents lblWelcome, lblMemberID, lblEmail, lblStatus As Label
    Friend WithEvents lblBorrowedBooks, lblFines As Label
    Friend WithEvents grpInfo, grpActions As GroupBox
    Friend WithEvents btnBrowseBooks, btnMyBooks, btnMyHistory, btnMyProfile, btnLogout As Button
End Class


' ====================================================================
' STEP 5: Create StudentMyBooksForm.vb (Forms/StudentMyBooksForm.vb)
' Create a NEW file and paste this complete code:
' ====================================================================

Public Class StudentMyBooksForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        Me.Text = "My Borrowed Books"
        Me.Size = New Size(900, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
        LoadMyBorrowedBooks()
    End Sub

    ' Load borrowed books
    Private Sub LoadMyBorrowedBooks()
        lvMyBooks.Items.Clear()
        
        For Each trans As Transaction In DataStore.Transactions
            If trans.MemberID = DataStore.CurrentUser.MemberID And trans.Status = "Issued" Then
                Dim item As New ListViewItem(trans.TransactionID.ToString())
                item.SubItems.Add(trans.BookTitle)
                item.SubItems.Add(trans.IssueDate.ToShortDateString())
                item.SubItems.Add(trans.DueDate.ToShortDateString())
                
                Dim daysRemaining As Integer = DateDiff(DateInterval.Day, Date.Today, trans.DueDate)
                item.SubItems.Add(daysRemaining.ToString() & " days")
                
                If trans.IsOverdue() Then
                    item.ForeColor = Color.Red
                    item.SubItems.Add("OVERDUE - $" & trans.CalculateFine().ToString("F2"))
                ElseIf daysRemaining <= 3 Then
                    item.ForeColor = Color.Orange
                    item.SubItems.Add("Due Soon")
                Else
                    item.ForeColor = Color.Green
                    item.SubItems.Add("On Time")
                End If
                
                lvMyBooks.Items.Add(item)
            End If
        Next
        
        lblCount.Text = "Currently Borrowed: " & lvMyBooks.Items.Count.ToString()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadMyBorrowedBooks()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.lvMyBooks = New ListView()
        Me.lblCount = New Label()
        Me.btnRefresh = New Button()
        Me.btnClose = New Button()
        Me.SuspendLayout()
        
        Me.lblCount.Location = New Point(20, 20)
        Me.lblCount.Size = New Size(200, 20)
        Me.lblCount.Font = New Font("Microsoft Sans Serif", 10.0!, FontStyle.Bold)
        
        Me.lvMyBooks.Location = New Point(20, 50)
        Me.lvMyBooks.Size = New Size(850, 380)
        Me.lvMyBooks.View = View.Details
        Me.lvMyBooks.GridLines = True
        Me.lvMyBooks.FullRowSelect = True
        Me.lvMyBooks.Columns.Add("Trans ID", 80)
        Me.lvMyBooks.Columns.Add("Book Title", 300)
        Me.lvMyBooks.Columns.Add("Issue Date", 100)
        Me.lvMyBooks.Columns.Add("Due Date", 100)
        Me.lvMyBooks.Columns.Add("Days Left", 100)
        Me.lvMyBooks.Columns.Add("Status", 150)
        
        Me.btnRefresh.Location = New Point(670, 440)
        Me.btnRefresh.Size = New Size(100, 30)
        Me.btnRefresh.Text = "Refresh"
        
        Me.btnClose.Location = New Point(780, 440)
        Me.btnClose.Size = New Size(90, 30)
        Me.btnClose.Text = "Close"
        
        Me.ClientSize = New Size(900, 490)
        Me.Controls.Add(btnClose)
        Me.Controls.Add(btnRefresh)
        Me.Controls.Add(lblCount)
        Me.Controls.Add(lvMyBooks)
        Me.Name = "StudentMyBooksForm"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents lvMyBooks As ListView
    Friend WithEvents lblCount As Label
    Friend WithEvents btnRefresh, btnClose As Button
End Class


' ====================================================================
' STEP 6: Update Project File
' Add these new forms to LibraryManagementSystem.vbproj in <ItemGroup>:
' ====================================================================

'<Compile Include="Forms\StudentDashboard.vb">
'  <SubType>Form</SubType>
'</Compile>
'<Compile Include="Forms\StudentMyBooksForm.vb">
'  <SubType>Form</SubType>
'</Compile>


' ====================================================================
' STEP 7: Test the Implementation
' ====================================================================

' 1. Build the solution (F6)
' 2. Run the application (F5)
' 3. Login with student credentials:
'    Username: alice
'    Password: student123
' 4. You should see the Student Dashboard!
' 5. Test "My Borrowed Books" button
' 6. Test Logout


' ====================================================================
' NEW LOGIN CREDENTIALS AFTER IMPLEMENTATION:
' ====================================================================

' ADMIN:
'   Username: admin
'   Password: admin123
'
' LIBRARIAN:
'   Username: librarian
'   Password: lib123
'
' STUDENT 1:
'   Username: alice
'   Password: student123
'   Member: Alice Brown
'
' STUDENT 2:
'   Username: bob
'   Password: student123
'   Member: Bob Wilson
'
' STUDENT 3:
'   Username: carol
'   Password: student123
'   Member: Carol Davis
