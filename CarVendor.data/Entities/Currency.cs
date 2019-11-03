using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Currency: TEntity<long>
    {
        public string Name { get; set; }

        public ICollection<Conversion> FromConversions { get; set; }
        public ICollection<Conversion> ToConversions { get; set; }

    }
}
