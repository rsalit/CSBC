using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CSBC.Components;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class Households1 : BaseForm
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "Households";
            base.Page_Load(sender, e);
            //Put user code to initialize the page here
            if (Session["UserID"] == null)
                Response.Redirect("Login.aspx");

            if (!Page.IsPostBack)
            {
                Session["CallingScreen"] = "Households1.aspx";

                SetControls();
                this.txtName.Focus();
                if (Master.HouseId == 0)
                {
                    ClearFields();
                }
                else
                {
                    LoadHousehold(Master.HouseId);
                }
            }
        }

        protected override void SetUser()
        {
            base.SetUser();
        }

        private void SetControls()
        {
            if (Master.AccessType == "R")
            {
                btnAdd.Enabled = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
                btnComments.Visible = false;
            }
        }

        protected void LoadHousehold(int householdId)
        {
            LoadRow(householdId);
            LoadMembers(householdId);
            LoadComments(householdId);
        }

        private string GetUserNameByHouseholdId(int houseId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new UserRepository(db);
                var user = rep.GetUserByHouseId(houseId);
                if (user == null)
                    return String.Empty;
                else
                    return user.UserName;
            }
        }
        private void LoadRow(int houseId)
        {
            try
            {
                if (houseId != 0)
                {
                    var household = GetHouseHoldObject(houseId);
                    ClearFields();
                    if (household != null)
                    {
                        txtName.Text = household.Name;
                        txtAddress.Text = household.Address1;
                        txtAddress2.Text = household.Address2;
                        txtCity.Text = household.City;
                        txtState.Text = household.State;
                        txtZip.Text = household.Zip;
                        txtPhone.Text = household.Phone;
                        txtEmail.Text = household.Email;
                        chkEmail.Checked = (household.EmailList == null) ? false : (bool)household.EmailList;
                        hidEMail.Value = household.Email;
                        txtCityCard.Text = household.SportsCard;
                        lblUserName.Text = GetUserNameByHouseholdId(houseId);
                        //if (AccessType_User() == "U")
                        //	lblUserName.ToolTip = rsData.;
                        btnComments.Enabled = true;
                        txtComments.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadRow::" + ex.Message;
            }
        }

        private static Household GetHouseHoldObject(int rowId)
        {
            var rep = new HouseholdRepository(new CSBCDbContext());
            var household = rep.GetById(rowId);
            return household;
        }

        private void LoadMembers(long RowID)
        {
            //CSBC.Components.Profile.ClsHouseholds oPeople = new CSBC.Components.Profile.ClsHouseholds();
            //DataTable rsData = default(DataTable);

            try
            {
                var rep = new PersonRepository(new CSBCDbContext());
                var members = rep.GetByHousehold(Master.HouseId).ToList();
                if (members.Any<Person>())
                {
                    grdMembersNew.DataSource = members;
                    grdMembersNew.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadMembers::" + ex.Message;
            }
        }

        private void ClearFields()
        {
            //grdMembersNew.Controls.Clear();
            txtName.Text = "";
            txtAddress.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "CORAL SPRINGS";
            txtState.Text = "FL";
            txtZip.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            chkEmail.Checked = false;
            hidEMail.Value = "";
            txtCityCard.Text = "";
            txtComments.Enabled = false;
            btnComments.Enabled = false;
            Session["ErrorMsg"] = "";
        }

        private void UpdateHousehold()
        {
            if (errorRTN() == true)
            {
                MsgBox("ERROR: " + lblError.Text);
                return;
            }
            if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
                UpdRow(Master.HouseId);
            //If Session["ErrorMsg"] = "" Then Call UpdEmail(Session["HouseID"])
            if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
                MsgBox("Changes successfully completed");
            lblError.Text = Session["ErrorMsg"].ToString();
        }

        private void AddHouseholds()
        {
            if (errorRTN() == true)
            {
                MsgBox("ERROR: " + lblError.Text);
                return;
            }
            if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
                ADDRow();
            //If Session["ErrorMsg"] = "" Then Call UpdEmail(Session["HouseID"])
            if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
                MsgBox("New Record Added Successfully");
            lblError.Text = Session["ErrorMsg"].ToString();
            btnComments.Enabled = true;
            txtComments.Enabled = true;
        }


        private bool errorRTN()
        {
            bool functionReturnValue = false;
            functionReturnValue = false;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                if (Master.AccessType == "R")
                {
                    lblError.Text = "Update Not allowed";
                }
                else
                {
                    lblError.Text = "Name missing ";
                }
                functionReturnValue = true;
            }
            return functionReturnValue;
        }

        public void MsgBox(string Message)
        {
            Label strScript = new Label();
            //System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)

            //System.Web.HttpContext.Current.Response.Write("alert(""" & Message & """)" & vbCrLf)

            //System.Web.HttpContext.Current.Response.Write("</SCRIPT>")


            strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "window.alert('" + Message +
                             "')</script>";

            Page.Controls.Add(strScript);

        }

        private void RemoveMember(long PeopleID)
        {
            CSBC.Components.Profile.ClsHouseholds oHousehold = new CSBC.Components.Profile.ClsHouseholds();
            try
            {
                var _with5 = oHousehold;
                _with5.HouseId = 0;
                oHousehold.UpdMember(PeopleID, Master.CompanyId);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = ex.Message;
            }
            finally
            {
                oHousehold = null;
            }
        }

        private void LoadComments(long HouseID)
        {
            //Website.ClsComments oComments = new Website.ClsComments();
            //int I = 0;
            //DataTable rsData = default(DataTable);
            //try {
            //    rsData = oComments.GetRecords(0, HouseID, Session["CompanyID"]);
            //    txtComments.Text = "";
            //    if ((rsData != null)) {
            //        for (I = 0; I <= rsData.Rows.Count - 1; I++) {
            //            txtComments.Text = txtComments.Text + " " + rsData.Rows(I).Item("Comment") + Constants.vbCrLf;
            //        }
            //    }
            //} catch (Exception ex) {
            //    lblError.Text = ex.Message;
            //} finally {
            //    oComments = null;
            //}
        }
        #region CRUD stuff
        private void ADDRow()
        {
            using (var db = new CSBCDbContext())
            {
                try
                {
                    var rep = new HouseholdRepository(db);
                    var household = new Household();

                    household.Name = txtName.Text;
                    household.Address1 = txtAddress.Text;
                    household.Address2 = txtAddress2.Text;
                    household.City = txtCity.Text;
                    household.Email = txtEmail.Text;
                    household.EmailList = chkEmail.Checked;
                    household.SportsCard = txtCityCard.Text;
                    household.State = txtState.Text;
                    household.Zip = txtZip.Text;
                    household.Phone = txtPhone.Text;
                    household.CreatedUser = Master.UserName;
                    household.CompanyID = Master.CompanyId;
                    Master.HouseId = rep.Insert(household).HouseID;
                }
                catch (Exception ex)
                {
                    Session["ErrorMSG"] = ex.Message;
                }
            }
        }
        private void UpdRow(int rowId)
        {
            var householdRepository = new HouseholdRepository(new CSBCDbContext());
            var household = householdRepository.GetById(rowId);
            try
            {
                household.Name = txtName.Text;
                household.Address1 = txtAddress.Text;
                household.Address2 = txtAddress2.Text;
                household.City = txtCity.Text;
                household.Email = txtEmail.Text;

                household.EmailList = chkEmail.Checked;
                household.SportsCard = txtCityCard.Text;
                household.State = txtState.Text;
                household.Zip = txtZip.Text;
                household.Phone = txtPhone.Text;
                householdRepository.Update(household);
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = "UpdRow::" + ex.Message;
            }

        }
        private void DELRow(long houseId)
        {
            if (Master.AccessType == "R")
                return;
            CSBC.Components.Profile.ClsHouseholds oHouseholds = new CSBC.Components.Profile.ClsHouseholds();
            try
            {
                oHouseholds.DELRow(houseId, Master.CompanyId);
            }
            catch (Exception ex)
            {
                Session["ErrorMsg"] = ex.Message;
            }
            finally
            {
                oHouseholds = null;
            }
        }
        #endregion

        #region page events
        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (Master.HouseId > 0)
            {
                UpdateHousehold();
            }
            else
            {
                AddHouseholds();
            }
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text))
            {
                Session["PeopleID"] = 0;
                Session["FirstLetter"] = txtName.Text;
                Session["SearchType"] = "LastName";
                Master.CurrentMode = CSBCAdminMasterPage.AppModes.AddHouseMember;
                Response.Redirect(Master.PeopleForm);
            }
        }
        protected void grdMembers_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var command = e.CommandName;
            switch (command)
            {
                case "Remove":
                    var rep = new PersonRepository(new CSBCDbContext());
                    rep.RemoveFromHousehold(Convert.ToInt32(e.CommandArgument));
                    break;

                case "Select":
                    Session["PeopleID"] = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect(Master.PeopleForm);
                    break;
                case "Register":
                    var personId = Convert.ToInt32(e.CommandArgument);
                    Master.PeopleId = personId;
                    Response.Redirect(Master.PaymentsForm);
                    break;
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (Master.HouseId > 0)
            {
                UpdateHousehold();
            }
            else
            {
                AddHouseholds();
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
                if (Master.HouseId > 0)
                {
                    DELRow(Master.HouseId);
                    //If Session["ErrorMsg"] = "" Then Call DELComments(Session["HouseID"])
                    //If Session["ErrorMsg"] = "" Then Call DELUserPtn(Session["HouseID"])
                    if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
                        RemoveMember(Master.HouseId);
                    //If Session["ErrorMsg"] = "" Then Call DELEmail(Session["HouseID"])
                    lblError.Text = Session["ErrorMsg"].ToString();
                    ClearFields();
                    //grdMembers.Clear()
                    // grdMembersNew.Controls.Clear();
                    //grdMembers.ResetBands()
                    Master.HouseId = 0;
                }
                lblDelete.Text = "";
                lblDelete.Visible = false;
                btn.CommandArgument = "Confirm";
            }

            //You are using the label as a message box of sorts and the CommandArgument of the button to tell the click event whether 
            //you need to confirm or have confirmed.
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
                if (Master.HouseId > 0)
                {
                    DELRow(Master.HouseId);
                    //If Session["ErrorMsg"] = "" Then Call DELComments(Session["HouseID"])
                    //If Session["ErrorMsg"] = "" Then Call DELUserPtn(Session["HouseID"])
                    if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
                        RemoveMember(Master.HouseId);
                    //If Session["ErrorMsg"] = "" Then Call DELEmail(Session["HouseID"])
                    lblError.Text = Session["ErrorMsg"].ToString();
                    ClearFields();
                    //grdMembers.Clear()
                    //grdMembersNew.Controls.Clear();
                    //grdMembers.ResetBands()
                    Master.HouseId = 0;
                }
                lblDelete.Text = "";
                lblDelete.Visible = false;
                btn.CommandArgument = "Confirm";
            }
        }

   
        private void btnComments_Click(System.Object sender, System.EventArgs e)
        {
            if (Master.HouseId == 0)
            {
                if (errorRTN() == true)
                {
                    MsgBox("ERROR: " + lblError.Text);
                    return;
                }
                ADDRow();
            }

            if (Master.HouseId > 0)
            {
                if (errorRTN() == true)
                {
                    MsgBox("ERROR: " + lblError.Text);
                    return;
                }
                UpdRow(Master.HouseId);
                Session.Add("LinkID", Master.HouseId);
                Session.Add("CommentType", "H");
                Session["CallingScreen"] = "HouseHolds.aspx";
                Response.Redirect("Comments.aspx");
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
            //does anything change?
        }
        #endregion

        /// <summary>
        /// LoadSearchValuesinEmptyFields() - not used - not sure what it does
        /// </summary>
        private void LoadSearchValuesinEmptyFields()
        {
            /*string SearchType = Session["SearchType"] + "";
             switch (SearchType) {
                 case "Name":
                     txtName.Text = Session["FirstLetter"];
                     break;
                 case "Email":
                     txtEmail.Text = Session["FirstLetter"];
                     break;
                 case "City":
                     txtCity.Text = Session["FirstLetter"];
                     break;
                 case "Phone":
                     txtPhone.Text = Session["FirstLetter"];
                     break;
                 case "Address1":
                     txtAddress.Text = Session["FirstLetter"];
                     break;
              * */
        }
    }
}