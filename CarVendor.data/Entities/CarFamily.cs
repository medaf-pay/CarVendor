using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class CarFamily:TEntity<int>
    {
        public String Name { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
