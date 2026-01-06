# QUICK START & TESTING GUIDE

## Getting Started in 5 Minutes

### Step 1: Open the Project (30 seconds)
1. Navigate to: `C:\Users\Zuko\OneDrive\Documents\VBProjects\WindowsApp1`
2. Double-click `WindowsApp1.slnx` to open in Visual Studio
3. Wait for the solution to load

### Step 2: Run the Application (30 seconds)
1. Press **F5** or click the green **Start** button
2. The application will compile and launch
3. The Login form should appear

### Step 3: Login (15 seconds)
- Username: `admin`
- Password: `admin123`
- Click **Login** button

### Step 4: Explore Dashboard (15 seconds)
You should see 8 colorful buttons:
- 📚 Add Books (Blue)
- 📖 View Books (Blue)
- 👤 Add Members (Green)
- 👥 View Members (Green)
- 📤 Issue Books (Yellow)
- 📥 Return Books (Yellow)
- 🚪 Logout (Orange)
- ❌ Exit Application (Red)

---

## Complete Testing Checklist

### Test 1: Add Books ✓

**Steps:**
1. Click "Add Books" from Dashboard
2. Book ID should show: BK0001 (auto-generated)
3. Fill in:
   - Title: `Introduction to Programming`
   - Author: `John Doe`
   - ISBN: `9781234567890`
   - Quantity: `5`
   - Category: Select `Technology`
4. Click "Add Book"
5. Success message should appear
6. Form should clear with new ID: BK0002

**Expected Results:**
- ✅ Book ID auto-generates
- ✅ Success message displays
- ✅ Form clears automatically
- ✅ New Book ID ready (BK0002)

**Error Testing:**
- Try clicking Add with empty Title → Error: "Please enter the book title"
- Try entering letters in ISBN → Error: "ISBN must contain numbers only"
- Try entering 0 in Quantity → Error: "Quantity must be a positive number"

---

### Test 2: View Books ✓

**Steps:**
1. Click "View Books" from Dashboard
2. You should see the book you just added
3. Try searching for "Programming"
4. Click "Refresh" to see all books again

**Expected Results:**
- ✅ Book displays in grid
- ✅ Search filters results
- ✅ Refresh shows all books
- ✅ Total count updates

**Additional Tests:**
- Select a book and click "Delete"
- Confirm deletion
- Book should be removed from list

---

### Test 3: Add Members ✓

**Steps:**
1. Click "Add Members" from Dashboard
2. Member ID should show: MEM0001
3. Fill in:
   - Name: `Alice Johnson`
   - Email: `alice@email.com`
   - Phone: `1234567890`
   - Type: Select `Student`
   - Date: (auto-set to today)
4. Click "Add Member"

**Expected Results:**
- ✅ Member ID auto-generates
- ✅ Success message displays
- ✅ Form clears for next entry

**Error Testing:**
- Try invalid email (no @) → Error message
- Try letters in phone → Prevented in real-time

---

### Test 4: Issue Books ✓

**Steps:**
1. Click "Issue Books" from Dashboard
2. Enter Book ID: `BK0001`
3. Click "Verify" next to Book ID
4. Book title and author should appear
5. Enter Member ID: `MEM0001`
6. Click "Verify" next to Member ID
7. Member name should appear
8. Issue date = Today (auto-set)
9. Due date = Today + 14 days (auto-set)
10. Click "Issue Book"

**Expected Results:**
- ✅ Book verification works
- ✅ Member verification works
- ✅ Dates auto-calculate
- ✅ Success message with Issue ID
- ✅ Book quantity decreases by 1

**Error Testing:**
- Try non-existent Book ID → "Book not found"
- Try Book ID with 0 quantity → "Book out of stock"
- Try non-existent Member ID → "Member not found"

---

### Test 5: Return Books (No Fine) ✓

**Steps:**
1. Click "Return Books" from Dashboard
2. You should see the book you just issued
3. Ensure return date is TODAY or before due date
4. Select the issued book
5. Click "Return Selected"
6. Confirm the return

**Expected Results:**
- ✅ Fine = $0.00 (on time)
- ✅ "On Time - No Fine" message
- ✅ Book removed from issued list
- ✅ Book quantity increases by 1

---

### Test 6: Return Books (With Fine) ✓

**Setup:**
First, you need a late book. Two options:

**Option A: Edit the data file**
1. Close the application
2. Open `issued_books.txt`
3. Change a due date to a past date (e.g., 2026-01-01)
4. Save and re-run the application

**Option B: Change return date**
1. In Return Books form
2. Select an issued book
3. Change the return date to AFTER the due date
4. Fine should calculate automatically

**Expected Results:**
- ✅ Fine calculates correctly (days late × $0.50)
- ✅ "Late Return - Fine: $X.XX" message in red
- ✅ Fine shown in confirmation dialog
- ✅ Fine recorded in data file

**Example:**
- Due Date: January 1, 2026
- Return Date: January 4, 2026
- Days Late: 3
- Fine: $1.50

---

### Test 7: Search Functionality ✓

**In View Books:**
1. Add several books with different titles
2. Search for partial text (e.g., "Pro" for "Programming")
3. Should filter results

**In View Members:**
1. Add several members
2. Search by name, email, or member type
3. Should filter results

**Expected Results:**
- ✅ Partial matching works
- ✅ Case-insensitive search
- ✅ Total count updates
- ✅ Refresh clears search

---

### Test 8: Error Handling ✓

**Test these error scenarios:**

1. **Login Errors:**
   - Empty username → "Please enter your username"
   - Empty password → "Please enter your password"
   - Wrong credentials → "Invalid username or password"

2. **Validation Errors:**
   - Empty required fields → Specific error messages
   - Invalid data types → Format errors
   - Out of range values → Range errors

3. **Business Logic Errors:**
   - Issue unavailable book → "Out of stock"
   - Verify non-existent ID → "Not found"

4. **File Operations:**
   - Files created automatically if missing
   - Graceful handling of file errors

---

## Using Sample Data

To quickly populate the system with test data:

### Option 1: Manual Copy
1. Close the application
2. Copy these files to the application directory:
   - `sample_books.txt` → rename to → `books.txt`
   - `sample_members.txt` → rename to → `members.txt`
   - `sample_issued_books.txt` → rename to → `issued_books.txt`
3. Run the application
4. All sample data should be visible

### Option 2: Use the samples as reference
- Keep sample files separate
- Manually add a few records using the forms
- This helps you learn the interface

---

## Performance Testing

### Test Data Volume:
- Add 50+ books
- Add 30+ members
- Issue 20+ books
- **Expected:** All operations remain fast

### Search Performance:
- With 50+ books, search should still be instant
- Filter should update immediately

---

## Screenshot Checklist for Report

Take screenshots of these scenarios:

**Basic Operations:**
- ✅ Login screen (with fields filled)
- ✅ Dashboard (all buttons visible)
- ✅ Add Book form (with data filled)
- ✅ View Books (showing multiple books)
- ✅ Add Member form (with data filled)
- ✅ View Members (showing multiple members)
- ✅ Issue Books (both verified)
- ✅ Return Books (with fine calculation)

**Success Messages:**
- ✅ Book added successfully
- ✅ Member added successfully
- ✅ Book issued successfully
- ✅ Book returned successfully

**Error Messages:**
- ✅ Empty field validation
- ✅ Invalid data type
- ✅ Book not found
- ✅ Book out of stock
- ✅ Invalid login

**Special Features:**
- ✅ Auto-generated IDs
- ✅ Search functionality
- ✅ Fine calculation
- ✅ Show password checkbox

---

## Troubleshooting Common Issues

### Issue: "forms cannot be found"
**Solution:** You may need to add forms to the project:
1. Right-click project in Solution Explorer
2. Add → Existing Item
3. Select all .vb and .Designer.vb files
4. Click Add

### Issue: "Startup form not set"
**Solution:**
1. Right-click project → Properties
2. Application tab
3. Startup form → Select "Form1"
4. Save and rebuild

### Issue: Data files not found
**Solution:** Files are created in the same folder as the .exe:
- Usually: `bin\Debug\` or `bin\Release\`
- Place sample data here if needed

### Issue: Old data showing
**Solution:**
1. Close application
2. Delete .txt files from bin\Debug folder
3. Restart application (creates fresh files)

---

## Pre-Submission Checklist

Before submitting your project:

**Code:**
- ✅ All forms compile without errors
- ✅ No warnings in Error List
- ✅ Comment explanations added where needed
- ✅ Consistent formatting

**Functionality:**
- ✅ All 10 requirements demonstrated
- ✅ Error handling works
- ✅ Data persists between runs
- ✅ Application runs until manually closed

**Documentation:**
- ✅ README.md complete
- ✅ PROJECT_REPORT.md has all sections
- ✅ Word document formatted correctly
- ✅ All screenshots taken
- ✅ References in APA 7 format

**Files to Submit:**
- ✅ Complete project folder
- ✅ Word document (formatted report)
- ✅ PDF version of report
- ✅ Sample data files

---

## Quick Demo Script (For Presentation)

**5-Minute Demo:**

1. **Login** (30 sec)
   - Show login screen
   - Enter credentials
   - Show dashboard

2. **Add Book** (1 min)
   - Show auto-generated ID
   - Fill form
   - Show validation (try empty field)
   - Successfully add book

3. **Add Member** (1 min)
   - Show auto-generated ID
   - Fill form
   - Successfully add member

4. **Issue Book** (1.5 min)
   - Verify book (show it exists)
   - Verify member
   - Show auto-calculated dates
   - Issue book
   - Show quantity decreased

5. **Return Book** (1 min)
   - Show issued books list
   - Select book
   - Change date to show fine calculation
   - Process return
   - Show quantity increased

**Talking Points:**
- "All IDs are auto-generated"
- "The system validates all inputs"
- "Fines are calculated automatically"
- "Data is stored in text files for portability"
- "The system handles all edge cases with proper error messages"

---

## Need More Test Data?

Run this in Visual Studio Immediate Window (Debug → Windows → Immediate):
```vb
' This will help you test quickly
For i = 1 To 10
    ' Add code to generate test records
Next
```

Or manually add varied data to test:
- Books with 0 quantity (out of stock)
- Members of different types
- Books with past due dates
- Various ISBN formats

---

## Final Testing Before Submission

**Run through all features ONCE MORE:**
1. Fresh start (delete all .txt files)
2. Add 5 books
3. Add 5 members
4. Issue 3 books
5. Return 1 book (on time)
6. Return 1 book (late → try changing return date)
7. Search in View Books
8. Search in View Members
9. Delete 1 book
10. Delete 1 member
11. Logout and login again (verify data persisted)

**If all 11 tests pass → You're ready to submit! 🎉**

---

**Good luck with your presentation and submission!**
