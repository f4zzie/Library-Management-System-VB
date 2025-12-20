' ====================================================================
' MessageHelper Class
' Provides consistent message display methods
' Demonstrates SELECTION structure
' ====================================================================

Public Class MessageHelper
    ' Show success message
    Public Shared Sub ShowSuccess(message As String)
        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Show error message
    Public Shared Sub ShowError(message As String)
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ' Show warning message
    Public Shared Sub ShowWarning(message As String)
        MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' Show confirmation dialog - returns True if user clicks Yes
    Public Shared Function ShowConfirmation(message As String) As Boolean
        Dim result As DialogResult = MessageBox.Show(message, "Confirmation",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Show information message
    Public Shared Sub ShowInfo(message As String)
        MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
