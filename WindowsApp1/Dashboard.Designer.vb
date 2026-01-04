<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Dashboard
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAddBooks = New System.Windows.Forms.Button()
        Me.btnViewBooks = New System.Windows.Forms.Button()
        Me.btnAddMembers = New System.Windows.Forms.Button()
        Me.btnViewMembers = New System.Windows.Forms.Button()
        Me.btnIssueBooks = New System.Windows.Forms.Button()
        Me.btnReturnBooks = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 80)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(110, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(580, 37)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Library Management System - Dashboard"
        '
        'btnAddBooks
        '
        Me.btnAddBooks.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnAddBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddBooks.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnAddBooks.ForeColor = System.Drawing.Color.White
        Me.btnAddBooks.Location = New System.Drawing.Point(80, 120)
        Me.btnAddBooks.Name = "btnAddBooks"
        Me.btnAddBooks.Size = New System.Drawing.Size(300, 60)
        Me.btnAddBooks.TabIndex = 1
        Me.btnAddBooks.Text = "📚 Add Books"
        Me.btnAddBooks.UseVisualStyleBackColor = False
        '
        'btnViewBooks
        '
        Me.btnViewBooks.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnViewBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewBooks.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnViewBooks.ForeColor = System.Drawing.Color.White
        Me.btnViewBooks.Location = New System.Drawing.Point(420, 120)
        Me.btnViewBooks.Name = "btnViewBooks"
        Me.btnViewBooks.Size = New System.Drawing.Size(300, 60)
        Me.btnViewBooks.TabIndex = 2
        Me.btnViewBooks.Text = "📖 View Books"
        Me.btnViewBooks.UseVisualStyleBackColor = False
        '
        'btnAddMembers
        '
        Me.btnAddMembers.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnAddMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddMembers.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnAddMembers.ForeColor = System.Drawing.Color.White
        Me.btnAddMembers.Location = New System.Drawing.Point(80, 200)
        Me.btnAddMembers.Name = "btnAddMembers"
        Me.btnAddMembers.Size = New System.Drawing.Size(300, 60)
        Me.btnAddMembers.TabIndex = 3
        Me.btnAddMembers.Text = "👤 Add Members"
        Me.btnAddMembers.UseVisualStyleBackColor = False
        '
        'btnViewMembers
        '
        Me.btnViewMembers.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnViewMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewMembers.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnViewMembers.ForeColor = System.Drawing.Color.White
        Me.btnViewMembers.Location = New System.Drawing.Point(420, 200)
        Me.btnViewMembers.Name = "btnViewMembers"
        Me.btnViewMembers.Size = New System.Drawing.Size(300, 60)
        Me.btnViewMembers.TabIndex = 4
        Me.btnViewMembers.Text = "👥 View Members"
        Me.btnViewMembers.UseVisualStyleBackColor = False
        '
        'btnIssueBooks
        '
        Me.btnIssueBooks.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(196, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.btnIssueBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIssueBooks.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnIssueBooks.ForeColor = System.Drawing.Color.White
        Me.btnIssueBooks.Location = New System.Drawing.Point(80, 280)
        Me.btnIssueBooks.Name = "btnIssueBooks"
        Me.btnIssueBooks.Size = New System.Drawing.Size(300, 60)
        Me.btnIssueBooks.TabIndex = 5
        Me.btnIssueBooks.Text = "📤 Issue Books"
        Me.btnIssueBooks.UseVisualStyleBackColor = False
        '
        'btnReturnBooks
        '
        Me.btnReturnBooks.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(196, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.btnReturnBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReturnBooks.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnReturnBooks.ForeColor = System.Drawing.Color.White
        Me.btnReturnBooks.Location = New System.Drawing.Point(420, 280)
        Me.btnReturnBooks.Name = "btnReturnBooks"
        Me.btnReturnBooks.Size = New System.Drawing.Size(300, 60)
        Me.btnReturnBooks.TabIndex = 6
        Me.btnReturnBooks.Text = "📥 Return Books"
        Me.btnReturnBooks.UseVisualStyleBackColor = False
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(80, 360)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(300, 60)
        Me.btnLogout.TabIndex = 7
        Me.btnLogout.Text = "🚪 Logout"
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(420, 360)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(300, 60)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "❌ Exit Application"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.btnReturnBooks)
        Me.Controls.Add(Me.btnIssueBooks)
        Me.Controls.Add(Me.btnViewMembers)
        Me.Controls.Add(Me.btnAddMembers)
        Me.Controls.Add(Me.btnViewBooks)
        Me.Controls.Add(Me.btnAddBooks)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "Dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Library Management System - Dashboard"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents btnAddBooks As Button
    Friend WithEvents btnViewBooks As Button
    Friend WithEvents btnAddMembers As Button
    Friend WithEvents btnViewMembers As Button
    Friend WithEvents btnIssueBooks As Button
    Friend WithEvents btnReturnBooks As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents btnExit As Button
End Class
