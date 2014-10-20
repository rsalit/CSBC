using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Models;
using CSBC.Core.Data;
using System.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web
{
    public partial class DraftListReport : System.Web.UI.Page
    {
        List<SeasonPlayer> Players = new List<SeasonPlayer>();

        protected void Page_Load(object sender, EventArgs e)
        {
            using (var db = new CSBCDbContext())
            {
                var repo = new PlayerRepository(db);
                Players = repo.GetDivisionPlayers(757).ToList();
                ObjectDataSource1.DataBind();
            }
        }
    }
}