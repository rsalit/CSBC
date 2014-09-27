using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;

namespace CSBC.Admin.Web
{
    public partial class SearchRegPay : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Title"] = "Registration Payments";
                LoadDivisions(Master.SeasonId);
                LoadRows();
            }
        }

        private void LoadRows()
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var players = rep.GetPlayers(Master.SeasonId).ToList<SeasonPlayer>();
            grdPlayers.DataSource = players;
            grdPlayers.DataBind();
        }
        private void LoadRows(int divisionId)
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var players = rep.GetDivisionPlayers(divisionId).ToList<SeasonPlayer>();
            grdPlayers.DataSource = players;
            grdPlayers.DataBind();
        }

        protected void grdPlayers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Session["PeopleID"] = e.CommandArgument;
                Response.Redirect(Master.PaymentsForm);
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
            LoadRows();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect(Master.SearchPeopleForm);
        }

        protected List<Division> GetDivisionList(int seasonId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new DivisionRepository(db);
                var divisions = rep.GetDivisions(seasonId).ToList<Division>();

                return divisions;
            }
        }
        protected void LoadDivisions(int seasonId)
        {
            var divisions = GetDivisionList(seasonId);
            foreach (Division division in divisions)
            {
                dropDownDivisions.Items.Add(new ListItem(division.Div_Desc, division.DivisionID.ToString()));
            }
            dropDownDivisions.Items.Insert(0, new ListItem("All"));
            dropDownDivisions.DataBind();
            dropDownDivisions.SelectedIndex = 0;
        }

        protected void dropDownDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropDownDivisions.SelectedIndex > 0)
            {
                LoadRows(Convert.ToInt32(dropDownDivisions.SelectedItem.Value));
            }
            else 
            {
                LoadRows();
            }
        }
    }
}