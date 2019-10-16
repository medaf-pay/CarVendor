using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Mail: TEntity<long>
    {
        public string mail { get; set; }
    }
}
