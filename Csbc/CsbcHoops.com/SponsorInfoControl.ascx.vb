Imports System.Data
Public Class SponsorInfoControl
    Inherits System.Web.UI.UserControl

    Public Sub SetData(ByVal dr As DataRowView, ByVal IsAlternating As Boolean)
        Me.lblName.Text = dr("SpoName")
        Me.lblAddress.Text = dr("Address")
        Me.lblWebsite.Text = dr("Website")
        Me.lblPhone.Text = dr("Phone")
        'If IsAlternating Then
        '    pnlMain.BackColor = Color.LightGray
        'End If
    End Sub

    Private Sub lblWebsite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWebsite.Click
        Response.Redirect("http://" + Me.lblWebsite.Text)
    End Sub

End Class
