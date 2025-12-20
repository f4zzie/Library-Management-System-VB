' ====================================================================
' Add Member Form - Demonstrates SEQUENTIAL and SELECTION with validation
' ====================================================================

Public Class AddMemberForm
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()

        Me.Text = "Add New Member"
        Me.Size = New Size(500, 450)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
    End Sub

    ' SEQUENTIAL & SELECTION: Validate and save member
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validate inputs
        If Not ValidationHelper.IsNotEmpty(txtName.Text) Then
            ValidationHelper.ShowValidationError("Name", "Name is required")
            txtName.Focus()
            Return
        End If

        If Not ValidationHelper.IsValidEmail(txtEmail.Text) Then
            ValidationHelper.ShowValidationError("Email", "Please enter a valid email address")
            txtEmail.Focus()
            Return
        End If

        If Not ValidationHelper.IsValidPhone(txtPhone.Text) Then
            ValidationHelper.ShowValidationError("Phone", "Please enter a valid phone number")
            txtPhone.Focus()
            Return
        End If

        If Not ValidationHelper.IsNotEmpty(txtAddress.Text) Then
            ValidationHelper.ShowValidationError("Address", "Address is required")
            txtAddress.Focus()
            Return
        End If

        ' Create new member
        Dim newMember As New Member()
        newMember.FullName = txtName.Text.Trim()
        newMember.Email = txtEmail.Text.Trim()
        newMember.PhoneNumber = txtPhone.Text.Trim()
        newMember.Address = txtAddress.Text.Trim()
        newMember.MembershipDate = dtpMembershipDate.Value
        newMember.Status = "Active"

        DataStore.AddMember(newMember)
        MessageHelper.ShowSuccess("Member added successfully!" & vbCrLf & "Member ID: " & newMember.MemberID)
        ClearForm()
    End Sub

    Private Sub ClearForm()
        txtName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtAddress.Clear()
        dtpMembershipDate.Value = Date.Today
        txtName.Focus()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If MessageHelper.ShowConfirmation("Clear all fields?") Then
            ClearForm()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.lblName = New Label()
        Me.lblEmail = New Label()
        Me.lblPhone = New Label()
        Me.lblAddress = New Label()
        Me.lblMembershipDate = New Label()
        Me.txtName = New TextBox()
        Me.txtEmail = New TextBox()
        Me.txtPhone = New TextBox()
        Me.txtAddress = New TextBox()
        Me.dtpMembershipDate = New DateTimePicker()
        Me.btnSave = New Button()
        Me.btnClear = New Button()
        Me.btnClose = New Button()
        Me.SuspendLayout()

        ' Labels and controls
        Me.lblName.Location = New Point(50, 30)
        Me.lblName.Size = New Size(100, 20)
        Me.lblName.Text = "Full Name:"

        Me.txtName.Location = New Point(150, 27)
        Me.txtName.Size = New Size(300, 20)

        Me.lblEmail.Location = New Point(50, 70)
        Me.lblEmail.Size = New Size(100, 20)
        Me.lblEmail.Text = "Email:"

        Me.txtEmail.Location = New Point(150, 67)
        Me.txtEmail.Size = New Size(300, 20)

        Me.lblPhone.Location = New Point(50, 110)
        Me.lblPhone.Size = New Size(100, 20)
        Me.lblPhone.Text = "Phone Number:"

        Me.txtPhone.Location = New Point(150, 107)
        Me.txtPhone.Size = New Size(200, 20)

        Me.lblAddress.Location = New Point(50, 150)
        Me.lblAddress.Size = New Size(100, 20)
        Me.lblAddress.Text = "Address:"

        Me.txtAddress.Location = New Point(150, 147)
        Me.txtAddress.Size = New Size(300, 60)
        Me.txtAddress.Multiline = True

        Me.lblMembershipDate.Location = New Point(50, 220)
        Me.lblMembershipDate.Size = New Size(100, 20)
        Me.lblMembershipDate.Text = "Member Since:"

        Me.dtpMembershipDate.Location = New Point(150, 217)
        Me.dtpMembershipDate.Size = New Size(200, 20)
        Me.dtpMembershipDate.Value = Date.Today

        Me.btnSave.Location = New Point(100, 280)
        Me.btnSave.Size = New Size(100, 30)
        Me.btnSave.Text = "Save"

        Me.btnClear.Location = New Point(220, 280)
        Me.btnClear.Size = New Size(100, 30)
        Me.btnClear.Text = "Clear"

        Me.btnClose.Location = New Point(340, 280)
        Me.btnClose.Size = New Size(100, 30)
        Me.btnClose.Text = "Close"

        Me.ClientSize = New Size(500, 350)
        Me.Controls.Add(btnClose)
        Me.Controls.Add(btnClear)
        Me.Controls.Add(btnSave)
        Me.Controls.Add(dtpMembershipDate)
        Me.Controls.Add(txtAddress)
        Me.Controls.Add(txtPhone)
        Me.Controls.Add(txtEmail)
        Me.Controls.Add(txtName)
        Me.Controls.Add(lblMembershipDate)
        Me.Controls.Add(lblAddress)
        Me.Controls.Add(lblPhone)
        Me.Controls.Add(lblEmail)
        Me.Controls.Add(lblName)
        Me.Name = "AddMemberForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblName, lblEmail, lblPhone, lblAddress, lblMembershipDate As Label
    Friend WithEvents txtName, txtEmail, txtPhone, txtAddress As TextBox
    Friend WithEvents dtpMembershipDate As DateTimePicker
    Friend WithEvents btnSave, btnClear, btnClose As Button
End Class
