using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using CSBC.Components;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class Teams : BaseForm
    {
        public string SQL;
        public int SelectedTeam
        {
            get
            {
                if (Session["TeamID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(Session["TeamID"]);
            }
            set
            {
                Session["TeamID"] = value;
            }
        }
        public int DivisionId
        {
            get
            {
                return Convert.ToInt32(cmbDivisions.SelectedValue);
            }
            set
            {
                Master.DivisionId = value;
                cmbDivisions.SelectedValue = value.ToString();
            }
        }

        public int TeamId
        {
            get
            {
                if (Session["TeamID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(Session["TeamID"]);
            }
            set
            {
                Session["TeamID"] = value;
            }
        }
        protected override void Page_Load(System.Object sender, System.EventArgs e)
        {

            Session["Title"] = "Teams";
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                SetUser();

                PopulateDropDownLists();

                //if Team is not defined clear fields, otherwise load the team info
                if (TeamId == 0)
                {
                    ClearFields();
                    if (!(String.IsNullOrEmpty((string)Session["FirstLetter"]) & ("TeamName" == (string)Session["SearchType"])))
                        txtName.Text = (string)Session["FirstLetter"];
                }
                else
                {
                    LoadRow(TeamId);
                    LoadTeamPlayers(TeamId);
                }
                if (!(String.IsNullOrEmpty(cmbDivisions.SelectedValue)))
                {
                    LoadUndrafted((int)Int32.Parse(cmbDivisions.SelectedValue));
                    LoadTeams((int)Int32.Parse(cmbDivisions.SelectedValue));
                    //lnkTeams.Text = cmbDivisions.SelectedItem.Text;
                }
                if (cmbDivisions.SelectedValue != DivisionId.ToString())
                {
                    DivisionId = Convert.ToInt32(cmbDivisions.SelectedValue);
                    Session["TeamID"] = 0;
                    //grdPlayers.Clear();
                    grdPlayers.Columns.Clear();
                    ClearFields();
                    LoadUndrafted((int)Int32.Parse(cmbDivisions.SelectedValue));
                    LoadTeams((int)Int32.Parse(cmbDivisions.SelectedValue));
                    //lnkTeams.Text = cmbDivisions.SelectedItem.Text;
                }
            }

        }

        private void PopulateDropDownLists()
        {
            LoadDivisions(0, Master.CompanyId, Master.SeasonId);
            Master.DivisionId = Convert.ToInt32(cmbDivisions.SelectedValue);
            LoadCoaches();
            LoadSponsors();
            LoadColors();
        }
        protected override void SetUser()
        {
            base.SetUser();
            if (Master.AccessType == "R")
            {
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void LoadRow(int TeamID)
        {
            lblColors.Text = "";
            var rep = new TeamRepository(new CSBCDbContext());

            try
            {
                var team = rep.GetById(TeamID);
                if (team != null)
                {
                    txtName.Text = team.TeamName;
                    int teamNo = Int32.Parse(team.TeamNumber);
                    if (teamNo > 0)
                    {
                        txtTeamNumber.Text = team.TeamNumber;
                    }
                    else
                    {
                        txtTeamNumber.Text = "";
                    }
                    cmbColors.SelectedValue = team.TeamColorID.ToString();
                    Session["DivisionID"] = team.DivisionID;
                    Session["CoachID"] = team.CoachID;
                    cmbAsstCoach.SelectedValue = team.AssCoachID.ToString();
                    lblCHPhone.Text = "(h) 954-444-44003";
                    lblCCPhone.Text = "(C) 209-480-2838";
                    lblCAsstPhone.Text = "(C) 305-480-2838";
                    lblHAsstPhone.Text = "(h) 561-480-2838";
                    if (!(String.IsNullOrEmpty(team.Coach.CoachPhone)))
                    {
                        lblCHPhone.Text = "H-" + (string)team.Coach.CoachPhone;
                    }
                    if (!(String.IsNullOrEmpty((string)team.Coach.CoachPhone)))
                    {

                        lblCCPhone.Text = "C-" + (string)team.Coach.CoachPhone;
                    }
                    if (!(String.IsNullOrEmpty((string)team.AsstCoach.CoachPhone)))
                    {
                        lblHAsstPhone.Text = "H-" + (string)team.AsstCoach.CoachPhone;
                    }
                    if (!(String.IsNullOrEmpty((string)team.AsstCoach.CoachPhone)))
                    {
                        lblCAsstPhone.Text = "C-" + (string)team.AsstCoach.CoachPhone;
                    }
                    if (String.IsNullOrEmpty(team.CoachID.ToString()))
                        Session["CoachID"] = 0;
                    Session["SponsorID"] = team.SponsorID;
                    if (String.IsNullOrEmpty(team.SponsorID.ToString()))
                        Session["SponsorID"] = 0;
                    if (team.TeamName == "REFUNDS")
                    {
                        btnDelete.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                        btnSave.Enabled = true;
                    }
                    Session.Add("LinkName", txtName.Text);
                    cmbDivisions.SelectedValue = (string)Session["DivisionID"];
                    cmbCoach.SelectedValue = (string)Session["CoachID"];
                    if (String.IsNullOrEmpty(team.SponsorID.ToString()))
                    {
                        cmbSponsors.SelectedValue = "0";
                    }
                    else
                    {
                        cmbSponsors.SelectedValue = (string)Session["SponsorID"];
                    }
                    lblDeleteTeam.Text = "";
                    lblDeleteTeam.Visible = false;
                    SponsorColors((int)Session["SponsorID"]);
                    lblTeamId.Value = TeamID.ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadRow::" + ex.Message;
            }
        }

        private void LoadDivisions(int division, int companyId, int seasonId)
        {
            var divisions = new DivisionRepository(new CSBCDbContext());
            try
            {
                var rsData = divisions.LoadDivisions(seasonId);
                var count = rsData.Count<vw_Divisions>();
                cmbDivisions.DataSource = rsData;
                cmbDivisions.DataValueField = "DivisionID";
                cmbDivisions.DataTextField = "Div_Desc";
                cmbDivisions.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadDivisions::" + ex.Message;
            }
            finally
            {
                //if (Session["DivisionID"] != null)
                //    cmbDivisions.SelectedValue = Session["DivisionID"].ToString();
            }
        }

        public void AddToTeam(Object sender, EventArgs e)
        {
            var x = sender.ToString();
        }
        private void LoadCoaches()
        {

            try
            {
                using (var db = new CSBCDbContext())
                {
                    var rep = new CoachRepository(db);
                    var rsData = rep.GetSeasonCoaches(Master.SeasonId);
                    cmbCoach.DataSource = rsData;
                    cmbCoach.DataValueField = "CoachID";
                    cmbCoach.DataTextField = "Name";
                    cmbCoach.DataBind();
                    cmbCoach.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    cmbCoach.SelectedIndex = 0;

                    cmbAsstCoach.DataSource = rsData;
                    cmbAsstCoach.DataValueField = "CoachID";
                    cmbAsstCoach.DataTextField = "Name";
                    cmbAsstCoach.DataBind();
                    cmbAsstCoach.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    cmbAsstCoach.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadCoaches::" + ex.Message;
            }

        }

        private void LoadSponsors()
        {
            using (var db = new CSBCDbContext())
            {
                try
                {
                    var rep = new SponsorVM();
                    var sponsors = rep.GetSeasonSponsors(Master.SeasonId);
                    cmbSponsors.DataSource = sponsors;
                    cmbSponsors.DataValueField = "SponsorId";
                    cmbSponsors.DataTextField = "SponsorName";
                    cmbSponsors.DataBind();
                    cmbSponsors.Items.Insert(0, (new ListItem(String.Empty)));
                }
                catch (Exception ex)
                {
                    lblError.Text = "LoadSponsors::" + ex.Message;
                }
            }
        }

        private void LoadColors()
        {
            try
            {
                var rep = new ColorRepository(new CSBCDbContext());
                var colors = rep.GetAll((int)Session["CompanyID"]).ToList();
                colors.Add(new Color());

                var _with5 = cmbColors;
                _with5.DataSource = colors;
                _with5.DataValueField = "ID";
                _with5.DataTextField = "ColorName";
                _with5.DataBind();
                cmbColors.Items.Insert(0, (new ListItem(String.Empty)));
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadColors::" + ex.Message;
            }
            finally
            {

            }
        }

        private void SponsorColors(Int32 SponsorID)
        {
            var oColors = new CSBC.Components.Season.clsSponsors();
            DataTable rsData = default(DataTable);
            lblColors.Text = "";
            try
            {
                rsData = oColors.SponsorsColor(SponsorID, (int)Session["CompanyID"], (int)Session["SeasonID"]);
                if (rsData.Rows.Count > 0)
                {
                    DataRow row = rsData.Rows[0];
                    lblColors.Text = (string)row["Color1Name"];
                    if (lblColors.Text != "" & (string)row["Color2Name"] != "")
                        lblColors.Text = lblColors.Text + ", ";
                    lblColors.Text = lblColors.Text + (string)row["Color2Name"];
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "SponsorColors::" + ex.Message;
            }
            finally
            {
                oColors = null;
            }

        }

        private void LoadUndrafted(int divisionId)
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            try
            {
                var undrafted = rep.GetUndrafterPlayers(divisionId);
                grdUndraftedPlayers.DataSource = undrafted;
                grdUndraftedPlayers.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadUndrafted::" + ex.Message;
            }
        }

        private void LoadTeamPlayers(int teamId)
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var players = rep.GetTeamPlayers(teamId);
            try
            {
                grdPlayers.DataSource = players;
                grdPlayers.DataBind();
                if (players.Any())
                {
                    lblRecordCount.Text = players.Count<SeasonPlayer>().ToString() + " players";
                    lblRecordCount.CssClass = "text-info";
                }
                else
                {
                    lblRecordCount.Text = "No players assigned to team!";
                    lblRecordCount.CssClass = "text-warning";
                }
                btnDelete.Enabled = !(players.Any());
            }
            catch (Exception ex)
            {
                lblError.Text = "TeamPlayers::" + ex.Message;
            }
        }

        private void LoadTeams(int divisionId)
        {
            try
            {
                var repTeams = new TeamVM();
                var teams = repTeams.GetDivisionTeams(divisionId);
                grdTeams.DataSource = teams;
                grdTeams.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadTeams::" + ex.Message;
            }
        }

        private void UpdRow(long TeamID)
        {

            var oTeam = new CSBC.Components.Season.ClsTeams();
            try
            {
                var _with12 = oTeam;
                _with12.TeamName = txtName.Text;
                _with12.TeamNumber = txtTeamNumber.Text;
                _with12.TeamColorID = Int32.Parse(cmbColors.SelectedValue);
                _with12.CoachID = Int32.Parse(cmbCoach.SelectedValue);
                _with12.DivisionID = Int32.Parse(cmbDivisions.SelectedValue);
                _with12.CoCoachID = Int32.Parse(cmbAsstCoach.SelectedValue);
                _with12.SponsorID = Int32.Parse(cmbSponsors.SelectedValue);             ///put this back
                _with12.CreatedUser = (string)Session["UserName"];
                _with12.SeasonID = Master.SeasonId;
                oTeam.UpdRow(TeamID, Master.CompanyId, 0);                      //send time zone info....
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "UpdRow::" + ex.Message;
            }
            finally
            {
                oTeam = null;
            }
        }

        private void ChangeTeam(int teamId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new TeamRepository(db);
                var team = rep.GetById(teamId);
                if (team != null)
                {
                    txtName.Text = team.TeamName;
                    TeamId = team.TeamID;
                    txtTeamNumber.Text = team.TeamNumber.ToString();
                    cmbColors.SelectedValue = team.TeamColorID.ToString();
                    if ((team.CoachID != null) && (team.CoachID != 0))
                    {
                        cmbCoach.SelectedValue = team.CoachID.ToString();
                        UpdateCoachPhone();
                    }
                    else
                    {
                        cmbCoach.ClearSelection();
                    }
                    if (team.AssCoachID != 0)
                        cmbAsstCoach.SelectedValue = team.AssCoachID.ToString();
                    else
                        cmbAsstCoach.SelectedValue = String.Empty;

                    if (team.SponsorID != 0)
                    {
                        if (cmbSponsors.Items.FindByValue(team.SponsorID.ToString()) != null)
                            cmbSponsors.SelectedValue = team.SponsorID.ToString();
                    }
                    else
                        cmbSponsors.SelectedValue = String.Empty;
                }
            }
            LoadTeamPlayers(SelectedTeam);
        }
        protected void ClearFields()
        {
            grdPlayers.Controls.Clear();
            txtName.Text = "";
            txtTeamNumber.Text = "";
            cmbColors.SelectedValue = "0";
            Session["CoachID"] = "0";
            TeamId = 0;
            cmbCoach.SelectedIndex = 0;
            cmbAsstCoach.SelectedIndex = 0;
            cmbSponsors.SelectedIndex = 0;
            lblColors.Text = "";
            lblCHPhone.Text = "";
            lblCCPhone.Text = "";
            lblHAsstPhone.Text = "";
            lblCAsstPhone.Text = "";
            Session["SponsorID"] = 0;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            lblError.Text = "";
        }

        private void AddRow()
        {
            if ((string)Session["AccessType"] == "R")
                return;
            var team = new CSBC.Components.Season.ClsTeams();
            try
            {
                team.SeasonID = (int)Session["SeasonID"];
                team.DivisionID = Int32.Parse(cmbDivisions.SelectedValue);
                team.CoachID = Int32.Parse(cmbCoach.SelectedValue);
                team.CoCoachID = Int32.Parse(cmbAsstCoach.SelectedValue);
                team.SponsorID = Int32.Parse(cmbSponsors.SelectedValue);
                team.TeamName = txtName.Text;
                team.TeamColorID = Int32.Parse(cmbColors.SelectedValue);
                team.TeamNumber = txtTeamNumber.Text;
                team.CreatedUser = (string)Session["UserName"];
                team.UpdRow(0, (int)Session["CompanyID"], (int)Session["TimeZone"]);
                Session["TeamId"] = team.TeamId;
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = ex.Message;
            }
            finally
            {
                team = null;
            }
        }

        private bool errorRTN()
        {
            bool functionReturnValue = false;
            functionReturnValue = false;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblError.Text = "Name missing";
                functionReturnValue = true;
                txtName.Focus();
            }
            else
            {
                int x;
                if (!(Int32.TryParse(txtTeamNumber.Text, out x)))
                {
                    lblError.Text = "Team Number missing or incorrect";
                    functionReturnValue = true;
                    txtTeamNumber.Focus();
                }
                if (functionReturnValue == true)
                    Response.End();
            }
            return functionReturnValue;
        }

        private void UpdPlayer(long PeopleID, long TeamID)
        {
            var oPlayers = new CSBC.Components.Season.ClsPlayers();
            try
            {
                //oPlayers.UpdatePlayer(PeopleID, (int)Session["CompanyID"], (int)Session["SeasonID"], TeamID);
                oPlayers.UpdatePlayer((int)PeopleID, (int)Session["CompanyID"], (int)Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                lblError.Text = "UpdPlayer::" + ex.Message;
            }
            finally
            {
                oPlayers = null;
            }

        }

        protected void btnDelete_Click(System.Object sender, System.EventArgs e)
        {
            //1) Put a label control beneath your button but sets its Visible property to false.  Name it lblConfirm.
            //2) Set the CommandArgument property of your button to "Confirm" (u can use any string, but "Confirm" will do for this example).
            //3) In your server-side click event, do this:
            //if ((string)Session["USERACCESS"] == "R")
            //    return;
            Button btn = (Button)sender;
            if (string.IsNullOrEmpty(lblDeleteTeam.Text))
                btn.CommandArgument = "Confirm";
            if (btn.CommandArgument == "Confirm")
            {
                lblDeleteTeam.Text = "*Click Delete button again to confirm.*";
                lblError.Text = lblDeleteTeam.Text;
                lblDeleteTeam.Visible = true;
                btn.CommandArgument = "Delete";
            }
            else if (btn.CommandArgument == "Delete")
            {
                if ((int)Session["TeamID"] > 0)
                {
                    DELRow((int)Session["TeamID"]);
                    grdPlayers.Controls.Clear();
                    Session["TeamID"] = 0;
                    LoadTeams((int)Int32.Parse(cmbDivisions.SelectedValue));
                    ClearFields();
                    //grdPlayers.Rows.Clear();
                    if (Master.DivisionId != 0)
                        LoadUndrafted(Master.DivisionId);
                }
                lblError.Text = "";
                lblDeleteTeam.Text = "";
                lblDeleteTeam.Visible = false;
                btn.CommandArgument = "Confirm";
                LoadTeams(Master.DivisionId);
                ClearFields();

            }

            //You are using the label as a message box of sorts and the CommandArgument of the button to tell the click event whether 
            //you need to confirm or have confirmed.
        }

        private void DELRow(int teamId)
        {
            //var oTeam = new CSBC.Components.Season.ClsTeams();
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var rep = new TeamRepository(db);
                    var deleted = rep.DeleteById(teamId);
                }
                //. oTeam.DELRow(TeamID, Session["CompanyID"], Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                Session["ErrorMsg"] = ex.Message;
            }

        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            if ((int)Session["TeamID"] == 0)
            {
                lblError.Text = "No team selected, a team must be added before drafting players";
                return;
                //    If errorRTN() = False Then Call UpdRow(Session["TeamID"])
                //Else
                //    If errorRTN() = False Then Call AddRow()
            }
            /*
                        for (idx = 0; idx <= grdUndrafted.DisplayLayout.SelectedRows.Count - 1; idx++)
                        {
                            Session["PeopleID"] = grdUndrafted.DisplayLayout.SelectedRows(idx).Cells.FromKey("PeopleID").Value;
                            UpdPlayer(Session["PeopleID"], Session["TeamID"]);
                        }
                        LoadUndrafted(Session["DivisionID"]);
                        TeamPlayers(Session["TeamID"]);
             * */
        }

        protected void lnkTeams_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("Divisions.aspx");
        }

        protected void imgTeam_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Session["FirstLetter"] = txtName.Text;
            Session["SearchType"] = "TeamName";
            Response.Redirect("SearchTeam.aspx");
        }

        protected void btnNew_Click(object sender, System.EventArgs e)
        {
            TeamId = 0;
            lblTeamId.Value = String.Empty;
            cmbCoach.SelectedValue = String.Empty;
            cmbAsstCoach.SelectedValue = String.Empty;
            cmbSponsors.SelectedValue = String.Empty;
            ClearFields();
            LoadTeams(Master.DivisionId);
            //LoadTeamPlayers(Convert.ToInt32(lblTeamId.Value));
            txtName.Focus();
        }


        private void cmbSponsors_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            SponsorColors(Int32.Parse(cmbSponsors.SelectedValue));
            Session["SponsorID"] = cmbSponsors.SelectedItem.Value;
        }

        protected void cmbCoach_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            lblCHPhone.Text = "";
            lblCCPhone.Text = "";
            UpdateCoachPhone();
        }

        private void UpdateCoachPhone()
        {
            if (cmbCoach.SelectedItem.Value != "")
            {
                Session["CoachID"] = cmbCoach.SelectedItem.Value;
                var coachId = Convert.ToInt32(cmbCoach.SelectedItem.Value);
                if (!(String.IsNullOrEmpty((string)Session["CoachID"])))
                {
                    var rep = new CoachRepository(new CSBCDbContext());
                    var coach = rep.GetCoach(coachId);
                    try
                    {
                        if (coach != null)
                        {
                            if (coach.CoachPhone.Length > 0)
                                lblCHPhone.Text = "(H) " + coach.CoachPhone;
                            else
                                lblCHPhone.Text = "";
                            if (coach.Cellphone.Length > 0)
                                lblCCPhone.Text = "(C) " + coach.Cellphone;
                            else
                                lblCHPhone.Text = "";

                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "cmbCoach::" + ex.Message;
                    }
                }
            }
        }
        private static string GetPhone(string phone, string prefix)
        {
            string value = "";
            if (!(String.IsNullOrEmpty(phone)))
            {
                value = prefix + "-" + phone;
            }
            return value;
        }

        protected void cmbAsstCoach_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            lblCAsstPhone.Text = "";
            lblHAsstPhone.Text = "";
            Session["AsstCoachID"] = cmbCoach.SelectedItem.Value;

            var coachId = Convert.ToInt32(cmbAsstCoach.SelectedItem.Value);
            if (!(String.IsNullOrEmpty((string)Session["AsstCoachID"])))
            {
                var rep = new CoachRepository(new CSBCDbContext());
                var coach = rep.GetCoach(coachId);
                try
                {
                    if (coach != null)
                    {
                        if (coach.CoachPhone.Length > 0)
                            lblHAsstPhone.Text = "(H) " + coach.CoachPhone;
                        else
                            lblHAsstPhone.Text = "";
                        if (coach.Cellphone.Length > 0)
                            lblCAsstPhone.Text = "(C) " + coach.Cellphone;
                        else
                            lblCAsstPhone.Text = "";

                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "cmbAsstCoach::" + ex.Message;
                }
            }
        }

        protected void grdUndraftedPlayers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "AddToTeam")
            {
                if (SelectedTeam == 0)
                {
                    MasterVM.MsgBox(this, "No Team Selected! Please select a team");
                }
                else
                {
                    try
                    {
                        var playerId = index;
                        using (var db = new CSBCDbContext())
                        {
                            var rep = new PlayerRepository(db);
                            var player = rep.GetById(playerId);
                            player.TeamID = SelectedTeam;
                            rep.Update(player);
                        }

                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "cmbCoach::" + ex.Message;
                    }
                }
            }
            if (e.CommandName == "RemoveFromTeam")
            {
                if (SelectedTeam == 0)
                {
                    MasterVM.MsgBox(this, "No Team Selected! Please select a team");
                }
                else
                {
                    try
                    {
                        var playerId = index;
                        using (var db = new CSBCDbContext())
                        {
                            var rep = new PlayerRepository(db);
                            var player = rep.GetById(playerId);
                            player.TeamID = 0;
                            rep.Update(player);
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "cmbCoach::" + ex.Message;
                    }

                }
            }
            LoadUndrafted(Master.DivisionId);
            LoadTeamPlayers(TeamId);
        }

        protected void grdTeams_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var teamId = index;
            ChangeTeam(teamId);
        }

        public void SaveTeam()
        {
            txtTeamNumber.Text = txtTeamNumber.Text.PadLeft(2, Convert.ToChar("0"));

            var rep = new TeamRepository(new CSBCDbContext());
            Team team;
            if (TeamId == 0)
            {
                //new team
                team = new Team();
            }
            else
            {
                //existing team
                team = rep.GetById(TeamId);
            }
            team.TeamName = txtName.Text;
            team.TeamNumber = txtTeamNumber.Text;
            team.TeamColorID = Int32.Parse(cmbColors.SelectedValue);
            if (cmbCoach.SelectedValue != "")
                team.CoachID = Int32.Parse(cmbCoach.SelectedValue);
            team.DivisionID = Int32.Parse(cmbDivisions.SelectedValue);
            Master.DivisionId = team.DivisionID;
            if (cmbAsstCoach.SelectedValue != "")
                team.AssCoachID = Int32.Parse(cmbAsstCoach.SelectedValue);
            //_with12.SponsorID = Int32.Parse(cmbSponsors.SelectedValue);    
            ///put this back
            team.CompanyID = Master.CompanyId;
            team.CreatedUser = (string)Session["UserName"];
            team.SeasonID = (int)Session["SeasonID"];
            if (team.TeamID == 0)
            {
                var newTeam = rep.Insert(team);
                //lblTeamId.Value = TeamId.ToString();
            }
            else
                rep.Update(team);
            lblError.Text = "Changes successfully completed";

            LoadTeams(Master.DivisionId);
            //LoadColors();
            //Call LoadCoaches()
            //LoadRow((long)Session["TeamID"]);
        }

        private void ChangeDivision(int divisionId)
        {
            DivisionId = divisionId; //sets in Master too
            LoadTeams(divisionId);
            LoadUndrafted(divisionId);
            ClearFields();
        }
        protected void btnSave_Click1(object sender, EventArgs e)
        {
            SaveTeam();

        }
        protected void DivisionChanged(Object sender, EventArgs e)
        {
            ChangeDivision(DivisionId);
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
            LoadDivisions(0, Master.CompanyId, Master.SeasonId);
            Master.DivisionId = Convert.ToInt32(cmbDivisions.Items[0].Value);
            LoadCoaches();
            LoadSponsors();
            ChangeDivision(DivisionId);
            //anything else?
        }

        protected void cmbSponsors_SelectedIndexChanged1(object sender, EventArgs e)
        {

            var sponsorId = Convert.ToInt32(cmbSponsors.SelectedValue);
            using (var db = new CSBCDbContext())
            {
                var rep = new SponsorRepository(db);
                var sponsor = rep.GetById(sponsorId);
                if (sponsor.Color1 != null)
                    lblColors.Text = sponsor.Color1;
            }
        }

    }
}


