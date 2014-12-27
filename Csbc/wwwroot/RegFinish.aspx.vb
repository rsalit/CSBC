Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.Mail
Imports System.Diagnostics
Imports System.Net
Imports System.IO
Imports CSBC.Components
Imports System.Net.Mail
'Imports nsoftware.IBizPayPal

Public Class RegFinish
    Inherits System.Web.UI.Page
    Dim SeasonID As Integer
    Dim Season As String
    Dim HouseID As Integer
    Dim Payer_ID As String
    Dim Amount As Decimal
    Dim PP_Fee As Decimal
    Dim Payer_Email As String
    Dim RegDate As Date
    Dim RegAmount As String
    Dim Txn_ID As String
    Dim Payment_Status As String
    Dim UserName As String
    Dim SQL As String
    Dim Email As String
    Dim UserID As String
    Dim CARTID As Integer
    Dim EmailBody As String
    Dim HouseData As String
    Dim RegistrationData As String
    Dim PlayersData As String
    Dim SponsorData As String
   
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Session("Title") = "Confirmation"
        If Session("HouseID") <= 0 Or Session("USERID") <= 0 Then
            Response.Redirect("RegLogin.aspx")
        End If

        If Page.IsPostBack = False Then
            'Call PaypalCharges()
            If Request.QueryString("Resend") = "YES" Then
                Call ReadShoopingCart()
            Else
                Call PayPalAnswer()
                'Call SP
                Call UpdateShoppingCart()
            End If
            btnDone.Focus()
            Call SendConfirmation()
        End If
    End Sub

    Private Sub ReadShoopingCart()
        Dim oPlayers As New Season.ClsPlayers
        Dim rsData As DataTable
        Try
            rsData = oPlayers.GetShoppingCart(Session("SeasonID"), Session("HouseID"))
            If rsData.Rows.Count > 0 Then
                HouseID = Session("HouseID")
                Payer_ID = rsData.Rows(0).Item("Payer_ID")
                Payer_Email = rsData.Rows(0).Item("Payer_Email")
                Payment_Status = rsData.Rows(0).Item("Payment_status")
                UserName = rsData.Rows(0).Item("CreatedUser")
                Season = Session("SeasonID")
                Amount = rsData.Rows(0).Item("Payment_Gross")
                Txn_ID = rsData.Rows(0).Item("txn_ID")
                PP_Fee = rsData.Rows(0).Item("Payment_Fee")
            End If
        Catch ex As Exception
            lblMSG.Text = "ReadShoopingCart::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Sub

    Private Sub PayPalAnswer()
        HouseID = Session("HouseID")
        Payer_ID = Session("IPAddress")
        Payer_Email = Session("EMAIL")
        Payment_Status = Session("ACK")
        UserName = Session("UserName") 'Session("Admin")
        Season = Session("Season")
        If Left(Session("ACK"), 7) = "Success" Then
            Amount = Session("AMOUNT")
            'txtCVV.value = Session("CVV")
            Txn_ID = Session("TransactionID")
            PP_Fee = Session("Fee")
        Else
            Amount = 0
            PP_Fee = 0
            lblMSG.Text = Session("ERRORMSG")
        End If
    End Sub

    'Private Sub SendEmails()
    '    Dim oEmail As New System.Net.Mail.MailMessage()
    '    Dim oSmtp As New SmtpClient("mail.csbchoops.net")
    '    oSmtp = New SmtpClient("mail.csbchoops.net")
    '    oSmtp.Host = "mail.csbchoops.net"
    '    oSmtp.Credentials = New System.Net.NetworkCredential("registrar@csbchoops.net", "csbc0317")
    '    oSmtp.Port = 25

    '    For I As Int16 = 0 To lstEmails.Items.Count - 1
    '        If IsEmail(lstEmails.Items(I).Value) = True Then
    '            oEmail = New System.Net.Mail.MailMessage()
    '            oEmail.From = New System.Net.Mail.MailAddress("registration@csbchoops.net")
    '            oEmail.To.Add(lstEmails.Items(I).Value)
    '            oEmail.IsBodyHtml = True
    '            oEmail.Body = htmlMail.Text
    '            oEmail.Subject = txtSubject.Text
    '            If GoodEmail(oSmtp, oEmail) = True Then EmailsSent += 1
    '            oEmail.Dispose()
    '            oEmail = Nothing
    '        End If
    '    Next

    '    If EmailsSent = 0 Then
    '        lblError.Text = ErrorMsg & " (0) Email(s) sent!"
    '    Else
    '        lblError.Text = "(" & EmailsSent & ") Email(s) sent!"
    '    End If
    '    txtSubject.Text = ""
    '    htmlMail.Text = ""
    'End Sub

    Private Sub SendConfirmation()
        ''Send Email
        Dim oEmail As New System.Net.Mail.MailMessage()
        Dim oSmtp As New SmtpClient("mail.csbchoops.net")
        oSmtp = New SmtpClient("mail.csbchoops.net")
        oSmtp.Host = "mail.csbchoops.net"
        oSmtp.Credentials = New System.Net.NetworkCredential("registrar@csbchoops.net", "csbc0317")
        oSmtp.Port = 25

        oEmail = New System.Net.Mail.MailMessage()
        oEmail.From = New System.Net.Mail.MailAddress("registration@csbchoops.net")
        HouseData = GetHouseholdData(HouseID)
        RegistrationData = GetRegistrationData(Session("SeasonID"), HouseID)
        Call GetMessages("Registration")
        PlayersData = GetPlayersData(Session("SeasonID"), HouseID)
        SponsorData = GetSponsorsData()
        EmailBody = "Registration for: " & Season
        EmailBody += HouseData & "<br>" & RegistrationData & "<br>" & PlayersData & "<br>" & SponsorData
        If Label4.Text > "" Then EmailBody += "<br>" & Label4.Text
        If PlayersData > "" Then EmailBody += "<br><h3>**TRYOUTS ARE MANDATORY**</h3>"
        oEmail.To.Add(Payer_Email)
        oEmail.IsBodyHtml = True
        oEmail.Body = EmailBody
        oEmail.Subject = "CSBC Registration"

        Try
            oSmtp.Send(oEmail)
            lblMSG.Text = "Confirmation Email sent!"
        Catch Ex As Exception
            lblMSG.Text = "Unable to send Confirmation mail!  " & Ex.Message
        Finally
            oEmail.Dispose()
            oEmail = Nothing
        End Try
        txtRegistration.Value = RegDate
        txtTrxID.Value = Txn_ID
        txtAmount.Value = RegAmount

    End Sub

    Private Sub UpdateShoppingCart()
        Dim oPlayers As New Season.ClsPlayers
        Try
            With oPlayers
                .HouseID = HouseID
                .CompanyId = Session("CompanyID")
                .PayerID = Payer_ID
                .PaidAmount = Amount
                .Email = Payer_Email
                .TrxID = Txn_ID
                .PaymentStatus = Payment_Status
                .UserName = UserName
                .PP_Fee = PP_Fee
                .ErrMsg = Session("ERRORMSG")
                .UpdatePayment()
            End With

        Catch ex As Exception
            Session("ErrorMSG") = "UpdateShoppingCart::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Sub

    Private Function GetHouseholdData(ByVal HouseID As Integer) As String
        GetHouseholdData = ""
        Dim oPlayers As New CSBC.Components.Season.ClsPlayers
        Try
            With oPlayers
                .GetHouseholdCart(Session("HouseID"), Session("SeasonID"))
                GetHouseholdData = "<br>Family: " & .Name
                GetHouseholdData = GetHouseholdData & "<br>" & .Address1 & " " & .Address2
                GetHouseholdData = GetHouseholdData & "<br>" & .City & ", " & .State & " " & .Zip
                GetHouseholdData = GetHouseholdData & "<br>" & .Phone
                Txn_ID = oPlayers.TrxID
            End With
        Catch ex As Exception
            lblMSG.Text = "Invalid HouseID: " & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Function

    Private Function GetRegistrationData(ByVal SeasonID As Integer, ByVal HouseID As Integer) As String
        GetRegistrationData = ""
        Dim Amount As Decimal = 0
        Dim TranID As String = ""
            Dim rsData As DataTable
            Dim oPlayers As New Season.ClsPlayers
            Try
                With oPlayers
                    rsData = .ReadRegistration(SeasonID, HouseID)
                    For I As Int16 = 0 To rsData.Rows.Count - 1
                    GetRegistrationData = GetRegistrationData & "<br>Registration Date: " & rsData.Rows(I).Item("CreatedDate")
                        RegDate = rsData.Rows(I).Item("CreatedDate")
                    GetRegistrationData = GetRegistrationData & "<br>Transaction ID: " & rsData.Rows(I).Item("Txn_ID")
                        GetRegistrationData = GetRegistrationData & "<br>Paid Amount: " & Format(rsData.Rows(I).Item("Payment_GRoss"), "currency")
                    Amount = Amount + rsData.Rows(I).Item("Payment_GRoss")
                        CARTID = rsData.Rows(I).Item("cartID")
                        Payer_Email = rsData.Rows(I).Item("Payer_Email")
                        GetRegistrationData = GetRegistrationData & "<br>"
                Next
                RegAmount = Format(Amount, "currency")
                End With
            Catch ex As Exception
                lblMSG.Text = "Invalid TrxID: " & ex.Message
            Finally
                oPlayers = Nothing
            End Try
    End Function

    Private Function GetPlayersData(ByVal SeasonID As Integer, ByVal HouseID As Integer) As String
        GetPlayersData = ""
        Dim rsData As DataTable
        Dim oPlayers As New Season.ClsPlayers
        Try
            With oPlayers
                rsData = .ReadPlayerByCartID(SeasonID, HouseID)
                Season = rsData.Rows(0).Item("Sea_Desc")
                For I As Int16 = 0 To rsData.Rows.Count - 1
                    GetPlayersData = GetPlayersData & rsData.Rows(I).Item("Name")
                    GetPlayersData = GetPlayersData & "<br>" & rsData.Rows(I).Item("Div_Desc")
                    GetPlayersData = GetPlayersData & "<br>*TRYOUTS: " & rsData.Rows(I).Item("DraftVenue")
                    GetPlayersData = GetPlayersData & ", " & rsData.Rows(I).Item("DraftDate") & ":" & rsData.Rows(I).Item("DraftTime") & "<br>"
                    GetPlayersData = GetPlayersData & "<br>"
                Next

            End With
            grdPlayers.Controls.Clear()
            If rsData.Rows.Count > 0 Then
                With grdPlayers
                    .DataSource = rsData
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            lblMSG.Text = "Invalid CartID: " & ex.Message
        Finally
            oPlayers = Nothing
        End Try

        If GetPlayersData > "" Then
            GetPlayersData = "<BR>Player(s):<br>" & GetPlayersData
        Else
            grdPlayers.Visible = False
        End If
    End Function

    Private Function GetSponsorsData() As String
        GetSponsorsData = ""
        Dim rsData As DataTable
        Dim oSponsors As New Season.clsSponsors
        Try
            With oSponsors
                rsData = .GetSponsorData(Session("HouseID"), Session("SeasonID"))
                GetSponsorsData = "<br>Sponsor: " & rsData.Rows(0).Item("SPOname")
                txtSponsor.Value = rsData.Rows(0).Item("SPOname")
                lblSponsor.Visible = True
                txtSponsor.Visible = True
                txtSponsorAmount.Value = Format(rsData.Rows(0).Item("SpoAmount"), "Currency")
                lblSponsorAmount.Visible = True
                txtSponsorAmount.Visible = True
                GetSponsorsData = GetSponsorsData & "<br>Website: " & rsData.Rows(0).Item("URL") & ""
                GetSponsorsData = GetSponsorsData & "<br>Contact Name: " & rsData.Rows(0).Item("ContactName") & ""
                GetSponsorsData = GetSponsorsData & "<br>Thank You for your support"
            End With
        Catch ex As Exception
            lblMSG.Text = ex.Message
        Finally
            oSponsors = Nothing
        End Try
    End Function

    Private Sub Logout()
        Session.RemoveAll()
        'Response.Write("<script language='javascript'> { window.top.close();}</script>")
        'Response.Write("<script language='javascript'> {window.opener=self;window.close();}</script>")
    End Sub


    Private Sub btnDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDone.Click
        Call Logout()
        'Response.Redirect("http://www.csbchoops.com")

        'Response.Write("<script language='javascript'> {window.open('http://www.csbchoops.com','mywin','left=0,top=0,width=1250,height=550,toolbar=1,location=1,status=1,menubar=1,scrollbars=1,resizable=1');}</script>")
        'Response.Write("<script language='javascript'> {window.open('http://www.csbchoops.com/Index.aspx','mywin','left=0,top=0,toolbar=1,location=1,status=1,menubar=1,scrollbars=1,resizable=1');}</script>")
        Server.Transfer("RegLogin.aspx")
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Response.Redirect("Report.aspx")
    End Sub

    Private Sub GetMessages(ByVal MessScreen As String)
        Dim oMessages As New Website.clsMessages
        Try
            With oMessages
                .GetRecords(Session("CompanyID"), MessScreen, RegDate)
                If .MessageSeq = 4 Then
                    Label4.Text = .Message
                End If
            End With
        Catch ex As Exception
            lblMSG.Text = ex.Message
        Finally
            oMessages = Nothing
        End Try
    End Sub

End Class
