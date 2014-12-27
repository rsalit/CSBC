Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class Report
    Inherits System.Web.UI.Page
    'Dim rdr As SqlClient.SqlDataReader
    'Dim SQL As String
    'Dim SqlDataAdapter1 As New SqlDataAdapter
    'Dim strExportFile As String
    'Dim dataset1 As DataSet
    'Dim oCol As CellColumn
    'Protected WithEvents SqlCn As System.Data.SqlClient.SqlConnection
    'Dim oGroup As Group
    '#Region " Web Form Designer Generated Code "

    '    'This call is required by the Web Form Designer.
    '    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    '        Me.SqlCn = New System.Data.SqlClient.SqlConnection
    '        '
    '        'SqlCn
    '        '
    '        Me.SqlCn.ConnectionString = ConfigurationSettings.AppSettings("MyCn")

    '    End Sub
    '    Protected WithEvents lblReport As System.Web.UI.WebControls.Label

    '    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    '    'Do not delete or move it.
    '    Private designerPlaceholderDeclaration As System.Object

    '    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    '        'CODEGEN: This method call is required by the Web Form Designer
    '        'Do not modify it using the code editor.
    '        InitializeComponent()
    '    End Sub

    '#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        PrintReport()
    End Sub

    Private Sub PrintReport()

        'Dim oReport As ColaReport.Report
        'SqlCn.Open()
        'Session("TransactionID") = "61875146L73796636"
        'SQL = "EXEC GetRegisteredPlayers @TransactionID=" & Quotes(Session("TransactionID"))
        'Dim selectCMD As SqlCommand = New SqlCommand(SQL, SqlCn)
        ''Create a instance of a Dataset
        'dataset1 = New DataSet
        'SqlDataAdapter1.SelectCommand = selectCMD
        'SqlDataAdapter1.Fill(dataset1, "Registration")

        'oReport = New ColaReport.Report
        'oReport.LinesPerPage = 80
        'oReport.Style.Add("font-family", "Times New Roman")
        'oReport.HeaderRowAttributes.BackColor = System.Drawing.Color.White
        'oReport.FirstPageHeader.Text = "<h4>" & "Registration" & "</h4>"
        'oReport.FirstPageHeader.Style.Add("font-family", "Times New Roman")

        'oReport.Width = System.Web.UI.WebControls.Unit.Pixel(660)
        'oReport.Attributes("Border") = "0"
        'oReport.BorderStyle = BorderStyle.None

        'oCol = oReport.CreateCellColumn("Name", "Name")
        'oCol.Width = WebControls.Unit.Pixel(240)

        'oCol = oReport.CreateCellColumn("DivDesc", "Division")
        'oCol.Width = WebControls.Unit.Pixel(200)

        'oCol = oReport.CreateCellColumn("DraftVenue", "tryouts Location")
        'oCol.Width = WebControls.Unit.Pixel(150)

        'oCol = oReport.CreateCellColumn("DraftDate", "Date")
        'oCol.Width = WebControls.Unit.Pixel(100)

        'oCol = oReport.CreateCellColumn("DraftTime", "Time")
        'oCol.Width = WebControls.Unit.Pixel(100)

        'oReport.Bind(dataset1)

        'lblReport.Text = oReport.Execute()
        'SqlCn.Close()
        'SqlCn = Nothing
    End Sub
End Class
