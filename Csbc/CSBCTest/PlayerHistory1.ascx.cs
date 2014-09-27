using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web
{
    public partial class PlayerHistory1 : System.Web.UI.UserControl
    {
       
        public List<PlayerHistory> PlayerHistoryList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // LoadPlayerHistory(Master.PeopleId);
            }
        }

        public void LoadPlayerHistory(int personId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PlayerRepository(db);
                PlayerHistoryList = rep.GetPlayerHistory(personId);
                gridPlayerHistory.DataSource = PlayerHistoryList;                                
                gridPlayerHistory.DataBind();
            }
        }
    }
}