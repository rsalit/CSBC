using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Admin.Web.ViewModels;

namespace CSBC.Admin.Web
{
    public partial class BaseForm : System.Web.UI.Page
    {
        public virtual string FormName
        {
            get
            {
                if (Session["Title"] != null)
                    return Session["Title"].ToString();
                else
                    return String.Empty;
            }
        }
        public string AccessType
        {
            get
            {
                if (Session["AccessType"] == null)
                    return "R"; //need to check this
                else
                {
                    return Session["AccessType"].ToString();
                }
            }
            set { Session["AccessType"] = value; }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            //Put user code to initialize the page here
            if (Session["UserID"] == null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                SetUser();
            }
        }

        protected virtual void SetUser()
        {
            Master.AccessType = MasterVM.AccessType(Master.UserId,
                FormName,
                Master.CompanyId,
                Master.SeasonId);
            if (Master.AccessType == "R")
            {
                DisableAllTextBoxes(Page);
            }

        }

        protected virtual void DisableAllTextBoxes(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                var tb = c as WebControl;
                if (tb != null)
                {

                    tb.Enabled = false;
                }
                DisableAllTextBoxes(c);

            }
        }
    }
}