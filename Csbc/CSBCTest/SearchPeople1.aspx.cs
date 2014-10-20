using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Core.Repositories;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class SearchPeople1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void SearchPeople()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PersonRepository(db);
                if (String.IsNullOrEmpty(txtLastName.Text) && (String.IsNullOrEmpty(txtFirstName.Text)))
                {
                    MasterVM.MsgBox(this, "A search value must be entered");
                }
                else
                {
                    var people = rep.FindPeopleByLastAndFirstName(txtLastName.Text, txtFirstName.Text, checkPlayersOnly.Checked).ToList<Person>();
                    grdPeople.DataSource = people;
                    grdPeople.DataBind();
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPeople();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["PeopleID"] = -1;
            Response.Redirect(Master.PeopleForm);
        }

        protected void grdPeople_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var personId = Convert.ToInt32(e.CommandArgument);
            Session["PeopleID"] = personId;
            SetPersonObject(personId);
            if (e.CommandName == "Select")
            {
                Response.Redirect(Master.PeopleForm);
            }
            else
            {
                if (e.CommandName == "Register")
                {
                    Response.Redirect(Master.PaymentsForm);
                }
            }

        }

        private void SetPersonObject(int personId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PersonRepository(db);
                var person = rep.GetById(personId);
                Master.Person = person;
            }
        }
    }
}