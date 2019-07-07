using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Color:TEntity<long>
    {

        public String Name { get; set; }
        public virtual ICollection<CarColor> CarColors { get; set; }
    }
}
