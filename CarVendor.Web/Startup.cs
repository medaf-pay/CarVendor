using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarVendor.Web.Startup))]
namespace CarVendor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
