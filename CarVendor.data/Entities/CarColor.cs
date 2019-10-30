using System.Collections.Generic;

namespace CarVendor.data.Entities
{
    public class CarColor:TEntity<long>
    {
        public long CarCategoryId { get; set; }
        public long ColorId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<CarImage> CarImages { get; set; }
        public virtual CarCategory CarCategory { get; set; }
        public virtual Color Color { get; set; }
        public virtual ICollection<Video> Videos { get; set; }


    }
}