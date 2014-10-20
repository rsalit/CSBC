using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PIHOA.pages
{
  public partial class News2 : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["MeetingXmlFile"] == null)
        {
          //Master.SetSessionVariables();
        }

      }
    }
  }
}
