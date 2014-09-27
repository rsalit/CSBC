using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Csbchoops.Web.Startup))]
namespace Csbchoops.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
