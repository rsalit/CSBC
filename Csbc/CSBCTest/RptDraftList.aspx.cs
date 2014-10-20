using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Data;
using Microsoft.Reporting.WebForms;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class RptDraftList : BaseForm
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
           // GenerateReport();
            Session["Title"] = "Draft List";
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                LoadDivisions();
            }
        }

        protected override void SetUser()
        {
            base.SetUser();
            if (Master.AccessType == "R")
            {
                ddlDivisions.Enabled = true;
                btnPrint.Enabled = true;
              
            }
        }
        protected void GenerateReport()
        {

            var rep = new ViewModels.PlayerVM();
            var players = ViewModels.PlayerVM.GetSeasonPlayers(Master.SeasonId);
            //ReportViewer1
            //DraftListDataSource.DataBind();
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportDataSource dataSource = new ReportDataSource("DraftReport", players);
            //ReportViewer1.LocalReport.DataSources.Add(dataSource);

            //ReportViewer1.ReportRefresh(players, new CancelEventArgs());

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Master is CSBCAdminMasterPage)
            {
                ((CSBCAdminMasterPage)this.Master).SeasonChanged += new EventHandler(HandleSeasonChanged);
            }
        }

        protected void ddlDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var divisionId = Convert.ToInt32(ddlDivisions.SelectedValue);

            LoadPlayers(divisionId);
        }

        private void LoadPlayers(int divisionId)
        {
            var vm = new PlayerVM();
            gridPlayers.DataSource = vm.GetDivisionPlayers(divisionId);
            gridPlayers.DataBind();
            if (!gridPlayers.Visible)
                gridPlayers.Visible = true;
        }

        private void LoadDivisions()
        {
            var rep = new DivisionRepository(new CSBCDbContext());

            try
            {
                var divisions = rep.GetDivisions(Master.SeasonId).ToList<Division>();
                ddlDivisions.DataSource = divisions;
                ddlDivisions.DataValueField = "DivisionID";
                ddlDivisions.DataTextField = "Div_Desc";
                ddlDivisions.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["DivisionID"] = Convert.ToInt32(ddlDivisions.SelectedValue);
            Session["DivisionName"] = ddlDivisions.SelectedItem.Text;
            //Session["Season"] = 
            Response.Redirect("reports/rptDivisionDraftList.aspx"); 
        }

      

        private void HandleSeasonChanged(object sender, EventArgs e)
        {
            LoadDivisions();
            gridPlayers.Visible = false;
        }
    }
}