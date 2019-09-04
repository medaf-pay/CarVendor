using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
   public class UserAddress:TEntity<long>
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public long AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
