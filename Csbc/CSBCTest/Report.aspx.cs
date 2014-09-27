using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CSBC.Admin.Web
{

    public partial class Report : System.Web.UI.Page
    {
        public string sqlString = "";
        public DataSet dataSet;
        protected System.Data.SqlClient.SqlConnection SqlConnection2;
        public SqlCommand selectCMD;
        //TODO:: Review the following fields

        public int iDiv;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Commented 5/28 because missing report form Manny
            //if (Session["Report"].ToString() == "Reports/DivSchedule.rpt")
            //    DivSchedule();

            //If Session["Report"] = "Reports/DivStanding.rpt" Then DivStanding()

            //If Session["Report"] = "Reports/PlayersStats.rpt" Then Stats()
            //Test()
        }


        private void Test()
        {
            //Dim oData As New CSBC_DLL.CSBC.Components.Season.ClsSchedules
            //Dim crystalReport As New ReportDocument()
            //crystalReport.Load(Server.MapPath("Reports\DivSchedule.rpt"))
            //'dsCustomers = DirectCast(ViewState("Customers_Data"), Customers)
            //CrystalReportViewer1.ReportSource = crystalReport
            //crystalReport.SetDataSource(oData.GetGames(Session["CompanyID"], Session["SeasonID"], Session["ScheduleNo"], Session["TeamNbr"], Session["ScheduleDesc"], Session["TeamName"]))
        }

        private void DivSchedule()
        {
            string strExportFile = null;
            strExportFile = Session["Report"].ToString();
            ReportDocument crReportDocument = default(ReportDocument);
            crReportDocument = new ReportDocument();
            crReportDocument.Load(Server.MapPath(Session["Report"].ToString()));
            CSBC.Components.Season.ClsSchedules oData = new CSBC.Components.Season.ClsSchedules();
            //Pass the populated dataset to the report
            crReportDocument.SetDataSource(oData.GetGames(1, 1, Convert.ToInt32(Session["ScheduleNo"]),
                Convert.ToInt32(Session["TeamNbr"]), Session["ScheduleDesc"].ToString(), Session["TeamName"].ToString()));
            crReportDocument.SetParameterValue("SeasonDesc", Session["SeasonDesc"]);
            crReportDocument.SetParameterValue("CompanyName", Session["CompanyName"]);
            crReportDocument.SetParameterValue("TeamName", Session["TeamName"]);
            crReportDocument.SetParameterValue("Division", Session["ScheduleDesc"]);
            System.IO.MemoryStream s =
                (MemoryStream) crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
            var _with1 = HttpContext.Current.Response;
            _with1.ClearContent();
            _with1.ClearHeaders();
            _with1.ContentType = "application/pdf";
            _with1.AddHeader("Content-Disposition", "inline; filename=" + strExportFile);
            _with1.BinaryWrite(s.ToArray());
            _with1.End();
            //CrystalReportViewer1.ReportSource = crReportDocument
            crReportDocument = null;
            oData = null;
        }


        private void DivStanding()
        {
            string strExportFile = null;
            strExportFile = Session["Report"].ToString();
            PrintDocument printDoc = new PrintDocument();
            //string PrinterName = "";
            string ReportPath = "";
            ReportDocument crReportDocument = new ReportDocument();

            ReportPath = "Reports/DivStanding.rpt";
            crReportDocument.Load(Server.MapPath(ReportPath));
            CSBC.Components.Season.clsGames oData = new CSBC.Components.Season.clsGames();
            //Pass the populated dataset to the report
            crReportDocument.SetDataSource(oData.GetStanding(1, Convert.ToInt32(Session["ScheduleNo"])));
            crReportDocument.SetParameterValue("SeasonDesc", Session["SeasonDesc"]);
            crReportDocument.SetParameterValue("CompanyName", Session["CompanyName"]);
            crReportDocument.SetParameterValue("Division", Session["ScheduleDesc"]);
            oData = null;
            System.IO.MemoryStream s =
                (MemoryStream) crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
            var _with2 = HttpContext.Current.Response;
            _with2.ClearContent();
            _with2.ClearHeaders();
            _with2.ContentType = "application/pdf";
            _with2.AddHeader("Content-Disposition", "inline; filename=" + strExportFile);
            _with2.BinaryWrite(s.ToArray());

            _with2.End();

            printDoc = null;
            crReportDocument = null;

        }


        private void Stats()
        {

            if (PrinterSettings.InstalledPrinters.Count == 0)
            {
            }
            else
            {
                string strExportFile = null;
                strExportFile = "PlayersStats";
                PrintDocument printDoc = new PrintDocument();
                string ReportPath = "";
                ReportDocument crReportDocument = new ReportDocument();

                ReportPath = "Reports/PlayersStats.rpt";
                crReportDocument.Load(Server.MapPath(ReportPath));
                CSBC.Components.Season.clsGames oData = new CSBC.Components.Season.clsGames();
                //Pass the populated dataset to the report
                crReportDocument.SetDataSource(oData.GetStats(Convert.ToInt32(Session["ScheduleNo"]), 1));
                crReportDocument.SetParameterValue("SeasonDesc", Session["SeasonDesc"]);
                crReportDocument.SetParameterValue("Division", Session["ScheduleDesc"]);
                oData = null;

                System.IO.MemoryStream s = (MemoryStream)crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
                var _with3 = HttpContext.Current.Response;
                _with3.ClearContent();
                _with3.ClearHeaders();
                _with3.ContentType = "application/pdf";
                _with3.AddHeader("Content-Disposition", "inline; filename=" + strExportFile);
                _with3.BinaryWrite(s.ToArray());
                _with3.End();

                printDoc = null;
                crReportDocument = null;

            }
        }
    }
}

