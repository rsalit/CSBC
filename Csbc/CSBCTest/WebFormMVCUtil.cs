using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace CSBC.Admin.Web
{
    
    public class WebFormController : Controller { }
    public class WebFormMVCUtil
    {
        private static HtmlHelper html;

        public static HtmlHelper Html
        {
            get { return html ?? (html = new HtmlHelper(CreateViewContext(), new ViewUserControl())); }
        }
        public static ViewContext CreateViewContext()
        {
            var httpCtx = new HttpContextWrapper(System.Web.HttpContext.Current);
            var rt = new RouteData();
            rt.Values.Add("controller", "WebFormController");
            var ctx = new ControllerContext(new RequestContext(httpCtx, rt), new WebFormController());
            IView view = ViewEngines.Engines.FindPartialView(ctx, "RenderActionPartial").View;
            var viewContext = new ViewContext(
                ctx, 
                view, 
                new ViewDataDictionary(), 
                new TempDataDictionary(),
                HttpContext.Current.Response.Output) ;
            view.Render(viewContext, System.Web.HttpContext.Current.Response.Output);
            return viewContext;
        }
    }
}