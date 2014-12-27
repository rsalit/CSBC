Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports CSBC.Components
Public Class RegUserName
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim sCity As String
    Dim ErrorFnd As Boolean = False
 

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Page.IsPostBack = False Then
            txtEmail.Focus()
        End If
    End Sub

    Private Sub comUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comUpdate.Click
        If comUpdate.Text = "Sign Up" Then
            Call ValidateInput()
        Else
            Server.Transfer("RegLogin.aspx")
        End If
    End Sub

    Private Sub ValidateInput()
        Dim ErrCode As Integer
        'Username cant be duplicate
        'Email must exist in households
        'password and password1 must match
        'HouseID can't be duplicate in Users
        ErrorFnd = False
        lblMSG.Text = ""
        If txtEmail.Text <= "" Or txtUserName.Text <= "" Or txtPassword.Value <= "" Or txtPassword2.Value <= "" Then
            lblMSG.Text = "ERROR - Missing data"
            ErrorFnd = True
            Exit Sub
        End If
        If txtPassword.Value <> txtPassword2.Value Then
            lblMSG.Text = "ERROR - Password Incorrect"
            ErrorFnd = True
            Exit Sub
        End If
        Session("HouseID") = GetHouseID()
        If Session("HouseID") = 0 Then
            lblMSG.Text = "ERROR - Invalid Email account (you need a validated email account with us)!"
            ErrorFnd = True
            Exit Sub
        End If
        If chkAgree.Checked = False Then
            lblMSG.Text = "Your electronic signature is required. Read the disclaimer and accept the conditions by clicking on 'agree'"
            ErrorFnd = True
            Exit Sub
        End If
        ErrCode = CheckUser()
        If ErrCode = 0 Then
            lblMSG.Text = "ERROR - UserName can't be added at this time!"
            ErrorFnd = True
            Exit Sub
        End If
        If ErrCode = 1 Then
            lblMSG.Text = "ERROR - Username already exists select another UserName!"
            ErrorFnd = True
            Exit Sub
        End If
        'If ErrCode = 2 Then
        '    lblMSG.Text = "ERROR - Household assigned to another Account/UserName!"
        '    ErrorFnd = True
        '    Exit Sub
        'End If
        If ErrCode = 3 Then
            lblMSG.Text = "ERROR - Username already exists for this email!"
            ErrorFnd = True
            Exit Sub
        End If

        If RTrim(UCase(sCity)) <> "CORAL SPRINGS" Then
            lblMSG.Text = "ERROR - Online Registration is only available for Coral Springs residents!"
            ErrorFnd = True
            Exit Sub
        End If

        If addNewUser() = 1 Then
            lblMSG.Text = "ERROR - Can't add user at this time"
            'An Email could be sent here to notify webmaster
        Else
            Call EmailSend(txtEmail.Text, txtUserName.Text, txtPassword.Value)
            If lblMSG.Text = "Email sent!" Then lblMSG.Text = "User successfully added"
            Session("UserName") = txtUserName.Text
            comUpdate.Text = "DONE!"
        End If

    End Sub

    Private Function CheckUser() As Integer
        CheckUser = 0
        Dim oUsers As New Security.ClsUsers
        Try
            'oUsers.CheckUserByHousehold(Session("HouseID"), txtUserName.Text)
            'If oUsers.UserName > "" Then
            '    CheckUser = 1
            '    If oUsers.HouseID = Session("HouseID") Then CheckUser = 3
            'Else
            '    CheckUser = 2
            'End If
        Catch ex As Exception
            CheckUser = 2 'No record found on Users table with nether houseid or usernama 
        Finally
            oUsers = Nothing
        End Try
    End Function

    Private Function addNewUser() As Integer
        Dim oUsers As New Security.ClsUsers
        addNewUser = 0
        Try
            With oUsers
                .UserName = Trim(txtUserName.Text)
                .Name = txtName.Text
                .PWord = txtPassword.Value
                .Usertype = 0
                .HouseID = Session("HouseID")
                .CreatedUser = Trim(txtUserName.Text)
                .AddUser(Session("TimeZone"))
            End With

        Catch ex As Exception
            addNewUser = 1
            lblMSG.Text = "addNewUser::" & ex.Message
        Finally
            oUsers = Nothing
        End Try

    End Function

    Private Function GetHouseID() As Integer
        GetHouseID = 0
        Dim oHouse As New Profile.ClsHouseholds
        Try
            'oHouse.GetHouseIDByEmail(txtEmail.Text)
            'GetHouseID = oHouse.HouseId
            'sCity = oHouse.City
        Catch ex As Exception
            GetHouseID = 0
        Finally
            oHouse = Nothing
        End Try
    End Function

    Private Sub lnkClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Write("<script language='javascript'> {window.opener=self;window.close();}</script>")
    End Sub

    Private Sub EmailSend(ByVal Email As String, ByVal UserID As String, ByVal Pwd As String)
        Dim oEmail As New System.Net.Mail.MailMessage()
        Dim oSmtp As New SmtpClient("mail.csbchoops.net")
        Try
            oSmtp = New SmtpClient("mail.csbchoops.net")
            oSmtp.Host = "mail.csbchoops.net"
            oSmtp.Credentials = New System.Net.NetworkCredential("registrar@csbchoops.net", "CSBC0317")
            oSmtp.Port = 25

            oEmail = New System.Net.Mail.MailMessage()
            oEmail.From = New System.Net.Mail.MailAddress("registration@csbchoops.net")
            oEmail.To.Add(Email)
            oEmail.IsBodyHtml = True
            oEmail.Body = "Thank you for signing up, you Username is: (" & UserID & ") and your password is: (" & Pwd & ")"

            oEmail.Subject = "CSBC New User created"
            oEmail.Dispose()
            lblMSG.Text = "Email sent!"
        Catch Ex As Exception
            lblMSG.Text = "Unable to send mail!  " & Ex.Message
        Finally
            oEmail = Nothing
        End Try
    End Sub

End Class
