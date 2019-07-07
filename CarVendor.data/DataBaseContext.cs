using CarVendor.data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data
{
    public class DataBaseContext : DbContext
    {
        static DataBaseContext()
        {
            Database.SetInitializer<DataBaseContext>(null);
        }
        public DataBaseContext()
                   : base("name=CarVendorDb")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
