Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class RegPlayers
    Inherits System.Web.UI.Page
  
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("HouseID") <= 0 Or Session("USERID") <= 0 Then
            Response.Redirect("RegLogin.aspx")
        End If
        Session("Title") = "Players Information"
        If Page.IsPostBack = False Then
            Call LoadPlayers(Session("HouseID"))
            comNext.Focus()
        End If
    End Sub

    Private Sub LoadPlayers(ByVal HouseID As Integer)
        Dim oPlayers As New Season.ClsPlayers
        Dim rsData As DataTable

        Try
            'rsData = oPlayers.GetPlayers(Session("SeasonID"), HouseID, Session("CompanyID"))
            rsData = oPlayers.GetHousePlayers(0, Session("SeasonID"), HouseID, Session("CompanyID"))
            grdMembers.Controls.Clear()
            If rsData.Rows.Count > 0 Then
                With grdMembers
                    .DataSource = rsData
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            lblMSG.Text = "LoadPlayers::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Sub

    Private Sub LoadRow(ByVal PeopleID As Long)
        Dim oPlayers As New Season.ClsPlayers
        Dim rsData As DataTable
        Try
            'rsData = oPlayers.GetPlayerByPeopleID(Session("SeasonID"), PeopleID, Session("CompanyID"))
            rsData = oPlayers.GetRecords(PeopleID, Session("CompanyID"), Session("SeasonID"))
            If Not rsData Is Nothing Then
                If rsData.Rows.Count > 0 Then
                    lblPlayerName.Text = rsData.Rows(0).Item("Name") & ""
                    If IsDBNull(rsData.Rows(0).Item("Gender")) Then
                    Else
                        If rsData.Rows(0).Item("Gender") = "M" Then
                            radPlayerGender.Items(0).Selected() = True
                            radPlayerGender.Items(1).Selected() = False
                        Else
                            radPlayerGender.Items(1).Selected() = True
                            radPlayerGender.Items(0).Selected() = False
                        End If
                    End If
                    txtSchool.Text = rsData.Rows(0).Item("schoolname") & ""
                    txtSchool.ToolTip = txtSchool.Text
                    cmbGrade.SelectedIndex = rsData.Rows(0).Item("Grade") & ""
                    cmbGradeOLD.SelectedIndex = rsData.Rows(0).Item("Grade") & ""
                    lblDate.Text = rsData.Rows(0).Item("BirthDate") & ""
                    txtDraftNotes.Text = rsData.Rows(0).Item("DraftNotes") & ""
                    txtDraftNotes.ToolTip = txtDraftNotes.Text
                    If IsDBNull(rsData.Rows(0).Item("Div_Desc")) Then
                        lblDiv.Text = ""
                    Else
                        lblDiv.Text = "Division: " & rsData.Rows(0).Item("Div_Desc")
                    End If
                End If
            End If
        Catch ex As Exception
            lblMSG.Text = "LoadRow::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Sub

    Private Sub HideShowFields(ByVal iOnOff As Boolean)
        Dim bOffOn As Boolean
        bOffOn = Not iOnOff
        txtSchool.Enabled = iOnOff
        cmbGrade.Enabled = iOnOff
        txtDraftNotes.Enabled = iOnOff
        comUpdate.Enabled = iOnOff
        comCancel.Enabled = iOnOff
    End Sub

    Private Sub UpdRow(ByVal PeopleID As Long)
        If Session("PlayerID") > 0 Then
            Call UPDPlayer()
        End If
    End Sub

    Private Sub comPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comPrev.Click
        Session("PeopleID") = 0
        Server.Transfer("RegParents.aspx")
    End Sub

    Private Sub comUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comUpdate.Click
        If Session("PeopleID") = 0 Then
            lblMSG.Text = "Registration failed, try again, select player and click on REGISTER"
            Call HideShowFields(0)
            Call ClearFields()
            Exit Sub
        Else
            Call UPDPlayer()
        End If

        lblMSG.Text = grdMembers.SelectedItem.Cells(2).Text & " REGISTERED ...PENDING PAYMENT!"
        Call HideShowFields(0)
        Call ClearFields()
        Call LoadPlayers(Session("HouseID"))
        Call ResetFields()
    End Sub

    Private Sub ClearFields()
        txtDraftNotes.Text = ""
        lblPlayerName.Text = ""
        lblDate.Text = ""
        txtSchool.Text = ""
        lblDiv.Text = ""
        radPlayerGender.Items(0).Selected() = False
        radPlayerGender.Items(1).Selected() = False
        cmbGrade.SelectedIndex = 13
        cmbGradeOLD.SelectedIndex = 13
    End Sub

    Private Sub grdMembers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMembers.SelectedIndexChanged
        If CheckUpdates() = True Then Call UPDPlayer()
        Session("PeopleID") = grdMembers.SelectedItem.Cells(0).Text
        If Session("PeopleID") > "0" Then
            Call LoadRow(Session("PeopleID"))
            Call SetFocus(txtSchool)
            Call HideShowFields(1)
            lblMSG.Text = ""
            Dim iDiv As Int16 = getDivision()
            lblDiv.ToolTip = iDiv
            If grdMembers.SelectedItem.Cells(4).Text = "&nbsp;" Or grdMembers.SelectedItem.Cells(4).Text = "NOT Registered" Then
                comUpdate.Enabled = True
                comCancel.Enabled = False
            Else
                comUpdate.Enabled = False
                comCancel.Enabled = True
            End If
            If grdMembers.SelectedItem.Cells(4).Text = "Registered" Or iDiv = 0 Then
                comCancel.Enabled = False
                comUpdate.Enabled = False
            End If
            If iDiv = 0 Then
                lblDiv.Text = "NO DIVISION FOR THIS AGE GROUP"
            End If
        End If
    End Sub

    Private Function getDivision() As Integer
        getDivision = 0
        Dim oDivision As New Season.ClsDivisions
        Dim rsData As DataTable

        Try
            rsData = oDivision.GetPlayerDivision(Session("PeopleID"), Session("CompanyID"), Session("SeasonID"))
            'rsData = oDivision.GetRecords(Session("PeopleID"), Session("CompanyID"), Session("SeasonID"))
            If rsData.Rows.Count > 0 Then
                If IsDBNull(rsData.Rows(0).Item("DivisionID")) Then
                Else
                    getDivision = rsData.Rows(0).Item("DivisionID")
                End If
            End If
        Catch ex As Exception
            lblMSG.Text = "getDivision::" & ex.Message
        Finally
            oDivision = Nothing
        End Try
    End Function

    Private Sub comCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comCancel.Click
        Dim oPlayers As New Season.ClsPlayers
        Try
            oPlayers.DELPlayerByPeople(Session("PeopleID"), Session("CompanyID"), Session("SeasonID"))
        Catch ex As Exception
            lblMSG.Text = "comCancel_Click::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
        lblMSG.Text = grdMembers.SelectedItem.Cells(2).Text & " IS NOT REGISTERED"
        Call HideShowFields(0)
        Call ClearFields()
        Call LoadPlayers(Session("HouseID"))
        Call ResetFields()
    End Sub

    Private Sub comNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comNext.Click
        'If Session("HouseID") > 0 Then
        If CheckUpdates() = True Then
            Call UPDPeople()
            Call UPDPlayer()
        End If
        If CheckRegisteredPlayers() = False Then
            Call MsgBox("No player registered...Return to (Players information) page, then select a player and click on REGISTER")
            'Exit Sub
        End If
        Session("PeopleID") = 0
        If CheckCoaches() > 0 Then
            Server.Transfer("RegCoaches.aspx")
        ElseIf CheckSponsors() > 0 Then
            Server.Transfer("RegSponsors.aspx")
        Else
            Server.Transfer("RegPaypalDirect.aspx")
        End If
    End Sub

    Private Function CheckRegisteredPlayers() As Boolean
        CheckRegisteredPlayers = False
        Dim oPlayers As New Season.ClsPlayers
        Dim rsData As DataTable

        Try
            rsData = oPlayers.GetHouseholdRegisteredPlayers(Session("HouseID"), Session("SeasonID"))
            If rsData.Rows(0).Item("PeopleID") > 0 Then CheckRegisteredPlayers = True
        Catch ex As Exception
            lblMSG.Text = "CheckRegisteredPlayers::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Function

    Private Function CheckUpdates() As Boolean
        CheckUpdates = False
        If txtSchool.ToolTip() <> txtSchool.Text Or _
            cmbGradeOLD.SelectedValue <> cmbGrade.SelectedValue Or _
            txtDraftNotes.ToolTip <> txtDraftNotes.Text Then
            'lblMSG.Text = "Changes pending, 'Save' or 'Cancel'??"
            CheckUpdates = True
        End If
    End Function

    Private Function CheckCoaches() As Int32
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

    Private Function CheckSponsors() As Int32
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

    Private Sub ResetFields()
        txtSchool.ToolTip = txtSchool.Text
        cmbGradeOLD.SelectedIndex = cmbGrade.SelectedIndex
        txtDraftNotes.ToolTip = txtDraftNotes.Text
    End Sub

    Private Sub UPDPlayer()
        Dim oPlayers As New Season.ClsPlayers
        Try
            oPlayers.School = txtSchool.Text
            oPlayers.DraftNotes = txtDraftNotes.Text
            oPlayers.UserID = Session("UserID")
            oPlayers.Grade = cmbGrade.SelectedIndex
            oPlayers.DivisionId = lblDiv.ToolTip
            oPlayers.UpdatePlayerOR(Session("PeopleID"), Session("CompanyID"), Session("SeasonID"))
        Catch ex As Exception
            lblMSG.Text = "UPDPlayer::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Sub

    Private Sub UPDPeople()
        Dim oPeople As New Profile.ClsPeople
        Try
            oPeople.SchoolName = txtSchool.Text
            oPeople.Grade = cmbGrade.SelectedIndex
            oPeople.UpdatePeopleOR(Session("PeopleID"), Session("CompanyID"))
        Catch ex As Exception
            lblMSG.Text = "UPDPeople::" & ex.Message
        Finally
            oPeople = Nothing
        End Try
    End Sub

    Public Sub MsgBox(ByVal Message As String)

        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)

        System.Web.HttpContext.Current.Response.Write("alert(""" & Message & """)" & vbCrLf)

        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

    End Sub
End Class
