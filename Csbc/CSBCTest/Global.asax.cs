using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;
using CSBC.Core.Models;
using CSBC.Core.Data;

namespace CSBC.Admin.Web
{
    public class Global : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
    
            
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Tell WebApi to use our custom Ioc (Ninject)
            //IocConfig.RegisterIoc(GlobalConfiguration.Configuration);   
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //RegisterRoutes(RouteTable.Routes);
          //  InitVariables();
        }

    
        private void InitVariables()
        {
            Session["CompanyID"] = Convert.ToInt32(ConfigurationManager.AppSettings["CompanyId"]); 
            Session["SeasonID"] = 1;
        }
    }
}