Imports System.Data
Imports CSBC.Components
Partial Class Index
    Inherits System.Web.UI.Page

    'Private Sub CheckBrowser()
    '    If Request.Browser.Browser <> "IE" Then
    '        lblBrowser.Text = "*** Internet Explorer is required to browse this site ***"
    '        lblBrowser.Text = lblBrowser.Text & "<br>(you are currently using " & Request.Browser.Browser & ")"
    '    End If
    'End Sub


    'Private Sub lnkWebmaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Server.Transfer("mailto:webmaster@csbchoops.net")
    'End Sub

    'Private Sub lnkSponsorsForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSponsorsForm.Click
    '    Response.Redirect("http://www.csbchoops.com/Sponsorform.pdf")
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SiteNameURL As String
        SiteNameURL = Request.ServerVariables("SERVER_NAME")
        CheckDB()
        Select Case Trim(SiteNameURL)
            Case "mobil.csbchoops.com"
                Response.Redirect("Mobil.aspx")
        End Select

        Session("Module") = "HomePage"
        Session("CompanyID") = 1
        Session("CompanyName") = "Coral Springs Basketball Club"
        'Put user code to initialize the page here
        If Page.IsPostBack = False Then
            Call GetMessages("Index")
            Call GetUpComingDates()
        End If
    End Sub

    Private Sub GetMessages(ByVal MessScreen As String)
        Dim oContent As New Website.ClsContent
        Dim rsData As DataTable
        Try
            SetVisibility()
            'TODO:: Change to HomePage instead of Index
            'rsData = oContent.GetContent(Session("CompanyID"), Session("Module"), Now())
            rsData = oContent.GetContent(Session("CompanyID"), "Index", Now())
            If rsData.Rows.Count = 0 Then

            End If
            For I As Int16 = 0 To rsData.Rows.Count - 1
                If rsData.Rows(I).Item("LineText") > "" Then
                    Select Case rsData.Rows(I).Item("cntSeq")
                        Case 1
                            SetProp(Link1, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 2
                            SetProp(Link2, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 3
                            SetProp(Link3, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 4
                            SetProp(Link4, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                            imgStandings.Visible = False
                            Link4.Visible = True
                        Case 5
                            SetProp(Link5, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                            imgStandings.Visible = False
                            Link5.Visible = True
                        Case 6
                            SetProp(Link6, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                            imgStandings.Visible = False
                            Link6.Visible = True
                        Case 7
                            SetProp(Link7, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                            imgStandings.Visible = False
                            Link7.Visible = True
                        Case 8
                            SetProp(Link8, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                            imgStandings.Visible = False
                            Link8.Visible = True
                        Case 9
                            SetProp(Link9, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                            imgStandings.Visible = False
                            Link9.Visible = True
                        Case 10
                            SetProp(Link10, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                            imgStandings.Visible = False
                            Link10.Visible = True
                    End Select
                End If
            Next
        Catch ex As Exception
            'lblError.Text = "GetMessages:" & ex.Message
        Finally
            oContent = Nothing
            rsData = Nothing
        End Try
    End Sub

    Private Sub SetVisibility()
        Link1.Text = ""
        Link2.Text = ""
        Link3.Text = ""
        Link4.Visible = False
        Link5.Visible = False
        Link6.Visible = False
        Link7.Visible = False
        Link8.Visible = False
        Link9.Visible = False
        Link10.Visible = False
    End Sub

    Private Sub SetProp(ByVal Link As HyperLink, ByVal sText As String, ByVal bBold As Boolean, ByVal bUnderLn As Boolean, ByVal bItalic As Boolean, ByVal sFontSize As String, ByVal sFontColor As String, ByVal sLink As String)
        Link.Text = sText
        Link.Font.Bold = bBold
        If bUnderLn = False Then Link.Style("text-decoration:none;text-underline:none;") = True
        Link.Font.Italic = bItalic
        Select Case sFontSize
            Case "XSMALL"
                Link.Font.Size = System.Web.UI.WebControls.FontUnit.XXSmall
            Case "SMALL"
                Link.Font.Size = System.Web.UI.WebControls.FontUnit.XSmall
            Case "MEDIUM"
                Link.Font.Size = System.Web.UI.WebControls.FontUnit.Small
            Case "LARGE"
                Link.Font.Size = System.Web.UI.WebControls.FontUnit.Medium
        End Select
        Link.ForeColor = System.Drawing.Color.FromName(sFontColor)
        Link.NavigateUrl = sLink
    End Sub


    Private Sub GetUpComingDates()
        Dim oCalendar As New Website.clsCalendar
        Dim rsData As DataTable
        Dim I As Integer
        Try
            rsData = oCalendar.GetCalendar(Session("CompanyID"), True, 1)
            For I = 0 To rsData.Rows.Count - 1
                'Label7.Text = Label7.Text + "*" + rsData.Rows(I).Item("Title") + "<BR>"
            Next
        Catch ex As Exception
            lblError.Text = "GetUpComingDates:" & ex.Message
        Finally
            oCalendar = Nothing
            rsData = Nothing
        End Try

    End Sub

    'Protected Sub lnk4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk4.Click
    '    Response.Redirect("Calendar.aspx")
    'End Sub

    Private Sub CheckDB()
        Dim oDB As New Security.ClsUsers
        Dim rsData As DataTable
        Try
            oDB.GetAccess(0, "", 0, 0)
        Catch ex As Exception
            Response.Redirect("WebsiteDown.aspx")
        Finally
            oDB = Nothing
            rsData = Nothing
        End Try

    End Sub
End Class
