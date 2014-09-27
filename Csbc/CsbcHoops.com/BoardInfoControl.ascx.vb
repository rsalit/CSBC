Imports System.Data
Partial Class BoardInfoControl
    Inherits System.Web.UI.UserControl
    Public Sub SetData(ByVal dr As DataRowView, ByVal IsAlternating As Boolean)
        Me.lblName.Text = dr("Name")
        Me.lblTitle.Text = dr("Title")
        Me.lnkEmail.Text = dr("Email")
        Me.lblPhone.Text = dr("Phone")
        'If IsAlternating Then
        '    pnlMain.BackColor = Color.LightGray
        'End If
    End Sub

    Private Sub lnkEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEmail.Click
        Dim sEmail As String
        sEmail = "mailto:" & Me.lnkEmail.Text
        Response.Redirect(sEmail)
    End Sub
End Class
