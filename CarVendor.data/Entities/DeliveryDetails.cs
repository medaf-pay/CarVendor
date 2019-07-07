using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
   public class DeliveryDetails :TEntity<long>
    {
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }

    }
}
