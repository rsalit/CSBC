Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class ReportRefunds
    Inherits System.Web.UI.Page
    Private rsData As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("UserID") Is Nothing Then Response.Redirect("Login.aspx")

        If Page.IsPostBack = False Then
            Call GetBatches()
        End If
        Session("Title") = "Refunds Report"
    End Sub

    Private Sub GetBatches()
        Dim oRefunds As New Season.ClsPlayers
        Dim rsData As DataTable

        Try
            rsData = oRefunds.GetBatches(0, Session("CompanyID"), "RefBatchId, Season, CreatedDate")
            grdBatches.Clear()
            grdBatches.Columns.Clear()
            If rsData.Rows.Count > 0 Then
                With grdBatches
                    .DataSource = rsData
                    .DataBind()
                    With grdBatches.DisplayLayout.Bands(0).Columns
                        '.FromKey("SeasonID").Hidden = True
                        '.FromKey("CreatedUser").Hidden = True
                        .FromKey("RefBatchId").CellStyle.HorizontalAlign = HorizontalAlign.Center
                        .FromKey("RefBatchId").Header.Caption = "Batch"
                        .FromKey("RefBatchId").Width = 70
                        .FromKey("CreatedDate").CellStyle.HorizontalAlign = HorizontalAlign.Center
                        .FromKey("CreatedDate").Header.Caption = "Date / Time"
                    End With
                End With
            End If
        Catch ex As Exception
            lblError.Text = "GetBatches::" & ex.Message
        Finally
            oRefunds = Nothing
        End Try
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If grdBatches.DisplayLayout.SelectedRows.Count = 0 Then Exit Sub
        Session("BatchID") = grdBatches.DisplayLayout.ActiveRow.Cells.FromKey("RefBatchId").Value()
        Session("RefundSeason") = grdBatches.DisplayLayout.ActiveRow.Cells.FromKey("Season").Value()
        Response.Redirect("report.aspx?Report=RefundsList.rpt&SeasonDesc=")
    End Sub

End Class

