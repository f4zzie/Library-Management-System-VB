' ====================================================================
' ValidationHelper Class
' Provides validation methods for user input
' Demonstrates SELECTION structure extensively
' ====================================================================

Public Class ValidationHelper
    ' SELECTION: Validate if string is not empty
    Public Shared Function IsNotEmpty(value As String) As Boolean
        If String.IsNullOrWhiteSpace(value) Then
            Return False
        Else
            Return True
        End If
    End Function

    ' SELECTION: Validate email format (basic check)
    Public Shared Function IsValidEmail(email As String) As Boolean
        If String.IsNullOrWhiteSpace(email) Then
            Return False
        End If

        ' Check if email contains @ and .
        If email.Contains("@") And email.Contains(".") Then
            Dim atPosition As Integer = email.IndexOf("@")
            Dim dotPosition As Integer = email.LastIndexOf(".")

            ' SELECTION: Check if @ comes before .
            If atPosition < dotPosition And atPosition > 0 And dotPosition < email.Length - 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    ' SELECTION: Validate phone number (basic check - only numbers)
    Public Shared Function IsValidPhone(phone As String) As Boolean
        If String.IsNullOrWhiteSpace(phone) Then
            Return False
        End If

        ' Remove common separators
        Dim cleanPhone As String = phone.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "")

        ' SELECTION & ITERATION: Check if all characters are digits
        If cleanPhone.Length >= 10 Then
            For Each c As Char In cleanPhone
                If Not Char.IsDigit(c) Then
                    Return False
                End If
            Next
            Return True
        Else
            Return False
        End If
    End Function

    ' SELECTION: Validate number (positive integer)
    Public Shared Function IsValidPositiveNumber(value As String) As Boolean
        Dim number As Integer
        If Integer.TryParse(value, number) Then
            If number > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    ' SELECTION: Validate ISBN format (basic check)
    Public Shared Function IsValidISBN(isbn As String) As Boolean
        If String.IsNullOrWhiteSpace(isbn) Then
            Return False
        End If

        ' Remove hyphens and spaces
        Dim cleanISBN As String = isbn.Replace("-", "").Replace(" ", "")

        ' SELECTION: Check if length is 10 or 13
        If cleanISBN.Length = 10 Or cleanISBN.Length = 13 Then
            Return True
        Else
            Return False
        End If
    End Function

    ' SELECTION: Validate year (between 1000 and current year)
    Public Shared Function IsValidYear(year As String) As Boolean
        Dim yearNum As Integer
        If Integer.TryParse(year, yearNum) Then
            If yearNum >= 1000 And yearNum <= Date.Today.Year Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    ' SELECTION: Validate password strength
    Public Shared Function IsValidPassword(password As String) As Boolean
        If String.IsNullOrWhiteSpace(password) Then
            Return False
        End If

        ' Password must be at least 6 characters
        If password.Length >= 6 Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Display validation error message
    Public Shared Sub ShowValidationError(fieldName As String, message As String)
        MessageBox.Show(message, "Validation Error: " & fieldName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub
End Class
