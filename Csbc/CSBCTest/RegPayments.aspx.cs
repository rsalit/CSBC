using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using CSBC.Admin.Web.ViewModels;
using CSBC.Components;
using CSBC.Components.Season;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;

namespace CSBC.Admin.Web
{
    public partial class RegPayments : BaseForm
    {
        private int playerId;
        public int PlayerId
        {
            get
            {
                if (Session["PlayerID"] == null)
                {
                    return 0;
                }
                else
                {
                    return (int)Session["PlayerID"];
                }
            }
            set { Session["PlayerID"] = value; }
        }


        protected override void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "Payments";
            base.Page_Load(sender, e);
            if (Page.IsPostBack == false)
            {
                InitPage();
                // Set the focus
                SetFocus(txtDraftID);
            }
        }

        private void InitPage()
        {
            Session.Remove("DivisionID");
            LoadDivisions();
            if (Master.PeopleId > 0)
            {
                ClearFields();
                LoadPeople(Master.PeopleId);
                PlayerId = GetPlayerId();
                if (PlayerId > 0)
                {
                    LoadPlayer(PlayerId);
                }
                var divisionId = GetDivision();
                if (Master.DivisionId > 0)
                {
                    cboDivisions.SelectedValue = Master.DivisionId.ToString();
                    LoadSeasonPlayers();
                }
                else
                {
                    btnTeam.Text = "*NO DIVISION FOUND*";
                    btnTeam.Visible = true;
                    //what to do? Should we not allow saving?
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
                //btnOR.Enabled = false;
                cboDivisions.Enabled = true;
                //lnkPlayerName.Enable = true;
                grdPlayers.Enabled = true;
            }
        }

        private void LoadDivisions()
        {
            var rep = new DivisionRepository(new CSBCDbContext());
            var divisions = rep.GetDivisions(Master.SeasonId).ToList();
            try
            {
                if (divisions.Any())
                {
                    var _with1 = cboDivisions;
                    _with1.DataSource = divisions;
                    _with1.DataValueField = "DivisionID";
                    _with1.DataTextField = "Div_Desc";
                    _with1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadDivisions::" + ex.Message;
            }
        }

        private void ClearFields()
        {
            mskPayDate.Text = DateTime.Today.ToShortDateString();
            mskAmount.Text = GetSeasonFee().ToString();
            mskBalance.Text = "0";
            txtDraftNotes.Text = "";
            txtMemo.Text = "";
            Session.Add("PayID", 0);
            txtDraftID.Text = "";
            txtDraftNotes.Text = "";
            cboRating.SelectedIndex = 0;
            btnTeam.Visible = false;
            //chkPlaysDown.Checked = False
            PlaysDownUp.SelectedIndex = 0;
            //lblbm.Visible = false;
        }

        private void LoadPeople(int peopleId)
        {
            var rep = new PersonRepository(new CSBCDbContext());
            var person = rep.GetById(peopleId);
            try
            {
                if (person != null)
                {
                    LoadPerson(person);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadPeople::" + ex.Message;
            }
        }

        private void LoadPerson(Person person)
        {
            if (person.HouseID != 0)
            {
                Session["HouseID"] = person.HouseID;
            }
            lnkName.Text = person.LastName + ", " + person.FirstName;
            if (person.Household != null)
            {
                lblPhone.Text = "";
            }
            else
            {
                lblPhone.Text = person.Household.Phone;
            }
            lblAddress.Text = person.Household.Address1;
            lblCSZ.Text = person.Household.City + " " + person.Household.State + " " + person.Household.Zip;
            if (person.FeeWaived == true)
                lblBM.Visible = true;

            txtPlaysUp.Text = person.GiftedLevelsUP.ToString();
        }

        private int GetPlayerId()
        {
            var PlayerId = 0;
            var rep = new PlayerRepository(new CSBCDbContext());
            var player = rep.GetPlayerByPersonAndSeasonId(Master.PeopleId, Master.SeasonId);
            if (player == null)
                player = rep.GetByPeopleId(Master.PeopleId);
            else
            {
                ClearPaymentFields();
            }
            if (player != null)
                PlayerId = player.PlayerID;
            return PlayerId;
        }

        private void ClearPaymentFields()
        {
            mskPayDate.Text = DateTime.Today.ToShortDateString();
            mskAmount.Text = Convert.ToDecimal(string.Format("{0:F2}", GetSeasonFee())).ToString();
            mskBalance.Text = Convert.ToDecimal(string.Format("{0:F2}", 0 )).ToString();
            txtDraftNotes.Text = "";
            txtMemo.Text = "";
            txtDraftID.Text = "";
            txtDraftNotes.Text = "";
            
            btnTeam.Visible = false;
            //chkPlaysDown.Checked = False
            PlaysDownUp.SelectedIndex = 0;

        }

        private void LoadPlayer(int playerId)
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var player = rep.GetById(playerId);
            try
            {
                if (player != null)
                    LoadPlayer(player);
                else
                {
                    btnTeam.Visible = false;
                    txtDraftID.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadPlayer::" + ex.Message;
            }
        }

        private void LoadPlayer(Player player)
        {
            if (player.SeasonID == Master.SeasonId)
            {
                txtDraftID.Text = player.DraftID;
                txtDraftNotes.Text = player.DraftNotes;
                if ((player.Rating == null))
                {
                    cboRating.SelectedIndex = 0;
                }
                else
                {
                    cboRating.SelectedIndex = (int)player.Rating;
                }
                btnTeam.Visible = true;
                if (player.Team != null)
                {
                    btnTeam.Text = player.Division.Div_Desc +
                                    " (Team: " + player.Team.TeamNumber + ")";
                }
                else
                {
                    if (player.Division != null)
                        btnTeam.Text = player.Division.Div_Desc + ": Undrafted";
                }
                Session["TeamID"] = player.TeamID;
                if (player.Team == null)
                    Session["TeamID"] = 0;
                //btnTeam.ToolTip = player.TeamName") + "-" + player.TeamColor;
                Session["DivisionID"] = player.DivisionID;
                if (player.DivisionID == null)
                    Session["DivisionID"] = 0;
                if (player.PaidDate != null)
                {
                    mskPayDate.Text = player.PaidDate.Value.ToShortDateString();
                }
                else
                {
                    mskPayDate.Text = "";
                }
                if (player.PaidAmount != null)
                     mskAmount.Text = Convert.ToDecimal(string.Format("{0:F2}", player.PaidAmount)).ToString();
                if (player.BalanceOwed != null)
                    mskBalance.Text = Convert.ToDecimal(string.Format("{0:F2}", player.BalanceOwed)).ToString();
  
                txtMemo.Text = player.NoteDesc;
                txtCheck.Text = player.CheckMemo;
                txtDraftNotes.Text = player.DraftNotes;
                radPayment.Enabled = true;
                radPayment.Items[0].Selected = false;
                radPayment.Items[1].Selected = false;
                radPayment.Items[2].Selected = false;
                radPayment.Items[3].Selected = false;
                if (player.PayType != null)
                {
                    if (player.PayType.ToUpper() == "CHECK")
                        radPayment.Items[0].Selected = true;
                    if (player.PayType.ToUpper() == "CC")
                        radPayment.Items[1].Selected = true;
                    if (player.PayType.ToUpper() == "OR")
                    {
                        radPayment.Items[2].Selected = true;
                        radPayment.Enabled = false;
                    }
                    if (player.PayType.ToUpper() == "CASH")
                        radPayment.Items[3].Selected = true;
                }
            }
            else
            {
                mskPayDate.Text = "";
                Session["TeamID"] = 0;
                cboRating.SelectedIndex = 0;
                txtDraftID.Text = "";
                txtDraftNotes.Text = "";
                Session["TeamID"] = 0;
            }
            chkWaived.Items[0].Selected = player.Scholarship ?? false;
            chkWaived.Items[1].Selected = player.Rollover?? false;
            chkWaived.Items[2].Selected = player.FamilyDisc ?? false;
            chkWaived.Items[3].Selected = player.AD ?? false;
            //chkWaived.Items(4).Selected() = player.PartialRefund")
            if (player.PlaysDown == true)
            {
                PlaysDownUp.SelectedIndex = 2;
            }
            else if (player.PlaysUp == true)
            {
                PlaysDownUp.SelectedIndex = 1;
            }
            else
            {
                PlaysDownUp.SelectedIndex = 0;
            }
            /*if this is a player that has a record but is not a current player set Player ID to 0 */
            if (player.SeasonID != Master.SeasonId)
                PlayerId = 0;
        }

        private int GetDivision()
        {
            ClsDivisions oDivisions = new CSBC.Components.Season.ClsDivisions();
            var rep = new DivisionRepository(new CSBCDbContext());
            var rsData = rep.GetPlayerDivision(Master.CompanyId, Master.SeasonId, Master.PeopleId);
            try
            {
                if (rsData == 0)
                {
                    Master.DivisionId = 0;
                }
                else
                {
                    Master.DivisionId = rsData;
                }
                return Master.DivisionId;
            }
            catch (Exception ex)
            {
                lblError.Text = "GetDivision::" + ex.Message;
                return 0;
            }
        }

        private void LoadSeasonPlayers()
        {
            try
            {
                //when division is selected master Division ID is changed
                var vm = new PlayerVM();
                var players = vm.GetDivisionPlayers(Master.DivisionId);
                grdPlayers.DataSource = players;
                grdPlayers.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadGrid::" + ex.Message;
            }
        }

        private decimal GetSeasonFee()
        {
            decimal functionReturnValue = 0;
            var oSeason = new SeasonRepository(new CSBCDbContext());
            functionReturnValue = 0;
            try
            {
                var season = oSeason.GetById(Master.SeasonId);
                functionReturnValue = Convert.ToDecimal(season.ParticipationFee);
            }
            catch (Exception ex)
            {
                lblError.Text = "GetSeasonFee::" + ex.Message;
            }
            return functionReturnValue;
        }

        public void MsgBox(string Message)
        {
            Label strScript = new Label();
            strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "window.alert('" + Message + "')</script>";
            Page.Controls.Add(strScript);
        }

        #region Crud Stuff
        private void SavePlayer()
        {
            if (PlayerId == 0)
            {
                AddPlayer();
            }
            else
            {
                UpdRow();
            }
        }

        private void AddPlayer()
        {
            var player = PutRowInObject();
            using (var db = new CSBCDbContext())
            {
                var rep = new PlayerRepository(db);
                player = rep.Insert(player);
                PlayerId = player.PlayerID;
            }
        }

        private Player PutRowInObject()
        {
            var player = new Player();
            try
            {
                if (PlayerId != 0)
                    player.PlayerID = PlayerId;
                player.PlaysDown = false;
                player.PlaysUp = false;
                if (PlaysDownUp.Items[2].Selected == true)
                {
                    player.PlaysDown = true;
                }
                else if (PlaysDownUp.Items[1].Selected == true)
                {
                    player.PlaysUp = true;
                }
                player.CompanyID = Master.CompanyId;
                player.SeasonID = Master.SeasonId;
                player.DivisionID = Master.DivisionId;
                player.PeopleID = Master.PeopleId;
                player.DraftID = txtDraftID.Text;
                player.DraftNotes = txtDraftNotes.Text;
                player.Rating = cboRating.SelectedIndex;
                player.PaidDate = Convert.ToDateTime(mskPayDate.Text);
                player.PaidAmount = Convert.ToDecimal(mskAmount.Text);
                player.BalanceOwed = Convert.ToDecimal(mskBalance.Text);
                player.CheckMemo = txtCheck.Text;
                if (radPayment.Items[0].Selected == true)
                    player.PayType = "CHECK";
                if (radPayment.Items[1].Selected == true)
                    player.PayType = "CC";
                if (radPayment.Items[2].Selected == true)
                    player.PayType = "CHECK";
                if (radPayment.Items[3].Selected == true)
                    player.PayType = "CASH";
                player.Scholarship = chkWaived.Items[0].Selected;
                player.RefundBatchID = 0;
                player.Rollover = (chkWaived.Items[1].Selected);
                player.FamilyDisc = (chkWaived.Items[2].Selected);
                player.AD = chkWaived.Items[3].Selected;
                player.OutOfTown = chkWaived.Items[4].Selected;
                //player.UUserID = Master.UserId;
                player.CreatedUser = Master.UserName;

                return player;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return player;
            }
        }

        private void UpdRow()
        {
            var player = PutRowInObject();
            using (var db = new CSBCDbContext())
            {
                var rep = new PlayerRepository(db);
                rep.Update(player);
            }
        }

        private bool ValidateForm()
        {
            bool functionReturnValue = false;
            int x;
            DateTime date;
            functionReturnValue = false;
            //txtDraftID.Text = txtDraftID.Text.Trim(" ");
            if (!DateTime.TryParse(mskPayDate.Text, out date))
            {
                MsgBox("Date Missing ");
                functionReturnValue = true;
            }
            else if (string.IsNullOrEmpty(mskAmount.Text))
            {
                MsgBox("Amount Missing ");
                functionReturnValue = true;
            }
            else if (string.IsNullOrEmpty(mskBalance.Text))
            {
                MsgBox("Incorrect Balance value");
                functionReturnValue = true;
            }
            else if (string.IsNullOrEmpty(txtDraftID.Text) & radPayment.Items[2].Selected == false)
            {
                MsgBox("Draft-ID Missing ");
                functionReturnValue = true;
            }
            else if (txtDraftID.Text.Length < 2)
            {
                MsgBox("Invalid Draft-ID, use at least a 2 digit number");
                functionReturnValue = true;
            }
            if (txtDraftID.Text.Length == 1)
                txtDraftID.Text = "0" + txtDraftID.Text;
            return functionReturnValue;
        }

        private void DeleteRow(int rowId)
        {
            if (Master.AccessType == "R")
                return;
            var oPlayers = new PlayerRepository(new CSBCDbContext());
            try
            {
                oPlayers.DeleteById(rowId);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            PlayerId = 0;
        }
        #endregion


        private void imgLast_Click(System.Object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("PaymentSearch.aspx");
        }

        private void btnOR_Click(System.Object sender, System.EventArgs e)
        {

            var oPlayers = new PlayerRepository(new CSBCDbContext());
            var player = oPlayers.GetById(Convert.ToInt32(cboDivisions.SelectedItem.Value));
            player.DraftID = txtDraftID.Text;
            try
            {
                oPlayers.Update(player);
                //UpdateDraftID(cboDivisions.SelectedItem.Value, Session["CompanyID"], Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            LoadPlayer(PlayerId);
            LoadSeasonPlayers();
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
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
                if (PlayerId > 0)
                {
                    DeleteRow(PlayerId);
                    if (!String.IsNullOrEmpty(lblError.Text))
                        return;
                    Response.Redirect(Master.SearchRegistrationPaymentsForm);
                }
                lblDelete.Text = "";
                lblDelete.Visible = false;
                btn.CommandArgument = "Confirm";
            }
            
        }

        protected void cboDivisions_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Master.DivisionId = Convert.ToInt32(cboDivisions.SelectedItem.Value);
            LoadSeasonPlayers();
        }

        protected void lnkName_OnClick(object sender, EventArgs e)
        {
            Debug.Assert(Master.PeopleForm != null, "Master.PeopleForm != null");
            Response.Redirect(Master.PeopleForm);
        }

        protected void grdPlayers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var playerId = Convert.ToInt32(e.CommandArgument);
            //PeopleId = grdPlayers.DisplayLayout.ActiveRow.Cells.FromKey("PeopleID").Value;
            var rep = new PlayerRepository(new CSBCDbContext());
            var player = rep.GetById(playerId);
            ClearFields();
            LoadPeople(player.PeopleID);
            //GetPlayerId();
            LoadPlayer(playerId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                SavePlayer();
                MsgBox("Changes successfully completed");
            }
            if (PlayerId > 0)
                LoadPlayer(PlayerId);
            if (Session["DivisionID"] != null)
            {
                if (Session["DivisionID"].ToString() != "0")
                {
                    cboDivisions.SelectedValue = Session["DivisionID"].ToString();
                    LoadSeasonPlayers();
                }
            }
            btnTeam.Visible = true;

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
            InitPage();
        }

        protected void btnTeam_Click(object sender, EventArgs e)
        {
            //make TeamId global!
            Response.Redirect("Teams.aspx");
        }
    }
}

