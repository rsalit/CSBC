Imports System.Data
Imports System.Net.Mail
Imports System.Data.SqlClient

Public Class RegForgot
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim Email As String
    Dim UserID As String
    Dim pwd As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack = False Then
            txtName.Focus()
        End If
    End Sub

    Private Sub comUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comUpdate.Click
        If txtName.Text <= "" Or txtPhone1.Text <= "" Or txtPhone2.Text <= "" Or txtPhone3.Text <= "" Then
            lblMSG.Text = "Missing data"
        Else
            Call GetUser()
            'If Email > " " Then Call EmailReset(Email, UserID, pwd)
        End If
    End Sub

    Private Sub GetUser()

        Dim sGlobal As New CSBC.Components.ClsGlobal

        Dim oSecurity As New CSBC.Components.Security.ClsUsers
        Try
            'With oSecurity
            '    .FindUser(txtName.Text, txtPhone1.Text & "-" & txtPhone2.Text & "-" & txtPhone3.Text)
            '    Email = .Email
            '    UserID = .UserName
            '    pwd = .PWord
            'End With
            'If Email > "" And pwd > "" And UserID > "" Then
            '    Call EmailReset(Email, UserID, pwd)
            '    'lblError.Text = "Email Sent!"
            'Else
            '    lblMSG.Text = "Invalid Name/Phone or email not found"
            'End If
            ''If Session("USERID") > 0 Or UCase(txtName.Text) = "TEST" Then
            ''    lblMSG.Text = ""
            ''    Server.Transfer("RegHouse.aspx")
            ''End If
        Catch ex As Exception
            lblMSG.Text = "Invalid Username/Phone"
        Finally
            oSecurity = Nothing
        End Try

    End Sub

    Private Sub EmailReset(ByVal Email As String, ByVal UserID As String, ByVal Pwd As String)
        Dim oEmail As New System.Net.Mail.MailMessage()
        Dim oSmtp As New SmtpClient("mail.csbchoops.net")
        Try
            oSmtp = New SmtpClient("mail.csbchoops.net")
            oSmtp.Host = "mail.csbchoops.net"
            oSmtp.Credentials = New System.Net.NetworkCredential("registrar@csbchoops.net", "csbc0317")
            oSmtp.Port = 25
            oEmail = New System.Net.Mail.MailMessage()
            oEmail.From = New System.Net.Mail.MailAddress("registration@csbchoops.net")
            oEmail.To.Add(Email)
            oEmail.IsBodyHtml = True
            oEmail.Body = "Username: (" & UserID & ") <br>password: (" & Pwd & ")"
            oEmail.Subject = "CSBC User Information Requested"
            If GoodEmail(oSmtp, oEmail) = True Then lblMSG.Text = "Email sent!"
            oEmail.Dispose()
            oEmail = Nothing

        Catch Ex As Exception
            lblMSG.Text = "Unable to send mail!  " & Ex.Message
        End Try
    End Sub

    Private Function GoodEmail(ByVal oSmtp As Object, ByVal oEmail As Object) As Boolean
        GoodEmail = True
        Try
            oSmtp.Send(oEmail)
        Catch ex As Exception
            lblMSG.Text = "Unable to send mail!  " & ex.Message
            GoodEmail = False
        End Try
    End Function

    Private Sub lnkClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkClose.Click
        Response.Write("<script language='javascript'> {window.opener=self;window.close();}</script>")
    End Sub

End Class
