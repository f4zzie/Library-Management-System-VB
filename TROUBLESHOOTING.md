# ?? Library Management System - Troubleshooting Guide

## Common Issues and Solutions

---

## ?? **Build/Compilation Issues**

### Error: "Cannot find LoginForm"
**Problem:** Form files not included in project  
**Solution:**
1. Right-click project in Solution Explorer
2. Choose "Add ? Existing Item"
3. Navigate to Forms folder
4. Select all form files (.vb and .Designer.vb)
5. Click Add
6. Rebuild solution

### Error: "BC30002: Type 'Book' is not defined"
**Problem:** Model classes not compiled or referenced  
**Solution:**
1. Check that all Model files (Book.vb, Member.vb, etc.) are in project
2. Clean solution: Build ? Clean Solution
3. Rebuild: Build ? Rebuild Solution
4. If still fails, close and reopen Visual Studio

### Error: "Resx file cannot be found"
**Problem:** Missing resource files  
**Solution:**
1. Run the create-resx.ps1 script:
   ```powershell
   powershell -ExecutionPolicy Bypass -File create-resx.ps1
   ```
2. Or manually create empty .resx files:
   - LoginForm.resx
   - MainDashboard.resx
   - Resources.resx

### Error: "Attribute cannot be applied multiple times"
**Problem:** Duplicate attributes in generated code  
**Solution:**
1. Open Application.Designer.vb
2. Remove duplicate attribute declarations
3. Keep only one of each attribute type
4. Rebuild

---

## ?? **Runtime Errors**

### Error: "Object reference not set to an instance"
**Possible Causes:**

#### 1. DataStore not initialized
**Solution:**
```vb
' Make sure LoginForm_Load calls:
DataStore.InitializeSampleData()
```

#### 2. Control not created
**Solution:**
```vb
' In form constructor, ensure InitializeComponent() is called first:
Public Sub New()
    InitializeComponent()  ' Must be first!
    ' Then your code...
End Sub
```

#### 3. Accessing member of Nothing
**Solution:**
```vb
' Always check for Nothing before using:
If book IsNot Nothing Then
    ' Use book here
End If
```

### Error: "Index was out of range"
**Problem:** Trying to access item that doesn't exist  
**Solution:**
```vb
' Check count first:
If lvBooks.SelectedItems.Count > 0 Then
    ' Access selected item
End If

' Or check bounds:
If index >= 0 And index < list.Count Then
    ' Access list(index)
End If
```

### Error: "Input string was not in a correct format"
**Problem:** Invalid conversion (e.g., text to number)  
**Solution:**
```vb
' Use TryParse instead of Parse:
Dim number As Integer
If Integer.TryParse(textBox.Text, number) Then
    ' Use number
Else
    ' Show error message
End If
```

---

## ??? **Application Behavior Issues**

### Application doesn't start / No window appears
**Checks:**
1. Verify LoginForm is set as startup form:
   - Check Application.Designer.vb
   - Should have: `Me.MainForm = Global.LibraryManagementSystem.LoginForm`

2. Check for errors in Form_Load event:
   - Add Try-Catch to see errors:
   ```vb
   Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
       Try
           ' Your code
       Catch ex As Exception
           MessageBox.Show(ex.Message)
       End Try
   End Sub
   ```

### Login button does nothing
**Checks:**
1. Verify button event handler:
   ```vb
   ' Should have: Handles btnLogin.Click
   Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
   ```

2. Check DataStore has users:
   ```vb
   ' In LoginForm_Load, add:
   MessageBox.Show("User count: " & DataStore.Users.Count)
   ```

3. Verify credentials exactly match:
   - Username: `admin` (lowercase)
   - Password: `admin123`

### Forms are blank / No controls visible
**Problem:** InitializeComponent not called or controls not added  
**Solution:**
1. Verify InitializeComponent is in constructor
2. Check that Me.Controls.Add() calls exist for all controls
3. Verify SuspendLayout() and ResumeLayout() are called

### Data doesn't persist between sessions
**Not a bug!** This is by design.  
**Explanation:** Data is stored in-memory using Lists. When app closes, data is lost.  
**Solution (for students):** Implement file or database storage.

---

## ?? **Data Issues**

### No books/members showing
**Checks:**
1. Verify InitializeSampleData() is called:
   ```vb
   ' In LoginForm_Load or Program startup:
   DataStore.InitializeSampleData()
   ```

2. Check if ListView is configured:
   ```vb
   ' Should have columns and View = Details
   lvBooks.View = View.Details
   lvBooks.Columns.Add("Title", 200)
   ```

3. Verify LoadBooks/LoadMembers is called:
   ```vb
   ' In form constructor or Load event
   LoadBooks()
   ```

### Book quantities not updating
**Problem:** UpdateBookAvailability not called after transaction  
**Solution:**
```vb
' After issuing or returning book:
DataStore.UpdateBookAvailability()
```

### Fines not calculating
**Checks:**
1. Verify Transaction.CalculateFine() is called
2. Check date comparison:
   ```vb
   ' Should be:
   If Date.Today > trans.DueDate Then
       ' Calculate fine
   End If
   ```
3. Ensure fine rate is set ($5/day default)

---

## ?? **UI/Display Issues**

### Colors not showing (Red/Orange/Green)
**Problem:** Color assignment not happening  
**Solution:**
```vb
' In LoadBooks():
If book.AvailableQuantity = 0 Then
    item.ForeColor = Color.Red
ElseIf book.AvailableQuantity < book.Quantity / 2 Then
    item.ForeColor = Color.Orange
Else
    item.ForeColor = Color.Green
End If
```

### ListView columns too small/large
**Solution:**
```vb
' Adjust column widths in InitializeComponent:
colTitle.Width = 250  ' Make title column wider
colID.Width = 50      ' Make ID column narrower
```

### Form appears off-screen
**Solution:**
```vb
' In form constructor:
Me.StartPosition = FormStartPosition.CenterScreen
' Or manually:
Me.Location = New Point(100, 100)
```

### Controls overlapping
**Solution:**
1. Open form in Designer (double-click .Designer.vb or form name)
2. Manually adjust control positions
3. Use Layout toolbar for alignment

---

## ?? **Validation Issues**

### Email validation too strict/loose
**Customize:**
```vb
' In ValidationHelper.IsValidEmail():
' Adjust the logic:
If email.Contains("@") And email.Contains(".") Then
    ' Add more checks as needed
End If
```

### Phone validation rejecting valid numbers
**Problem:** Might include dashes or spaces  
**Solution:**
```vb
' Clean the input first:
Dim cleanPhone As String = phone.Replace("-", "").Replace(" ", "")
' Then validate cleanPhone
```

### Year validation rejecting current year
**Check:**
```vb
' Should be:
If yearNum >= 1000 And yearNum <= Date.Today.Year Then
    ' Not: Date.Today.Year - 1
End If
```

---

## ?? **Data Loss / Reset Issues**

### Data disappears after restart
**Expected behavior!** Data is in-memory only.

**Temporary solution:**
Keep app running during testing.

**Permanent solution (for students to implement):**
```vb
' Option 1: Text file storage
' Save on exit:
Private Sub MainDashboard_FormClosing(...)
    SaveDataToFile()
End Sub

' Load on start:
Private Sub LoginForm_Load(...)
    If File.Exists("data.txt") Then
        LoadDataFromFile()
    Else
        DataStore.InitializeSampleData()
    End If
End Sub

' Option 2: XML Serialization
' Option 3: Database (SQL Server, SQLite)
```

---

## ?? **Debugging Tips**

### Use Breakpoints
1. Click in left margin of code line
2. Red dot appears
3. Run with F5
4. Execution pauses at breakpoint
5. Hover over variables to see values
6. Use F10 to step through code

### Watch Variables
1. While debugging, right-click variable
2. Choose "Add Watch"
3. See value in Watch window
4. Track changes as code runs

### Immediate Window
1. View ? Immediate Window (Ctrl+Alt+I)
2. While debugging, type: `?variableName`
3. See current value
4. Execute code: `DataStore.Books.Count`

### Output Debug Info
```vb
' Add to your code:
Debug.WriteLine("Book count: " & DataStore.Books.Count)
Debug.WriteLine("Selected index: " & listView.SelectedIndex)

' View in Output window (View ? Output)
```

---

## ?? **Common Student Mistakes**

### 1. Forgetting to call InitializeComponent()
**Result:** Blank forms, crashes  
**Fix:** Always call as first line in constructor

### 2. Not handling Nothing/null
**Result:** Object reference errors  
**Fix:** Always check `IsNot Nothing` before using

### 3. Wrong event handler
**Result:** Button doesn't respond  
**Fix:** Check `Handles buttonName.Click` is present

### 4. Incorrect loop variable
**Result:** Wrong data shown  
**Fix:** Use correct variable inside loop:
```vb
' Wrong:
For Each book In Books
    item.Text = member.Name  ' WRONG!
Next

' Right:
For Each book In Books
    item.Text = book.Title  ' Correct
Next
```

### 5. Not refreshing UI after data change
**Result:** Old data shown  
**Fix:** Call LoadData() after add/edit/delete

### 6. Comparing strings incorrectly
**Result:** Login fails even with correct password  
**Fix:** 
```vb
' Case-sensitive comparison:
If username = "admin" Then  ' Correct

' Or case-insensitive:
If username.ToLower() = "admin" Then
```

---

## ?? **Testing Checklist**

Before submitting your project, test:

- [ ] Application starts without errors
- [ ] Login works with both credentials
- [ ] Can logout and login again
- [ ] All menu buttons work
- [ ] Can add new book
- [ ] Can view all books
- [ ] Can search books
- [ ] Can delete book (with confirmation)
- [ ] Can add new member
- [ ] Can view all members
- [ ] Can issue book (availability updates)
- [ ] Can return book (fine calculated if overdue)
- [ ] Transaction history shows correct data
- [ ] Overdue report works
- [ ] Dashboard statistics update
- [ ] Validation works on all forms
- [ ] Error messages are clear
- [ ] Confirmation dialogs appear
- [ ] Forms close properly
- [ ] No crashes or unhandled exceptions

---

## ?? **Still Having Issues?**

### Check These Resources:

1. **README.md** - Project overview and setup
2. **STUDENT_FEATURES_GUIDE.md** - Features and extensions
3. **FEATURES_CHECKLIST.md** - What's working
4. **Code Comments** - Every file is documented

### Debug Strategy:

1. **Identify the exact error message**
2. **Note when it occurs** (startup, button click, etc.)
3. **Check this guide** for that specific error
4. **Add Debug.WriteLine** statements
5. **Use breakpoints** to trace execution
6. **Check variable values** at error point
7. **Compare your code** to original files

### Getting Help from Instructor:

Provide:
- Exact error message
- Steps to reproduce
- What you tried
- Screenshots if helpful
- Relevant code snippet

---

## ?? **Learning from Errors**

Remember: **Errors are learning opportunities!**

- ? Read error messages carefully
- ? Look up error codes online
- ? Understand WHY it happened
- ? Learn the correct way
- ? Document your solution

**"A smooth sea never made a skilled sailor."** - Keep debugging, keep learning!

---

**Last Updated:** December 20, 2024  
**Version:** 1.0  

*For additional help, review the code comments - they explain every section!*
