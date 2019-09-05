using CarVendor.data.Entities;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace CarVendor.mvc.Common
{
    public static class JWTManager
    {

        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public static string GenerateToken(User user)
        {


            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            List<Claim> UserRoles = new List<Claim>();
            foreach(var UserRole in user.UserRoles)
            {
                UserRoles.Add(new Claim(ClaimTypes.Role, UserRole.Role.Name));
            }
            ClaimsIdentity Claims = new ClaimsIdentity();
            Claims.AddClaims(UserRoles);
            Claims.AddClaims(new[]
                        {
                        new Claim(ClaimTypes.Name, user.FName),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Email", user.Email.ToString()),
                        new Claim("Name", user.FName.ToString())
                        });
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = Claims,


                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }


}