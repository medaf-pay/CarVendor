using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Order:TEntity<long>
    {
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        #region [ DeliveryDetails ]
        public long DeliveryDetailsId { get; set; }
        public virtual DeliveryDetails DeliveryDetails { get; set; }
        #endregion
        
        #region [ Owner ]
        public long OwnerId { get; set; }
        public virtual User Owner { get; set; }
        #endregion
    }
}
