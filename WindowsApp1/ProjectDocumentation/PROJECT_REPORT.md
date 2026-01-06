# LIBRARY MANAGEMENT SYSTEM - PROJECT REPORT

---

## PAGE 1: TITLE PAGE

**LIBRARY MANAGEMENT SYSTEM**

**Visual Basic .NET - Windows Forms Application**

*(Group Members: To be filled by student)*

---

## PAGE 2: INTRODUCTION TO THE PROJECT

### Project Overview

The Library Management System is a comprehensive desktop application developed using Visual Basic .NET with Windows Forms. This system provides a complete solution for managing library operations including book inventory, member registration, book issuance, and return processing.

### Why This Project?

Libraries are essential institutions in educational and community settings. Managing a library manually can be time-consuming, error-prone, and inefficient. This Library Management System was developed to:

1. **Automate Operations**: Reduce manual paperwork and streamline library processes
2. **Improve Accuracy**: Minimize human errors in record-keeping and transaction processing
3. **Enhance Efficiency**: Enable quick search and retrieval of books and member information
4. **Track Inventory**: Maintain real-time tracking of book availability and issued books
5. **Calculate Fines**: Automatically compute late return fines
6. **Maintain Records**: Keep comprehensive digital records of all library transactions

### Problem Statement

Traditional library management faces several challenges:
- Manual record-keeping is time-consuming and prone to errors
- Difficulty in tracking book availability and location
- Inefficient member registration and verification process
- Manual calculation of late fees is error-prone
- No centralized database for quick information retrieval
- Difficulty in generating reports and statistics

### Solution

Our Library Management System addresses these challenges by providing:
- User-friendly graphical interface for all operations
- Automated book and member ID generation
- Real-time inventory tracking
- Automated fine calculation for late returns
- Efficient search and filter capabilities
- File-based data storage for easy portability
- Comprehensive validation and error handling

### Steps Followed in Development

1. **Requirements Analysis**
   - Identified core features needed for library management
   - Determined user requirements and system capabilities
   - Defined data structures for books, members, and transactions

2. **System Design**
   - Designed user interface mockups for all forms
   - Created database schema for file-based storage
   - Planned navigation flow between different modules

3. **Implementation**
   - Developed Login form with authentication
   - Created Dashboard for centralized navigation
   - Implemented Book Management (Add/View Books)
   - Developed Member Management (Add/View Members)
   - Built Transaction Management (Issue/Return Books)
   - Integrated file-based data storage system

4. **Testing**
   - Performed unit testing on individual modules
   - Conducted integration testing
   - Validated error handling and edge cases
   - Tested data persistence and retrieval

5. **Documentation**
   - Created comprehensive user documentation
   - Documented code with detailed comments
   - Prepared project report with algorithms

### System Features

**1. User Authentication**
- Secure login system
- Password visibility toggle
- Input validation

**2. Book Management**
- Add new books with auto-generated Book IDs
- View all books with search functionality
- Delete books from inventory
- Track book quantity and availability

**3. Member Management**
- Register new members with auto-generated Member IDs
- View all members with search capability
- Delete member records
- Store member contact information

**4. Book Issuance**
- Verify book availability before issuance
- Verify member credentials
- Track issue and due dates
- Update book inventory automatically

**5. Book Returns**
- Process book returns
- Calculate late return fines automatically
- Update book availability
- Maintain return history

**6. Data Management**
- File-based storage using text files
- Data persistence across sessions
- Search and filter capabilities
- Data validation and error handling

---

## PAGE 3: ALGORITHMS USED

### Algorithm 1: User Login Authentication

**Pseudocode:**
```
ALGORITHM: UserLogin
INPUT: username, password
OUTPUT: Authentication result

BEGIN
    IF username is empty OR password is empty THEN
        DISPLAY "Please enter both username and password"
        RETURN False
    END IF
    
    // Check against default credentials
    IF username == "admin" AND password == "admin123" THEN
        RETURN True
    END IF
    
    // Check against users file
    IF users.txt exists THEN
        FOR each line IN users.txt DO
            Split line by "|" into parts
            IF parts[0] == username AND parts[1] == password THEN
                RETURN True
            END IF
        END FOR
    END IF
    
    RETURN False
END
```

### Algorithm 2: Generate Book ID

**Pseudocode:**
```
ALGORITHM: GenerateNextBookID
INPUT: None
OUTPUT: Next book ID (e.g., "BK0001")

BEGIN
    IF books.txt exists AND not empty THEN
        Read all lines from books.txt
        Get last line
        Split last line by "|"
        Extract book ID from parts[0]
        Remove "BK" prefix to get number
        Parse number as integer
        Increment number by 1
        Format as "BK" + number with 4 digits (padded with zeros)
        RETURN formatted ID
    ELSE
        RETURN "BK0001"
    END IF
END
```

### Algorithm 3: Add New Book

**Pseudocode:**
```
ALGORITHM: AddBook
INPUT: bookID, title, author, isbn, quantity, category
OUTPUT: Success or error message

BEGIN
    // Validation
    IF title is empty THEN
        DISPLAY "Please enter the book title"
        RETURN False
    END IF
    
    IF author is empty THEN
        DISPLAY "Please enter the author name"
        RETURN False
    END IF
    
    IF isbn is empty OR not numeric THEN
        DISPLAY "Please enter a valid ISBN"
        RETURN False
    END IF
    
    IF quantity is empty OR not positive integer THEN
        DISPLAY "Quantity must be a positive number"
        RETURN False
    END IF
    
    IF category is empty THEN
        DISPLAY "Please select a category"
        RETURN False
    END IF
    
    // Create record
    bookRecord = bookID + "|" + title + "|" + author + "|" + isbn + "|" + quantity + "|" + category
    
    // Save to file
    APPEND bookRecord to books.txt
    
    DISPLAY "Book added successfully"
    RETURN True
END
```

### Algorithm 4: Search Books

**Pseudocode:**
```
ALGORITHM: SearchBooks
INPUT: searchText
OUTPUT: List of matching books

BEGIN
    Initialize empty results list
    
    IF books.txt exists THEN
        Read all lines from books.txt
        
        FOR each line IN lines DO
            IF line is not empty THEN
                Split line by "|" into parts
                
                // Check if search text matches any field
                IF searchText is empty OR
                   parts[0] contains searchText OR       // Book ID
                   parts[1] contains searchText OR       // Title
                   parts[2] contains searchText OR       // Author
                   parts[5] contains searchText THEN     // Category
                    
                    Add parts to results list
                END IF
            END IF
        END FOR
    END IF
    
    RETURN results list
END
```

### Algorithm 5: Issue Book

**Pseudocode:**
```
ALGORITHM: IssueBook
INPUT: bookID, memberID, issueDate, dueDate
OUTPUT: Success or error message

BEGIN
    // Verify book exists and is available
    bookInfo = GetBookInfo(bookID)
    IF bookInfo is empty THEN
        DISPLAY "Book not found"
        RETURN False
    END IF
    
    Split bookInfo by "|" into bookParts
    IF bookParts[4] <= 0 THEN  // quantity
        DISPLAY "Book is out of stock"
        RETURN False
    END IF
    
    // Verify member exists
    memberInfo = GetMemberInfo(memberID)
    IF memberInfo is empty THEN
        DISPLAY "Member not found"
        RETURN False
    END IF
    
    // Generate issue ID
    issueID = GenerateNextIssueID()
    
    // Create issue record
    issueRecord = issueID + "|" + bookID + "|" + memberID + "|" + 
                  issueDate + "|" + dueDate + "|" + bookTitle + "|Active"
    
    // Save to file
    APPEND issueRecord to issued_books.txt
    
    // Update book quantity
    UpdateBookQuantity(bookID, -1)
    
    DISPLAY "Book issued successfully"
    RETURN True
END
```

### Algorithm 6: Return Book and Calculate Fine

**Pseudocode:**
```
ALGORITHM: ReturnBook
INPUT: issueID, returnDate
OUTPUT: Success message with fine amount

BEGIN
    // Get issue details
    Read issued_books.txt
    Find record with matching issueID
    IF not found THEN
        DISPLAY "Issue record not found"
        RETURN False
    END IF
    
    Split record by "|" into parts
    bookID = parts[1]
    dueDate = parts[4]
    
    // Calculate fine
    fine = 0
    IF returnDate > dueDate THEN
        daysLate = returnDate - dueDate (in days)
        fine = daysLate * 0.50  // $0.50 per day
    END IF
    
    // Update issue record
    newRecord = parts[0] + "|" + parts[1] + "|" + parts[2] + "|" + 
                parts[3] + "|" + parts[4] + "|" + parts[5] + 
                "|Returned|" + returnDate + "|" + fine
    
    Replace old record with newRecord in issued_books.txt
    
    // Update book quantity
    UpdateBookQuantity(bookID, +1)
    
    IF fine > 0 THEN
        DISPLAY "Book returned. Late fee: $" + fine
    ELSE
        DISPLAY "Book returned successfully"
    END IF
    
    RETURN True
END
```

### Algorithm 7: Input Validation (Generic)

**Pseudocode:**
```
ALGORITHM: ValidateInput
INPUT: fieldName, value, dataType, required
OUTPUT: True if valid, False otherwise

BEGIN
    // Check if required field is empty
    IF required AND value is empty THEN
        DISPLAY fieldName + " is required"
        RETURN False
    END IF
    
    // Skip further validation if not required and empty
    IF NOT required AND value is empty THEN
        RETURN True
    END IF
    
    // Type-specific validation
    CASE dataType OF
        "numeric":
            IF value is not numeric THEN
                DISPLAY fieldName + " must be a number"
                RETURN False
            END IF
            
        "email":
            IF value does not match email pattern THEN
                DISPLAY "Please enter a valid email address"
                RETURN False
            END IF
            
        "phone":
            IF value is not numeric THEN
                DISPLAY "Phone number must contain only digits"
                RETURN False
            END IF
            
        "positive_integer":
            IF value is not integer OR value <= 0 THEN
                DISPLAY fieldName + " must be a positive number"
                RETURN False
            END IF
    END CASE
    
    RETURN True
END
```

### Algorithm 8: Data Persistence (Save/Load)

**Pseudocode:**
```
ALGORITHM: SaveRecord
INPUT: filename, recordData
OUTPUT: Success or error

BEGIN
    TRY
        OPEN file filename in append mode
        WRITE recordData to file
        ADD new line character
        CLOSE file
        RETURN True
    CATCH exception
        DISPLAY "Error saving record: " + exception message
        RETURN False
    END TRY
END

ALGORITHM: LoadRecords
INPUT: filename
OUTPUT: Array of records

BEGIN
    Initialize empty records array
    
    TRY
        IF file filename exists THEN
            OPEN file filename in read mode
            READ all lines into lines array
            CLOSE file
            
            FOR each line IN lines DO
                IF line is not empty THEN
                    ADD line to records array
                END IF
            END FOR
        END IF
        
        RETURN records array
    CATCH exception
        DISPLAY "Error loading records: " + exception message
        RETURN empty array
    END TRY
END
```

---
 