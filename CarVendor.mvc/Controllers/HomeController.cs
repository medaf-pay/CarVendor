using CarVendor.data;
using CarVendor.mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarVendor.mvc.Controllers
{
    public class HomeController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        public static List<CartModel> _shopingCarts = new List<CartModel>();
        
        [Route("home/Index")]
        public ActionResult Index(long Category=0, long Color=0)
        {


            var cars = db.Cars.Select(s =>
                new CarViewModel
                {
                    Brand = s.Brand.Name,
                    BrandId=s.BrandId,
                    Name = s.Name,
                    Id = s.Id,
                    FirstImageView=s.CarColors.Select(s1=>s1.CarImages.Select(s3=>s3.ImageURL).FirstOrDefault()).FirstOrDefault(),
                    Categories = s.Carcategories.Select(s1 => new CategoryViewModel { Id = s1.Category.Id, Name = s1.Category.Name, Price = s1.Price }).ToList(),
                    Colors = s.CarColors.Select(s2 => new ColorViewModel
                    {
                        Id = s2.Color.Id,
                        Name = s2.Color.Name,
                        Images = s2.CarImages.Select(s3 => new BaseViewModel { Id = s3.Id, Name = s3.ImageURL }).ToList()

                    }).ToList()
                }).ToList();
        
            if (Category != 0)
            {
                cars = cars.Where(c => c.Categories.Any(c1=>c1.Id==Category)).ToList();
            }
            if (Color != 0)
            {
                cars = cars.Where(c => c.Colors.Any(c1 => c1.Id == Color)).Select(s=> { s.FirstImageView = s.Colors.Where(w => w.Id == Color).Select(s1 => s1.Images.Select(s3 => s3.Name).FirstOrDefault()).FirstOrDefault(); return s; }).ToList();
               
            }
            return View(cars);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Cart(CartModel model)
        {
            var userGuid = Guid.NewGuid();
            model.Guid = userGuid.ToString();
            model.SessionId = model.Guid;
            _shopingCarts.Add(model);
            return Json(model);
        }

        [Route("Home/cart")]
        public ActionResult Cart(string RequestId)
        {
            
            return View();
        }

        [Route("Home/CustomerInfo")]
        public ActionResult CustomerInfo()
        {
            return View();
        }

        [Route("Home/NewCar")]
        public ActionResult NewCar()
        {
            return View();
        }

        [Route("Home/CardInfo")]
        public ActionResult CardInfo(string RequestId)
        {
            var items = _shopingCarts.FirstOrDefault(cart => cart.SessionId == RequestId).CartItems;
            decimal total = 0;
            foreach (var item in items)
            {
                total += db.CarCategories.Where(c => c.CarId == item.CarId && c.CategoryId == item.Category.Id).Select(s => s.Price).FirstOrDefault() * item.Quantity;
            }
            ViewData["total"] = total;
                
                return View();
        }

    }
}