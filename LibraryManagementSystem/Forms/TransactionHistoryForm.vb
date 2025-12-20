' ====================================================================
' Transaction History Form - Demonstrates ITERATION and SELECTION
' ====================================================================

Public Class TransactionHistoryForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
        
        Me.Text = "Transaction History"
        Me.Size = New Size(900, 550)
        Me.StartPosition = FormStartPosition.CenterScreen
        LoadAllTransactions()
    End Sub

    ' ITERATION: Load all transactions
    Private Sub LoadAllTransactions()
        lvTransactions.Items.Clear()

        For Each trans As Transaction In DataStore.Transactions
            Dim item As New ListViewItem(trans.TransactionID.ToString())
            item.SubItems.Add(trans.BookTitle)
            item.SubItems.Add(trans.MemberName)
            item.SubItems.Add(trans.IssueDate.ToShortDateString())
            item.SubItems.Add(trans.DueDate.ToShortDateString())
            item.SubItems.Add(If(trans.ReturnDate = Nothing, "-", trans.ReturnDate.ToShortDateString()))
            item.SubItems.Add(trans.Status)
            item.SubItems.Add("$" & trans.Fine.ToString("F2"))

            ' SELECTION: Color code by status
            If trans.Status = "Returned" Then
                item.ForeColor = Color.Gray
            ElseIf trans.IsOverdue() Then
                item.ForeColor = Color.Red
            Else
                item.ForeColor = Color.Green
            End If

            lvTransactions.Items.Add(item)
        Next

        lblTotal.Text = "Total Transactions: " & DataStore.Transactions.Count.ToString()
    End Sub

    ' SELECTION: Filter by status
    Private Sub btnFilterActive_Click(sender As Object, e As EventArgs) Handles btnFilterActive.Click
        lvTransactions.Items.Clear()

        For Each trans As Transaction In DataStore.Transactions
            If trans.Status = "Issued" Then
                Dim item As New ListViewItem(trans.TransactionID.ToString())
                item.SubItems.Add(trans.BookTitle)
                item.SubItems.Add(trans.MemberName)
                item.SubItems.Add(trans.IssueDate.ToShortDateString())
                item.SubItems.Add(trans.DueDate.ToShortDateString())
                item.SubItems.Add("-")
                item.SubItems.Add(trans.Status)
                item.SubItems.Add("$0.00")
                lvTransactions.Items.Add(item)
            End If
        Next

        lblTotal.Text = "Active Transactions: " & lvTransactions.Items.Count.ToString()
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        LoadAllTransactions()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.lvTransactions = New ListView()
        Me.lblTotal = New Label()
        Me.btnFilterActive = New Button()
        Me.btnShowAll = New Button()
        Me.btnClose = New Button()
        Me.SuspendLayout()

        Me.lblTotal.Location = New Point(20, 20)
        Me.lblTotal.Size = New Size(250, 20)
        Me.lblTotal.Text = "Total Transactions: 0"
        Me.lblTotal.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)

        Me.lvTransactions.Location = New Point(20, 50)
        Me.lvTransactions.Size = New Size(850, 420)
        Me.lvTransactions.View = View.Details
        Me.lvTransactions.GridLines = True
        Me.lvTransactions.FullRowSelect = True
        Me.lvTransactions.Columns.Add("Trans ID", 70)
        Me.lvTransactions.Columns.Add("Book Title", 220)
        Me.lvTransactions.Columns.Add("Member", 150)
        Me.lvTransactions.Columns.Add("Issue Date", 90)
        Me.lvTransactions.Columns.Add("Due Date", 90)
        Me.lvTransactions.Columns.Add("Return Date", 90)
        Me.lvTransactions.Columns.Add("Status", 80)
        Me.lvTransactions.Columns.Add("Fine", 60)

        Me.btnFilterActive.Location = New Point(20, 480)
        Me.btnFilterActive.Size = New Size(120, 30)
        Me.btnFilterActive.Text = "Show Active Only"

        Me.btnShowAll.Location = New Point(150, 480)
        Me.btnShowAll.Size = New Size(100, 30)
        Me.btnShowAll.Text = "Show All"

        Me.btnClose.Location = New Point(770, 480)
        Me.btnClose.Size = New Size(100, 30)
        Me.btnClose.Text = "Close"

        Me.ClientSize = New Size(900, 530)
        Me.Controls.Add(btnClose)
        Me.Controls.Add(btnShowAll)
        Me.Controls.Add(btnFilterActive)
        Me.Controls.Add(lblTotal)
        Me.Controls.Add(lvTransactions)
        Me.Name = "TransactionHistoryForm"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents lvTransactions As ListView
    Friend WithEvents lblTotal As Label
    Friend WithEvents btnFilterActive, btnShowAll, btnClose As Button
End Class
