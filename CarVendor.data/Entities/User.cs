using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
   public class User:TEntity<long>
    {
        public int Individually { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }
        public string  NationalId { get; set; }
       
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
       public virtual ICollection<Order> Orders { get; set; }
    }
}
