
using CarVendor.data;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
//using Microsoft.Owin.Security.Jwt;
using Owin;


namespace CarVendor.mvc
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = System.Configuration.ConfigurationManager.AppSettings["issuer"];
            var secret = TextEncodings.Base64Url.Decode(System.Configuration.ConfigurationManager.AppSettings["secret"]);
            app.CreatePerOwinContext(() => new DataBaseContext());


            app.UseJwtBearerAuthentication(new Microsoft.Owin.Security.Jwt.JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
        {
        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
        }
            });
        }
    }
}