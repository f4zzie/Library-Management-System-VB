<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.grpStatistics = New System.Windows.Forms.GroupBox()
        Me.lblTotalBooks = New System.Windows.Forms.Label()
        Me.lblAvailableBooks = New System.Windows.Forms.Label()
        Me.lblTotalMembers = New System.Windows.Forms.Label()
        Me.lblActiveTransactions = New System.Windows.Forms.Label()
        Me.lblOverdue = New System.Windows.Forms.Label()
        Me.grpBookManagement = New System.Windows.Forms.GroupBox()
        Me.btnViewBooks = New System.Windows.Forms.Button()
        Me.btnAddBook = New System.Windows.Forms.Button()
        Me.btnSearchBooks = New System.Windows.Forms.Button()
        Me.grpMemberManagement = New System.Windows.Forms.GroupBox()
        Me.btnViewMembers = New System.Windows.Forms.Button()
        Me.btnAddMember = New System.Windows.Forms.Button()
        Me.grpTransactions = New System.Windows.Forms.GroupBox()
        Me.btnIssueBook = New System.Windows.Forms.Button()
        Me.btnReturnBook = New System.Windows.Forms.Button()
        Me.btnViewTransactions = New System.Windows.Forms.Button()
        Me.grpReports = New System.Windows.Forms.GroupBox()
        Me.btnOverdueReport = New System.Windows.Forms.Button()
        Me.btnManageUsers = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.grpStatistics.SuspendLayout()
        Me.grpBookManagement.SuspendLayout()
        Me.grpMemberManagement.SuspendLayout()
        Me.grpTransactions.SuspendLayout()
        Me.grpReports.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblWelcome
        '
        Me.lblWelcome.AutoSize = True
        Me.lblWelcome.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWelcome.Location = New System.Drawing.Point(30, 20)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(150, 24)
        Me.lblWelcome.TabIndex = 0
        Me.lblWelcome.Text = "Welcome, User"
        '
        'grpStatistics
        '
        Me.grpStatistics.Controls.Add(Me.lblTotalBooks)
        Me.grpStatistics.Controls.Add(Me.lblAvailableBooks)
        Me.grpStatistics.Controls.Add(Me.lblTotalMembers)
        Me.grpStatistics.Controls.Add(Me.lblActiveTransactions)
        Me.grpStatistics.Controls.Add(Me.lblOverdue)
        Me.grpStatistics.Location = New System.Drawing.Point(30, 60)
        Me.grpStatistics.Name = "grpStatistics"
        Me.grpStatistics.Size = New System.Drawing.Size(700, 100)
        Me.grpStatistics.TabIndex = 1
        Me.grpStatistics.TabStop = False
        Me.grpStatistics.Text = "Library Statistics"
        '
        'lblTotalBooks
        '
        Me.lblTotalBooks.AutoSize = True
        Me.lblTotalBooks.Location = New System.Drawing.Point(20, 30)
        Me.lblTotalBooks.Name = "lblTotalBooks"
        Me.lblTotalBooks.Size = New System.Drawing.Size(85, 13)
        Me.lblTotalBooks.TabIndex = 0
        Me.lblTotalBooks.Text = "Total Books: 0"
        '
        'lblAvailableBooks
        '
        Me.lblAvailableBooks.AutoSize = True
        Me.lblAvailableBooks.Location = New System.Drawing.Point(20, 55)
        Me.lblAvailableBooks.Name = "lblAvailableBooks"
        Me.lblAvailableBooks.Size = New System.Drawing.Size(70, 13)
        Me.lblAvailableBooks.TabIndex = 1
        Me.lblAvailableBooks.Text = "Available: 0"
        '
        'lblTotalMembers
        '
        Me.lblTotalMembers.AutoSize = True
        Me.lblTotalMembers.Location = New System.Drawing.Point(250, 30)
        Me.lblTotalMembers.Name = "lblTotalMembers"
        Me.lblTotalMembers.Size = New System.Drawing.Size(100, 13)
        Me.lblTotalMembers.TabIndex = 2
        Me.lblTotalMembers.Text = "Total Members: 0"
        '
        'lblActiveTransactions
        '
        Me.lblActiveTransactions.AutoSize = True
        Me.lblActiveTransactions.Location = New System.Drawing.Point(250, 55)
        Me.lblActiveTransactions.Name = "lblActiveTransactions"
        Me.lblActiveTransactions.Size = New System.Drawing.Size(90, 13)
        Me.lblActiveTransactions.TabIndex = 3
        Me.lblActiveTransactions.Text = "Active Loans: 0"
        '
        'lblOverdue
        '
        Me.lblOverdue.AutoSize = True
        Me.lblOverdue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverdue.Location = New System.Drawing.Point(480, 40)
        Me.lblOverdue.Name = "lblOverdue"
        Me.lblOverdue.Size = New System.Drawing.Size(120, 13)
        Me.lblOverdue.TabIndex = 4
        Me.lblOverdue.Text = "Overdue Books: 0"
        '
        'grpBookManagement
        '
        Me.grpBookManagement.Controls.Add(Me.btnViewBooks)
        Me.grpBookManagement.Controls.Add(Me.btnAddBook)
        Me.grpBookManagement.Controls.Add(Me.btnSearchBooks)
        Me.grpBookManagement.Location = New System.Drawing.Point(30, 180)
        Me.grpBookManagement.Name = "grpBookManagement"
        Me.grpBookManagement.Size = New System.Drawing.Size(220, 160)
        Me.grpBookManagement.TabIndex = 2
        Me.grpBookManagement.TabStop = False
        Me.grpBookManagement.Text = "Book Management"
        '
        'btnViewBooks
        '
        Me.btnViewBooks.Location = New System.Drawing.Point(20, 30)
        Me.btnViewBooks.Name = "btnViewBooks"
        Me.btnViewBooks.Size = New System.Drawing.Size(180, 30)
        Me.btnViewBooks.TabIndex = 0
        Me.btnViewBooks.Text = "View All Books"
        Me.btnViewBooks.UseVisualStyleBackColor = True
        '
        'btnAddBook
        '
        Me.btnAddBook.Location = New System.Drawing.Point(20, 70)
        Me.btnAddBook.Name = "btnAddBook"
        Me.btnAddBook.Size = New System.Drawing.Size(180, 30)
        Me.btnAddBook.TabIndex = 1
        Me.btnAddBook.Text = "Add New Book"
        Me.btnAddBook.UseVisualStyleBackColor = True
        '
        'btnSearchBooks
        '
        Me.btnSearchBooks.Location = New System.Drawing.Point(20, 110)
        Me.btnSearchBooks.Name = "btnSearchBooks"
        Me.btnSearchBooks.Size = New System.Drawing.Size(180, 30)
        Me.btnSearchBooks.TabIndex = 2
        Me.btnSearchBooks.Text = "Search Books"
        Me.btnSearchBooks.UseVisualStyleBackColor = True
        '
        'grpMemberManagement
        '
        Me.grpMemberManagement.Controls.Add(Me.btnViewMembers)
        Me.grpMemberManagement.Controls.Add(Me.btnAddMember)
        Me.grpMemberManagement.Location = New System.Drawing.Point(270, 180)
        Me.grpMemberManagement.Name = "grpMemberManagement"
        Me.grpMemberManagement.Size = New System.Drawing.Size(220, 160)
        Me.grpMemberManagement.TabIndex = 3
        Me.grpMemberManagement.TabStop = False
        Me.grpMemberManagement.Text = "Member Management"
        '
        'btnViewMembers
        '
        Me.btnViewMembers.Location = New System.Drawing.Point(20, 30)
        Me.btnViewMembers.Name = "btnViewMembers"
        Me.btnViewMembers.Size = New System.Drawing.Size(180, 30)
        Me.btnViewMembers.TabIndex = 0
        Me.btnViewMembers.Text = "View All Members"
        Me.btnViewMembers.UseVisualStyleBackColor = True
        '
        'btnAddMember
        '
        Me.btnAddMember.Location = New System.Drawing.Point(20, 70)
        Me.btnAddMember.Name = "btnAddMember"
        Me.btnAddMember.Size = New System.Drawing.Size(180, 30)
        Me.btnAddMember.TabIndex = 1
        Me.btnAddMember.Text = "Add New Member"
        Me.btnAddMember.UseVisualStyleBackColor = True
        '
        'grpTransactions
        '
        Me.grpTransactions.Controls.Add(Me.btnIssueBook)
        Me.grpTransactions.Controls.Add(Me.btnReturnBook)
        Me.grpTransactions.Controls.Add(Me.btnViewTransactions)
        Me.grpTransactions.Location = New System.Drawing.Point(510, 180)
        Me.grpTransactions.Name = "grpTransactions"
        Me.grpTransactions.Size = New System.Drawing.Size(220, 160)
        Me.grpTransactions.TabIndex = 4
        Me.grpTransactions.TabStop = False
        Me.grpTransactions.Text = "Transactions"
        '
        'btnIssueBook
        '
        Me.btnIssueBook.Location = New System.Drawing.Point(20, 30)
        Me.btnIssueBook.Name = "btnIssueBook"
        Me.btnIssueBook.Size = New System.Drawing.Size(180, 30)
        Me.btnIssueBook.TabIndex = 0
        Me.btnIssueBook.Text = "Issue Book"
        Me.btnIssueBook.UseVisualStyleBackColor = True
        '
        'btnReturnBook
        '
        Me.btnReturnBook.Location = New System.Drawing.Point(20, 70)
        Me.btnReturnBook.Name = "btnReturnBook"
        Me.btnReturnBook.Size = New System.Drawing.Size(180, 30)
        Me.btnReturnBook.TabIndex = 1
        Me.btnReturnBook.Text = "Return Book"
        Me.btnReturnBook.UseVisualStyleBackColor = True
        '
        'btnViewTransactions
        '
        Me.btnViewTransactions.Location = New System.Drawing.Point(20, 110)
        Me.btnViewTransactions.Name = "btnViewTransactions"
        Me.btnViewTransactions.Size = New System.Drawing.Size(180, 30)
        Me.btnViewTransactions.TabIndex = 2
        Me.btnViewTransactions.Text = "Transaction History"
        Me.btnViewTransactions.UseVisualStyleBackColor = True
        '
        'grpReports
        '
        Me.grpReports.Controls.Add(Me.btnOverdueReport)
        Me.grpReports.Location = New System.Drawing.Point(30, 360)
        Me.grpReports.Name = "grpReports"
        Me.grpReports.Size = New System.Drawing.Size(220, 100)
        Me.grpReports.TabIndex = 5
        Me.grpReports.TabStop = False
        Me.grpReports.Text = "Reports"
        '
        'btnOverdueReport
        '
        Me.btnOverdueReport.Location = New System.Drawing.Point(20, 30)
        Me.btnOverdueReport.Name = "btnOverdueReport"
        Me.btnOverdueReport.Size = New System.Drawing.Size(180, 30)
        Me.btnOverdueReport.TabIndex = 0
        Me.btnOverdueReport.Text = "Overdue Books Report"
        Me.btnOverdueReport.UseVisualStyleBackColor = True
        '
        'btnManageUsers
        '
        Me.btnManageUsers.Location = New System.Drawing.Point(290, 390)
        Me.btnManageUsers.Name = "btnManageUsers"
        Me.btnManageUsers.Size = New System.Drawing.Size(180, 30)
        Me.btnManageUsers.TabIndex = 6
        Me.btnManageUsers.Text = "Manage Users (Admin)"
        Me.btnManageUsers.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(530, 390)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(90, 30)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnLogout
        '
        Me.btnLogout.Location = New System.Drawing.Point(640, 390)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(90, 30)
        Me.btnLogout.TabIndex = 8
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'MainDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 500)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnManageUsers)
        Me.Controls.Add(Me.grpReports)
        Me.Controls.Add(Me.grpTransactions)
        Me.Controls.Add(Me.grpMemberManagement)
        Me.Controls.Add(Me.grpBookManagement)
        Me.Controls.Add(Me.grpStatistics)
        Me.Controls.Add(Me.lblWelcome)
        Me.Name = "MainDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Library Management System - Dashboard"
        Me.grpStatistics.ResumeLayout(False)
        Me.grpStatistics.PerformLayout()
        Me.grpBookManagement.ResumeLayout(False)
        Me.grpMemberManagement.ResumeLayout(False)
        Me.grpTransactions.ResumeLayout(False)
        Me.grpReports.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblWelcome As Label
    Friend WithEvents grpStatistics As GroupBox
    Friend WithEvents lblTotalBooks As Label
    Friend WithEvents lblAvailableBooks As Label
    Friend WithEvents lblTotalMembers As Label
    Friend WithEvents lblActiveTransactions As Label
    Friend WithEvents lblOverdue As Label
    Friend WithEvents grpBookManagement As GroupBox
    Friend WithEvents btnViewBooks As Button
    Friend WithEvents btnAddBook As Button
    Friend WithEvents btnSearchBooks As Button
    Friend WithEvents grpMemberManagement As GroupBox
    Friend WithEvents btnViewMembers As Button
    Friend WithEvents btnAddMember As Button
    Friend WithEvents grpTransactions As GroupBox
    Friend WithEvents btnIssueBook As Button
    Friend WithEvents btnReturnBook As Button
    Friend WithEvents btnViewTransactions As Button
    Friend WithEvents grpReports As GroupBox
    Friend WithEvents btnOverdueReport As Button
    Friend WithEvents btnManageUsers As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnLogout As Button
End Class
