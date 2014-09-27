Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared


Partial Class report
    Inherits System.Web.UI.Page
    Public sqlString As String = ""
    Public dataSet As DataSet
    Protected WithEvents SqlConnection2 As System.Data.SqlClient.SqlConnection
    Public selectCMD As SqlCommand
    'TODO:: Review the following fields
    Public iDiv As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Report") = "Reports/DivSchedule.rpt" Then DivSchedule()

        'If Session("Report") = "Reports/DivStanding.rpt" Then DivStanding()

        'If Session("Report") = "Reports/PlayersStats.rpt" Then Stats()
        'Test()
    End Sub

    Private Sub Test()

        'Dim oData As New CSBC_DLL.CSBC.Components.Season.ClsSchedules
        'Dim crystalReport As New ReportDocument()
        'crystalReport.Load(Server.MapPath("Reports\DivSchedule.rpt"))
        ''dsCustomers = DirectCast(ViewState("Customers_Data"), Customers)
        'CrystalReportViewer1.ReportSource = crystalReport
        'crystalReport.SetDataSource(oData.GetGames(Session("CompanyID"), Session("SeasonID"), Session("ScheduleNo"), Session("TeamNbr"), Session("ScheduleDesc"), Session("TeamName")))
    End Sub

    Private Sub DivSchedule()
        Dim strExportFile As String
        strExportFile = Session("Report")
        Dim crReportDocument As ReportDocument
        crReportDocument = New ReportDocument
        crReportDocument.Load(Server.MapPath(Session("Report")))
        Dim oData As New CSBC.Components.Season.ClsSchedules
        'Pass the populated dataset to the report
        crReportDocument.SetDataSource(oData.GetGames(Session("CompanyID"), Session("SeasonID"), Session("ScheduleNo"), Session("TeamNbr"), Session("ScheduleDesc"), Session("TeamName")))
        crReportDocument.SetParameterValue("SeasonDesc", Session("SeasonDesc"))
        crReportDocument.SetParameterValue("CompanyName", Session("CompanyName"))
        crReportDocument.SetParameterValue("TeamName", Session("TeamName"))
        crReportDocument.SetParameterValue("Division", Session("ScheduleDesc"))
        Dim s As System.IO.MemoryStream = crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)
        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "inline; filename=" & strExportFile)
            .BinaryWrite(s.ToArray)
            .End()
        End With
        'CrystalReportViewer1.ReportSource = crReportDocument
        crReportDocument = Nothing
        oData = Nothing
    End Sub


    Private Sub DivStanding()
        Dim strExportFile As String
        strExportFile = Session("Report")
        Dim printDoc As New PrintDocument
        Dim PrinterName As String = ""
        Dim ReportPath As String = ""
        Dim crReportDocument As ReportDocument = New ReportDocument

        ReportPath = "Reports/DivStanding.rpt"
        crReportDocument.Load(Server.MapPath(ReportPath))
        Dim oData As New CSBC.Components.Season.clsGames
        'Pass the populated dataset to the report
        crReportDocument.SetDataSource(oData.GetStanding(Session("CompanyID"), Session("ScheduleNo")))
        crReportDocument.SetParameterValue("SeasonDesc", Session("SeasonDesc"))
        crReportDocument.SetParameterValue("CompanyName", Session("CompanyName"))
        crReportDocument.SetParameterValue("Division", Session("ScheduleDesc"))
        oData = Nothing
        Dim s As System.IO.MemoryStream = crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)
        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "inline; filename=" & strExportFile)
            .BinaryWrite(s.ToArray)

            .End()
        End With

        printDoc = Nothing
        crReportDocument = Nothing

    End Sub


    Private Sub Stats()
        If PrinterSettings.InstalledPrinters.Count = 0 Then

        Else
            Dim strExportFile As String
            strExportFile = "PlayersStats"
            Dim printDoc As New PrintDocument
            Dim ReportPath As String = ""
            Dim crReportDocument As ReportDocument = New ReportDocument

            ReportPath = "Reports/PlayersStats.rpt"
            crReportDocument.Load(Server.MapPath(ReportPath))
            Dim oData As New CSBC.Components.Season.clsGames
            'Pass the populated dataset to the report
            crReportDocument.SetDataSource(oData.GetStats(Session("ScheduleNo"), Session("SeasonID")))
            crReportDocument.SetParameterValue("SeasonDesc", Session("SeasonDesc"))
            crReportDocument.SetParameterValue("Division", Session("ScheduleDesc"))
            oData = Nothing

            Dim s As System.IO.MemoryStream = crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)
            With HttpContext.Current.Response
                .ClearContent()
                .ClearHeaders()
                .ContentType = "application/pdf"
                .AddHeader("Content-Disposition", "inline; filename=" & strExportFile)
                .BinaryWrite(s.ToArray)
                .End()
            End With

            printDoc = Nothing
            crReportDocument = Nothing

        End If
    End Sub
End Class
