'Imports System.Web.Mail
Imports System.Data
Imports System.Net.Mail
Imports System.Data.SqlClient
'Imports System.Security.Cryptography
Imports CSBC.Components
Imports CSBC.Core

Public Class _RegLogin
    Inherits System.Web.UI.Page
  
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Session("Module") = "RegLogin.aspx"
        Session("Title") = "Login"
        If Page.IsPostBack = False Then
            'Call GetMessages("Registration")
            Call CheckBrowser()
            If Session("UserName") > "" Then
                txtUser.Value = Session("UserName")
                txtPassword.Focus()
            Else
                Me.txtUser.Focus()
            End If
        End If
    End Sub

    Private Sub GetMessages(ByVal MessScreen As String)
        Dim oMessages As New Website.clsMessages
        Try
            ' oMessages.GetRecords(Session("CompanyID"), MessScreen, Now())
            ' label1.Text = oMessages.Message
            'label2.Text = oMessages.MessageCont
        Catch ex As Exception
            lblError.Text = "lnkForgot:" & ex.Message
        Finally
            oMessages = Nothing
        End Try

    End Sub

    Private Sub CheckBrowser()
        If Request.Browser.Browser <> "IE" Then
            lblBrowser.Text = "***Internet Explorer is required to browse this site *** You are using " & Request.Browser.Browser
        End If
    End Sub

    Private Sub GetUser()
        Dim SignDate As DateTime
        Dim SignDateEND As DateTime
        Dim Userfnd As Boolean = False

        Dim sGlobal As New CSBC.Components.ClsGlobal

        Dim oSecurity As New Security.ClsUsers
        Try
            'With oSecurity
            '    .GetLoginInfo(txtUser.Value, txtPassword.Value)
            '    Session("UserName") = .UserName
            '    Session("Season") = .SeasonDesc
            '    Session("Usertype") = .Usertype
            '    Session("UserID") = .UserID
            '    Session("CompanyID") = .CompanyID
            '    Session("CompanyName") = .CompanyName
            '    Session("EmailSender") = .EmailSender
            '    Session("ImageName") = .ImageName
            '    Session("TimeZone") = .TimeZone
            '    Session("SeasonID") = .SignUpSeasonID

            '    Session("HouseID") = .HouseID
            '    SignDate = .SignedDate
            '    SignDateEND = .SignedDateEnd
            '    Userfnd = True
            'End With
            Dim rep = New Models.UserRepository(New Data.CSBCDbContext)
            Dim user As Models.User = rep.GetUser(txtUser.Value, txtPassword.Value)
            If (user Is Nothing) OrElse (user.UserID = 0) Then
                Return
            Else
                Session("UserName") = user.UserName
                'Session("Season") = user.SeasonDesc
                Session("Usertype") = user.UserType
                Session("UserID") = user.UserID
                Session("CompanyID") = 1
                'ssion("CompanyName") = user.CompanyName
                'Session("EmailSender") = user.EmailSender
                'Session("ImageName") = user.ImageName
                'Session("TimeZone") = user.TimeZone
                Session("SeasonID") = 70 'change when we convert!

                Session("HouseID") = user.HouseID
                'SignDate = .SignedDate
                'SignDateEND = .SignedDateEnd
                Userfnd = True
            End If
        Catch ex As Exception
            lblError.Text = "Invalid Username/Password"
        Finally
            oSecurity = Nothing
        End Try

        If Userfnd = False Then
            lblError.Text = "Invalid Username/Password"
        ElseIf (SignDate = "1900-01-01 00:00:00" Or SignDate > sGlobal.TimeAdjusted(Session("Timezone"), Now()) Or SignDateEND <= sGlobal.TimeAdjusted(Session("Timezone"), Now())) And UCase(txtUser.Value) <> "TEST" Then
            Session.RemoveAll()
            If SignDate = "1900-01-01 00:00:00" Then
                lblError.Text = "Online Registration not available come back again!"
            Else
                If SignDate > sGlobal.TimeAdjusted(Session("Timezone"), Now()) Then
                    lblError.Text = "Online Registration not available until: " & SignDate
                Else
                    lblError.Text = "Online Registration Temporary closed, try it later.."
                End If
            End If
        ElseIf Session("USERID") > 0 Or UCase(txtUser.Value) = "TEST" Then
            pnlLogin.Visible = False
            lblError.Text = ""
            Server.Transfer("RegHouse.aspx")
        Else
            lblError.Text = "Invalid Username/Password"
        End If

    End Sub

    Private Sub btnLogin_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.ServerClick
        Session.RemoveAll()
        If CheckEncryption(txtUser.Value) = False Then UpdatePWD(txtUser.Value, txtPassword.Value)
        Call GetUser()
    End Sub


    Private Function CheckEncryption(ByVal susername As String) As Boolean
        Dim oSecurity As New Security.ClsUsers
        Try
            CheckEncryption = oSecurity.CheckEncryption(susername)
        Catch ex As Exception
            lblError.Text = "Invalid Username/Password"
        Finally
            oSecurity = Nothing
        End Try
    End Function

    Private Sub UpdatePWD(ByVal sUsername As String, ByVal PWD As String)
        Dim oSecurity As New Security.ClsUsers
        Try
            oSecurity.CompanyID = 1
            oSecurity.UserName = sUsername
            oSecurity.PWord = PWD
            oSecurity.UpdPWD()
        Catch ex As Exception
            lblError.Text = "Invalid Username/Password"
        Finally
            oSecurity = Nothing
        End Try
      
    End Sub

    Private Sub EmailReset(ByVal Email As String, ByVal Pwd As String)
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
            oEmail.Body = "Your password is: " & Pwd
            oEmail.Subject = "Password Information Requested"
            If GoodEmail(oSmtp, oEmail) = True Then lblError.Text = "Email sent!"
            oEmail.Dispose()
        Catch Ex As Exception
            lblError.Text = "Unable to send mail!  " & Ex.Message
        Finally
            oEmail = Nothing
        End Try
    End Sub

    Private Function GoodEmail(ByVal oSmtp As Object, ByVal oEmail As Object) As Boolean
        GoodEmail = True
        Try
            oSmtp.Send(oEmail)
        Catch ex As Exception
            lblError.Text = "Unable to send mail!  " & ex.Message
            GoodEmail = False
        End Try
    End Function

    Private Sub lnkPassReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkPassReset.Click
        Dim Email As String = ""
        Dim pwd As String = ""
        Dim oUsers As New Security.ClsUsers
        Try
            oUsers.GetEmail(Session("CompanyID"), txtUser.Value)
            Email = oUsers.Email
            pwd = oUsers.PWord
        Catch ex As Exception
            lblError.Text = "lnkPassReset:" & ex.Message
        Finally
            oUsers = Nothing
        End Try

        If Email > "" And pwd > "" Then
            Call EmailReset(Email, pwd)
            'lblError.Text = "Email Sent!"
        Else
            lblError.Text = "Invalid Username/Password"
        End If
    End Sub

    Private Sub lnkEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEmail.Click
        Server.Transfer("RegForgot.aspx")
    End Sub

    Private Sub lnkNewUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkNewUser.Click
        Server.Transfer("RegUserName.aspx")
    End Sub

End Class
