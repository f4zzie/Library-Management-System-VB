' ====================================================================
' Transaction Model Class
' Represents a book borrowing transaction
' Uses basic properties to track book issues and returns
' ====================================================================

Public Class Transaction
    ' SEQUENTIAL STRUCTURE: Properties defined one after another
    Public Property TransactionID As Integer
    Public Property BookID As Integer
    Public Property BookTitle As String
    Public Property MemberID As Integer
    Public Property MemberName As String
    Public Property IssueDate As Date
    Public Property DueDate As Date
    Public Property ReturnDate As Date
    Public Property Status As String ' Issued, Returned, Overdue
    Public Property Fine As Decimal

    ' Constructor
    Public Sub New()
        TransactionID = 0
        BookID = 0
        BookTitle = ""
        MemberID = 0
        MemberName = ""
        IssueDate = Date.Today
        DueDate = Date.Today.AddDays(14) ' 14 days loan period
        ReturnDate = Nothing
        Status = "Issued"
        Fine = 0
    End Sub

    ' Parameterized Constructor
    Public Sub New(id As Integer, bookId As Integer, bookTitle As String,
                   memberId As Integer, memberName As String,
                   issueDate As Date, dueDate As Date)
        TransactionID = id
        BookID = bookId
        Me.BookTitle = bookTitle
        MemberID = memberId
        Me.MemberName = memberName
        Me.IssueDate = issueDate
        Me.DueDate = dueDate
        ReturnDate = Nothing
        Status = "Issued"
        Fine = 0
    End Sub

    ' SELECTION STRUCTURE: Check if book is overdue
    Public Function IsOverdue() As Boolean
        If Status = "Issued" And Date.Today > DueDate Then
            Return True
        Else
            Return False
        End If
    End Function

    ' SELECTION STRUCTURE: Calculate fine for overdue books
    Public Function CalculateFine() As Decimal
        If IsOverdue() Then
            Dim daysOverdue As Integer = DateDiff(DateInterval.Day, DueDate, Date.Today)
            Dim finePerDay As Decimal = 5.0 ' $5 per day
            Return daysOverdue * finePerDay
        Else
            Return 0
        End If
    End Function

    ' Get transaction information
    Public Function GetTransactionInfo() As String
        Return String.Format("ID: {0}, Book: {1}, Member: {2}, Status: {3}, Due: {4}",
                           TransactionID, BookTitle, MemberName, Status, DueDate.ToShortDateString())
    End Function
End Class
