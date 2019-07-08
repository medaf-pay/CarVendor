using CarVendor.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarVendor.mvc.Controllers
{
 
    public class CarDetailsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();
        [HttpGet]
        [Route("api/CarDetails/GetImageByColorId/{carId}/{colorId}")]
        public IHttpActionResult GetImageByColorId(long carId,long colorId)
        {
            var image = db.CarColors.Where(c => c.CarId == carId & c.ColorId == colorId).Select(s=>s.CarImages.Select(s1=>s1.ImageURL).FirstOrDefault()).FirstOrDefault();
            return Ok("/images/"+image);
        }
        [HttpGet]
        [Route("api/CarDetails/GetPriceCategoryId/{carId}/{categoryId}")]
        public IHttpActionResult GetPriceCategoryId(long carId, long categoryId)
        {
            var Price = db.CarCategories.Where(c => c.CarId == carId && c.CategoryId == categoryId).Select(s => s.Price).FirstOrDefault();
            return Ok(Price);
        }
      

    }
}
