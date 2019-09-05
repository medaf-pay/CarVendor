
using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(CarVendor.mvc.Startup))]

namespace CarVendor.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }
    }
}
