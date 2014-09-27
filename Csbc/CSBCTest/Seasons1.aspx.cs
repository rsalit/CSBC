using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Admin.Web.ViewModels;


namespace CSBC.Admin.Web
{
    public partial class Seasons1 : BaseForm
    {
        //private CSBC.Components.ClsGlobal sGlobal = new CSBC.Components.ClsGlobal();

        protected override void Page_Load(System.Object sender, System.EventArgs e)
        {
            Session["Title"] = "Seasons";
            base.Page_Load(sender, e);
            if (Page.IsPostBack == false)
            {
                this.txtName.Focus();
                PopulateGrid();
            }
        }

        protected override void SetUser()
        {
            base.SetUser();
            if (Master.AccessType == "R")
            {
                btnSave.Enabled = false;
                btnNew.Enabled = false;
                btnSave.Attributes.Add("disabled", "disabled");
            }
        }

        protected void grdSeasons_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "SelectSeason")
            {
                Master.SeasonId = Convert.ToInt32(e.CommandArgument);
            }
            else if (e.CommandName == "ViewSeason")
            {
                Master.SeasonId = Convert.ToInt32(e.CommandArgument);
                LoadRecord(Master.SeasonId);
            }
        }

        public void PopulateGrid()
        {
            grdSeasons.DataSource = GetSeasons();
            grdSeasons.DataBind();
            btnSave.Visible = true;
        }

        public IEnumerable<SelectSeasonVM> GetSeasons()
        {
            var vm = new ViewModels.SelectSeasonVM();
            var seasons = vm.GetRecords(Master.CompanyId);
            return seasons;
        }
        private void UpdRow(int rowId)
        {
            var repSeason = new SeasonRepository(new CSBCDbContext());
            DateTime date;
            Season season;
            if (rowId != 0)
                season = repSeason.GetById(rowId);
            else
            {
                season = new Season();
            }
            try
            {
                season.CompanyID = Master.CompanyId;
                season.SeasonID = rowId;
                season.Description = txtName.Text;
                if (DateTime.TryParse(mskStartDate.Text, out date))
                    season.FromDate = date;
                if (DateTime.TryParse(mskEndDate.Text, out date))
                    season.ToDate = date;
                season.NewSchoolYear = chkNewSchool.Checked;
                season.CurrentSeason = chkCurrentSeason.Checked;
                season.CurrentSignUps = chkRegistration.Checked;
                season.CurrentSchedule = chkSchedules.Checked;

                // Set dates
                if (DateTime.TryParse(mskORStart.Text, out date))
                    season.SignUpsDate = date;
                if (DateTime.TryParse(mskOREnd.Text, out  date))
                    season.SignUpsEND = date;

                // Set fees
                decimal fee;
                season.ParticipationFee = Decimal.TryParse(txtPlayersFee.Text, out fee) ? fee : 0;
                season.SponsorFee = Decimal.TryParse(mskSponsorFee.Text, out fee) ? fee : 0;
                season.ConvenienceFee = Decimal.TryParse(mskSponsorFeeDiscounted.Text, out fee) ? fee : 0;

                season.CreatedUser = Session["UserName"].ToString();
                if (rowId == 0)
                    repSeason.Insert(season);
                else
                    repSeason.Update(season);

            }
            catch (Exception e)
            {
                lblError.Text = "SaveRecord::" + e.Message;
            }
        }

        private void ClearFields()
        {
            grdSeasons.Controls.Clear();
            //If chkCurrentSeason.Checked = True Then Session["SeasonDesc"] = txtName.Text
            txtName.Text = "";
            Session["ErrorMsg"] = "";
            mskStartDate.Text = "";
            mskEndDate.Text = "";
            mskORStart.Text = "";
            //mskOREnd.Text = "";
            lblNewYear.Visible = false;
            chkNewSchool.Visible = false;
            txtPlayersFee.Text = "";
            mskSponsorFee.Text = "";
            mskSponsorFeeDiscounted.Text = "";
            chkCurrentSeason.Checked = false;
            chkRegistration.Checked = false;
            chkSchedules.Checked = false;

        }

        private void Save()
        {
            //add error checking after testing
            if (errorRTN() == true)
            {
                MsgBox("ERROR: " + Session["ErrorMsg"]);
                return;
            }
            var seasonId = txtSeasonID.Value == "" ? 0 : Convert.ToInt32(txtSeasonID.Value);
            UpdRow(seasonId);
            if (Session["ErrorMsg"] != null)
            {
                if (txtSeasonID.Value != "")
                {
                    MsgBox("Changes successfully completed");
                }
                else
                {
                    MsgBox("New Record Added Successfully");
                }
            }
            //ClearFields();
            PopulateGrid();

        }

        private bool errorRTN()
        {
            DateTime date;
            Decimal money;
            bool functionReturnValue = false;
            functionReturnValue = false;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Session.Add("ErrorMsg", "Name missing ");
                functionReturnValue = true;
            }
            if (!DateTime.TryParse(mskStartDate.Text, out date))
            {
                Session.Add("ErrorMsg", "Invalid/missing Minimun date mm/dd/yyyy ");
                functionReturnValue = true;
            }
            if (!DateTime.TryParse(mskEndDate.Text, out date))
            {
                Session.Add("ErrorMsg", "Invalid/missing Maximum date mm/dd/yyyy ");
                functionReturnValue = true;
            }
            if (!Decimal.TryParse(txtPlayersFee.Text, out money))
            {
                Session.Add("ErrorMsg", "Players Fee Invalid/missing ");
                functionReturnValue = true;
            }
            if (!Decimal.TryParse(mskSponsorFee.Text, out money))
            {
                Session.Add("ErrorMsg", "Sponsors Fee Invalid/missing ");
                functionReturnValue = true;
            }
            //if (!Decimal.TryParse(mskSponsorFeeDiscounted.Text, out money))
            //{
            //    Session.Add("ErrorMsg", "Sponsors Discounted Fee Invalid/missing ");
            //    functionReturnValue = true;
            //}
            if (chkRegistration.Checked == true)
            {
                if (!DateTime.TryParse(mskORStart.Text, out date))
                {
                    Session.Add("ErrorMsg", "Invalid/missing OR Start date mm/dd/yyyy ");
                    functionReturnValue = true;
                }
                //check out business logic - what if we don't know end date!
                //if (!DateTime.TryParse(mskOREnd.Text, out date))
                //{
                //    Session.Add("ErrorMsg", "Invalid/missing OR End date mm/dd/yyyy ");
                //    functionReturnValue = true;
                //}
            }
            return functionReturnValue;
        }

        public void MsgBox(string Message)
        {
            Label strScript = new Label();
            strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "window.alert('" + Message +
                             "')</script>";
            Page.Controls.Add(strScript);
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            txtSeasonID.Value = "0";
            ClearFields();
            btnSave.Visible = true;
            chkNewSchool.Visible = true;
            lblNewYear.Visible = true;
            txtName.Focus();
        }

        private void LoadRecord(int rowId)
        {
            var repSeason = new SeasonRepository(new CSBCDbContext());
            var season = repSeason.GetById(rowId);

            try
            {
                //season.TimeZone = Session["TimeZone"]
                txtSeasonID.Value = rowId.ToString();
                txtName.Text = season.Description;
                mskStartDate.Text = season.FromDate.Value.ToString("yyyy-MM-dd");
                mskEndDate.Text = season.ToDate.Value.ToString("yyyy-MM-dd");
                chkNewSchool.Checked = Convert.ToBoolean(season.NewSchoolYear);
                chkCurrentSeason.Checked = Convert.ToBoolean(season.CurrentSeason);
                chkRegistration.Checked = Convert.ToBoolean(season.CurrentSignUps);
                var curReg = Convert.ToBoolean(season.CurrentSignUps);

                //mskORStart.Enabled = curReg;
                //mskOREnd.Enabled = curReg;
                if (season.SignUpsDate != null)
                    mskORStart.Text = season.SignUpsDate.Value.ToString("yyyy-MM-dd");
                if (season.SignUpsEND != null)
                    mskOREnd.Text = season.SignUpsEND.Value.ToString("yyyy-MM-dd");

                chkSchedules.Checked = Convert.ToBoolean(season.CurrentSchedule);
                if (season.ParticipationFee != null)
                    txtPlayersFee.Text = Convert.ToDecimal(string.Format("{0:F2}", season.ParticipationFee)).ToString();
                if (season.SponsorFee != null)
                    mskSponsorFee.Text = Convert.ToDecimal(string.Format("{0:F2}", season.SponsorFee)).ToString();
                if (season.ConvenienceFee != null)
                    mskSponsorFeeDiscounted.Text = Convert.ToDecimal(string.Format("{0:F2}", season.ConvenienceFee)).ToString();
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadRecord::" + ex.Message;
            }

        }

        protected void chkRegistration_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkRegistration.Checked == true)
            {
                //lblORStarts.Enabled = true;
                //lblORStops.Enabled = true;
                mskORStart.Enabled = true;
                mskOREnd.Enabled = true;
            }
            else
            {
                //lblORStarts.Enabled = false;
                //lblORStops.Enabled = false;
                mskORStart.Enabled = false;
                mskOREnd.Enabled = false;
            }
        }


        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            txtSeasonID.Value = "0";
            ClearFields();
            btnSave.Visible = true;
            chkNewSchool.Visible = true;
            lblNewYear.Visible = true;
            txtName.Focus();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Save();
        }
    }
}


