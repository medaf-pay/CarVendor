using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Brand : TEntity<long>
    {
        public string Name { get; set; }
    }
}
