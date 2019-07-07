using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class CarImage: TEntity<long>
    {
        public string ImageURL { get; set; }
        public long CarColorId { get; set; }
        public virtual CarColor CarColor { get; set; }
    }
}
