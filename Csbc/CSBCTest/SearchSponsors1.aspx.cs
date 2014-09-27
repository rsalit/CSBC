using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using CSBC.Components;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class SearchSponsors1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            { 
                Session["Title"] = "Search Sponsors";
                GetData(String.Empty);  
            }
        }

        protected void grdSearchSponsor_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var id = Convert.ToInt32(e.CommandArgument);
            Session["SponsorProfileID"] = id;
            Response.Redirect(Master.SponsorForm);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["FirstLetter"] = "";
            if (!String.IsNullOrEmpty(txtSponsorName.Text))
            {
                Session["FirstLetter"] = txtSponsorName.Text;
            }
            GetData(txtSponsorName.Text);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["SponsorProfileID"] = 0;
            Response.Redirect(Master.SponsorForm);
        }

        private void GetData(string value)
        {
            GetSponsorsNotInSeason(value);
            GetSponsorsForSeason(Master.SeasonId);
        }

        private void GetSponsorsNotInSeason(string value)
        {
            using (var db = new CSBCDbContext())
            {
                try
                {
                    var rep = new SponsorProfileRepository(db);
                    List<SponsorProfile> sponsors;
                    if (String.IsNullOrEmpty(value))
                        sponsors = rep.GetSponsorsNotInSeason(Master.CompanyId, Master.SeasonId).ToList<SponsorProfile>();
                    else
                        sponsors = rep.GetAll(Master.CompanyId, value).ToList<SponsorProfile>();

                    grdSearchSponsor.DataSource = sponsors;
                    grdSearchSponsor.DataBind();
                }
                catch (Exception ex)
                {
                    lblError.Text = "GetData::" + ex.Message;
                }
            }
        }

        private void GetSponsorsForSeason(int seasonId)
        {
           
            using (var db = new CSBCDbContext())
            {
                try
                {
                    var rep = new SponsorProfileRepository(db);
                    List<SponsorProfile> sponsors; 
                    //if (String.IsNullOrEmpty(value))
                        sponsors = rep.GetSponsorsInSeason(Master.CompanyId, Master.SeasonId).ToList<SponsorProfile>();
                    //else
                    //    sponsors = rep.GetAll(Master.CompanyId).ToList<SponsorProfile>();

                    gridSponsorsInSeason.DataSource = sponsors;
                    gridSponsorsInSeason.DataBind();
                }
                catch (Exception ex)
                {
                    lblError.Text = "GetData::" + ex.Message;
                }
                }
        }
        protected void grdSearchSponsor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var commandName = e.CommandName;
            switch (commandName)
            {
                case "AddSponsor":
                    AddToSeason(Convert.ToInt32(e.CommandArgument));
                    GetData(String.Empty);
                    break;
                case "Removesponsor":
                    break;
                case "SelectSponsor":
                    GoToSponsor(Convert.ToInt32(e.CommandArgument));
                    break;
            }
            
        }

        protected void AddToSeason(int sponsorProfileId)
        {
            SponsorProfileVM.AddSponsorToSeason(Master.CompanyId, Master.SeasonId, sponsorProfileId);
        }
        protected void GoToSponsor(int sponsorProfileId)
        {
            Session["SponsorProfileID"] = sponsorProfileId;
            Response.Redirect(Master.SponsorForm);
        }

        protected void gridSponsorsInSeason_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

 
        /*
         protected void btnNew_Click(object sender, System.EventArgs e)
         {
             Session.Add("SponsorProfileID", 0);
             //Session["FirstLetter"] = grdSponsors.DisplayLayout.ActiveRow.Cells.FromKey("SpoName").Value()
             Response.Redirect("Sponsors.aspx");
         }

         protected void grdSponsors_SelectedRowsChange(object sender, EventArgs e)
         {
             Session["SponsorProfileID"] = grdSponsors.DisplayLayout.ActiveRow.Cells.FromKey("SponsorProfileID").Value();
             Session["FirstLetter"] = grdSponsors.DisplayLayout.ActiveRow.Cells.FromKey("SpoName").Value();
             Response.Redirect("Sponsors.aspx");
         }*/

    }
}

