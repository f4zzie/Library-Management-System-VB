' ====================================================================
' Member Model Class
' Represents a library member
' Uses basic properties to store member information
' ====================================================================

Public Class Member
    ' SEQUENTIAL STRUCTURE: Properties defined one after another
    Public Property MemberID As Integer
    Public Property FullName As String
    Public Property Email As String
    Public Property PhoneNumber As String
    Public Property Address As String
    Public Property MembershipDate As Date
    Public Property Status As String ' Active, Suspended, Expired

    ' Constructor
    Public Sub New()
        MemberID = 0
        FullName = ""
        Email = ""
        PhoneNumber = ""
        Address = ""
        MembershipDate = Date.Today
        Status = "Active"
    End Sub

    ' Parameterized Constructor
    Public Sub New(id As Integer, name As String, email As String,
                   phone As String, address As String, memberDate As Date, status As String)
        MemberID = id
        FullName = name
        Me.Email = email
        PhoneNumber = phone
        Me.Address = address
        MembershipDate = memberDate
        Me.Status = status
    End Sub

    ' Method to display member information
    Public Function GetMemberInfo() As String
        Return String.Format("ID: {0}, Name: {1}, Status: {2}, Phone: {3}",
                           MemberID, FullName, Status, PhoneNumber)
    End Function

    ' SELECTION STRUCTURE: Check if member is active
    Public Function IsActive() As Boolean
        If Status = "Active" Then
            Return True
        Else
            Return False
        End If
    End Function

    ' SELECTION STRUCTURE: Check if membership has expired (more than 1 year)
    Public Function IsMembershipExpired() As Boolean
        Dim daysDifference As Integer = DateDiff(DateInterval.Day, MembershipDate, Date.Today)
        If daysDifference > 365 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
