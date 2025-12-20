' ====================================================================
' Return Book Form - Demonstrates ITERATION, SELECTION, and fine calculation
' ====================================================================

Public Class ReturnBookForm
    Inherits System.Windows.Forms.Form

    Private selectedTransaction As Transaction

    Public Sub New()
        InitializeComponent()

        Me.Text = "Return Book"
        Me.Size = New Size(700, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
        LoadIssuedBooks()
    End Sub

    ' ITERATION: Load issued books
    Private Sub LoadIssuedBooks()
        lvIssued.Items.Clear()
        For Each trans As Transaction In DataStore.Transactions
            If trans.Status = "Issued" Then
                Dim item As New ListViewItem(trans.TransactionID.ToString())
                item.SubItems.Add(trans.BookTitle)
                item.SubItems.Add(trans.MemberName)
                item.SubItems.Add(trans.IssueDate.ToShortDateString())
                item.SubItems.Add(trans.DueDate.ToShortDateString())

                ' SELECTION: Highlight overdue
                If trans.IsOverdue() Then
                    item.ForeColor = Color.Red
                    item.SubItems.Add("OVERDUE")
                Else
                    item.ForeColor = Color.Green
                    item.SubItems.Add("On Time")
                End If

                lvIssued.Items.Add(item)
            End If
        Next
    End Sub

    ' Handle selection
    Private Sub lvIssued_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvIssued.SelectedIndexChanged
        If lvIssued.SelectedItems.Count > 0 Then
            Dim transID As Integer = Integer.Parse(lvIssued.SelectedItems(0).Text)

            ' Find transaction
            For Each trans As Transaction In DataStore.Transactions
                If trans.TransactionID = transID Then
                    selectedTransaction = trans
                    DisplayTransactionDetails()
                    Exit For
                End If
            Next
        End If
    End Sub

    ' SELECTION: Display transaction details and calculate fine
    Private Sub DisplayTransactionDetails()
        If selectedTransaction IsNot Nothing Then
            lblBookTitle.Text = "Book: " & selectedTransaction.BookTitle
            lblMemberName.Text = "Member: " & selectedTransaction.MemberName
            lblIssueDate.Text = "Issued: " & selectedTransaction.IssueDate.ToShortDateString()
            lblDueDate.Text = "Due: " & selectedTransaction.DueDate.ToShortDateString()

            If selectedTransaction.IsOverdue() Then
                Dim fine As Decimal = selectedTransaction.CalculateFine()
                lblFine.Text = "Fine: $" & fine.ToString("F2")
                lblFine.ForeColor = Color.Red
            Else
                lblFine.Text = "Fine: $0.00"
                lblFine.ForeColor = Color.Green
            End If

            btnReturn.Enabled = True
        End If
    End Sub

    ' Return book
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        If selectedTransaction Is Nothing Then
            MessageHelper.ShowWarning("Please select a transaction")
            Return
        End If

        If MessageHelper.ShowConfirmation("Confirm book return?") Then
            selectedTransaction.ReturnDate = Date.Today
            selectedTransaction.Status = "Returned"
            selectedTransaction.Fine = selectedTransaction.CalculateFine()

            DataStore.UpdateBookAvailability()

            MessageHelper.ShowSuccess("Book returned successfully!" & vbCrLf &
                                    If(selectedTransaction.Fine > 0,
                                       "Fine amount: $" & selectedTransaction.Fine.ToString("F2"),
                                       "No fine"))

            LoadIssuedBooks()
            ClearDetails()
        End If
    End Sub

    Private Sub ClearDetails()
        selectedTransaction = Nothing
        lblBookTitle.Text = "Book: -"
        lblMemberName.Text = "Member: -"
        lblIssueDate.Text = "Issued: -"
        lblDueDate.Text = "Due: -"
        lblFine.Text = "Fine: $0.00"
        btnReturn.Enabled = False
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.lvIssued = New ListView()
        Me.grpDetails = New GroupBox()
        Me.lblBookTitle = New Label()
        Me.lblMemberName = New Label()
        Me.lblIssueDate = New Label()
        Me.lblDueDate = New Label()
        Me.lblFine = New Label()
        Me.btnReturn = New Button()
        Me.btnClose = New Button()
        Me.SuspendLayout()

        Me.lvIssued.Location = New Point(20, 20)
        Me.lvIssued.Size = New Size(650, 280)
        Me.lvIssued.View = View.Details
        Me.lvIssued.GridLines = True
        Me.lvIssued.FullRowSelect = True
        Me.lvIssued.Columns.Add("Trans ID", 70)
        Me.lvIssued.Columns.Add("Book Title", 220)
        Me.lvIssued.Columns.Add("Member", 150)
        Me.lvIssued.Columns.Add("Issue Date", 90)
        Me.lvIssued.Columns.Add("Due Date", 90)
        Me.lvIssued.Columns.Add("Status", 80)

        Me.grpDetails.Location = New Point(20, 310)
        Me.grpDetails.Size = New Size(650, 120)
        Me.grpDetails.Text = "Transaction Details"

        Me.lblBookTitle.Location = New Point(20, 25)
        Me.lblBookTitle.Size = New Size(400, 20)
        Me.lblBookTitle.Text = "Book: -"

        Me.lblMemberName.Location = New Point(20, 50)
        Me.lblMemberName.Size = New Size(400, 20)
        Me.lblMemberName.Text = "Member: -"

        Me.lblIssueDate.Location = New Point(20, 75)
        Me.lblIssueDate.Size = New Size(200, 20)
        Me.lblIssueDate.Text = "Issued: -"

        Me.lblDueDate.Location = New Point(230, 75)
        Me.lblDueDate.Size = New Size(200, 20)
        Me.lblDueDate.Text = "Due: -"

        Me.lblFine.Location = New Point(440, 75)
        Me.lblFine.Size = New Size(150, 20)
        Me.lblFine.Text = "Fine: $0.00"
        Me.lblFine.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)

        Me.grpDetails.Controls.Add(lblFine)
        Me.grpDetails.Controls.Add(lblDueDate)
        Me.grpDetails.Controls.Add(lblIssueDate)
        Me.grpDetails.Controls.Add(lblMemberName)
        Me.grpDetails.Controls.Add(lblBookTitle)

        Me.btnReturn.Location = New Point(450, 445)
        Me.btnReturn.Size = New Size(100, 35)
        Me.btnReturn.Text = "Return Book"
        Me.btnReturn.Enabled = False

        Me.btnClose.Location = New Point(570, 445)
        Me.btnClose.Size = New Size(100, 35)
        Me.btnClose.Text = "Close"

        Me.ClientSize = New Size(700, 500)
        Me.Controls.Add(btnClose)
        Me.Controls.Add(btnReturn)
        Me.Controls.Add(grpDetails)
        Me.Controls.Add(lvIssued)
        Me.Name = "ReturnBookForm"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents lvIssued As ListView
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents lblBookTitle, lblMemberName, lblIssueDate, lblDueDate, lblFine As Label
    Friend WithEvents btnReturn, btnClose As Button
End Class
