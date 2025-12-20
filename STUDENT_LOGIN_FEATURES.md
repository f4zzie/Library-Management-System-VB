# ?? Student Login & Features Guide

## Current System vs. Proposed Student Features

---

## ?? **Current System Overview**

### **Existing User Types:**
1. **Admin** - Full system access
2. **Librarian** - Library operations

### **Current Login Credentials:**
```
Admin:
  Username: admin
  Password: admin123
  Access: Full system control

Librarian:
  Username: librarian
  Password: lib123
  Access: Book and member management
```

---

## ?? **Proposed: Adding Student User Type**

### **What Students Should Be Able to Do:**

#### **? Level 1: Basic Student Features (Recommended for Beginners)**

1. **View Their Own Profile**
   - See personal information
   - View membership status
   - Check membership expiry date

2. **Browse Available Books**
   - View all books in library
   - See availability status
   - Search for books by title/author

3. **View Their Borrowed Books**
   - See currently borrowed books
   - Check due dates
   - View overdue status

4. **View Their Transaction History**
   - Past borrowing history
   - Returned books
   - Fine history

5. **Check Outstanding Fines**
   - See current fine amount
   - View fine breakdown
   - Payment status

---

## ??? **Implementation Guide for Students**

### **Step 1: Extend the User Model**

Add a new property to track user type:

```vb
' In Models/User.vb - ADD this property
Public Property UserType As String ' "Admin", "Librarian", or "Student"
Public Property MemberID As Integer ' Link student user to member record
```

### **Step 2: Add Student Users to DataStore**

```vb
' In Data/DataStore.vb - InitializeSampleData()
' Add after existing Users.Add statements:

' Add student users (linked to existing members)
Users.Add(New User(3, "alice", "student123", "Alice Brown", "Student", "alice@email.com") With {.MemberID = 1})
Users.Add(New User(4, "bob", "student123", "Bob Wilson", "Student", "bob@email.com") With {.MemberID = 2})
Users.Add(New User(5, "carol", "student123", "Carol Davis", "Student", "carol@email.com") With {.MemberID = 3})

NextUserID = 6
```

**New Student Login Credentials:**
```
Student 1:
  Username: alice
  Password: student123
  Linked to: Alice Brown (Member ID: 1)

Student 2:
  Username: bob
  Password: student123
  Linked to: Bob Wilson (Member ID: 2)

Student 3:
  Username: carol
  Password: student123
  Linked to: Carol Davis (Member ID: 3)
```

---

### **Step 3: Modify Login Form to Handle Students**

```vb
' In Forms/LoginForm.vb - btnLogin_Click
' After successful authentication, add:

If user.Role = "Student" Then
    ' Show Student Dashboard
    Me.Hide()
    Dim studentDashboard As New StudentDashboard()
    studentDashboard.ShowDialog()
    Me.Close()
ElseIf user.Role = "Admin" Or user.Role = "Librarian" Then
    ' Existing code - show main dashboard
    Me.Hide()
    Dim mainForm As New MainDashboard()
    mainForm.ShowDialog()
    Me.Close()
End If
```

---

### **Step 4: Create Student Dashboard Form**

Create a new file: `Forms/StudentDashboard.vb`

```vb
' ====================================================================
' Student Dashboard Form
' Shows student-specific features and information
' Demonstrates SELECTION and ITERATION structures
' ====================================================================

Public Class StudentDashboard
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        
        Me.Text = "Library Management System - Student Portal"
        Me.Size = New Size(800, 600)
        Me.StartPosition = FormStartPosition.CenterScreen
        
        ' Load student information
        LoadStudentDashboard()
    End Sub

    ' SEQUENTIAL & SELECTION: Load student-specific data
    Private Sub LoadStudentDashboard()
        Dim currentStudent As Member = DataStore.GetMemberByID(DataStore.CurrentUser.MemberID)
        
        If currentStudent IsNot Nothing Then
            ' Display student information
            lblWelcome.Text = "Welcome, " & currentStudent.FullName & "!"
            lblMemberID.Text = "Member ID: " & currentStudent.MemberID.ToString()
            lblEmail.Text = "Email: " & currentStudent.Email
            lblStatus.Text = "Status: " & currentStudent.Status
            
            ' Load borrowed books count
            LoadBorrowedBooksCount()
            
            ' Load outstanding fines
            LoadOutstandingFines()
        End If
    End Sub

    ' ITERATION: Count borrowed books for this student
    Private Sub LoadBorrowedBooksCount()
        Dim borrowedCount As Integer = 0
        
        ' ITERATION: Loop through transactions
        For Each trans As Transaction In DataStore.Transactions
            ' SELECTION: Check if transaction belongs to this student and is active
            If trans.MemberID = DataStore.CurrentUser.MemberID And trans.Status = "Issued" Then
                borrowedCount += 1
            End If
        Next
        
        lblBorrowedBooks.Text = "Currently Borrowed: " & borrowedCount.ToString()
    End Sub

    ' SELECTION & ITERATION: Calculate outstanding fines
    Private Sub LoadOutstandingFines()
        Dim totalFines As Decimal = 0
        
        ' ITERATION: Loop through transactions
        For Each trans As Transaction In DataStore.Transactions
            ' SELECTION: Check if overdue and belongs to this student
            If trans.MemberID = DataStore.CurrentUser.MemberID And trans.IsOverdue() Then
                totalFines += trans.CalculateFine()
            End If
        Next
        
        lblFines.Text = "Outstanding Fines: $" & totalFines.ToString("F2")
        
        ' SELECTION: Change color if fines exist
        If totalFines > 0 Then
            lblFines.ForeColor = Color.Red
        Else
            lblFines.ForeColor = Color.Green
        End If
    End Sub

    ' View all available books
    Private Sub btnBrowseBooks_Click(sender As Object, e As EventArgs) Handles btnBrowseBooks.Click
        Dim browseForm As New StudentBrowseBooksForm()
        browseForm.ShowDialog()
        LoadStudentDashboard() ' Refresh after closing
    End Sub

    ' View borrowed books
    Private Sub btnMyBooks_Click(sender As Object, e As EventArgs) Handles btnMyBooks.Click
        Dim myBooksForm As New StudentMyBooksForm()
        myBooksForm.ShowDialog()
        LoadStudentDashboard() ' Refresh
    End Sub

    ' View transaction history
    Private Sub btnMyHistory_Click(sender As Object, e As EventArgs) Handles btnMyHistory.Click
        Dim historyForm As New StudentHistoryForm()
        historyForm.ShowDialog()
    End Sub

    ' View profile
    Private Sub btnMyProfile_Click(sender As Object, e As EventArgs) Handles btnMyProfile.Click
        Dim profileForm As New StudentProfileForm()
        profileForm.ShowDialog()
        LoadStudentDashboard() ' Refresh
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

    ' Designer code
    Private Sub InitializeComponent()
        ' Create controls
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
        
        ' lblWelcome
        Me.lblWelcome.Font = New Font("Microsoft Sans Serif", 14.0!, FontStyle.Bold)
        Me.lblWelcome.Location = New Point(30, 20)
        Me.lblWelcome.Size = New Size(500, 30)
        Me.lblWelcome.Text = "Welcome, Student!"
        
        ' grpInfo
        Me.grpInfo.Location = New Point(30, 60)
        Me.grpInfo.Size = New Size(730, 150)
        Me.grpInfo.Text = "My Information"
        
        ' Info labels inside group
        Me.lblMemberID.Location = New Point(20, 30)
        Me.lblMemberID.Size = New Size(300, 20)
        Me.lblMemberID.Text = "Member ID: -"
        
        Me.lblEmail.Location = New Point(20, 60)
        Me.lblEmail.Size = New Size(400, 20)
        Me.lblEmail.Text = "Email: -"
        
        Me.lblStatus.Location = New Point(20, 90)
        Me.lblStatus.Size = New Size(200, 20)
        Me.lblStatus.Text = "Status: -"
        
        Me.lblBorrowedBooks.Location = New Point(400, 30)
        Me.lblBorrowedBooks.Size = New Size(250, 20)
        Me.lblBorrowedBooks.Text = "Currently Borrowed: 0"
        
        Me.lblFines.Font = New Font("Microsoft Sans Serif", 10.0!, FontStyle.Bold)
        Me.lblFines.Location = New Point(400, 60)
        Me.lblFines.Size = New Size(250, 20)
        Me.lblFines.Text = "Outstanding Fines: $0.00"
        
        Me.grpInfo.Controls.AddRange(New Control() {
            lblMemberID, lblEmail, lblStatus, lblBorrowedBooks, lblFines
        })
        
        ' grpActions
        Me.grpActions.Location = New Point(30, 230)
        Me.grpActions.Size = New Size(730, 280)
        Me.grpActions.Text = "Student Features"
        
        ' Buttons
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
        Me.Text = "Student Portal"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents lblWelcome, lblMemberID, lblEmail, lblStatus As Label
    Friend WithEvents lblBorrowedBooks, lblFines As Label
    Friend WithEvents grpInfo, grpActions As GroupBox
    Friend WithEvents btnBrowseBooks, btnMyBooks, btnMyHistory, btnMyProfile, btnLogout As Button
End Class
```

---

### **Step 5: Create Student-Specific Forms**

#### **5A. Student Browse Books Form**

```vb
' Forms/StudentBrowseBooksForm.vb
Public Class StudentBrowseBooksForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        LoadAvailableBooks()
    End Sub

    ' ITERATION: Load only available books
    Private Sub LoadAvailableBooks()
        lvBooks.Items.Clear()
        
        ' ITERATION: Loop through books
        For Each book As Book In DataStore.Books
            ' SELECTION: Only show available books
            If book.IsAvailable() Then
                Dim item As New ListViewItem(book.BookID.ToString())
                item.SubItems.Add(book.Title)
                item.SubItems.Add(book.Author)
                item.SubItems.Add(book.Category)
                item.SubItems.Add(book.AvailableQuantity.ToString())
                
                ' Color code availability
                If book.AvailableQuantity > 3 Then
                    item.ForeColor = Color.Green
                ElseIf book.AvailableQuantity > 0 Then
                    item.ForeColor = Color.Orange
                End If
                
                lvBooks.Items.Add(item)
            End If
        Next
        
        lblTotal.Text = "Available Books: " & lvBooks.Items.Count.ToString()
    End Sub

    ' Search functionality
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchTerm As String = txtSearch.Text.Trim()
        
        If String.IsNullOrEmpty(searchTerm) Then
            LoadAvailableBooks()
            Return
        End If
        
        lvBooks.Items.Clear()
        Dim results As List(Of Book) = DataStore.SearchBooks(searchTerm)
        
        For Each book As Book In results
            If book.IsAvailable() Then
                Dim item As New ListViewItem(book.BookID.ToString())
                item.SubItems.Add(book.Title)
                item.SubItems.Add(book.Author)
                item.SubItems.Add(book.Category)
                item.SubItems.Add(book.AvailableQuantity.ToString())
                lvBooks.Items.Add(item)
            End If
        Next
        
        lblTotal.Text = "Search Results: " & lvBooks.Items.Count.ToString()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Designer code here...
End Class
```

#### **5B. Student My Books Form**

```vb
' Forms/StudentMyBooksForm.vb
Public Class StudentMyBooksForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        LoadMyBorrowedBooks()
    End Sub

    ' ITERATION & SELECTION: Load books borrowed by this student
    Private Sub LoadMyBorrowedBooks()
        lvMyBooks.Items.Clear()
        
        ' ITERATION: Loop through transactions
        For Each trans As Transaction In DataStore.Transactions
            ' SELECTION: Filter by current student and issued status
            If trans.MemberID = DataStore.CurrentUser.MemberID And trans.Status = "Issued" Then
                Dim item As New ListViewItem(trans.TransactionID.ToString())
                item.SubItems.Add(trans.BookTitle)
                item.SubItems.Add(trans.IssueDate.ToShortDateString())
                item.SubItems.Add(trans.DueDate.ToShortDateString())
                
                ' Calculate days remaining
                Dim daysRemaining As Integer = DateDiff(DateInterval.Day, Date.Today, trans.DueDate)
                item.SubItems.Add(daysRemaining.ToString() & " days")
                
                ' SELECTION: Color code based on due date
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

    ' Designer code here...
End Class
```

#### **5C. Student History Form**

```vb
' Forms/StudentHistoryForm.vb
Public Class StudentHistoryForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        LoadMyHistory()
    End Sub

    ' ITERATION & SELECTION: Load transaction history for this student
    Private Sub LoadMyHistory()
        lvHistory.Items.Clear()
        Dim totalFinesPaid As Decimal = 0
        
        ' ITERATION: Loop through all transactions
        For Each trans As Transaction In DataStore.Transactions
            ' SELECTION: Filter by current student
            If trans.MemberID = DataStore.CurrentUser.MemberID Then
                Dim item As New ListViewItem(trans.TransactionID.ToString())
                item.SubItems.Add(trans.BookTitle)
                item.SubItems.Add(trans.IssueDate.ToShortDateString())
                item.SubItems.Add(trans.DueDate.ToShortDateString())
                item.SubItems.Add(If(trans.ReturnDate = Nothing, "-", trans.ReturnDate.ToShortDateString()))
                item.SubItems.Add(trans.Status)
                item.SubItems.Add("$" & trans.Fine.ToString("F2"))
                
                ' SELECTION: Color code by status
                If trans.Status = "Returned" Then
                    item.ForeColor = Color.Gray
                ElseIf trans.IsOverdue() Then
                    item.ForeColor = Color.Red
                Else
                    item.ForeColor = Color.Green
                End If
                
                lvHistory.Items.Add(item)
                totalFinesPaid += trans.Fine
            End If
        Next
        
        lblTotal.Text = "Total Transactions: " & lvHistory.Items.Count.ToString()
        lblFines.Text = "Total Fines Paid: $" & totalFinesPaid.ToString("F2")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Designer code here...
End Class
```

#### **5D. Student Profile Form**

```vb
' Forms/StudentProfileForm.vb
Public Class StudentProfileForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        LoadStudentProfile()
    End Sub

    ' SELECTION: Load student profile information
    Private Sub LoadStudentProfile()
        Dim student As Member = DataStore.GetMemberByID(DataStore.CurrentUser.MemberID)
        
        If student IsNot Nothing Then
            txtName.Text = student.FullName
            txtEmail.Text = student.Email
            txtPhone.Text = student.PhoneNumber
            txtAddress.Text = student.Address
            lblMemberSince.Text = "Member Since: " & student.MembershipDate.ToShortDateString()
            lblStatus.Text = "Status: " & student.Status
            
            ' SELECTION: Check if membership is expired
            If student.IsMembershipExpired() Then
                lblStatus.ForeColor = Color.Red
                lblStatus.Text = "Status: EXPIRED - Please renew"
            Else
                lblStatus.ForeColor = Color.Green
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Designer code here...
End Class
```

---

## ?? **Student Features Summary**

### **What Students CAN Do:**
? Login with student credentials  
? View personal dashboard with statistics  
? Browse all available books  
? Search for books  
? View currently borrowed books  
? See due dates and overdue status  
? Check outstanding fines  
? View complete transaction history  
? View personal profile  
? Logout securely  

### **What Students CANNOT Do:**
? Issue books themselves (must go to librarian)  
? Return books themselves (must go to librarian)  
? Add/edit/delete books  
? Add/edit/delete members  
? Access admin functions  
? View other students' information  
? Modify transaction records  

---

## ?? **Control Structures in Student Features**

### **SEQUENTIAL Examples:**
```vb
' Loading dashboard in order
LoadStudentInformation()    ' Step 1
LoadBorrowedBooks()        ' Step 2
LoadOutstandingFines()     ' Step 3
```

### **SELECTION Examples:**
```vb
' Checking user role
If user.Role = "Student" Then
    ShowStudentDashboard()
Else
    ShowStaffDashboard()
End If

' Color coding due dates
If IsOverdue() Then
    Color = Red
ElseIf DaysRemaining <= 3 Then
    Color = Orange
Else
    Color = Green
End If
```

### **ITERATION Examples:**
```vb
' Finding student's books
For Each transaction In Transactions
    If transaction.MemberID = CurrentStudentID Then
        DisplayTransaction(transaction)
    End If
Next
```

---

## ?? **Implementation Steps for Students**

### **Beginner Level (2-3 hours):**
1. Add UserType property to User model
2. Create 3 student users in DataStore
3. Modify LoginForm to check user type
4. Create basic StudentDashboard form
5. Test login with student credentials

### **Intermediate Level (5-7 hours):**
6. Create StudentBrowseBooksForm
7. Create StudentMyBooksForm
8. Add search functionality
9. Implement color coding
10. Test all student features

### **Advanced Level (10+ hours):**
11. Create StudentHistoryForm
12. Create StudentProfileForm
13. Add statistics and calculations
14. Implement fine tracking
15. Add comprehensive error handling

---

## ?? **Updated Login Credentials**

After implementation, you'll have:

```
=== ADMIN ===
Username: admin
Password: admin123
Role: Administrator
Access: Full System

=== LIBRARIAN ===
Username: librarian
Password: lib123
Role: Librarian
Access: Book & Member Management

=== STUDENTS ===
Username: alice
Password: student123
Role: Student
Member: Alice Brown
Access: Personal Dashboard

Username: bob
Password: student123
Role: Student
Member: Bob Wilson
Access: Personal Dashboard

Username: carol
Password: student123
Role: Student
Member: Carol Davis
Access: Personal Dashboard
```

---

## ?? **Additional Student Features (Extensions)**

### **Easy Additions:**
- Book reservation system
- Email notifications for due dates
- Wish list / favorite books
- Reading statistics

### **Medium Additions:**
- Book reviews and ratings
- Recommend books to friends
- Request new books
- E-book downloads

### **Advanced Additions:**
- Mobile app for students
- Barcode scanner integration
- Library map (find book location)
- Study room booking

---

## ?? **Learning Outcomes**

By implementing student features, you'll learn:
- ? Role-based access control
- ? User authentication
- ? Filtering data by user
- ? Dashboard design
- ? Permission management
- ? Multi-user systems
- ? Data security basics

---

## ?? **Testing Checklist**

- [ ] Student can login successfully
- [ ] Student sees personalized dashboard
- [ ] Student can browse books
- [ ] Student can search books
- [ ] Student sees only their borrowed books
- [ ] Overdue books show in red
- [ ] Fines calculate correctly
- [ ] Transaction history is accurate
- [ ] Profile displays correctly
- [ ] Student cannot access admin features
- [ ] Logout works properly

---

**Ready to implement? Start with Step 1 and work your way through!**

*Remember: Each step builds on the previous one. Test thoroughly before moving to the next step!* ??
