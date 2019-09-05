using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CarVendor.data;
using CarVendor.data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace CarVendor.mvc
{[AllowAnonymous]
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
      ///  OAuthGrantResourceOwnerCredentialsContext _context;
       //public CustomOAuthProvider(OAuthGrantResourceOwnerCredentialsContext context)
       // {
       //     this._context = context;
       // }
      
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = context.OwinContext.Get<DataBaseContext>().Users.FirstOrDefault(u => u.Email == context.UserName);
            if (user ==null ||!(user.Password==context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, user), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        private static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, User user)
        {
            var identity = new ClaimsIdentity("JWT");
            //identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //identity.AddClaim(new Claim("sub", context.UserName));


            List<Claim> UserRoles = new List<Claim>();
            foreach (var UserRole in user.UserRoles)
            {
                UserRoles.Add(new Claim(ClaimTypes.Role, UserRole.Role.Name));
            }
            //    ClaimsIdentity Claims = new ClaimsIdentity();
            identity.AddClaims(UserRoles);
            identity.AddClaims(new[]
                        {
                        new Claim(ClaimTypes.Name, user.FName),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Email", user.Email.ToString()),
                        new Claim("Name", user.FName.ToString())
                        });


            return identity;
        }
    }
}