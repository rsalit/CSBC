using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Web.UI.WebControls;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Components;
using System.Linq;
using Csbchoops.Web.ViewModels;

namespace Csbchoops.Web
{

    public partial class GameSchedules : System.Web.UI.Page
    {
        public int DivisionId { get; set; }
        public int ScheduleNo { get; set; }
        protected void Page_Load(System.Object sender, System.EventArgs e)
        {

            Session["Module"] = "Games";
            if (Page.IsPostBack == false)
            {
                //Session["ReportName"] = lnkSwitch.Text '"Schedules"
                GetSeason();
                GetDivisions();
                DivisionId = Convert.ToInt32(ddlDivisions.SelectedValue);

                Session["DivStats"] = false;
                if (Session["ScheduleNo"] == null) { Session["ScheduleNo"] = 0; }
                if (Int32.Parse(Session["ScheduleNo"].ToString()) > 0)
                {
                    ddlDivisions.SelectedValue = Session["ScheduleNo"].ToString();
                }
                if (Session["ScheduleNo"] == null) { Session["ScheduleNo"] = 0; }
                GetTeams(DivisionId);
                if (ddlDivisions.Items.Count > 1)
                {
                    //LoadSchedule(DivisionId, 0);
                }

            }
            else
            {
                if (DivisionId == 0) { DivisionId = 0; }
                if (ScheduleNo == 0) { ScheduleNo = 0; }
            }
            grdSchedule.Columns[9].Visible = lblName.Visible;
        }

        private void GetSeason()
        {
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var rep = new SeasonRepository(db);
                    var currentSeason = rep.GetCurrentSeason(1);

                    Session["SeasonDesc"] = currentSeason.Description;
                    Session["SeasonID"] = currentSeason.SeasonID;

                }
            }
            catch (Exception ex)
            {
                //lblError.Text = "GetSeason:" + ex.Message;
            }
        }

        private void GetDivisions()
        {
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var rep = new DivisionRepository(db);
                    var divisions = rep.GetDivisions(Convert.ToInt32(Session["SeasonID"]));

                    var grid = ddlDivisions;
                    grid.DataSource = divisions.ToList<Division>();
                    grid.DataValueField = "DivisionID";
                    grid.DataTextField = "Div_Desc";
                    grid.Items.Add("");
                    grid.DataBind();
                    LoadSchedule(divisions.FirstOrDefault().DivisionID, 0);
                }

            }
            catch (Exception ex)
            {
                //lblError.Text = "GetDivisions:" + ex.Message;
            }
        }

        private void GetTeams(int divisionId)
        {
            try
            {
                var teams = TeamViewModel.GetDivisionTeams(divisionId);
                {
                    var grid = cobTeams;
                    grid.DataSource = teams;
                    grid.DataValueField = "TeamID";
                    grid.DataTextField = "TeamName";
                    grid.Items.Insert(0, "All Teams");
                    grid.DataBind();

                    //LoadSchedule(DivisionId, Convert.ToInt32(grid.SelectedItem));
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = "GetTeams:" + ex.Message;
            }
        }

        private void LoadSchedule(int divisionId, int teamId)
        {
            if (panelGames.Visible)
            {
                try
                {
                    if (cbAllTeams.Checked)
                        teamId = 0;
                    var vm = new GameSchedulesViewModel();
                    var games = new List<GameSchedulesViewModel>();
                    if (teamId == 0)
                    {
                        games = vm.GetGames(Convert.ToInt32(Session["SeasonID"]), divisionId);
                    }
                    else
                    {
                        games = vm.GetGames(Convert.ToInt32(Session["SeasonID"]), divisionId, teamId);
                    }

                    var grid = grdSchedule;
                    grid.DataSource = games.OrderBy(g => g.GameDate);
                    grid.DataBind();
                    if (divisionId > 0)
                    {
                        //lblTitle.Text = games..FirdivisionDescription + " Schedule";
                    }
                    //if (iTeam > 0)
                    //{
                    //    lblTitle.Text = divisionDescription + " Team " + sTeamDesc + " Schedule";
                    //}
                    Session["ScheduleNo"] = divisionId;
                    //Session["ScheduleDesc"] = divisionDescription;
                    //Session["TeamName"] = sTeamDesc;
                    Session["ReportName"] = "Schedules";
                    cobTeams.Visible = true;
                    grdSchedule.Columns[9].Visible = ((Session["Editing"] != null & Session["Editing"] == "All") ||
                        (Session["User"] != null & CheckAD(Convert.ToInt32(ddlDivisions.SelectedValue), (User)Session["User"]))
                        );
                    //btnSubmit.Visible = false;
                }
                catch (Exception ex)
                {
                    //lblError.Text = "LoadSchedule:" + ex.Message;
                }
            }

        }

        //Private Sub CreateXML(ByVal rsdata As DataTable)
        //    Dim ds As New System.Data.DataSet()
        //    Try
        //        rsdata.TableName = "DivSchedule"
        //        ds.Tables.Add(rsdata)

        //        ds.WriteXml("c:\VS2005\CSBC\DivSchedule.xml", XmlWriteMode.WriteSchema)
        //    Catch ex As Exception
        //        lblError.Text = "CreateXML:" & ex.Message
        //    Finally
        //        ds = Nothing
        //End Try
        //End Sub

        private void cobTeams_SelectedIndexChanged(System.Object sender, EventArgs e)
        {
            //LoadSchedule(ddlDivisions.SelectedItem.Text,Convert.ToInt32( ddlDivisions.SelectedItem.Value), cobTeams.SelectedItem.Text//, Convert.ToInt32(cobTeams.SelectedItem.Value));
        }

        protected void ddlDivisions_SelectedIndexChanged(System.Object sender, EventArgs e)
        {
            var divisionId = Convert.ToInt32(ddlDivisions.SelectedValue);
            GetTeams(divisionId);
            var team = 0;
            if (!cbAllTeams.Checked)
            {
                team = Convert.ToInt32(cobTeams.Items[0].Value);
            }
            LoadSchedule(divisionId, team);
            LoadStandings(divisionId);
        }


        private void LoadStandings(int iDiv)
        {
            if (panelStandings.Visible)
            {
                var vm = new ScheduleStandingsViewModel();
                var standings = vm.GetStandings(iDiv);

                grdStandings.DataSource = standings;
                //grdStandings.DataValueField = "DivisionID";
                //grdStandings.DataTextField = "Div_Desc";
                grdStandings.DataBind();
            }
        }

        private string AccessType(long Usercode, string sScreen)
        {
            //string functionReturnValue = null;
            //Security.ClsUsers oUser = new Security.ClsUsers();
            //try
            //{
            //    oUser.GetAccess(Usercode, sScreen, Session["CompanyID"]);
            //    functionReturnValue = oUser.AccessType;
            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = "AccessType:" + ex.Message;
            //    functionReturnValue = 0;
            //    return functionReturnValue;
            //}
            //finally
            //{
            //    oUser = null;
            //}
            //return functionReturnValue;
            return "R";
        }


        private void ValidateAccess()
        {
            //lblError.Text = "";
            //if (AccessType(Session["USERCODE"], "Scores") != "U" & CheckAD() == 0 & Session["HouseID"] > 0)
            //{
            //    lblError.Text = "Warning - not allow to update games scores ";
            //    Session["USERNAME"] = "";
            //    Session["Admin"] = "";
            //    //imgStandings.Visible = True
            //    if (Session["HouseID"] > 0)
            //    {
            //        lblUsername.Visible = true;
            //        txtUser.Visible = true;
            //        lblPassword.Visible = true;
            //        txtPwd.Visible = true;
            //        btnSubmit.Visible = true;
            //        lnkForgot.Visible = true;
            //        lblLocation.Visible = false;
            //        lblHome.Visible = false;
            //        lblDate.Visible = false;
            //        lblVisitor.Visible = false;
            //        btnUpdate.Visible = false;
            //        txtVScores.Visible = false;
            //        txtHScores.Visible = false;
            //        //imgStandings.Visible = False
            //        lblError.Text = lblError.Text + "SELECT YOUR DIVISION GAMES.";
            //    }
            //    else
            //    {
            //        lblUsername.Visible = false;
            //        txtUser.Visible = false;
            //        lblPassword.Visible = false;
            //        txtPwd.Visible = false;
            //        btnSubmit.Visible = false;
            //        lnkForgot.Visible = false;
            //        lblLocation.Visible = true;
            //        lblHome.Visible = true;
            //        lblDate.Visible = true;
            //        lblVisitor.Visible = true;
            //        btnUpdate.Visible = true;
            //        txtVScores.Visible = true;
            //        txtHScores.Visible = true;
            //        //imgStandings.Visible = False
            //    }
            //    Session["HouseID"] = 0;
            //}
            //else
            //{
            //    lblUsername.Visible = false;
            //    txtUser.Visible = false;
            //    lblPassword.Visible = false;
            //    txtPwd.Visible = false;
            //    btnSubmit.Visible = false;
            //    lnkForgot.Visible = false;
            //    lblLocation.Visible = true;
            //    lblHome.Visible = true;
            //    lblDate.Visible = true;
            //    lblVisitor.Visible = true;
            //    btnUpdate.Visible = true;
            //    txtVScores.Visible = true;
            //    txtHScores.Visible = true;
            //    //imgStandings.Visible = False
            //}

        }

        //Private Sub CheckForStats()
        //    If KeepStats() Then
        //        'transfer to stats
        //        Response.Redirect("GamesStats.aspx")
        //        Exit Sub
        //    End If

        //End Sub

        private bool CheckAD(int divisionId, User user)
        {
            bool functionReturnValue = false;
            using (var db = new CSBCDbContext())
            {
                var divisionRepo = new DivisionRepository(db);
                var division = divisionRepo.GetById(divisionId);
                if (division.DirectorID != 0)
                {
                    if (user.HouseID != 0)
                    {
                        var householdMembers = db.Set<Person>().Where(p => p.HouseID == user.HouseID);
                        functionReturnValue = householdMembers.Any(h => h.PeopleID == division.DirectorID);
                        lblName.Text = user.Name;
                        lblName.Visible = true;
                    }
                }
            }
            return functionReturnValue;

        }

        private void lnkTie_Click(System.Object sender, System.EventArgs e)
        {
            Response.Redirect("http://www.csbchoops.com/TieBreaker.pdf");
        }

        private void EmailReset(string Email, string Pwd)
        {
            //Create an instance of the MailMessage class
            try
            {
                System.Net.Mail.MailMessage oEmail = new System.Net.Mail.MailMessage();
                SmtpClient oSmtp = new SmtpClient("mail.csbchoops.net");

                oEmail.To.Add(Email);
                oEmail.From = new MailAddress("Registration@csbchoops.net");
                oEmail.Subject = "Password Information Requested";
                oEmail.Body = "Your password is: " + Pwd;
                //"Hi!" & vbCrLf & vbCrLf & "How are you doing?"
                oSmtp.Host = "mail.csbchoops.net";
                //Dim instance As SmtpPermission
                //instance.AddPermission(oSmtp)
                oSmtp.Credentials = new System.Net.NetworkCredential("registrar@csbchoops.net", "0317");
                //oSmtp.UseDefaultCredentials = True
                oSmtp.Send(oEmail);
                //lblError.Text = "Email sent!";
                oEmail.Dispose();
                oSmtp = null;
            }
            catch (Exception ex)
            {
                //lblError.Text = "Unable to send mail!  " + ex.Message;
            }
        }

        private bool IsEmail(string Email)
        {
            bool functionReturnValue = false;
            if (String.IsNullOrEmpty(Email))
            {
                string pattern = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
                Match emailAddressMatch = Regex.Match(Email, pattern);
                if (emailAddressMatch.Success)
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }
            }
            else
            {
                functionReturnValue = false;
            }
            return functionReturnValue;
        }


        protected void lnkSwitch_Click(object sender, System.EventArgs e)
        {
            //imgStandings.Visible = True
            if (Session["ReportName"] == "Standings")
            {
                cobTeams.Visible = true;

                //lnkTie.Visible = false;
                grdSchedule.Visible = true;
                //grdStanding.Visible = false;
                //lnkPrint.Visible = true;
            }
            else
            {
                cobTeams.Visible = false;

                //lnkTie.Visible = true;
                grdSchedule.Visible = false;
                //grdStanding.Visible = true;
                //lnkPrint.Visible = false;
            }

            //if (lnkSwitch.Text == "Standings")
            //{
            //    lnkSwitch.Text = "Schedules";
            //    lblUsername.Visible = false;
            //    txtUser.Visible = false;
            //    lblPassword.Visible = false;
            //    txtPwd.Visible = false;
            //    btnSubmit.Visible = false;
            //    lnkForgot.Visible = false;
            //    lblDate.Visible = false;
            //    lblLocation.Visible = false;
            //    lblVisitor.Visible = false;
            //    lblHome.Visible = false;
            //    txtVScores.Visible = false;
            //    txtHScores.Visible = false;
            //    btnUpdate.Visible = false;
            //}
            //else
            //{
            //    lnkSwitch.Text = "Standings";
            //    GetTeams(Convert.ToInt32(ddlDivisions.SelectedItem.Value));
            //}
            //If Session["DivStats"] = True Then
            //    lnkStats.Visible = True
            //Else
            //    lnkStats.Visible = False
            //End If
            //if (lnkSwitch.Text == "Schedules")
            //    LoadStandings(Session["ScheduleDesc"], Session["ScheduleNo"]);
            //if (lnkSwitch.Text == "Standings")
            //    LoadSchedule(ddlDivisions.SelectedItem.Text, ddlDivisions.SelectedItem.Value, cobTeams.SelectedItem.Text, cobTeams.SelectedItem.Value);
        }

        protected void lnkForgot_Click(object sender, System.EventArgs e)
        {
            string Email = null;
            string pwd = null;
            //Security.ClsUsers oUser = new Security.ClsUsers();
            try
            {
                //oUser.GetEmail(Session["CompanyID"], txtUser.Text);
                //Email = oUser.Email;
                if (!IsEmail(Email))
                {
                    // lblError.Text = "Invalid/missing Email";
                    return;
                }
                //pwd = oUser.PWord;
                //if (Email > "" & pwd > "")
                {
                    EmailReset(Email, pwd);
                }
                //else
                {
                    //lblError.Text = "Invalid/missing Username";
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = "lnkForgot:" + ex.Message;
            }

        }


        protected void lnkPrint_Click(object sender, System.EventArgs e)
        {
            //Dim strExportFile As String
            //strExportFile = Session["Report"]
            //Dim printDoc As New PrintDocument
            //Dim PrinterName As String = ""
            //Dim ReportPath As String = ""
            //Dim crReportDocument As ReportDocument = New ReportDocument

            //If PrinterSettings.InstalledPrinters.Count = 0 Then
            //    lblError.Text = "No printer installed"
            //Else
            //PrinterName = printDoc.PrinterSettings.PrinterName
            //If PrinterName = "" Then PrinterName = PrinterSettings.InstalledPrinters.Item(0)
            //switch (Session["ReportName"])
            //{
            //    case "Schedules":
            //        //ReportPath = "Reports/DivSchedule.rpt"

            //        Session["Report"] = "Reports/DivSchedule.rpt";

            //        break;
            //        Session["TeamNbr"] = cobTeams.SelectedItem.Value
            //        crReportDocument.Load(Server.MapPath(ReportPath))
            //        Dim oData As New CSBC.Components.Season.ClsSchedules
            //        'Pass the populated dataset to the report
            //        crReportDocument.SetDataSource(oData.GetGames(Session["CompanyID"], Session["SeasonID"], Session["ScheduleNo"], Session["TeamNbr"], Session["ScheduleDesc"], Session["TeamName"]))
            //        crReportDocument.SetParameterValue("SeasonDesc", Session["SeasonDesc"])
            //        crReportDocument.SetParameterValue("CompanyName", Session["CompanyName"])
            //        crReportDocument.SetParameterValue("TeamName", Session["TeamName"])
            //        crReportDocument.SetParameterValue("Division", Session["ScheduleDesc"])
            //        oData = Nothing

            //case "Standings":
            //    Session["Report"] = "Reports/DivStanding.rpt";
            //    break;
            //ReportPath = "Reports/DivStanding.rpt"
            //        crReportDocument.Load(Server.MapPath(ReportPath))
            //        Dim oData As New CSBC.Components.Season.clsGames
            //        'Pass the populated dataset to the report
            //        crReportDocument.SetDataSource(oData.GetStanding(Session["CompanyID"], Session["ScheduleNo"]))
            //        crReportDocument.SetParameterValue("SeasonDesc", Session["SeasonDesc"])
            //        crReportDocument.SetParameterValue("CompanyName", Session["CompanyName"])
            //        crReportDocument.SetParameterValue("Division", Session["ScheduleDesc"])
            //        oData = Nothing

            //Dim s As System.IO.MemoryStream = crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)
            //With HttpContext.Current.Response
            //    .ClearContent()
            //    .ClearHeaders()
            //    .ContentType = "application/pdf"
            //    .AddHeader("Content-Disposition", "inline; filename=" & strExportFile)
            //    .BinaryWrite(s.ToArray)

            //    .End()
            //End With

            //printDoc = Nothing
            //crReportDocument = Nothing

            //Server.Transfer("Report.aspx", True)
            //OJO BELOW
            //if (grdSchedule.RecordCount > 0)
            //    Response.Redirect("Report.aspx?Report=Reports/DivSchedule.rpt");

            //Dim strScript As New Label
            //strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>window.open('Report.aspx','Report','status=0','toolbar=0');</script>"
            //Page.Controls.Add(strScript)
        }

        protected void lnkStats_Click(object sender, System.EventArgs e)
        {
            Session["Report"] = "Reports/PlayersStats.rpt";

            Response.Redirect("Report.aspx");

            //Dim strScript As New Label
            //strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>window.open('Report.aspx','Report','status=0','toolbar=0');</script>"
            //Page.Controls.Add(strScript)
        }




        protected void grdSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void cobTeams_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cbAllTeams.Checked = false;
            LoadSchedule(Convert.ToInt32(ddlDivisions.SelectedValue), Convert.ToInt32(cobTeams.SelectedValue));
        }

        protected void cbAllTeams_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllTeams.Checked)
            {
                LoadSchedule(Convert.ToInt32(ddlDivisions.SelectedValue), 0);
            }
            else
            {
                LoadSchedule(Convert.ToInt32(ddlDivisions.SelectedValue), Convert.ToInt32(cobTeams.SelectedValue));
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            //UserData entity = new UserData();

            //entity.Email = txtLoginEmail.Text;
            //entity.Password = txtLoginPassword.Text;

            System.Diagnostics.Debugger.Break();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            GetUserAccess();
        }

        private void GetUserAccess()
        {
            var msg = "Invalid use name / password combination";
            var giveAccess = false;
            using (var db = new CSBCDbContext())
            {
                var rep = new UserRepository(db);
                User user = rep.GetUser(txtUserName.Text, txtPassword.Text);
                if (user != null & user.HouseID != 0)
                {
                    if (user.PassWord.ToUpper() == txtPassword.Text.ToUpper())
                    {
                        var repoRole = new RoleRepository(db);
                        var accessTypes = repoRole.GetRoles(user.UserID);
                        Session["User"] = user;
                        if (accessTypes.Any(r => r.ScreenName.ToUpper() == "SCORES"))
                        {

                            giveAccess = true;
                            Session["Editing"] = "All";

                        }
                        else
                        {
                            //check to see if they are AD
                            var divisionId = Convert.ToInt32(ddlDivisions.SelectedValue);
                            giveAccess = CheckAD(divisionId, user);
                            if (!giveAccess)
                            {
                                msg = "Use does not have right to edit scores";
                            }
                        }
                        if (giveAccess)
                        {
                            //btnEdit1.Visible = true;
                            grdSchedule.Columns[9].Visible = true;   
                        }
                        Session["UserID"] = user.UserID;
                        Session["UserName"] = user.Name;
                        Session["UserType"] = user.UserType;

                        lblName.Text = user.Name;
                        lblName.Visible = true;
                        btnLogout.Visible = true;
                        loginForm.Visible = false;
                        return;

                    }
                    else
                    {
                        msg = "Incorrect password";
                    }

                }
            }
            labelLoginError.Text = msg;
            labelLoginError.Visible = true;
        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            loginForm.Visible = true;
            btnLogin.Visible = false;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            lblName.Visible = false;
            btnLogout.Visible = false;
            btnLogin.Visible = true;
        }

        protected void grdSchedule_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (lblName.Visible)
            {
                grdSchedule.EditIndex = e.NewEditIndex;
                LoadSchedule(Convert.ToInt32(ddlDivisions.SelectedValue), Convert.ToInt32(cobTeams.SelectedValue));
            }
            else
            {
                //lblError.Visible = true;
                //lblError.Text = "Please log in to edit";
            }
        }

        protected void grdSchedule_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var index = e.RowIndex;
            var gameLabel = grdSchedule.Rows[index].FindControl("lblGameNumber");
            var gameNumber = Convert.ToInt32((gameLabel as Label).Text);
            var division = Convert.ToInt32(ddlDivisions.SelectedValue);
            var homeTeam = (TextBox)grdSchedule.Rows[index].FindControl("txtHomeScore");
            var homeScore = Convert.ToInt32((homeTeam as TextBox).Text);
            var visitingTeam = (TextBox)grdSchedule.Rows[index].FindControl("txtVisitingScore");
            var visitingScore = Convert.ToInt32((visitingTeam as TextBox).Text);

            GameSchedulesViewModel.UpdateScore(gameNumber, division, homeScore, visitingScore);

            Utilities.MsgBox(this, "Record Saved");
            grdSchedule.EditIndex = -1;
            LoadSchedule(Convert.ToInt32(ddlDivisions.SelectedValue), Convert.ToInt32(cobTeams.SelectedValue));

        }

        protected void linkPanelSelection_Click(object sender, EventArgs e)
        {
            if (linkPanelSelection.Text == "Show Standings")
            {
                linkPanelSelection.Text = "ShowGames";
                panelStandings.Visible = true;
                LoadStandings(Convert.ToInt32(ddlDivisions.SelectedValue));
                panelGames.Visible = false;
            }
            else
            {
                panelGames.Visible = true;
                panelStandings.Visible = false;
                LoadSchedule(Convert.ToInt32(ddlDivisions.SelectedValue), Convert.ToInt32(cobTeams.SelectedValue));
                linkPanelSelection.Text = "Show Standings";

            }
        }

        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            loginForm.Visible = false;
        }

        protected void grdSchedule_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            var index = e.RowIndex;
            grdSchedule.EditIndex = -1;
        }

    }

}