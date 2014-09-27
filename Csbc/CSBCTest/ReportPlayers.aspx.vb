Imports System.Data
Imports System.Data.SqlClient
Imports CSBC.Components

Public Class ReportPlayers
    Inherits System.Web.UI.Page
    Private rsData As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("UserID") Is Nothing Then Response.Redirect("Login.aspx")


        Session("Title") = "Players Reports"
    End Sub

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If chkFilters.Items(0).Selected = True Then
            Session("Paid") = 1
        Else
            Session("Paid") = 0
        End If
        If chkFilters.Items(1).Selected = True Then
            Session("Active") = 1
        Else
            Session("Active") = 0
        End If
        If chkFilters.Items(2).Selected = True Then
            Session("Insured") = 1
        Else
            Session("Insured") = 0
        End If
        If chkFilters.Items(3).Selected = True Then
            Session("Residents") = 1
        Else
            Session("Residents") = 0
        End If
        Response.Redirect("report.aspx?Report=PlayersList.rpt")
    End Sub

End Class

