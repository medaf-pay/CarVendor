using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Carosel
    {
        public int Id { get; set; }
        [Display(Name = "Car Model")]
        public string Model { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Price")]
        public decimal? Price { get; set; }
        [Display(Name = "Mileage")]
        public string Mileage { get; set; }
        [Display(Name = "Volume Capacity")]
        public string VolumeCapacity { get; set; }
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }



    }
}
