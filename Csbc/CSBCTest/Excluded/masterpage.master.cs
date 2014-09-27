using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;

namespace CSBC.Admin.Web
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        //CSBC.Core.Data.CSBCDbContext DbContext;
        public int CompanyId
        {
            get
            {
                if (Session["CompanyID"] == null)
                {
                    return 1;
                }
                else
                {
                    return (int)Session["CompanyID"];
                }
            }
        }
        public int SeasonId
        {
            get
            {
                return (int)Session["SeasonID"];
            }
        }    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                InitializeVariables(new CSBCDbContext());

            }
            else
            {
                InitializeVariables(new CSBCDbContext());
                if (Session["UserID"] != null)
                {
                    var rep = new UserRepository(new CSBCDbContext());
                    var user = rep.GetById((int)Session["UserID"]);
                    //lblUserName.Text = "Welcome" + user.Name;
                   // lblTitle.Text = Session["Title"].ToString();
                }
                    
            }
        }
        protected void InitializeVariables(CSBCDbContext context)
        {
            if (Session["CompanyID"] == null) 
                Session["CompanyID"] = 1;

            if (Session["SeasonID"] == null) 
            {
                var seasonrep = new SeasonRepository(context);
                var season = seasonrep.GetCurrentSeason(GetSessionCompany());
                Session["SeasonID"] = season.SeasonID;
                //lblSeason.Text = season.Sea_Desc;
                
                
            }
            //test other variables
        }
        public int GetSessionCompany()
        {
            return GetSessionVar("CompanyID", 1);
        }
        public int GetSessionVar(string sessionName, int defaultValue)
        {
            try
            {
                if (Session[sessionName] == null)
                {
                    Session[sessionName] = defaultValue;
                }
            }
            catch
            {
                Session[sessionName] = defaultValue;
            }
            return (int)Session[sessionName];
        }

    }

}