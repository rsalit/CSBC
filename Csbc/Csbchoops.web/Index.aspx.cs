using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CSBC.Components;

namespace Csbchoops.Web
{
    partial class Index : System.Web.UI.Page
    {

        //Private Sub CheckBrowser()
        //    If Request.Browser.Browser <> "IE" Then
        //        lblBrowser.Text = "*** Internet Explorer is required to browse this site ***"
        //        lblBrowser.Text = lblBrowser.Text & "<br>(you are currently using " & Request.Browser.Browser & ")"
        //    End If
        //End Sub


        //Private Sub lnkWebmaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        //    Server.Transfer("mailto:webmaster@csbchoops.net")
        //End Sub

        //Private Sub lnkSponsorsForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSponsorsForm.Click
        //    Response.Redirect("http://www.csbchoops.com/Sponsorform.pdf")
        //End Sub

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string siteNameUrl = null;
            siteNameUrl = Request.ServerVariables["SERVER_NAME"];
            CheckDB();

            Session["Module"] = "HomePage";
            Session["CompanyID"] = 1;
            Session["CompanyName"] = "Coral Springs Basketball Club";
            //Put user code to initialize the page here
            if (Page.IsPostBack == false)
            {
                GetMessages("Index");
                GetUpComingDates();
            }
        }

        private void GetMessages(string MessScreen)
        {
            //Website.ClsContent oContent = new Website.ClsContent();
            //DataTable rsData = default(DataTable);
            //try
            //{
            //    SetVisibility();
            //    //TODO:: Change to HomePage instead of Index
            //    //rsData = oContent.GetContent(Session["CompanyID"], Session["Module"], Now())
            //    rsData = oContent.GetContent(Session["CompanyID"], "Index", Now());

            //    if (rsData.Rows.Count == 0)
            //    {
            //    }
            //    for (Int16 I = 0; I <= rsData.Rows.Count - 1; I++)
            //    {
            //        if (rsData.Rows(I).Item("LineText") > "")
            //        {
            //            switch (rsData.Rows(I).Item("cntSeq"))
            //            {
            //                case 1:
            //                    SetProp(Link1, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    break;
            //                case 2:
            //                    SetProp(Link2, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    break;
            //                case 3:
            //                    SetProp(Link3, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    break;
            //                case 4:
            //                    SetProp(Link4, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    imgStandings.Visible = false;
            //                    Link4.Visible = true;
            //                    break;
            //                case 5:
            //                    SetProp(Link5, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    imgStandings.Visible = false;
            //                    Link5.Visible = true;
            //                    break;
            //                case 6:
            //                    SetProp(Link6, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    imgStandings.Visible = false;
            //                    Link6.Visible = true;
            //                    break;
            //                case 7:
            //                    SetProp(Link7, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    imgStandings.Visible = false;
            //                    Link7.Visible = true;
            //                    break;
            //                case 8:
            //                    SetProp(Link8, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    imgStandings.Visible = false;
            //                    Link8.Visible = true;
            //                    break;
            //                case 9:
            //                    SetProp(Link9, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    imgStandings.Visible = false;
            //                    Link9.Visible = true;
            //                    break;
            //                case 10:
            //                    SetProp(Link10, rsData.Rows(I).Item("LineText"), rsData.Rows(I).Item("Bold"), rsData.Rows(I).Item("UnderLN"), rsData.Rows(I).Item("Italic"), rsData.Rows(I).Item("FontSize"), rsData.Rows(I).Item("FontColor"), rsData.Rows(I).Item("Link"));
            //                    imgStandings.Visible = false;
            //                    Link10.Visible = true;
            //                    break;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //lblError.Text = "GetMessages:" & ex.Message
            //}
            //finally
            //{
            //    oContent = null;
            //    rsData = null;
            //}
        }

        private void SetVisibility()
        {
            //Link1.Text = "";
            //Link2.Text = "";
            //Link3.Text = "";
            //Link4.Visible = false;
            //Link5.Visible = false;
            //Link6.Visible = false;
            //Link7.Visible = false;
            //Link8.Visible = false;
            //Link9.Visible = false;
            //Link10.Visible = false;
        }

        private void SetProp(HyperLink Link, string sText, bool bBold, bool bUnderLn, bool bItalic, string sFontSize, string sFontColor, string sLink)
        {
            //Link.Text = sText;
            //Link.Font.Bold = bBold;
            //if (bUnderLn == false)
            //    Link.Style("text-decoration:none;text-underline:none;") = true;
            //Link.Font.Italic = bItalic;
            //switch (sFontSize)
            //{
            //    case "XSMALL":
            //        Link.Font.Size = System.Web.UI.WebControls.FontUnit.XXSmall;
            //        break;
            //    case "SMALL":
            //        Link.Font.Size = System.Web.UI.WebControls.FontUnit.XSmall;
            //        break;
            //    case "MEDIUM":
            //        Link.Font.Size = System.Web.UI.WebControls.FontUnit.Small;
            //        break;
            //    case "LARGE":
            //        Link.Font.Size = System.Web.UI.WebControls.FontUnit.Medium;
            //        break;
            //}
            //Link.ForeColor = System.Drawing.Color.FromName(sFontColor);
            //Link.NavigateUrl = sLink;
        }


        private void GetUpComingDates()
        {
            //Website.clsCalendar oCalendar = new Website.clsCalendar();
            //DataTable rsData = default(DataTable);
            //int I = 0;
            //try
            //{
            //    rsData = oCalendar.GetCalendar(Session["CompanyID"], true, 1);
            //    for (I = 0; I <= rsData.Rows.Count - 1; I++)
            //    {
            //        //Label7.Text = Label7.Text + "*" + rsData.Rows(I).Item("Title") + "<BR>"
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = "GetUpComingDates:" + ex.Message;
            //}
            //finally
            //{
            //    oCalendar = null;
            //    rsData = null;
            //}

        }

        //Protected Sub lnk4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk4.Click
        //    Response.Redirect("Calendar.aspx")
        //End Sub

        private void CheckDB()
        {
            //Security.ClsUsers oDB = new Security.ClsUsers();
            //DataTable rsData = default(DataTable);
            //try
            //{
            //    oDB.GetAccess(0, "", 0, 0);
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("WebsiteDown.aspx");
            //}
            //finally
            //{
            //    oDB = null;
            //    rsData = null;
            //}

        }
    }
}