using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AjaxControlToolkit;
using CSBC.Components;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class People1 : BaseForm
    {
        private const string sBalance = "Balance: ";
        private const string sBalanceIsZero = "Balance: $0";

        public int PlayerId { get; set; }

        protected override void Page_Load(System.Object sender, System.EventArgs e)
        {
            Session["Title"] = "People";
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                if (Master.PeopleId != 0)
                {
                    LoadPerson(Master.PeopleId);
                }
                else
                {
                    if ((Master.HouseId != 0) && (Master.CurrentMode == CSBCAdminMasterPage.AppModes.AddHouseMember))
                    {
                        ReadHouse();
                        PlayerHistory1.LoadPlayerHistory(0);
                        if (txtLastName.Text == String.Empty)
                        {
                            txtLastName.Text = lnkHouseName.Text;
                        } 

                    }
                    else
                    {
                        Response.Redirect(Master.SearchPeopleForm);
                        //ClearFields();
                    }
                }
            }
        }

        private void LoadPerson(int personId)
        {
            var context = new CSBCDbContext();
            var rep = new PersonRepository(context);
            var person = rep.GetById(personId);
            Master.HouseId = person.HouseID;
            var repSeason = new SeasonRepository(context);
            var seasonId = repSeason.GetCurrentSeason(Master.CompanyId).SeasonID;
            GetPlayerInfo(seasonId, personId);
            if (Master.HouseId > 0)
                ReadHouse();

            LoadPersonDetails(person, PlayerVM.LastSeason(personId));
            PlayerHistory1.LoadPlayerHistory(personId);
            LoadComments(personId);
        }

        protected override void SetUser()
        {
            base.SetUser();

            btnRegister.Visible = (Master.PeopleId != 0);
            btnDelete.Visible = (Master.PeopleId != 0);
            btnAdd.Visible = (Master.PeopleId != 0);
            if (Master.AccessType == "R")
            {
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
                btnComments.Visible = false;
                btnRegister.Enabled = false;
                btnAdd.Enabled = false;
            }

        }

        private Player GetPlayerInfo(int seasonId, int peopleId)
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var player = rep.GetPlayerByPersonAndSeasonId(peopleId, seasonId);
            if (player == null)
            {
                //if none found get last season
                var newSeason = rep.GetLastSeasonPlayed(peopleId);
               //need to call something!
            }
            FillPlayerFields(player);

            return player;
        }

        protected void FillPlayerFields(Player player)
        {
            if (player != null)
            {
                btnTeam.Visible = true;

                if (player.Division != null)
                {
                    btnTeam.Text = player.Division.Div_Desc;
                }
                if (player.Team != null)
                {
                    btnTeam.Text = btnTeam.Text + " (Team: " + player.Team.TeamNumber + ")";
                    btnTeam.ToolTip = player.Team.TeamName + "-" + player.Team.TeamColor;
                }
                else
                {
                    btnTeam.Text = btnTeam.Text + ": Undrafted";
                }
                if (player.BalanceOwed != null)
                {
                    lblBalance.Text = sBalance + String.Format(player.BalanceOwed.ToString(), "{0:c}");
                }
                else
                {
                    lblBalance.Text = sBalanceIsZero;
                }
            }
            else
            {
                btnTeam.Visible = false;
                lblBalance.Text = sBalanceIsZero;
            }

        }

        private int GetPlayer()
        {
            int functionReturnValue = 0;
            functionReturnValue = 0;
            lblBalance.Text = "";
            btnTeam.Visible = false;
            var oPlayer = new CSBC.Components.Season.ClsPlayers();
            DataTable rsData = default(DataTable);
            try
            {
                rsData = oPlayer.GetRecords(Master.PeopleId, Master.CompanyId, Master.SeasonId);
                if ((rsData != null))
                {
                    if (rsData.Rows.Count > 0)
                    {
                        var row = rsData.Rows[0];
                        functionReturnValue = Convert.ToInt32(row["PlayerID"]);
                        btnTeam.Visible = true;
                        Session["TeamID"] = Convert.ToInt32(row["TeamID"]);
                        if (String.IsNullOrEmpty((string)row["TeamID"]))
                            Session["TeamID"] = 0;
                        Session["DivisionID"] = Convert.ToInt32(row["DivisionID"]);
                        if (row["DivisionID"] == null)
                            Session["DivisionID"] = 0;
                        btnTeam.Text = (string)row["Div_Desc"] + " (Team: " + (string)row["TeamNumber"] + ")";
                        if (row["TeamNumber"] == null)
                            btnTeam.Text = row["Div_Desc"].ToString() + ": Undrafted";
                        btnTeam.ToolTip = row["TeamName"].ToString() + "-" + row["TeamColor"].ToString();
                        lblBalance.Text = "Balance: " + String.Format((string)row["BalanceOwed"], "{0:c}");
                    }
                }
                Session.Add("LinkName", txtFirstName.Text + ", " + txtLastName.Text);
            }
            catch (Exception ex)
            {
                lblError.Text = "GetPlayer::" + ex.Message;
            }
            finally
            {
                oPlayer = null;
                Session.Add("LinkName", txtLastName.Text);
            }
            return functionReturnValue;
        }

        private void ReadHouse()
        {
            var rep = new HouseholdRepository(new CSBCDbContext());

            try
            {
                var house = rep.GetById(Master.HouseId);
                if ((house != null))
                {

                    lnkHouseName.Text = house.Name;
                    lblAddress.Text = " " + house.Address1;
                    lblCSZ.Text = house.City + " " + house.State + " " + house.Zip;
                    lblPhone.Text = house.Phone;
                    lblEmail.Text = house.Email;
                    var repPeople = new PersonRepository(new CSBCDbContext());
                    var people = repPeople.GetByHousehold(Master.HouseId);
                    if (people.Any<Person>())
                        LoadMembers(people.ToList<Person>());

                }
                Session.Add("LinkName", txtFirstName.Text + ", " + txtLastName.Text);
            }
            catch (Exception ex)
            {
                lblError.Text = "ReadHouse::" + ex.Message;
            }
            finally
            {
                Session.Add("LinkName", txtLastName.Text);
            }

            //LoadMembers(Master.HouseId);
        }

        private void LoadPersonDetails(int peopleId)
        {
            var rep = new PersonRepository(new CSBCDbContext());

            try
            {
                var person = rep.GetById(peopleId);
                //rsData = oPeople.LoadPeople(PeopleID, Master.CompanyId);
                if (person != null)
                {
                    LoadPersonDetails(person, PlayerVM.LastSeason(peopleId));
                }

                Session.Add("LinkName", txtFirstName.Text + ", " + txtLastName.Text);
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadPeople::" + ex.Message;
            }
            finally
            {
                Session.Add("LinkName", txtLastName.Text);
            }
            if (Master.HouseId > 0)
                ReadHouse();

            txtLastName.Focus();
        }

        private void LoadPersonDetails(Person person, Player player)
        {
            Master.HouseId = person.HouseID;

            txtFirstName.Text = person.FirstName;
            txtLastName.Text = person.LastName;
            txtCellPhone.Text = person.Cellphone;
            txtWorkPhone.Text = person.Workphone;
            //LoadPlayerHistory(person.PeopleID);
            //lblLastSeason.Text = "Last Season: UNKNOWN";
            //if (player.Season != null)
            //{
            //    lblLastSeason.Text = "Last Season: " + player.Season.Description;
            // }
            //lblLastRating.Text = "Last Season: " + ((player.Rating == null)? String.Empty : player.Rating.ToString());
            if (person.BirthDate != null)
                mskBirthDate.Text = person.BirthDate.Value.ToString("YYYY-MM-DD");
            else
            {
                mskBirthDate.Text = String.Empty;
            }

            if (!String.IsNullOrEmpty(person.Gender))
            {
                if (person.Gender == "M")
                {
                    radGender.Items[0].Selected = true;
                    radGender.Items[1].Selected = false;
                }
                else
                {
                    radGender.Items[1].Selected = true;
                    radGender.Items[0].Selected = false;
                }
            }
            txtSchool.Text = person.SchoolName;
            cmbGrade.SelectedIndex = person.Grade ?? -1;

            chkBC.Checked = person.BC ?? false;
            chkMoney.Items[0].Selected = person.FeeWaived ?? false;
            chkMoney.Items[1].Selected = person.GiftedLevelsUP > 0;

            chkVolunteer.Items[0].Selected = person.BoardOfficer ?? false;
            chkVolunteer.Items[1].Selected = person.BoardMember ?? false; ;
            chkVolunteer.Items[2].Selected = person.AD ?? false; ;
            chkVolunteer.Items[3].Selected = person.Sponsor ?? false; ;
            chkVolunteer.Items[4].Selected = person.SignUps ?? false; ;
            chkVolunteer.Items[5].Selected = person.TryOuts ?? false; ;
            chkVolunteer.Items[6].Selected = person.TeeShirts ?? false; ;
            chkVolunteer.Items[7].Selected = person.Printing ?? false; ;
            chkVolunteer.Items[8].Selected = person.Equipment ?? false; ;
            chkVolunteer.Items[9].Selected = person.Electrician ?? false; ;
            chkVolunteer.Items[10].Selected = person.AsstCoach ?? false; ;

            chkParentPlayer.Items[0].Selected = person.Parent ?? false; ;
            chkParentPlayer.Items[1].Selected = person.Coach ?? false; ;
            chkParentPlayer.Items[2].Selected = person.Player ?? false; ;
            txtComments.Enabled = true;
            btnComments.Enabled = true;
        }

        private void LoadPlayerHistory(int p)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PlayerHistoryVM(db);
                var players = rep.PlayerHistory(p);
                //gridPlayerHistory.DataSource = players;
                //gridPlayerHistory.DataBind();
            }
        }

        private void LoadComments(long peopleId)
        {
            var oComments = new CSBC.Components.Website.ClsComments();
            DataTable rsData = default(DataTable);
            try
            {
                rsData = oComments.GetRecords(peopleId, "P", Master.CompanyId);
                if ((rsData != null))
                {
                    for (int I = 0; I <= rsData.Rows.Count - 1; I++)
                    {
                        txtComments.Text = txtComments.Text + " " + rsData.Rows[I]["Comment"];
                    }
                }
                Session.Add("LinkName", txtFirstName.Text + ", " + txtLastName.Text);
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadComments::" + ex.Message;
            }
            finally
            {
                oComments = null;
                Session.Add("LinkName", txtLastName.Text);
            }
        }
        private void UpdatePerson()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PersonRepository(db);
                var person = rep.GetById(Master.PeopleId);
                person = PutRowInObject(person);
                rep.Update(person);
                //SetDivision();
                PlayerId = GetPlayer();
                //LoadPersonDetails(Master.PeopleId);
            }
        }
       
        private void ClearFields()
        {
            txtFirstName.Text = "";
            txtWorkPhone.Text = "";
            //txtCellPhone.Text = "";
            mskBirthDate.Text = "";
            txtSchool.Text = "";
            cmbGrade.SelectedIndex = 13;
            chkMoney.Items[0].Selected = false;
            chkMoney.Items[1].Selected = false;
            chkBC.Checked = false;
            chkParentPlayer.Items[0].Selected = false;
            chkParentPlayer.Items[1].Selected = false;
            chkParentPlayer.Items[2].Selected = false;
            chkVolunteer.Items[0].Selected = false;
            chkVolunteer.Items[1].Selected = false;
            chkVolunteer.Items[2].Selected = false;
            chkVolunteer.Items[3].Selected = false;
            chkVolunteer.Items[4].Selected = false;
            chkVolunteer.Items[5].Selected = false;
            chkVolunteer.Items[6].Selected = false;
            chkVolunteer.Items[7].Selected = false;
            chkVolunteer.Items[8].Selected = false;
            chkVolunteer.Items[9].Selected = false;
            chkVolunteer.Items[10].Selected = false;
            txtComments.Enabled = false;
            btnComments.Enabled = false;
        }

        private void AddPerson()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PersonRepository(db);
                var person = rep.Insert(PutRowInObject(new Person()));
                Master.PeopleId = person.PeopleID;
            }
        }

        private Person PutRowInObject(Person person)
        {
            DateTime date;
            //var person = new Person();
            person.HouseID = Master.HouseId;
            if (Master.PeopleId != 0)
                person.PeopleID = Master.PeopleId;
            person.LastName = txtLastName.Text;
            person.FirstName = txtFirstName.Text;
            person.Workphone = txtWorkPhone.Text;
            person.Cellphone = txtCellPhone.Text;
            if (DateTime.TryParse(mskBirthDate.Text, out date))
                person.BirthDate = DateTime.Parse(mskBirthDate.Text);
            person.BC = chkBC.Checked;
            if (radGender.Items[0].Selected)
                person.Gender = "M";
            if (radGender.Items[1].Selected)
                person.Gender = "F";
            person.SchoolName = txtSchool.Text;
            person.Grade = cmbGrade.SelectedIndex;
            person.FeeWaived = chkMoney.Items[0].Selected;
            person.GiftedLevelsUP = chkMoney.Items[1].Selected ? 1 : 0;
            person.Parent = chkParentPlayer.Items[0].Selected;
            person.Coach = chkParentPlayer.Items[1].Selected;
            person.Player = chkParentPlayer.Items[2].Selected;
            person.BoardOfficer = chkVolunteer.Items[0].Selected;
            person.BoardMember = chkVolunteer.Items[1].Selected;
            person.AD = chkVolunteer.Items[2].Selected;
            person.Sponsor = chkVolunteer.Items[3].Selected;
            person.SignUps = chkVolunteer.Items[4].Selected;
            person.TryOuts = chkVolunteer.Items[5].Selected;
            person.TeeShirts = chkVolunteer.Items[6].Selected;
            person.Printing = chkVolunteer.Items[7].Selected;
            person.Equipment = chkVolunteer.Items[8].Selected;
            person.Electrician = chkVolunteer.Items[9].Selected;
            person.AsstCoach = chkVolunteer.Items[10].Selected;
            person.BC = chkBC.Checked;
            person.CompanyID = Master.CompanyId;
            person.CreatedUser = Master.UserName;
            return person;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (errorRTN())
            {
                MasterVM.MsgBox(this, "ERROR: " + lblError.Text, MasterVM.MessageTypes.Error);
                return;
            }
            if (Master.PeopleId > 0)
            {
                if (String.IsNullOrEmpty((string)Session["ErrorMsg"]))
                {
                    UpdatePerson();
                    PlayerHistory1.LoadPlayerHistory(Master.PeopleId);
                    MasterVM.MsgBox(this, txtFirstName.Text + " Changes successfully completed", MasterVM.MessageTypes.Success);
                }
                else
                {
                    lblError.Text = Session["ErrorMsg"].ToString();
                }
                txtLastName.Focus();
            }
            else
            {
                if (String.IsNullOrEmpty((string)Session["ErrorMsg"]))
                {
                    AddPerson();
                    MasterVM.MsgBox(this, txtFirstName.Text + " New Record Added Successfully");
                    GetPlayerInfo(Master.SeasonId, Master.PeopleId);
                    ReadHouse();
                    PlayerHistory1.LoadPlayerHistory(Master.PeopleId);
                    SetUser();

                }
                lblError.Text = Session["ErrorMsg"] == null ? String.Empty : Session["ErrorMsg"].ToString();
            }
        }

        private void btnComments_Click(System.Object sender, System.EventArgs e)
        {
            if (Master.PeopleId == 0)
            {
                if (errorRTN() == false)
                    ADDPeople();
            }
            if (Master.PeopleId > 0)
            {
                if (errorRTN() == false)
                    UpdPeople(Master.PeopleId);
                Session["CallingScreen"] = "People.aspx";
                Session.Add("LinkID", Master.PeopleId);
                Session.Add("CommentType", "P");
                Session["Title"] = "Comments";
                Response.Redirect("Comments.aspx");
            }
        }

        private bool errorRTN()
        {
            bool functionReturnValue = false;
            functionReturnValue = false;
            if (Master.HouseId == 0)
            {
                lblError.Text = "Household missing";
                //imgHouse.Focus();
                functionReturnValue = true;
            }
            else if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                lblError.Text = "Name missing";
                txtFirstName.Focus();
                functionReturnValue = true;
            }
            else if (string.IsNullOrEmpty(txtLastName.Text))
            {
                lblError.Text = "Last Name missing";
                txtLastName.Focus();
                functionReturnValue = true;
            }
            else if ((mskBirthDate.Text == "") & (chkParentPlayer.Items[2].Selected == true))
            {
                lblError.Text = "BirthDate Missing ";
                mskBirthDate.Focus();
                functionReturnValue = true;
            }
            else if (radGender.Items[0].Selected == false & radGender.Items[1].Selected == false)
            {
                lblError.Text = "Gender Missing ";
                radGender.Focus();
                functionReturnValue = true;
            }
            else if ((chkParentPlayer.Items[0].Selected == false) &
                (chkParentPlayer.Items[1].Selected == false) &
                (chkParentPlayer.Items[2].Selected == false))
            {
                lblError.Text = "Parent, Coach or Player not selected";
                chkParentPlayer.Focus();
                functionReturnValue = true;
            }
            return functionReturnValue;
        }

        private void btnTeam_Click(System.Object sender, System.EventArgs e)
        {
            Response.Redirect(Master.TeamForm);
        }


        private void btnDelete_Click(System.Object sender, System.EventArgs e)
        {
            //1) Put a label control beneath your button but sets its Visible property to false.  Name it lblConfirm.
            //2) Set the CommandArgument property of your button to "Confirm" (u can use any string, but "Confirm" will do for this example).
            //3) In your server-side click event, do this:

            Button btn = (Button)sender;
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
                if (Master.PeopleId > 0)
                {
                    if (DeleteRow(Master.PeopleId))
                    {
                        //Call DELComments(PeopleId)
                        //Call DELPlayerPtn(PeopleId)
                        //if (Master.HouseId > 0)
                        //    LoadMembers(Master.HouseId);
                        ClearFields();
                        ReadHouse();
                        PlayerHistory1.LoadPlayerHistory(Master.PeopleId);
                        SetUser();
                        txtFirstName.Focus();
                    }
                }
                lblDelete.Text = "";
                lblDelete.Visible = false;
                btn.CommandArgument = "Confirm";
            }
            //You are using the label as a message box of sorts and the CommandArgument of the button to tell the click event whether 
            //you need to confirm or have confirmed.
        }

        private bool DeleteRow(Int32 personId)
        {
            bool retval = false;
            using (var db = new CSBCDbContext())
            {
                try
                {
                    var rep = new PersonRepository(db);
                    if (rep.CanDelete(personId))
                    {
                        rep.DeleteById(personId);
                        retval = true;
                    }
                    else
                    {
                        Session["ErrorMSG"] = "Cannot delete person - there may historical player registrations";
                    }
                }
                catch (Exception ex)
                {
                    Session["ErrorMSG"] = "DeleteRow::" + ex.Message;
                }
            }
            return retval;
        }

        private void LoadMembers(List<Person> people)
        {
            try
            {
                var _with3 = grdHouseholdMembers;
                _with3.DataSource = people;
                _with3.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadMembers::" + ex.Message;
            }
            finally
            {
                Session.Add("LinkName", txtLastName.Text);
            }
        }

        private long SetDivision()
        {
            long functionReturnValue = 0;

            var oPlayer = new CSBC.Components.Season.ClsPlayers();
            try
            {
                var _with5 = oPlayer;
                oPlayer.SetDivision(Master.PeopleId, Master.CompanyId, Master.SeasonId);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "SetDivision::" + ex.Message;
            }
            finally
            {
                oPlayer = null;
            }
            return functionReturnValue;
        }

        protected void grdMembers_SelectedRowsChange(object sender, EventArgs e)
        {
            //PeopleId = grdMembers.DisplayLayout.ActiveRow.Cells.FromKey("PeopleID").Value;
            if (Master.PeopleId > 0)
            {
                var rep = new SeasonRepository(new CSBCDbContext());
                var currentSeasonId = rep.GetCurrentSeason(Master.CompanyId).SeasonID;
                GetPlayerInfo(currentSeasonId, Master.PeopleId);
                LoadPersonDetails(Master.PeopleId);
                LoadComments(Master.PeopleId);
            }
        }

        protected void imgHouse_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (lnkHouseName.Text == "Name")
            {
                Session["FirstLetter"] = txtLastName.Text;
            }
            else
            {
                Session["FirstLetter"] = lnkHouseName.Text;
            }
            Session["SearchType"] = "Name";
            Session["CallingScreen"] = "People.aspx";
            Response.Redirect("SearchHouse.aspx");
        }

        private void imgFirstName_Click(System.Object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Master.PeopleId > 0)
                Master.HouseId = 0;
            Session["FirstLetter"] = txtFirstName.Text;
            Session["SearchType"] = "FirstName";
            Session["CallingScreen"] = "People.aspx";
            Response.Redirect("SearchPeople.aspx");
        }

        protected void lnkHouseName_Click(object sender, System.EventArgs e)
        {
            Response.Redirect(Master.HouseholdForm);
        }

        protected void imgLastName_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //If PeopleId > 0 Then Master.HouseId = 0
            Session["FirstLetter"] = txtLastName.Text;
            Session["SearchType"] = "LastName";
            Session["CallingScreen"] = "People.aspx";
            Response.Redirect("SearchPeople.aspx");
        }

        protected void btnRegister_Click(object sender, System.EventArgs e)
        {
            if (Master.PeopleId > 0)
            {
                if (errorRTN() == false)
                {
                    UpdatePerson();
                }
            }
            else
            {
                if (errorRTN() == false)
                {
                    AddPerson();
                }
            }

            if (Master.PeopleId > 0)
            {
                Response.Redirect(Master.PaymentsForm);
            }
        }

        protected void grdHouseholdMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var personId = Convert.ToInt32(e.CommandArgument);
            Master.PeopleId = personId;
            //Session["PeopleID"] = PeopleId;
            LoadPerson(personId);
            SetUser();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ReadHouse();
            Master.PeopleId = 0;
            PlayerHistory1.LoadPlayerHistory(Master.PeopleId);
            SetUser();
        }

        private void ADDPeople()
        {
            var oPeople = new CSBC.Components.Profile.ClsPeople();
            try
            {
                DateTime date;
                var _with2 = oPeople;
                _with2.HouseId = Master.HouseId;
                _with2.LastName = txtLastName.Text;
                _with2.FirstName = txtFirstName.Text;
                _with2.WorkPhone = txtWorkPhone.Text;
                //_with2.CellPhone = txtCellPhone.Text;
                if (DateTime.TryParse(mskBirthDate.Text, out date))
                    _with2.BirthDate = DateTime.Parse(mskBirthDate.Text);

                if (radGender.Items[0].Selected == true)
                    _with2.Gender = "M";
                if (radGender.Items[1].Selected == true)
                    _with2.Gender = "F";
                _with2.SchoolName = txtSchool.Text;
                _with2.Grade = cmbGrade.SelectedIndex;
                if (chkMoney.Items[0].Selected == true)
                {
                    _with2.FeeWaived = 1;
                }
                else
                {
                    _with2.FeeWaived = 0;
                }
                if (chkMoney.Items[1].Selected == true)
                {
                    _with2.GiftedLevelsUP = 1;
                }
                else
                {
                    _with2.GiftedLevelsUP = 0;
                }
                if (chkParentPlayer.Items[0].Selected == true)
                {
                    _with2.Parent = 1;
                }
                else
                {
                    _with2.Parent = 0;
                }
                if (chkParentPlayer.Items[1].Selected == true)
                {
                    _with2.Coach = 1;
                }
                else
                {
                    _with2.Coach = 0;
                }
                if (chkParentPlayer.Items[2].Selected == true)
                {
                    _with2.Player = 1;
                }
                else
                {
                    _with2.Player = 0;
                }
                if (chkVolunteer.Items[0].Selected == true)
                {
                    _with2.BoardOfficer = 1;
                }
                else
                {
                    _with2.BoardOfficer = 0;
                }
                if (chkVolunteer.Items[1].Selected == true)
                {
                    _with2.BoardMember = 1;
                }
                else
                {
                    _with2.BoardMember = 0;
                }
                if (chkVolunteer.Items[2].Selected == true)
                {
                    _with2.AD = 1;
                }
                else
                {
                    _with2.AD = 0;
                }
                if (chkVolunteer.Items[3].Selected == true)
                {
                    _with2.Sponsor = 1;
                }
                else
                {
                    _with2.Sponsor = 0;
                }
                if (chkVolunteer.Items[4].Selected == true)
                {
                    _with2.SignUps = 1;
                }
                else
                {
                    _with2.SignUps = 0;
                }
                if (chkVolunteer.Items[5].Selected == true)
                {
                    _with2.TryOuts = 1;
                }
                else
                {
                    _with2.TryOuts = 0;
                }
                if (chkVolunteer.Items[6].Selected == true)
                {
                    _with2.TeeShirts = 1;
                }
                else
                {
                    _with2.TeeShirts = 0;
                }
                if (chkVolunteer.Items[7].Selected == true)
                {
                    _with2.Printing = 1;
                }
                else
                {
                    _with2.Printing = 0;
                }
                if (chkVolunteer.Items[8].Selected == true)
                {
                    _with2.Equipment = 1;
                }
                else
                {
                    _with2.Equipment = 0;
                }
                if (chkVolunteer.Items[9].Selected == true)
                {
                    _with2.Electrician = 1;
                }
                else
                {
                    _with2.Electrician = 0;
                }
                if (chkVolunteer.Items[10].Selected == true)
                {
                    _with2.AsstCoach = 1;
                }
                else
                {
                    _with2.AsstCoach = 0;
                }
                if (chkBC.Checked == true)
                {
                    _with2.BC = 1;
                }
                else
                {
                    _with2.BC = 0;
                }
                _with2.CreatedUser = Master.UserName;
                oPeople.UpdRow(0, Master.CompanyId, Master.TimeZone);
                Master.PeopleId = (int)_with2.PeopleId;
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "ADDPeople::" + ex.Message;
            }

            LoadPersonDetails(Master.PeopleId);
        }
        private void UpdPeople(Int32 iPeopleID)
        {
            var oPeople = new CSBC.Components.Profile.ClsPeople();
            try
            {
                DateTime date;
                var _with1 = oPeople;
                _with1.LastName = txtLastName.Text;
                _with1.FirstName = txtFirstName.Text;
                _with1.WorkPhone = txtWorkPhone.Text;
                //_with1.CellPhone = txtCellPhone.Text;
                if (DateTime.TryParse(mskBirthDate.Text, out date))
                    _with1.BirthDate = DateTime.Parse(mskBirthDate.Text);
                _with1.HouseId = Master.HouseId;
                if (radGender.Items[0].Selected == true)
                    _with1.Gender = "M";
                if (radGender.Items[1].Selected == true)
                    _with1.Gender = "F";
                _with1.SchoolName = txtSchool.Text;
                _with1.Grade = cmbGrade.SelectedIndex;
                if (chkMoney.Items[1].Selected == true)
                {
                    _with1.GiftedLevelsUP = 1;
                }
                else
                {
                    _with1.GiftedLevelsUP = 0;
                }
                if (chkMoney.Items[0].Selected == true)
                {
                    _with1.FeeWaived = 1;
                }
                else
                {
                    _with1.FeeWaived = 0;
                }
                if (chkParentPlayer.Items[0].Selected == true)
                {
                    _with1.Parent = 1;
                }
                else
                {
                    _with1.Parent = 0;
                }
                if (chkParentPlayer.Items[1].Selected)
                {
                    _with1.Coach = 1;
                }
                else
                {
                    _with1.Coach = 0;
                }
                if (chkParentPlayer.Items[2].Selected == true)
                {
                    _with1.Player = 1;
                }
                else
                {
                    _with1.Player = 0;
                }
                if (chkVolunteer.Items[0].Selected)
                {
                    _with1.BoardOfficer = 1;
                }
                else
                {
                    _with1.BoardOfficer = 0;
                }
                if (chkVolunteer.Items[1].Selected)
                {
                    _with1.BoardMember = 1;
                }
                else
                {
                    _with1.BoardMember = 0;
                }
                if (chkVolunteer.Items[2].Selected)
                {
                    _with1.AD = 1;
                }
                else
                {
                    _with1.AD = 0;
                }
                if (chkVolunteer.Items[3].Selected)
                {
                    _with1.Sponsor = 1;
                }
                else
                {
                    _with1.Sponsor = 0;
                }
                if (chkVolunteer.Items[4].Selected)
                {
                    _with1.SignUps = 1;
                }
                else
                {
                    _with1.SignUps = 0;
                }
                if (chkVolunteer.Items[5].Selected)
                {
                    _with1.TryOuts = 1;
                }
                else
                {
                    _with1.TryOuts = 0;
                }
                if (chkVolunteer.Items[6].Selected)
                {
                    _with1.TeeShirts = 1;
                }
                else
                {
                    _with1.TeeShirts = 0;
                }
                if (chkVolunteer.Items[7].Selected)
                {
                    _with1.Printing = 1;
                }
                else
                {
                    _with1.Printing = 0;
                }
                if (chkVolunteer.Items[8].Selected)
                {
                    _with1.Equipment = 1;
                }
                else
                {
                    _with1.Equipment = 0;
                }
                if (chkVolunteer.Items[9].Selected)
                {
                    _with1.Electrician = 1;
                }
                else
                {
                    _with1.Electrician = 0;
                }
                if (chkVolunteer.Items[10].Selected)
                {
                    _with1.AsstCoach = 1;
                }
                else
                {
                    _with1.AsstCoach = 0;
                }
                if (chkBC.Checked)
                {
                    _with1.BC = 1;
                }
                else
                {
                    _with1.BC = 0;
                }
                oPeople.UpdRow((long)iPeopleID, Master.CompanyId, Master.TimeZone);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "UpdPeople::" + ex.Message;
            }
            finally
            {
                oPeople = null;
            }
            //PlayerId = GetPlayer()
            //If PlayerId > 0 Then
            SetDivision();
            PlayerId = GetPlayer();
            //End If
            LoadPersonDetails(Master.PeopleId);
        }

        //Private Sub DELComments(ByVal RowID As Long)
        //    If Session["AccessType"] = "R" Then Exit Sub
        //    Dim oComments As New Website.ClsComments
        //    Try
        //        oComments.DeleteRow(0, RowID, "P", Master.CompanyId)
        //    Catch ex As Exception
        //        Session["ErrorMSG"] = "DELComments::" & ex.Message
        //    Finally
        //        oComments = Nothing
        //    End Try
        //End Sub

        //Private Sub DELPlayerPtn(ByVal RowID As Long)
        //    If Session["AccessType"] = "R" Then Exit Sub
        //    Dim oPlayer As New Season.ClsPlayers
        //    Try
        //        With oPlayer
        //            oPlayer.DELPlayerByPeople(RowID, Master.CompanyId, Master.SeasonId)
        //        End With
        //    Catch ex As Exception
        //        Session["ErrorMSG"] = "DELPlayerPtn::" & ex.Message
        //    Finally
        //        oPlayer = Nothing
        //    End Try

        //End Sub
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
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
                if (Master.PeopleId > 0)
                {
                    if (DeleteRow(Master.PeopleId))
                    {
                        //Call DELComments(PeopleId)
                        //Call DELPlayerPtn(PeopleId)
                        //if (Master.HouseId > 0)
                        //    LoadMembers(Master.HouseId);
                        ClearFields();
                        ReadHouse();
                        PlayerHistory1.LoadPlayerHistory(Master.PeopleId);
                        SetUser();
                        txtFirstName.Focus();
                    }
                }
                lblDelete.Text = "";
                lblDelete.Visible = false;
                btn.CommandArgument = "Confirm";
            }
        }
    }
}
