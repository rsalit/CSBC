Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components
Imports System
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Encore.PayPal.Nvp

Public Class RegPayPalDirect
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        'If Session("HouseID") <> 563 Then
        '    Response.Redirect("Temp.htm")
        'Else
        If Session("HouseID") <= 0 Or Session("USERID") <= 0 Then
            Response.Redirect("RegLogin.aspx")
        End If

        Session("Title") = "Payment Completion"
        If Request.QueryString("Process") = "TRUE" And Session("Pay") = 0 Then
            Session("Pay") = 1
            Call LoadYears(Year(Now))
            Call PaypalCharges()
        ElseIf Page.IsPostBack = False Then
            Call LoadYears(Year(Now))
            Call LoadData(Session("HouseID"))
            txtName.Focus()
        End If
        'End If
    End Sub

    Private Sub LoadYears(ByVal firstYear As Integer)
        Dim i As Integer
        For i = 0 To 9
            cmbYear.Items.Add(New ListItem(firstYear + i, firstYear + i))
        Next i
        cmbMonth.SelectedValue = Month(Now)
    End Sub

    Private Sub LoadData(ByVal HouseID As Long)
        Dim oPlayers As New Season.ClsPlayers
        Dim rsData As DataTable
        Try
            rsData = oPlayers.GetShoppingCart(Session("SeasonID"), HouseID)

            txtSeason.Value = rsData.Rows(0).Item("Sea_Desc")
            If Session("MailCheck") = 0 Then
                lblFee.Text = ((rsData.Rows(0).Item("PlayersAmount") + rsData.Rows(0).Item("SponsorAmount")) * 0.029) + 0.305
                If lblFee.Text < 0 Then lblFee.Text = 0
                lblFee.Text = Format(lblFee.Text * 1.029, "currency")
                lblFee.ToolTip = lblFee.Text * 1.029
                If rsData.Rows(0).Item("PlayersAmount") + rsData.Rows(0).Item("SponsorAmount") = 0D Then lblFee.Text = 0
                lblAmount.Text = Format(rsData.Rows(0).Item("PlayersAmount") + rsData.Rows(0).Item("SponsorAmount") + lblFee.Text, "currency")
                lblAmount.ToolTip = rsData.Rows(0).Item("PlayersAmount") + rsData.Rows(0).Item("SponsorAmount") + lblFee.Text
            Else
                '***************************************************
                lblFee.Text = (rsData.Rows(0).Item("PlayersAmount") * 0.029) + 0.305
                If lblFee.Text < 0 Then lblFee.Text = 0
                lblFee.Text = Format(lblFee.Text * 1.029, "currency")
                lblFee.ToolTip = lblFee.Text * 1.029
                If rsData.Rows(0).Item("PlayersAmount") = 0D Then lblFee.Text = 0
                lblAmount.Text = Format(rsData.Rows(0).Item("PlayersAmount") + lblFee.Text, "currency")
                lblAmount.ToolTip = rsData.Rows(0).Item("PlayersAmount") + lblFee.Text
                '***************************************************
            End If
            lblPlayers.Text = Format(rsData.Rows(0).Item("PlayersAmount"), "currency")
            If rsData.Rows(0).Item("SponsorAmount") > 0 Then
                lblSponsor.Text = Format(rsData.Rows(0).Item("SponsorAmount"), "currency")
            Else
                lblSponsor.Text = Format(0, "currency")
            End If
            Session("EMAIL") = rsData.Rows(0).Item("Email") & ""
            txtLast.Value = rsData.Rows(0).Item("LastName") & ""
            txtAddress.Value = rsData.Rows(0).Item("address1") & ""
            txtCity.Value = rsData.Rows(0).Item("city")
            txtState.Value = rsData.Rows(0).Item("state")
            txtZip.Value = rsData.Rows(0).Item("zip")
            lblAmount2.Text = lblAmount.Text
            If lblAmount.Text.ToString = "0" Then
                btnPayNow.Enabled = False
            Else
                btnPayNow.Enabled = True
            End If
            If Not IsDBNull(rsData.Rows(0).Item("Phone")) Then
                txtPhone1.Value = Left(rsData.Rows(0).Item("Phone"), 3) & ""
                txtPhone2.Value = Mid(rsData.Rows(0).Item("Phone"), 5, 3) & ""
                txtPhone3.Value = Mid(rsData.Rows(0).Item("Phone"), 9, 4) & ""
            End If

        Catch ex As Exception
            lblMSG.Text = "LoadData::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try

    End Sub

    Private Sub comPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comPrev.Click
        If CheckSponsors() > 0 Then
            Server.Transfer("RegSponsors.aspx")
        ElseIf CheckCoaches() > 0 Then
            Server.Transfer("RegCoaches.aspx")
        Else
            Server.Transfer("RegPlayers.aspx")
        End If
    End Sub

    Private Function CheckCoaches() As Integer
        CheckCoaches = 0
        Dim oPlayers As New Season.clsCoaches
        Dim dtResults As DataTable
        Try
            dtResults = oPlayers.GetHouseholdRegisteredCoaches(Session("HouseID"), Session("SeasonID"), Session("CompanyID"))
            If dtResults.Rows(0).Item("PeopleID") > 0 Then CheckCoaches = 1

        Catch ex As Exception
            lblMSG.Text = "CheckCoaches::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try


    End Function

    Private Function CheckSponsors() As Integer
        CheckSponsors = 0
        Dim oPlayers As New Season.clsSponsors
        Dim dtResults As DataTable
        Try
            dtResults = oPlayers.GetHouseholdRegisteredSponsor(Session("HouseID"), Session("SeasonID"), Session("CompanyID"))
            If dtResults.Rows(0).Item("PeopleID") > 0 Then CheckSponsors = 1
        Catch ex As Exception
            lblMSG.Text = "CheckSponsors::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Function

    Private Sub PaypalCharges()
        ' This creates the DoDirectPayment object. 
        Dim ppPay As New NvpDoDirectPayment()

        ' For this example application, get API credentials from Session, if available. 
        ' If you skip this, the class library will look in the web.config for the credentials. 


        Globals.SetCredentials(ppPay)



        ' Set the required parameters. Look below to see how 
        ' required parameters are set. 
        SetRequiredParameters(ppPay)

        ' Set any desired optional parameters. Look below to see how 
        ' optional parameters are set. 
        SetOptionalParameters(ppPay)

        '' If we're only creating an API request string, do that and exit. 
        'If Object.ReferenceEquals(e.GetType(), GetType(ActionEventArgs)) Then
        '    produceRequestString.RequestString = ppPay.RequestString
        '    Return
        'End If

        ' Call the Post() function to initiate the DoDirectPayment 
        ' API call. This will return true on success or false on failure. 
        If ppPay.Post() Then
            ' Payment was successful -- we are done! Display the results. 
            Session("TransactionID") = ppPay.Get(NvpDoDirectPayment.Response.TRANSACTIONID)
            Session("ACK") = ppPay.Get(NvpCommonResponse.ACK) '"Success"
            Session("AVS") = ppPay.Get(NvpDoDirectPayment.Response.AVSCODE)
            Session("CVV") = ppPay.Get(NvpDoDirectPayment.Response.CVV2MATCH)
            Response.Redirect("RegLoading.Aspx?Page=RegFinish.aspx")
        Else
            Session("ACK") = ppPay.Get(NvpCommonResponse.ACK) '"Failed"
            'lblMSG.Text = PayPalMessages(ppPay.ErrorList(0).Code, ppPay.ErrorList(0).LongMessage)
            lblMSG.Text = "(" & ppPay.ErrorList(0).Code & ") " & ppPay.ErrorList(0).LongMessage
            SaveErrorMessageOnPlayer("(" & ppPay.ErrorList(0).Code & ") " & ppPay.ErrorList(0).LongMessage)
            btnPayNow.Enabled = True

        End If
    End Sub

    Private Function PayPalMessages(ByVal RtnError As String, ByVal Errmsg As String) As String
        Select Case RtnError
            Case 10102
                PayPalMessages = "Error(" & RtnError & ") - Service momentary unavailable, try later"
            Case 10502, 10508, 15007
                PayPalMessages = "Error(" & RtnError & ") - Expired Credit Card or invalid expiration date"
            Case 10503, 10528
                PayPalMessages = "Error(" & RtnError & ") - Credit Limit Exceeded"
            Case 10505
                PayPalMessages = "Error(" & RtnError & ") - Card declined, check your address"
            Case 10506, 10505, 10751
                PayPalMessages = "Error(" & RtnError & ") - Transaction declined"
            Case 10519, 10521, 10522, 10527, 10535
                PayPalMessages = "Error(" & RtnError & ") - Invalid Card number"
            Case 10540, 10701, 10702, 10703, 10704, 10705, 10706, 10707, 10708, 10709, 10710, 10711, _
                10712, 10713, 10714, 10715, 10716, 10717, 10718, 10751, 10756
                PayPalMessages = "Error(" & RtnError & ") - Invalid address"
            Case 10542
                PayPalMessages = "Error(" & RtnError & ") - Invalid email provided"
            Case 10543, 10544, 10545, 10546, 10547, 10752, 10754, 10755, 10763
                PayPalMessages = "Error(" & RtnError & ") - We failed to authorize credit card"
            Case 10762, 10504, 10748, 15004
                PayPalMessages = "Error(" & RtnError & ") - Invalid Card Verification number"
            Case 10752, 15005, 15006
                PayPalMessages = "Error(" & RtnError & ") - Declined by your bank"
            Case 10534, 10539, 10541, 10544, 10545, 10546, 10754, 10759, 15002
                PayPalMessages = "Error(" & RtnError & ") - Declined by our bank (Paypal)"
            Case Else
                PayPalMessages = "Error(" & RtnError & ") - " & Errmsg
        End Select
    End Function

    Private Sub btnPayNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayNow.Click
        'If Session("HouseID") = 563 Then
        '    test()
        '    Response.Redirect("RegLoading.Aspx?Page=RegPayPalDirect.aspx?Process=TRUE")

        '    Exit Sub
        'End If
        If ErrorFnd() = False Then
            btnPayNow.Enabled = False
            Call SaveData()
            Response.Redirect("RegLoading.Aspx?Page=RegPayPalDirect.aspx?Process=TRUE")
        End If
    End Sub

    Private Function ErrorFnd() As Boolean
        ErrorFnd = False
        lblMSG.Text = ""
        If txtName.Value <= "" Then
            lblMSG.Text = "ERROR - Missing Name"
            SetFocus(txtName)
            ErrorFnd = True
            Exit Function
        End If
        If txtLast.Value <= "" Then
            lblMSG.Text = "ERROR - Missing Last Name"
            SetFocus(txtLast)
            ErrorFnd = True
            Exit Function
        End If
        If txtAddress.Value <= "" Then
            lblMSG.Text = "ERROR - Missing Address"
            SetFocus(txtAddress)
            ErrorFnd = True
            Exit Function
        End If
        If txtCity.Value <= "" Then
            lblMSG.Text = "ERROR - Missing City"
            SetFocus(txtCity)
            ErrorFnd = True
            Exit Function
        End If
        If txtState.Value <= "" Then
            lblMSG.Text = "ERROR - Missing State"
            SetFocus(txtState)
            ErrorFnd = True
            Exit Function
        End If
        If txtZip.Value <= "" Then
            lblMSG.Text = "ERROR - Missing Zipcode"
            SetFocus(txtZip)
            ErrorFnd = True
            Exit Function
        End If
        If Not IsNumeric(txtPhone1.Value) Or Not IsNumeric(txtPhone2.Value) Or Not IsNumeric(txtPhone3.Value) Then
            lblMSG.Text = "ERROR - Missing or Incomplete Phone Number"
            SetFocus(txtPhone1)
            ErrorFnd = True
            Exit Function
        End If
        If cmbCC.SelectedIndex = 0 Then
            lblMSG.Text = "ERROR - Missing Credit Card Type"
            SetFocus(cmbCC)
            ErrorFnd = True
            Exit Function
        End If
        If Not IsNumeric(txtCard.Value) Then
            lblMSG.Text = "ERROR - Missing or Invalid Credit Card Number"
            SetFocus(txtCard)
            ErrorFnd = True
            Exit Function
        End If
        If Not IsNumeric(txtCVV2.Value) Then
            lblMSG.Text = "ERROR - Missing or Invalid CC Verification Number"
            SetFocus(txtCVV2)
            ErrorFnd = True
            Exit Function
        End If
        If (cmbCC.SelectedIndex = 1 Or cmbCC.SelectedIndex = 2 Or cmbCC.SelectedIndex = 3) And Len(txtCVV2.Value) < 3 Then
            lblMSG.Text = "ERROR - Invalid Verification Number"
            SetFocus(txtCVV2)
            ErrorFnd = True
            Exit Function
        End If
        If cmbCC.SelectedIndex = 4 And Len(txtCVV2.Value) <> 4 Then
            lblMSG.Text = "ERROR - Invalid Verification Number"
            SetFocus(txtCVV2)
            ErrorFnd = True
            Exit Function
        End If
    End Function

    Private Sub SaveData()
        Session("Season") = txtSeason.Value
        Session("SponsorAmount") = lblSponsor.Text
        Session("PlayersTotal") = lblPlayers.Text
        Session("AMOUNT") = lblAmount.Text
        Session("Fee") = lblFee.Text
        Session("CCTYPE") = cmbCC.SelectedIndex
        Session("CCNumber") = txtCard.Value
        Session("ExpMonth") = Integer.Parse(cmbMonth.SelectedItem.Value).ToString("D2")
        Session("ExpYear") = Integer.Parse(cmbYear.SelectedItem.Value).ToString("D4")
        Session("CVV2") = Integer.Parse(txtCVV2.Value).ToString("D3")
        Session("Name") = UCase(txtName.Value)
        Session("Last") = UCase(txtLast.Value)
        Session("txtAddress") = txtAddress.Value
        Session("City") = txtCity.Value
        Session("State") = txtState.Value
        Session("Zip") = txtZip.Value
        Session("Phone1") = txtPhone1.Value
        Session("Phone2") = txtPhone2.Value
        Session("Phone3") = txtPhone3.Value
        Session("Pay") = 0
    End Sub

    Private Sub SaveErrorMessageOnPlayer(ByVal sErrorMsg As String)
        Dim oPlayers As New Season.ClsPlayers
        Try
            oPlayers.PayErrorLog(Session("SeasonID"), Session("HouseID"), sErrorMsg)
            'Sql = "Insert into PayErrorLog (SeasonID, HouseID, ErrorMSG) VALUES (" & Session("SeasonID") & ", " & Session("HouseID") & ", " & Quo(sErrorMsg) & ")"
        Catch ex As Exception
            lblMSG.Text = "SaveErrorMessageOnPlayer::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Sub


    Private Sub SetRequiredParameters(ByVal ppPay As NvpDoDirectPayment)
        ' AMT, PAYMENTACTION, IPADDRESS, CREDITCARDTYPE, ACCT, EXPDATE, FIRSTNAME, and LASTNAME 
        ' are the parameters required to initialize the DoDirectPayment API call. 

        ppPay.Add(NvpDoDirectPayment.Request._AMT, Session("AMOUNT"))
        ppPay.Add(NvpDoDirectPayment.Request._PAYMENTACTION, NvpPaymentActionCodeType.Sale)
        ppPay.Add(NvpDoDirectPayment.Request._IPADDRESS, Session("IPAddress"))

        Select Case Session("CCTYPE")
            Case 1
                ppPay.Add(NvpDoDirectPayment.Request._CREDITCARDTYPE, NvpCreditCardTypeType.Visa)
            Case 2
                ppPay.Add(NvpDoDirectPayment.Request._CREDITCARDTYPE, NvpCreditCardTypeType.MasterCard)
            Case 3
                ppPay.Add(NvpDoDirectPayment.Request._CREDITCARDTYPE, NvpCreditCardTypeType.Discover)
            Case 4
                ppPay.Add(NvpDoDirectPayment.Request._CREDITCARDTYPE, NvpCreditCardTypeType.Amex)
        End Select

        ppPay.Add(NvpDoDirectPayment.Request._ACCT, Session("CCNumber"))
        Dim exp As String = String.Empty
        exp = Session("ExpMonth")
        exp += Session("ExpYear")

        ppPay.Add(NvpDoDirectPayment.Request._EXPDATE, exp)
        ppPay.Add(NvpDoDirectPayment.Request._FIRSTNAME, Session("Name"))
        ppPay.Add(NvpDoDirectPayment.Request._LASTNAME, Session("Last"))
    End Sub

    ' Look here to see the optional parameters. 
    Private Sub SetOptionalParameters(ByVal ppPay As NvpDoDirectPayment)
        ' optional credit card info 
        ppPay.Add(NvpDoDirectPayment.Request.CVV2, Session("CVV2"))
        ppPay.Add(NvpDoDirectPayment.Request.EMAIL, Session("EMAIL"))

        ' optional credit card address info 
        ppPay.Add(NvpDoDirectPayment.Request.STREET, Session("txtAddress"))
        ppPay.Add(NvpDoDirectPayment.Request.CITY, Session("City"))
        ppPay.Add(NvpDoDirectPayment.Request.STATE, Session("State"))
        ppPay.Add(NvpDoDirectPayment.Request.ZIP, Session("Zip"))
        ppPay.Add(NvpDoDirectPayment.Request.COUNTRYCODE, "US")

        ' optional payment details 
        ppPay.Add(NvpDoDirectPayment.Request.DESC, Session("Season"))
    End Sub


End Class
