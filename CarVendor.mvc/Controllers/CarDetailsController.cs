using CarVendor.data;
using CarVendor.mvc.Models;
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
        [HttpGet]
        [Route("api/CarDetails/CartData")]
        public IHttpActionResult CartData(string SessionId)
        {
            var userCart =HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == SessionId);
            if (userCart == null)
                return Ok(NotFound());
            List<CartItemModel> Cars = new List<CartItemModel>();
            CartItemModel car;
            foreach (var item in userCart.CartItems)
            {
                if(Cars.Any(c=>c.CarId==item.CarId && c.Category.Id==item.Category.Id && c.Color.Id == item.Color.Id))
                {
                   Cars.Where(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id).Select(s=> { s.Quantity = s.Quantity + 1; return s; }).ToList();
                    continue;
                }
                car = new CartItemModel();
                car = db.Cars.Where(c => c.Id == item.CarId/* && c.Carcategories.Any(g=>g.CategoryId==item.Category.Id) && c.CarColors.Any(g => g.ColorId == item.Color.Id)*/).Select(s =>
                     new CartItemModel
                     {
                         Brand = s.Brand.Name,
                         CarId = s.Id,
                         CarName = s.Name,
                         Category = s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => new CategoryModel { Id = s1.Category.Id, Text = s1.Category.Name }).FirstOrDefault(),
                         Color = s.CarColors.Where(c => c.ColorId == item.Color.Id).Select(s1 => new ColorModel { Id = s1.Color.Id, Text = s1.Color.Name }).FirstOrDefault(),
                         Price = s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.Price).FirstOrDefault(),
                         Quantity = 1 }).FirstOrDefault();
                Cars.Add(car);
              }
            return Ok(Cars);

            // return View(userCart.CartItems);
        }
        [HttpPost]
        [Route("api/CartDetails/SetFinalItems")]
        public IHttpActionResult SetFinalItems(string SessionId,List<CartItemModel> Items)
        {
            HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == SessionId).CartItems = Items;
            return Ok();

            // return View(userCart.CartItems);
        }
        [HttpGet]
        [Route("api/CartDetails/GetFinalItems")]
        public IHttpActionResult GetFinalItems(string SessionId)
        {
         var Items=   HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == SessionId).CartItems;
            return Ok(Items);

            // return View(userCart.CartItems);
        }
        [HttpPost]
        [Route("api/CartDetails/Payment")]
        public IHttpActionResult Payment(string SessionId,CustomerInfoModel CustomerInfo)
        {
            HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == SessionId).CustomerInfo= CustomerInfo;

            return Ok();
        }
    }
}
