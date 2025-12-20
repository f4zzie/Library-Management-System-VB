' ====================================================================
' DataStore Class
' Stores all application data using Arrays and Lists
' This replaces database functionality for learning purposes
' Demonstrates SEQUENTIAL, SELECTION, and ITERATION structures
' ====================================================================

Imports System.Collections.Generic

Public Class DataStore
    ' Shared (Static) collections to hold data across the application
    Public Shared Users As New List(Of User)
    Public Shared Books As New List(Of Book)
    Public Shared Members As New List(Of Member)
    Public Shared Transactions As New List(Of Transaction)

    ' Current logged-in user
    Public Shared CurrentUser As User

    ' Counters for auto-generating IDs
    Public Shared NextBookID As Integer = 1
    Public Shared NextMemberID As Integer = 1
    Public Shared NextTransactionID As Integer = 1
    Public Shared NextUserID As Integer = 1

    ' ====================================================================
    ' INITIALIZATION: Load sample data when application starts
    ' Demonstrates SEQUENTIAL STRUCTURE - statements executed in order
    ' ====================================================================
    Public Shared Sub InitializeSampleData()
        ' Clear existing data
        Users.Clear()
        Books.Clear()
        Members.Clear()
        Transactions.Clear()

        ' SEQUENTIAL: Add sample users
        Users.Add(New User(1, "admin", "admin123", "John Administrator", "Admin", "admin@library.com"))
        Users.Add(New User(2, "librarian", "lib123", "Sarah Johnson", "Librarian", "sarah@library.com"))
        
        ' SEQUENTIAL: Add student users (linked to existing members)
        Dim student1 As New User(3, "alice", "student123", "Alice Brown", "Student", "alice@email.com")
        student1.MemberID = 1
        Users.Add(student1)
        
        Dim student2 As New User(4, "bob", "student123", "Bob Wilson", "Student", "bob@email.com")
        student2.MemberID = 2
        Users.Add(student2)
        
        Dim student3 As New User(5, "carol", "student123", "Carol Davis", "Student", "carol@email.com")
        student3.MemberID = 3
        Users.Add(student3)
        
        NextUserID = 6

        ' SEQUENTIAL: Add sample books
        Books.Add(New Book(1, "978-0-7475-3269-9", "Harry Potter and the Philosopher's Stone", "J.K. Rowling", "Fantasy", 5, 5, "Bloomsbury", 1997))
        Books.Add(New Book(2, "978-0-06-112008-4", "To Kill a Mockingbird", "Harper Lee", "Fiction", 3, 3, "Harper Collins", 1960))
        Books.Add(New Book(3, "978-0-14-028329-3", "1984", "George Orwell", "Science Fiction", 4, 4, "Penguin Books", 1949))
        Books.Add(New Book(4, "978-0-7432-7356-5", "The Great Gatsby", "F. Scott Fitzgerald", "Classic", 2, 2, "Scribner", 1925))
        Books.Add(New Book(5, "978-0-452-28423-4", "The Catcher in the Rye", "J.D. Salinger", "Fiction", 3, 3, "Little, Brown", 1951))
        Books.Add(New Book(6, "978-0-06-093546-7", "Pride and Prejudice", "Jane Austen", "Romance", 4, 4, "Modern Library", 1813))
        Books.Add(New Book(7, "978-0-14-017739-8", "Of Mice and Men", "John Steinbeck", "Fiction", 2, 2, "Penguin", 1937))
        Books.Add(New Book(8, "978-0-7434-8772-5", "The Da Vinci Code", "Dan Brown", "Mystery", 5, 5, "Doubleday", 2003))
        Books.Add(New Book(9, "978-0-316-76948-0", "The Hunger Games", "Suzanne Collins", "Adventure", 3, 3, "Scholastic", 2008))
        Books.Add(New Book(10, "978-0-545-01022-1", "Harry Potter and the Deathly Hallows", "J.K. Rowling", "Fantasy", 4, 4, "Scholastic", 2007))
        NextBookID = 11

        ' SEQUENTIAL: Add sample members
        Members.Add(New Member(1, "Alice Brown", "alice@email.com", "555-0101", "123 Main St", Date.Today.AddMonths(-6), "Active"))
        Members.Add(New Member(2, "Bob Wilson", "bob@email.com", "555-0102", "456 Oak Ave", Date.Today.AddMonths(-3), "Active"))
        Members.Add(New Member(3, "Carol Davis", "carol@email.com", "555-0103", "789 Pine Rd", Date.Today.AddMonths(-9), "Active"))
        Members.Add(New Member(4, "David Miller", "david@email.com", "555-0104", "321 Elm St", Date.Today.AddMonths(-12), "Active"))
        Members.Add(New Member(5, "Emma Garcia", "emma@email.com", "555-0105", "654 Maple Dr", Date.Today.AddMonths(-2), "Active"))
        NextMemberID = 6

        ' SEQUENTIAL: Add sample transactions (some issued books)
        Dim trans1 As New Transaction(1, 1, "Harry Potter and the Philosopher's Stone", 1, "Alice Brown", Date.Today.AddDays(-5), Date.Today.AddDays(9))
        trans1.Status = "Issued"
        Transactions.Add(trans1)

        Dim trans2 As New Transaction(2, 3, "1984", 2, "Bob Wilson", Date.Today.AddDays(-10), Date.Today.AddDays(4))
        trans2.Status = "Issued"
        Transactions.Add(trans2)

        ' Add a returned transaction
        Dim trans3 As New Transaction(3, 5, "The Catcher in the Rye", 3, "Carol Davis", Date.Today.AddDays(-20), Date.Today.AddDays(-6))
        trans3.ReturnDate = Date.Today.AddDays(-8)
        trans3.Status = "Returned"
        Transactions.Add(trans3)

        NextTransactionID = 4

        ' Update book availability based on transactions
        UpdateBookAvailability()
    End Sub

    ' ====================================================================
    ' ITERATION STRUCTURE: Loop through transactions to update book counts
    ' ====================================================================
    Public Shared Sub UpdateBookAvailability()
        ' Reset all books to full quantity
        For Each book As Book In Books
            book.AvailableQuantity = book.Quantity
        Next

        ' ITERATION: Loop through transactions and reduce available quantity for issued books
        For Each trans As Transaction In Transactions
            If trans.Status = "Issued" Then
                ' ITERATION: Find the book and reduce available quantity
                For Each book As Book In Books
                    If book.BookID = trans.BookID Then
                        book.AvailableQuantity -= 1
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    ' ====================================================================
    ' SEARCH METHODS - Demonstrate ITERATION and SELECTION structures
    ' ====================================================================

    ' ITERATION & SELECTION: Search for a user by username and password
    Public Shared Function FindUser(username As String, password As String) As User
        ' ITERATION: Loop through all users
        For Each user As User In Users
            ' SELECTION: Check if username and password match
            If user.Username = username And user.Password = password Then
                Return user ' Found the user
            End If
        Next
        Return Nothing ' User not found
    End Function

    ' ITERATION & SELECTION: Search books by title or author
    Public Shared Function SearchBooks(searchTerm As String) As List(Of Book)
        Dim results As New List(Of Book)

        ' ITERATION: Loop through all books
        For Each book As Book In Books
            ' SELECTION: Check if search term matches title or author
            If book.Title.ToLower().Contains(searchTerm.ToLower()) Or
               book.Author.ToLower().Contains(searchTerm.ToLower()) Then
                results.Add(book)
            End If
        Next

        Return results
    End Function

    ' ITERATION & SELECTION: Get available books only
    Public Shared Function GetAvailableBooks() As List(Of Book)
        Dim availableBooks As New List(Of Book)

        ' ITERATION: Loop through all books
        For Each book As Book In Books
            ' SELECTION: Check if book has available copies
            If book.AvailableQuantity > 0 Then
                availableBooks.Add(book)
            End If
        Next

        Return availableBooks
    End Function

    ' ITERATION & SELECTION: Get overdue transactions
    Public Shared Function GetOverdueTransactions() As List(Of Transaction)
        Dim overdueList As New List(Of Transaction)

        ' ITERATION: Loop through all transactions
        For Each trans As Transaction In Transactions
            ' SELECTION: Check if transaction is overdue
            If trans.IsOverdue() Then
                overdueList.Add(trans)
            End If
        Next

        Return overdueList
    End Function

    ' ITERATION: Get all issued transactions for a specific member
    Public Shared Function GetMemberTransactions(memberID As Integer) As List(Of Transaction)
        Dim memberTrans As New List(Of Transaction)

        ' ITERATION: Loop through all transactions
        For Each trans As Transaction In Transactions
            ' SELECTION: Check if transaction belongs to the member and is issued
            If trans.MemberID = memberID And trans.Status = "Issued" Then
                memberTrans.Add(trans)
            End If
        Next

        Return memberTrans
    End Function

    ' ====================================================================
    ' CRUD OPERATIONS - Create, Read, Update, Delete
    ' ====================================================================

    ' Add a new book
    Public Shared Sub AddBook(book As Book)
        book.BookID = NextBookID
        NextBookID += 1
        Books.Add(book)
    End Sub

    ' Add a new member
    Public Shared Sub AddMember(member As Member)
        member.MemberID = NextMemberID
        NextMemberID += 1
        Members.Add(member)
    End Sub

    ' Add a new transaction
    Public Shared Sub AddTransaction(trans As Transaction)
        trans.TransactionID = NextTransactionID
        NextTransactionID += 1
        Transactions.Add(trans)
    End Sub

    ' ITERATION & SELECTION: Find book by ID
    Public Shared Function GetBookByID(bookID As Integer) As Book
        For Each book As Book In Books
            If book.BookID = bookID Then
                Return book
            End If
        Next
        Return Nothing
    End Function

    ' ITERATION & SELECTION: Find member by ID
    Public Shared Function GetMemberByID(memberID As Integer) As Member
        For Each member As Member In Members
            If member.MemberID = memberID Then
                Return member
            End If
        Next
        Return Nothing
    End Function

    ' ITERATION & SELECTION: Delete book by ID
    Public Shared Function DeleteBook(bookID As Integer) As Boolean
        For i As Integer = 0 To Books.Count - 1
            If Books(i).BookID = bookID Then
                Books.RemoveAt(i)
                Return True
            End If
        Next
        Return False
    End Function

    ' ITERATION & SELECTION: Delete member by ID
    Public Shared Function DeleteMember(memberID As Integer) As Boolean
        For i As Integer = 0 To Members.Count - 1
            If Members(i).MemberID = memberID Then
                Members.RemoveAt(i)
                Return True
            End If
        Next
        Return False
    End Function
End Class
