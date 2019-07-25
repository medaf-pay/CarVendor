using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class CarCategory : TEntity<long>
    {
     
        public long CarId { get; set; }
        public virtual Car Car { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<CarColor> CarColors { get; set; }
    }
}
