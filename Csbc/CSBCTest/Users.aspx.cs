using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using CSBC.Components;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class Users : BaseForm
    {
        private string HouseHoldName;
        public int UserId
        {
            get
            {
                if (Session["UserId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(Session["UserId"]);
            }
            set
            {
                Session["UserId"] = value;
            }
        }
        private string HouseEmail;
        protected override void Page_Load(System.Object sender, System.EventArgs e)
        {
            Session["Title"] = "Users";
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                LoadRoles(0);
                LoadList();
                if (Session["CallingScreen"] == "Users.aspx")
                {
                    Session["CallingScreen"] = "";
                    //ReadHousehold(Master.HouseId);
                    lblHouseID.Text = Master.HouseId.ToString();
                    lblEmail.Text = HouseEmail;
                    lblHouseHold.Text = HouseHoldName;
                    btnSave.Enabled = true;
                    if (UserId > 0)
                    {
                        //gridUsers.Rows(Session["GridIndex"]).Selected = true;
                        LoadRow(UserId);

                        LoadRoles(UserId);
                    }
                }
            }
        }
        protected override void SetUser()
        {
            base.SetUser();

            if (Master.AccessType == "R")
            {
                btnSave.Enabled = false;
                //btnNew.Enabled = false;
                //btnDelete.Enabled = false;
                txtPWord.Enabled = false;
                txtPWord.TextMode = TextBoxMode.Password;
            }
        }

        //private List<>
        private void LoadRow(int id)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new UserRepository(db);
                try
                {
                    var user = rep.GetById(id);
                    if ((user != null))
                    {
                        lblId.Value = user.UserID.ToString();
                        txtName.Text = user.Name;
                        txtUserName.Text = user.UserName;
                        Master.HouseId = (int)user.HouseID;
                        txtPWord.Text = user.PWord.ToString();
                        radioUserType.SelectedValue = user.UserType.ToString(); //change this to radio
                        if (user.HouseID != 0)
                        {
                            var repHouse = new HouseholdRepository(db);
                            var house = repHouse.GetById(user.HouseID);
                            lblHouseHold.Text = house.Name;
                            lblEmail.Text = house.Email;
                            lblHouseID.Text = user.HouseID.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "LoadRow::" + ex.Message;
                }
            }
        }

        //private void ReadHousehold(long RowID)
        //{
        //    Profile.ClsHouseholds oHouseholds = new Profile.ClsHouseholds();
        //    DataTable rsData = default(DataTable);
        //    try
        //    {
        //        rsData = oHouseholds.GetRecords(RowID, Session["CompanyID"]);
        //        if ((rsData != null))
        //        {
        //            if (rsData.Rows.Count > 0)
        //            {
        //                HouseHoldName = rsData.Rows(0).Item("Name") + "";
        //                HouseEmail = rsData.Rows(0).Item("email") + "";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = "ReadHousehold::" + ex.Message;
        //    }
        //    finally
        //    {
        //        oHouseholds = null;
        //    }
        //}

        private void LoadList()
        {
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var rep = new UserRepository(db);
                    var users = new List<User>();
                    if (radioFilterUserType.SelectedValue != "5") //check to see if its not all
                    {
                       users = rep.GetByUserType(Convert.ToInt32(radioFilterUserType.SelectedValue)).ToList<User>();
                    }
                    else
                    {
                        users = rep.GetAll(Master.CompanyId).OrderBy(p => p.Name).ToList<User>();
                    }
                    var users2 = new List<UserVM>();
                    foreach(User user in users)
                    {
                        users2.Add(new UserVM { UserId = user.UserID, Name = user.Name });
                    }
                    gridUsers.DataSource = users2;
                    gridUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadList::" + ex.Message;
            }
        }

        private void LoadRoles(int id)
        {
            using (var db = new CSBCDbContext())
            {
                var repUsers = new UserVM();
                var roles = repUsers.Roles;
                listRoles.Items.Clear();
                foreach( String role in roles)
                {
                    listRoles.Items.Add( new ListItem(role.ToString()));
                }

                if (id > 0)
                {
                    var repRoles = new RoleRepository(db);
                    //var userRoles = repRoles.GetRoles(id).ToList<Role>();
                    foreach (ListItem item in listRoles.Items)
                    {
                        item.Selected = db.Roles.Any(r => r.UserID == id && r.ScreenName.ToUpper() == item.Text.ToUpper());
                    }
                }
            }
        }

        private void UpdRow(long RowID)
        {
            using (var db = new CSBCDbContext())
            {
                try
                {
                    var rep = new UserRepository(db);
                    var user = new User();
                   
                    user.HouseID = Convert.ToInt32(lblHouseID.Text);
                    user.Name = txtName.Text;
                    user.UserName = txtUserName.Text;
                    user.UserType = (Convert.ToInt32(radioUserType.SelectedValue));
                    user.PWord = txtPWord.Text;
                    user.CreatedUser = Master.UserName;
                    //user.Roles = GetRoles();
                    rep.Update(user);
                }
                catch (Exception ex)
                {
                    Session["ErrorMSG"] = "UpdRow::" + ex.Message;
                }
            }
        }

        private string GetRoles()
        {
            string sRoles = "";
            try
            {
                //for (Int32 I = 0; I <= lstRoles.Items.Count - 1; I++)
                //{
                //    if (lstRoles.Items(I).Selected == true)
                //    {
                //        if (sRoles > "")
                //            sRoles = sRoles + ", ";
                //        sRoles = sRoles + lstRoles.Items(I).Text;
                //    }
                //}

            }
            catch (Exception ex)
            {
                lblError.Text = "GetRoles::" + ex.Message;
            }
            return sRoles;
        }

        private void ClearFields()
        {
            //gridUsers.Controls.Clear();
            //var item = lstRoles.Items[I];
            //for (Int32 I = 0; I <= lstRoles.Items.Count - 1; I++)
            //{
            //    lstRoles.Items[I].Selected = false;
            //}
            txtName.Text = "";
            txtName.Focus();
            txtUserName.Text = "";
            lblHouseHold.Text = "";
            lblHouseID.Text = "";
            radioUserType.SelectedValue = "0";
            txtPWord.Text = "";
            lblEmail.Text = "";
            Session["ErrorMsg"] = "";
            UserId = 0;
        }

        private void ADDRow()
        {
            using (var db = new CSBCDbContext())
            {
                try
                {
                    var rep = new UserRepository(db);
                    var user = new User();
                    user.HouseID = Convert.ToInt32(lblHouseID.Text); //must be filled in?
                    user.Name = txtName.Text;
                    user.UserName = txtUserName.Text;
                    user.UserType = Convert.ToInt32(radioUserType.SelectedValue);
                    user.PWord = txtPWord.Text;
                    user.CreatedUser = Master.UserName;
                    //user.Roles = GetRoles();
                    rep.Insert(user);
                    UserId = user.UserID;
                }
                catch (Exception ex)
                {
                    Session["ErrorMSG"] = ex.Message;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (UserId > 0)
            {
                UpdateUser();
            }
            else
            {
                AddUser();
            }
            LoadList();
        }

        private void UpdateUser()
        {
            if (errorRTN() == true)
            {
                MsgBox("ERROR: " + lblError.Text);
                return;
            }
            
            UpdRow(UserId);
            MsgBox("Changes successfully completed");
            //lblError.Text = Session["ErrorMsg"].ToString();
        }

        private void AddUser()
        {
            if (errorRTN() == true)
            {
                MsgBox("ERROR: " + lblError.Text);
                return;
            }
            if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
                ADDRow();
            if (string.IsNullOrEmpty(Session["ErrorMsg"].ToString()))
                MsgBox("New Record Added Successfully");
            lblError.Text = Session["ErrorMsg"].ToString();
        }

        private bool errorRTN()
        {
            bool functionReturnValue = false;
            functionReturnValue = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblError.Text = "Name missing ";
                txtName.Focus();
                return functionReturnValue;
            }
            if (string.IsNullOrEmpty(lblHouseHold.Text))
            {
                lblError.Text = "Household missing ";
                return functionReturnValue;
            }
            if (string.IsNullOrEmpty(lblEmail.Text))
            {
                lblError.Text = "Email missing ";
                return functionReturnValue;
            }
            if (string.IsNullOrEmpty(txtPWord.Text))
            {
                lblError.Text = "Password missing ";
                txtPWord.Focus();
                return functionReturnValue;
            }
            if (String.IsNullOrEmpty(lblHouseID.Text))
            {
                lblError.Text = "No Household selected";
                return functionReturnValue;

            }
            else
            {
                string username = CheckHouseHold(Convert.ToInt32(lblHouseID.Text));

                if (!String.IsNullOrEmpty(username))
                {
                    lblError.Text = "Household Already has Username (" + username + ")";
                    txtPWord.Focus();
                    return functionReturnValue;
                }
                functionReturnValue = false;
                return functionReturnValue;
            }
        }

        private string CheckHouseHold(Int32 iHouseID)
        {
            string functionReturnValue = null;
            functionReturnValue = "";
            //Security.ClsUsers oUser = new Security.ClsUsers();
            //try
            //{
            //    oUser.GetUserByHousehold(iHouseID, UserId, Session["CompanyID"]);
            //    functionReturnValue = oUser.UserName;
            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = "CheckHouseHold::" + ex.Message;
            //}
            //finally
            //{
            //    oUser = null;
            //}
            return functionReturnValue;
        }

        public void MsgBox(string Message)
        {
            Label strScript = new Label();
            strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "window.alert('" + Message + "')</script>";
            Page.Controls.Add(strScript);
        }

        //protected void grdUsers_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
        //{
        //    UserId = grdUsers.DisplayLayout.ActiveRow.Cells.FromKey("UserID").Value;
        //    Session["GridIndex"] = grdUsers.DisplayLayout.ActiveRow.Index;
        //    lblError.Text = "";
        //    if (UserId > 0)
        //    {
        //        LoadRow(UserId);

        //        ReadHousehold(Session["HouseID"]);
        //        lblHouseHold.Text = HouseHoldName;
        //        lblEmail.Text = HouseEmail;
        //        lblHouseID.Text = Session["HouseID"];

        //        for (Int32 I = 0; I <= lstRoles.Items.Count - 1; I++)
        //        {
        //            lstRoles.Items(I).Selected = false;
        //        }
        //        HilightRoles(UserId);
        //        btnSave.Enabled = true;
        //        btnDelete.Enabled = true;
        //    }
        //    if (Session["AccessType"] == "R")
        //    {
        //        btnSave.Enabled = false;
        //        btnDelete.Enabled = false;
        //    }
        //}

        protected void btnNew_Click(object sender, System.EventArgs e)
        {
            ClearFields();
            UserId = 0;
            txtName.Focus();
            btnSave.Enabled = true;
        }


        protected void imgName_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Session["FirstLetter"] = lblHouseHold.Text;
            Session["SearchType"] = "Name";
            Session["CallingScreen"] = "Users.aspx";
            Response.Redirect("SearchHouse.aspx");
        }


        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            //1) Put a label control beneath your button but sets its Visible property to false.  Name it lblConfirm.
            //2) Set the CommandArgument property of your button to "Confirm" (u can use any string, but "Confirm" will do for this example).
            //3) In your server-side click event, do this:
            if (Session["AccessType"] == "R" | UserId == 0)
                return;

            Button btn = (Button)sender;
            if (string.IsNullOrEmpty(lblDeleteUser.Text))
                btn.CommandArgument = "Confirm";
            if (btn.CommandArgument == "Confirm")
            {
                lblDeleteUser.Text = "*Click Delete button again to confirm.*";
                lblDeleteUser.Visible = true;
                btn.CommandArgument = "Delete";
            }
            else if (btn.CommandArgument == "Delete")
            {
                if (UserId > 0)
                {
                    DELRow(UserId);
                    ClearFields();
                    UserId = 0;
                    LoadList();
                }
                lblDeleteUser.Text = "";
                lblDeleteUser.Visible = false;
                btn.CommandArgument = "Confirm";
            }
            //You are using the label as a message box of sorts and the CommandArgument of the button to tell the click event whether 
            //you need to confirm or have confirmed.
        }

        private void DELRow(int id)
        {
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var rep = new UserRepository(db);
                    var user = new User();
                    rep.Delete(rep.GetById(id));
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "DELRow::" + ex.Message;
            }
        }

        protected void gridUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var id = Convert.ToInt32(e.CommandArgument);
            LoadRow(id);
            LoadRoles(id);
        }

        protected void listRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem list in listRoles.Items)
            {
                if (list.Selected)
                    AddUserRole(Convert.ToInt32(lblId.Value), list.Value, Master.UserName);
                else
                    DeleteUserRole(Convert.ToInt32(lblId.Value), list.Value);
            }
        }

        private void DeleteUserRole(int userId, string screenName)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new RoleRepository(db);
                rep.DeleteUserRole((decimal)userId, screenName);
            }
        }

        private void AddUserRole(int userId, string screenName, string userName)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new RoleRepository(db);
                if (db.Roles.FirstOrDefault(r => r.UserID == userId && r.ScreenName == screenName) == null)
                    rep.AddUserRole((decimal)userId, screenName, userName);
            }
        }

        protected void radioFilterUserTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }



    }

}
