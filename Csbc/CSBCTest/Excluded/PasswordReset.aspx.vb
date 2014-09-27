Imports System.Data
Imports CSBC.Components
Partial Class PasswordReset
    Inherits System.Web.UI.Page
    Dim lblTitle As Label

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Session("Title") = "Password Reset"
            lblUserName.Text = Session("UserName")
            Me.txtOldPassword.Focus()
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If ErrorData = True Then Exit Sub
        CheckUser()
        If Session("UserID") > 0 And Session("Usertype") > 0 Then
            UpdatePwd(lblUserName.Text, txtNewPassword.Text)
            lblError.Text = "Password Successfully Changed"
            Call MsgBox("Password successfully Changed")
        Else
            lblError.Text = "Invalid Username/Password"
        End If
    End Sub

    Private Function ErrorData() As Boolean
        ErrorData = True
        If txtOldPassword.Text = "" Then
            lblError.Text = "Missing Current Password"
            txtOldPassword.Focus()
            Exit Function
        End If
        If txtNewPassword.Text = "" Then
            lblError.Text = "Missing New Password"
            txtOldPassword.Focus()
            Exit Function
        End If
        If txtConfirm.Text = "" Then
            lblError.Text = "Missing Confirm Password"
            txtOldPassword.Focus()
            Exit Function
        End If
        If txtNewPassword.Text <> txtConfirm.Text Then
            lblError.Text = "New password does not match"
            txtOldPassword.Focus()
            Exit Function
        End If
        ErrorData = False
    End Function
    'ojo here I need to check if the sessionid is needed or the signupseasonid
    Private Sub CheckUser()
        Dim oSecurity As New Security.ClsUsers
        Try
            With oSecurity
                .GetUser(lblUserName.Text, txtOldPassword.Text)
                Session("UserName") = .UserName
                Session("SeasonDesc") = .SeasonDesc
                Session("Usertype") = .Usertype
                Session("UserID") = .UserID
                Session("CompanyID") = .CompanyID
                Session("CompanyName") = .CompanyName
                Session("EmailSender") = .EmailSender
                Session("ImageName") = .ImageName
                Session("TimeZone") = .TimeZone
                Session("SeasoniD") = .SeasonID
            End With
        Catch ex As Exception
            lblError.Text = "Invalid Username/Password"
        Finally
            oSecurity = Nothing
        End Try
    End Sub

    Private Sub UpdatePwd(ByVal sUserName As String, ByVal sPassword As String)
        Dim oSecurity As New Security.ClsUsers
        Try
            With oSecurity
                .UpdPWD(Session("CompanyID"), sUserName, sPassword)
            End With
            txtOldPassword.Text = ""
            txtNewPassword.Text = ""
            txtConfirm.Text = ""
        Catch ex As Exception
            lblError.Text = "UpdatePwd::" & ex.Message
        Finally
            oSecurity = Nothing
        End Try
    End Sub

    Public Sub MsgBox(ByVal Message As String)
        Dim strScript As New Label
        strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" & Environment.NewLine & "window.alert('" + Message + "')</script>"
        Page.Controls.Add(strScript)
    End Sub
End Class
