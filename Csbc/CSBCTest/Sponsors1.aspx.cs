using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using CSBC.Components;
using CSBC.Core;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class Sponsors1 : BaseForm
    {
        
        public string ErrorMessage
        {
            get
            {
                if (Session["ErrorMSG"] == null)
                    return String.Empty;
                else
                    return Session["ErrorMSG"].ToString();
            }
            set { Session["ErrorMSG"] = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "Sponsors";
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                SetUser();
                this.txtSponsorName.Focus();
                LoadLists();
                if (Master.SponsorId == 0)
                {
                    if (Master.SponsorProfileId != 0)
                    {
                        LoadProfile(Master.SponsorProfileId);
                    }
                    else
                    {
                        ClearFields();
                        btnPayments.Enabled = false;
                        lnkComments.Enabled = false;
                        txtComments.Enabled = false;
                        lnkComments.Enabled = false;
                        btnPayments.Enabled = false;
                    }
                }
                else
                {
                    LoadRow(Master.SponsorId);
                    LoadComments(Master.SponsorId);
                }
            }
        }


        private void LoadLists()
        {
            LoadPlayers();
            LoadSponsors();
            LoadColors();
            LoadFees();
        }

        protected override void SetUser()
        {
            base.SetUser();
            if (Master.AccessType == "R")
            {
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
                lnkComments.Enabled = false;
                btnPayments.Enabled = false;
                txtSponsorName.Attributes.Add("disabled", "disabled");
            }
        }

        private void LoadSponsors()
        {
            var rep = new SponsorRepository(new CSBCDbContext());
            var sponsors = rep.GetSeasonSponsors(Master.SeasonId).ToList();
            try
            {
                var _with1 = grdSponsors;
                _with1.DataSource = sponsors;
                _with1.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadSponsors::" + ex.Message;
            }
        }

        private void LoadProfile(int sponsorProfileId)
        {
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var rep = new SponsorProfileRepository(db);
                    var sponsorProfile = rep.GetById(sponsorProfileId);

                    if (sponsorProfile != null)
                    {
                        txtSponsorName.Text = sponsorProfile.SpoName;
                        Master.SponsorProfileId = sponsorProfile.SponsorProfileID;
                        txtContact.Text = sponsorProfile.ContactName;
                        txtAddress.Text = sponsorProfile.Address;
                        txtCity.Text = sponsorProfile.City;
                        txtState.Text = sponsorProfile.State;
                        txtZip.Text = sponsorProfile.Zip;
                        txtPhone.Text = sponsorProfile.Phone;
                        txtWebsite.Text = sponsorProfile.URL;
                        txtEmail.Text = sponsorProfile.EMAIL;
                        //need to call function that gets balance
                        var repSponsor = new SponsorRepository(db);
                        var balance = repSponsor.GetSponsorBalance(sponsorProfile.SponsorProfileID);
                        lblBalance.Text = balance.ToString("C");
                        if (balance > 0)
                            lblBalance.ForeColor = System.Drawing.Color.Red;
                        if (balance == 0)
                            lblBalance.ForeColor = System.Drawing.Color.Black;
                        if (balance < 0)
                            lblBalance.ForeColor = System.Drawing.Color.DarkGreen;

                    }
                }

            }
            catch (Exception ex)
            {
                lblError.Text = "LoadProfile::" + ex.Message;
            }

        }

        private void LoadRow(int sponsorId)
        {
            Contract.Ensures(sponsorId != 0);
            try
            {
                var rep = new SponsorRepository(new CSBCDbContext());
                var sponsor = rep.GetById(sponsorId);

                if (sponsor != null)
                {
                    Master.SponsorId = sponsorId;
                    txtSponsorName.Text = sponsor.SponsorProfile.SpoName;
                    Master.SponsorProfileId = sponsor.SponsorProfileID;
                    txtContact.Text = sponsor.SponsorProfile.ContactName;
                    txtAddress.Text = sponsor.SponsorProfile.Address;
                    txtCity.Text = sponsor.SponsorProfile.City;
                    txtState.Text = sponsor.SponsorProfile.State;
                    txtZip.Text = sponsor.SponsorProfile.Zip;
                    txtPhone.Text = sponsor.SponsorProfile.Phone;
                    txtWebsite.Text = sponsor.SponsorProfile.URL;
                    txtEmail.Text = sponsor.SponsorProfile.EMAIL;
                    txtUniformName.Text = sponsor.ShirtName;
                    
                    var balance = rep.GetSponsorBalance(sponsor.SponsorProfile.SponsorProfileID);
                   
                    //lblBalance.Text = sponsor.SpoAmount.ToString();
                    lblBalance.Text = balance.ToString("C");
                    if (balance > 0)
                        lblBalance.ForeColor = System.Drawing.Color.Red;
                    if (balance == 0)
                        lblBalance.ForeColor = System.Drawing.Color.Black;
                    if (balance < 0)
                        lblBalance.ForeColor = System.Drawing.Color.DarkGreen;

                    cmbColors.SelectedValue = sponsor.Color1;
                    cmbColors2.SelectedValue = sponsor.Color2;
                    cmbSizes.SelectedValue = sponsor.ShirtSize;
                    if (sponsor.FeeID != null)
                    {
                        cmbFees.SelectedValue = LoadFee((decimal)sponsor.FeeID);
                    }
                    else
                    {
                        cmbFees.SelectedIndex = 0;
                    }

                }

                //If IsNumeric(rsData.Rows(0).Item("Master.SponsorId")) Then
                //    Master.SponsorId = rsData.Rows(0).Item("Master.SponsorId")
                //End If

                lnkComments.Enabled = true;
                txtComments.Enabled = true;
                btnPayments.Enabled = true;
                Session.Add("LinkName", txtSponsorName.Text);
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadRow::" + ex.Message;
            }

            GetKids(Master.SponsorId);
        }

        private string LoadFee(decimal fee)
        {
            var feeValue = cmbFees.SelectedValue;
            var item = cmbFees.Items.FindByText(fee.ToString());
            return item.Value;
        }

        private void GetKids(int sponsorId)
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var players = rep.GetSponsorPlayers(Master.SeasonId, Master.SponsorId);
            try
            {

                var grid = grdKids;
                grid.DataSource = players;
                grid.DataBind();

                /*grdKids.Columns.Add("Remove", "");
                var _with4 = grdKids.DisplayLayout.Bands(0).Columns;
                _with4.FromKey("PeopleID").Hidden = true;
                _with4.FromKey("PlayerID").Hidden = true;
                _with4.FromKey("Name").Width = 130;
                _with4.FromKey("Remove").Width = 80;
                _with4.FromKey("Remove").NullText = "Remove";
                _with4.FromKey("Remove").Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Button;
                _with4.FromKey("Remove").CellStyle.HorizontalAlign = HorizontalAlign.Center;
                 */

            }
            catch (Exception ex)
            {
                lblError.Text = "GetKids::" + ex.Message;
            }
        }

        private void LoadPlayers()
        {
            var rep = new PlayerRepository(new CSBCDbContext());
            var players = rep.GetPlayers(Master.SeasonId);
            try
            {
                if (players.Any<SeasonPlayer>())
                {
                    grdPlayers.DataSource = players;
                    grdPlayers.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadPlayers::" + ex.Message;
            }

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
                if (Master.SponsorId > 0)
                {
                    DELRow(Master.SponsorId);
                    ClearFields();
                    Master.SponsorId = 0;
                    LoadSponsors();
                    LoadPlayers();
                    grdKids.Controls.Clear();
                }
                lblDelete.Text = "";
                lblDelete.Visible = false;
                btn.CommandArgument = "Confirm";
            }

            //You are using the label as a message box of sorts and the CommandArgument of the button to tell the click event whether 
            //you need to confirm or have confirmed.

        }

        private void ClearFields()
        {
            txtSponsorName.Text = "";
            Master.SponsorProfileId = 0;
            txtContact.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "FL";
            txtZip.Text = "";
            txtPhone.Text = "";
            txtUniformName.Text = "";
            cmbColors.ClearSelection();
            cmbColors2.ClearSelection();
            cmbSizes.ClearSelection();
            cmbFees.SelectedIndex = 2;
            cmbSizes.SelectedValue = "LARGE";
            txtEmail.Text = "";
            txtWebsite.Text = "";
            lblBalance.Text = "";
            //lblBalance.ForeColor = Drawing.Color.Black;

            txtComments.Text = "";
            txtComments.Enabled = false;
            lnkComments.Enabled = false;
            btnPayments.Enabled = false;
            //grdKids.Clear()
            //grdKids.Columns.Clear()

        }

        private void DELRow(long sponsorId)
        {
            var oSponsor = new CSBC.Components.Season.clsSponsors();
            try
            {
                //need to implement method
                //oSponsor.DELRow(iMaster.SponsorId, Session["CompanyID"], Session["SeasonID"]);
            }
            catch (Exception ex)
            {
                ErrorMessage = "DELRow::" + ex.Message;
            }


        }

        public void MsgBox(string Message)
        {
            Label strScript = new Label();
            strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "window.alert('" + Message +
                             "')</script>";
            Page.Controls.Add(strScript);
        }

        private void LoadComments(long RowID)
        {
            var oComments = new CSBC.Components.Website.ClsComments();
            DataTable rsData = default(DataTable);
            txtComments.Text = "";
            try
            {
                rsData = oComments.GetRecords(0, Master.SponsorProfileId, "S", Master.CompanyId);
                if ((rsData != null))
                {
                    for (int I = 0; I <= rsData.Rows.Count - 1; I++)
                    {
                        txtComments.Text = txtComments.Text + " " + rsData.Rows[I]["Comment"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadComments::" + ex.Message;
            }
            finally
            {
                oComments = null;
            }

        }

        protected void grdSponsors_Click(object sender, EventArgs e)
        {
            ///this needs to changed - use template fields
            Master.SponsorId = Convert.ToInt32(grdSponsors.SelectedRow.Cells[0].Text);
            Master.SponsorProfileId = Convert.ToInt32(grdSponsors.SelectedRow.Cells[1].Text);
            LoadRow(Master.SponsorId);
            LoadComments(Master.SponsorId);
        }

        protected void grdPlayers_DblClick(object sender, EventArgs e)
        {
            if (Master.SponsorId > 0)
            {
                Session["PlayerID"] = Convert.ToInt32(grdPlayers.SelectedRow.Cells[0].Text);
                UpdPlayer(Master.SponsorId, (int)Session["PlayerID"]);
                if (ErrorMessage != null)
                {
                    lblError.Text = ErrorMessage.ToString();
                    return;
                }
                LoadPlayers();
                LoadRow(Master.SponsorId);
                GetKids(Master.SponsorId);
            }
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {

            if (errorRTN())
            {
                MsgBox("Error: " + ErrorMessage);
                return;
            }
            else
            {
                if (Master.SponsorId > 0)
                {
                    if (string.IsNullOrEmpty(ErrorMessage))
                        UpdateRow(Master.SponsorId);
                    if (string.IsNullOrEmpty(ErrorMessage))
                        MsgBox(txtSponsorName.Text + " Changes successfully completed");
                    lblError.Text = ErrorMessage;
                    txtSponsorName.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(ErrorMessage))
                        UpdateRow(0);
                    if (string.IsNullOrEmpty(ErrorMessage))
                        MsgBox(txtSponsorName.Text + " New Record Added Successfully");
                    //If ErrorMessage = "" Then Call ClearFields()
                    lblError.Text = ErrorMessage;
                    txtSponsorName.Focus();
                    //Master.SponsorId = 0
                    //Session["SponsorMaster.SponsorId"] = 0
                    LoadSponsors();
                }
            }
        }

        protected void grdKids_SelectedRowsChange(object sender, EventArgs e)
        {
            Session["PeopleID"] = Convert.ToInt32(grdKids.SelectedRow.Cells[0].Text);
            if ((int)Session["PeopleID"] > 0)
            {
                Master.SponsorId = 0;
                Session["PlayerID"] = 0;
                Response.Redirect("Payments.aspx");
            }
        }

        protected void grdKids_ClickCellButton(object sender, EventArgs e)
        {
            Session["PlayerID"] = Convert.ToInt32(grdKids.SelectedRow.Cells[1].Text);
            if ((int)Session["PlayerID"] > 0)
            {
                UpdPlayer(0, (int)Session["PlayerID"]);
                LoadRow(Master.SponsorId);
                LoadPlayers();
                GetKids(Master.SponsorId);
            }
        }

        private void UpdPlayer(int sponsorId, int PlayerID)
        {

            var oPlayers = new CSBC.Components.Season.ClsPlayers();
            try
            {
                var _with7 = oPlayers;
                _with7.SponsorID = Master.SponsorId;
                //oPlayers.UPDSponsor(Session["CompanyID"], Session["SeasonID"], PlayerID); ///implement this!!!!
            }
            catch (Exception ex)
            {
                ErrorMessage = "UpdPlayer::" + ex.Message;
            }
            finally
            {
                oPlayers = null;
            }
        }


        private void UpdateRow(int rowId)
        {
            using (var db = new CSBCDbContext())
            {
                var repSponsorProfile = new SponsorProfileRepository(db);
                var repSponsor = new SponsorRepository(db);

                try
                {
                    var sponsorProfile = CreateSponsorProfileObjectFromRow();
                    var sponsorProfileId = repSponsorProfile.Insert(sponsorProfile);
                    var sponsor = CreateSponsorObjectFromRow(sponsorProfileId.SponsorProfileID);

                    //sponsor.SpoAmount = cmbFees.SelectedItem.Text; // fee need to check

                    //need to update both tables
                    //oSponsors.UpdRow(RowID, Session["CompanyID"], Session["TimeZone"]);
                    
                    Master.SponsorId = sponsor.SponsorID;
                    Master.SponsorProfileId = sponsorProfile.SponsorProfileID;
                }
                catch (Exception ex)
                {
                    ErrorMessage = "UpdRow::" + ex.Message;
                }
            }
        }

        private SponsorProfile CreateSponsorProfileObjectFromRow()
        {
            var sponsorProfile = new SponsorProfile {
            SpoName = txtSponsorName.Text,
            SponsorProfileID = Master.SponsorProfileId,
            ContactName = txtContact.Text,
            Address = txtAddress.Text,
            City = txtCity.Text,
            State = txtState.Text,
            Zip = txtZip.Text,
            URL = txtWebsite.Text,
            EMAIL = txtEmail.Text,
            Phone = txtPhone.Text,
            CreatedUser = Master.UserId.ToString()
            };
            return sponsorProfile;
        }

        private Sponsor CreateSponsorObjectFromRow(int sponsorProfileId)
        {
            var sponsor = new Sponsor
            {
                HouseID = Convert.ToInt32(Session["HouseID"]),
                SeasonID = Master.SeasonId,
                ShirtName = txtUniformName.Text,
                Color1ID = Convert.ToInt32(cmbColors.SelectedItem.Value),
                Color2 = cmbColors2.SelectedItem.Value,
                ShirtSize = cmbSizes.SelectedItem.Text,
                SponsorProfileID = sponsorProfileId
            };
            return sponsor;
        }

        private void lnkComments_Click(System.Object sender, System.EventArgs e)
        {
            if (Master.SponsorProfileId == 0)
            {
                if (errorRTN() == false)
                    UpdateRow(0);
            }
            if (Master.SponsorProfileId > 0)
            {
                if (errorRTN() == false)
                    UpdateRow(Master.SponsorProfileId);
                Session["CallingScreen"] = "Sponsors.aspx";
                Session.Add("LinkID", Master.SponsorProfileId);
                Session.Add("CommentType", "S");
                Session["Title"] = "Comments";
                Response.Redirect("Comments.aspx");
            }

        }

        private bool errorRTN()
        {
            bool functionReturnValue = false;
            functionReturnValue = false;
            if (string.IsNullOrEmpty(txtSponsorName.Text))
            {
                lblError.Text = "Name missing";
                txtSponsorName.Focus();
                functionReturnValue = true;
            }
            else if ((!String.IsNullOrEmpty(txtEmail.Text)) & (IsEmail(txtEmail.Text) == false))
            {
                lblError.Text = "Invalid Email format";
                txtEmail.Focus();
                functionReturnValue = true;
                //ElseIf Not IsDate(mskBirthDate.Text) And chkParentPlayer.Items(2).Selected() = True Then
                //    lblError.Text = "BirthDate Missing "
                //    mskBirthDate.Focus()
                //    errorRTN = True
                //ElseIf radGender.Items(0).Selected = False And radGender.Items(1).Selected = False Then
                //    lblError.Text = "Gender Missing "
                //    radGender.Focus()
                //    errorRTN = True
                //ElseIf chkParentPlayer.Items(0).Selected = False And chkParentPlayer.Items(1).Selected = False And chkParentPlayer.Items(2).Selected = False Then
                //    lblError.Text = "Parent, Coach or Player not selected"
                //    chkParentPlayer.Focus()
                //    errorRTN = True
            }
            return functionReturnValue;
        }

        private bool IsEmail(string Email)
        {
            bool functionReturnValue = false;
            string pattern =
                "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
            var emailAddressMatch = Regex.Match(Email, pattern);
            if (emailAddressMatch.Success)
            {
                functionReturnValue = true;
            }
            else
            {
                functionReturnValue = false;
            }
            return functionReturnValue;
        }

        protected void imgSponsors_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Session["FirstLetter"] = txtSponsorName.Text;
            Master.SponsorId = 0;
            Response.Redirect("SearchSponsor.aspx");
        }

        protected void btnPayments_Click(object sender, System.EventArgs e)
        {
            if (Master.SponsorId > 0)
            {
                if (errorRTN() == false)
                {
                    UpdateRow(Master.SponsorId);
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (errorRTN() == false)
                {
                    UpdateRow(0);
                }
                else
                {
                    return;
                }
            }
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Redirect(Master.AccountingForm);
        }

        protected void grdKids_DblClick(object sender, EventArgs e)
        {
            Session["PeopleID"] = Convert.ToInt32(grdKids.SelectedRow.Cells[0].Text);
            Response.Redirect("People.aspx");
        }

        private void LoadColors()
        {
            var rep = new ColorRepository(new CSBCDbContext());
            try
            {
                var colors = rep.GetAll(Master.CompanyId).ToList();

                var list = cmbColors;
                list.DataSource = colors;
                list.DataValueField = "ID";
                list.DataTextField = "ColorName";
                list.DataBind();

                list = cmbColors2;
                list.DataSource = colors;
                list.DataValueField = "ID";
                list.DataTextField = "ColorName";
                list.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadColors::" + ex.Message;
            }

        }

        private void LoadFees()
        {
            var repSeasons = new SeasonRepository(new CSBCDbContext());

            try
            {
                var seasonFees = repSeasons.GetSeasonFees((int)Session["SeasonID"]);
                cmbFees.DataSource = seasonFees;
                cmbFees.DataValueField = "Name";
                cmbFees.DataTextField = "Amount";
                cmbFees.DataBind();
                //IQueryable<seasonFees> rsdata = seasonFees;
                //rsData = oSeasons.GetSeasonFees(Session["SeasonID"], Session["CompanyID"]);
                //if (rsData.Rows.Count == 0)
                //    return;

                /*var _with11 = cmbFees;
                _with11.DataSource = rsData;
                _with11.DataValueField = "Fee";
                _with11.DataTextField = "Fee";
                _with11.DataBind();
                _with11.SelectedIndex = 2;    */
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadFees::" + ex.Message;
            }

        }

        protected void lnkPrint_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("report.aspx?Report=Sponsors.rpt&amp;Type=pdf");
        }


        protected void grdSponsors_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Master.SponsorId = Convert.ToInt32(e.CommandArgument);
                //Master.SponsorProfileId = Convert.ToInt32(grdSponsors.SelectedRow.Cells[1].Text);
                LoadRow(Master.SponsorId);
                LoadComments(Master.SponsorId);
            }
        }

        protected void grd_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var keyId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Add")
            {
                var rep = new PlayerRepository(new CSBCDbContext());
                var player = rep.GetById(keyId);
                player.SponsorID = Master.SponsorId;
                rep.Update(player);
                //LoadPlayers();
                GetKids(Master.SponsorId);

            }
            else if (e.CommandName == "Remove")
            {
                var rep = new PlayerRepository(new CSBCDbContext());
                var player = rep.GetById(keyId);
                player.SponsorID = 0;
                rep.Update(player);
                GetKids(Master.SponsorId);
            }
            else if (e.CommandName == "SelectSponsor")
            {
                LoadRow(keyId);
            }
        }

        protected void btnSearch_OnClick(object sender, ImageClickEventArgs e)
        {
            Session["FirstLetter"] = txtSponsorName.Text;
            Master.SponsorId = 0;
            Response.Redirect("SearchSponsor1.aspx");
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            LoadLists();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddToSeason_Click(object sender, EventArgs e)
        {
            SponsorProfileVM.AddSponsorToSeason(Master.CompanyId, Master.SeasonId, Master.SponsorProfileId);

        }
    }
}

