Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class ReportVolunteers
    Inherits System.Web.UI.Page
    Private rsData As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("UserID") Is Nothing Then Response.Redirect("Login.aspx")

        If Page.IsPostBack = False Then
            Call LoadVolunteers()
        End If
        Session("Title") = "Volunteer Reports"
    End Sub

    Private Sub LoadVolunteers()
        Dim rsData As New DataTable
        Dim dRow As DataRow
        Dim oCol As DataColumn

        oCol = New DataColumn
        oCol.ColumnName = "VolunteerID"
        rsData.Columns.Add(oCol)

        oCol = New DataColumn
        oCol.ColumnName = "Volunteer"
        rsData.Columns.Add(oCol)

        Try
            dRow = rsData.NewRow
            dRow("VolunteerID") = 0
            dRow("Volunteer") = "Board Officer"
            rsData.Rows.InsertAt(dRow, 0)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 1
            dRow("Volunteer") = "Board Member"
            rsData.Rows.InsertAt(dRow, 1)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 2
            dRow("Volunteer") = "Athletic Director"
            rsData.Rows.InsertAt(dRow, 2)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 3
            dRow("Volunteer") = "Coach"
            rsData.Rows.InsertAt(dRow, 3)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 4
            dRow("Volunteer") = "Asst Coach"
            rsData.Rows.InsertAt(dRow, 4)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 5
            dRow("Volunteer") = "Sponsor"
            rsData.Rows.InsertAt(dRow, 5)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 6
            dRow("Volunteer") = "Sign Ups"
            rsData.Rows.InsertAt(dRow, 6)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 7
            dRow("Volunteer") = "Try Outs"
            rsData.Rows.InsertAt(dRow, 7)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 8
            dRow("Volunteer") = "Tee Shirts"
            rsData.Rows.InsertAt(dRow, 8)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 9
            dRow("Volunteer") = "Printing Co."
            rsData.Rows.InsertAt(dRow, 9)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 10
            dRow("Volunteer") = "Equipment"
            rsData.Rows.InsertAt(dRow, 10)

            dRow = rsData.NewRow
            dRow("VolunteerID") = 11
            dRow("Volunteer") = "Electrician"
            rsData.Rows.InsertAt(dRow, 11)

            grdVolunteers.ResetBands()
            With grdVolunteers
                .DataSource = rsData
                .DataBind()
            End With
            With grdVolunteers.DisplayLayout.Bands(0).Columns
                .FromKey("VolunteerID").Hidden = True
                .FromKey("Volunteer").Width = 100
            End With
        Catch ex As Exception
            lblError.Text = "LoadVolunteers::" & ex.Message
        End Try
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        If grdVolunteers.DisplayLayout.SelectedRows.Count = 0 Then Exit Sub
        Session("VolunteerID") = grdVolunteers.DisplayLayout.ActiveRow.Cells.FromKey("VolunteerID").Value()
        Session("VolunteerDesc") = grdVolunteers.DisplayLayout.ActiveRow.Cells.FromKey("Volunteer").Value()
        Session("Current") = 0
        Response.Redirect("report.aspx?Report=VolunteersList.rpt")
    End Sub

    Protected Sub btnCurrent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCurrent.Click
        If grdVolunteers.DisplayLayout.SelectedRows.Count = 0 Then Exit Sub
        Session("VolunteerID") = grdVolunteers.DisplayLayout.ActiveRow.Cells.FromKey("VolunteerID").Value()
        Session("VolunteerDesc") = grdVolunteers.DisplayLayout.ActiveRow.Cells.FromKey("Volunteer").Value()
        Session("Current") = 1
        Response.Redirect("report.aspx?Report=VolunteersList.rpt")
    End Sub
End Class

