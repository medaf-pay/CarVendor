using CarVendor.data;
using CarVendor.mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarVendor.mvc.ViewModels;
using CarVendor.data.Entities;
using CarVendor.mvc.Dtos;
using CarVendor.mvc.Common;
using System.Web;
using System.IO;
using Microsoft.AspNet.Identity;

namespace CarVendor.mvc.Controllers
{

    public class CarDetailsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();


        [HttpGet]
        [Route("api/CarDetails/IndexData")]
        public IHttpActionResult IndexData(long Brand = 0, long Category = 0, long Color = 0)
        {

            var cars = db.Cars.Select(s =>
                new CarViewModel
                {
                    Brand = s.Brand.Name,
                    BrandId = s.BrandId,
                    Name = s.Name,
                    Id = s.Id,
                    Categories = s.Carcategories.Select(s1 =>
                    new CategoryViewModel
                    {
                        Id = s1.Category.Id,
                        Name = s1.Category.Name,
                        Colors = s1.CarColors.Select(s2 => new ColorViewModel
                        {
                            Id = s2.ColorId,
                            Name = s2.Color.Name,
                            Price = s2.Price,
                            Images = s2.CarImages.Select(s3 => new BaseViewModel { Id = s3.Id, Name = s3.ImageURL }).ToList()

                        }).ToList()


                    }).ToList(),
                    FirstImageView = s.Carcategories.Select(s1 => s1.CarColors.Select(s2 => s2.CarImages.Select(s3 => s3.ImageURL).FirstOrDefault()).FirstOrDefault()).FirstOrDefault(),
                    CarFamily = new CarFamilyModel { Id = s.Type.Id, Name = s.Type.Name }
                }).ToList();
            if (Brand != 0)
            {
                cars = cars.Where(c => c.BrandId == Brand).ToList();
            }

            if (Category != 0)
            {
                cars = cars.Select(s => { s.Categories = s.Categories.Where(c1 => c1.Id == Category).Select(s1 => { return s1; }).ToList(); return s; }).Where(c => c.Categories.Count > 0).ToList();
                cars = cars.Where(c => c.Categories.Count > 0).ToList();
            }
            if (Color != 0)
            {
                cars = cars.Select(s =>
                {
                    s.Categories = s.Categories.Select(s1 =>
                    { s1.Colors = s1.Colors.Where(c => c.Id == Color).Select(s2 => { return s2; }).ToList(); return s1; }).Where(c1 => c1.Colors.Count > 0).ToList();
                    s.FirstImageView = s.Categories.Select(s1 => s1.Colors.Select(s2 => s2.Images.Select(s3 => s3.Name).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();

                    return s;
                }).Where(c => c.Categories.Count > 0).ToList();

            }
            return Ok(cars);
        }

        [HttpGet]
        [Route("api/CarDetails/GetImageByColorId/{carId}/{colorId}")]
        public IHttpActionResult GetImageByColorId(long carId, long colorId)
        {
            var image = db.CarColors.Where(c => c.CarCategoryId == carId & c.ColorId == colorId).Select(s => s.CarImages.Select(s1 => s1.ImageURL).FirstOrDefault()).FirstOrDefault();
            return Ok("/images/" + image);
        }

        [HttpGet]
        [Route("api/CarDetails/GetPriceCategoryId/{carId}/{categoryId}")]
        public IHttpActionResult GetPriceCategoryId(long carId, long categoryId)
        {
            var Price = db.CarCategories.Where(c => c.CarId == carId && c.CategoryId == categoryId).FirstOrDefault();
            return Ok(Price);
        }

        [HttpGet]
        [Route("api/CarDetails/CartData")]
        public IHttpActionResult CartData()
        {
            if (Utilities._shopingCarts.Count()== 0)
                return Ok(new List<CartItemModel>());

                var userCart = Utilities._shopingCarts.FirstOrDefault();

            if (userCart.CartItems == null)
                return Ok(new List<CartItemModel>());
            List<CartItemModel> Cars = new List<CartItemModel>();
            CartItemModel car;
            foreach (var item in userCart.CartItems)
            {
                if (Cars.Any(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id))
                {
                    Cars.Where(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id).Select(s => { s.Quantity = s.Quantity +1; return s; }).ToList();
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
                         Color = s.Carcategories.Where(c => c.CategoryId == item.Category.Id).
                         Select(s2 => s2.CarColors.Where(c => c.ColorId == item.Color.Id).
                         Select(s1 => new ColorModel { Id = s1.Color.Id, Text = s1.Color.Name, Price = s1.Price }).FirstOrDefault()).FirstOrDefault(),
                         Price = s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.CarColors.Select(s2=>s2.Price).FirstOrDefault()).FirstOrDefault(),
                         Quantity = item.Quantity==0?1: item.Quantity
                     }).FirstOrDefault();
                Cars.Add(car);
            }
            Utilities._shopingCarts.FirstOrDefault().CartItems = Cars;
            return Ok(Cars);
        }

        [HttpPost]
        [Route("api/CartDetails/SetFinalItems")]
        public IHttpActionResult SetFinalItems( List<CartItemModel> Items)
        {
            Utilities._shopingCarts.FirstOrDefault().CartItems = Items;
            return Ok();

            // return View(userCart.CartItems);
        }

        [HttpGet]
        [Route("api/CartDetails/GetFinalItems")]
        public IHttpActionResult GetFinalItems()
        {
            var Items = Utilities._shopingCarts.FirstOrDefault().CartItems;
            return Ok(Items);
        }

        [HttpPost]
        [Route("api/CartDetails/Payment")]
        public IHttpActionResult Payment( CustomerInfoModel CustomerInfo)
        {
            string Email = User.Identity.GetUserName();
            var Address = db.Users.Where(c => c.Email == Email).Select(s => s.UserAddresses.Where(c1 => c1.IsDeleted != true).OrderByDescending(o => o.Id).Select(s1 => s1.Address).FirstOrDefault()).FirstOrDefault();

            Address.DeliveryAddress = CustomerInfo.DeliveryAddress;
            db.SaveChanges();
            if(Utilities._shopingCarts.FirstOrDefault().CartItems.Sum(s=>s.Quantity)>10)
            {
                long UserId = db.Users.Where(c => c.Email == Email).Select(s => s.Id).FirstOrDefault();
                Utilities.SetOrderDetails(db,null,null, UserId);
                Utilities._shopingCarts = new List<CartModel>();
                return Ok(10);
            }

            return Ok(1);
        }

        [HttpPost]
        [Route("api/cartdetails/paybycreditcard")]
        public IHttpActionResult PayCreditCard( CreditCardModel creditCard)
        {
            string Email = User.Identity.GetUserName();
            long UserId = db.Users.Where(c => c.Email == Email).Select(s => s.Id).FirstOrDefault();
            long result = Utilities.SetOrderDetails( db, creditCard,null, UserId);
            if (result == -1)
                return NotFound();
            Utilities._shopingCarts = new List<CartModel>();
            return Ok(result);
        }

        [HttpPost]
        [Route("api/cartdetails/SetInfoBankTransfer")]
        public IHttpActionResult SetInfoBankTransfer(string sessionId, BankTransferModel BankTransfer)
        {
            string Email = User.Identity.GetUserName();
            long UserId = db.Users.Where(c => c.Email == Email).Select(s => s.Id).FirstOrDefault();
            long result = Utilities.SetOrderDetails( db, null, BankTransfer, UserId);
            if (result == -1)
                return NotFound();
            Utilities._shopingCarts = new List<CartModel>();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/CartDetails/getFilters")]
        public IHttpActionResult getFilters()
        {
            filtersModel filters = new filtersModel();
            filters.Brands = db.Brands.Select(s => new BaseViewModel { Id = s.Id, Name = s.Name }).ToList();
            filters.Categories = db.Categories.Select(s => new BaseViewModel { Id = s.Id, Name = s.Name }).ToList();
            filters.Colors = db.Colors.Select(s => new BaseViewModel { Id = s.Id, Name = s.Name }).ToList();
            filters.CarFamilies = db.CarFamilies.Select(s => new CarFamilyModel { Id = s.Id, Name = s.Name }).ToList();
            filters.Models = new List<string>();
            var year = DateTime.Now.Year;
            for (int y = year + 1; y > year - 10; y--)
            {

                filters.Models.Add(y.ToString());

            }
            return Ok(filters);
        }

        //[HttpGet]
        //[Route("api/CartDetails/Payment")]
        //public IHttpActionResult Payment()
        //{
        //    return Ok();
        //}

        [Route("api/CartDetails/UploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            List<string> filesPaths = new List<string>();
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/images/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            foreach (string key in HttpContext.Current.Request.Files)
            {
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[key];
                postedFile.SaveAs(path + postedFile.FileName);
                filesPaths.Add(postedFile.FileName);
            }

            //Send OK Response to Client.
            return Request.CreateResponse(filesPaths);
        }

        [Route("api/CartDetails/AddNewCar")]
        [HttpPost]
        public HttpResponseMessage AddNewCar(NewCarModel carModel)
        {

            Car car = new Car()
            {
                BrandId = carModel.Brand,
                Condition = CarCondition.New,
                IsDeleted = false,
                Model = carModel.Model,
                Name = carModel.CarName,
                TypeId = carModel.CarFamily
            };
            List<CarCategory> carCategories = new List<CarCategory>();
            CarCategory carCategory;
            CarColor CarColor;
            foreach (var item in carModel.Options)
            {
                carCategory = new CarCategory();
                carCategory.CategoryId = item.Category;
                carCategory.IsDeleted = false;
                carCategory.CarColors = new List<CarColor>();
                foreach (var colorData in item.moreDetails)
                {
                    CarColor = new CarColor();
                    CarColor.CarImages = new List<CarImage>()
                    {
                        new CarImage{ ImageURL=colorData.file,IsDeleted=false}
                    };
                    CarColor.ColorId = colorData.Color;
                    CarColor.IsDeleted = false;
                    CarColor.Price = colorData.Price;
                    CarColor.Quantity = colorData.Quantity;

                    carCategory.CarColors.Add(CarColor);
                }
                carCategories.Add(carCategory);

            }
            car.Carcategories = carCategories;
            db.Cars.Add(car);
            db.SaveChanges();

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
