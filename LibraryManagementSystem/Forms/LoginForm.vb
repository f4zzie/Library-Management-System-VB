' ====================================================================
' Login Form
' User authentication interface
' Demonstrates SEQUENTIAL, SELECTION, and ITERATION structures
' ====================================================================

Public Class LoginForm
    ' Form Load Event - SEQUENTIAL structure
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize sample data when application starts
        DataStore.InitializeSampleData()

        ' Set form properties
        Me.Text = "Library Management System - Login"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Size = New Size(400, 300)

        ' Initialize controls
        InitializeControls()

        ' Display login instructions
        MessageHelper.ShowInfo("Sample Login Credentials:" & vbCrLf &
                             "Admin: username='admin', password='admin123'" & vbCrLf &
                             "Librarian: username='librarian', password='lib123'" & vbCrLf &
                             "Student: username='alice', password='student123'")
    End Sub

    ' SEQUENTIAL: Initialize form controls
    Private Sub InitializeControls()
        ' Create and configure controls
        ' (In a real project, these would be designed in the Form Designer)
        ' This is a placeholder - actual form will be designed visually
    End Sub

    ' Login Button Click Event - SELECTION structure
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Get input values
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        ' SELECTION: Validate inputs
        If Not ValidationHelper.IsNotEmpty(username) Then
            MessageHelper.ShowError("Please enter username")
            txtUsername.Focus()
            Return
        End If

        If Not ValidationHelper.IsNotEmpty(password) Then
            MessageHelper.ShowError("Please enter password")
            txtPassword.Focus()
            Return
        End If

        ' SELECTION & ITERATION: Authenticate user
        Dim user As User = DataStore.FindUser(username, password)

        If user IsNot Nothing Then
            ' Login successful
            DataStore.CurrentUser = user

            MessageHelper.ShowSuccess("Welcome, " & user.FullName & "!")

            ' SELECTION: Route based on user role
            Me.Hide()
            
            If user.IsStudent() Then
                ' SELECTION: Show Student Dashboard for students
                Dim studentForm As New StudentDashboard()
                studentForm.ShowDialog()
            Else
                ' SELECTION: Show Main Dashboard for Admin/Librarian
                Dim mainForm As New MainDashboard()
                mainForm.ShowDialog()
            End If
            
            Me.Close()
        Else
            ' Login failed
            MessageHelper.ShowError("Invalid username or password!" & vbCrLf &
                                  "Please try again.")
            txtPassword.Clear()
            txtPassword.Focus()
        End If
    End Sub

    ' Exit Button Click Event
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        ' SELECTION: Confirm before exiting
        If MessageHelper.ShowConfirmation("Are you sure you want to exit?") Then
            Application.Exit()
        End If
    End Sub

    ' Form Closing Event
    Private Sub LoginForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Ensure application exits completely
        Application.Exit()
    End Sub
End Class
