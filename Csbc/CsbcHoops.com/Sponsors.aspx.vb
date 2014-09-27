Imports System.Data
Imports CSBC.Components
Partial Class Sponsors
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Call LoadSponsors()
        'Call LoadXML()
    End Sub

    Private Sub LoadSponsors()
        Dim oSponsors As New Season.clsSponsors
        Dim rsData As DataTable
        Try
            rsData = oSponsors.GetSponsorsInfo(Session("CompanyID"))
            With repSponsors
                .DataSource = rsData
                .DataBind()
            End With
        Catch ex As Exception
            lblError.Text = "LoadSponsors:" & ex.Message
        Finally
            oSponsors = Nothing
        End Try

    End Sub

    Private Sub LoadXML()
        '            Dim ds As New DataSet("Data")
        '            ds.ReadXml(MapPath("XML/Sponsors.xml"))

        '            rptSponsors.DataSource = ds.Tables(0)
        '            rptSponsors.DataBind()
    End Sub

    Private Sub repSponsors_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repSponsors.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item) Then
            CType(e.Item.FindControl("SponsorInfo"), SponsorInfoControl).SetData(CType(e.Item.DataItem, DataRowView), False)
        End If
        If (e.Item.ItemType = ListItemType.AlternatingItem) Then
            CType(e.Item.FindControl("SponsorInfo"), SponsorInfoControl).SetData(CType(e.Item.DataItem, DataRowView), True)
        End If
    End Sub

End Class
