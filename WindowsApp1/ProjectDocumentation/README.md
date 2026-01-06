# Library Management System - README

## Project Overview
A comprehensive Library Management System built using Visual Basic .NET with Windows Forms. This application provides complete automation for library operations including book management, member registration, book issuance, and returns with automatic fine calculation.

## Features
- ✅ **User Authentication** - Secure login system
- ✅ **Book Management** - Add, view, search, and delete books
- ✅ **Member Management** - Register, view, and manage library members
- ✅ **Issue Books** - Track book issuance with automatic inventory updates
- ✅ **Return Books** - Process returns with automated fine calculation
- ✅ **Search Functionality** - Quick search across all modules
- ✅ **Data Persistence** - File-based storage (books.txt, members.txt, issued_books.txt)
- ✅ **Error Handling** - Comprehensive validation and error messages
- ✅ **Professional UI** - Modern, color-coded interface

## Default Login Credentials
- **Username:** admin
- **Password:** admin123

## How to Run the Application

### Option 1: Using Visual Studio
1. Open the solution file `WindowsApp1.slnx` in Visual Studio
2. Press `F5` or click the "Start" button to run the application
3. Use the default credentials to log in

### Option 2: Running the Executable
1. Navigate to `bin\Debug` or `bin\Release` folder
2. Double-click `WindowsApp1.exe`
3. Log in with the default credentials

## System Requirements
- Windows 7/8/10/11
- .NET Framework 4.7.2 or higher
- 4GB RAM (minimum)
- 100MB free disk space

## File Structure
```
WindowsApp1/
│
├── Form1.vb                    # Login Form
├── Dashboard.vb                # Main Dashboard
├── AddBooks.vb                 # Add Books Module
├── ViewBooks.vb                # View Books Module
├── AddMembers.vb               # Add Members Module
├── ViewMembers.vb              # View Members Module
├── IssueBooks.vb               # Issue Books Module
├── ReturnBooks.vb              # Return Books Module
├── PROJECT_REPORT.md           # Complete Project Documentation
├── README.md                   # This file
└── [Designer files].Designer.vb # UI Designer files for each form
```

## Data Files
The application creates three text files in the application directory:

1. **books.txt** - Stores book records
   - Format: `BookID|Title|Author|ISBN|Quantity|Category`

2. **members.txt** - Stores member records
   - Format: `MemberID|FullName|Email|Phone|MemberType|RegistrationDate`

3. **issued_books.txt** - Stores book issuance records
   - Format: `IssueID|BookID|MemberID|IssueDate|DueDate|BookTitle|Status[|ReturnDate|Fine]`

## Usage Guide

### 1. Login
- Launch the application
- Enter username: `admin`
- Enter password: `admin123`
- Click "Login"

### 2. Adding Books
1. From Dashboard, click "Add Books"
2. Book ID is auto-generated
3. Enter book details (Title, Author, ISBN, Quantity, Category)
4. Click "Add Book"
5. Book is saved and form clears for next entry

### 3. Viewing Books
1. From Dashboard, click "View Books"
2. All books are displayed in a grid
3. Use search box to filter books
4. Select a book and click "Delete" to remove it

### 4. Adding Members
1. From Dashboard, click "Add Members"
2. Member ID is auto-generated
3. Enter member details (Name, Email, Phone, Type, Date)
4. Click "Add Member"
5. Member is saved and form clears for next entry

### 5. Issuing Books
1. From Dashboard, click "Issue Books"
2. Enter Book ID and click "Verify" to check availability
3. Enter Member ID and click "Verify" to confirm member
4. Issue and Due dates are set automatically
5. Click "Issue Book" to complete

### 6. Returning Books
1. From Dashboard, click "Return Books"
2. All active issued books are displayed
3. Select a book to return
4. Set return date (defaults to today)
5. System calculates any late fees automatically
6. Click "Return Selected" to process

## Fine Calculation
- Books are issued for 14 days
- Late fee: **$0.50 per day**
- Fine is calculated automatically when returning books
- Example: 3 days late = $1.50 fine

## Validation Rules

### Books
- Title: Required
- Author: Required
- ISBN: Required, numeric only
- Quantity: Required, positive integer only
- Category: Required, must select from list

### Members
- Name: Required
- Email: Required, valid email format
- Phone: Required, numeric only
- Member Type: Required, from dropdown
- Registration Date: Auto-set to today

### Issue Books
- Book must exist and be available (quantity > 0)
- Member must exist in database
- Both Book ID and Member ID must be verified

## Troubleshooting

### "File access denied" error
- Ensure the application has write permissions in its directory
- Run as Administrator if needed

### Books/Members not displaying
- Check if the respective .txt files exist
- Ensure files are not corrupted
- Try adding a new record

### Login not working
- Use exact credentials: admin / admin123
- Check for extra spaces
- Ensure Caps Lock is off

## Programming Concepts Demonstrated

1. **User Interface Design** - Professional Windows Forms
2. **Data Types** - Strings, Integers, Decimals, Dates, Arrays
3. **Procedures** - Subs and Functions with parameters
4. **Decision Structures** - If-Then-Else, Select Case
5. **Loops** - For, For Each, While loops
6. **Input Validation** - Multi-layer validation system
7. **Error Handling** - Try-Catch-Finally blocks
8. **Algorithms** - ID generation, search, fine calculation
9. **Data Storage** - File I/O operations
10. **Continuous Operation** - Runs until user exits

## Report Documentation
For complete project documentation including:
- Algorithms and Pseudocode
- Flowcharts
- Full Code with comments
- Screenshots
- Challenges and Solutions
- References

Please refer to: **PROJECT_REPORT.md**

## Author Notes
This project fulfills all requirements for a comprehensive library management system including:
- Complete CRUD operations
- Automated processes
- Error handling
- Professional UI
- Detailed documentation

## Support
For questions or issues:
1. Check the PROJECT_REPORT.md for detailed documentation
2. Review the code comments in each .vb file
3. Check the troubleshooting section above

---
**Developed as an educational project demonstrating Visual Basic .NET programming concepts**
