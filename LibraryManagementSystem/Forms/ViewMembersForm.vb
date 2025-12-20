' ====================================================================
' View Members Form - Demonstrates ITERATION to display members
' ====================================================================

Public Class ViewMembersForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()

        Me.Text = "View All Members"
        Me.Size = New Size(900, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
        LoadMembers()
    End Sub

    ' ITERATION: Load all members
    Private Sub LoadMembers()
        lvMembers.Items.Clear()
        For Each member As Member In DataStore.Members
            Dim item As New ListViewItem(member.MemberID.ToString())
            item.SubItems.Add(member.FullName)
            item.SubItems.Add(member.Email)
            item.SubItems.Add(member.PhoneNumber)
            item.SubItems.Add(member.Status)
            item.SubItems.Add(member.MembershipDate.ToShortDateString())
            lvMembers.Items.Add(item)
        Next
        lblTotal.Text = "Total Members: " & DataStore.Members.Count.ToString()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadMembers()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.lvMembers = New ListView()
        Me.lblTotal = New Label()
        Me.btnRefresh = New Button()
        Me.btnClose = New Button()
        Me.SuspendLayout()

        Me.lblTotal.Location = New Point(20, 20)
        Me.lblTotal.Size = New Size(200, 20)
        Me.lblTotal.Text = "Total Members: 0"

        Me.lvMembers.Location = New Point(20, 50)
        Me.lvMembers.Size = New Size(850, 380)
        Me.lvMembers.View = View.Details
        Me.lvMembers.GridLines = True
        Me.lvMembers.FullRowSelect = True
        Me.lvMembers.Columns.Add("ID", 50)
        Me.lvMembers.Columns.Add("Name", 180)
        Me.lvMembers.Columns.Add("Email", 200)
        Me.lvMembers.Columns.Add("Phone", 120)
        Me.lvMembers.Columns.Add("Status", 80)
        Me.lvMembers.Columns.Add("Member Since", 120)

        Me.btnRefresh.Location = New Point(670, 440)
        Me.btnRefresh.Size = New Size(100, 30)
        Me.btnRefresh.Text = "Refresh"

        Me.btnClose.Location = New Point(780, 440)
        Me.btnClose.Size = New Size(90, 30)
        Me.btnClose.Text = "Close"

        Me.ClientSize = New Size(900, 500)
        Me.Controls.Add(btnClose)
        Me.Controls.Add(btnRefresh)
        Me.Controls.Add(lblTotal)
        Me.Controls.Add(lvMembers)
        Me.Name = "ViewMembersForm"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents lvMembers As ListView
    Friend WithEvents lblTotal As Label
    Friend WithEvents btnRefresh, btnClose As Button
End Class
