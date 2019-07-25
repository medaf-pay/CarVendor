using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc.ViewModels
{
    public class filtersModel
    {
        public List<BaseViewModel> Brands { get; set; }
        public List<BaseViewModel> Categories { get; set; }
        public List<BaseViewModel> Colors { get; set; }
        public List<CarFamilyModel> CarFamilies { get; set; }
        public List<String> Models { get; set; }
    }

}