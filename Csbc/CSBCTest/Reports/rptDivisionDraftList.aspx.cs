using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Data;
using Microsoft.Reporting.WebForms;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web.Reports
{
    public partial class rptDivisionDraftList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var divisionId = Convert.ToInt32(Session["DivisionId"]);
            Session["SeasonName"] = (string)GetSeasonName(); 
            var vm = new PlayerVM();
            gridPlayers.DataSource = vm.GetDivisionPlayers(divisionId);
            gridPlayers.DataBind();
        }

        private string GetSeasonName()
        {
            using (var db = new CSBCDbContext())
            {
                var repo = new SeasonRepository(db);
                var season = repo.GetById(Convert.ToInt32(Session["SeasonId"]));
                return season.Description;
            }
        }
    }
}