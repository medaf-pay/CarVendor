using CarVendor.data.Entities;
using CarVendor.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(CarVendor.Web.Startup))]
namespace CarVendor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "madmin@hoda.com";
                user.Email = "madmin@hoda.com";
                user.user = new User();
                user.user.Email = "madmin@hoda.com";
                user.user.Password = "123456";
                user.user.FName = "Mahmoud";
                user.user.MName = "Sayed";
                user.user.LName = "Omar";
                user.user.Individually = 1;
                user.user.NationalId = "12345678765456";
                user.user.Phone = "01000410514";
                user.user.UserAddresses = new List<UserAddress>() {
                new UserAddress() {
                    Address = new Address() {

                                                MainAddress="yegdyeddyhe",
                                                DeliveryAddress ="eebhebei",
                                                Country ="hydbyhxbwy",
                                                City ="uwywxuwi",
                                                State ="yxgwyxwy",
                                                Zip ="55555"

                             } } };


                var chkUser = UserManager.Create(user, "123456");

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("User"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

            }

        }
    }
}
