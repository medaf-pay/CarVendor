using CarVendor.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CustomAuthenticationMVC.CustomAuthentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Role> Roles { get; set; }

        #endregion

        public CustomMembershipUser(User user):base("CustomMembership", user.Email, user.Id, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId =(int) user.Id;
            FirstName = user.FName;
            LastName = user.LName;
            Roles = user.UserRoles != null ? user.UserRoles.Where(c=>c.IsDeleted!=true).Select(s => new Role { Id = s.Role.Id, Name = s.Role.Name }).ToList():new List<Role>();
        }
    }
}