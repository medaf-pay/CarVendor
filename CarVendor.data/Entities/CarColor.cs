using System.Collections.Generic;

namespace CarVendor.data.Entities
{
    public class CarColor:TEntity<long>
    {
        public long CarId { get; set; }
        public long ColorId { get; set; }
        public virtual ICollection<CarImage> CarImages { get; set; }
        public virtual Car Car { get; set; }
        public virtual Color Color { get; set; }
    }
}