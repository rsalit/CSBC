Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class RegParents
    Inherits System.Web.UI.Page
   
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("HouseID") <= 0 Or Session("USERID") <= 0 Then
            Response.Redirect("RegLogin.aspx")
        End If
        Session("Title") = "Parents Information"
        If Page.IsPostBack = False Then
            Call LoadParents(Session("HouseID"))
            comNext.Focus()
        End If
    End Sub

    Private Sub LoadParents(ByVal HouseID As Integer)
        Dim oPeople As New Profile.ClsPeople
        Dim rsData As DataTable

        Try
            rsData = oPeople.LoadParents(HouseID, Session("CompanyID"))
            grdMembers.Controls.Clear()
            If rsData.Rows.Count > 0 Then
                With grdMembers
                    .DataSource = rsData
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            lblMSG.Text = "LoadParents::" & ex.Message
        Finally
            oPeople = Nothing
        End Try
    End Sub

    Private Sub LoadRow(ByVal PeopleID As Long)
        Dim oPeople As New Profile.ClsPeople
        Dim rsData As DataTable

        chkGuardianOLD.Checked = False
        chkGuardian.Checked = False

        Try
            rsData = oPeople.LoadPeople(PeopleID, Session("CompanyID"))
            If Not rsData Is Nothing Then
                If rsData.Rows.Count > 0 Then
                    txtFirstName.Text = rsData.Rows(0).Item("FirstName") & ""
                    txtFirstName.ToolTip = txtFirstName.Text
                    txtLastName.Text = rsData.Rows(0).Item("LastName") & ""
                    txtLastName.ToolTip = txtLastName.Text
                    chkGuardian.ToolTip = rsData.Rows(0).Item("Guardian")
                    If rsData.Rows(0).Item("Guardian") = PeopleID Then
                        chkGuardian.Checked = True
                        chkGuardian.Enabled = False
                        chkGuardianOLD.Checked = True
                    Else
                        chkGuardian.Checked = False
                        chkGuardian.Enabled = True
                        chkGuardianOLD.Checked = False
                    End If
                    If Not IsDBNull(rsData.Rows(0).Item("CellPhone")) Then
                        txtCell1.Text = Left(rsData.Rows(0).Item("CellPhone"), 3) & ""
                        txtCell1.ToolTip = txtCell1.Text
                        txtCell2.Text = Mid(rsData.Rows(0).Item("CellPhone"), 5, 3) & ""
                        txtCell2.ToolTip = txtCell2.Text
                        txtCell3.Text = Mid(rsData.Rows(0).Item("CellPhone"), 9, 4) & ""
                        txtCell3.ToolTip = txtCell3.Text
                    End If
                    If Not IsDBNull(rsData.Rows(0).Item("WorkPhone")) Then
                        txtWork1.Text = Left(rsData.Rows(0).Item("WorkPhone"), 3) & ""
                        txtWork1.ToolTip = txtWork1.Text
                        txtWork2.Text = Mid(rsData.Rows(0).Item("WorkPhone"), 5, 3) & ""
                        txtWork2.ToolTip = txtWork2.Text
                        txtWork3.Text = Mid(rsData.Rows(0).Item("WorkPhone"), 9, 4) & ""
                        txtWork3.ToolTip = txtWork3.Text
                    End If
                    If IsDBNull(rsData.Rows(0).Item("Gender")) Then
                    Else
                        If rsData.Rows(0).Item("Gender") = "M" Then
                            radGender.Items(0).Selected() = True
                            radGender.Items(1).Selected() = False
                            radGenderOLD.Items(0).Selected() = True
                            radGenderOLD.Items(1).Selected() = False
                        Else
                            radGender.Items(1).Selected() = True
                            radGender.Items(0).Selected() = False
                            radGenderOLD.Items(1).Selected() = True
                            radGenderOLD.Items(0).Selected() = False
                        End If
                    End If
                    chkVolunteer.Items(0).Selected() = rsData.Rows(0).Item("BoardOfficer")
                    chkVolunteer.Items(1).Selected() = rsData.Rows(0).Item("BoardMember")
                    chkVolunteer.Items(2).Selected() = rsData.Rows(0).Item("AD")
                    chkVolunteer.Items(3).Selected() = rsData.Rows(0).Item("Sponsor")
                    chkVolunteer.Items(4).Selected() = rsData.Rows(0).Item("SignUps")
                    chkVolunteer.Items(5).Selected() = rsData.Rows(0).Item("TryOuts")
                    chkVolunteer.Items(6).Selected() = rsData.Rows(0).Item("TeeShirts")
                    chkVolunteer.Items(7).Selected() = rsData.Rows(0).Item("Printing")
                    chkVolunteer.Items(8).Selected() = rsData.Rows(0).Item("Equipment")
                    chkVolunteer.Items(9).Selected() = rsData.Rows(0).Item("Electrician")
                    chkVolunteer.Items(10).Selected() = rsData.Rows(0).Item("Coach")
                    lblVolPositions.Text = chkVolunteer.Items(0).Selected() & _
                                chkVolunteer.Items(1).Selected() & _
                                chkVolunteer.Items(2).Selected() & _
                                chkVolunteer.Items(3).Selected() & _
                                chkVolunteer.Items(4).Selected() & _
                                chkVolunteer.Items(5).Selected() & _
                                chkVolunteer.Items(6).Selected() & _
                                chkVolunteer.Items(7).Selected() & _
                                chkVolunteer.Items(8).Selected() & _
                                chkVolunteer.Items(9).Selected() & _
                                chkVolunteer.Items(10).Selected()
                    lblVolPositions.ToolTip = lblVolPositions.Text
                End If
            End If
        Catch ex As Exception
            lblMSG.Text = "LoadRow::" & ex.Message
        Finally
            oPeople = Nothing
        End Try
    End Sub

    Private Sub HideShowFields(ByVal iOnOff As Boolean)
        Dim bOffOn As Boolean
        bOffOn = Not iOnOff
        txtLastName.Enabled = iOnOff
        txtFirstName.Enabled = iOnOff
        txtCell1.Enabled = iOnOff
        txtCell2.Enabled = iOnOff
        txtCell3.Enabled = iOnOff
        txtWork1.Enabled = iOnOff
        txtWork2.Enabled = iOnOff
        txtWork3.Enabled = iOnOff
        radGender.Enabled = iOnOff
        chkVolunteer.Enabled = iOnOff
        comUpdate.Enabled = iOnOff
        comCancel.Enabled = iOnOff
    End Sub

    Private Sub UpdRow(ByVal PeopleID As Long)
        Dim oPeople As New Profile.ClsPeople
        Try
            With oPeople
                .LastName = txtLastName.Text
                .FirstName = txtFirstName.Text
                If IsNumeric(txtWork1.Text) And IsNumeric(txtWork2.Text) And IsNumeric(txtWork3.Text) Then
                    .WorkPhone = txtWork1.Text & "-" & txtWork2.Text & "-" & txtWork3.Text
                Else
                    .WorkPhone = vbNull
                End If
                If IsNumeric(txtCell1.Text) And IsNumeric(txtCell2.Text) And IsNumeric(txtCell3.Text) Then
                    .CellPhone = txtCell1.Text & "-" & txtCell2.Text & "-" & txtCell3.Text
                Else
                    .CellPhone = vbNull
                End If

                If radGender.Items(0).Selected() = True Then .Gender = "M"
                If radGender.Items(1).Selected() = True Then .Gender = "F"
                If chkVolunteer.Items(0).Selected() Then
                    .BoardOfficer = 1
                Else
                    .BoardOfficer = 0
                End If
                If chkVolunteer.Items(1).Selected() Then
                    .BoardMember = 1
                Else
                    .BoardMember = 0
                End If
                If chkVolunteer.Items(2).Selected() Then
                    .AD = 1
                Else
                    .AD = 0
                End If
                If chkVolunteer.Items(3).Selected() Then
                    .Sponsor = 1
                Else
                    .Sponsor = 0
                End If
                If chkVolunteer.Items(4).Selected() Then
                    .SignUps = 1
                Else
                    .SignUps = 0
                End If
                If chkVolunteer.Items(5).Selected() Then
                    .TryOuts = 1
                Else
                    .TryOuts = 0
                End If
                If chkVolunteer.Items(6).Selected() Then
                    .TeeShirts = 1
                Else
                    .TeeShirts = 0
                End If
                If chkVolunteer.Items(7).Selected() Then
                    .Printing = 1
                Else
                    .Printing = 0
                End If
                If chkVolunteer.Items(8).Selected() Then
                    .Equipment = 1
                Else
                    .Equipment = 0
                End If
                If chkVolunteer.Items(9).Selected() Then
                    .Electrician = 1
                Else
                    .Electrician = 0
                End If
                If chkVolunteer.Items(10).Selected() Then
                    .Coach = 1
                Else
                    .Coach = 0
                End If
                .Parent = 1
                .Grade = 13
                .HouseId = Session("HouseID")
                oPeople.UpdRow(PeopleID, Session("CompanyID"), Session("TimeZone"))
            End With
        Catch ex As Exception
            Session("ErrorMSG") = "UpdRow::" & ex.Message
        Finally
            oPeople = Nothing
        End Try
    End Sub

    Private Function errorRTN() As Boolean
        errorRTN = False
        If txtLastName.Text = "" Then
            lblMSG.Text = "Last Name missing "
            errorRTN = True
        ElseIf txtFirstName.Text = "" Then
            lblMSG.Text = "First Name missing "
            errorRTN = True
        ElseIf (txtWork1.Text > "" Or txtWork2.Text > "" Or txtWork3.Text > "") And _
              (Not IsNumeric(txtWork1.Text) Or Len(txtWork1.Text) < 3 Or _
               Not IsNumeric(txtWork2.Text) Or Len(txtWork2.Text) < 3 Or _
               Not IsNumeric(txtWork3.Text) Or Len(txtWork3.Text) < 4) Then
            lblMSG.Text = "Invalid Work phone"
            errorRTN = True
        ElseIf (txtCell1.Text > "" Or txtCell2.Text > "" Or txtCell3.Text > "") And _
              (Not IsNumeric(txtCell1.Text) Or Len(txtCell1.Text) < 3 Or _
               Not IsNumeric(txtCell2.Text) Or Len(txtCell2.Text) < 3 Or _
               Not IsNumeric(txtCell3.Text) Or Len(txtCell3.Text) < 4) Then
            lblMSG.Text = "Invalid Cell phone"
            errorRTN = True
        End If
    End Function

    Private Sub comPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comPrev.Click
        Session("PeopleID") = 0
        Server.Transfer("RegHouse.aspx")
    End Sub

    Private Sub comUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comUpdate.Click
        Call SaveData()
    End Sub

    Private Sub SaveData()
        If Session("PeopleID") > 0 Then
            If errorRTN() = False Then
                Call UpdRow(Session("PeopleID"))
                Call UpdateHouseGuardian()
                If chkVolunteer.Items(10).Selected() = False Then Call RemoveCoach()
                If chkVolunteer.Items(3).Selected() = False Then Call RemoveSponsor()
                lblMSG.Text = grdMembers.SelectedItem.Cells(2).Text & " SUCCESSFULLY UPDATED!"
                Call HideShowFields(0)
                Call ClearFields()
            End If
        End If
    End Sub

    Private Sub UpdateHouseGuardian()
        If chkGuardian.Checked = chkGuardianOLD.Checked Then Exit Sub
        Dim oHouseholds As New Profile.ClsHouseholds
        Try
            With oHouseholds
                If chkGuardian.Checked = False Then .Guardian = 0
                If chkGuardian.Checked = True Then .Guardian = Session("PeopleID")
                .UpdGuardian(Session("HouseID"), Session("CompanyID"))
            End With
        Catch ex As Exception
            Session("ErrorMSG") = "UpdateHouseGuardian::" & ex.Message
        Finally
            oHouseholds = Nothing
        End Try
    End Sub

    Private Sub RemoveCoach()
        Dim oCoaches As New Season.clsCoaches
        Try
            oCoaches.UnassignCoach(Session("PeopleID"), Session("CompanyID"), Session("SeasonID"))
        Catch ex As Exception
            Session("ErrorMSG") = "RemoveCoach::" & ex.Message
        Finally
            oCoaches = Nothing
        End Try
    End Sub

    Private Sub RemoveSponsor()
        Dim oSponsors As New Season.clsSponsors
        Try
            oSponsors.UnassignSponsor(Session("HouseID"), Session("CompanyID"), Session("SeasonID"))
        Catch ex As Exception
            Session("ErrorMSG") = "RemoveSponsor::" & ex.Message
        Finally
            oSponsors = Nothing
        End Try

    End Sub

    Private Sub ClearFields()
        txtLastName.Text = ""
        txtFirstName.Text = ""
        txtCell1.Text = ""
        txtCell2.Text = ""
        txtCell3.Text = ""
        txtWork1.Text = ""
        txtWork2.Text = ""
        txtWork3.Text = ""
        txtLastName.ToolTip = ""
        txtFirstName.ToolTip = ""
        txtCell1.ToolTip = ""
        txtCell2.ToolTip = ""
        txtCell3.ToolTip = ""
        txtWork1.ToolTip = ""
        txtWork2.ToolTip = ""
        txtWork3.ToolTip = ""
        chkGuardian.Checked = False
        chkGuardianOLD.Checked = False
        radGender.Items(0).Selected() = False
        radGender.Items(1).Selected() = False
        radGenderOLD.Items(0).Selected() = False
        radGenderOLD.Items(1).Selected() = False
        chkVolunteer.Items(0).Selected() = False
        chkVolunteer.Items(1).Selected() = False
        chkVolunteer.Items(2).Selected() = False
        chkVolunteer.Items(3).Selected() = False
        chkVolunteer.Items(4).Selected() = False
        chkVolunteer.Items(5).Selected() = False
        chkVolunteer.Items(6).Selected() = False
        chkVolunteer.Items(7).Selected() = False
        chkVolunteer.Items(8).Selected() = False
        chkVolunteer.Items(9).Selected() = False
        chkVolunteer.Items(10).Selected() = False
    End Sub

    Private Sub grdMembers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMembers.SelectedIndexChanged
        Call SaveData()
        Session("PeopleID") = grdMembers.SelectedItem.Cells(0).Text
        If Session("PeopleID") > "0" Then
            Call LoadRow(Session("PeopleID"))
            Call SetFocus(txtWork1)
            Call HideShowFields(1)
            lblMSG.Text = ""
        End If
    End Sub

    Private Sub comCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comCancel.Click
        Call HideShowFields(0)
        If Session("PeopleID") > "0" Then
            Call LoadRow(Session("PeopleID"))
            Call SetFocus(txtWork1)
            Call HideShowFields(1)
            lblMSG.Text = "Parent data refreshed!"
        Else
            Call ClearFields()
        End If
    End Sub

    Private Sub comNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comNext.Click
        Dim ErrMsg As Boolean = errorRTN()
        If Session("PeopleID") > 0 And ErrMsg = False Then
            If CheckUpdates() = True Then
                Call SaveData()
            End If
        End If
        If ErrMsg = False Or Session("PeopleID") = 0 Then
            Session("PeopleID") = 0
            Server.Transfer("RegPlayers.aspx")
        End If
    End Sub

    Private Function CheckUpdates() As Boolean
        CheckUpdates = False
        lblVolPositions.Text = chkVolunteer.Items(0).Selected() & _
                        chkVolunteer.Items(1).Selected() & _
                        chkVolunteer.Items(2).Selected() & _
                        chkVolunteer.Items(3).Selected() & _
                        chkVolunteer.Items(4).Selected() & _
                        chkVolunteer.Items(5).Selected() & _
                        chkVolunteer.Items(6).Selected() & _
                        chkVolunteer.Items(7).Selected() & _
                        chkVolunteer.Items(8).Selected() & _
                        chkVolunteer.Items(9).Selected() & _
                        chkVolunteer.Items(10).Selected()
        If txtLastName.ToolTip() <> txtLastName.Text Or _
            txtFirstName.ToolTip <> txtFirstName.Text Or _
            txtCell1.ToolTip <> txtCell1.Text Or _
            txtCell2.ToolTip <> txtCell2.Text Or _
            txtCell3.ToolTip <> txtCell3.Text Or _
            txtWork1.ToolTip <> txtWork1.Text Or _
            txtWork2.ToolTip <> txtWork2.Text Or _
            txtWork3.ToolTip <> txtWork3.Text Or _
            radGender.Items(0).Selected() <> radGenderOLD.Items(0).Selected() Or _
            radGender.Items(1).Selected() <> radGenderOLD.Items(1).Selected() Or _
            chkGuardian.Checked <> chkGuardianOLD.Checked Or _
            lblVolPositions.Text <> lblVolPositions.ToolTip Then
            CheckUpdates = True
        End If
    End Function

End Class
