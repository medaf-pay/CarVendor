using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc.ViewModels
{
    public class CarModel
    {
        public string CarName { get; set; }
        public string Model { get; set; }
        public long Brand { get; set; }
        public int CarFamily { get; set; }
        public List<Option> Options { get; set; }
        public long Id { get; internal set; }
    }

    public class Option
    {
        public long Category { get; set; }
        public List<MoreDetails> moreDetails { get; set; }
      
    }
    public class MoreDetails
    {
       
        public decimal Price { get; set; }
        public long Color { get; set; }
        public int Quantity { get; set; }
        public string file { get; set; }
        public List<BaseViewModel> Images { get; internal set; }
    }
}