using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using CSBC.Components;

using System.Web.UI.WebControls;

namespace Csbchoops.Web
{
    public partial class ClubDocs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Put user code to initialize the page here
            if (Page.IsPostBack == false)
            {
                //GetMessages("Documents");
            }
        }
        private void GetMessages(string MessScreen)
        {
            //var oContent = new CSBC.Components.Website.ClsContent();
            //DataTable rsData = default(DataTable);

            //SetVisibility();
            //rsData = oContent.GetContentNodate(Int32.Parse(Session["CompanyID"].ToString()), MessScreen);
            //for (Int16 I = 0; I <= rsData.Rows.Count - 1; I++)
            //{
            //    if (String.IsNullOrEmpty(rsData.Rows[I]["LineText"].ToString()))
            //    {
            //    }
                //switch (rsData.Rows[I]["cntSeq"].ToString())
                //{
                //    case 1:
                //        SetProp(lnk1, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 3:
                //        SetProp(Lnk5, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 4:
                //        SetProp(Lnk7, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 5:
                //        SetProp(Lnk6, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 6:
                //        SetProp(Lnk9, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 7:
                //        SetProp(Lnk2, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 8:
                //        SetProp(Lnk4, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 9:
                //        SetProp(Lnk3, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 10:
                //        SetProp(Lnk8, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 11:
                //        SetProp(Lnk9, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 12:
                //        SetProp(Lnk10, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 13:
                //        SetProp(Lnk17, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 14:
                //        SetProp(Lnk18, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 15:
                //        SetProp(Lnk19, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 16:
                //        SetProp(Lnk20, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 17:
                //        SetProp(Lnk11, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 18:
                //        SetProp(Lnk21, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 19:
                //        SetProp(Lnk12, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 20:
                //        SetProp(Lnk22, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 21:
                //        SetProp(Lnk13, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 22:
                //        SetProp(Lnk23, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 23:
                //        SetProp(Lnk14, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 24:
                //        SetProp(Lnk15, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 25:
                //        SetProp(Lnk24, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 26:
                //        SetProp(Lnk25, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 27:
                //        SetProp(Lnk16, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());
                //        break;
                //    case 28:
                //        SetProp(Lnk26, rsData.Rows[I]["LineText"].ToString(), rsData.Rows[I]["Bold"].ToString(), rsData.Rows[I]["UnderLN"].ToString(), rsData.Rows[I]["Italic"].ToString(), rsData.Rows[I]["FontSize"].ToString(), rsData.Rows[I]["FontColor"].ToString(), rsData.Rows[I]["Link"].ToString());

                //        break;
                //  }
                //
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        lblError.Text = "GetMessages:" + ex.Message;
                //    }
            //}
        }

        //}

        //private void SetVisibility()
        //{
        //    lnk1.Text = "";
        //    lnk2.Text = "";
        //    lnk3.Text = "";
        //    lnk4.Text = "";
        //    lnk5.Text = "";
        //    lnk6.Text = "";
        //    lnk7.Text = "";
        //    lnk8.Text = "";
        //    lnk9.Text = "";
        //    lnk10.Text = "";
        //    lnk11.Text = "";
        //    Lnk12.Text = "";
        //    Lnk13.Text = "";
        //    Lnk14.Text = "";
        //    Lnk15.Text = "";
        //    Lnk16.Text = "";
        //    Lnk17.Text = "";
        //    Lnk18.Text = "";
        //    Lnk19.Text = "";
        //    Lnk20.Text = "";
        //    Lnk21.Text = "";
        //    Lnk22.Text = "";
        //    Lnk23.Text = "";
        //    Lnk24.Text = "";
        //    Lnk25.Text = "";
        //    Lnk26.Text = "";
        //}

        private void SetProp(HyperLink Link, string sText, bool bBold, bool bUnderLn, bool bItalic, string sFontSize, string sFontColor, string sLink)
        {
        //    Link.Text = sText;
        //    Link.Font.Bold = bBold;
        //    //if (bUnderLn == false)
        //    //Link.Style["text-decoration:none;text-underline:none;") = true;
        //    Link.Font.Italic = bItalic;
        //    switch (sFontSize)
        //    {
        //        case "XSMALL":
        //            Link.Font.Size = System.Web.UI.WebControls.FontUnit.XXSmall;
        //            break;
        //        case "SMALL":
        //            Link.Font.Size = System.Web.UI.WebControls.FontUnit.XSmall;
        //            break;
        //        case "MEDIUM":
        //            Link.Font.Size = System.Web.UI.WebControls.FontUnit.Small;
        //            break;
        //        case "LARGE":
        //            Link.Font.Size = System.Web.UI.WebControls.FontUnit.Medium;
        //            break;
        //    }
        //    Link.ForeColor = System.Drawing.Color.FromName(sFontColor);
        //    Link.NavigateUrl = sLink;
        }

    }
}