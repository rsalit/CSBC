Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class RegCoaches
    Inherits System.Web.UI.Page
 
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Session("Title") = "Coaches Information"
        If Page.IsPostBack = False Then
            Call LoadPlayers(Session("HouseID"))
            Call LoadParents(Session("HouseID"))
            setfocus(comNext)
        End If
    End Sub

    Private Sub LoadPlayers(ByVal HouseID As Integer)
        Dim oPlayers As New Season.ClsPlayers
        Dim rsData As DataTable
        Try
            rsData = oPlayers.GetHouseholdRegisteredPlayers(HouseID, Session("SeasonID"))
            grdPlayers.Controls.Clear()
            If rsData.Rows.Count > 0 Then
                With grdPlayers
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

    Private Sub LoadParents(ByVal HouseID As Integer)
        Dim oCoaches As New Season.clsCoaches
        Dim rsData As DataTable
        Try
            rsData = oCoaches.GetHouseholdRegisteredCoaches(HouseID, Session("SeasonID"), Session("CompanyID"))
            cmbParents.Controls.Clear()
            cmbParentsOLD.Controls.Clear()
            cmbParents.Items.Add(New ListItem("Select Parent/Coach", 0))
            cmbParentsOLD.Items.Add(New ListItem("Select Parent/Coach", 0))

            For I As Int16 = 0 To rsData.Rows.Count - 1
                cmbParents.Items.Add(New ListItem(Trim(rsData.Rows(I).Item("Name")), rsData.Rows(I).Item("PeopleID")))
                cmbParentsOLD.Items.Add(New ListItem(Trim(rsData.Rows(I).Item("Name")), rsData.Rows(I).Item("PeopleID")))
            Next
        Catch ex As Exception
            lblMSG.Text = "LoadParents::" & ex.Message
        Finally
            oCoaches = Nothing
        End Try

    End Sub

    Private Sub LoadRow(ByVal PeopleID As Long)
        Dim oCoaches As New Season.clsCoaches
        Dim rsData As DataTable
        Try
            rsData = oCoaches.GetPlayerAndCoachByPeopleID(Session("SeasonID"), PeopleID, Session("CompanyID"))
            If Not rsData Is Nothing Then
                If rsData.Rows.Count > 0 Then
                    If IsNumeric(rsData.Rows(0).Item("CoachID")) Then
                        If rsData.Rows(0).Item("CoachID") > 0 Then
                            comUpdate.Enabled = True
                            comCancel.Enabled = True
                            cmbParents.Enabled = True
                            cmbShirts.Enabled = True
                            txtPhone1.Enabled = True
                            txtPhone2.Enabled = True
                            txtPhone3.Enabled = True
                            cmbParents.SelectedValue = rsData.Rows(0).Item("PeopleID")
                            cmbParentsOLD.SelectedValue = rsData.Rows(0).Item("PeopleID")
                        End If
                    End If
                    If Not IsDBNull(rsData.Rows(0).Item("ShirtSize")) Then
                        cmbShirts.SelectedValue = rsData.Rows(0).Item("ShirtSize") & ""
                        cmbShirtsOLD.SelectedValue = rsData.Rows(0).Item("ShirtSize") & ""
                    End If
                    If Not IsDBNull(rsData.Rows(0).Item("Phone")) Then
                        txtPhone1.Text = Left(rsData.Rows(0).Item("Phone"), 3) & ""
                        txtPhone1.ToolTip = txtPhone1.Text
                        txtPhone2.Text = Mid(rsData.Rows(0).Item("Phone"), 5, 3) & ""
                        txtPhone2.ToolTip = txtPhone2.Text
                        txtPhone3.Text = Mid(rsData.Rows(0).Item("Phone"), 9, 4) & ""
                        txtPhone3.ToolTip = txtPhone3.Text
                    End If
                End If

                comUpdate.Enabled = True
                comCancel.Enabled = True
                btnCancel.Enabled = True
                cmbParents.Enabled = True
                cmbShirts.Enabled = True
                txtPhone1.Enabled = True
                txtPhone2.Enabled = True
                txtPhone3.Enabled = True
            End If
        Catch ex As Exception
            lblMSG.Text = "LoadRow::" & ex.Message
        Finally
            oCoaches = Nothing
        End Try
    End Sub

    Private Sub UpdRow(ByVal PeopleID As Long)
        If Session("PlayerID") > 0 Then
            Call UPDPlayer()
        End If
    End Sub

    Private Sub comPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comPrev.Click
        If errorRTN() = False Then
            If CheckUpdates() = True Then
                Call SaveData()
            End If
        Else
            If cmbParents.SelectedIndex > 0 Then Exit Sub
        End If

        Server.Transfer("RegPlayers.aspx")
    End Sub

    Private Sub comUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comUpdate.Click
        Call SaveData()
    End Sub

    Private Sub SaveData()
        If errorRTN() = False Then
            Call UPDPlayer()
            lblMSG.Text = cmbParents.SelectedItem.Text & " SIGNED UP FOR COACHING " & lblPlayer.Text 'cmbPlayers.SelectedItem.Text
            lblPlayer.Text = ""
            'cmbPlayers.SelectedIndex = 0
            Call ClearFields()
            'Call ResetFields()
            Call LoadPlayers(Session("HouseID"))
        End If
    End Sub

    Private Function errorRTN() As Boolean
        errorRTN = False
        If cmbParents.SelectedIndex = 0 Then
            lblMSG.Text = "Select a Parent/Coach "
            errorRTN = True
        ElseIf cmbShirts.SelectedIndex = 0 Then
            lblMSG.Text = "Select a Shirt Size "
            errorRTN = True
        ElseIf (txtPhone1.Text > "" Or txtPhone2.Text > "" Or txtPhone3.Text > "") And _
            (Not IsNumeric(txtPhone1.Text) Or Len(txtPhone1.Text) < 3 Or _
            Not IsNumeric(txtPhone2.Text) Or Len(txtPhone2.Text) < 3 Or _
            Not IsNumeric(txtPhone3.Text) Or Len(txtPhone3.Text) < 4) Then
            lblMSG.Text = "phone missing or invalid"
            errorRTN = True
        End If
        'If errorRTN = True Then Response.End()
    End Function

    Private Sub ClearFields()
        cmbParents.SelectedIndex = 0
        cmbParentsOLD.SelectedIndex = 0
        txtPhone1.Text = ""
        txtPhone1.ToolTip = txtPhone1.Text
        txtPhone2.Text = ""
        txtPhone2.ToolTip = txtPhone2.Text
        txtPhone3.Text = ""
        txtPhone3.ToolTip = txtPhone3.Text
        'cmbShirts.SelectedItem.Value = 0
        cmbShirts.SelectedIndex = 0
        cmbShirtsOLD.SelectedIndex = 0
        comCancel.Enabled = False
        comUpdate.Enabled = False
    End Sub

    Private Sub comNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comNext.Click
        If errorRTN() = False Then
            If CheckUpdates() = True Then
                'Exit Sub
                Call SaveData()
            Else
                If CheckCoaches() = False Then Call MsgBox("No Coach registered...Return to (Coaches Sign Up) page, there select a player and a coach then click on REGISTER COACH")

                If CheckSponsors() > 0 Then
                    Server.Transfer("RegSponsors.aspx")
                Else
                    Server.Transfer("RegPaypalDirect.aspx")
                End If
            End If
        Else
            If CheckCoaches() = False Then Call MsgBox("No Coach registered...Return to (Coaches Sign Up) page, there select a player and a coach then click on REGISTER COACH")

            If lblPlayer.Text > "" And (cmbParents.SelectedItem.Text > "0" Or cmbShirts.SelectedValue > "0" Or _
                txtPhone1.Text > "" Or txtPhone2.Text > "" Or txtPhone3.Text > "") Then Exit Sub
            If CheckSponsors() = True Then
                Server.Transfer("RegSponsors.aspx")
            Else
                Server.Transfer("RegPaypalDirect.aspx")
            End If
        End If
    End Sub

    Private Function CheckSponsors() As Boolean
        CheckSponsors = False
        Dim oPlayers As New Season.clsSponsors
        Dim rsData As DataTable

        Try
            rsData = oPlayers.GetHouseholdRegisteredSponsor(Session("HouseID"), Session("SeasonID"), Session("CompanyID"))
            If rsData.Rows(0).Item("PeopleID") > 0 Then CheckSponsors = True

        Catch ex As Exception
            lblMSG.Text = "CheckSponsors::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try
    End Function

    Private Sub UPDPlayer()
        Dim oCoaches As New Season.clsCoaches
        Try
            With oCoaches
                If IsNumeric(txtPhone1.Text) And IsNumeric(txtPhone2.Text) And IsNumeric(txtPhone3.Text) Then
                    .CoachPhone = txtPhone1.Text & "-" & txtPhone2.Text & "-" & txtPhone3.Text
                Else
                    .CoachPhone = ""
                End If
                .ShirtSize = cmbShirts.SelectedItem.Text
                .CreatedUser = Session("UserID")
                .CoachID = cmbParents.SelectedItem.Value
                .AssignCoach(Session("PeopleID"), Session("CompanyID"), Session("SeasonID"))
            End With

        Catch ex As Exception
            lblMSG.Text = "UPDPlayer::" & ex.Message
        Finally
            oCoaches = Nothing
        End Try

    End Sub

    Private Sub comCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comCancel.Click
        Dim oCoaches As New Season.clsCoaches
        Try
            With oCoaches
                .UnassignCoach_OR(Session("PeopleID"), Session("CompanyID"), Session("SeasonID"))
            End With

        Catch ex As Exception
            lblMSG.Text = "UPDPlayer::" & ex.Message
        Finally
            oCoaches = Nothing
        End Try
        lblMSG.Text = cmbParents.SelectedItem.Text & " WAS REMOVED FROM COACHING " & lblPlayer.Text 'cmbPlayers.SelectedItem.Text
        lblPlayer.Text = ""
        'cmbPlayers.SelectedIndex = 0
        Call ClearFields()
        Call LoadPlayers(Session("HouseID"))
    End Sub

    Private Function CheckUpdates() As Boolean
        CheckUpdates = False
        If cmbShirts.SelectedValue <> cmbShirtsOLD.SelectedValue Or _
            cmbParents.SelectedValue <> cmbParentsOLD.SelectedValue Or _
            txtPhone1.ToolTip <> txtPhone1.Text Or _
            txtPhone2.ToolTip <> txtPhone2.Text Or _
            txtPhone3.ToolTip <> txtPhone3.Text Then
            'lblMSG.Text = "Changes pending, 'Save' or 'Cancel'??"
            CheckUpdates = True
        End If
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        lblMSG.Text = "Coach changes NOT done"
        Call ResetFields()
    End Sub

    Private Sub ResetFields()
        cmbShirts.SelectedValue = cmbShirtsOLD.SelectedValue
        cmbParents.SelectedValue = cmbParentsOLD.SelectedValue
        txtPhone1.Text = txtPhone1.ToolTip & ""
        txtPhone2.Text = txtPhone2.ToolTip & ""
        txtPhone3.Text = txtPhone3.ToolTip & ""
    End Sub

    Private Sub grdPlayers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPlayers.SelectedIndexChanged
        Session("PeopleID") = grdPlayers.SelectedItem.Cells(0).Text
        lblPlayer.Text = grdPlayers.SelectedItem.Cells(2).Text
        If Session("PeopleID") > "0" Then
            Call ClearFields()
            Call LoadRow(Session("PeopleID"))
            lblMSG.Text = ""
        End If
    End Sub

    Public Sub MsgBox(ByVal Message As String)

        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)

        System.Web.HttpContext.Current.Response.Write("alert(""" & Message & """)" & vbCrLf)

        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

    End Sub

    Private Function CheckCoaches() As Boolean
        CheckCoaches = False
        Dim oPlayers As New Season.clsCoaches
        Dim rsData As DataTable

        Try
            rsData = oPlayers.GetHouseholdRegisteredCoaches(Session("HouseID"), Session("SeasonID"), Session("CompanyID"))
            If rsData.Rows(0).Item("PeopleID") > 0 Then CheckCoaches = True

        Catch ex As Exception
            lblMSG.Text = "CheckCoaches::" & ex.Message
        Finally
            oPlayers = Nothing
        End Try

    End Function
End Class
