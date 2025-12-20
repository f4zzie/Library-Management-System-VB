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
# Library Management System (VB.NET) — Codemap & File Reference

This README explains the role of every primary file in this workspace, lists the functions / subs exposed by each source file, and describes which control structures (sequential, selection, iteration, exception handling) are used and where. Use this as a guided code map when studying or extending the project.

Notes:
- The application is a learning/demo project using Windows Forms and in-memory collections (`List(Of T)`) instead of a database.
- Control-structure terminology used in comments: Sequential (straight-line statements), Selection (If / Select Case), Iteration (For, For Each, While), Exception handling (Try / Catch).

--------------------------------------------------------------------------------

**Project root files**

- `create-resx.ps1`: PowerShell helper script (if present) used to regenerate .resx resource files for forms. Not required to run the app.
- `LibraryManagementSystem.sln`: Visual Studio solution file that groups the project. Contains project references and build configuration.
- `IMPLEMENT_STUDENT_LOGIN.vb`: A guidance script containing copy-paste code and step-by-step instructions to implement the student login/dashboard features. It contains full example subs for student dashboards, plus example control-flow usage (For Each loops, If checks) — intended as documentation/boilerplate rather than compiled code.
- Documentation files (`FEATURES_CHECKLIST.md`, `STUDENT_FEATURES_GUIDE.md`, `STUDENT_LOGIN_FEATURES.md`, `MINIMAL_STUDENT_LOGIN_COMPLETE.md`, `TEST_STUDENT_LOGIN_NOW.md`, `TROUBLESHOOTING.md`, etc.): plain-text or Markdown guides describing features, troubleshooting steps and recommended exercises.

--------------------------------------------------------------------------------

**Project: LibraryManagementSystem/**

Top-level project files:

- `App.config` — application configuration (app settings, connection strings if any). In this project it contains runtime configuration for the Windows Forms application.
- `LibraryManagementSystem.vbproj` — MSBuild project file describing build items (source files, resources). Add/remove files here when you add new forms.
- `ApplicationEvents.vb` — small partial class used by VB's application framework. It contains application-level hooks (startup/shutdown) — currently empty; no explicit control structures.

--------------------------------------------------------------------------------

**Data layer**

- `Data/DataStore.vb` — central in-memory data store used across the app.
   - Purpose: hold application data in shared `List(Of T)` collections and provide search, CRUD and utility methods.
   - Public collections: `Users`, `Books`, `Members`, `Transactions` (all `Shared` so they act like global, application-wide storage).
   - Key subs/functions (with control structures):
      - `InitializeSampleData()` — creates initial users, books, members, transactions and calls `UpdateBookAvailability()`.
         - Uses sequential statements to add sample data; demonstrates sequential control flow.
      - `UpdateBookAvailability()` — loops (`For Each`) through `Books`, resets `AvailableQuantity`, then iterates `Transactions` and decrements availability when `trans.Status = "Issued"`.
         - Control: Iteration (`For Each`), Selection (`If trans.Status = "Issued"`, `If book.BookID = trans.BookID`), and `Exit For` when match found.
      - `FindUser(username, password)` — iterates `Users` and returns the first match (iteration + selection).
      - `SearchBooks(searchTerm)` — iterates `Books`, uses `String.Contains` to match title/author (iteration + selection).
      - `GetAvailableBooks()` — iterates and selects books with `AvailableQuantity > 0`.
      - `GetOverdueTransactions()` — iterates `Transactions` and calls `trans.IsOverdue()` (iteration + selection).
      - `GetMemberTransactions(memberID)` — iteration + selection for member transactions.
      - CRUD helpers: `AddBook(book)`, `AddMember(member)`, `AddTransaction(trans)` — sequential steps to set IDs and add to lists.
      - `GetBookByID`, `GetMemberByID` — iterate and return found entity (iteration + selection).
      - `DeleteBook(bookID)`, `DeleteMember(memberID)` — iterate by index (`For i = 0 To Count - 1`) and `RemoveAt(i)` when found (iteration + selection).

   - Notes on control structures: `DataStore` contains the clearest examples of iteration (For Each / For), selection (If … Then / Else) and uses straightforward sequential initialization.

--------------------------------------------------------------------------------

**Models (LibraryManagementSystem/Models/)**

- `Book.vb` — `Book` class
   - Properties: `BookID`, `ISBN`, `Title`, `Author`, `Category`, `Quantity`, `AvailableQuantity`, `Publisher`, `YearPublished`.
   - Constructors: default and parameterized (sequential assignment of properties).
   - Methods:
      - `GetBookInfo()` — returns a formatted string (sequential).
      - `IsAvailable()` — selection structure: `If AvailableQuantity > 0 Then Return True Else Return False`.

- `Member.vb` — `Member` class
   - Properties: `MemberID`, `FullName`, `Email`, `PhoneNumber`, `Address`, `MembershipDate`, `Status`.
   - Methods:
      - `GetMemberInfo()` — returns formatted string.
      - `IsActive()` — selection (`If Status = "Active" Then ...`).
      - `IsMembershipExpired()` — uses `DateDiff` and an `If` to determine expiry (selection).

- `Transaction.vb` — `Transaction` class
   - Properties: IDs, `BookTitle`, `MemberName`, `IssueDate`, `DueDate`, `ReturnDate`, `Status`, `Fine`.
   - Methods:
      - `IsOverdue()` — selection: checks `Status = "Issued" And Date.Today > DueDate`.
      - `CalculateFine()` — selection + sequential arithmetic: computes days overdue via `DateDiff` and multiplies by fixed per-day fine (example `5.0`).
      - `GetTransactionInfo()` — returns formatted string.

- `User.vb` — `User` class
   - Properties: `UserID`, `Username`, `Password`, `FullName`, `Role`, `Email`, `MemberID` (link to `Member` for student accounts).
   - Methods:
      - `IsAdmin()`, `IsStudent()` — simple selection (`If Role = "Admin" Then Return True Else Return False`).
      - `GetUserInfo()` — formatted string.

--------------------------------------------------------------------------------

**Utilities (LibraryManagementSystem/Utilities/)**

- `ValidationHelper.vb` — input validation utilities
   - Validations included: `IsNotEmpty`, `IsValidEmail`, `IsValidPhone`, `IsValidPositiveNumber`, `IsValidISBN`, `IsValidYear`, `IsValidPassword`.
   - Control structures: heavy use of Selection (`If` / `Else`), small Iteration examples (looping through characters in `IsValidPhone`), `Integer.TryParse` checks.
   - `ShowValidationError(fieldName, message)` — displays a messagebox.

- `MessageHelper.vb` — centralized message dialogs
   - Methods: `ShowSuccess`, `ShowError`, `ShowWarning`, `ShowInfo` (all sequential calls to `MessageBox.Show`).
   - `ShowConfirmation(message)` returns `True` if user clicks `Yes` — selection structure based on `DialogResult`.

--------------------------------------------------------------------------------

**Forms (LibraryManagementSystem/Forms/) — UI and event handlers**

Each form demonstrates event-driven programming: UI events (Load, Click, SelectedIndexChanged) trigger Subs that use sequential steps, selections, and iterations. Below are the important forms and a short listing of functions (Subs) and control-flow patterns.

- `LoginForm.vb`
   - Purpose: authentication and routing to dashboards.
   - Important subs:
      - `LoginForm_Load(sender, e)` — calls `DataStore.InitializeSampleData()` (sequential initialization).
      - `InitializeControls()` — placeholder in code (UI setup).
      - `btnLogin_Click(...)` — validation (`If` checks), calls `DataStore.FindUser`, routes user to `StudentDashboard` or `MainDashboard` depending on role (selection + iteration inside `FindUser`).
      - `btnExit_Click(...)` — confirmation dialog (selection).
   - Control structures: selection (field validation, role routing), sequential initialization, iteration via `FindUser`.

- `MainDashboard.vb`
   - Purpose: central navigation after Admin/Librarian login, updates summary stats.
   - Important subs:
      - `MainDashboard_Load(...)` — updates welcome label and calls `UpdateDashboardStatistics()`.
      - `UpdateDashboardStatistics()` — iterates `Books` and `Transactions` to compute totals; selection used to color UI elements when overdue exists.
      - Navigation button click handlers (e.g., `btnViewBooks_Click`) show forms and refresh stats.
   - Control structures: iteration (For Each book/trans), selection for role-based UI visibility and color-coding.

- `ViewBooksForm.vb`
   - Purpose: list all books in a `ListView`.
   - Important subs:
      - `LoadBooks()` — iterates `DataStore.Books`, constructs `ListViewItem` for each book, color-codes items using selection (`If AvailableQuantity = 0 Then ...`).
      - `btnEdit_Click`, `btnDelete_Click`, `btnRefresh_Click` — selection checks on selection count, confirmation dialogs, call `DataStore.DeleteBook`.
   - Control structures: iteration and selection.

- `AddBookForm.vb`
   - Purpose: add a new book with validation.
   - Important subs:
      - `btnSave_Click(...)` — many `If` validations using `ValidationHelper` (selection), then sequentially create `Book` object and call `DataStore.AddBook`.
      - `ClearForm()` — sequential clearing of fields.
   - Control structures: selection-heavy validation, sequential object construction.

- `AddMemberForm.vb`
   - Purpose: add a new member with validation.
   - Important subs:
      - `btnSave_Click(...)` — selection validation (`IsValidEmail`, `IsValidPhone`), sequential creation and `DataStore.AddMember`.

- `IssueBookForm.vb`
   - Purpose: issue a book to a member.
   - Important subs:
      - `LoadAvailableBooks()` and `LoadActiveMembers()` — iteration to populate combo boxes.
      - `btnIssue_Click(...)` — selection checks for chosen inputs, parse IDs, check `book.IsAvailable()`, create `Transaction`, call `DataStore.AddTransaction`, `DataStore.UpdateBookAvailability()`.
   - Control structures: iteration (populate lists) and selection for validation and availability checks.

- `ReturnBookForm.vb`
   - Purpose: list issued transactions, allow return and fine calculation.
   - Important subs:
      - `LoadIssuedBooks()` — iterates transactions, checks `trans.Status = "Issued"`.
      - `lvIssued_SelectedIndexChanged(...)` — iterates to find transaction by ID.
      - `DisplayTransactionDetails()` — selection to determine overdue and fine, toggle UI elements.
      - `btnReturn_Click(...)` — selection and sequential follow-up: set `ReturnDate`, `Status`, compute `Fine`, update availability.
   - Control structures: iteration, selection, date arithmetic in fine calculation.

- `SearchBooksForm.vb` — search box, calls `DataStore.SearchBooks(searchTerm)` and iterates results to display.

- `ViewMembersForm.vb` — iteration over `DataStore.Members` to populate `ListView`.

- `TransactionHistoryForm.vb` — displays all transactions and includes filtering by status (iteration + selection), color coding by status.

- `OverdueReportForm.vb` — iterates `DataStore.GetOverdueTransactions()`, computes total fines, shows summary and warning when overdue count is high (selection).

- `StudentDashboard.vb` — minimal student portal showing the student's borrowed books and total fines.
   - Key subs: `LoadStudentInfo()`, `LoadMyBooks()`, `CalculateFines()` — use iteration through `Transactions` and selection checks to compute student-specific information.

--------------------------------------------------------------------------------

**Designer and auto-generated files**

- Files named `*.Designer.vb` (for example `LoginForm.Designer.vb`, `MainDashboard.Designer.vb`) are auto-generated by Visual Studio's WinForms designer. They contain control declarations and layout code. You normally do not edit them manually; instead use the Form Designer.
- `*.resx` files (resources) contain binary/text resources used by forms (images, strings). `create-resx.ps1` can help regenerate them if needed.

--------------------------------------------------------------------------------

**My Project directory (auto-generated project metadata)**

- `My Project/Application.Designer.vb` — VB application framework glue (auto-generated).
- `My Project/AssemblyInfo.vb` — assembly metadata (title, version, company).
- `My Project/Resources.Designer.vb` and `Resources.resx` — strongly-typed resource accessors.
- `My Project/Settings.Designer.vb` and `Settings.settings` — application settings (user and app-scoped settings).

These files use basic sequential initialization and property definitions; they are generated by the IDE and rarely need manual edits.

--------------------------------------------------------------------------------

How control structures are used across the project (summary):

- Sequential: object construction, default value assignments, initialization of sample data.
- Selection (If / Else): input validation, role checks (admin/student), availability checks, UI color-coding and confirmation dialogs.
- Iteration (For / For Each): populating lists and listviews, scanning transactions/books/members for searches, computing aggregates and counts, updating availability.
- Exception handling: this project uses limited explicit Try/Catch blocks; validation prevents most runtime errors. If you add file IO or DB access, wrap operations with Try/Catch and surface friendly messages via `MessageHelper.ShowError`.

--------------------------------------------------------------------------------

Guide for contributors / students who want to extend the code:

- To add persistence: replace `DataStore` with database access (introduce repository classes, use parameterized queries / ORMs).
- To add editing of books/members: implement edit forms, pre-populate fields from `GetBookByID` / `GetMemberByID`, call update helpers and refresh views.
- To internationalize UI text: move strings into `Resources.resx` and use resource lookups in forms.
- To add exception handling: surround critical operations (parsing, IO) with Try/Catch and call `MessageHelper.ShowError` with the exception message.

--------------------------------------------------------------------------------

If you want, I can:
- Commit this README update to the repo.
- Generate a per-file markdown file for each source file with line-by-line explanations.
- Run a quick static scan for unused methods or simple refactors.

Tell me which next step you prefer.