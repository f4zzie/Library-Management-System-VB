' ============================================
' Dashboard - Library Management System
' Main Menu with all navigation options
' ============================================

Public Class Dashboard
    ' Form Load Event
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set welcome message
        Label1.Text = "Library Management System - Dashboard"

        ' Initialize data files
        InitializeDataFiles()
    End Sub

    ' Initialize data files if they don't exist
    Private Sub InitializeDataFiles()
        Try
            ' Create books file if it doesn't exist
            If Not System.IO.File.Exists("books.txt") Then
                System.IO.File.Create("books.txt").Close()
            End If

            ' Create members file if it doesn't exist
            If Not System.IO.File.Exists("members.txt") Then
                System.IO.File.Create("members.txt").Close()
            End If

            ' Create issued books file if it doesn't exist
            If Not System.IO.File.Exists("issued_books.txt") Then
                System.IO.File.Create("issued_books.txt").Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error initializing data files: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Add Books Button
    Private Sub btnAddBooks_Click(sender As Object, e As EventArgs) Handles btnAddBooks.Click
        Try
            Dim addBooksForm As New AddBooks()
            addBooksForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening Add Books form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' View Books Button
    Private Sub btnViewBooks_Click(sender As Object, e As EventArgs) Handles btnViewBooks.Click
        Try
            Dim viewBooksForm As New ViewBooks()
            viewBooksForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening View Books form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Add Members Button
    Private Sub btnAddMembers_Click(sender As Object, e As EventArgs) Handles btnAddMembers.Click
        Try
            Dim addMembersForm As New AddMembers()
            addMembersForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening Add Members form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' View Members Button
    Private Sub btnViewMembers_Click(sender As Object, e As EventArgs) Handles btnViewMembers.Click
        Try
            Dim viewMembersForm As New ViewMembers()
            viewMembersForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening View Members form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Issue Books Button
    Private Sub btnIssueBooks_Click(sender As Object, e As EventArgs) Handles btnIssueBooks.Click
        Try
            Dim issueBooksForm As New IssueBooks()
            issueBooksForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening Issue Books form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Return Books Button
    Private Sub btnReturnBooks_Click(sender As Object, e As EventArgs) Handles btnReturnBooks.Click
        Try
            Dim returnBooksForm As New ReturnBooks()
            returnBooksForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening Return Books form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Logout Button
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Show login form again
                Dim loginForm As New Form1()
                Me.Close()
                loginForm.Show()
            End If

        Catch ex As Exception
            MessageBox.Show("Error during logout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Exit Button
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Application.Exit()
            End If

        Catch ex As Exception
            MessageBox.Show("Error during exit: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
