
Partial Class MasterPageNoMenu
    Inherits System.Web.UI.MasterPage
    Public sErrorMsg As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            lblTitle.Text = Session("Title")
        End If
    End Sub


End Class

