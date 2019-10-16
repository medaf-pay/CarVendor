using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using CarVendor.data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarVendor.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {



        public long? userId { get; set; }

        public virtual User user { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CarVendor.data.Entities.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<CarVendor.data.Entities.Car> Cars { get; set; }

        public System.Data.Entity.DbSet<CarVendor.data.Entities.Brand> Brands { get; set; }

        public System.Data.Entity.DbSet<CarVendor.data.Entities.CarFamily> CarFamilies { get; set; }

        public System.Data.Entity.DbSet<CarVendor.data.Entities.Category> Categories { get; set; }
        public System.Data.Entity.DbSet<CarVendor.data.Entities.Mail> Mails { get; set; }

    }
}