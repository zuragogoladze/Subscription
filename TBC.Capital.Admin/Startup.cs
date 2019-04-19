using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TBC.Capital.Admin.Startup))]
namespace TBC.Capital.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
