using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class TEntity<T>
          where T : struct
    {
        public T Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
