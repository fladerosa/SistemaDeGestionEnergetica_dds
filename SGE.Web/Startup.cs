using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SGE.Web.Startup))]
namespace SGE.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
