# Library Management System - README

## Project Overview
This is a comprehensive **Library Management System** developed in **Visual Basic .NET** using Windows Forms. The system provides a complete solution for managing library operations including book cataloging, member registration, book issuing, and return processing with automatic fine calculation.

---

## Table of Contents
1. [System Architecture](#system-architecture)
2. [Algorithms Used](#algorithms-used)
3. [Error Handling Implementation](#error-handling-implementation)
4. [Control Structures Implementation](#control-structures-implementation)
5. [Data Storage](#data-storage)
6. [Features](#features)
7. [How to Use](#how-to-use)

---

## System Architecture

The application follows a modular design with the following components:

- **Form1.vb** - Login/Authentication Module
- **Dashboard.vb** - Main Navigation Hub
- **AddBooks.vb** - Book Management (Add)
- **ViewBooks.vb** - Book Management (View/Search)
- **AddMembers.vb** - Member Management (Add)
- **ViewMembers.vb** - Member Management (View/Search)
- **IssueBooks.vb** - Book Issuing Module
- **ReturnBooks.vb** - Book Return & Fine Calculation Module

---

## Algorithms Used

### 1. **Authentication Algorithm** (Form1.vb)
**Purpose:** Secure user login with credential validation

**Source File:** `WindowsApp1/Form1.vb`  
**Function:** `ValidateLogin(username As String, password As String) As Boolean`  
**Lines:** 107-140

**Algorithm Steps:**
```
1. START
2. User enters username and password
3. VALIDATE input is not empty or placeholder text
4. IF valid THEN
   5. Read default credentials (hardcoded)
   6. Compare username and password
   7. IF match found THEN
      8. Success ? Open Dashboard
   9. ELSE
      10. Check users.txt file (optional external credentials)
      11. FOR each line in file DO
          12. Split line by delimiter "|"
          13. Compare username and password
          14. IF match found THEN
              15. Success ? Open Dashboard
              16. EXIT LOOP
          END IF
      END FOR
   END IF
8. IF no match THEN
   9. Display error message
   10. Clear password field
END IF
11. END
```

**Actual Code Implementation:**
```vb
' Function to validate login credentials
Private Function ValidateLogin(username As String, password As String) As Boolean
    Try
        ' Check against default credentials
        If username.ToLower() = DEFAULT_USERNAME.ToLower() And password = DEFAULT_PASSWORD Then
            Return True
        End If

        ' Check against users file if it exists
        Dim usersFile As String = "users.txt"
        If File.Exists(usersFile) Then
            Dim lines() As String = File.ReadAllLines(usersFile)
            For Each line As String In lines
                Dim parts() As String = line.Split("|"c)
                If parts.Length >= 2 Then
                    If parts(0).ToLower() = username.ToLower() And parts(1) = password Then
                        Return True
                    End If
                End If
            Next
        End If

        Return False

    Catch ex As Exception
        MessageBox.Show("Error reading user credentials: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return False
    End Try
End Function

```

### 2. **User Login Authentication** (Form1.vb)
**Purpose:** Authenticate user login and navigate to the dashboard

**Source File:** `WindowsApp1/Form1.vb`  
**Entry Point:** `Button1_Click` (Lines 74-103)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: UserLogin
INPUT: username, password
OUTPUT: Login success/failure, Dashboard access

1. START
2. Display Login Form
3. User enters username in TextBox2
4. User enters password in TextBox1
5. Optional: User checks "Show Password" checkbox
   5.1 IF CheckBox1.Checked THEN
       5.2 TextBox1.UseSystemPasswordChar = False
   5.3 ELSE
       5.4 TextBox1.UseSystemPasswordChar = True
   5.5 END IF
6. User clicks "Login" button
7. BEGIN Try-Catch Block
8. VALIDATE username field
   8.1 IF username is empty OR username = "Username" (placeholder) THEN
       8.2 DISPLAY "Please enter your username"
       8.3 SET focus to username field
       8.4 RETURN (exit algorithm)
   8.5 END IF
9. VALIDATE password field
   9.1 IF password is empty OR password = "Password" (placeholder) THEN
       9.2 DISPLAY "Please enter your password"
       9.3 SET focus to password field
       9.4 RETURN (exit algorithm)
   9.5 END IF
10. CALL ValidateLogin(username, password)
    10.1 BEGIN ValidateLogin Function
    10.2 Compare with default credentials (admin/admin123)
         10.2.1 IF username matches DEFAULT_USERNAME AND password matches DEFAULT_PASSWORD THEN
                10.2.2 RETURN True
         10.2.3 END IF
    10.3 CHECK if users.txt file exists
         10.3.1 IF File.Exists("users.txt") THEN
                10.3.2 READ all lines from file
                10.3.3 FOR EACH line in file DO
                       10.3.3.1 SPLIT line by "|" delimiter
                       10.3.3.2 IF username matches parts[0] AND password matches parts[1] THEN
                                10.3.3.3 RETURN True
                       10.3.3.4 END IF
                10.3.4 END FOR
         10.3.5 END IF
    10.4 RETURN False (no match found)
    10.5 END ValidateLogin Function
11. IF ValidateLogin returns True THEN
    11.1 DISPLAY "Login Successful! Welcome to Library Management System"
    11.2 CREATE new Dashboard object
    11.3 HIDE current login form
    11.4 SHOW Dashboard form
    11.5 CLOSE login form
12. ELSE
    12.1 DISPLAY "Invalid username or password"
    12.2 CLEAR password field
    12.3 RESET password masking
    12.4 SET focus to password field
13. END IF
14. CATCH Exception
    14.1 DISPLAY "An error occurred during login: " + error message
15. END Try-Catch Block
16. END
```

**Code Reference:**
```vb
' Form1.vb - Lines 74-103 (Button Click Handler)
' Form1.vb - Lines 107-140 (ValidateLogin Function)
```

**Time Complexity:** O(n) where n = number of users in file  



---

### FEATURE 2: Add New Book

**Source File:** `WindowsApp1/AddBooks.vb`  
**Entry Point:** `btnAdd_Click` (Lines 38-65)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: AddNewBook
INPUT: Book details (title, author, ISBN, category, publisher, year, copies, shelf location)
OUTPUT: New book record in books.txt

1. START
2. User navigates: Dashboard ? Click "Add Books" button
3. OPEN AddBooks form
4. AUTO-GENERATE next Book ID
   4.1 CALL GenerateNextBookID()
       4.1.1 IF books.txt exists THEN
             4.1.2 READ all lines from file
             4.1.3 GET last line (most recent book)
             4.1.4 EXTRACT Book ID from first field
             4.1.5 REMOVE "BK" prefix
             4.1.6 CONVERT to integer
             4.1.7 INCREMENT by 1
             4.1.8 FORMAT as "BK" + 4-digit number (e.g., BK0005)
             4.1.9 RETURN formatted ID
       4.1.10 ELSE
             4.1.11 RETURN "BK0001" (default first ID)
       4.1.12 END IF
5. DISPLAY generated Book ID in txtBookID (read-only)
6. SET dtpAddedDate to current date
7. User fills in book details:
   7.1 Enter Title in txtTitle
   7.2 Enter Author in txtAuthor
   7.3 Enter ISBN in txtISBN (numeric only)
   7.4 Enter Publisher in txtPublisher
   7.5 Enter Publish Year in txtPublishYear (numeric only)
   7.6 Enter Total Copies in txtTotalCopies (numeric only)
   7.7 Enter Shelf Location in txtShelfLocation
   7.8 Select Category from cmbCategory dropdown
8. User clicks "Add" button
9. BEGIN Try-Catch Block
10. CALL ValidateInputs()
    10.1 CHECK if Title is empty
         10.1.1 IF empty THEN DISPLAY error, RETURN False
    10.2 CHECK if Author is empty
         10.2.1 IF empty THEN DISPLAY error, RETURN False
    10.3 CHECK if ISBN is empty
         10.3.1 IF empty THEN DISPLAY error, RETURN False
    10.4 VALIDATE ISBN is numeric
         10.4.1 IF NOT numeric THEN DISPLAY error, RETURN False
    10.5 CHECK if Publisher is empty
         10.5.1 IF empty THEN DISPLAY error, RETURN False
    10.6 CHECK if Publish Year is empty
         10.6.1 IF empty THEN DISPLAY error, RETURN False
    10.7 VALIDATE Year is numeric and within range (1800 to current year + 1)
         10.7.1 IF NOT valid THEN DISPLAY error, RETURN False
    10.8 CHECK if Total Copies is empty
         10.8.1 IF empty THEN DISPLAY error, RETURN False
    10.9 VALIDATE Total Copies is numeric and positive
         10.9.1 IF NOT valid THEN DISPLAY error, RETURN False
    10.10 CHECK if Shelf Location is empty
          10.10.1 IF empty THEN DISPLAY error, RETURN False
    10.11 CHECK if Category is selected
          10.11.1 IF not selected THEN DISPLAY error, RETURN False
    10.12 RETURN True (all validations passed)
11. IF ValidateInputs returns False THEN
    11.1 EXIT algorithm (validation failed)
12. END IF
13. PARSE Total Copies to integer
14. CREATE book record string:
    Format: "BookID|Title|Author|ISBN|Category|Publisher|PublishYear|TotalCopies|AvailableCopies|ShelfLocation|AddedDate"
    14.1 BookID = generated ID
    14.2 Title = txtTitle.Text (trimmed)
    14.3 Author = txtAuthor.Text (trimmed)
    14.4 ISBN = txtISBN.Text (trimmed)
    14.5 Category = cmbCategory.Text (trimmed)
    14.6 Publisher = txtPublisher.Text (trimmed)
    14.7 PublishYear = txtPublishYear.Text (trimmed)
    14.8 TotalCopies = parsed copies value
    14.9 AvailableCopies = TotalCopies (initially all copies available)
    14.10 ShelfLocation = txtShelfLocation.Text (trimmed)
    14.11 AddedDate = dtpAddedDate.Value formatted as "yyyy-MM-dd"
15. APPEND book record to books.txt file
    15.1 OPEN books.txt in append mode
    15.2 WRITE bookRecord + newline
    15.3 CLOSE file
16. DISPLAY "Book added successfully! Book ID: [ID]"
17. CALL ClearForm()
    17.1 CLEAR all text fields
    17.2 RESET category dropdown
    17.3 RESET date picker to current date
    17.4 GENERATE next Book ID
    17.5 SET focus to Title field
18. CATCH Exception
    18.1 DISPLAY "Error adding book: " + error message
19. END Try-Catch Block
20. END
```

**Code Reference:**
```vb
' AddBooks.vb - Lines 38-65 (btnAdd_Click)
' AddBooks.vb - Lines 66-148 (ValidateInputs)
' AddBooks.vb - Lines 18-35 (GenerateNextBookID)
' AddBooks.vb - Lines 150-158 (ClearForm)
```

**Real-Time Input Validation:**
- ISBN: Lines 202-208 (txtISBN_KeyPress) - blocks non-numeric input
- Year: Lines 195-201 (txtPublishYear_KeyPress) - blocks non-numeric input
- Copies: Lines 188-194 (txtTotalCopies_KeyPress) - blocks non-numeric input

**Time Complexity:** O(n) for ID generation (reading file), O(1) for adding record
**Space Complexity:** O(1) for new record

---

### FEATURE 3: Add New Member

**Source File:** `WindowsApp1/AddMembers.vb`  
**Entry Point:** `btnAdd_Click` (Lines 38-66)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: AddNewMember
INPUT: Member details (name, student ID, gender, address, email, phone, member type)
OUTPUT: New member record in members.txt

1. START
2. User navigates: Dashboard ? Click "Add Members" button
3. OPEN AddMembers form
4. AUTO-GENERATE next Member ID
   4.1 CALL GenerateNextMemberID()
       4.1.1 IF members.txt exists THEN
             4.1.2 READ all lines from file
             4.1.3 GET last line (most recent member)
             4.1.4 EXTRACT Member ID from first field
             4.1.5 REMOVE "MEM" prefix
             4.1.6 CONVERT to integer
             4.1.7 INCREMENT by 1
             4.1.8 FORMAT as "MEM" + 4-digit number (e.g., MEM0005)
             4.1.9 RETURN formatted ID
       4.1.10 ELSE
             4.1.11 RETURN "MEM0001" (default first ID)
       4.1.12 END IF
5. DISPLAY generated Member ID in txtMemberID (read-only)
6. SET dtpRegistration to current date
7. User fills in member details:
   7.1 Enter Name in txtName
   7.2 Enter Student ID in txtStudentID
   7.3 Select Gender from cmbGender dropdown
   7.4 Enter Address in txtAddress (multi-line)
   7.5 Enter Email in txtEmail
   7.6 Enter Phone in txtPhone (numeric only)
   7.7 Select Member Type from cmbMemberType dropdown
8. User clicks "Add" button
9. BEGIN Try-Catch Block
10. CALL ValidateInputs()
    10.1 CHECK if Name is empty
         10.1.1 IF empty THEN DISPLAY error, RETURN False
    10.2 CHECK if Student ID is empty
         10.2.1 IF empty THEN DISPLAY error, RETURN False
    10.3 CHECK if Gender is selected
         10.3.1 IF not selected THEN DISPLAY error, RETURN False
    10.4 CHECK if Address is empty
         10.4.1 IF empty THEN DISPLAY error, RETURN False
    10.5 CHECK if Email is empty
         10.5.1 IF empty THEN DISPLAY error, RETURN False
    10.6 VALIDATE Email format
         10.6.1 CALL IsValidEmail(email)
                10.6.1.1 TRY create MailAddress object
                10.6.1.2 IF successful THEN RETURN True
                10.6.1.3 CATCH exception THEN RETURN False
         10.6.2 IF NOT valid THEN DISPLAY error, RETURN False
    10.7 CHECK if Phone is empty
         10.7.1 IF empty THEN DISPLAY error, RETURN False
    10.8 VALIDATE Phone is numeric
         10.8.1 IF NOT numeric THEN DISPLAY error, RETURN False
    10.9 CHECK if Member Type is selected
         10.9.1 IF not selected THEN DISPLAY error, RETURN False
    10.10 RETURN True (all validations passed)
11. IF ValidateInputs returns False THEN
    11.1 EXIT algorithm (validation failed)
12. END IF
13. CREATE member record string:
    Format: "MemberID|Name|StudentID|Gender|Address|Email|Phone|MemberType|RegistrationDate"
    13.1 MemberID = generated ID
    13.2 Name = txtName.Text (trimmed)
    13.3 StudentID = txtStudentID.Text (trimmed)
    13.4 Gender = cmbGender.Text (trimmed)
    13.5 Address = txtAddress.Text (trimmed, line breaks replaced with spaces)
    13.6 Email = txtEmail.Text (trimmed)
    13.7 Phone = txtPhone.Text (trimmed)
    13.8 MemberType = cmbMemberType.Text (trimmed)
    13.9 RegistrationDate = dtpRegistration.Value formatted as "yyyy-MM-dd"
14. APPEND member record to members.txt file
    14.1 OPEN members.txt in append mode
    14.2 WRITE memberRecord + newline
    14.3 CLOSE file
15. DISPLAY "Member added successfully! Member ID: [ID]"
16. CALL ClearForm()
    16.1 CLEAR all text fields
    16.2 RESET dropdowns
    16.3 RESET date picker to current date
    16.4 GENERATE next Member ID
    16.5 SET focus to Name field
17. CATCH Exception
    17.1 DISPLAY "Error adding member: " + error message
18. END Try-Catch Block
19. END
```

**Code Reference:**
```vb
' AddMembers.vb - Lines 38-66 (btnAdd_Click)
' AddMembers.vb - Lines 68-131 (ValidateInputs)
' AddMembers.vb - Lines 134-141 (IsValidEmail)
' AddMembers.vb - Lines 18-35 (GenerateNextMemberID)
' AddMembers.vb - Lines 143-151 (ClearForm)
```

**Real-Time Input Validation:**
- Phone: Lines 173-179 (txtPhone_KeyPress) - blocks non-numeric input

**Time Complexity:** O(n) for ID generation, O(1) for adding record
**Space Complexity:** O(1) for new record

---

### FEATURE 4: Issue Book (Borrow Book)

**Source File:** `WindowsApp1/IssueBooks.vb`  
**Entry Point:** `btnIssue_Click` (Lines 165-204)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: IssueBook
INPUT: Book selection, Member ID
OUTPUT: Issue record in issued_books.txt, Updated available copies in books.txt

1. START
2. User navigates: Dashboard ? Click "Issue Books" button
3. OPEN IssueBooks form
4. AUTO-GENERATE next Issue ID
   4.1 CALL GenerateNextIssueID()
       4.1.1 IF issued_books.txt exists THEN
             4.1.2 READ all lines from file
             4.1.3 GET last line
             4.1.4 EXTRACT Issue ID from first field
             4.1.5 REMOVE "ISS" prefix
             4.1.6 CONVERT to integer
             4.1.7 INCREMENT by 1
             4.1.8 FORMAT as "ISS" + 4-digit number (e.g., ISS0005)
             4.1.9 RETURN formatted ID
       4.1.10 ELSE
             4.1.11 RETURN "ISS0001" (default first ID)
       4.1.12 END IF
5. SET dtpIssueDate to current date
6. CALCULATE and SET dtpDueDate = current date + 14 days
7. LOAD available books into dropdown
   7.1 CALL LoadAvailableBooks()
       7.1.1 CLEAR cmbBookSelect dropdown
       7.1.2 IF books.txt exists THEN
             7.1.3 READ all lines from file
             7.1.4 FOR EACH line in file DO
                   7.1.4.1 SPLIT line by "|" delimiter
                   7.1.4.2 EXTRACT AvailableCopies (field 8)
                   7.1.4.3 IF AvailableCopies > 0 THEN
                           7.1.4.4 EXTRACT BookID, Title
                           7.1.4.5 FORMAT display text: "BookID - Title (Available: X)"
                           7.1.4.6 ADD to dropdown
                   7.1.4.7 END IF
             7.1.5 END FOR
       7.1.6 END IF
8. User can optionally SEARCH for book
   8.1 User types in txtSearchBook
   8.2 TRIGGER txtSearchBook_TextChanged event
       8.2.1 GET search text (convert to lowercase)
       8.2.2 CLEAR dropdown
       8.2.3 RELOAD books matching search criteria
       8.2.4 FOR EACH book with available copies DO
             8.2.4.1 IF search text is empty OR book info contains search text THEN
                     8.2.4.2 ADD to dropdown
             8.2.4.3 END IF
       8.2.5 END FOR
9. User selects book from cmbBookSelect dropdown
   9.1 TRIGGER cmbBookSelect_SelectedIndexChanged event
   9.2 EXTRACT Book ID from selected text
   9.3 DISPLAY Book ID in txtBookID
10. User enters Member ID in txtMemberID
11. User clicks "Verify Member" button
    11.1 CALL btnVerifyMember_Click
         11.1.1 IF Member ID is empty THEN
                11.1.2 DISPLAY "Please enter a Member ID"
                11.1.3 RETURN
         11.1.4 END IF
         11.1.5 CALL GetMemberInfo(memberID)
                11.1.5.1 IF members.txt exists THEN
                         11.1.5.2 READ all lines from file
                         11.1.5.3 FOR EACH line in file DO
                                  11.1.5.3.1 SPLIT line by "|" delimiter
                                  11.1.5.3.2 IF Member ID matches parts[0] THEN
                                             11.1.5.3.3 RETURN entire line
                                  11.1.5.3.4 END IF
                         11.1.5.4 END FOR
                11.1.5.5 END IF
                11.1.5.6 RETURN empty string (not found)
         11.1.6 IF member found THEN
                11.1.7 EXTRACT member name (field 1)
                11.1.8 DISPLAY member name in txtMemberName
                11.1.9 DISPLAY "Member verified successfully!"
         11.1.10 ELSE
                11.1.11 DISPLAY "Member ID not found"
                11.1.12 CLEAR txtMemberName
         11.1.13 END IF
12. User clicks "Issue" button
13. BEGIN Try-Catch Block
14. CALL ValidateInputs()
    14.1 CHECK if book is selected (cmbBookSelect.SelectedIndex >= 0)
         14.1.1 IF not selected THEN DISPLAY error, RETURN False
    14.2 CHECK if Book ID is loaded
         14.2.1 IF empty THEN DISPLAY error, RETURN False
    14.3 CHECK if Member ID is entered
         14.3.1 IF empty THEN DISPLAY error, RETURN False
    14.4 CHECK if Member Name is displayed (verified)
         14.4.1 IF empty THEN DISPLAY error, RETURN False
    14.5 RETURN True (all validations passed)
15. IF ValidateInputs returns False THEN
    15.1 EXIT algorithm (validation failed)
16. END IF
17. CREATE issue record string:
    Format: "IssueID|BookID|MemberID|IssueDate|DueDate|BookTitle|Status"
    17.1 IssueID = generated ID
    17.2 BookID = txtBookID.Text (trimmed)
    17.3 MemberID = txtMemberID.Text (trimmed)
    17.4 IssueDate = dtpIssueDate.Value formatted as "yyyy-MM-dd"
    17.5 DueDate = dtpDueDate.Value formatted as "yyyy-MM-dd"
    17.6 BookTitle = extracted from dropdown selection
    17.7 Status = "Active"
18. APPEND issue record to issued_books.txt file
    18.1 OPEN issued_books.txt in append mode
    18.2 WRITE issueRecord + newline
    18.3 CLOSE file
19. UPDATE book available copies
    19.1 CALL UpdateBookQuantity(bookID, -1)
         19.1.1 IF books.txt exists THEN
                19.1.2 READ all lines from file
                19.1.3 CREATE empty list for updated lines
                19.1.4 FOR EACH line in file DO
                       19.1.4.1 SPLIT line by "|" delimiter
                       19.1.4.2 IF Book ID matches target THEN
                                19.1.4.3 EXTRACT current AvailableCopies (field 8)
                                19.1.4.4 CALCULATE new AvailableCopies = current + change (-1)
                                19.1.4.5 RECONSTRUCT line with new AvailableCopies
                                19.1.4.6 ADD to updated list
                       19.1.4.7 ELSE
                                19.1.4.8 ADD original line to updated list
                       19.1.4.9 END IF
                19.1.5 END FOR
                19.1.6 WRITE all updated lines back to books.txt
         19.1.7 END IF
20. DISPLAY "Book issued successfully! Issue ID: [ID], Due Date: [Date]"
21. CALL ClearForm()
    21.1 CLEAR search box and dropdowns
    21.2 CLEAR Book ID and Member fields
    21.3 RESET dates to defaults
    21.4 GENERATE next Issue ID
    21.5 RELOAD available books (refresh list)
22. CATCH Exception
    22.1 DISPLAY "Error issuing book: " + error message
    22.2 ROLLBACK: Book quantity not updated (transaction incomplete)
23. END Try-Catch Block
24. END
```

**Code Reference:**
```vb
' IssueBooks.vb - Lines 165-204 (btnIssue_Click)
' IssueBooks.vb - Lines 209-229 (ValidateInputs)
' IssueBooks.vb - Lines 92-110 (GenerateNextIssueID)
' IssueBooks.vb - Lines 25-47 (LoadAvailableBooks)
' IssueBooks.vb - Lines 61-89 (txtSearchBook_TextChanged)
' IssueBooks.vb - Lines 114-139 (btnVerifyMember_Click)
' IssueBooks.vb - Lines 143-162 (GetMemberInfo)
' IssueBooks.vb - Lines 232-256 (UpdateBookQuantity)
```

**Time Complexity:** 
- Book loading: O(n) where n = number of books
- Member verification: O(m) where m = number of members
- Book update: O(n) for rewriting file

**Space Complexity:** O(n) for storing file lines in memory

---

### FEATURE 5: Return Book

**Source File:** `WindowsApp1/ReturnBooks.vb`  
**Entry Point:** `btnReturn_Click` (Lines 70-108)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: ReturnBook
INPUT: Selected issued book, Return date
OUTPUT: Updated issue record in issued_books.txt, Updated available copies in books.txt, Fine calculation

1. START
2. User navigates: Dashboard ? Click "Return Books" button
3. OPEN ReturnBooks form
4. SET dtpReturnDate to current date
5. SETUP DataGridView columns
   5.1 CALL SetupDataGridView()
       5.1.1 CLEAR existing columns
       5.1.2 ADD columns: IssueID, BookID, MemberID, IssueDate, DueDate, BookTitle
       5.1.3 SET column widths
       5.1.4 SET properties (read-only, no add rows, full row select)
6. LOAD issued books (active only)
   6.1 CALL LoadIssuedBooks()
       6.1.1 CLEAR DataGridView rows
       6.1.2 IF issued_books.txt does not exist THEN
             6.1.3 DISPLAY "Total Issued Books: 0"
             6.1.4 RETURN
       6.1.5 END IF
       6.1.6 READ all lines from file
       6.1.7 INITIALIZE count = 0
       6.1.8 FOR EACH line in file DO
             6.1.8.1 SPLIT line by "|" delimiter
             6.1.8.2 IF Status (field 6) = "Active" THEN
                     6.1.8.3 ADD row to DataGridView with issue details
                     6.1.8.4 INCREMENT count
             6.1.8.5 END IF
       6.1.9 END FOR
       6.1.10 DISPLAY "Total Issued Books: " + count
7. User selects an issued book from DataGridView (clicks row)
8. TRIGGER dgvIssuedBooks_SelectionChanged event
   8.1 IF a row is selected THEN
       8.2 EXTRACT DueDate from selected row (column 4)
       8.3 GET current return date from dtpReturnDate
       8.4 CALL CalculateFine(dueDate, returnDate)
           8.4.1 DEFINE FINE_PER_DAY = $0.50
           8.4.2 IF returnDate > dueDate THEN
                 8.4.3 CALCULATE daysLate = returnDate - dueDate
                 8.4.4 CALCULATE fine = daysLate × FINE_PER_DAY
                 8.4.5 RETURN fine
           8.4.6 ELSE
                 8.4.7 RETURN 0 (no fine)
           8.4.8 END IF
       8.5 IF fine > 0 THEN
           8.6 DISPLAY "Late Return - Fine: $X.XX (Y days late)" in red color
       8.7 ELSE
           8.8 DISPLAY "On Time - No Fine" in green color
       8.9 END IF
   8.10 END IF
9. User can change return date in dtpReturnDate (recalculates fine)
10. User clicks "Return" button
11. BEGIN Try-Catch Block
12. CHECK if a row is selected
    12.1 IF no row selected THEN
         12.2 DISPLAY "Please select a book to return"
         12.3 RETURN (exit algorithm)
    12.4 END IF
13. EXTRACT issue details from selected row:
    13.1 IssueID = column 0
    13.2 BookID = column 1
    13.3 DueDate = column 4 (parse to Date)
14. CALCULATE fine
    14.1 CALL CalculateFine(dueDate, returnDate)
15. BUILD confirmation message
    15.1 message = "Book returned successfully!"
    15.2 IF fine > 0 THEN
         15.3 APPEND "Late Return Fine: $" + fine
         15.4 APPEND "Days Late: " + daysLate
    15.5 END IF
16. DISPLAY confirmation dialog with message
    16.1 IF user clicks "No" THEN
         16.2 RETURN (cancel operation)
    16.3 END IF
17. UPDATE issued book record
    17.1 CALL UpdateIssuedBook(issueID, returnDate, fine)
         17.1.1 IF issued_books.txt exists THEN
                17.1.2 READ all lines from file
                17.1.3 CREATE empty list for updated lines
                17.1.4 FOR EACH line in file DO
                       17.1.4.1 SPLIT line by "|" delimiter
                       17.1.4.2 IF Issue ID matches target THEN
                                17.1.4.3 CREATE new line with status "Returned"
                                17.1.4.4 APPEND return date and fine amount
                                17.1.4.5 FORMAT: "IssueID|BookID|MemberID|IssueDate|DueDate|BookTitle|Returned|ReturnDate|Fine"
                                17.1.4.6 ADD updated line to list
                       17.1.4.7 ELSE
                                17.1.4.8 ADD original line to list
                       17.1.4.9 END IF
                17.1.5 END FOR
                17.1.6 WRITE all updated lines back to issued_books.txt
         17.1.7 END IF
18. UPDATE book available copies
    18.1 CALL UpdateBookQuantity(bookID, +1)
         18.1.1 IF books.txt exists THEN
                18.1.2 READ all lines from file
                18.1.3 CREATE empty list for updated lines
                18.1.4 FOR EACH line in file DO
                       18.1.4.1 SPLIT line by "|" delimiter
                       18.1.4.2 IF Book ID matches target THEN
                                18.1.4.3 EXTRACT current AvailableCopies (field 8)
                                18.1.4.4 CALCULATE new AvailableCopies = current + change (+1)
                                18.1.4.5 RECONSTRUCT line with new AvailableCopies
                                18.1.4.6 ADD to updated list
                       18.1.4.7 ELSE
                                18.1.4.8 ADD original line to updated list
                       18.1.4.9 END IF
                18.1.5 END FOR
                18.1.6 WRITE all updated lines back to books.txt
         18.1.7 END IF
19. RELOAD issued books list (refresh DataGridView)
    19.1 CALL LoadIssuedBooks()
20. DISPLAY "Book returned successfully!"
21. CATCH Exception
    21.1 DISPLAY "Error returning book: " + error message
    21.2 ROLLBACK: Issue status and book quantity not updated
22. END Try-Catch Block
23. END
```

**Code Reference:**
```vb
' ReturnBooks.vb - Lines 70-108 (btnReturn_Click)
' ReturnBooks.vb - Lines 116-125 (CalculateFine)
' ReturnBooks.vb - Lines 128-156 (UpdateIssuedBook)
' ReturnBooks.vb - Lines 158-182 (UpdateBookQuantity)
' ReturnBooks.vb - Lines 20-34 (SetupDataGridView)
' ReturnBooks.vb - Lines 37-68 (LoadIssuedBooks)
' ReturnBooks.vb - Lines 193-214 (dgvIssuedBooks_SelectionChanged)
```

**Time Complexity:** 
- Loading issued books: O(n) where n = number of issue records
- Updating records: O(n) for rewriting files
- Fine calculation: O(1)

**Space Complexity:** O(n) for storing file lines in memory

---

### FEATURE 6: View Books

**Source File:** `WindowsApp1/ViewBooks.vb`  
**Entry Point:** `ViewBooks_Load` (Lines 8-15)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: ViewBooks
INPUT: Optional search text
OUTPUT: Display all books (or filtered books) in DataGridView

1. START
2. User navigates: Dashboard ? Click "View Books" button
3. OPEN ViewBooks form
4. SETUP DataGridView
   4.1 CALL SetupDataGridView()
       4.1.1 CLEAR existing columns
       4.1.2 ADD columns: BookID, Title, Author, ISBN, Category, Publisher, Year, Total, Available, ShelfLoc, AddedDate
       4.1.3 SET column widths for optimal display
       4.1.4 SET properties (read-only, no add rows, full row select)
5. LOAD all books
   5.1 CALL LoadBooks("")
       5.1.1 CLEAR DataGridView rows
       5.1.2 IF books.txt does not exist THEN
             5.1.3 DISPLAY "Total Books: 0"
             5.1.4 RETURN
       5.1.5 END IF
       5.1.6 READ all lines from file
       5.1.7 INITIALIZE count = 0
       5.1.8 FOR EACH line in file DO
             5.1.8.1 IF line is not empty THEN
                     5.1.8.2 SPLIT line by "|" delimiter
                     5.1.8.3 IF parts.Length >= 11 THEN
                             5.1.8.4 ADD row to DataGridView with all book fields
                             5.1.8.5 INCREMENT count
                     5.1.8.6 END IF
             5.1.8.7 END IF
       5.1.9 END FOR
       5.1.10 DISPLAY "Total Books: " + count
6. DISPLAY form with loaded books

OPTIONAL: User wants to SEARCH for specific books
7. User enters search text in txtSearch
8. User clicks "Search" button OR presses Enter key
9. CALL LoadBooks(searchText)
   9.1 CLEAR DataGridView rows
   9.2 READ all lines from books.txt
   9.3 INITIALIZE count = 0
   9.4 FOR EACH line in file DO
       9.4.1 SPLIT line by "|" delimiter
       9.4.2 CONVERT search text to lowercase
       9.4.3 IF search text is empty OR
              Book ID contains search text OR
              Title contains search text OR
              Author contains search text OR
              Category contains search text OR
              Publisher contains search text THEN
              9.4.4 ADD row to DataGridView
              9.4.5 INCREMENT count
       9.4.6 END IF
   9.5 END FOR
   9.6 DISPLAY "Total Books: " + count (filtered count)

OPTIONAL: User wants to DELETE a book
10. User selects a book row in DataGridView
11. User clicks "Delete" button
12. BEGIN Try-Catch Block
13. CHECK if a row is selected
    13.1 IF no row selected THEN
         13.2 DISPLAY "Please select a book to delete"
         13.3 RETURN
    13.4 END IF
14. EXTRACT Book ID from selected row (column 0)
15. DISPLAY confirmation dialog:
    "Are you sure you want to delete this book? Book ID: [ID]"
16. IF user clicks "No" THEN
    16.1 RETURN (cancel deletion)
17. END IF
18. CALL DeleteBook(bookID)
    18.1 IF books.txt exists THEN
         18.2 READ all lines from file
         18.3 CREATE empty list for remaining lines
         18.4 FOR EACH line in file DO
              18.4.1 SPLIT line by "|" delimiter
              18.4.2 IF Book ID does NOT match target THEN
                     18.4.3 ADD line to remaining list
              18.4.4 END IF
         18.5 END FOR
         18.6 WRITE remaining lines back to books.txt
    18.7 END IF
19. RELOAD books list
    19.1 CALL LoadBooks()
20. DISPLAY "Book deleted successfully!"
21. CATCH Exception
    21.1 DISPLAY "Error deleting book: " + error message
22. END Try-Catch Block

OPTIONAL: User wants to REFRESH the list
23. User clicks "Refresh" button
24. CLEAR search text
25. CALL LoadBooks("") to reload all books

26. User clicks "Close" button
27. CLOSE form
28. END
```

**Code Reference:**
```vb
' ViewBooks.vb - Lines 8-15 (ViewBooks_Load)
' ViewBooks.vb - Lines 18-53 (SetupDataGridView)
' ViewBooks.vb - Lines 56-100 (LoadBooks with optional search)
' ViewBooks.vb - Lines 103-105 (btnSearch_Click)
' ViewBooks.vb - Lines 108-111 (btnRefresh_Click)
' ViewBooks.vb - Lines 114-139 (btnDelete_Click)
' ViewBooks.vb - Lines 142-161 (DeleteBook)
```

**Time Complexity:** 
- Loading books: O(n) where n = number of books
- Searching: O(n) with string comparison
- Deleting: O(n) for rewriting file

**Space Complexity:** O(n) for storing file lines in memory

---

### FEATURE 7: View Members

**Source File:** `WindowsApp1/ViewMembers.vb`  
**Entry Point:** `ViewMembers_Load` (Lines 8-15)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: ViewMembers
INPUT: Optional search text
OUTPUT: Display all members (or filtered members) in DataGridView

1. START
2. User navigates: Dashboard ? Click "View Members" button
3. OPEN ViewMembers form
4. SETUP DataGridView
   4.1 CALL SetupDataGridView()
       4.1.1 CLEAR existing columns
       4.1.2 ADD columns: MemberID, FullName, StudentID, Gender, Address, Email, Phone, Type, Date
       4.1.3 SET column widths for optimal display
       4.1.4 SET properties (read-only, no add rows, full row select)
5. LOAD all members
   5.1 CALL LoadMembers("")
       5.1.1 CLEAR DataGridView rows
       5.1.2 IF members.txt does not exist THEN
             5.1.3 DISPLAY "Total Members: 0"
             5.1.4 RETURN
       5.1.5 END IF
       5.1.6 READ all lines from file
       5.1.7 INITIALIZE count = 0
       5.1.8 FOR EACH line in file DO
             5.1.8.1 IF line is not empty THEN
                     5.1.8.2 SPLIT line by "|" delimiter
                     5.1.8.3 IF parts.Length >= 9 THEN
                             5.1.8.4 ADD row to DataGridView with all member fields
                             5.1.8.5 INCREMENT count
                     5.1.8.6 END IF
             5.1.8.7 END IF
       5.1.9 END FOR
       5.1.10 DISPLAY "Total Members: " + count
6. DISPLAY form with loaded members

OPTIONAL: User wants to SEARCH for specific members
7. User enters search text in txtSearch
8. User clicks "Search" button OR presses Enter key
9. CALL LoadMembers(searchText)
   9.1 CLEAR DataGridView rows
   9.2 READ all lines from members.txt
   9.3 INITIALIZE count = 0
   9.4 FOR EACH line in file DO
       9.4.1 SPLIT line by "|" delimiter
       9.4.2 CONVERT search text to lowercase
       9.4.3 IF search text is empty OR
              Member ID contains search text OR
              Name contains search text OR
              Student ID contains search text OR
              Address contains search text OR
              Email contains search text OR
              Member Type contains search text THEN
              9.4.4 ADD row to DataGridView
              9.4.5 INCREMENT count
       9.4.6 END IF
   9.5 END FOR
   9.6 DISPLAY "Total Members: " + count (filtered count)

OPTIONAL: User wants to DELETE a member
10. User selects a member row in DataGridView
11. User clicks "Delete" button
12. BEGIN Try-Catch Block
13. CHECK if a row is selected
    13.1 IF no row selected THEN
         13.2 DISPLAY "Please select a member to delete"
         13.3 RETURN
    13.4 END IF
14. EXTRACT Member ID from selected row (column 0)
15. DISPLAY confirmation dialog:
    "Are you sure you want to delete this member? Member ID: [ID]"
16. IF user clicks "No" THEN
    16.1 RETURN (cancel deletion)
17. END IF
18. CALL DeleteMember(memberID)
    18.1 IF members.txt exists THEN
         18.2 READ all lines from file
         18.3 CREATE empty list for remaining lines
         18.4 FOR EACH line in file DO
              18.4.1 SPLIT line by "|" delimiter
              18.4.2 IF Member ID does NOT match target THEN
                     18.4.3 ADD line to remaining list
              18.4.4 END IF
         18.5 END FOR
         18.6 WRITE remaining lines back to members.txt
    18.7 END IF
19. RELOAD members list
    19.1 CALL LoadMembers()
20. DISPLAY "Member deleted successfully!"
21. CATCH Exception
    21.1 DISPLAY "Error deleting member: " + error message
22. END Try-Catch Block

OPTIONAL: User wants to REFRESH the list
23. User clicks "Refresh" button
24. CLEAR search text
25. CALL LoadMembers("") to reload all members

26. User clicks "Close" button
27. CLOSE form
28. END
```

**Code Reference:**
```vb
' ViewMembers.vb - Lines 8-15 (ViewMembers_Load)
' ViewMembers.vb - Lines 18-49 (SetupDataGridView)
' ViewMembers.vb - Lines 52-94 (LoadMembers with optional search)
' ViewMembers.vb - Lines 97-99 (btnSearch_Click)
' ViewMembers.vb - Lines 102-105 (btnRefresh_Click)
' ViewMembers.vb - Lines 108-133 (btnDelete_Click)
' ViewMembers.vb - Lines 136-155 (DeleteMember)
```

**Time Complexity:** 
- Loading members: O(n) where n = number of members
- Searching: O(n) with string comparison
- Deleting: O(n) for rewriting file

**Space Complexity:** O(n) for storing file lines in memory

---

### FEATURE 8: Logout

**Source File:** `WindowsApp1/Dashboard.vb`  
**Entry Point:** `btnLogout_Click` (Lines 86-98)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: Logout
INPUT: User confirmation
OUTPUT: Return to login screen

1. START
2. User clicks "Logout" button on Dashboard
3. BEGIN Try-Catch Block
4. DISPLAY confirmation dialog:
   "Are you sure you want to logout?"
5. GET user response (Yes/No)
6. IF user clicks "Yes" THEN
   6.1 CREATE new Form1 (login form) object
   6.2 CLOSE current Dashboard form
   6.3 SHOW login form
7. ELSE
   7.1 RETURN (cancel logout, remain on Dashboard)
8. END IF
9. CATCH Exception
   9.1 DISPLAY "Error during logout: " + error message
10. END Try-Catch Block
11. END
```

**Code Reference:**
```vb
' Dashboard.vb - Lines 86-98 (btnLogout_Click)
```

**Time Complexity:** O(1)  
**Space Complexity:** O(1)

---

### FEATURE 9: Exit Application

**Source Files:** 
- `WindowsApp1/Form1.vb` - Lines 64-69 (from login screen)
- `WindowsApp1/Dashboard.vb` - Lines 101-112 (from dashboard)

**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: ExitApplication
INPUT: User confirmation
OUTPUT: Close entire application

1. START
2. User clicks "Exit" button (on Login Form or Dashboard)
3. BEGIN Try-Catch Block
4. DISPLAY confirmation dialog:
   "Are you sure you want to exit the application?"
5. GET user response (Yes/No)
6. IF user clicks "Yes" THEN
   6.1 CALL Application.Exit()
   6.2 TERMINATE entire application
   6.3 CLOSE all open forms
   6.4 RELEASE all resources
7. ELSE
   7.1 RETURN (cancel exit, remain in application)
8. END IF
9. CATCH Exception
   9.1 DISPLAY "Error during exit: " + error message
10. END Try-Catch Block
11. END
```

**Code Reference:**
```vb
' Form1.vb - Lines 64-69 (Button2_Click - Exit from Login)
' Dashboard.vb - Lines 101-112 (btnExit_Click - Exit from Dashboard)
```

**Time Complexity:** O(1)  
**Space Complexity:** O(1)

---

### FEATURE 10: Dashboard Navigation

**Source File:** `WindowsApp1/Dashboard.vb`  
**Entry Point:** `Dashboard_Load` (Lines 8-13)  
**User Role:** Admin/Librarian

**Algorithm Steps:**
```
ALGORITHM: DashboardNavigation
INPUT: User button clicks
OUTPUT: Open respective forms

1. START
2. User successfully logs in
3. OPEN Dashboard form
4. DISPLAY Dashboard with navigation buttons
5. INITIALIZE data files
   5.1 CALL InitializeDataFiles()
       5.1.1 IF books.txt does not exist THEN
             5.1.2 CREATE empty books.txt file
       5.1.3 END IF
       5.1.4 IF members.txt does not exist THEN
             5.1.5 CREATE empty members.txt file
       5.1.6 END IF
       5.1.7 IF issued_books.txt does not exist THEN
             5.1.8 CREATE empty issued_books.txt file
       5.1.9 END IF
6. WAIT for user action

NAVIGATION OPTIONS:
7. IF user clicks "Add Books" button THEN
   7.1 CREATE new AddBooks form object
   7.2 SHOW AddBooks form as dialog
   7.3 RETURN to Dashboard when form closes
8. ELSE IF user clicks "View Books" button THEN
   8.1 CREATE new ViewBooks form object
   8.2 SHOW ViewBooks form as dialog
   8.3 RETURN to Dashboard when form closes
9. ELSE IF user clicks "Add Members" button THEN
   9.1 CREATE new AddMembers form object
   9.2 SHOW AddMembers form as dialog
   9.3 RETURN to Dashboard when form closes
10. ELSE IF user clicks "View Members" button THEN
    10.1 CREATE new ViewMembers form object
    10.2 SHOW ViewMembers form as dialog
    10.3 RETURN to Dashboard when form closes
11. ELSE IF user clicks "Issue Books" button THEN
    11.1 CREATE new IssueBooks form object
    11.2 SHOW IssueBooks form as dialog
    11.3 RETURN to Dashboard when form closes
12. ELSE IF user clicks "Return Books" button THEN
    12.1 CREATE new ReturnBooks form object
    12.2 SHOW ReturnBooks form as dialog
    12.3 RETURN to Dashboard when form closes
13. ELSE IF user clicks "Logout" button THEN
    13.1 CALL Logout algorithm (see Feature 8)
14. ELSE IF user clicks "Exit" button THEN
    14.1 CALL Exit Application algorithm (see Feature 9)
15. END IF

16. REPEAT from step 6 (wait for next user action)
17. END
```

**Code Reference:**
```vb
' Dashboard.vb - Lines 8-13 (Dashboard_Load)
' Dashboard.vb - Lines 16-34 (InitializeDataFiles)
' Dashboard.vb - Lines 37-44 (btnAddBooks_Click)
' Dashboard.vb - Lines 47-54 (btnViewBooks_Click)
' Dashboard.vb - Lines 57-64 (btnAddMembers_Click)
' Dashboard.vb - Lines 67-74 (btnViewMembers_Click)
' Dashboard.vb - Lines 77-84 (btnIssueBooks_Click)
' Dashboard.vb - Lines 86-93 (btnReturnBooks_Click)
' Dashboard.vb - Lines 96-108 (btnLogout_Click)
' Dashboard.vb - Lines 111-123 (btnExit_Click)
```

**Time Complexity:** O(1) for each navigation action  
**Space Complexity:** O(1) for each form instance

---

## Summary of Feature Algorithms

| Feature | Main Algorithm | User Role | Input | Output | Time Complexity |
|---------|---------------|-----------|-------|--------|----------------|
| **User Login** | Authentication validation | Admin/Librarian | Username, Password | Dashboard access | O(n) users |
| **Add Book** | Auto-ID generation + Validation + File append | Admin/Librarian | Book details | New book record | O(n) for ID, O(1) for add |
| **Add Member** | Auto-ID generation + Validation + File append | Admin/Librarian | Member details | New member record | O(n) for ID, O(1) for add |
| **Issue Book** | Availability check + Member verification + Quantity update | Admin/Librarian | Book ID, Member ID | Issue record, Updated inventory | O(n) books + O(m) members |
| **Return Book** | Fine calculation + Status update + Quantity update | Admin/Librarian | Issue selection, Return date | Updated records, Fine amount | O(n) records |
| **View Books** | File read + Optional search filter + Display | Admin/Librarian | Optional search text | Filtered book list | O(n) books |
| **View Members** | File read + Optional search filter + Display | Admin/Librarian | Optional search text | Filtered member list | O(n) members |
| **Delete Book** | Record removal + File rewrite | Admin/Librarian | Book ID | Updated book file | O(n) books |
| **Delete Member** | Record removal + File rewrite | Admin/Librarian | Member ID | Updated member file | O(n) members |
| **Logout** | Confirmation + Form transition | Admin/Librarian | User confirmation | Login screen | O(1) |
| **Exit** | Confirmation + Application termination | Admin/Librarian | User confirmation | Application closed | O(1) |
| **Dashboard Navigation** | Form opening based on user action | Admin/Librarian | Button clicks | Various forms | O(1) |

---

## Data Storage

The application uses **text-based file storage** with pipe-delimited format for simplicity and portability.

### Storage Location
All data files are stored in the application's executable directory:
- `books.txt` - Book inventory database
- `members.txt` - Member registration database
- `issued_books.txt` - Book circulation and transaction history

---

### 1. Books Database (books.txt)

**Format:** Pipe-delimited text file (|)

**Structure:**
```
BookID|Title|Author|ISBN|Category|Publisher|PublishYear|TotalCopies|AvailableCopies|ShelfLocation|AddedDate
```

**Field Descriptions:**
