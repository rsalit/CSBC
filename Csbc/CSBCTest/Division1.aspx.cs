using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Diagnostics;
using CSBC.Components;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class Division1 : BaseForm
    {
        public int CoachId { get; set; }
        public int DivisionId
        {
            get
            {
                if (Session["DivisionID"] == null)
                    return 0;
                else
                {
                    return Convert.ToInt32(Session["DivisionID"]);
                }
            }
            set
            {
                Session["DivisionID"] = value;
            }
        }

        protected override  void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "Divisions";
            base.Page_Load(sender, e);
            if (Page.IsPostBack == false)
            {
                this.txtName.Focus();
                //Session["ReportName"] = "TryoutsInfo"
                LoadDivisions();
                LoadDirectors();
                if (DivisionId == 0)
                {
                    ClearFields();
                }
                else
                {
                    LoadRow(DivisionId);
                }
            }
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
        private void LoadDivisions()
        {
            //var oDivisions = new CSBC.Components.Season.ClsDivisions();
            //DataTable rsData = default(DataTable);
            var rep = new DivisionRepository(new CSBCDbContext());
            try
            {
                var data = rep.GetDivisions(Master.SeasonId).ToList();

                grdDivisions.DataSource = data;
                grdDivisions.DataBind();
                var _with2 = grdDivisions;

            }
            catch (Exception ex)
            {
                lblError.Text = "LoadDivisions::" + ex.Message;
            }
            finally
            {

            }
        }

        public void LoadDirectors()
        {
            DataRow dRow = default(DataRow);
            var oDivisions = new CSBC.Components.Season.ClsDivisions();
            DataTable rsData = default(DataTable);
            try
            {
                rsData = oDivisions.LoadDirector((int)Session["CompanyID"]);
                cboAD.Items.Clear();

                dRow = rsData.NewRow();
                dRow["PeopleID"] = 0;
                dRow["Name"] = "NO AD";
                rsData.Rows.InsertAt(dRow, 0);
                var _with3 = cboAD;
                _with3.DataSource = rsData;
                _with3.DataValueField = "PeopleID";
                _with3.DataTextField = "Name";
                _with3.DataBind();

            }
            catch (Exception ex)
            {
                lblError.Text = "LoadDirectors::" + ex.Message;
            }
            finally
            {
                oDivisions = null;
            }

        }

        private void ClearFields()
        {
            lblDivisionID.Value = "0";
            txtName.Text = "";
            txtMinDate.Text = "";
            txtMaxDate.Text = "";
            hdnMinDateOld.Value = "";
            hdnMaxDateOld.Value = "";
            txtMinDate2.Text = "";
            txtMaxDate2.Text = "";
            hdnMinDate2Old.Value = "";
            hdnMaxDate2Old.Value = "";
            txtTime.Text = "";
            txtDate.Text = "";
            txtVenue.Text = "";
            cboAD.SelectedIndex = 0;
            lblHPhon.Text = "";
            lblCPhon.Text = "";
            grdTeams.Columns.Clear();
            //hdnGenderOLD.Value = "";
            //radGender.Items[0].Selected() = false;
            //radGender.Items[1].Selected() = false;
            //hdnGender2OLD.Value = "";
            //radGender2.Items(0).Selected() = false;
            //radGender2.Items(1).Selected() = false;
            lblError.Text = "";
            lblDelete.Text = "";
        }

        private void LoadRow(int rowId)
        {
            try
            {
                var rep = new DivisionRepository(new CSBCDbContext());
                var division = rep.GetById(Convert.ToInt32(rowId));
                DivisionId = division.DivisionID;
                LoadRow(division);
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadRow::" + ex.Message;
            }

            LoadTeams(DivisionId);
        }

        private void LoadRow(Division division)
        {
            try
            {
                lblDivisionID.Value = division.DivisionID.ToString();
                txtName.Text = division.Div_Desc.ToString();
                if (division.MinDate.Value != null)
                {
                    txtMinDate.Text = division.MinDate.Value.ToString("yyyy-MM-dd");
                    hdnMinDateOld.Value = division.MinDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtMinDate.Text = String.Empty;
                    hdnMinDateOld.Value = String.Empty;
                }
                if (division.MaxDate.Value != null)
                {
                    txtMaxDate.Text = division.MaxDate.Value.ToString("yyyy-MM-dd");
                    hdnMaxDateOld.Value = division.MaxDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtMaxDate.Text = String.Empty;
                    hdnMaxDateOld.Value = String.Empty;

                }
                if (division.MinDate2 != null)
                {
                    txtMinDate2.Text = division.MinDate2.Value.ToString("yyyy-MM-dd");
                    hdnMinDate2Old.Value = division.MinDate2.Value.ToString("yyyy-MM-dd");
                }
                else {
                    txtMinDate2.Text = String.Empty;
                    hdnMinDate2Old.Value = String.Empty;
                }
                if (division.MaxDate2 != null)
                {
                    txtMaxDate2.Text = division.MaxDate2.Value.ToString("yyyy-MM-dd");
                    hdnMaxDate2Old.Value = division.MaxDate2.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtMaxDate2.Text = String.Empty;
                    hdnMaxDate2Old.Value = String.Empty;

                }
                lblHPhon.Text = "";
                lblCPhon.Text = "";
                if (division.Director != null)
                {
                    if (division.Director.Cellphone != null)
                    {
                        lblCPhon.Text += "  C-" + division.Director.Cellphone.ToString();
                    }
                    if (division.Director.Household != null)
                    {
                        if (division.Director.Household.Phone != null)
                        {
                            lblHPhon.Text = "H-" + division.Director.Household.Phone.ToString();
                        }
                    }
                }

                cboAD.SelectedValue = "0";
                if (division.DirectorID != 0)
                {
                    //if (Information.IsNumeric(row["DirectorID"]) & rsData.Rows(0).Item("AD"] == true) {
                    cboAD.SelectedValue = division.DirectorID.ToString();
                    //}
                }
                hdnGenderOld.Value = division.Gender.ToString().Trim();
                if (division.Gender.ToString().Trim() == "M")
                {

                    radGender.SelectedIndex = 0;
                }
                else
                {
                    radGender.SelectedIndex = 1;
                    //radGender.Items(1).Selected() = true;
                    //radGender.Items(0).Selected() = false;
                }
                if (!String.IsNullOrEmpty(division.Gender2))
                {
                    hdnGender2Old.Value = division.Gender2.ToString().Trim();
                    if (division.Gender2.ToString().Trim() == "M")
                    {
                        radGender2.SelectedIndex = 0;

                    }
                    else
                    {
                        radGender2.SelectedIndex = 1;
                    }
                }
                if (!String.IsNullOrEmpty(division.DraftVenue))
                    txtVenue.Text = division.DraftVenue.ToString();
                else
                    txtVenue.Text = String.Empty;
                if (division.DraftDate.HasValue)
                    txtDate.Text = division.DraftDate.Value.ToShortDateString();
                else txtDate.Text = String.Empty;
                if (!String.IsNullOrEmpty(division.DraftTime))
                    txtTime.Text = division.DraftTime.ToString();
                else txtTime.Text = String.Empty;
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadRow::" + ex.Message;
            }

            //LoadTeams(division.DivisionID);
        }

        private void LoadTeams(Int32 divisionId)
        {
            var rep = new TeamVM();

            try
            {
                var teams = rep.GetDivisionTeams(divisionId);
                grdTeams.DataSource = teams;
                grdTeams.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadTeams::" + ex.Message;
            }

        }

        private void grdTeams_DblClick(object sender, EventArgs e)
        {
            //Session["TeamID"] = grdTeams.DisplayLayout.ActiveRow.Cells(0).Text();
            if ((int)Session["TeamID"] > 0)
            {
                Response.Redirect("Teams.aspx");
            }
        }

        //private void UpdRow(long RowID)
        //{
        //    var rep = new DivisionRepository(new CSBCDbContext());
        //    var division = new Division();
        //    DateTime date;
        //    try
        //    {
        //        division.SeasonID = (int)Session["SeasonID"];
        //        division.CompanyID = Master.CompanyId;
        //        division.Div_Desc = txtName.Text;
        //        division.MinDate = DateTime.Parse(txtMinDate.Text);
        //        division.MaxDate = DateTime.Parse(txtMaxDate.Text);
        //        if (radGender.SelectedIndex == 0)
        //            division.Gender = "M";
        //        if (radGender.SelectedIndex == 1)
        //            division.Gender = "F";
        //        if (DateTime.TryParse(txtMinDate2.Text, out date))
        //            division.MinDate2 = date;
        //        DateTime goodDate;
        //        if (DateTime.TryParse(txtMaxDate2.Text, out goodDate))
        //            division.MaxDate2 = DateTime.Parse(txtMaxDate2.Text);
        //        if (radGender2.SelectedIndex == 0)
        //            division.Gender2 = "M";
        //        if (radGender2.SelectedIndex == 1)
        //            division.Gender2 = "F";
        //        division.DraftVenue = txtVenue.Text;
        //        if (DateTime.TryParse(txtDate.Text, out date))
        //            division.DraftDate = date;
        //        division.DraftTime = txtTime.Text;
        //        division.DirectorID = Int32.Parse(cboAD.SelectedValue);
        //        division.CoDirectorID = 0;
        //        //cboAD.SelectedValue
        //        division.CreatedUser = Session["UserName"].ToString();
        //        rep.Update(division);

        //    }
        //    catch (Exception ex)
        //    {
        //        Session["ErrorMSG"] = "UpdRow::" + ex.Message;
        //    }

        //}

        //private void ADDRow()
        //{
        //    var oDivisions = new CSBC.Components.Season.ClsDivisions();
        //    DateTime date;
        //    try
        //    {
        //        var _with7 = oDivisions;
        //        _with7.SeasonID = (int)Session["SeasonID"];
        //        _with7.Div_Desc = txtName.Text;
        //        _with7.DirectorID = Int32.Parse(cboAD.SelectedValue.ToString());
        //        _with7.DraftVenue = txtVenue.Text;
        //        if (DateTime.TryParse(txtDate.Text, out date))
        //            _with7.DraftDate = date;
        //        _with7.DraftTime = txtTime.Text;
        //        _with7.MinDate = DateTime.Parse(txtMinDate.Text);
        //        _with7.MaxDate = DateTime.Parse(txtMaxDate.Text);
        //        if (radGender.SelectedIndex == 0)
        //            _with7.Gender = "M";
        //        if (radGender.SelectedIndex == 1)
        //            _with7.Gender = "F";
        //        if (DateTime.TryParse(txtMinDate2.Text, out date))
        //            _with7.MinDate2 = date;
        //        if (DateTime.TryParse(txtMaxDate2.Text, out date))
        //            _with7.MaxDate2 = DateTime.Parse(txtMaxDate2.Text);
        //        if (radGender2.SelectedIndex == 0)
        //            _with7.Gender2 = "M";
        //        if (radGender2.SelectedIndex == 1)
        //            _with7.Gender2 = "F";

        //        _with7.CreatedUser = Session["UserName"].ToString();
        //        oDivisions.UpdRow((long)0, (int)Session["CompanyID"], (int)Session["TimeZone"]);
        //        DivisionId = _with7.DivisionID;
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["ErrorMSG"] = "ADDRow::" + ex.Message;
        //    }
        //    finally
        //    {
        //        oDivisions = null;
        //    }

        //    AddGroup(DivisionId, "REFUNDS");
        //    AddGroup(DivisionId, "WAITING LIST");
        //    AddGroup(DivisionId, "NO SHOW");
        //}

        private void AddGroup(int DivisionID, string sGroup)
        {
            if (Master.AccessType == "R" | getGroup(DivisionID.ToString(), sGroup) > 0)
                return;
            var oTeams = new CSBC.Components.Season.ClsTeams();
            try
            {
                var _with8 = oTeams;
                _with8.SeasonID = (int)Session["SeasonID"];
                _with8.DivisionID = DivisionID;
                _with8.TeamName = sGroup;
                _with8.TeamNumber = "0";
                _with8.CoCoachID = 0;
                _with8.CreatedUser = Session["UserName"].ToString();
                oTeams.UpdRow(0, (int)Session["CompanyID"], (int)Session["TimeZone"]);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "AddGroup::" + ex.Message;
            }
            finally
            {
                oTeams = null;
            }
        }

        private int getGroup(string DivisionID, string sGroup)
        {
            int functionReturnValue = 0;
            var oTeams = new CSBC.Components.Season.ClsTeams();
            functionReturnValue = 0;
            try
            {
                functionReturnValue = oTeams.GetTeamCount(Int32.Parse(DivisionID), sGroup, (int)Session["CompanyID"], (int)Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "getGroup::" + ex.Message;
            }
            finally
            {
                oTeams = null;
            }
            return functionReturnValue;

        }

        private bool errorRTN()
        {
            bool functionReturnValue = false;
            int PlayersCount = 0;
            if (DivisionId > 0)
                PlayersCount = PlayersAssigned();
            functionReturnValue = false;
            if (PlayersCount > 0)
            {
                if ((hdnGenderOld.Value == "M" & radGender.SelectedIndex == 0) | (hdnGenderOld.Value == "F" & radGender.SelectedIndex == 1))
                {
                    Session.Add("ErrorMsg", "Can't change Gender, Unasigned players from division first ");
                    functionReturnValue = true;
                }
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Session.Add("ErrorMsg", "Name missing ");
                txtName.Focus();
                functionReturnValue = true;
            }
            if (string.IsNullOrEmpty(txtMinDate.Text) & functionReturnValue == false)
            {
                Session.Add("ErrorMsg", "Invalid/missing Minimun date mm/dd/yyyy ");
                txtMinDate.Focus();
                functionReturnValue = true;
            }
            if (string.IsNullOrEmpty(txtMaxDate.Text) & functionReturnValue == false)
            {
                Session.Add("ErrorMsg", "Invalid/missing Maximum date mm/dd/yyyy ");
                txtMaxDate.Focus();
                functionReturnValue = true;
            }
            if (radGender.SelectedIndex == 0 & radGender.SelectedIndex == 1 & functionReturnValue == false)
            {
                Session.Add("ErrorMsg", "Gender missing ");
                radGender.Focus();
                functionReturnValue = true;
            }
            if (string.IsNullOrEmpty(txtMinDate2.Text) & !String.IsNullOrEmpty(txtMaxDate2.Text) & functionReturnValue == false)
            {
                Session.Add("ErrorMsg", "Invalid/missing Minimum 2 date mm/dd/yyyy ");
                txtMinDate2.Focus();
                functionReturnValue = true;
            }
            if (string.IsNullOrEmpty(txtMaxDate2.Text) & !String.IsNullOrEmpty(txtMinDate2.Text) & functionReturnValue == false)
            {
                Session.Add("ErrorMsg", "Invalid/missing Maximum 2 date mm/dd/yyyy ");
                txtMaxDate2.Focus();
                functionReturnValue = true;
            }
            if (!String.IsNullOrEmpty(txtMinDate2.Text) & !String.IsNullOrEmpty(txtMaxDate2.Text) & (radGender2.SelectedIndex == 0e & radGender2.SelectedIndex == 1) & functionReturnValue == false)
            {
                Session.Add("ErrorMsg", "Gender2 missing ");
                radGender2.Focus();
                functionReturnValue = true;
            }

            if (functionReturnValue == true)
            {
                lblError.Text = Session["ErrorMSG"].ToString();
            }
            return functionReturnValue;
        }


        //' '' ''Private Sub btnAddPeople_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPeople.Click
        //' '' ''    If DivisionId > 0 Then
        //' '' ''        Session.Add("PeopleID", 0)
        //' '' ''        Response.Redirect("PeopleUPD.aspx")
        //' '' ''    Else
        //' '' ''        Session.Add("ErrorMsg", "Must Add Division First")
        //' '' ''        Response.Redirect("MsgBox.aspx")
        //' '' ''    End If
        //' '' ''End Sub

        public void MsgBox(string Message)
        {
            Label strScript = new Label();
            strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "window.alert('" + Message + "')</script>";
            Page.Controls.Add(strScript);
        }


        private int PlayersAssigned()
        {
            int functionReturnValue = 0;
            if (Master.AccessType == "R")
                return functionReturnValue;
            var oTeams = new CSBC.Components.Season.ClsTeams();
            functionReturnValue = 0;
            try
            {
                functionReturnValue = oTeams.GetPlayersCount(DivisionId, (int)Session["CompanyID"], (int)Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "PlayersAssigned::" + ex.Message;
            }
            finally
            {
                oTeams = null;
            }
            return functionReturnValue;
        }

        private void DELRow(long RowID)
        {
            if (Master.AccessType == "R")
                return;
            var oDivisions = new CSBC.Components.Season.ClsDivisions();
            try
            {
                oDivisions.DELRow(RowID, (int)Session["CompanyID"], (int)Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "DELRow::" + ex.Message;
            }
            finally
            {
                oDivisions = null;
            }

        }

        private void ReassignDivision(long DivisionID)
        {
            var oDivisions = new CSBC.Components.Season.ClsDivisions();
            try
            {
                var _with9 = oDivisions;
                oDivisions.ReassignDiv(DivisionID, (int)Session["CompanyID"], (int)Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "ReassignDivision::" + ex.Message;
            }
            finally
            {
                oDivisions = null;
            }

        }

        //Private Sub UpdPlayers(ByVal DivisionID As Long)
        //    If Session["AccessType"] = "R" Then Exit Sub

        //    Dim oDivisions As New Season.ClsDivisions
        //    Try
        //        With oDivisions
        //            .TeamID = 0
        //            .DivisionID = 0
        //            oDivisions.UpdPlayers(DivisionID, Session["CompanyID"], Session["SeasonID"])
        //        End With
        //    Catch ex As Exception
        //        Session["ErrorMSG"] = "UpdPlayers::" & ex.Message
        //    Finally
        //        oDivisions = Nothing
        //    End Try
        //End Sub

        protected void grdDivisions_SelectedRowsChange(object sender, EventArgs e)
        {
            //DivisionId = grdDivisions.DisplayLayout.ActiveRow.Cells.FromKey("DivisionID").Value;
            ClearFields();
            LoadRow(DivisionId);
            this.txtName.Focus();
        }

       protected void btnNew_Command(object sender, CommandEventArgs e)
        {
            DivisionId = 0;
            ClearFields();
            this.txtName.Focus();
            LoadDivisions();
            grdTeams.Columns.Clear();
        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            //1) Put a label control beneath your button but sets its Visible property to false.  Name it lblConfirm.
            //2) Set the CommandArgument property of your button to "Confirm" (u can use any string, but "Confirm" will do for this example).
            //3) In your server-side click event, do this:
            if (Master.AccessType == "R")
                return;

            int PlayersCount = 0;
            Button btn = (Button)sender;
            PlayersCount = PlayersAssigned();
            if (PlayersCount > 0)
            {
                Session["ErrorMSG"] = "(" + PlayersCount + ") players must be removed before deleting";
                lblError.Text = Session["ErrorMSG"].ToString();
                //MsgBox(Session["ErrorMSG"]);
            }
            if (string.IsNullOrEmpty(lblDelete.Text))
                btn.CommandArgument = "Confirm";
            if (btn.CommandArgument == "Confirm")
            {
                lblDelete.Text = "*Click Delete button again to confirm.*";
                lblDelete.Visible = true;
                btn.CommandArgument = "Delete";
            }
            else if (btn.CommandArgument == "Delete")
            {
                if (DivisionId > 0)
                {
                    DELRow(DivisionId);
                    //grdTeams.Rows.Clear();
                    DivisionId = 0;
                    ClearFields();
                    LoadDivisions();
                    //grdTeams.Clear();
                }
                lblDelete.Text = "";
                lblDelete.Visible = false;
                btn.CommandArgument = "Confirm";
            }

            //You are using the label as a message box of sorts and the CommandArgument of the button to tell the click event whether 
            //you need to confirm or have confirmed.

        }

        //protected void btnSave_Click(object sender, System.EventArgs e)
        //{
        //    lblError.Text = "";
        //    Session["ErrorMSG"] = "";
        //    if (DivisionId > 0)
        //    {
        //        UpdDivision();
        //    }
        //    else
        //    {
        //        AddDivision();
        //    }
        //}

        //private void AddDivision()
        //{
        //    if (errorRTN() == true)
        //    {
        //        MsgBox("ERROR: " + lblError.Text);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
        //        ADDRow();
        //    if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
        //        //Interaction.MsgBox("New Record Added Successfully");
        //        if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
        //            LoadTeams(DivisionId);
        //    if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
        //        LoadRow(DivisionId);
        //    if (String.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
        //        ReassignDivision(DivisionId);
        //    if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
        //        LoadDivisions();
        //    lblError.Text = Session["ErrorMsg"].ToString();

        //}

        //private void UpdDivision()
        //{
        //    if (errorRTN() == true)
        //    {
        //        //Interaction.MsgBox("ERROR: " + lblError.Text);
        //        return;
        //    }
        //    if (String.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
        //    {
        //        UpdRow(DivisionId);

        //        //Interaction.MsgBox("Changes successfully completed");

        //        LoadTeams(DivisionId);

        //        LoadRow(DivisionId);

        //        ReassignDivision(DivisionId);

        //        LoadDivisions();
        //    }
        //    else
        //    {
        //        lblError.Text = Session["ErrorMsg"].ToString();
        //    }
        //}

        protected void btnSave_Command(object sender, CommandEventArgs e)
        {
            var division = new Division();
            DateTime date;
            if (lblDivisionID.Value == "")
                division.DivisionID = 0;
            else
                division.DivisionID = Convert.ToInt32(lblDivisionID.Value);
            division.CompanyID = Master.CompanyId;
            division.SeasonID = Master.SeasonId;
            division.Div_Desc = txtName.Text;
            division.MinDate = DateTime.Parse(txtMinDate.Text);
            division.MaxDate = DateTime.Parse(txtMaxDate.Text);
            if (radGender.SelectedIndex == 0)
                division.Gender = "M";
            if (radGender.SelectedIndex == 1)
                division.Gender = "F";
            if (DateTime.TryParse(txtMinDate2.Text, out date))
                division.MinDate2 = date;
            DateTime goodDate;
            if (DateTime.TryParse(txtMaxDate2.Text, out goodDate))
                division.MaxDate2 = DateTime.Parse(txtMaxDate2.Text);
            if (radGender2.SelectedIndex == 0)
                division.Gender2 = "M";
            if (radGender2.SelectedIndex == 1)
                division.Gender2 = "F";
            division.DraftVenue = txtVenue.Text;
            if (DateTime.TryParse(txtDate.Text, out date))
                division.DraftDate = date;
            division.DraftTime = txtTime.Text;
            division.DirectorID = Int32.Parse(cboAD.SelectedValue);
            division.CoDirectorID = 0;
            //cboAD.SelectedValue
            division.CreatedUser = Session["UserName"].ToString();
            using (var db = new CSBCDbContext())
            {
                try
                {
                    var rep = new DivisionRepository(db);
                    if (division.DivisionID == 0)
                    {
                        var newDivision = rep.Insert(division);
                        lblDivisionID.Value = newDivision.DivisionID.ToString();
                    }
                    else
                        rep.Update(division);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to save" + ex.InnerException);
                }
            }
            ReassignDivision(DivisionId);
            LoadDivisions();
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            var rep = new TeamVM();
            var divisionId = Convert.ToInt32(lblDivisionID.Value);
            var teams = rep.GetDivisionTeams(divisionId);
            if (teams.Count() > 0)
            {
                lblError.Text = "Cannot delete Division because teams exist!";
            }
            else
            {
                var repDivision = new DivisionRepository(new CSBCDbContext());
                repDivision.Delete(repDivision.GetById(divisionId));
                LoadDivisions();
                DivisionId = 0;
                //LoadTeams();
            }
        }

        protected void grdDivisions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var id = e.CommandArgument.ToString();
            var rep = new DivisionRepository(new CSBCDbContext());
            var division = rep.GetById(Convert.ToInt32(id));
            DivisionId = division.DivisionID;
            LoadRow(division);
            LoadTeams(DivisionId);
        }

        protected void grdTeams_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "SelectCoach":
                    Session["CoachID"] = e.CommandArgument;
                    Response.Redirect(Master.CoachForm);
                    break;

                case "SelectTeam":
                    Session["TeamID"] = e.CommandArgument;
                    Response.Redirect(Master.TeamForm);
                    break;

                case "SelectColor":
                    Session["ColorID"] = e.CommandArgument;
                    Response.Redirect(Master.ColorForm);
                    break;

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
            ClearFields();
            LoadDivisions();
            LoadDirectors();
        }

    }

}

