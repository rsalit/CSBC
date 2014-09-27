Imports System.Data
Imports CSBC.Components
Public Class ContactUs
    Inherits System.Web.UI.Page
    Dim SQL As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Call LoadBoard()
    End Sub

    Private Sub LoadBoard()
        Dim oDirectors As New Volunteers.ClsDirectors
        Dim rsData As DataTable
        Try
            rsData = oDirectors.GetBoard(Session("CompanyID"))
            With repBoard
                .DataSource = rsData
                .DataBind()
            End With
        Catch ex As Exception
            lblError.Text = "LoadBoard:" & ex.Message
        Finally
            oDirectors = Nothing
        End Try


    End Sub

    Private Sub repBoard_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repBoard.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item) Then
            CType(e.Item.FindControl("BoardInfo"), BoardInfoControl).SetData(CType(e.Item.DataItem, DataRowView), False)
        End If
        If (e.Item.ItemType = ListItemType.AlternatingItem) Then
            CType(e.Item.FindControl("BoardInfo"), BoardInfoControl).SetData(CType(e.Item.DataItem, DataRowView), True)
        End If
    End Sub

End Class

