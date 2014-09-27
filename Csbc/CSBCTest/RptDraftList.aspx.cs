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

namespace CSBC.Admin.Web
{
    public partial class RptDraftList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateReport();
        }

        protected void GenerateReport()
        {

            var rep = new ViewModels.PlayerVM();
            var players = ViewModels.PlayerVM.GetSeasonPlayers(Master.SeasonId);
            //ReportViewer1
            DraftListDataSource.DataBind();
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

        private void HandleSeasonChanged(object sender, EventArgs e)
        {
            GenerateReport();
        }
    }
}