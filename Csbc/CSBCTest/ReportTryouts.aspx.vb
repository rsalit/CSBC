Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class ReportTryouts
    Inherits System.Web.UI.Page
    Private rsData As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("UserID") Is Nothing Then Response.Redirect("Login.aspx")

        If Page.IsPostBack = False Then
            Call GetDivisions()
        End If
        Session("Title") = "Tryouts Reports"
    End Sub

    Private Sub GetDivisions()
        Dim oDivisions As New Season.ClsDivisions
        Dim rsData As DataTable
        Try
            rsData = oDivisions.LoadDivision(0, Session("CompanyID"), Session("SeasonID"), "DivisionID, Div_Desc as Division")
            grdDivisions.ResetBands()
            With grdDivisions
                .DataSource = rsData
                .DataBind()
            End With
            With grdDivisions.DisplayLayout.Bands(0).Columns
                .FromKey("DivisionID").Hidden = True
                .FromKey("Division").Width = 100
            End With
        Catch ex As Exception
            lblError.Text = "GetDivisions::" & ex.Message
        Finally
            oDivisions = Nothing
        End Try
    End Sub

    Protected Sub btnByDivision_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnByDivision.Click
        'Dim idx As Long
        'Dim sDivisions As String = ""

        'For idx = 0 To grdDivisions.DisplayLayout.SelectedRows.Count - 1
        '    sDivisions += grdDivisions.DisplayLayout.SelectedRows(idx).Cells.FromKey("DivisionID").Value
        'Next idx
        If grdDivisions.DisplayLayout.SelectedRows.Count = 0 Then Exit Sub
        Session("DivisionID") = grdDivisions.DisplayLayout.ActiveRow.Cells.FromKey("DivisionID").Value()
        Session("DivDesc") = grdDivisions.DisplayLayout.ActiveRow.Cells.FromKey("Division").Value()
        '    If Session("ReportName") = "DraftList" Then Call Tryouts("Tryouts.rpt")
        Response.Redirect("report.aspx?Report=Tryouts.rpt")
    End Sub

    Protected Sub btnAllPlayers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllPlayers.Click
        '    If Session("ReportName") = "DraftListFull" Then Call DraftListFull()
        Session("DivisionID") = 0
        Response.Redirect("report.aspx?Report=TryoutsAll.rpt")
    End Sub
End Class

