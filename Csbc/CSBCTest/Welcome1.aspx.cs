using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using CSBC.Components;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web
{
    public partial class Welcome1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack == false)
            {
                Session["Title"] = "Welcome";
                //LoadSeason();
                LoadTotals();
            }
            else
            {
                //LoadSeason();
                LoadTotals();
            }
        }

        private void LoadSeason()
        {
            var rep = new SeasonRepository(new CSBCDbContext());
            var season = new Season();
            //CSBC.Components.Season.ClsSeasons oSeason = new CSBC.Components.Season.ClsSeasons();
            var master = new CSBCAdminMasterPage();
            try
            {
                season = rep.GetCurrentSeason(master.GetSessionCompany());
                //_with1.GetCurrentSeason(1);
                Session["SeasonDesc"] = season.Description;
                Session["CompanyID"] = season.CompanyID;
                Session["SeasonID"] = season.SeasonID;
            }
            catch (Exception ex)
            {
                //lblError.Text = "Invalid Username/Password"
            }
        }

        protected void LoadTotals()
        {
            var oSeasonCounts = new CSBC.Components.Season.ClsSeasons();
            DataTable rsData = default(DataTable);
            using (var ctx = new CSBCDbContext())
            {
                var idParam = new SqlParameter
                {
                    ParameterName = "CompanyID",
                    Value = 1
                };
            }        
    
            try
            {
                rsData = oSeasonCounts.GetSeasonCounts(Master.SeasonId, Master.CompanyId);
                var x = rsData.Rows.Count;
                grdTotals.DataSource = rsData;
                grdTotals.DataBind();
                grdTotals.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = "Invalid Username/Password " + ex.ToString();
            }
          

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Master is CSBCAdminMasterPage)
            {
                ((CSBCAdminMasterPage)this.Master).SeasonChanged += new EventHandler(HandleSeasonChanged);
            }
        }

        private void HandleSeasonChanged(object sender, EventArgs e)
        {
            LoadTotals();
        }
    }
}

