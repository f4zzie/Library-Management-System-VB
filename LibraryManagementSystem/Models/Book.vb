' ====================================================================
' Book Model Class
' Represents a book in the library system
' Uses basic properties to store book information
' ====================================================================

Public Class Book
    ' SEQUENTIAL STRUCTURE: Properties defined one after another
    Public Property BookID As Integer
    Public Property ISBN As String
    Public Property Title As String
    Public Property Author As String
    Public Property Category As String
    Public Property Quantity As Integer
    Public Property AvailableQuantity As Integer
    Public Property Publisher As String
    Public Property YearPublished As Integer

    ' Constructor - Creates a new book object
    Public Sub New()
        ' Initialize with default values
        BookID = 0
        ISBN = ""
        Title = ""
        Author = ""
        Category = ""
        Quantity = 0
        AvailableQuantity = 0
        Publisher = ""
        YearPublished = 0
    End Sub

    ' Parameterized Constructor
    Public Sub New(id As Integer, isbn As String, title As String, author As String,
                   category As String, quantity As Integer, available As Integer,
                   publisher As String, year As Integer)
        BookID = id
        Me.ISBN = isbn
        Me.Title = title
        Me.Author = author
        Me.Category = category
        Me.Quantity = quantity
        AvailableQuantity = available
        Me.Publisher = publisher
        YearPublished = year
    End Sub

    ' Method to display book information
    Public Function GetBookInfo() As String
        Return String.Format("ID: {0}, Title: {1}, Author: {2}, Available: {3}/{4}",
                           BookID, Title, Author, AvailableQuantity, Quantity)
    End Function

    ' SELECTION STRUCTURE: Check if book is available
    Public Function IsAvailable() As Boolean
        If AvailableQuantity > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
