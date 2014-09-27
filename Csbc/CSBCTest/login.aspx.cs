using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CSBC.Components;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;

namespace CSBC.Admin.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(System.Object sender, System.EventArgs e)
        {
            //Put user code to initialize the page here
            if (!IsPostBack)
            {
                Session["UserID"] = null;
                var db = new CSBCDbContext();
                db.TestMode = false;
            }
        }
 
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var rep = new UserRepository(new CSBCDbContext());
            User user = rep.GetUser(txtUserName.Text, txtPassword.Text);
            if ((user == null) || (user.UserID == 0))
            {
                lblError.Text = "Invalid user / password";
                lblError.Visible = true;
            }
            else
            {
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.Name;
                Session["UserType"] = user.UserType;
                Session["CompanyID"] = rep.GetById(user.UserID).CompanyID;
                Session["TestMode"] = checkTestMode.Checked;

                var seasonrep = new SeasonRepository(new CSBCDbContext());
                var season = seasonrep.GetCurrentSeason((int)Session["CompanyID"]);
                Session["SeasonID"] = season.SeasonID;
                Response.Redirect("welcome1.aspx");
            }
        }  

    }
}