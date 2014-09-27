Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class ReportSchedules
    Inherits System.Web.UI.Page
    Private rsData As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("UserID") Is Nothing Then Response.Redirect("Login.aspx")

        If Page.IsPostBack = False Then
            Call GetDivisions()
        End If
        Session("Title") = "Schedules Reports"
    End Sub

    Private Sub GetDivisions()
        Dim oDivisions As New Season.ClsDivisions
        Dim rsData As DataTable
        Dim dRow As DataRow
        Try
            rsData = oDivisions.LoadDivision(0, Session("CompanyID"), Session("SeasonID"), "DivisionID, Div_Desc as Division")
            grdDivisions.ResetBands()
            dRow = rsData.NewRow
            dRow("DivisionID") = 0
            dRow("Division") = "Master"
            rsData.Rows.InsertAt(dRow, 0)
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

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If grdDivisions.DisplayLayout.SelectedRows.Count = 0 Then Exit Sub
        Response.Redirect("Schedules/" & grdDivisions.DisplayLayout.ActiveRow.Index & ".pdf")
    End Sub

  
End Class

