using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Car : TEntity<long>
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public CarCondition Condition { get; set; }
        public int TypeId { get; set; }
        public CarFamily Type { get; set; }

        #region [ Brand ]
        public long BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        #endregion
        public virtual ICollection<CarCategory> Carcategories { get; set; }


    }
}
