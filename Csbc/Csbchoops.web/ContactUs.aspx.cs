using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace Csbchoops.Web
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateList();
        }

        public void PopulateList()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new DirectorRepository(db);
                var board = rep.GetAll().ToList<Director>().Where(b => b.CompanyID == 1).OrderBy(b => b.Seq);
                repBoard.DataSource = board;
                repBoard.DataBind();

            }
        }
    }
}