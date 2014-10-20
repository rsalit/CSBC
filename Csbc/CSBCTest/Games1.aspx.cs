using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using CSBC.Components;
using CSBC.Core.Models;
using CSBC.Core.Data;
using CSBC.Core.Repositories;
using CSBC.Admin.Web.ViewModels;
using System.Text.RegularExpressions;

namespace CSBC.Admin.Web
{
    public partial class Games1 : BaseForm
    {
        private int EmailsSent;
        private string ErrorMsg;
        public int ScheduleNo
        {
            get
            {
                if (Session["ScheduleNo"] == null)
                    return 0;
                else
                {
                    return Convert.ToInt32(Session["ScheduleNo"]);
                }
            }
            set { Session["ScheduleNo"] = value; }
        }
        public DateTime ScheduleDate
        {
            get
            {
                if (Session["ScheduleNo"] == null)
                    return DateTime.Today;
                else
                {
                    return Convert.ToDateTime(Session["ScheduleDate"]);
                }
            }
            set { Session["ScheduleDate"] = value; }
        }
        public int GameNo
        {
            get
            {
                if (Session["GameNumber"] == null)
                    return 0;
                else
                {
                    return Convert.ToInt32(Session["GameNumber"]);
                }
            }
            set { Session["GameNumber"] = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "Games";
            base.Page_Load(sender, e);
            if (Page.IsPostBack == false)
            {
                txtScheduleDate.Text = DateTime.Today.ToShortDateString();
                //Format(Now(), "d/m/y")
                ScheduleDate = DateTime.Today;
                //this.Calendar1.TodaysDate = DateTime.Now;
                //this.Calendar1.TodayDayStyle.BackColor = System.Drawing.Color.LightGray;

                LoadCombos();
                cmbDivisions.SelectedIndex = 0;
                ScheduleNo = ScheduleGamesVM.GetScheduleNumber(Convert.ToInt32(cmbDivisions.SelectedValue));
                radioRegularorPlayoff.SelectedIndex = 0;
                panelPlayoffGrid.Visible = false;
                panelRegularGamesGrid.Visible = true;
                LoadRegularSeasonGames();
            }
        }

        protected override void SetUser()
        {
            base.SetUser();
            if (Master.AccessType == "R")
            {
                btnSave.Enabled = false;
                btnNew.Enabled = false;
                //btnSend.Enabled = false;
            }
        }

        private void UpdateRow(int gameNo)
        {
            var scheduleNo = ScheduleGamesVM.GetScheduleNumber(Convert.ToInt32(ddlDivisions.SelectedValue));
            if (radioRegularorPlayoff.SelectedIndex == 0)
                UpdateRegularSeasonGame(scheduleNo, gameNo);
            else
                UpdatePlayoffGame(scheduleNo, gameNo);

        }

        private void UpdatePlayoffGame(int scheduleNo, int gameNo)
        {

            try
            {
                if (gameNo > 0)
                {
                    var vm = new ScheduleGamesVM();
                    var game = vm.GetPlayoffByScheduleAndGameNo(scheduleNo, gameNo);
                    if (game != null)
                    {
                        game.GameDate = ConcatDateTime(mskDate.Text, txtTime.Text);
                        game.GameTime = txtTime.Text;
                        game.LocationNumber = Convert.ToInt32(cmbVenues.SelectedItem.Value);
                        game.HomeTeam = txtHome.Text;
                        game.VisitingTeam = txtVisitor.Text;
                        game.Descr = txtDescr.Text;
                        game.DivisionId = Convert.ToInt32(ddlDivisions.SelectedValue);
                        using (var db = new CSBCDbContext())
                        {
                            var rep = new SchedulePlayoffRepository(db);
                            rep.Update(game);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    var date = ConcatDateTime(mskDate.Text, txtTime.Text);
                    
                    ScheduleGamesVM.AddPlayoffGame(scheduleNo, date, txtTime.Text,
                        Convert.ToInt32(cmbVenues.SelectedItem.Value), txtHome.Text, txtVisitor.Text, txtDescr.Text,Convert.ToInt32(cmbDivisions.SelectedValue));
                }

            }
            catch (Exception e)
            {
                lblError.Text = "Update Regular Season Game:" + e.Message;
            }

        }

        private void UpdateRegularSeasonGame(int scheduleNo, int gameNo)
        {
            var vm = new ScheduleGamesVM();
            var game = vm.GetByScheduleAndGameNo(scheduleNo, gameNo);

            try
            {
                game.GameDate = (DateTime)ConcatDateTime(mskDate.Text, txtTime.Text);
                game.GameTime = txtTime.Text;
                game.LocationNumber = Convert.ToInt32(cmbVenues.SelectedItem.Value);
                game.HomeTeamNumber = (int)ScheduleGamesVM.GetScheduleTeamNumberFromTeamNumber(scheduleNo, Convert.ToInt32(txtHome.Text));
                game.VisitingTeamNumber = (int)ScheduleGamesVM.GetScheduleTeamNumberFromTeamNumber(scheduleNo, Convert.ToInt32(txtVisitor.Text));

                using (var db = new CSBCDbContext())
                {
                    new ScheduleGameRepository(db).Update(game);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                lblError.Text = "Update Regular Season Game:" + e.Message;
            }

        }

        private void ClearFields()
        {
            //cmbDivisions.SelectedIndex = 0;
            //lblPlayoff.Visible = false;
            //mskDate.Text = "";
            txtTime.Text = "";
            txtHome.Text = "";
            txtVisitor.Text = "";
            cmbVenues.SelectedIndex = 0;
            lblError.Text = "";
            txtDescr.Text = "";
        }


        private void AddRow()
        {
            var scheduleNo = ScheduleGamesVM.GetScheduleNumber(Convert.ToInt32(ddlDivisions.SelectedValue));
            if (radioRegularorPlayoff.SelectedIndex == 0)
                AddRegularSeasonGame(scheduleNo);
            else
                AddPlayoffGame(scheduleNo);
        }

        private void AddRegularSeasonGame(int scheduleNo)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                var game = new ScheduleGame();
                try
                {
                    game.ScheduleNumber = scheduleNo;
                    game.GameDate = (DateTime)ConcatDateTime(mskDate.Text, txtTime.Text);
                    game.GameTime = txtTime.Text;
                    game.HomeTeamNumber = ScheduleGamesVM.GetScheduleTeamNumberFromTeamNumber(ScheduleNo, Convert.ToInt32(txtHome.Text));
                    game.VisitingTeamNumber = ScheduleGamesVM.GetScheduleTeamNumberFromTeamNumber(ScheduleNo, Convert.ToInt32(txtVisitor.Text));
                    game.LocationNumber = Convert.ToInt32(cmbVenues.SelectedItem.Value);
                    //_with1.Descr = txtDescr.Text;
                    var newGame = rep.Insert(game);
                    db.SaveChanges(); //this should be in UOM!
                    GameNo = newGame.GameNumber;
                }
                catch (Exception ex)
                {
                    Session["ErrorMSG"] = "ADDRow::" + ex.Message;
                }
            }

        }
        private void AddPlayoffGame(int scheduleNo)
        {

            using (var db = new CSBCDbContext())
            {
                var rep = new SchedulePlayoffRepository(db);
                var game = new SchedulePlayoff();
                try
                {
                    game.ScheduleNumber = scheduleNo;
                    game.GameDate = (DateTime)ConcatDateTime(mskDate.Text, txtTime.Text);
                    game.GameTime = txtTime.Text;
                    game.HomeTeam = txtHome.Text;
                    game.VisitingTeam = txtVisitor.Text;
                    game.LocationNumber = Convert.ToInt32(cmbVenues.SelectedItem.Value);
                    game.Descr = txtDescr.Text;
                    game.DivisionId = Convert.ToInt32(ddlDivisions.SelectedValue);
                    var newGame = rep.Insert(game);
                    db.SaveChanges(); //this should be in UOM!
                    GameNo = newGame.GameNumber;
                }
                catch (Exception ex)
                {
                    Session["ErrorMSG"] = "ADDRow::" + ex.Message;
                }
            }

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateRecord() == true)
            {
                MasterVM.MsgBox(this, "ERROR: " + lblError.Text);
                return;
            }
            else
            {
                if (GameNo > 0)
                {
                    UpdateRow(GameNo);
                    MasterVM.MsgBox(this, "Changes successfully completed", MasterVM.MessageTypes.Success);
                }
                else
                {
                    AddRow();
                    MasterVM.MsgBox(this, "New Record Added Successfully", MasterVM.MessageTypes.Success);
                }
                LoadGames();
            }
        }

        private bool ValidateRecord()
        {
            bool invalidRecord = true;

            if (string.IsNullOrEmpty(mskDate.Text))
            {
                lblError.Text = "Date missing ";
                mskDate.Focus();
                return invalidRecord;
            }
            DateTime date;
            var validDate = DateTime.TryParse(mskDate.Text, out date);
            if (!validDate)
            {
                lblError.Text = "Date is invalid ";
                mskDate.Focus();
                return invalidRecord;
            }

            if (string.IsNullOrEmpty(txtTime.Text))
            {
                lblError.Text = "Time missing";
                txtTime.Focus();
                return invalidRecord;
            }

            if (ConcatDateTime(mskDate.Text, txtTime.Text) == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                lblError.Text = "Time is invalid ";
                mskDate.Focus();
                return invalidRecord;
            }

            if (String.IsNullOrEmpty(txtHome.Text))
            {
                lblError.Text = "Home Team must be filled in";
                txtHome.Focus();
                return invalidRecord;
            }

            if (String.IsNullOrEmpty(txtVisitor.Text))
            {
                lblError.Text = "Visitor Team must be filled in";
                txtVisitor.Focus();
                return invalidRecord;
            }

            if (txtHome.Text == txtVisitor.Text)
            {
                lblError.Text = "Home Team and Visitor Team must be different";
                txtHome.Focus();
                return invalidRecord;
            }
            //if (cmbVenues.SelectedIndex == 0)
            //{
            //    lblError.Text = "Location missing";
            //    functionReturnValue = true;
            //    cmbVenues.Focus();
            //}
            //if (functionReturnValue == true)
            //    Response.End();
            invalidRecord = false;
            return invalidRecord;
        }
        private DateTime? ConcatDateTime(string sDate, string sTime)
        {
            DateTime date;
            var fullDate = sDate + " " + sTime;
            var validTime = DateTime.TryParse(fullDate, out date);
            return date;
        }

        private void btnDelete_Click(System.Object sender, System.EventArgs e)
        {
            //1) Put a label control beneath your button but sets its Visible property to false.  Name it lblConfirm.
            //2) Set the CommandArgument property of your button to "Confirm" (u can use any string, but "Confirm" will do for this example).
            //3) In your server-side click event, do this:

            Button btn = (Button)sender;
            if (string.IsNullOrEmpty(lblDeleteDate.Text))
                btn.CommandArgument = "Confirm";
            if (btn.CommandArgument == "Confirm")
            {
                lblDeleteDate.Text = "*Click Delete button again to confirm.*";
                lblDeleteDate.Visible = true;
                btn.CommandArgument = "Delete";
            }
            else if (btn.CommandArgument == "Delete")
            {
                if (ScheduleNo > 0)
                {
                    DeleteRow(ScheduleNo, GameNo);
                    ClearFields();
                    LoadRegularSeasonGames();
                    ScheduleNo = 0;
                }
                lblDeleteDate.Text = "";
                lblDeleteDate.Visible = false;
                btn.CommandArgument = "Confirm";
                //cmbDivisions.Enabled = false;
            }

            //You are using the label as a message box of sorts and the CommandArgument of the button to tell the click event whether 
            //you need to confirm or have confirmed.
        }

        private void DeleteRow(int ScheduleNumber, int GameNumber)
        {
            var oGames = new CSBC.Components.Season.clsGames();
            try
            {
                oGames.DELRow((short)ScheduleNumber, (short)GameNumber);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                oGames = null;
            }
        }
        #region Lookups
        private void LoadCombos()
        {
            LoadVenues();
            LoadDivisions();
            LoadRegularSeasonGames();
            if (ScheduleNo > 0)
                LoadTeams();
            //LoadEmails(0);
        }

        private void LoadVenues()
        {
            var rep = new ScheduleLocationRepository(new CSBCDbContext());

            try
            {
                var venues = rep.GetAll().ToList<ScheduleLocation>();
                cmbVenues.DataSource = venues;
                cmbVenues.DataValueField = "LocationNumber";
                cmbVenues.DataTextField = "LocationName";
                cmbVenues.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadVenues::" + ex.Message;
            }

        }

        private void LoadDivisions()
        {
            var rep = new DivisionRepository(new CSBCDbContext());

            try
            {
                var divisions = rep.GetDivisions(Master.SeasonId).ToList<Division>();
                cmbDivisions.DataSource = divisions;
                cmbDivisions.DataValueField = "DivisionID";
                cmbDivisions.DataTextField = "Div_Desc";
                cmbDivisions.DataBind();

                ddlDivisions.DataSource = divisions;
                ddlDivisions.DataValueField = "DivisionID";
                ddlDivisions.DataTextField = "Div_Desc";
                ddlDivisions.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadDivisions::" + ex.Message;
            }
        }

        private void LoadTeams()
        {
            var rep = new TeamRepository(new CSBCDbContext());
            // var teams = rep.GetSeasonTeams(Master.SeasonId);
            var oTeams = new CSBC.Components.Season.ClsTeams();
            DataTable rsData = default(DataTable);
            try
            {
                rsData = oTeams.LoadDivisionTeams(ScheduleNo, Master.CompanyId, Master.SeasonId, true);
                //txtHome.Text = rsData.Rows[0]["Home"].ToString();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadTeams::" + ex.Message;
            }
            finally
            {
                oTeams = null;
            }
        }
        #endregion

        private void LoadRegularSeasonGames()
        {
            try
            {
                List<ScheduleGamesVM> games;
                var vm = new ScheduleGamesVM();

                if (checkAllDates.Checked)
                {
                    games = vm.GetGames(Convert.ToInt32(cmbDivisions.SelectedValue)).ToList();
                }
                else
                {
                    var date = getDateToQuery();
                    games = vm.GetGames(Master.SeasonId, Convert.ToInt32(cmbDivisions.SelectedValue), date);
                }
                grdGames.DataSource = games.ToList<ScheduleGamesVM>();
                grdGames.DataBind();
                
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadGames:" + ex.Message;

            }
        }

        private DateTime getDateToQuery()
        {
            DateTime date;
            if (checkAllDates.Checked)
                date = DateTime.MinValue;
            else
            {
                var tryDate = DateTime.TryParse(txtScheduleDate.Text, out date);
            }
            return date;
        }

        private void LoadPlayoffGames()
        {
            try
            { 
                List<ScheduleGamesVM> games;
                var vm = new ScheduleGamesVM();
                if (checkAllDates.Checked)
                {
                    games = vm.GetPlayoffGames(Convert.ToInt32(cmbDivisions.SelectedValue));
                }
                else
                {
                    var date = getDateToQuery();
                    games = vm.GetPlayoffGames(Master.SeasonId, Convert.ToInt32(cmbDivisions.SelectedValue), date);
                }
                grdPlayoffGames.DataSource = games.ToList<ScheduleGamesVM>();
                grdPlayoffGames.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadGames:" + ex.Message;

            }
        }
        protected void Calendar1_SelectionChanged(object sender, System.EventArgs e)
        {
            ClearFields();
            DateTime date = DateTime.Today;
            var tryDate = DateTime.TryParse(txtScheduleDate.Text, out date);
            mskDate.Text = date.ToShortDateString();
            LoadGames();
        }

        protected void grdGames_DblClick(object sender, EventArgs e)
        {
            SelectGame();
        }

        protected void grdGames_SelectedRowsChange(object sender, EventArgs e)
        {
            SelectGame();
        }

        private void SelectGame()
        {
            //if (checkAllDivisions.Checked)
            //    ScheduleNo = 0;
            //else
                ScheduleNo = Convert.ToInt32(grdGames.SelectedRow.Cells[0].Text);
            GameNo = Convert.ToInt32(grdGames.SelectedRow.Cells[1].Text);
            Session["LocationNumber"] = Convert.ToInt32(grdGames.SelectedRow.Cells[2].Text);
            Session["GameType"] = grdGames.SelectedRow.Cells[0].Text;
            if (Session["GameType"].ToString() == "P")
            {
                //lblPlayoff.Visible = true;
                btnDelete.Enabled = true;
                txtHome.Enabled = true;
                txtVisitor.Enabled = true;
                txtDescr.Visible = true;
            }
            else
            {
                //lblPlayoff.Visible = false;
                btnDelete.Enabled = false;
                txtHome.Enabled = false;
                txtVisitor.Enabled = false;
                txtDescr.Visible = false;
                cmbDivisions.Enabled = false;
            }
            ClearFields();
            cmbVenues.SelectedIndex = Convert.ToInt32(grdGames.SelectedRow.Cells[8].Text);
            cmbDivisions.SelectedValue = grdGames.SelectedRow.Cells[12].Text;
            txtDescr.Text = grdGames.SelectedRow.Cells[13].Text;
            txtHome.Text = grdGames.SelectedRow.Cells[10].Text;
            if (txtDescr.Text == txtHome.Text)
                txtHome.Text = "";
            txtVisitor.Text = grdGames.SelectedRow.Cells[11].Text;
            mskDate.Text = grdGames.SelectedRow.Cells[0].Text;
            txtTime.Text = grdGames.SelectedRow.Cells[4].Text;
        }

        protected void btnNew_Click(object sender, System.EventArgs e)
        {
            ClearFields();
            //ScheduleNo = 0;
            //Session.Remove("ScheduleNo");
            GameNo = 0;
            //Session.Remove("GameNumber");
            Session.Remove("LocationNumber");
            Session.Remove("GameType");

            //cmbDivisions.Enabled = true;
            btnSave.Enabled = true;
            cmbDivisions.Focus();
        }

        protected void radioRegularorPlayoff_SelectedIndexChanged(object sender, EventArgs e)
        {
            //do something when they switch from Regular Season game to Playoff
            if (radioRegularorPlayoff.SelectedIndex == 0)
            {
                panelPlayoffGrid.Visible = false;
                panelRegularGamesGrid.Visible = true;
                txtDescr.Visible = false;
                lblDescription.Visible = false;
            }
            else
            {
                panelPlayoffGrid.Visible = true;
                panelRegularGamesGrid.Visible = false;
                txtDescr.Visible = true;
                lblDescription.Visible = true;
            }
            LoadGames();
        }

        protected void grdGames_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var gameObject = e.CommandArgument.ToString();
            var parameters = gameObject.Split(':');
            var scheduleNo = Convert.ToInt32(parameters[0]);
            var gameNo = Convert.ToInt32(parameters[1]);
            GameNo = gameNo;
            ScheduleGamesVM game;
            var vm = new ScheduleGamesVM();
            //get games
            if (e.CommandName == "SelectGame")
            {
                game = vm.GetGame(Convert.ToInt32(cmbDivisions.SelectedValue), gameNo, Master.SeasonId);
            }
            else
            {
                game = vm.GetPlayoffGame(Convert.ToInt32(cmbDivisions.SelectedValue), gameNo, Master.SeasonId);
            }
            LoadGameDetail(game);
        }

        private void LoadGameDetail(ScheduleGamesVM game)
        {
            ddlDivisions.SelectedValue = game.DivisionId.ToString();
            mskDate.Text = game.GameDate.ToShortDateString();
            txtTime.Text = game.GameTime;
            //cmbVenues.SelectedItem.Value = game.LocationNumber.ToString();
            cmbVenues.SelectedValue = game.LocationNumber.ToString();
            
            if (game.GameType == ScheduleGamesVM.GameTypes.Playoff)
            {
                txtHome.Text = game.HomeTeam;
                txtVisitor.Text = game.VisitorTeam;
                txtDescr.Text = game.Description;
            }
            else
            {
                txtHome.Text = game.HomeTeamNo.ToString();
                txtVisitor.Text = game.VisitorTeamNo.ToString();
            }
        }

        protected void cmbDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get ScheduleNumber of 
            var divisionId = Convert.ToInt32(cmbDivisions.SelectedValue);
            int scheduleNumber = ScheduleGamesVM.GetScheduleNumber(divisionId);
            ScheduleNo = scheduleNumber;
            LoadGames();

        }

        private void LoadGames()
        {
            if (radioRegularorPlayoff.SelectedIndex == 0)
            {
                LoadRegularSeasonGames();
            }
            else
            {
                LoadPlayoffGames();
            }
        }

        protected void checkAllDates_CheckedChanged(object sender, EventArgs e)
        {
            txtScheduleDate.Enabled = !(checkAllDates.Checked);
            LoadGames();

        }

        //protected void checkAllDivisions_CheckedChanged(object sender, EventArgs e)
        //{
        //    cmbDivisions.Enabled = !(checkAllDivisions.Checked);
        //    ScheduleNo = 0;
        //    LoadGames();

        //}

        protected void grdGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var x = grdGames.SelectedIndex;
        }

        protected void txtScheduleDate_TextChanged(object sender, EventArgs e)
        {
            LoadGames();
        }



        #region Email routines
        //private void btnSend_Click(System.Object sender, System.EventArgs e)
        //{

        //    SendEmails();
        //    for (Int16 I = 0; I <= lstEmails.Items.Count - 1; I++)
        //    {
        //        lstEmails.Items[I].Selected = false;
        //    }
        //}

        //private void LoadEmails(Int32 iGroupType)
        //{
        //    var oEmails = new CSBC.Components.Profile.ClsHouseholds();
        //    DataTable rsData = default(DataTable);
        //    try
        //    {
        //        rsData = oEmails.LoadEmails(iGroupType, Session["CompanyID"], Session["SeasonID"]);
        //        lstEmails.Items.Clear();
        //        if (rsData.Rows.Count > 0)
        //        {
        //            //   |||||   Set the DataValueField to the Primary Key
        //            lstEmails.DataValueField = "Email";
        //            //   |||||   Set the DataTextField to the text/data you want to display
        //            lstEmails.DataTextField = "Name";
        //            //   |||||   Set the DataSource the the OleDBDataReader's result
        //            lstEmails.DataSource = rsData;
        //            //   |||||   Bind the Source Data to the Web/Server Control
        //            lstEmails.DataBind();

        //        }
        //        for (Int16 I = 0; I <= lstEmails.Items.Count - 1; I++)
        //        {
        //            lstEmails.Items[I].Selected = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = "LoadEmails::" + ex.Message;
        //    }

        //}

        //private void SendEmails()
        //{
        //    System.Net.Mail.MailMessage oEmail = new System.Net.Mail.MailMessage();
        //    SmtpClient oSmtp = new SmtpClient("mail.csbchoops.net");
        //    string EmailBody = null;
        //    oSmtp = new SmtpClient("mail.csbchoops.net");
        //    oSmtp.Host = "mail.csbchoops.net";
        //    oSmtp.Credentials = new System.Net.NetworkCredential("registrar@csbchoops.net", "csbc0317");
        //    oSmtp.Port = 25;

        //    EmailBody = "<table border='3' WIDTH = '500'>";
        //    EmailBody +=
        //        "<tr bgcolor='#99CCFF'><th>Location Name</th><th>Division</th><th align='center'>Game Time</th><th>Teams</th></tr>";
        //    for (int idx = 0; idx <= grdGames.Rows.Count - 1; idx++)
        //    {
        //        EmailBody += "<tr><td>" + grdGames.Rows[idx].Cells[1].Text;
        //        EmailBody += "</td><td>" + grdGames.Rows[idx].Cells[2].Text;
        //        EmailBody += "</td><td>" + grdGames.Rows[idx].Cells[4].Text;
        //        EmailBody += "</td><td>" + grdGames.Rows[idx].Cells[5].Text + "</td></tr>";
        //    }
        //    EmailBody += "</table>";


        //    for (Int16 counter = 0; counter <= lstEmails.Items.Count - 1; counter++)
        //    {
        //        if (IsEmail(lstEmails.Items[counter].Value) == true & lstEmails.Items[counter].Selected == true)
        //        {
        //            oEmail = new System.Net.Mail.MailMessage();
        //            oEmail.From = new System.Net.Mail.MailAddress("registration@csbchoops.net");
        //            oEmail.To.Add(lstEmails.Items[counter].Value);
        //            oEmail.IsBodyHtml = true;
        //            oEmail.Subject = Calendar1.SelectedDate.ToShortDateString() + " Games Scheduled";
        //            oEmail.Body = EmailBody;
        //            if (GoodEmail(oSmtp, oEmail) == true)
        //                EmailsSent += 1;
        //            oEmail.Dispose();
        //            oEmail = null;
        //        }
        //    }

        //    if (EmailsSent == 0)
        //    {
        //        lblError.Text = ErrorMsg + " (0) Email(s) sent!";
        //    }
        //    else
        //    {
        //        lblError.Text = "(" + EmailsSent + ") Email(s) sent!";
        //    }
        //    //txtSubject.Text = ""
        //    //htmlMail.Text = ""
        //}

        //private bool IsEmail(string Email)
        //{
        //    bool functionReturnValue = false;
        //    string pattern =
        //        "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
        //    Match emailAddressMatch = Regex.Match(Email, pattern);
        //    if (emailAddressMatch.Success)
        //    {
        //        functionReturnValue = true;
        //    }
        //    else
        //    {
        //        functionReturnValue = false;
        //    }
        //    return functionReturnValue;
        //}

        //private bool GoodEmail(SmtpClient oSmtp, MailMessage oEmail)
        //{
        //    bool functionReturnValue = false;
        //    functionReturnValue = true;
        //    try
        //    {
        //        oSmtp.Send(oEmail);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = "Unable to send mail!  " + ex.Message;
        //        functionReturnValue = false;
        //    }
        //    return functionReturnValue;
        //}

        #endregion





    }
}