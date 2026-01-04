Imports System.IO

Public Class Form1
    'default credentials
    Private Const DEFAULT_USERNAME As String = "admin"
    Private Const DEFAULT_PASSWORD As String = "admin123"

    'Form Load Event
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set password character
        TextBox1.UseSystemPasswordChar = True

        ' Set placeholder text
        TextBox2.Text = "Username"
        TextBox1.Text = "Password"
        TextBox2.ForeColor = Color.Gray
        TextBox1.ForeColor = Color.Gray
    End Sub

    ' Label Click Event
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' No action needed
    End Sub

    ' Panel Paint Event
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
        ' No action needed
    End Sub

    ' Exit Button Click Event
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Confirm before exit
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    ' Show/Hide Password Checkbox
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox1.UseSystemPasswordChar = False
        Else
            TextBox1.UseSystemPasswordChar = True
        End If
    End Sub

    ' Login Button Click Event
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Input Validation
            If String.IsNullOrWhiteSpace(TextBox2.Text) Or TextBox2.Text = "Username" Then
                MessageBox.Show("Please enter your username", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox2.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(TextBox1.Text) Or TextBox1.Text = "Password" Then
                MessageBox.Show("Please enter your password", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox1.Focus()
                Return
            End If

            ' Validate credentials
            If ValidateLogin(TextBox2.Text.Trim(), TextBox1.Text) Then
                ' Login successful
                MessageBox.Show("Login Successful! Welcome to Library Management System", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open Dashboard
                Dim dashboard As New Dashboard()
                Me.Hide()
                dashboard.ShowDialog()
                Me.Close()
            Else
                ' Login failed
                MessageBox.Show("Invalid username or password. Please try again." & vbNewLine & vbNewLine & "Default: admin / admin123", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Clear()
                TextBox1.UseSystemPasswordChar = True
                TextBox1.Focus()
            End If

        Catch ex As Exception
            ' Error handling
            MessageBox.Show("An error occurred during login: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Function to validate login credentials
    Private Function ValidateLogin(username As String, password As String) As Boolean
        Try
            ' Check against default credentials
            If username.ToLower() = DEFAULT_USERNAME.ToLower() And password = DEFAULT_PASSWORD Then
                Return True
            End If

            ' Check against users file if it exists
            Dim usersFile As String = "users.txt"
            If File.Exists(usersFile) Then
                Dim lines() As String = File.ReadAllLines(usersFile)
                For Each line As String In lines
                    Dim parts() As String = line.Split("|"c)
                    If parts.Length >= 2 Then
                        If parts(0).ToLower() = username.ToLower() And parts(1) = password Then
                            Return True
                        End If
                    End If
                Next
            End If

            Return False

        Catch ex As Exception
            MessageBox.Show("Error reading user credentials: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' Username TextBox - Enter Event (Clear placeholder)
    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        If TextBox2.Text = "Username" Then
            TextBox2.Text = ""
            TextBox2.ForeColor = Color.Black
        End If
    End Sub

    ' Username TextBox - Leave Event (Restore placeholder)
    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            TextBox2.Text = "Username"
            TextBox2.ForeColor = Color.Gray
        End If
    End Sub

    ' Password TextBox - Enter Event (Clear placeholder)
    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        If TextBox1.Text = "Password" Then
            TextBox1.Text = ""
            TextBox1.ForeColor = Color.Black
            TextBox1.UseSystemPasswordChar = True
        End If
    End Sub

    ' Password TextBox - Leave Event (Restore placeholder)
    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            TextBox1.Text = "Password"
            TextBox1.ForeColor = Color.Gray
            TextBox1.UseSystemPasswordChar = False
        End If
    End Sub
End Class
