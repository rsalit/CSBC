Imports System.Data
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports CSBC.Repositories
Imports CSBC.Components



Partial Class GamesNotReady
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("Module") = "" Then
            Response.Redirect("default.aspx")
        End If
        Session("Module") = "Games"
        If Page.IsPostBack = False Then
            'Session("ReportName") = lnkSwitch.Text '"Schedules"
          
        End If
    End Sub

    
End Class
