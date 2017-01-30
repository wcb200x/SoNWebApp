using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoNWebApp.Startup))]
namespace SoNWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
