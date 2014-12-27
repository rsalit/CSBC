Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class RegHouse
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("HouseID") <= 0 Or Session("USERID") <= 0 Then
            Response.Redirect("RegLogin.aspx")
        End If
        Session("Module") = "RegHouse.aspx"
        Session("Title") = "Households Information"
        If Page.IsPostBack = False Then
            If Session("ADMINCODE") > 0 Then Session("USERID") = Session("ADMINCODE")
            Call LoadRow(Session("HouseID"))
            If Session("CartID") > 0 Then hpEmail.Visible = True
            comNext.Focus()
        End If
    End Sub

    Private Sub LoadRow(ByVal RowID As Long)
        Dim oPlayers As New Season.ClsPlayers
        Try
            With oPlayers
                .GetHouseholdCart(RowID, Session("SeasonID"))
                txtName.Text = .Name
                txtName.ToolTip() = .Name
                txtAddress.Text = .Address1
                txtAddress.ToolTip = txtAddress.Text
                txtAddress2.Text = .Address2
                txtAddress2.ToolTip = txtAddress2.Text
                txtCity.Text = .City
                txtCity.ToolTip = txtCity.Text
                txtZip.Text = .Zip
                txtZip.ToolTip = txtZip.Text
                If Not IsDBNull(.Phone) Then
                    txtPhone1.Text = Left(.Phone, 3)
                    txtPhone1.ToolTip = txtPhone1.Text
                    txtPhone2.Text = Mid(.Phone, 5, 3)
                    txtPhone2.ToolTip = txtPhone2.Text
                    txtPhone3.Text = Mid(.Phone, 9, 4)
                    txtPhone3.ToolTip = txtPhone3.Text
                End If
                txtEmail.Text = .Email
                txtEmail.ToolTip = txtEmail.Text
                txtEmailOLD.Text = .Email
                txtState.Text = .State
                TxtEmailListing.Text = .EmailList
                Session("CartID") = .CartID
                Session.Add("LinkName", txtName.Text)
            End With
        Catch ex As Exception
            lblMSG.Text = "Invalid HouseID"
        Finally
            oPlayers = Nothing
        End Try
    End Sub

    Private Sub UpdRow(ByVal RowID As Long)
        Dim oHouseholds As New Profile.ClsHouseholds
        Try
            With oHouseholds
                .Name = txtName.Text
                .Address1 = txtAddress.Text
                .Address2 = txtAddress2.Text
                .City = txtCity.Text
                .Email = txtEmail.Text
                .Zip = txtZip.Text
                .Phone = txtPhone1.Text & "-" & txtPhone2.Text & "-" & txtPhone3.Text
                .State = txtState.Text
                .EmailList = TxtEmailListing.Text
                oHouseholds.UpdRow(RowID, Session("CompanyID"), Session("TimeZone"))
                txtName.ToolTip() = txtName.Text
                txtAddress.ToolTip = txtAddress.Text
                txtAddress2.ToolTip = txtAddress2.Text
                txtCity.ToolTip = txtCity.Text
                txtZip.ToolTip = txtZip.Text
                txtEmail.ToolTip = txtEmail.Text
                txtEmailOLD.Text = txtEmail.ToolTip

            End With
        Catch ex As Exception
            Session("ErrorMSG") = "UpdRow::" & ex.Message
        Finally
            oHouseholds = Nothing
        End Try
    End Sub

    Private Sub ClearFields()
        txtName.Text = ""
        txtAddress.Text = ""
        txtAddress2.Text = ""
        txtCity.Text = "CORAL SPRINGS"
        txtZip.Text = ""
        txtPhone1.Text = ""
        txtPhone2.Text = ""
        txtPhone3.Text = ""
        txtEmail.Text = ""
        txtEmailOLD.Text = ""
    End Sub

    Private Sub ResetFields()
        txtName.Text = txtName.Text & ""
        txtAddress.Text = txtAddress.ToolTip & ""
        txtAddress2.Text = txtAddress2.ToolTip & ""
        txtCity.Text = txtCity.ToolTip & ""
        txtZip.Text = txtZip.ToolTip & ""
        txtEmail.Text = txtEmail.ToolTip & ""
        txtPhone1.Text = txtPhone1.ToolTip & ""
        txtPhone2.Text = txtPhone2.ToolTip & ""
        txtPhone3.Text = txtPhone3.ToolTip & ""
    End Sub


    Private Function errorRTN() As Boolean
        errorRTN = False
        If txtName.Text = "" Then
            lblMSG.Text = "Name missing "
            errorRTN = True
        ElseIf txtAddress.Text = "" Then
            lblMSG.Text = "Address missing "
            errorRTN = True
        ElseIf txtCity.Text = "" Then
            lblMSG.Text = "City missing "
            errorRTN = True
        ElseIf txtZip.Text = "" Then
            lblMSG.Text = "Zip code missing "
            errorRTN = True
        ElseIf Not IsNumeric(txtPhone1.Text) Or Len(txtPhone1.Text) < 3 Or _
            Not IsNumeric(txtPhone2.Text) Or Len(txtPhone2.Text) < 3 Or _
            Not IsNumeric(txtPhone3.Text) Or Len(txtPhone3.Text) < 4 Then
            lblMSG.Text = "phone missing or invalid"
            errorRTN = True
        ElseIf txtEmail.Text = "" Then
            lblMSG.Text = "email missing"
            errorRTN = True
        End If
    End Function

    Private Sub UpdEmail(ByVal RowID As Long)
        If Session("USERACCESS") = "R" Then Exit Sub
        Dim oEmail As New Profile.ClsEmails
        If txtEmail.Text <> txtEmailOLD.Text And txtEmail.Text > "" Then
            Try
                With oEmail
                    .EmailAddress = txtEmail.Text
                    .SeasonId = Session("SeasonID")
                    .UpdEmail(RowID, Session("CompanyID"))
                End With
            Catch ex As Exception
                lblMSG.Text = "Invalid Email"
            Finally
                oEmail = Nothing
            End Try
        End If
    End Sub

    Private Sub SaveData()
        If Session("HouseID") > 0 Then
            If errorRTN() = False Then
                Call UpdRow(Session("HouseID"))
                Call UpdEmail(Session("HouseID"))
                lblMSG.Text = "Household changes Saved"
                'Call ResetFields()
            End If
        End If
    End Sub

    Private Sub lnkNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Server.Transfer("RegParents.aspx", True)
    End Sub

    Private Sub comNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comNext.Click
        Dim ErrFound As Boolean = errorRTN()
        If Session("HouseID") > 0 And ErrFound = False Then
            If CheckUpdates() = True Then
                Call SaveData()
                'Exit Sub
            End If
        End If
        If ErrFound = True Then Exit Sub
        Session("Email") = txtEmail.Text
        Server.Transfer("RegParents.aspx")
    End Sub

    Private Function CheckUpdates() As Boolean
        CheckUpdates = False
        If txtName.ToolTip <> txtName.Text Or _
            txtAddress.ToolTip <> txtAddress.Text Or _
            txtAddress2.ToolTip <> txtAddress2.Text Or _
            txtCity.ToolTip <> txtCity.Text Or _
            txtZip.ToolTip <> txtZip.Text Or _
            txtEmail.ToolTip <> txtEmail.Text Or _
            txtPhone1.ToolTip <> txtPhone1.Text Or _
            txtPhone2.ToolTip <> txtPhone2.Text Or _
            txtPhone3.ToolTip <> txtPhone3.Text Then
            'lblMSG.Text = "Changes pending, 'Save' or 'Cancel'??"
            CheckUpdates = True
        End If
    End Function

End Class
