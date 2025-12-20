' ====================================================================
' User Model Class
' Represents a system user (Admin, Librarian, or Student)
' Uses basic properties for authentication
' ====================================================================

Public Class User
    ' SEQUENTIAL STRUCTURE: Properties defined one after another
    Public Property UserID As Integer
    Public Property Username As String
    Public Property Password As String
    Public Property FullName As String
    Public Property Role As String ' Admin, Librarian, or Student
    Public Property Email As String
    Public Property MemberID As Integer ' Link to Member for students

    ' Constructor
    Public Sub New()
        UserID = 0
        Username = ""
        Password = ""
        FullName = ""
        Role = "Librarian"
        Email = ""
        MemberID = 0
    End Sub

    ' Parameterized Constructor
    Public Sub New(id As Integer, username As String, password As String,
                   fullName As String, role As String, email As String)
        UserID = id
        Me.Username = username
        Me.Password = password
        Me.FullName = fullName
        Me.Role = role
        Me.Email = email
        MemberID = 0
    End Sub

    ' SELECTION STRUCTURE: Check if user is admin
    Public Function IsAdmin() As Boolean
        If Role = "Admin" Then
            Return True
        Else
            Return False
        End If
    End Function

    ' SELECTION STRUCTURE: Check if user is student
    Public Function IsStudent() As Boolean
        If Role = "Student" Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Get user information
    Public Function GetUserInfo() As String
        Return String.Format("Username: {0}, Name: {1}, Role: {2}",
                           Username, FullName, Role)
    End Function
End Class
