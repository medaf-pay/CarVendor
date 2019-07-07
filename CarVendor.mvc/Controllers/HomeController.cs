using CarVendor.data;
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
        public ActionResult Index()
        {
            var cars = from carCategoris in db.CarCategories
                       join carColors in db.CarColors on carCategoris.CarId equals carColors.CarId
                       select new CarViewModel { Name = carCategoris.Car.Name, Brand = carCategoris.Car.Brand.Name, Category = carCategoris.Category.Name, Color = carColors.Color.Name, Price = carCategoris.Price };
            var availableCars = cars.ToList();
            return View(availableCars);
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
    }
}