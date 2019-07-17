using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Order:TEntity<long>
    {
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        #region [ DeliveryDetails ]
        public long DeliveryDetailsId { get; set; }
        [ForeignKey("DeliveryDetailsId")]
        public virtual CustomerDeliveryDetails DeliveryDetails { get; set; }
        #endregion
        
     
    }
}
