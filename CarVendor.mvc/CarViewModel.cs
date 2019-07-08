using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc
{
    public class CarViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<ColorViewModel> Colors { get; set; }
   

    }
    public class BaseViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
       
    }
    public class CategoryViewModel : BaseViewModel
    {
        public decimal Price { get; set; }
    }
    public class ColorViewModel : BaseViewModel
    {
        
        public List<BaseViewModel> Images { get; set; }
    }
  
}