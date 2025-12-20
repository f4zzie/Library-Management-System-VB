' ====================================================================
' Issue Book Form - Demonstrates ITERATION and SELECTION
' ====================================================================

Public Class IssueBookForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()

        Me.Text = "Issue Book to Member"
        Me.Size = New Size(600, 400)
        Me.StartPosition = FormStartPosition.CenterScreen
        LoadAvailableBooks()
        LoadActiveMembers()
    End Sub

    ' ITERATION: Load available books into combo box
    Private Sub LoadAvailableBooks()
        cboBook.Items.Clear()
        For Each book As Book In DataStore.GetAvailableBooks()
            cboBook.Items.Add(book.BookID & " - " & book.Title & " (Available: " & book.AvailableQuantity & ")")
        Next
    End Sub

    ' ITERATION: Load active members into combo box
    Private Sub LoadActiveMembers()
        cboMember.Items.Clear()
        For Each member As Member In DataStore.Members
            If member.IsActive() Then
                cboMember.Items.Add(member.MemberID & " - " & member.FullName)
            End If
        Next
    End Sub

    ' SEQUENTIAL & SELECTION: Issue book
    Private Sub btnIssue_Click(sender As Object, e As EventArgs) Handles btnIssue.Click
        ' Validate selections
        If cboBook.SelectedIndex = -1 Then
            MessageHelper.ShowWarning("Please select a book")
            Return
        End If

        If cboMember.SelectedIndex = -1 Then
            MessageHelper.ShowWarning("Please select a member")
            Return
        End If

        ' Extract IDs
        Dim bookID As Integer = Integer.Parse(cboBook.Text.Split("-"c)(0).Trim())
        Dim memberID As Integer = Integer.Parse(cboMember.Text.Split("-"c)(0).Trim())

        Dim book As Book = DataStore.GetBookByID(bookID)
        Dim member As Member = DataStore.GetMemberByID(memberID)

        ' SELECTION: Check if book is available
        If Not book.IsAvailable() Then
            MessageHelper.ShowError("Book is not available!")
            Return
        End If

        ' Create transaction
        Dim issueDate As Date = dtpIssueDate.Value
        Dim dueDate As Date = dtpDueDate.Value

        Dim transaction As New Transaction()
        transaction.BookID = bookID
        transaction.BookTitle = book.Title
        transaction.MemberID = memberID
        transaction.MemberName = member.FullName
        transaction.IssueDate = issueDate
        transaction.DueDate = dueDate
        transaction.Status = "Issued"

        DataStore.AddTransaction(transaction)
        DataStore.UpdateBookAvailability()

        MessageHelper.ShowSuccess("Book issued successfully!" & vbCrLf &
                                "Transaction ID: " & transaction.TransactionID & vbCrLf &
                                "Due Date: " & dueDate.ToShortDateString())

        LoadAvailableBooks()
        ClearForm()
    End Sub

    Private Sub ClearForm()
        cboBook.SelectedIndex = -1
        cboMember.SelectedIndex = -1
        dtpIssueDate.Value = Date.Today
        dtpDueDate.Value = Date.Today.AddDays(14)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.lblBook = New Label()
        Me.lblMember = New Label()
        Me.lblIssueDate = New Label()
        Me.lblDueDate = New Label()
        Me.cboBook = New ComboBox()
        Me.cboMember = New ComboBox()
        Me.dtpIssueDate = New DateTimePicker()
        Me.dtpDueDate = New DateTimePicker()
        Me.btnIssue = New Button()
        Me.btnClose = New Button()
        Me.SuspendLayout()

        Me.lblBook.Location = New Point(50, 40)
        Me.lblBook.Size = New Size(100, 20)
        Me.lblBook.Text = "Select Book:"

        Me.cboBook.Location = New Point(160, 37)
        Me.cboBook.Size = New Size(380, 21)
        Me.cboBook.DropDownStyle = ComboBoxStyle.DropDownList

        Me.lblMember.Location = New Point(50, 90)
        Me.lblMember.Size = New Size(100, 20)
        Me.lblMember.Text = "Select Member:"

        Me.cboMember.Location = New Point(160, 87)
        Me.cboMember.Size = New Size(380, 21)
        Me.cboMember.DropDownStyle = ComboBoxStyle.DropDownList

        Me.lblIssueDate.Location = New Point(50, 140)
        Me.lblIssueDate.Size = New Size(100, 20)
        Me.lblIssueDate.Text = "Issue Date:"

        Me.dtpIssueDate.Location = New Point(160, 137)
        Me.dtpIssueDate.Size = New Size(200, 20)
        Me.dtpIssueDate.Value = Date.Today

        Me.lblDueDate.Location = New Point(50, 190)
        Me.lblDueDate.Size = New Size(100, 20)
        Me.lblDueDate.Text = "Due Date:"

        Me.dtpDueDate.Location = New Point(160, 187)
        Me.dtpDueDate.Size = New Size(200, 20)
        Me.dtpDueDate.Value = Date.Today.AddDays(14)

        Me.btnIssue.Location = New Point(200, 260)
        Me.btnIssue.Size = New Size(100, 35)
        Me.btnIssue.Text = "Issue Book"

        Me.btnClose.Location = New Point(320, 260)
        Me.btnClose.Size = New Size(100, 35)
        Me.btnClose.Text = "Close"

        Me.ClientSize = New Size(600, 330)
        Me.Controls.Add(btnClose)
        Me.Controls.Add(btnIssue)
        Me.Controls.Add(dtpDueDate)
        Me.Controls.Add(dtpIssueDate)
        Me.Controls.Add(cboMember)
        Me.Controls.Add(cboBook)
        Me.Controls.Add(lblDueDate)
        Me.Controls.Add(lblIssueDate)
        Me.Controls.Add(lblMember)
        Me.Controls.Add(lblBook)
        Me.Name = "IssueBookForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblBook, lblMember, lblIssueDate, lblDueDate As Label
    Friend WithEvents cboBook, cboMember As ComboBox
    Friend WithEvents dtpIssueDate, dtpDueDate As DateTimePicker
    Friend WithEvents btnIssue, btnClose As Button
End Class
