using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Models;
using Microsoft.Reporting.WebForms;


namespace CSBC.Admin.Web
{
    public partial class ReportForm : BaseForm
    {
        public enum CsbcReports { DraftListReport, PlayerList, TryoutList }

        public CsbcReports CurrentReport
        {
            get { return (CsbcReports)Session["CurrentReport"]; }
            set { Session["CurrentReport"] = value; }
        }
        protected void btnGenerate_OnClick(object sender, EventArgs e)
        {
            GenerateReport();
        }

        protected void ddlReports_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentReport = ConvertReportToEnum(ddlReports.SelectedValue.ToString());
        }

        private CsbcReports ConvertReportToEnum(string reportName)
        {
            switch (reportName)
            {
                case ("Draft List"):
                    return CsbcReports.DraftListReport;
                    break;
                case ("Player List"):
                    return CsbcReports.PlayerList;
                    break;
                case ("Tryout List"):
                    return CsbcReports.TryoutList;
                    break;
                default:
                    return CsbcReports.DraftListReport;
            }
        }

        protected void GenerateReport()
        {
            SetReportData(CurrentReport, ReportViewer1);
            //ReportViewer1.DataBind();
            ReportViewer1.Visible = true;
            //ReportViewer1.ReportRefresh(this, new CancelEventArgs());
        }

        private void SetReportData(CsbcReports currentReport, ReportViewer viewer)
        {
            StandardReport report;

            switch (CurrentReport)
            {
                case CsbcReports.DraftListReport:
                    report = new DraftList(Master.SeasonId, "reports/DraftListReport.rdlc",
                        "CSBC.Admin.Web.DraftListReport.rdlc", ReportViewer1);
                    ReportViewer1 = report.GenerateReport();
                   
                    ReportViewer1.LocalReport.Refresh();
                    break;
                case CsbcReports.TryoutList:
                    report = new TryoutList(Master.SeasonId, "reports/TryoutList.rdlc",
                        "CSBC.Admin.Web.TryoutList.rdlc", ReportViewer1);
                    ReportViewer1 = report.GenerateReport();
                    
                    ReportViewer1.LocalReport.Refresh();
                    break;
                default:
                    break;
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

        protected override void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "Reports";
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                SetReportList();
            }
           
        }
        protected override void SetUser()
        {
            //base.SetUser();
            foreach (Control c in Controls)
            {
                var tb = c as WebControl;
                if (tb != null)
                {
                    tb.Enabled = true;
                }
            }
        }
        private void SetReportList()
        {
            ddlReports.Items.Clear();
            ddlReports.Items.Add(new ListItem("Draft List"));
            ddlReports.Items.Add(new ListItem("Player List"));
            ddlReports.Items.Add(new ListItem("Tryout List"));
        }
        private void HandleSeasonChanged(object sender, EventArgs e)
        {
            GenerateReport();
        }
    }

    public interface ICsbcReport
    {
        string ReportPath { get; set; }
        string ReportEmbeddedResource { get; set; }
        
        int SeasonId { get; set; }
        ReportViewer Viewer { get; set; }
        ReportViewer GenerateReport();
    }

    public class StandardReport : ICsbcReport
    {
        public virtual int SeasonId { get; set; }
        public ReportViewer Viewer { get; set; }
        public virtual string ReportPath
        {
            get { return "reports/DraftListReport.rdlc"; }
            set { }
        }

        public virtual string ReportEmbeddedResource
        {
            get { return "CSBC.Admin.Web.DraftListReport.rdlc"; }
            set { }
        }

        public virtual string DataSetName
        {
            get { return "DataSet1"; }
            set { }
        }
        public StandardReport()
        {
        }

        public StandardReport(int seasonId, string reportPath, string reportEmbeddedResource, ReportViewer viewer )
        {
            SeasonId = seasonId;
            ReportPath = reportPath;
            ReportEmbeddedResource = reportEmbeddedResource;
            Viewer = viewer;
        }

        public virtual List<SeasonPlayer> GetData()
        {
            var rep = new ViewModels.PlayerVM();
            var data = ViewModels.PlayerVM.GetSeasonPlayers(SeasonId);
            return data;
        }

        public virtual ReportViewer GenerateReport()
        {
            var data = GetData();
            var ds = new ReportDataSource(DataSetName, data);

            Viewer.Reset();
            Viewer.LocalReport.ReportEmbeddedResource = ReportEmbeddedResource;
            Viewer.LocalReport.ReportPath = ReportPath;
            Viewer.LocalReport.DataSources.Clear();
            Viewer.LocalReport.DataSources.Add(ds);
            Viewer.DataBind();
            return Viewer;
        }
    }

    public class TryoutList : StandardReport, ICsbcReport
    {
        public override string ReportPath
        {
            get { return "reports/TryoutList.rdlc"; }
            set {}
        }

        public override string ReportEmbeddedResource
        {
            get { return "CSBC.Admin.Web.TryoutList.rdlc"; }
            set { }
        }
        public override string DataSetName
        {
            get { return "TryoutDs"; }
            set { }
        }
        public TryoutList(int seasonId, string reportPath, string reportEmbeddedResource, ReportViewer viewer) : base(seasonId, reportPath,  reportEmbeddedResource, viewer  )     {
           
        }
       public override List<SeasonPlayer> GetData()
        {
            var rep = new ViewModels.PlayerVM();
            var data = ViewModels.PlayerVM.GetSeasonPlayers(SeasonId);
            return data;
        }
    }

   
    public class DraftList : StandardReport, ICsbcReport
    {
        
        public override string ReportPath
        {
            get { return "reports/DraftListReport.rdlc"; }
            set { }
        }

        public override string ReportEmbeddedResource
        {
            get { return "CSBC.Admin.Web.DraftListReport.rdlc"; }
            set { }
        }

        public DraftList(int seasonId, string reportPath, string reportEmbeddedResource, ReportViewer viewer) : base(seasonId, reportPath, reportEmbeddedResource, viewer)
        {
           
        }

        public override List<SeasonPlayer> GetData()
        {
            var rep = new ViewModels.PlayerVM();
            var data = ViewModels.PlayerVM.GetSeasonPlayers(SeasonId);
            return data;
        }

    }
}