Imports System.Data
Partial Class LogOut
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session.RemoveAll()
        Response.Write("<script language='javascript'> { window.opener = top; window.close();}</script>")

    End Sub
End Class
