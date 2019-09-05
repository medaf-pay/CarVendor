using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
   public class Address:TEntity<long>
    {
        public string MainAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<UserAddress> UserAddresses { get; set; }

    }
}
