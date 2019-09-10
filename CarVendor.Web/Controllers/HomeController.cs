using CarVendor.data;
using CarVendor.mvc.Common;
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
      
        
        [Route("home/Index")]
        public ActionResult Index()
        {
            return View();
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
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Cart(CartModel model)
        {
           
            Utilities._shopingCarts = new List<CartModel>();
            Utilities._shopingCarts.Add(model);
            return Json(model);
        }
        [Authorize]
        [Route("Home/cart")]
        public ActionResult Cart(string RequestId)
        {
            
            return View();
        }
        [Authorize]
        [Route("Home/CustomerInfo")]
        public ActionResult CustomerInfo()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        [Route("Home/NewCar")]
        public ActionResult NewCar()
        {
            return View();
        }

        [Route("Home/CardInfo")]
        public ActionResult CardInfo(string RequestId)
        {
            var items = Utilities._shopingCarts.FirstOrDefault().CartItems;
            decimal total = 0;
            foreach (var item in items)
            {
             //   total += db.CarCategories.Where(c => c.CarId == item.CarId && c.CategoryId == item.Category.Id).Select(s => s.Price).FirstOrDefault() * item.Quantity;
            }
            ViewData["total"] = total;
                
                return View();
        }

    }
}