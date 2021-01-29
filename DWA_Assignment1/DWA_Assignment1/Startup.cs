using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DWA_Assignment1.Startup))]
namespace DWA_Assignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
