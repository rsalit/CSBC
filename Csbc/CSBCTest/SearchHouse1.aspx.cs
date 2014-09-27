using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CSBC.Components;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web
{
	public partial class SearchHouse1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			if (Page.IsPostBack == false)
			{
                //SetSessionVariables();
                //if (Session["FirstLetter"].ToString() != String.Empty)
                //{
                //    GetData();
                //}
                //else
                //{
                //    txtLastName.Focus();
                //}
				Session["Title"] = "Search Households";
			}
		}

        private void SetSessionVariables()
        {
            Session["Name"] = "";
            Session["Address"] = "";
            Session["Phone"] = "";
            Session["Email"] = "";
            switch (Session["SearchType"].ToString())
            {
                case "Name":
                    Session["Name"] = Session["FirstLetter"];
                    break;
                case "Address1":
                    Session["Address"] = Session["FirstLetter"];
                    break;
                case "Phone":
                    Session["Phone"] = Session["FirstLetter"];
                    break;
                case "Email":
                    Session["Email"] = Session["FirstLetter"];
                    break;
                default:
                    Session["Name"] = "";
                    Session["Address"] = "";
                    Session["Phone"] = "";
                    Session["Email"] = "";
                    break;
            }
        }

		protected void btnSearch_Click(System.Object sender, System.EventArgs e)
		{
			Session["SearchByName"] = "";
			Session["SearchByAddress"] = "";
			Session["Name"] = "";
			Session["Address"] = "";
			Session["Phone"] = "";
			Session["Email"] = "";
			if (txtLastName.Text != String.Empty)
			{
				Session["SearchByName"] = "Name";
				Session["Name"] = txtLastName.Text;
				
			}
			if (txtAddress.Text != String.Empty)
			{
				Session["SearchByAddress"] = "Address1";
				Session["Address"] = txtAddress.Text;
				
			}
			if (txtPhone.Text != String.Empty)
			{
				Session["Phone"] = txtPhone.Text;

			}
			if (txtEmail.Text != String.Empty)
			{
				Session["Email"] = txtEmail.Text;
			}
			GetData();

		}

		private void GetData()
		{
			var rep = new HouseholdRepository(new CSBCDbContext());

			try
			{
				var rsData = rep.GetRecords( Master.CompanyId, Session["Name"].ToString(),
					Session["Address"].ToString(), Session["Phone"].ToString(), Session["Email"].ToString()).ToList();
				//grdHouseholds.Clear()
				if (rsData.Any())
				{
					var _with1 = grdHouseholds;
					_with1.DataSource = rsData;
					_with1.DataBind();
				}
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
			}
			finally
			{
				rep = null;
			}
		}

		protected void btnNew_Click(object sender, System.EventArgs e)
		{
			Session.Add("HouseID", 0);
			Response.Redirect(Master.HouseholdForm);
		}


		protected void grdHouseholds_OnRowCommand(object sender, GridViewCommandEventArgs e)
		{
			Session["HouseID"] = Convert.ToInt32(e.CommandArgument.ToString());
			Response.Redirect(Master.HouseholdForm);
		}



	}
}

