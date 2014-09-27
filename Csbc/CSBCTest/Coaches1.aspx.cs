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
	public partial class Coaches1 : BaseForm
	{
		public int CoachId {
			get
			{
				if (Session["coachId"] == null)
					return 0;
				else
				{
					return Convert.ToInt32(Session["coachId"]);
				}
			}
			set { Session["coachId"] = value; } 
		}
		public int UserId { get; set; }

		protected override void Page_Load(object sender, EventArgs e)
		{
			Session["Title"] = "Coaches";
			base.Page_Load(sender, e);
			//Put user code to initialize the page here
	
			if (Page.IsPostBack == false)
			{
	
			
				//Me.txtName.Focus()
				LoadVolunteers();
				LoadPlayers();
				LoadCoaches();
				if (Session["coachId"] == null) 
				{
					CoachId = 0;
					ClearFields();
					txtComments.Enabled = false;
					lnkComments.Enabled = false;
				}
				else
				{

					if (CoachId != 0)
					{
						LoadCoach(CoachId);
						LoadComments(CoachId);
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
				btnDelete.Enabled = false;
			}
		}

		private void LoadCoaches()
		{
			var rep = new CoachRepository(new CSBCDbContext());
			try
			{
				var rsData = rep.GetSeasonCoaches(Master.SeasonId);
				
				//grdCoaches.Columns.Clear();
				if (rsData.Count<vw_Coaches>() > 0)
				{
					var _with1 = grd;
					grd.DataSource = rsData;
					grd.DataBind();
			
				}
			}
			catch (Exception ex)
			{
				lblError.Text = "LoadCoaches::" + ex.Message;
			}
		}

		private void LoadRow(int CoachID)
		{
			var oCoaches = new CSBC.Components.Season.clsCoaches();
			DataTable rsData = default(DataTable);
			try
			{
				rsData = oCoaches.LoadCoaches(CoachID, Master.CompanyId, Master.SeasonId);
				if (rsData.Rows.Count > 0)
				{
					lnkName.Text = rsData.Rows[0]["Name"].ToString();
					lblAddress.Text = rsData.Rows[0]["Address1"].ToString();
					lblCSZ.Text = rsData.Rows[0]["City"].ToString() + "  " + rsData.Rows[0]["state"].ToString() + "  " + 
						rsData.Rows[0]["Zip"].ToString();
					lblPhone.Text = rsData.Rows[0]["HousePhone"].ToString();
					txtCoachPhone.Text = rsData.Rows[0]["CoachPhone"].ToString();
					if (String.IsNullOrEmpty(rsData.Rows[0]["ShirtSize"].ToString()))
					{
						cmbSizes.SelectedValue = "N/A";
					}
					else
					{
						cmbSizes.SelectedValue = rsData.Rows[0]["ShirtSize"].ToString();
					}
																	int x;
					if (Int32.TryParse(rsData.Rows[0]["PeopleID"].ToString(), out x ))
					{
						Session["PeopleID"] = rsData.Rows[0]["PeopleID"].ToString();
					}

					lnkComments.Enabled = true;
					txtComments.Enabled = true;

				}
			}
			catch (Exception ex)
			{
				lblError.Text = "LoadRow::" + ex.Message;
			}
			finally
			{
				oCoaches = null;
			}

			GetKids(CoachID);
			cmbCoaches.Visible = false;
			pnlCoach.Visible = true;
		}

		private void GetKids(int coachId)
		{
			var rep = new PlayerRepository(new CSBCDbContext());
			var players = rep.GetCoachPlayers(Master.SeasonId, coachId);
			//var oPlayers = new CSBC.Components.Season.ClsPlayers();
			//DataTable rsData = default(DataTable);
			try
			{
				grdKids.Columns.Clear();
				var _with3 = grdKids;
				_with3.DataSource = players;
				_with3.DataBind();
				
			}
			catch (Exception ex)
			{
				lblError.Text = "getKids::" + ex.Message;
			}
		}

		private void LoadPlayers()
		{
			var rep = new PlayerRepository(new CSBCDbContext());
			
			try
			{
					var players = rep.GetPlayers(Master.SeasonId);
					grdPlayers.DataSource = players;
					grdPlayers.DataBind();
				
			}
			catch (Exception ex)
			{
				lblError.Text = "LoadPlayers::" + ex.Message;
			}

		}

		private void LoadVolunteers()
		{                                                                   
			var rep = new CoachRepository(new CSBCDbContext());

			try
			{
				var coaches = rep.GetCoachVolunteers(Master.CompanyId, Master.SeasonId);
				vw_Coaches coach = new vw_Coaches { PeopleID = 0, Name = "" };
				
				cmbCoaches.DataSource = coaches;
				cmbCoaches.DataValueField = "PeopleID";
				cmbCoaches.DataTextField = "Name";
				cmbCoaches.DataBind();
				cmbCoaches.Items.Insert(0, new ListItem(String.Empty, String.Empty));
				cmbCoaches.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				lblError.Text = "LoadVolunteers::" + ex.Message;
			}
		}

		private int Create()
		{
			var rep = new CoachRepository(new CSBCDbContext());
			var coach = new Coach();
			try
			{
				
				coach.ShirtSize = cmbSizes.Text;
				coach.SeasonID = Master.SeasonId;
				coach.CompanyID = Master.CompanyId;
				coach.CreatedDate = DateTime.Today;
				coach.PeopleID = Convert.ToInt32(cmbCoaches.SelectedValue);
				if (txtCoachPhone.Text != "")
				{
					coach.CoachPhone = txtCoachPhone.Text;
				}
				else
				{
					coach.CoachPhone = "";
				}
				coach.CreatedUser = Session["UserName"].ToString();
				rep.Insert(coach);
				CoachId = (int)coach.CoachID;
				Session["coachId"] = CoachId;
			}
			catch (Exception ex)
			{
				Session["ErrorMSG"] = ex.Message;
			}
			return CoachId;
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
				if (CoachId > 0)
				{
					DELRow(CoachId);
					ClearFields();
					CoachId = 0;
					LoadCoaches();
					LoadVolunteers();
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
			cmbCoaches.Visible = true;
			pnlCoach.Visible = false;
			txtComments.Text = "";
			txtCoachPhone.Text = "";
			txtComments.Enabled = false;
			lnkComments.Enabled = false;
			cmbCoaches.SelectedIndex = 0;
			cmbSizes.SelectedIndex = 0;
			cmbSizes.DataValueField = "";
			txtCoachPhone.Text = "";
			lblCoachId.Value = "";
			grdKids.Columns.Clear();

			//cobPlayers.SelectedValue = "0"
			//cobPlayers2.SelectedValue = "0"
		}

		private void DELRow(int iCoachID)
		{
			if (Master.AccessType == "R")
				return;
			var oCoach = new CSBC.Components.Season.clsCoaches();
			try
			{
				oCoach.DELRow(iCoachID, Master.CompanyId);
			}
			catch (Exception ex)
			{
				Session["ErrorMSG"] = "DELRow::" + ex.Message;
			}
			finally
			{
				oCoach = null;
			}

		}

		private int Update()
		{
			var rep = new CoachRepository(new CSBCDbContext());
			if (CoachId != 0)
			{
				//var id = Convert.ToInt32(lblCoachId.Value);
				var coach = rep.GetCoachForSeason(Master.SeasonId, Master.PeopleId); //change this logic to only have one call
                if (coach == null)
                {
                    Create();
                }
                else
                {
                    try
                    {
                        coach.ShirtSize = cmbSizes.Text;
                        if (txtCoachPhone.Text != "")
                        {
                            coach.CoachPhone = txtCoachPhone.Text;
                        }
                        else
                        {
                            coach.CoachPhone = "";
                        }
                        rep.Update(coach);

                    }
                    catch
                    {
                    }
                }
			}
			else
			{

			}
			return (CoachId);
		}
        //private void UpdRow(long RowID)
        //{
        //    if (Master.AccessType == "R")
        //        return;
        //    var oCoach = new CSBC.Components.Season.clsCoaches();
        //    try
        //    {
        //        oCoach.SeasonID = Master.SeasonId;
        //        oCoach.PeopleID = (int)Session["PeopleID"];
        //        oCoach.ShirtSize = cmbSizes.Text;
        //        if (txtCoachPhone.Text != "")
        //        {
        //            oCoach.CoachPhone = txtCoachPhone.Text;
        //        }
        //        else
        //        {
        //            oCoach.CoachPhone = "";
        //        }
        //        //oCoach.UPDCoach(CompanyId, RowID) Add routine!!!!!
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["ErrorMSG"] = "UpdRow::" + ex.Message;
        //    }
        //    finally
        //    {
        //        oCoach = null;
        //    }

        //}

		private void LoadComments(long CoachID)
		{
			var oComments = new CSBC.Components.Website.ClsComments();
			DataTable rsData = default(DataTable);
			try
			{
				rsData = oComments.GetRecords(0, CoachId, "C", Master.CompanyId);
				if ((rsData != null))
				{
					for (int I = 0; I <= rsData.Rows.Count - 1; I++)
					{
						txtComments.Text = txtComments.Text + " " + rsData.Rows[I]["Comment"].ToString();// Constants.vbCrLf;
					}
				}
				//Session.Add("LinkName", txtFirstName.Text & ", " & txtLastName.Text())
			}
			catch (Exception ex)
			{
				lblError.Text = "LoadComments::" + ex.Message;
			}
			finally
			{
				oComments = null;
				//Session.Add("LinkName", txtLastName.Text)
			}

		}

		private void UpdPlayer(long CoachID, long PlayerID)
		{
			if (Master.AccessType == "R")
				return;
			var oCoach = new CSBC.Components.Season.clsCoaches();
			try
			{
				//oCoach.UpdatePlayer(CompanyId, SeasonId, coachId, PlayerID) -- update
			}
			catch (Exception ex)
			{
				Session["ErrorMSG"] = "UpdPlayer::" + ex.Message;
			}
			finally
			{
				oCoach = null;
			}
		}


		protected void grdKids_SelectedRowsChange(object sender, EventArgs e)
		{
			//Session["PeopleID"] = grdKids.DisplayLayout.ActiveRow.Cells.FromKey("PeopleID").Value;
			if ((int)Session["PeopleID"] > 0)
			{
				CoachId = 0;
				Session["PlayerID"] = 0;
				Response.Redirect("People.aspx");
			}
		}


		protected void grdKids_ClickCellButton(object sender, EventArgs e)
		{
			//(int)Session["PlayerID"] = grdKids.DisplayLayout.ActiveRow.Cells.FromKey("PlayerID").Value;
			if ((int)Session["PlayerID"] > 0)
			{
				UpdPlayer(0, (int)Session["PlayerID"]);
				LoadPlayers();
				GetKids(CoachId);
			}
		}

		protected void lnkComments_Click(object sender, System.EventArgs e)
		{
			if (CoachId > 0)
			{
				//UpdRow(CoachId);
			}
			else
			{
				Create();
			}
			if (CoachId > 0)
			{
				Session["CallingScreen"] = "Coaches.aspx";
				Session.Add("LinkID", CoachId);
				Session.Add("CommentType", "C");
				Response.Redirect("Comments.aspx");
			}
		}

		protected void grdCoaches_DblClick(object sender, EventArgs e)
		{
			//CoachId = grdCoaches.DisplayLayout.ActiveRow.Cells.FromKey("coachId").Value();
			LoadCoach(CoachId);
			LoadComments(CoachId);
		}

		protected void cmbCoaches_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var oPeople = new CSBC.Components.Profile.ClsPeople();
			try
			{
				cmbSizes.SelectedValue = oPeople.GetShirtSize(Convert.ToInt32(cmbCoaches.SelectedValue), Master.CompanyId);
			}
			catch (Exception ex)
			{
				lblError.Text = "cmbCoaches_SelectedIndexChanged::" + ex.Message;
			}
			finally
			{
				oPeople = null;
			}
		}


		protected void grdPlayers_DblClick(object sender, EventArgs e)
		{
			if (CoachId > 0)
			{
				//Session["PlayerID"] = grdPlayers.DisplayLayout.ActiveRow.Cells.FromKey("PlayerID").Value();
				UpdPlayer(CoachId, (int)Session["PlayerID"]);
				LoadPlayers();
				GetKids(CoachId);
			}
		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{ 
            int realCoachId; //because we use People ID in other cases - need to fix!
            if (cmbCoaches.Visible)
                Master.PeopleId = Convert.ToInt32(cmbCoaches.SelectedValue);
			if (CoachId != 0)
			{
				CoachId = Update();
				MasterVM.MsgBox(this, "Changes successfully completed");
			}
			else
			{
				CoachId = Create();
				MasterVM.MsgBox(this, "New Record Added Successfully");
			}
			LoadPlayers();
			LoadVolunteers();
			if (CoachId != 0)
				LoadCoach(CoachId);
			LoadCoaches();
			lnkComments.Enabled = true;
			txtComments.Enabled = true;

		}

		protected void btnNew_Click(object sender, System.EventArgs e)
		{
			ClearFields();
			CoachId = 0;
		}

		protected void lnkName_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("People.aspx");
		}

		protected void lnkPrint_Click(object sender, System.EventArgs e)
		{
			//    'Response.write to a new Windows (not from the menu)
			//    'Does not work if the URL is hidden
			//    Session["ReportName"] = "SeasonCoaches"
			//    Dim strS As String 'Loading.Aspx?Page=Reports.aspx
			//    strS = "Reports.aspx',null, 'target=_top,toolbar=Yes"
			//    Response.Write("<script>" & vbCrLf)
			//    Response.Write("window.open('" & strS & "');" & vbCrLf)
			//    Response.Write("</script>")
			Response.Redirect("report.aspx?Report=Coaches.rpt");
		}

		protected void grdCoaches_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int index = Convert.ToInt32(e.CommandArgument);
			if (e.CommandName == "Select")
			{
				var coachId = index;
				LoadCoach(coachId);
			}
		}

		private void LoadCoach(int coachId)
		{
			using (var db = new CSBCDbContext())
			{
				var rep = new CoachRepository(db);
				cmbCoaches.Visible = false;

				var coach = rep.GetById(coachId);
				CoachId = coach.CoachID;
                Master.PeopleId = coach.PeopleID;
				lblCoachId.Value = CoachId.ToString();
				if (coach.Person != null)
				{
					lnkName.Text = coach.Person.FirstName + " " + coach.Person.LastName;
					if (coach.Person.Household != null)
					{
						lblAddress.Text = coach.Person.Household.Address1;
						lblPhone.Text = coach.Person.Household.Phone;
						lblCSZ.Text = coach.Person.Household.City + ", " + coach.Person.Household.State + " " + coach.Person.Household.Zip;
					}
				}

				txtCoachPhone.Text = coach.CoachPhone;
				cmbSizes.SelectedValue = coach.ShirtSize;
			}
			pnlCoach.Visible = true;
			GetKids(coachId);
		}

		protected void btnDelete_Click1(object sender, EventArgs e)
		{
			var rep = new CoachRepository(new CSBCDbContext());
			rep.DeleteById(Convert.ToInt32(lblCoachId.Value));
			ClearFields();
			lblCoachId.Value = "";
			LoadCoaches();
			LoadVolunteers();
			
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
			lblCoachId.Value = "";
			LoadCoaches();
			LoadVolunteers();
			LoadPlayers();
		}
	   
	}
}
