' ====================================================================
' Overdue Report Form - Demonstrates ITERATION and SELECTION for reporting
' ====================================================================

Public Class OverdueReportForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()

        Me.Text = "Overdue Books Report"
        Me.Size = New Size(900, 550)
        Me.StartPosition = FormStartPosition.CenterScreen
        LoadOverdueBooks()
    End Sub

    ' ITERATION & SELECTION: Load overdue transactions
    Private Sub LoadOverdueBooks()
        lvOverdue.Items.Clear()

        Dim overdueTransactions As List(Of Transaction) = DataStore.GetOverdueTransactions()
        Dim totalFines As Decimal = 0

        ' ITERATION: Loop through overdue transactions
        For Each trans As Transaction In overdueTransactions
            Dim item As New ListViewItem(trans.TransactionID.ToString())
            item.SubItems.Add(trans.BookTitle)
            item.SubItems.Add(trans.MemberName)
            item.SubItems.Add(trans.IssueDate.ToShortDateString())
            item.SubItems.Add(trans.DueDate.ToShortDateString())

            ' Calculate days overdue
            Dim daysOverdue As Integer = DateDiff(DateInterval.Day, trans.DueDate, Date.Today)
            item.SubItems.Add(daysOverdue.ToString())

            ' Calculate fine
            Dim fine As Decimal = trans.CalculateFine()
            item.SubItems.Add("$" & fine.ToString("F2"))

            totalFines += fine
            item.ForeColor = Color.Red

            lvOverdue.Items.Add(item)
        Next

        ' Update summary
        lblOverdueCount.Text = "Overdue Books: " & overdueTransactions.Count.ToString()
        lblTotalFines.Text = "Total Fines: $" & totalFines.ToString("F2")

        ' SELECTION: Display warning if many overdue books
        If overdueTransactions.Count > 5 Then
            lblWarning.Text = "? WARNING: High number of overdue books!"
            lblWarning.ForeColor = Color.Red
            lblWarning.Visible = True
        Else
            lblWarning.Visible = False
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadOverdueBooks()
        MessageHelper.ShowSuccess("Report refreshed!")
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        MessageHelper.ShowInfo("Print functionality - Coming soon!" & vbCrLf &
                             "This would generate a printable report.")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.lvOverdue = New ListView()
        Me.lblOverdueCount = New Label()
        Me.lblTotalFines = New Label()
        Me.lblWarning = New Label()
        Me.btnRefresh = New Button()
        Me.btnPrint = New Button()
        Me.btnClose = New Button()
        Me.SuspendLayout()

        Me.lblOverdueCount.Location = New Point(20, 20)
        Me.lblOverdueCount.Size = New Size(200, 20)
        Me.lblOverdueCount.Text = "Overdue Books: 0"
        Me.lblOverdueCount.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)

        Me.lblTotalFines.Location = New Point(230, 20)
        Me.lblTotalFines.Size = New Size(200, 20)
        Me.lblTotalFines.Text = "Total Fines: $0.00"
        Me.lblTotalFines.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        Me.lblTotalFines.ForeColor = Color.Red

        Me.lblWarning.Location = New Point(450, 20)
        Me.lblWarning.Size = New Size(400, 20)
        Me.lblWarning.Text = "? WARNING: High number of overdue books!"
        Me.lblWarning.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        Me.lblWarning.ForeColor = Color.Red
        Me.lblWarning.Visible = False

        Me.lvOverdue.Location = New Point(20, 50)
        Me.lvOverdue.Size = New Size(850, 420)
        Me.lvOverdue.View = View.Details
        Me.lvOverdue.GridLines = True
        Me.lvOverdue.FullRowSelect = True
        Me.lvOverdue.Columns.Add("Trans ID", 70)
        Me.lvOverdue.Columns.Add("Book Title", 240)
        Me.lvOverdue.Columns.Add("Member", 180)
        Me.lvOverdue.Columns.Add("Issue Date", 90)
        Me.lvOverdue.Columns.Add("Due Date", 90)
        Me.lvOverdue.Columns.Add("Days Late", 80)
        Me.lvOverdue.Columns.Add("Fine", 90)

        Me.btnRefresh.Location = New Point(20, 480)
        Me.btnRefresh.Size = New Size(100, 30)
        Me.btnRefresh.Text = "Refresh"

        Me.btnPrint.Location = New Point(130, 480)
        Me.btnPrint.Size = New Size(100, 30)
        Me.btnPrint.Text = "Print Report"

        Me.btnClose.Location = New Point(770, 480)
        Me.btnClose.Size = New Size(100, 30)
        Me.btnClose.Text = "Close"

        Me.ClientSize = New Size(900, 530)
        Me.Controls.Add(btnClose)
        Me.Controls.Add(btnPrint)
        Me.Controls.Add(btnRefresh)
        Me.Controls.Add(lblWarning)
        Me.Controls.Add(lblTotalFines)
        Me.Controls.Add(lblOverdueCount)
        Me.Controls.Add(lvOverdue)
        Me.Name = "OverdueReportForm"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents lvOverdue As ListView
    Friend WithEvents lblOverdueCount, lblTotalFines, lblWarning As Label
    Friend WithEvents btnRefresh, btnPrint, btnClose As Button
End Class
