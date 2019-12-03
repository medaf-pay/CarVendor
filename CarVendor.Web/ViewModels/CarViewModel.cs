using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarVendor.data.Entities;
using CarVendor.Web.Dtos;


namespace CarVendor.mvc
{
    public class CarViewModel
    {
       public CarViewModel()
        {
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public string FirstImageView { get; set; }
        public long BrandId { get; set; }
        public CarFamilyModel CarFamily { get;  set; }
        public  CurrencyDTO SelectedCurrency { get;  set; }
    }
    public class BaseViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
       
    }
    public class CategoryViewModel : BaseViewModel
    {
        public List<ColorViewModel> Colors { get; set; }
        public string CategoryCode { get; internal set; }
    }
    public class ColorViewModel : BaseViewModel
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public decimal NewPrice { get; set; }

        public List<BaseViewModel> Images { get; set; }
    }
    public class CarFamilyModel : BaseViewModel
    {

    }

}