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
using CSBC.Core.Repositories;
using CSBC.Components;

namespace Csbchoops.Web

{

    public partial class GameSchedules : System.Web.UI.Page
    {

        protected void Page_Load(System.Object sender, System.EventArgs e)
        {
            //Put user code to initialize the page here
            if (string.IsNullOrEmpty(Session["Module"]))
            {
                Response.Redirect("default.aspx");
            }
            Session["Module"] = "Games";
            if (Page.IsPostBack == false)
            {
                //Session["ReportName"] = lnkSwitch.Text '"Schedules"
                GetSeason();
                GetDivisions();
                Session["DivStats"] = false;
                if (Session["ScheduleNo"] > 0)
                {
                    cobDivisions.SelectedValue = Session["ScheduleNo"];
                }
                GetTeams(cobDivisions.SelectedItem.Value);
                if (cobDivisions.Items.Count > 1)
                {
                    LoadSchedule(cobDivisions.SelectedItem.Text, cobDivisions.SelectedItem.Value, "ALL TEAMS", 0);
                }
            }
        }

        private void GetSeason()
        {
            Season.ClsSeasons oSeason = new Season.ClsSeasons();
            try
            {
                oSeason.GetSeason(Session["CompanyID"]);
                Session["SeasonDesc"] = oSeason.SeasonDesc;
                Session["SeasonID"] = oSeason.SeasonID;

            }
            catch (Exception ex)
            {
                lblError.Text = "GetSeason:" + ex.Message;
            }
            finally
            {
                oSeason = null;
            }
        }

        private void GetDivisions()
        {
            Season.ClsDivisions oDivisions = new Season.ClsDivisions();
            DataTable rsData = default(DataTable);
            try
            {
                rsData = oDivisions.GetRecords(Session["CompanyID"], Session["SeasonID"]);
                cobDivisions.Items.Clear();
                if (rsData.Rows.Count == 0)
                {
                    DataRow dRow = default(DataRow);
                    dRow = rsData.NewRow;
                    dRow("ScheduleNumber") = 0;
                    dRow("Div_Desc") = " ";
                    rsData.Rows.InsertAt(dRow, 0);
                }
                var _with1 = cobDivisions;
                _with1.DataSource = rsData;
                _with1.DataValueField = "ScheduleNumber";
                _with1.DataTextField = "Div_Desc";
                _with1.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "GetDivisions:" + ex.Message;
            }
            finally
            {
                oDivisions = null;
            }
        }

        private void GetTeams(int iDiv)
        {
            Season.ClsTeams oTeams = new Season.ClsTeams();
            DataTable rsData = default(DataTable);
            DataRow dRow = default(DataRow);
            try
            {
                rsData = oTeams.LoadDivisionTeams(iDiv, Session["CompanyID"], Session["SeasonID"], true);
                cobTeams.Items.Clear();
                dRow = rsData.NewRow;
                dRow("TeamNo") = 0;
                dRow("TeamDescr") = "ALL TEAMS";
                rsData.Rows.InsertAt(dRow, 0);
                if (rsData.Rows.Count > 0)
                {
                    if (Information.IsDBNull(rsData.Rows(0).Item("Stats")))
                    {
                        Session["DivStats"] = false;
                    }
                    else
                    {
                        Session["DivStats"] = rsData.Rows(0).Item("Stats");
                    }
                }
                var _with2 = cobTeams;
                _with2.DataSource = rsData;
                _with2.DataValueField = "TeamNo";
                _with2.DataTextField = "TeamDescr";
                _with2.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "GetTeams:" + ex.Message;
            }
            finally
            {
                oTeams = null;
            }
        }

        private void LoadSchedule(string sDivDesc, int iDiv, string sTeamDesc, int iTeam)
        {
            ScheduleGameRepository oGames = new ScheduleGameRepository(new CSBC.Core.Data.CSBCDbContext());
            DataSet dataset = default(DataSet);
            DataTable rsData = default(DataTable);
            try
            {
                dataset = oGames.GetGames(1, iDiv, iTeam, sDivDesc, sTeamDesc);
                rsData = dataset.Tables(0);

                //CreateXML(rsData)


                var _with3 = grdSchedule;
                _with3.DataSource = rsData;
                _with3.DataBind();
                if (iDiv > 0)
                {
                    lblTitle.Text = sDivDesc + " Schedule";
                }
                if (iTeam > 0)
                {
                    lblTitle.Text = sDivDesc + " Team " + sTeamDesc + " Schedule";
                }
                Session["ScheduleNo"] = iDiv;
                Session["ScheduleDesc"] = sDivDesc;
                Session["TeamName"] = sTeamDesc;
                Session["ReportName"] = "Schedules";
                cobTeams.Visible = true;
                lblTeams.Visible = true;

                lblUsername.Visible = false;
                txtUser.Visible = false;
                lblPassword.Visible = false;
                txtPwd.Visible = false;
                btnSubmit.Visible = false;
                lnkForgot.Visible = false;
                lblLocation.Visible = false;
                lblHome.Visible = false;
                lblDate.Visible = false;
                lblVisitor.Visible = false;
                btnUpdate.Visible = false;
                txtVScores.Visible = false;
                txtHScores.Visible = false;
                //imgStandings.Visible = True

                lblError.Text = "";
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadSchedule:" + ex.Message;
            }
            finally
            {
                oGames = null;
                rsData = null;
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

        private void cobTeams_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            LoadSchedule(cobDivisions.SelectedItem.Text, cobDivisions.SelectedItem.Value, cobTeams.SelectedItem.Text, cobTeams.SelectedItem.Value);
        }

        private void cobDivisions_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            GetTeams(cobDivisions.SelectedItem.Value);
            if (Session["ReportName"] == "Schedules")
            {
                LoadSchedule(cobDivisions.SelectedItem.Text, cobDivisions.SelectedItem.Value, cobTeams.SelectedItem.Text, cobTeams.SelectedItem.Value);
            }
            if (Session["ReportName"] == "Standings")
            {
                cobTeams.SelectedIndex = 0;
                LoadStandings(cobDivisions.SelectedItem.Text, cobDivisions.SelectedItem.Value);
            }

            if (Session["DivStats"] == true)
            {
                lnkStats.Visible = true;
            }
            else
            {
                lnkStats.Visible = false;
            }
        }


        private void LoadStandings(string sDesc, int iDiv)
        {
            Season.clsGames oGames = new Season.clsGames();
            DataTable rsData = default(DataTable);
            try
            {
                rsData = oGames.GetStanding(Session["CompanyID"], iDiv);
                //CreateXML(rsData)
                var _with4 = grdStanding;
                _with4.DataSource = rsData;
                _with4.DataBind();
                //With .DisplayLayout.Bands(0).Columns
                //    .FromKey("TeamNo").Hidden = True
                //    .FromKey("ScheduleName").Hidden = True
                //    .FromKey("DivNo").Hidden = True
                //    .FromKey("TeamName").Hidden = True
                //    .FromKey("TieBreaker").Hidden = True
                //    .FromKey("Team").Width = 130
                //    .FromKey("Team").Header.Fixed = True
                //    .FromKey("Won").CellStyle.HorizontalAlign = HorizontalAlign.Center
                //    .FromKey("won").Width = 40
                //    .FromKey("won").Header.Fixed = True
                //    .FromKey("Lost").CellStyle.HorizontalAlign = HorizontalAlign.Center
                //    .FromKey("Lost").Width = 40
                //    .FromKey("Lost").Header.Fixed = True
                //    .FromKey("PCT").CellStyle.HorizontalAlign = HorizontalAlign.Center
                //    .FromKey("PCT").Width = 60
                //    .FromKey("PCT").Header.Fixed = True
                //    .FromKey("Streak").CellStyle.HorizontalAlign = HorizontalAlign.Center
                //    .FromKey("Streak").Width = 60
                //    .FromKey("Streak").Header.Fixed = True
                //    .FromKey("PF").CellStyle.HorizontalAlign = HorizontalAlign.Center
                //    .FromKey("PF").Width = 60
                //    .FromKey("PF").Header.Fixed = True
                //    .FromKey("PA").CellStyle.HorizontalAlign = HorizontalAlign.Center
                //    .FromKey("PA").Width = 60
                //    .FromKey("PA").Header.Fixed = True
                //    .FromKey("GB").CellStyle.HorizontalAlign = HorizontalAlign.Center
                //    .FromKey("GB").Header.Fixed = True
                //    .FromKey("GB").Width = 100
                //End With
                if (iDiv > 0)
                {
                    lblTitle.Text = sDesc + " Standing";
                }
                else
                {
                    lblTitle.Text = "Standing";
                }
                Session["ReportName"] = "Standings";
                Session["ScheduleDesc"] = sDesc;
                Session["ScheduleNo"] = iDiv;
                lblError.Text = " ";
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadStandings:" + ex.Message;
            }
            finally
            {
                oGames = null;
            }
            //grdStanding.Columns(7).Header.Title = "PF=>All games points scored (by team), PA=All Games point scored (by opponents), GB=Games Behind the division leader"
        }

        private string AccessType(long Usercode, string sScreen)
        {
            string functionReturnValue = null;
            Security.ClsUsers oUser = new Security.ClsUsers();
            try
            {
                oUser.GetAccess(Usercode, sScreen, Session["CompanyID"]);
                functionReturnValue = oUser.AccessType;
            }
            catch (Exception ex)
            {
                lblError.Text = "AccessType:" + ex.Message;
                functionReturnValue = 0;
                return functionReturnValue;
            }
            finally
            {
                oUser = null;
            }
            return functionReturnValue;
        }

        private void GetUser()
        {
            Security.ClsUsers oUser = new Security.ClsUsers();
            try
            {
                oUser.GetUser(txtUser.Text, txtPwd.Text);
                //I NEED TO EVALUATE FOR NOTHING COMING BACK
                Session.Add("USERCODE", oUser.UserID);
                Session.Add("USERNAME", oUser.UserName);
                Session.Add("HouseID", oUser.HouseID);
            }
            catch (Exception ex)
            {
                lblError.Text = "GetUser:" + ex.Message;
                return;
            }
            finally
            {
                oUser = null;
            }
            if (Session["USERCODE"] > 0)
            {
                ValidateAccess();
                //Call CheckForStats()
            }
            else
            {
                lblError.Text = "Invalid Username/Password";
            }
        }

        //Private Function CheckEncryption(ByVal susername As String) As Boolean
        //Dim rdr As SqlClient.SqlDataReader
        //SqlCn.Open()
        //Sql = "EXEC CheckEncryption @UserName = " & Quotes(txtUser.Value)
        //Dim selectCMD As SqlCommand = New SqlCommand(Sql, SqlCn)
        //rdr = selectCMD.ExecuteReader
        //CheckEncryption = False
        //Try
        //    While rdr.Read()
        //        CheckEncryption = rdr.Item("PWD")
        //    End While
        //    rdr.Close()
        //Catch ex As Exception
        //    Response.Write(ex.Message)
        //    Response.Write(Sql)
        //    Response.End()
        //Finally
        //    If SqlCn.State = ConnectionState.Open Then
        //        SqlCn.Close()
        //    End If
        //End Try


        //End Function

        //Private Sub UpdatePWD(ByVal sUsername As String, ByVal PWD As String)
        //            Dim rdr As SqlClient.SqlDataReader
        //            SqlCn.Open()
        //            SQL = "EXEC CheckLoginNE @UserName = " & Quotes(sUsername) & ", @Pword = " & Quo(PWD) & ", @PassWord = " & Quo(HashPassword(PWD))
        //            Dim selectCMD As SqlCommand = New SqlCommand(SQL, SqlCn)
        //            Try
        //                selectCMD.ExecuteNonQuery()
        //            Catch ex As Exception
        //                Response.Write(ex.Message)
        //                Response.Write(SQL)
        //                Response.End()
        //            Finally
        //                If SqlCn.State = ConnectionState.Open Then
        //                    SqlCn.Close()
        //                End If
        //            End Try
        //End Sub

        private string HashPassword(string password)
        {
            //Dim hashedPassword As String
            //Dim hashProvider As SHA256Managed
            //Try
            //    Dim passwordBytes() As Byte
            //    Dim hashBytes() As Byte
            //    passwordBytes = System.Text.Encoding.Unicode.GetBytes(password)
            //    hashProvider = New SHA256Managed
            //    hashProvider.Initialize()
            //    passwordBytes = hashProvider.ComputeHash(passwordBytes)
            //    hashedPassword = Convert.ToBase64String(passwordBytes)
            //Finally
            //    If Not hashProvider Is Nothing Then
            //        hashProvider.Clear()
            //        hashProvider = Nothing
            //    End If
            //End Try
            //Return hashedPassword

        }


        private void ValidateAccess()
        {
            lblError.Text = "";
            if (AccessType(Session["USERCODE"], "Scores") != "U" & CheckAD() == 0 & Session["HouseID"] > 0)
            {
                lblError.Text = "Warning - not allow to update games scores ";
                Session["USERNAME"] = "";
                Session["Admin"] = "";
                //imgStandings.Visible = True
                if (Session["HouseID"] > 0)
                {
                    lblUsername.Visible = true;
                    txtUser.Visible = true;
                    lblPassword.Visible = true;
                    txtPwd.Visible = true;
                    btnSubmit.Visible = true;
                    lnkForgot.Visible = true;
                    lblLocation.Visible = false;
                    lblHome.Visible = false;
                    lblDate.Visible = false;
                    lblVisitor.Visible = false;
                    btnUpdate.Visible = false;
                    txtVScores.Visible = false;
                    txtHScores.Visible = false;
                    //imgStandings.Visible = False
                    lblError.Text = lblError.Text + "SELECT YOUR DIVISION GAMES.";
                }
                else
                {
                    lblUsername.Visible = false;
                    txtUser.Visible = false;
                    lblPassword.Visible = false;
                    txtPwd.Visible = false;
                    btnSubmit.Visible = false;
                    lnkForgot.Visible = false;
                    lblLocation.Visible = true;
                    lblHome.Visible = true;
                    lblDate.Visible = true;
                    lblVisitor.Visible = true;
                    btnUpdate.Visible = true;
                    txtVScores.Visible = true;
                    txtHScores.Visible = true;
                    //imgStandings.Visible = False
                }
                Session["HouseID"] = 0;
            }
            else
            {
                lblUsername.Visible = false;
                txtUser.Visible = false;
                lblPassword.Visible = false;
                txtPwd.Visible = false;
                btnSubmit.Visible = false;
                lnkForgot.Visible = false;
                lblLocation.Visible = true;
                lblHome.Visible = true;
                lblDate.Visible = true;
                lblVisitor.Visible = true;
                btnUpdate.Visible = true;
                txtVScores.Visible = true;
                txtHScores.Visible = true;
                //imgStandings.Visible = False
            }

        }

        //Private Sub CheckForStats()
        //    If KeepStats() Then
        //        'transfer to stats
        //        Response.Redirect("GamesStats.aspx")
        //        Exit Sub
        //    End If

        //End Sub

        private int CheckAD()
        {
            int functionReturnValue = 0;
            functionReturnValue = 0;
            Season.ClsDivisions oDivisions = new Season.ClsDivisions();
            if (Session["HouseID"] > 0)
            {
                try
                {
                    functionReturnValue = oDivisions.DivisionAD(Session["CompanyID"], Session["SeasonID"], Session["ScheduleNoAD"], txtUser.Text, txtPwd.Text);
                }
                catch (Exception ex)
                {
                    lblError.Text = "CheckAD:" + ex.Message;
                }
                finally
                {
                    oDivisions = null;
                }

            }
            else
            {
                return functionReturnValue;
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
                lblError.Text = "Email sent!";
                oEmail.Dispose();
                oSmtp = null;
            }
            catch (Exception ex)
            {
                lblError.Text = "Unable to send mail!  " + ex.Message;
            }
        }

        private bool IsEmail(string Email)
        {
            bool functionReturnValue = false;
            if (Email > "")
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

        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {
            //If CheckEncryption(txtUser.Value) = False Then UpdatePWD(txtUser.Value, txtPassword.Value)
            GetUser();
            //Session["USERACCESS"] = AccessType(Session["USERCODE"])
            //txtUser.Value = Session["USERNAME"]


            //CheckUser()
            //If Session["UserID"] > 0 And Session["Usertype"] > 0 Then
            //    lblError.Text = ""
            //    txtUser.Text = ""
            //    txtPwd.Text = ""
            //    lblUsername.Visible = False
            //    txtUser.Visible = False
            //    lblPassword.Visible = False
            //    txtPwd.Visible = False
            //    btnSubmit.Visible = False
            //    imgStandings.Visible = False

            //    lblLocation.Visible = True
            //    lblHome.Visible = True
            //    lblDate.Visible = True
            //    lblVisitor.Visible = True
            //    btnUpdate.Visible = True
            //    txtVScores.Visible = True
            //    txtHScores.Visible = True
            //Else
            //    lblError.Text = "Invalid Username/Password"
            //End If
        }

        private void CheckUser()
        {
            Security.ClsUsers oSecurity = new Security.ClsUsers();
            try
            {
                var _with5 = oSecurity;
                _with5.GetUser(txtUser.Text, txtPwd.Text);
                //Session["UserName"] = .UserName
                //Session["SeasonDesc"] = .SeasonDesc
                Session["Usertype"] = _with5.Usertype;
                Session["UserID"] = _with5.UserID;
                Session["HouseID"] = _with5.HouseID;
                Session["CompanyID"] = _with5.CompanyID;
                //Session["CompanyName"] = .CompanyName
                //Session["ImageName"] = .ImageName
                //Session["TimeZone"] = .TimeZone
                Session["SeasoniD"] = _with5.SeasonID;
            }
            catch (Exception ex)
            {
                lblError.Text = "Invalid Username/Password";
            }
            finally
            {
                oSecurity = null;
            }
        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            lblLocation.Visible = false;
            lblHome.Visible = false;
            lblDate.Visible = false;
            lblVisitor.Visible = false;
            btnUpdate.Visible = false;
            txtVScores.Visible = false;
            txtHScores.Visible = false;
            //imgStandings.Visible = True

            if ((!Information.IsNumeric(txtHScores.Text) & txtHScores.Text > " ") | (!Information.IsNumeric(txtVScores.Text) & txtVScores.Text > " "))
            {
                Response.Write("Invalid value entered");
                Response.End();
                return;
            }
            UpdateScores();
            LoadSchedule(Session["ScheduleDesc"], Session["ScheduleNo"], cobTeams.SelectedItem.Text, cobTeams.SelectedItem.Value);

        }

        private void UpdateScores()
        {
            Season.clsGames oGames = new Season.clsGames();
            try
            {
                if (Information.IsNumeric(txtHScores.Text) & Information.IsNumeric(txtVScores.Text))
                {
                    oGames.UpdateGameScores(Session["ScheduleNo"], Session["GameNumber"], Session["GameType"], txtHScores.Text, txtVScores.Text);
                }
                else
                {
                    oGames.UpdateGameScores(Session["ScheduleNo"], Session["GameNumber"], Session["GameType"]);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "UpdateScores:" + ex.Message;
            }
            finally
            {
                oGames = null;
            }
        }

        //Protected Sub grdGames_DblClick(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.ClickEventArgs) Handles grdGames.DblClick
        //    If lnkSwitch.Text = "Schedules" Then
        //        'turn off selection on grid
        //        'grdGames.DisplayLayout.ActiveRow.Activated = False
        //        Exit Sub

        //    End If

        //    If grdGames.DisplayLayout.ActiveRow Is Nothing Then Exit Sub

        //    Session["GameNumber"] = grdGames.DisplayLayout.ActiveRow.Cells.FromKey("GameNumber").Value
        //    Session["GameType"] = grdGames.DisplayLayout.ActiveRow.Cells.FromKey("RecType").Value
        //    Session["HomeTeam"] = grdGames.DisplayLayout.ActiveRow.Cells.FromKey("HomeTeamNumber").Value
        //    Session["VisitingTeam"] = grdGames.DisplayLayout.ActiveRow.Cells.FromKey("VisitingTeamNumber").Value

        //    If KeepStats() Then
        //        'transfer to stats
        //        Response.Redirect("GamesStats.aspx")
        //        Exit Sub
        //    End If

        //    If Session["ScheduleNoAD"] <> grdGames.DisplayLayout.ActiveRow.Cells.FromKey("ScheduleNumber").Value Then
        //        Session["ScheduleNoAD"] = grdGames.DisplayLayout.ActiveRow.Cells.FromKey("ScheduleNumber").Value
        //        Call ValidateAccess()
        //        If lblError.Text > "" Then Exit Sub
        //    End If
        //    Session["ScheduleNoAD"] = grdGames.DisplayLayout.ActiveRow.Cells.FromKey("ScheduleNumber").Value

        //    Dim oGames As New Season.clsGames
        //    Try
        //        oGames.GetGames(Session["ScheduleNoAD"], Session["GameNumber"], Session["GameType"])
        //        'I NEED TO EVALUATE FOR NOTHING COMING BACK
        //        lblDate.Text = oGames.GameDate & " " & oGames.GameTime
        //        lblLocation.Text = oGames.Location
        //        lblHome.Text = oGames.Home
        //        lblVisitor.Text = oGames.Visitor
        //        If Not IsDBNull(oGames.HomeTeamScore) Then
        //            txtHScores.Text = oGames.HomeTeamScore
        //        Else
        //            txtHScores.Text = ""
        //        End If
        //        If Not IsDBNull(oGames.VisitingTeamScore) Then
        //            txtVScores.Text = oGames.VisitingTeamScore
        //        Else
        //            txtVScores.Text = ""
        //        End If
        //    Catch ex As Exception
        //        lblError.Text = "grdGames:" & ex.Message
        //        Exit Sub
        //    Finally
        //        oGames = Nothing
        //    End Try
        //    lblError.Text = ""
        //    imgStandings.Visible = False
        //    If Session["USERNAME"] = "" And Session["Admin"] = "" Then
        //        lblUsername.Visible = True
        //        txtUser.Visible = True
        //        lblPassword.Visible = True
        //        txtPwd.Visible = True
        //        btnSubmit.Visible = True
        //        lnkForgot.Visible = True
        //        imgStandings.Visible = False

        //        lblDate.Visible = False
        //        lblLocation.Visible = False
        //        lblVisitor.Visible = False
        //        lblHome.Visible = False
        //        txtVScores.Visible = False
        //        txtHScores.Visible = False
        //        btnUpdate.Visible = False
        //    Else

        //        lblUsername.Visible = False
        //        txtUser.Visible = False
        //        lblPassword.Visible = False
        //        txtPwd.Visible = False
        //        btnSubmit.Visible = False
        //        lnkForgot.Visible = False
        //        lblDate.Visible = True
        //        lblLocation.Visible = True
        //        lblVisitor.Visible = True
        //        lblHome.Visible = True
        //        txtVScores.Visible = True
        //        txtHScores.Visible = True
        //        btnUpdate.Visible = True
        //        'Call CheckForStats()

        //    End If
        //End Sub

        private bool KeepStats()
        {
            bool functionReturnValue = false;
            if (Session["DivStats"] == true)
            {
                functionReturnValue = true;
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
                lblTeams.Visible = true;
                lnkTie.Visible = false;
                grdSchedule.Visible = true;
                grdStanding.Visible = false;
                lnkPrint.Visible = true;
            }
            else
            {
                cobTeams.Visible = false;
                lblTeams.Visible = false;
                lnkTie.Visible = true;
                grdSchedule.Visible = false;
                grdStanding.Visible = true;
                lnkPrint.Visible = false;
            }

            if (lnkSwitch.Text == "Standings")
            {
                lnkSwitch.Text = "Schedules";
                lblUsername.Visible = false;
                txtUser.Visible = false;
                lblPassword.Visible = false;
                txtPwd.Visible = false;
                btnSubmit.Visible = false;
                lnkForgot.Visible = false;
                lblDate.Visible = false;
                lblLocation.Visible = false;
                lblVisitor.Visible = false;
                lblHome.Visible = false;
                txtVScores.Visible = false;
                txtHScores.Visible = false;
                btnUpdate.Visible = false;
            }
            else
            {
                lnkSwitch.Text = "Standings";
                GetTeams(cobDivisions.SelectedItem.Value);
            }
            //If Session["DivStats"] = True Then
            //    lnkStats.Visible = True
            //Else
            //    lnkStats.Visible = False
            //End If
            if (lnkSwitch.Text == "Schedules")
                LoadStandings(Session["ScheduleDesc"], Session["ScheduleNo"]);
            if (lnkSwitch.Text == "Standings")
                LoadSchedule(cobDivisions.SelectedItem.Text, cobDivisions.SelectedItem.Value, cobTeams.SelectedItem.Text, cobTeams.SelectedItem.Value);
        }

        protected void lnkForgot_Click(object sender, System.EventArgs e)
        {
            string Email = null;
            string pwd = null;
            Security.ClsUsers oUser = new Security.ClsUsers();
            try
            {
                if (string.IsNullOrEmpty(txtUser.Text))
                {
                    lblError.Text = "ERROR - Missing User";
                    return;
                }
                oUser.GetEmail(Session["CompanyID"], txtUser.Text);
                Email = oUser.Email;
                if (!IsEmail(Email))
                {
                    lblError.Text = "Invalid/missing Email";
                    return;
                }
                pwd = oUser.PWord;
                if (Email > "" & pwd > "")
                {
                    EmailReset(Email, pwd);
                }
                else
                {
                    lblError.Text = "Invalid/missing Username";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "lnkForgot:" + ex.Message;
            }
            finally
            {
                oUser = null;
            }
        }

        //Private Sub imgPrint_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPrint.Click
        //    'Function to open connection and table
        //    Dim dt As DataTable
        //    Dim SQLString As String = TKSUCSearchChild.SQLWhereClause
        //    Try
        //        'dt = GetTableData("View_Item", SQLString, SQLOrderByClause)
        //        'dt = Your DataTable 
        //        oRpt = New YourReportName
        //        oRpt.SetDataSource(dt)
        //        View_PickingSlip.ReportSource = oRpt
        //        Dim exp As ExportOptions
        //        Dim req As ExportRequestContext
        //        Dim st As System.IO.Stream
        //        Dim b() As Byte
        //        Dim pg As Page
        //        pg = View_PickingSlip.Page
        //        exp = New ExportOptions
        //        exp.ExportFormatType = ExportFormatType.PortableDocFormat
        //        exp.FormatOptions = New PdfRtfWordFormatOptions
        //        req = New ExportRequestContext
        //        req.ExportInfo = exp
        //        With oRpt.FormatEngine.PrintOptions
        //            '.PaperSize = PaperSize.PaperLegal
        //            .PaperOrientation = PaperOrientation.Landscape
        //        End With
        //        st = oRpt.FormatEngine.ExportToStream(req)
        //        pg.Response.ClearHeaders()
        //        pg.Response.ClearContent()
        //        pg.Response.ContentType = "application/pdf"
        //        ReDim b(st.Length)
        //        st.Read(b, 0, CInt(st.Length))
        //        pg.Response.BinaryWrite(b)
        //        pg.Response.End()
        //        dt.Dispose()
        //    Catch ex As Exception
        //        lblError.Text = ex.Message
        //    End Try

        //End Sub

        //Protected Sub btnStats_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStats.Click
        //    Dim strExportFile As String
        //    strExportFile = "PlayersStats"
        //    Dim printDoc As New PrintDocument
        //    Dim ReportPath As String = ""
        //    Dim crReportDocument As ReportDocument = New ReportDocument

        //    ReportPath = "Reports/PlayersStats.rpt"

        //    crReportDocument.Load(Server.MapPath(ReportPath))
        //    Dim oData As New CSBC.Components.Season.clsGames
        //    'Pass the populated dataset to the report
        //    crReportDocument.SetDataSource(oData.GetStats(Session["ScheduleNo"], Session["SeasonID"]))
        //    crReportDocument.SetParameterValue("SeasonDesc", Session["SeasonDesc"])
        //    crReportDocument.SetParameterValue("Division", Session["ScheduleDesc"])
        //    oData = Nothing

        //    Dim s As System.IO.MemoryStream = crReportDocument.ExportToStream(ExportFormatType.PortableDocFormat)
        //    With HttpContext.Current.Response
        //        .ClearContent()
        //        .ClearHeaders()
        //        .ContentType = "application/pdf"
        //        .AddHeader("Content-Disposition", "inline; filename=" & strExportFile)
        //        .BinaryWrite(s.ToArray)
        //        .End()
        //    End With

        //    printDoc = Nothing
        //    crReportDocument = Nothing

        //    Response.Redirect("Report.aspx")
        //End Sub

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
            switch (Session["ReportName"])
            {
                case "Schedules":
                    //ReportPath = "Reports/DivSchedule.rpt"

                    Session["Report"] = "Reports/DivSchedule.rpt";

                    break;
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

                case "Standings":
                    Session["Report"] = "Reports/DivStanding.rpt";
                    break;
                //ReportPath = "Reports/DivStanding.rpt"
                //        crReportDocument.Load(Server.MapPath(ReportPath))
                //        Dim oData As New CSBC.Components.Season.clsGames
                //        'Pass the populated dataset to the report
                //        crReportDocument.SetDataSource(oData.GetStanding(Session["CompanyID"], Session["ScheduleNo"]))
                //        crReportDocument.SetParameterValue("SeasonDesc", Session["SeasonDesc"])
                //        crReportDocument.SetParameterValue("CompanyName", Session["CompanyName"])
                //        crReportDocument.SetParameterValue("Division", Session["ScheduleDesc"])
                //        oData = Nothing
            }
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
            if (grdSchedule.RecordCount > 0)
                Response.Redirect("Report.aspx?Report=Reports/DivSchedule.rpt");

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



        protected void grdGames_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //If lnkSwitch.Text = "Schedules" Then
            //    'turn off selection on grid
            //    'grdGames.DisplayLayout.ActiveRow.Activated = False
            //    Exit Sub

            //End If
            ////JavaScript call
            //if (e.CommandName == "select")
            //{
            //string s = string.Empty;
            //s += "Row Index:" + e.Item.Index.ToString();
            //s += "<br />1:" + e.Item.Cells[1].Value.ToString();
            //s += "<br />2:" + e.Item.Cells[2].Value.ToString();
            //s += "<br />3:" + e.Item.Cells[3].Value.ToString();
            //LabelDebugInfo.Text = s;

            if (e.CommandName != "select")
                return;

            Session["GameNumber"] = e.Item.Cells(0).Value.ToString();
            Session["GameType"] = e.Item.Cells(1).Value.ToString();
            Session["HomeTeam"] = e.Item.Cells(6).Value.ToString();
            Session["VisitingTeam"] = e.Item.Cells(7).Value.ToString();

            if (KeepStats())
            {
                //transfer to stats
                Response.Redirect("GamesStats.aspx");
                return;
            }

            if (Session["ScheduleNoAD"] != e.Item.Cells(2).Value.ToString())
            {
                Session["ScheduleNoAD"] = e.Item.Cells(2).Value.ToString();
                ValidateAccess();
                if (lblError.Text > "")
                    return;
            }
            Session["ScheduleNoAD"] = e.Item.Cells(2).Value.ToString();

            Season.clsGames oGames = new Season.clsGames();
            try
            {
                oGames.GetGames(Session["ScheduleNoAD"], Session["GameNumber"], Session["GameType"]);
                //I NEED TO EVALUATE FOR NOTHING COMING BACK
                lblDate.Text = oGames.GameDate + " " + oGames.GameTime;
                lblLocation.Text = oGames.Location;
                lblHome.Text = oGames.Home;
                lblVisitor.Text = oGames.Visitor;
                if (!Information.IsDBNull(oGames.HomeTeamScore))
                {
                    txtHScores.Text = oGames.HomeTeamScore;
                }
                else
                {
                    txtHScores.Text = "";
                }
                if (!Information.IsDBNull(oGames.VisitingTeamScore))
                {
                    txtVScores.Text = oGames.VisitingTeamScore;
                }
                else
                {
                    txtVScores.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "grdGames:" + ex.Message;
                return;
            }
            finally
            {
                oGames = null;
            }
            lblError.Text = "";
            //imgStandings.Visible = False
            if (string.IsNullOrEmpty(Session["USERNAME"]) & string.IsNullOrEmpty(Session["Admin"]))
            {
                lblUsername.Visible = true;
                txtUser.Visible = true;
                lblPassword.Visible = true;
                txtPwd.Visible = true;
                btnSubmit.Visible = true;
                lnkForgot.Visible = true;
                //imgStandings.Visible = False

                lblDate.Visible = false;
                lblLocation.Visible = false;
                lblVisitor.Visible = false;
                lblHome.Visible = false;
                txtVScores.Visible = false;
                txtHScores.Visible = false;
                btnUpdate.Visible = false;

            }
            else
            {
                lblUsername.Visible = false;
                txtUser.Visible = false;
                lblPassword.Visible = false;
                txtPwd.Visible = false;
                btnSubmit.Visible = false;
                lnkForgot.Visible = false;
                lblDate.Visible = true;
                lblLocation.Visible = true;
                lblVisitor.Visible = true;
                lblHome.Visible = true;
                txtVScores.Visible = true;
                txtHScores.Visible = true;
                btnUpdate.Visible = true;
                //Call CheckForStats()

            }
            //End Sub
            //    If e.CommandName = "Select" Then

            //        lblError.Text = "Bingo"
            //    End If
        }

    }

}