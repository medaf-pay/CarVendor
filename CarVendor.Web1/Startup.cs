using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarVendor.Web1.Startup))]
namespace CarVendor.Web1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
