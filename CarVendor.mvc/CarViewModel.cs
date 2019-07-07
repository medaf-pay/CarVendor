using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc
{
    public class CarViewModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

    }
}