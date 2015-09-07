using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trend.Web.Startup))]
namespace Trend.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
