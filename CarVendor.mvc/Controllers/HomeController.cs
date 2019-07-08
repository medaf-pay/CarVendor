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
        //[Route("home/Index")]
        //public ActionResult Index()
        //{
        //    //var cars = from carCategoris in db.CarCategories
        //    //           join carColors in db.CarColors on carCategoris.CarId equals carColors.CarId
        //    //           select new CarViewModel { Name = carCategoris.Car.Name, Brand = carCategoris.Car.Brand.Name, Category = carCategoris.Category.Name, Color = carColors.Color.Name, Price = carCategoris.Price };
        //    //var availableCars = cars.ToList();
        //    var cars = db.Cars.Select(s => 
        //        new CarViewModel {Brand=s.Brand.Name,Name=s.Name,Id=s.Id,
        //        Categories =s.Carcategories.Select(s1=>new CategoryViewModel{ Id=s1.Category.Id,Name=s1.Category.Name,Price=s1.Price }).ToList(),
        //        Colors =s.CarColors.Select(s2=>new ColorViewModel {Id=s2.Color.Id,Name=s2.Color.Name,
        //            Images =s2.CarImages.Select(s3=>new BaseViewModel {Id=s3.Id,Name=s3.ImageURL }).ToList()

        //        }).ToList() }).ToList();
        //  //  ViewData["Colors"] = new SelectList(cars.Select(s=>s.Colors).ToList(), "Id", "Name");
        //    return View(cars);
        //}
        [Route("home/Index/{id}")]
        public ActionResult Index(long id=0)
        {
           
            var cars = db.Cars.Select(s =>
                new CarViewModel
                {
                    Brand = s.Brand.Name,
                    Name = s.Name,
                    Id = s.Id,
                    Categories = s.Carcategories.Select(s1 => new CategoryViewModel { Id = s1.Category.Id, Name = s1.Category.Name, Price = s1.Price }).ToList(),
                    Colors = s.CarColors.Select(s2 => new ColorViewModel
                    {
                        Id = s2.Color.Id,
                        Name = s2.Color.Name,
                        Images = s2.CarImages.Select(s3 => new BaseViewModel { Id = s3.Id, Name = s3.ImageURL }).ToList()

                    }).ToList()
                }).ToList();
            if(id!=0)
            {
                cars = cars.Where(c => c.Id == id).ToList();
            }
            //  ViewData["Colors"] = new SelectList(cars.Select(s=>s.Colors).ToList(), "Id", "Name");
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

        public ActionResult Cart()
        {

            return View();
        }
    }
}