using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;
using CSBC.Admin.Web.ViewModels;
using CSBC.Core.Models;
using CSBC.Components;

namespace CSBC.Admin.Web
{
    public partial class SelectSeason : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] == null)
                Response.Redirect("Login.aspx");

            if (Page.IsPostBack == false)
            {
                Session["Title"] = "Search Seasons";
                Session["AccessType"] = AccessType();
                PopulateGrid();
            }
        }

        private string AccessType()
        {
            string functionReturnValue = null;
            var oSecurity = new CSBC.Components.Security.ClsUsers();
            try
            {
                oSecurity.GetAccess((int)Session["UserID"], "Seasons", (int)Session["CompanyID"], (int)Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                lblError.Text = "AccessType::" + ex.Message;
            }
            finally
            {
                functionReturnValue = oSecurity.AccessType;
                oSecurity = null;
            }
            return functionReturnValue;
        }

      
        protected void grdSeasons_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "SelectSeason")
            {
                Master.SeasonId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("Welcome1.aspx");
            }
            else if (e.CommandName == "ViewSeason")
            {
                Master.SeasonId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("Seasons.aspx");
            }
        }
     
       public void PopulateGrid()
        {
            grdSeasons.DataSource = GetSeasons();
            grdSeasons.DataBind();
        }
        public IEnumerable<SelectSeasonVM> GetSeasons()
        {
            var vm = new ViewModels.SelectSeasonVM();
            var seasons = vm.GetRecords(Master.CompanyId);
            return seasons;
        }
    }
}

