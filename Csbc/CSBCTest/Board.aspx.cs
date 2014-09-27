using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using CSBC.Components;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class Board : BaseForm
    {
        public int DirectorId
        {
            get
            {
                if (Session["DirectorID"] != null)
                    return Convert.ToInt32(Session["DirectorID"]);
                else
                    return 0;
            }
            set
            {
                Session["DirectorID"] = value;
            }
        }
        protected override void Page_Load(System.Object sender, System.EventArgs e)
        {
            Session["Title"] = "Directors";
            base.Page_Load(sender, e);
   
            if (Page.IsPostBack == false)
            {
                LoadDirectors();
                LoadVolunteerList();
                if (DirectorId != 0)
                {
                    LoadRow(DirectorId);
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

        public List<vw_Directors> GetAllRecords()
        {
            var rep = new DirectorRepository(new CSBCDbContext());
            return rep.GetAll(Master.CompanyId).ToList<vw_Directors>();
        }

        #region Load Data
        private void LoadVolunteerList()
        {
            try
            {
                using (var db = new CSBCDbContext())
                {
                    var rep = new DirectorRepository(db);
                    var directors = rep.GetDirectorVolunteers(Master.CompanyId);
                    var _with1 = cboBM;
                    _with1.DataSource = directors;
                    _with1.DataValueField = "PeopleID";
                    _with1.DataTextField = "Name";
                    _with1.DataBind();
                    _with1.Items.Insert(0, new ListItem(String.Empty, "0"));
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadFlagged::" + ex.Message;
            }

        }

        private void LoadRow(Director director)
        {
            lblName.Text = director.People.FirstName + " " + director.People.LastName;
            lblAddress.Text = director.People.Household.Address1;
            lblCSZ.Text = director.People.Household.City + ", " + director.People.Household.State + " " + director.People.Household.Zip;
            lblPhone.Text = director.People.Household.Phone;
            txtTitle.Text = director.Title;
            lblEmail.Text = director.People.Email;
            txtSequence.Text = director.Seq.ToString();
            cobPhones.Items.Add(new ListItem("NONE", "0"));
            cobPhones.Items.Add(new ListItem(director.People.Household.Phone, "1"));
            cobPhones.Items.Add(new ListItem(director.People.Cellphone, "2"));
            cobPhones.Items.Add(new ListItem(director.People.Workphone, "3"));
            //lblBoard.Visible = false;
            chkEmail.Checked = (director.EmailPref == 1);

            switch (director.PhonePref)
            {
                case "HOME":
                    cobPhones.Items[1].Selected = true;
                    break;
                case "CELL":
                    cobPhones.Items[2].Selected = true;
                    break;
                case "WORK":
                    cobPhones.Items[3].Selected = true;
                    break;
                default:
                    cobPhones.Items[0].Selected = true;
                    break;
            }
            cboBM.Visible = false;
        }
        private void LoadRow(long RowID)
        {
            var oDirectors = new CSBC.Components.Volunteers.ClsDirectors();
            try
            {
                var rsData = oDirectors.GetDirectors(Master.CompanyId, RowID);
                cobPhones.Items.Clear();

                if ((rsData != null))
                {
                    if (rsData.Rows.Count > 0)
                    {
                        var row = rsData.Rows[0];
                        lblName.Text = row["Name"] + "";
                        lblAddress.Text = row["Address1"] + "";
                        lblCSZ.Text = row["CITY"] + " " + row["STATE"] + " " + row["Zip"];
                        lblPhone.Text = row["PHONE"] + "";
                        txtTitle.Text = row["Title"] + "";
                        lblEmail.Text = row["Email"] + "";
                        cobPhones.Items.Add(new ListItem("NONE", "0"));
                        cobPhones.Items.Add(new ListItem((row["Phone"].ToString().Trim() + ""), "1"));
                        cobPhones.Items.Add(new ListItem((row["CellPhone"].ToString().Trim() + ""), "2"));
                        cobPhones.Items.Add(new ListItem((row["WorkPhone"].ToString().Trim() + ""), "3"));
                        //lblBoard.Visible = false;
                        chkEmail.Checked = (bool)row["EmailPref"];

                        switch (row["PhonePref"].ToString())
                        {
                            case "HOME":
                                cobPhones.Items[1].Selected = true;
                                break;
                            case "CELL":
                                cobPhones.Items[2].Selected = true;
                                break;
                            case "WORK":
                                cobPhones.Items[3].Selected = true;
                                break;
                            default:
                                cobPhones.Items[0].Selected = true;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadRow::" + ex.Message;
            }
            finally
            {
                oDirectors = null;
            }

            cboBM.Visible = false;
        }

        private void LoadPeople(int personId)
        {
            try
            {
                ClearFields();
                cobPhones.Items.Clear();

                using (var db = new CSBCDbContext())
                {
                    var rep = new PersonRepository(db);
                    var person = rep.GetById(personId);
                    if ((person != null))
                    {
                        lblName.Text = person.FirstName + " " + person.LastName;
                        if (person.Household != null)
                        {
                            lblAddress.Text = person.Household.Address1;
                            lblCSZ.Text = person.Household.City + ", " + person.Household.State + " " + person.Household.Zip;
                            lblPhone.Text = person.Household.Phone;
                            lblEmail.Text = person.Email;
                            cobPhones.Items.Add(new ListItem("NONE", "0"));
                            cobPhones.Items.Add(new ListItem(person.Household.Phone, "1"));
                            cobPhones.Items.Add(new ListItem(person.Cellphone, "2"));
                            cobPhones.Items.Add(new ListItem(person.Workphone, "3"));
                            //lblBoard.Visible = false;
                            cobPhones.Items[1].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadPeople::" + ex.Message;
            }

            cboBM.Visible = false;
        }
        #endregion
        #region Crud Stuff
        private void AddRecord()
        {
         using (var db = new CSBCDbContext())
         { 
            
            try
            {
                var director = new Director();
                director.PeopleID = Int32.Parse(cboBM.SelectedItem.Value);
                director.Title = txtTitle.Text;
                if (cobPhones.Items[0].Selected == true)
                    director.PhonePref = "None";
                if (cobPhones.Items[1].Selected == true)
                    director.PhonePref = "HOME";
                if (cobPhones.Items[2].Selected == true)
                    director.PhonePref = "CELL";
                if (cobPhones.Items[3].Selected == true)
                    director.PhonePref = "WORK";
                director.CompanyID = Master.CompanyId;
                director.EmailPref = (chkEmail.Checked ? 1: 0);
                director.Seq = Convert.ToInt32(txtSequence.Text);
                director.CreatedUser = Master.UserName;
                var rep = new DirectorRepository(db);
                rep.Insert(director);
            }
            catch (Exception ex)
            {
                lblError.Text = "ADDRow::" + ex.Message;
            }
            }
        }
        private void ADDRow()
        {

            var oDirectors = new CSBC.Components.Volunteers.ClsDirectors();
            try
            {
                oDirectors.PeopleID = Int32.Parse(cboBM.SelectedItem.Value);
                oDirectors.Title = txtTitle.Text;
                if (cobPhones.Items[0].Selected == true)
                    oDirectors.PhoneType = (int)ePhoneType.NONE;
                if (cobPhones.Items[1].Selected == true)
                    oDirectors.PhoneType = (int)ePhoneType.HOME;
                if (cobPhones.Items[2].Selected == true)
                    oDirectors.PhoneType = (int)ePhoneType.CELL;
                if (cobPhones.Items[3].Selected == true)
                    oDirectors.PhoneType = (int)ePhoneType.WORK;
                oDirectors.EmailPref = chkEmail.Checked;
                oDirectors.UserID = Master.UserId;
                oDirectors.CompanyID = Master.CompanyId;
                oDirectors.AddDirector(Master.CompanyId, Master.TimeZone);
            }
            catch (Exception ex)
            {
                lblError.Text = "ADDRow::" + ex.Message;
            }
            finally
            {
                oDirectors = null;
            }
        }

        private void UpdRow(long RowID)
        {
            using (var db = new CSBCDbContext())
            {

                try
                {
                    var rep = new DirectorRepository(db);
                    var director = new Director();
                    director.PeopleID = Int32.Parse(cboBM.SelectedItem.Value.ToString());
                    director.Title = txtTitle.Text;
                    if (cobPhones.Items[0].Selected == true)
                        director.PhonePref = ePhoneType.NONE.ToString();
                    if (cobPhones.Items[1].Selected == true)
                        director.PhonePref = ePhoneType.HOME.ToString();
                    if (cobPhones.Items[2].Selected == true)
                        director.PhonePref = ePhoneType.CELL.ToString();
                    if (cobPhones.Items[3].Selected == true)
                        director.PhonePref = ePhoneType.WORK.ToString();
                    director.EmailPref = chkEmail.Checked ? 1 : 0;
                    director.Seq = Convert.ToInt32(txtSequence.Text);
                    director.CompanyID = Master.CompanyId;
                    director.CreatedUser = Master.UserName;
                    rep.Update(director);
                }
                catch (Exception ex)
                {
                    lblError.Text = "UpdRow::" + ex.Message;
                }
            }

        }

        private void DeleteRecord(int directorId)
        {
            var rep = new DirectorRepository(new CSBCDbContext());
            try
            {
                var director = rep.GetById(directorId);
                rep.Delete(director); //oDirectors.DELRow(directorId, (int)Session["CompanyID"]);
            }
            catch (Exception ex)
            {
                lblError.Text = "DELRow::" + ex.Message;
            }
        }
        #endregion

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            if (ErrorsFound() == true)
            {
                MasterVM.MsgBox(this, "ERROR: " + lblError.Text);
                return;
            }
            string msg = String.Empty;
            if (DirectorId > 0)
            {
                UpdRow(DirectorId);
                if (string.IsNullOrEmpty(lblError.Text))
                {
                    msg = "Changes successfully completed";
                    //ClearFields();
                    //cboBM.SelectedValue = "0";
                }
            }
            else
            {
                AddRecord();
                if (string.IsNullOrEmpty(lblError.Text))
                {
                    msg = "New Record Added Successfully";
                    //cboBM.SelectedValue = "0";
                }
            }
            MasterVM.MsgBox(this, msg);
            LoadDirectors();
        }

        private void LoadDirectors()
        {
            var directors = GetAllRecords();
            grdBM.DataSource = directors;
            grdBM.DataBind();
        }

        private bool ErrorsFound()
        {
            bool functionReturnValue = false;
            functionReturnValue = false;
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                functionReturnValue = true;
                lblError.Text = "Missing Title description";
                txtTitle.Focus();
            }
            if (Int32.Parse(cboBM.SelectedItem.Value) == 0 & cboBM.Visible == true)
            {
                functionReturnValue = true;
                lblError.Text = "Missing Name";
                cboBM.Focus();
            }
            return functionReturnValue;

        }


        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            //1) Put a label control beneath your button but sets its Visible property to false.  Name it lblConfirm.
            //2) Set the CommandArgument property of your button to "Confirm" (u can use any string, but "Confirm" will do for this example).
            //3) In your server-side click event, do this:
            if (DirectorId == 0)
            {
                return;
            }
            else
            {
                if (DirectorId > 0)
                {
                    DeleteRecord(DirectorId);
                    ClearFields();
                    LoadVolunteerList();
                }
                lblDeleteBM.Text = "";
                lblDeleteBM.Visible = false;
            }
            LoadDirectors();
        }

        private void ClearFields()
        {
            cboBM.Visible = true;
            txtTitle.Text = "";
            cobPhones.Items.Clear();
            lblEmail.Text = "";
            lblAddress.Text = "";
            lblCSZ.Text = "";
            lblName.Text = "";
            //lblBoard.Visible = true;
            lblPhone.Text = "";
            lblDeleteBM.Text = "";
            lblError.Text = "";
            DirectorId = 0;
        }

        protected void cboBM_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadPeople(Int32.Parse(cboBM.SelectedItem.Value));
        }

        private void Regroup(Int32 iSeq)
        {
            var oDirectors = new CSBC.Components.Volunteers.ClsDirectors();
            try
            {
                oDirectors.updDirectorSeq(iSeq, (int)Session["CompanyID"]);
            }
            catch (Exception ex)
            {
                lblError.Text = "grdBM_ClickCellButton::" + ex.Message;
            }
            finally
            {
                oDirectors = null;
            }

        }

        protected void btnNew_Click(object sender, System.EventArgs e)
        {
            ClearFields();
            cboBM.SelectedValue = "0";
            DirectorId = 0;
        }

        protected void grdBM_SelectedRowsChange(object sender, EventArgs e)
        {
            //Session["DirectorID"] = grdBM.DisplayLayout.ActiveRow.Cells.FromKey("ID").Value;
            LoadRow(DirectorId);
        }

        protected void grdBM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var id = Convert.ToInt32(e.CommandArgument);
            var rep = new DirectorRepository(new CSBCDbContext());
            var director = rep.GetById(id);
            DirectorId = id;
            LoadRow(director);
        }
        protected void cboBM_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
}
}

