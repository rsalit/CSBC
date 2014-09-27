using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSBC.Admin.Web.Startup))]
namespace CSBC.Admin.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
