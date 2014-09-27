Imports System.Data
Imports CSBC.Components
Partial Class Documents
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Page.IsPostBack = False Then
            Call GetMessages("Documents")
        End If
    End Sub

    Private Sub GetMessages(ByVal MessScreen As String)
        Dim oContent As New Website.ClsContent
        Dim rsData As DataTable
        Try
            SetVisibility()
            rsData = oContent.GetContentNodate(Session("CompanyID"), MessScreen)
            For I As Int16 = 0 To rsData.Rows.Count - 1
                If rsData.Rows(I).Item("LineText") > "" Then
                    Select Case rsData.Rows(I).Item("cntSeq")
                        Case 1
                            SetProp(lnk1, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 3
                            SetProp(Lnk5, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 4
                            SetProp(Lnk7, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 5
                            SetProp(Lnk6, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 6
                            SetProp(Lnk9, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 7
                            SetProp(Lnk2, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 8
                            SetProp(Lnk4, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 9
                            SetProp(Lnk3, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 10
                            SetProp(Lnk8, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 11
                            SetProp(Lnk9, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 12
                            SetProp(Lnk10, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 13
                            SetProp(Lnk17, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 14
                            SetProp(Lnk18, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 15
                            SetProp(Lnk19, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 16
                            SetProp(Lnk20, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 17
                            SetProp(Lnk11, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 18
                            SetProp(Lnk21, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 19
                            SetProp(Lnk12, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 20
                            SetProp(Lnk22, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 21
                            SetProp(Lnk13, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 22
                            SetProp(Lnk23, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 23
                            SetProp(Lnk14, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 24
                            SetProp(Lnk15, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 25
                            SetProp(Lnk24, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 26
                            SetProp(Lnk25, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 27
                            SetProp(Lnk16, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))
                        Case 28
                            SetProp(Lnk26, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"))

                    End Select
                End If
            Next
        Catch ex As Exception
            lblError.Text = "GetMessages:" & ex.Message
        Finally
            oContent = Nothing
            rsData = Nothing
        End Try

    End Sub

    Private Sub SetVisibility()
        lnk1.Text = ""
        lnk2.Text = ""
        lnk3.Text = ""
        lnk4.Text = ""
        lnk5.Text = ""
        lnk6.Text = ""
        lnk7.Text = ""
        lnk8.Text = ""
        lnk9.Text = ""
        lnk10.Text = ""
        lnk11.Text = ""
        Lnk12.Text = ""
        Lnk13.Text = ""
        Lnk14.Text = ""
        Lnk15.Text = ""
        Lnk16.Text = ""
        Lnk17.Text = ""
        Lnk18.Text = ""
        Lnk19.Text = ""
        Lnk20.Text = ""
        Lnk21.Text = ""
        Lnk22.Text = ""
        Lnk23.Text = ""
        Lnk24.Text = ""
        Lnk25.Text = ""
        Lnk26.Text = ""
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
End Class
