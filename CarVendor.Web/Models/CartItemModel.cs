using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc.Models
{
    public class CartItemModel
    {
        public long CarId { get; set; }
        public string CarName { get; set; }
        public string Brand { get; set; }
        public ColorModel Color { get; set; }
        public CategoryModel Category { get; set; }
        public decimal  Price { get; set; }
        public decimal NewPrice { get; set; }

        public long Quantity { get; set; }
    }
}