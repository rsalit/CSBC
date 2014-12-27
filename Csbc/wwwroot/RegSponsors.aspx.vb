Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class RegSponsors
    Inherits System.Web.UI.Page
    Private sGlobal As New CSBC.Components.ClsGlobal
    Dim SQL As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Session("Title") = "Sponsor Information"
        If Session("HouseID") <= 0 Or Session("USERID") <= 0 Then
            Response.Redirect("RegLogin.aspx")
        End If
        If Page.IsPostBack = False Then
            Call LoadColors()
            Call LoadRow(Session("HouseID"))
            Call LoadPlayers(Session("HouseID"))
            comNext.Focus()
        End If
    End Sub

    Private Sub LoadRow(ByVal HouseID As Long)
        Dim oSponsors As New Season.clsSponsors
        Dim rsData As DataTable
        Try
            lblPlayers.Text = ""
            rsData = oSponsors.GetSponsor(HouseID, Session("SeasonID"), Session("CompanyID"))
            If rsData.Rows.Count > 0 Then
                Session("SponsorID") = rsData.Rows(0).Item("SponsorID")
                txtSponsor.Text = rsData.Rows(0).Item("SpoName") & ""
                txtSponsor.ToolTip = txtSponsor.Text
                txtContact.Text = rsData.Rows(0).Item("ContactName") & ""
                txtContact.ToolTip = txtContact.Text
                txtAddress.Text = rsData.Rows(0).Item("Address") & ""
                txtAddress.ToolTip = txtAddress.Text
                txtCity.Text = rsData.Rows(0).Item("City") & ""
                txtCity.ToolTip = txtCity.Text
                txtState.Text = rsData.Rows(0).Item("State") & ""
                txtState.ToolTip = txtState.Text
                txtZip.Text = rsData.Rows(0).Item("Zip") & ""
                txtZip.ToolTip = txtZip.Text
                txtEmail.Text = rsData.Rows(0).Item("Email") & ""
                txtEmail.ToolTip = txtEmail.Text
                txtWebsite.Text = rsData.Rows(0).Item("URL") & ""
                txtWebsite.ToolTip = txtWebsite.Text
                cmbColors1.SelectedValue = rsData.Rows(0).Item("Color1ID") & ""
                cmbColors1.ToolTip = rsData.Rows(0).Item("Color1ID") & ""
                cmbColors2.SelectedValue = rsData.Rows(0).Item("Color2ID") & ""
                cmbColors2.ToolTip = rsData.Rows(0).Item("Color2ID") & ""
                chkCheck.Checked = rsData.Rows(0).Item("MailCheck")
                chkCheckOLD.Checked = chkCheck.Checked
                If Not IsDBNull(rsData.Rows(0).Item("ShirtSize")) Then
                    cmbShirts.SelectedValue = rsData.Rows(0).Item("ShirtSize") & ""
                    cmbShirtsOLD.SelectedValue = rsData.Rows(0).Item("ShirtSize") & ""
                End If
                If Not IsDBNull(rsData.Rows(0).Item("Phone")) Then
                    txtPhone1.Text = Trim(Left(rsData.Rows(0).Item("Phone"), 3)) & ""
                    txtPhone1.ToolTip = txtPhone1.Text
                    txtPhone2.Text = Trim(Mid(rsData.Rows(0).Item("Phone"), 5, 3)) & ""
                    txtPhone2.ToolTip = txtPhone2.Text
                    txtPhone3.Text = Trim(Mid(rsData.Rows(0).Item("Phone"), 9, 4)) & ""
                    txtPhone3.ToolTip = txtPhone3.Text
                End If
                comCancel.Enabled = True
                'btnCancel.Enabled = True
            Else
                Call ClearFields()
            End If
        Catch ex As Exception
            lblMSG.Text = "LoadRow::" & ex.Message
        Finally
            oSponsors = Nothing
        End Try
    End Sub

    Private Sub LoadPlayers(ByVal HouseID As Integer)
        Dim oPlayers As New Season.ClsPlayers
        Dim rsData As DataTable
        Try
            lblPlayers.Text = ""
            rsData = oPlayers.GetHouseholdRegisteredPlayers(HouseID, Session("SeasonID"))
            chkPlayers.Items.Clear()
            'cmbColors1.Items.Add(New ListItem("Select 1st Choice", 0))
            For I As Int16 = 0 To rsData.Rows.Count - 1
                chkPlayers.Items.Add(New ListItem(Trim(rsData.Rows(I).Item("Name")), rsData.Rows(I).Item("PlayerID")))
                If chkPlayers.Items(I).Selected = True Then
                    If lblPlayers.Text > " " Then
                        lblPlayers.Text = lblPlayers.Text & "," & rsData.Rows(I).Item("PlayerID")
                    Else
                        lblPlayers.Text = rsData.Rows(I).Item("PlayerID")
                    End If
                End If
                If rsData.Rows(I).Item("SponsorID") = Session("SponsorID") Then
                    chkPlayers.Items(I).Selected = True
                End If

            Next
            If rsData.Rows.Count = 0 Then
                chkPlayers.Items.Add(New ListItem("ANY", 0))
                chkPlayers.Items(0).Selected = True
            End If
        Catch ex As Exception
            lblMSG.Text = "LoadPlayers::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Sub

    Private Sub UpdPlayer()
        Dim oPlayers As New Season.ClsPlayers
        Dim I As Integer
        Try
            For I = 0 To chkPlayers.Items.Count - 1
                If chkPlayers.Items(I).Selected = True Then oPlayers.SponsorID = Session("SponsorID")
                If chkPlayers.Items(I).Selected = False Then oPlayers.SponsorID = 0
                oPlayers.UpdatePlayerSponsor(Session("CompanyID"), chkPlayers.Items(I).Value)
            Next I
        Catch ex As Exception
            lblMSG.Text = "UpdPlayer::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Sub

    Private Function CheckCoaches() As String
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

    Private Sub comUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comUpdate.Click
        Call SaveData()

    End Sub

    Private Sub SaveData()
        If errorRTN() = False Then
            comCancel.Enabled = True
            If txtSponsor.ToolTip > "" Then
                Call UPDSponsor()
                Call UpdPlayer()
                lblMSG.Text = "Changes successfully completed"
            Else
                Call UPDSponsor()
                Call UpdPlayer()
                lblMSG.Text = txtSponsor.Text & " SIGNED UP AS SPONSOR "
                Call ResetFields()
            End If

            'Call ClearFields()
        End If
    End Sub

    Private Function errorRTN() As Boolean
        Dim I As Integer
        Dim Players As Integer = 0
        errorRTN = False
        lblMSG.Text = ""
        For I = 0 To chkPlayers.Items.Count - 1
            If chkPlayers.Items(I).Selected = True Then Players = Players + 1
        Next I
        If txtSponsor.Text = "" Then
            lblMSG.Text = "Company Name missing "
            errorRTN = True
        ElseIf txtAddress.Text = "" Then
            lblMSG.Text = "Address missing "
            errorRTN = True
        ElseIf txtEmail.Text = "" Then 'Or validateEmail(txtEmail.Text) = False Then
            'TODO NEED TO VALIDATE EMAIL
            lblMSG.Text = "Email missing or invalid"
            errorRTN = True
        ElseIf txtCity.Text = "" Then
            lblMSG.Text = "City Name missing "
            errorRTN = True
        ElseIf txtState.Text = "" Then
            lblMSG.Text = "State missing "
            errorRTN = True
        ElseIf (txtPhone1.Text > "" Or txtPhone2.Text > "" Or txtPhone3.Text > "") And _
            (Not IsNumeric(txtPhone1.Text) Or Len(txtPhone1.Text) < 3 Or _
            Not IsNumeric(txtPhone2.Text) Or Len(txtPhone2.Text) < 3 Or _
            Not IsNumeric(txtPhone3.Text) Or Len(txtPhone3.Text) < 4) Then
            lblMSG.Text = "phone missing or invalid"
            errorRTN = True
        ElseIf Players = 0 Then
            lblMSG.Text = "No Player(s) selected"
            errorRTN = True
        End If
    End Function

    Private Sub ClearFields()
        Dim I As Integer
        txtSponsor.Text = ""
        txtSponsor.ToolTip = ""
        txtAddress.Text = ""
        txtAddress.ToolTip = ""
        txtCity.Text = "Coral Springs"
        txtCity.ToolTip = txtCity.Text
        cmbColors1.SelectedValue = 0
        cmbColors1.ToolTip = ""
        cmbColors2.SelectedValue = 0
        cmbColors2.ToolTip = ""
        cmbColors2.SelectedValue = 0
        txtContact.Text = ""
        txtContact.ToolTip = ""
        txtEmail.Text = ""
        txtEmail.ToolTip = ""
        txtState.Text = "FL"
        txtState.ToolTip = txtState.Text
        txtWebsite.Text = ""
        txtWebsite.ToolTip = ""
        txtZip.Text = ""
        txtZip.ToolTip = ""
        txtPhone1.Text = ""
        txtPhone1.ToolTip = ""
        txtPhone2.Text = ""
        txtPhone2.ToolTip = ""
        txtPhone3.Text = ""
        txtPhone3.ToolTip = ""
        'cmbShirts.SelectedItem.Value = 0
        cmbShirts.SelectedValue = "N/A"
        cmbShirtsOLD.SelectedValue = cmbShirts.SelectedValue
        chkCheck.Checked = False
        chkCheckOLD.Checked = False
        For I = 0 To chkPlayers.Items.Count - 1
            chkPlayers.Items(I).Selected = False
        Next I
    End Sub

    Private Sub UPDSponsor()
        Dim oSponsor As New Season.clsSponsors
        Try
            With oSponsor
                If IsNumeric(txtPhone1.Text) And IsNumeric(txtPhone2.Text) And IsNumeric(txtPhone3.Text) Then
                    .Phone = txtPhone1.Text & "-" & txtPhone2.Text & "-" & txtPhone3.Text
                Else
                    .Phone = ""
                End If
                .ShirtSize = cmbShirts.SelectedItem.Value
                .SpoName = txtSponsor.Text
                .ContactName = txtContact.Text
                .Email = txtEmail.Text
                .URL = txtWebsite.Text
                .Address = txtAddress.Text
                .City = txtCity.Text
                .State = txtState.Text
                .Zip = txtZip.Text
                .Color1ID = cmbColors1.SelectedValue
                .Color2ID = cmbColors2.SelectedValue
                .UserName = Session("UserName")
                If chkCheck.Checked = True Then .MailCheck = 1
                If chkCheck.Checked = False Then .MailCheck = 0
                txtSponsor.ToolTip = txtSponsor.Text
                txtContact.ToolTip = txtContact.Text
                txtAddress.ToolTip = txtAddress.Text
                txtCity.ToolTip = txtCity.Text
                txtZip.ToolTip = txtZip.Text
                txtState.ToolTip = txtState.Text
                txtWebsite.ToolTip = txtWebsite.Text
                txtEmail.ToolTip = txtEmail.Text
                txtPhone1.ToolTip = txtPhone1.Text
                txtPhone2.ToolTip = txtPhone2.Text
                txtPhone3.ToolTip = txtPhone3.Text
                cmbColors1.ToolTip = cmbColors1.SelectedValue
                cmbColors2.ToolTip = cmbColors2.SelectedValue
                cmbShirts.SelectedValue = cmbShirtsOLD.SelectedValue
                chkCheckOLD.Checked = chkCheck.Checked
            End With
            Session("SponsorID") = oSponsor.AssignSponsor(Session("HouseID"), Session("CompanyID"), Session("SeasonID"))

        Catch ex As Exception
            lblMSG.Text = "UPDSponsor::" & ex.Message
        Finally
            oSponsor = Nothing
        End Try

    End Sub

    Private Sub comCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comCancel.Click
        '1) Put a label control beneath your button but sets its Visible property to false.  Name it lblConfirm.
        '2) Set the CommandArgument property of your button to "Confirm" (u can use any string, but "Confirm" will do for this example).
        '3) In your server-side click event, do this:
        Dim btn As Button = CType(sender, Button)
        If lblDeleteSponsor.Text = "" Then btn.CommandArgument = "Confirm"
        If btn.CommandArgument = "Confirm" Then
            lblDeleteSponsor.Text = "*Click Delete button again to confirm.*"
            lblDeleteSponsor.Visible = True
            btn.CommandArgument = "Delete"
        ElseIf btn.CommandArgument = "Delete" Then
            Call DELSponsor(Session("HouseID"))
            lblDeleteSponsor.Visible = False
            btn.CommandArgument = "Confirm"
        End If

        'You are using the label as a message box of sorts and the CommandArgument of the button to tell the click event whether 
        'you need to confirm or have confirmed.
    End Sub

    Private Sub DELSponsor(ByVal HouseID As Integer)
        Dim oSponsors As New Season.clsSponsors
        Try
            oSponsors.DeleteSponsorOR(HouseID, Session("SeasonID"))
        Catch ex As Exception
            lblMSG.Text = "DELSponsor::" & ex.Message
        Finally
            oSponsors = Nothing
        End Try
        lblMSG.Text = txtSponsor.Text & " WAS REMOVED FROM SPONSORING "
        Call ClearFields()
    End Sub

    Private Function CheckUpdates() As Boolean
        CheckUpdates = False
        Dim I As Integer
        Dim PlayersCount As Integer = 0
        Dim sPlayers As String = ""
        For I = 0 To chkPlayers.Items.Count - 1
            If chkPlayers.Items(I).Selected = True Then
                PlayersCount = PlayersCount + 1
                If sPlayers > "" Then
                    sPlayers = sPlayers & "," & chkPlayers.Items(I).Value
                Else
                    sPlayers = chkPlayers.Items(I).Value
                End If
            End If
        Next I
        If PlayersCount = 0 Then Exit Function
        If txtSponsor.ToolTip <> txtSponsor.Text Or _
            txtContact.ToolTip <> txtContact.Text Or _
            txtAddress.ToolTip <> txtAddress.Text Or _
            txtCity.ToolTip <> txtCity.Text Or _
            txtZip.ToolTip <> txtZip.Text Or _
            txtState.ToolTip <> txtState.Text Or _
            txtWebsite.ToolTip <> txtWebsite.Text Or _
            txtEmail.ToolTip <> txtEmail.Text Or _
            txtPhone1.ToolTip <> txtPhone1.Text Or _
            txtPhone2.ToolTip <> txtPhone2.Text Or _
            txtPhone3.ToolTip <> txtPhone3.Text Or _
            cmbColors1.ToolTip <> cmbColors1.SelectedValue Or _
            cmbColors2.ToolTip <> cmbColors2.SelectedValue Or _
            chkCheck.Checked <> chkCheckOLD.Checked Or _
            sPlayers <> lblPlayers.Text Or _
            cmbShirts.SelectedValue <> cmbShirtsOLD.SelectedValue Then
            'lblMSG.Text = "Changes pending, 'Save' or 'Cancel'??"
            CheckUpdates = True
        End If

    End Function

    Private Sub ResetFields()
        txtSponsor.Text = txtSponsor.ToolTip & ""
        txtContact.Text = txtContact.ToolTip & ""
        txtAddress.Text = txtAddress.ToolTip & ""
        txtCity.Text = txtCity.ToolTip & ""
        txtZip.Text = txtZip.ToolTip & ""
        txtState.Text = txtState.ToolTip & ""
        txtWebsite.Text = txtWebsite.ToolTip & ""
        txtEmail.Text = txtEmail.ToolTip & ""
        cmbShirts.SelectedValue = cmbShirtsOLD.SelectedValue
        txtPhone1.Text = txtPhone1.ToolTip & ""
        txtPhone2.Text = txtPhone2.ToolTip & ""
        txtPhone3.Text = txtPhone3.ToolTip & ""
        cmbColors1.SelectedValue = cmbColors1.ToolTip
        cmbColors2.SelectedValue = cmbColors2.ToolTip
        chkCheck.Checked = chkCheckOLD.Checked
    End Sub

    Private Sub LoadColors()
        Dim oColors As New Website.ClsColors
        Dim rsData As DataTable

        Try
            rsData = oColors.LoadColors(0, Session("CompanyID"), 0, 0)
            cmbColors1.Controls.Clear()
            cmbColors2.Controls.Clear()

            cmbColors1.Items.Add(New ListItem("Select 1st Choice", 0))
            cmbColors2.Items.Add(New ListItem("Select 2nd Choice", 0))

            For I As Int16 = 0 To rsData.Rows.Count - 1
                cmbColors1.Items.Add(New ListItem(Trim(rsData.Rows(I).Item("ColorName")), rsData.Rows(I).Item("ID")))
                cmbColors2.Items.Add(New ListItem(Trim(rsData.Rows(I).Item("ColorName")), rsData.Rows(I).Item("ID")))
            Next
        Catch ex As Exception
            lblMSG.Text = "LoadColors::" & ex.Message
        Finally
            oColors = Nothing
        End Try
    End Sub

    Protected Sub comPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles comPrev.Click
        Call SaveData()
        If CheckCoaches() > 0 Then
            Server.Transfer("RegCoaches.aspx")
        Else
            Server.Transfer("RegPlayers.aspx")
        End If

    End Sub

    Protected Sub comNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles comNext.Click
        Server.Transfer("RegPayPalDirect.aspx")
    End Sub

End Class
