# ?? Library Management System - VB.NET
## University Project - Beginner Friendly with Three Control Structures

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)]()
[![VB.NET](https://img.shields.io/badge/VB.NET-Framework%204.7.2-blue)]()
[![License](https://img.shields.io/badge/license-Educational-yellow)]()

---

## ?? Project Overview

A **comprehensive Library Management System** built with **Visual Basic .NET** for educational purposes. This project demonstrates the **three fundamental control structures** in programming while providing a fully functional library management application.

### ?? Perfect For:
- University programming courses
- VB.NET beginners
- Learning control structures
- Windows Forms development
- First major programming project

---

## ? Key Features

### ?? **User Management**
- Secure login system
- Role-based access (Admin & Librarian)
- Session management

### ?? **Book Management**
- ? View all books with color-coded availability
- ? Add new books with validation
- ? Search books by title/author
- ? Delete books with confirmation
- ? Track quantities and availability

### ?? **Member Management**
- ? View all library members
- ? Add new members with validation
- ? Email and phone validation
- ? Membership tracking

### ?? **Transaction Management**
- ? Issue books to members
- ? Return books with fine calculation
- ? 14-day default loan period
- ? **Automatic fine calculation** ($5/day for overdue)
- ? Transaction history tracking

### ?? **Reports & Dashboard**
- ? Real-time statistics
- ? Overdue books report
- ? Fine calculations
- ? Visual indicators (color coding)

---

## ?? **Three Control Structures Demonstrated**

This project clearly demonstrates all three fundamental control structures:

### 1?? **SEQUENTIAL Structure**
Statements executed in order, one after another.
```vb
' Example from DataStore.vb
Users.Clear()          ' Step 1
Books.Clear()          ' Step 2
Members.Clear()        ' Step 3
Transactions.Clear()   ' Step 4
```

### 2?? **SELECTION Structure**
Decision-making using If-Then-Else.
```vb
' Example from Book.vb
If AvailableQuantity > 0 Then
    Return True
Else
    Return False
End If
```

### 3?? **ITERATION Structure**
Loops using For and For Each.
```vb
' Example from ViewBooksForm.vb
For Each book As Book In DataStore.Books
    ' Display each book
    lvBooks.Items.Add(item)
Next
```

**All control structures are clearly marked with comments in the code!**

---

## ?? Quick Start

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2+
- Windows OS

### Installation & Running

1. **Clone the repository:**
   ```bash
   git clone https://github.com/TeddyMbithi/Library-Management-System-VB.git
   cd Library-Management-System-VB
   ```

2. **Open in Visual Studio:**
   - Double-click `LibraryManagementSystem.sln`

3. **Build the solution:**
   - Press `F6` or `Build > Build Solution`

4. **Run the application:**
   - Press `F5` or click Start button

5. **Login with credentials:**
   ```
   Admin:      username: admin       password: admin123
   Librarian:  username: librarian   password: lib123
   ```

---

## ?? Project Structure

```
LibraryManagementSystem/
??? Models/                      # Data entities
?   ??? Book.vb                 # Book model with properties
?   ??? Member.vb               # Member model
?   ??? Transaction.vb          # Transaction model with fine calculation
?   ??? User.vb                 # User model for authentication
?
??? Data/                        # Data storage
?   ??? DataStore.vb            # In-memory data using Lists
?                                 (10 books, 5 members, 3 transactions)
?
??? Forms/                       # User interface
?   ??? LoginForm.vb            # Authentication
?   ??? MainDashboard.vb        # Main navigation
?   ??? ViewBooksForm.vb        # Display books
?   ??? AddBookForm.vb          # Add new books
?   ??? SearchBooksForm.vb      # Search functionality
?   ??? ViewMembersForm.vb      # Display members
?   ??? AddMemberForm.vb        # Add new members
?   ??? IssueBookForm.vb        # Issue books
?   ??? ReturnBookForm.vb       # Return books
?   ??? TransactionHistoryForm.vb  # View transactions
?   ??? OverdueReportForm.vb    # Overdue report
?
??? Utilities/                   # Helper classes
    ??? ValidationHelper.vb     # Input validation
    ??? MessageHelper.vb        # Message displays
```

---

## ?? Sample Data Included

The application comes pre-loaded with:

- **10 Books:** Harry Potter, 1984, The Great Gatsby, and more
- **5 Members:** Complete with contact information
- **3 Transactions:** Active and returned examples
- **2 Users:** Admin and Librarian accounts

**No database required!** Data is stored in memory using `List(Of T)`.

---

## ?? Learning Objectives

### For Students:
- ? Understand Object-Oriented Programming
- ? Master Control Structures (Sequential, Selection, Iteration)
- ? Learn Windows Forms development
- ? Practice input validation
- ? Implement business logic
- ? Work with collections (Lists)
- ? Handle events (button clicks, form loads)
- ? Design user interfaces

### Code Quality:
- **Heavily commented** - Every file explains its purpose
- **Beginner-friendly** - Clear, simple code
- **Well-organized** - Proper folder structure
- **Control structures marked** - Easy to identify examples

---

## ?? Documentation Files

| Document | Description |
|----------|-------------|
| **README.md** | This file - Project overview |
| **[STUDENT_FEATURES_GUIDE.md](STUDENT_FEATURES_GUIDE.md)** | Detailed feature list & extension ideas |
| **[FEATURES_CHECKLIST.md](FEATURES_CHECKLIST.md)** | What's working & quick reference |
| **[TROUBLESHOOTING.md](TROUBLESHOOTING.md)** | Common issues & solutions |

---

## ?? Key Features for Students

### **What's Working:**
- ? Complete authentication system
- ? Full CRUD operations for books
- ? Member management
- ? Book issuing and returns
- ? Fine calculation ($5/day)
- ? Search and filter
- ? Validation on all inputs
- ? Color-coded UI (Red/Orange/Green)

### **Extension Ideas (For Practice):**
- ?? Edit book/member functionality
- ?? Book categories management
- ?? Email notifications
- ?? Export reports to PDF
- ?? Database integration (SQL Server)
- ?? Barcode scanning
- ?? Advanced analytics

---

## ?? Validation Features

The system includes comprehensive validation:

| Field | Validation Rule |
|-------|----------------|
| Email | Must contain @ and . in correct positions |
| Phone | 10+ digits, numbers only |
| ISBN | 10 or 13 characters |
| Year | Between 1000 and current year |
| Quantity | Positive integers only |
| All Fields | No empty/whitespace |

---

## ?? UI/UX Features

- **Color Coding:**
  - ?? Red = Out of stock / Overdue
  - ?? Orange = Low stock
  - ?? Green = Available / On time

- **User-Friendly Messages:**
  - Success confirmations
  - Error alerts
  - Warning dialogs
  - Confirmation prompts

- **Intuitive Navigation:**
  - Clear button labels
  - Organized dashboard
  - Easy-to-use forms

---

## ?? Project Statistics

- **Total Files:** 35+
- **VB.NET Code Files:** 21
- **Forms:** 11
- **Model Classes:** 4
- **Lines of Code:** ~2,500+
- **Control Structure Examples:** 50+
- **Build Time:** ~14 seconds
- **Executable Size:** 86 KB

---

## ?? Testing the Application

### Test Scenario 1: Complete Book Workflow
1. Login as admin
2. View all books ? See 10 pre-loaded books
3. Add new book ? Enter details with validation
4. Search for book ? Find by title/author
5. Delete book ? Confirm deletion

### Test Scenario 2: Member & Transaction
1. Add new member ? Complete registration
2. Issue book ? Select book and member
3. View dashboard ? See updated statistics
4. Return book ? Calculate fines if overdue
5. View reports ? Check overdue books

---

## ??? Technical Details

- **Language:** Visual Basic .NET
- **Framework:** .NET Framework 4.7.2
- **UI:** Windows Forms
- **Data Storage:** In-Memory Collections (List<T>)
- **IDE:** Visual Studio 2019+
- **Build Status:** ? 0 Errors, 0 Warnings

---

## ?? Learning Resources

### Within This Project:
- Read code comments (every file is documented)
- Study control structure examples (marked clearly)
- Review validation techniques
- Examine form design patterns

### External Resources:
- [Microsoft VB.NET Documentation](https://docs.microsoft.com/en-us/dotnet/visual-basic/)
- [Windows Forms Guide](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
- [VB.NET Programming Guide](https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/)

---

## ?? Contributing (For Students)

This project is designed for learning. Feel free to:
- ? Add new features
- ? Improve existing code
- ? Fix bugs
- ? Enhance UI
- ? Add documentation
- ? Share your improvements

---

## ?? Important Notes

1. **Data is not persistent** - Resets when app closes (by design for learning)
2. **No database required** - Uses in-memory Lists
3. **Beginner-focused** - Clear, simple code
4. **Educational purpose** - Not production-ready
5. **Room for improvement** - Intentional gaps for student practice

---

## ?? Assessment Criteria (For Instructors)

### Demonstrates:
- ? Sequential control structure understanding
- ? Selection (If-Then-Else) implementation
- ? Iteration (loops) proficiency
- ? Object-Oriented Programming concepts
- ? Event-driven programming
- ? Input validation techniques
- ? UI design principles
- ? Code organization and structure

---

## ?? License

This project is created for **educational purposes**. Feel free to use it for learning, teaching, or academic projects.

---

## ????? Author

**TeddyMbithi**
- GitHub: [@TeddyMbithi](https://github.com/TeddyMbithi)
- Project: Library Management System VB.NET

---

## ?? Acknowledgments

- Created for university programming students
- Designed to demonstrate control structures clearly
- Built with beginner programmers in mind
- Sample data inspired by popular literature

---

## ?? Support

Having issues? Check:
1. **[TROUBLESHOOTING.md](TROUBLESHOOTING.md)** - Common problems & solutions
2. **Code comments** - Inline documentation
3. **[FEATURES_CHECKLIST.md](FEATURES_CHECKLIST.md)** - Feature verification

---

## ?? Perfect For

- **VB.NET Beginners** - Learn by example
- **University Projects** - Complete, working system
- **Control Structure Learning** - Clear examples
- **Windows Forms Practice** - Real application
- **First Major Project** - Comprehensive yet manageable

---

## ? Features Students Love

- **Works immediately** - No setup complexity
- **Clear documentation** - Every file explained
- **Sample data included** - Test right away
- **Room to grow** - Many extension opportunities
- **Real-world application** - Practical use case
- **Visual feedback** - Color coding and messages

---

**?? Ready to Start Learning? Clone, Build, and Explore!**

```bash
git clone https://github.com/TeddyMbithi/Library-Management-System-VB.git
cd Library-Management-System-VB
# Open LibraryManagementSystem.sln in Visual Studio
# Press F5 to run!
```

---

**Made with ?? for Students Learning VB.NET**

*Last Updated: December 20, 2024*