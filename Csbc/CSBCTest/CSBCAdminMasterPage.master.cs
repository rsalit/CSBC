using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;

namespace CSBC.Admin.Web
{
    public partial class CSBCAdminMasterPage : System.Web.UI.MasterPage
    {
        #region Master properties
        //CSBC.Core.Data.CSBCDbContext DbContext;
        public int CompanyId
        {
            get
            {
                if (Session["CompanyID"] == null)
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["CompanyId"]); 
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
            set
            {
                Session["SeasonID"] = value;

                ddlSeasons.DataValueField = SeasonId.ToString();  
            }
        }
        public int DivisionId
        {
            get
            {
                if (Session["DivisionID"] == null)
                    return 0;
                else
                    return (int)Session["DivisionID"];
            }
            set
            {
                Session["DivisionID"] = value;
            }
        }

        public int UserId
        {
            get
            {
                return Convert.ToInt32(Session["UserID"]);
            }
            set
            {
                Session["UserID"] = value;
                var rep = new UserRepository(new CSBCDbContext());
                UserName = rep.GetById(value).UserName;
            }
        }

        public string UserName
        {
            get { return Session["UserName"].ToString(); }
            set { Session["UserName"] = value; }

        }

        public int TimeZone
        {
            get { return Convert.ToInt32(Session["TimeZone"]); }
            set { Session["UserName"] = value; }
        }

        public string AccessType
        {
            get
            {
                if (Session["AccessType"] == null)
                    return "R"; //need to check this
                else
                {
                    return Session["AccessType"].ToString();
                }
            }
            set { Session["AccessType"] = value; }
        }
        public int PeopleId 
        {               
            get
            {
                if (Session["PeopleID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(Session["PeopleID"]);
            } 
            set 
            { Session["PeopleID"] = value; }
        }

        public Person Person
        {
            get
            {
                if (Session["Person"] == null)
                    return new Person();
                else
                    return (Person)Session["PeopleID"];
            }
            set
            { Session["Person"] = value; }
        }

        public int HouseId
        {
            get
            {
                if (Session["HouseID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(Session["HouseID"]);
            }
            set
            { Session["HouseID"] = value; }
        }
        public int SponsorId
        {
            get { return Convert.ToInt32(Session["SponsorID"]); }
            set { Session["SponsorID"] = value; }
        }

        public int SponsorProfileId
        {
            get { return Convert.ToInt32(Session["SponsorProfileId"]); }
            set { Session["SponsorProfileId"] = value; }
        }
        public bool TestMode { get; set; }

        public enum AppModes { AddHouseMember, AddHouseHold };
        public AppModes CurrentMode { get; set; }
        #endregion

        #region constants for form names
        public const string pageDirectoryPath = "";
        public string LoginForm
        {
            get { return "Login.aspx"; }
        }
        public string SelectSeason
        {
            get { return pageDirectoryPath + "SelectSeason.aspx"; }
        }
        public string SearchHouseholds
        {
            get { return pageDirectoryPath + "SearchHouse1.aspx"; }
        }
        public string SearchPeopleForm
        {
            get { return pageDirectoryPath + "SearchPeople1.aspx"; }
        }
        public string SearchSponsors
        {
            get { return pageDirectoryPath + "SearchSponsors1.aspx"; }
        }
        public string HouseholdForm
        {
            get { return pageDirectoryPath + "Households1.aspx"; }
        }
        public string PeopleForm
        {
            get { return pageDirectoryPath + "People1.aspx"; }
        }
        public string CoachForm
        {
            get { return pageDirectoryPath + "Coaches1.aspx"; }
        }
        public static string DivisionForm
        {
            get { return pageDirectoryPath + "Division1.aspx"; }
        }
        public string TeamForm
        {
            get { return pageDirectoryPath + "Teams.aspx"; }
        }
        public string ColorForm
        {
            get { return "Colors1.aspx"; }
        }
        public string SeasonForm
        {
            get { return pageDirectoryPath + "Seasons1.aspx"; }
        }
        public string GamesForm
        {
            get { return pageDirectoryPath + "Games1.aspx"; }
        }
        public string SponsorForm
        {
            get { return pageDirectoryPath + "Sponsors1.aspx"; }
        }
        public string AccountingForm
        {
            get { return pageDirectoryPath + "Accounting1.aspx"; }
        }
        public string SearchRegistrationPaymentsForm
        {
            get { return pageDirectoryPath + "SearchRegPay.aspx"; }
        }
        public string PaymentsForm
        {
            get {return pageDirectoryPath + "RegPayments.aspx"; }
        }
        public string BoardForm
        {
            get { return pageDirectoryPath + "Board.aspx"; }
        }
        public string UserForm
        {
            get { return pageDirectoryPath + "Users.aspx"; }
        }
        public string AnnouncementsForm
        {
            get { return pageDirectoryPath + "EmailBlast.aspx"; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["UserID"] == null)
                {
                    Response.Redirect(LoginForm);
                }
                InitializeVariables(new CSBCDbContext());
                SetHeader(); 
                ddlSeasons.Enabled = true;

            }
            else
            {
                InitializeVariables(new CSBCDbContext());
                if (Session["UserID"] != null)
                {
                    var rep = new UserRepository(new CSBCDbContext());
                    var user = rep.GetById((int)Session["UserID"]);
                }
            }
        }

        protected void InitializeVariables(CSBCDbContext context)
        {
            if (Session["CompanyID"] == null)
                Session["CompanyID"] = Convert.ToInt32(ConfigurationManager.AppSettings["CompanyId"]); 

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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the page's Season Changed event
            SeasonChanged += new EventHandler(SeasonChanged);
        }

        public event EventHandler SeasonChanged; 

        protected void SetHeader()
        {
            var rep = new UserRepository(new CSBCDbContext());
            var user = rep.GetById(UserId);
            lblUser1.Text = "Welcome " + user.Name;
            var title = "No Page Selected";
            if (Session["Title"] != null)
            {
                title = Session["Title"].ToString();
            }
            lblTitle.Text = title;
            LoadSeasons();
            ddlSeasons.SelectedValue = SeasonId.ToString();
        }
        protected void LoadSeasons()
        {
            using (var context = new CSBCDbContext())
            {
                var rep = new SeasonRepository(context);
                var seasons = rep.GetSeasons(CompanyId).OrderByDescending(s => s.FromDate).ToList();
                ddlSeasons.DataValueField = "SeasonID";
                ddlSeasons.DataTextField = "Description";
                ddlSeasons.DataSource = seasons;
                ddlSeasons.DataBind();
            }
        }

        protected void ddlSeasons_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SeasonId = Convert.ToInt32(ddlSeasons.SelectedValue);
            OnSeasonChanged(EventArgs.Empty);    
        }

        protected void OnSeasonChanged(EventArgs e)
        {
            if (this.SeasonChanged != null)
            {
                this.SeasonChanged(this, e);
            }
        }
        protected void Logout()
        {
            Session.Clear();
            Response.Redirect(LoginForm);
        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }
    }

}