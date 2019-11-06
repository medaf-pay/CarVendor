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
using CarVendor.Web.Dtos;

namespace CarVendor.mvc.Controllers
{

    public class CarDetailsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();


        [HttpGet]
        [Route("api/CarDetails/IndexData")]
        public IHttpActionResult IndexData(long Brand = 0, long Family = 0, long Category = 0, long Color = 0)
        {
            decimal ExchangeRate=1;
            if (Utilities._currencyDTO.Code!=1)
            {
                ExchangeRate = db.Conversions.Where(cc => cc.ToCurrencyId == Utilities._currencyDTO.Code).OrderByDescending(o => o.CreationDate).Select(s => s.Value).FirstOrDefault();
            }
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
                            Price =s2.Price/ ExchangeRate,
                            Images = s2.CarImages.Select(s3 => new BaseViewModel { Id = s3.Id, Name = s3.ImageURL }).ToList(),
                            Discount =s2.Discount/ ExchangeRate,
                            NewPrice =(( s2.Price/ ExchangeRate) -(s2.Discount/ ExchangeRate))
                        }).ToList()


                    }).ToList(),
                    FirstImageView = s.Carcategories.Select(s1 => s1.CarColors.Select(s2 => s2.CarImages.Select(s3 => s3.ImageURL).FirstOrDefault()).FirstOrDefault()).FirstOrDefault(),
                    CarFamily = new CarFamilyModel { Id = s.Type.Id, Name = s.Type.Name },
                    SelectedCurrency =new CurrencyDTO() { Code = Utilities._currencyDTO.Code, Name = Utilities._currencyDTO.Name }
                    }).ToList();


            if (Brand != 0)
            {
                cars = cars.Where(c => c.BrandId == Brand).ToList();
            }
            if (Family != 0)
            {
                cars = cars.Where(c => c.CarFamily.Id == Family).ToList();
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
        [Route("api/CarDetails/GetCarByCode/{Id}")]
        public IHttpActionResult GetCarByCode(long Id)
        {

            CarModel cars = db.Cars.Where(c => c.Id == Id).Select(s =>
                    new CarModel
                    {
                        Brand = s.Brand.Id,
                        CarName = s.Name,
                        Id = s.Id,
                        Model = s.Model,
                        Options = s.Carcategories.Select(s1 =>
                     new Option
                     {
                         Id = s1.Id,
                         Category = s1.Category.Id,
                         moreDetails = s1.CarColors.Select(s2 => new MoreDetails
                         {
                             Id = s2.Id,
                             Color = s2.ColorId,
                             Quantity = s2.Quantity,
                             Price = s2.Price,
                             Discount = s2.Discount,
                             Images = s2.CarImages.Select(s3 => new BaseViewModel { Id = s3.Id, Name = s3.ImageURL }).ToList()

                         }).ToList()


                     }).ToList(),
                        CarFamily = s.TypeId
                    }).FirstOrDefault();

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
            decimal ExchangeRate = 1;
            if (Utilities._currencyDTO.Code != 1)
            {
                ExchangeRate = db.Conversions.Where(cc => cc.ToCurrencyId == Utilities._currencyDTO.Code).OrderByDescending(o => o.CreationDate).Select(s => s.Value).FirstOrDefault();
            }
            if (Utilities._shopingCarts.Count() == 0)
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
                    Cars.Where(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id).Select(s => { s.Quantity = s.Quantity + 1; return s; }).ToList();
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
                         Select(s1 => new ColorModel { Id = s1.Color.Id, Text = s1.Color.Name  , Price = s1.Price , NewPrice = s1.Price-s1.Discount}).FirstOrDefault()).FirstOrDefault(),
                         Price =( s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.CarColors.Select(s2 => s2.Price).FirstOrDefault()).FirstOrDefault())/ ExchangeRate,
                         NewPrice = (s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.CarColors.Select(s2 => s2.Price-s2.Discount).FirstOrDefault()).FirstOrDefault())/ ExchangeRate,

                         Quantity = item.Quantity == 0 ? 1 : item.Quantity,

                         Currency = new CurrencyDTO() { Code = Utilities._currencyDTO.Code, Name = Utilities._currencyDTO.Name }
                     }).FirstOrDefault();
                Cars.Add(car);
            }
            Utilities._shopingCarts.FirstOrDefault().CartItems = Cars;
            return Ok(Cars);
        }

        [HttpPost]
        [Route("api/CartDetails/SetFinalItems")]
        public IHttpActionResult SetFinalItems(List<CartItemModel> Items)
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
        public IHttpActionResult Payment(CustomerInfoModel CustomerInfo)
        {
            string Email = User.Identity.GetUserName();
            var Address = db.Users.Where(c => c.Email == Email).Select(s => s.UserAddresses.Where(c1 => c1.IsDeleted != true).OrderByDescending(o => o.Id).Select(s1 => s1.Address).FirstOrDefault()).FirstOrDefault();

            Address.DeliveryAddress = CustomerInfo.DeliveryAddress;
            db.SaveChanges();
            if (Utilities._shopingCarts.FirstOrDefault().CartItems.Sum(s => s.Quantity) > 10)
            {
                long UserId = db.Users.Where(c => c.Email == Email).Select(s => s.Id).FirstOrDefault();
                Utilities.SetOrderDetails(db, null, null, UserId);
                Utilities._shopingCarts = new List<CartModel>();
                return Ok(10);
            }

            return Ok(1);
        }

        [HttpPost]
        [Route("api/cartdetails/paybycreditcard")]
        public IHttpActionResult PayCreditCard(CreditCardModel creditCard)
        {
            string Email = User.Identity.GetUserName();
            long UserId = db.Users.Where(c => c.Email == Email).Select(s => s.Id).FirstOrDefault();
            long result = Utilities.SetOrderDetails(db, creditCard, null, UserId);
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
            long result = Utilities.SetOrderDetails(db, null, BankTransfer, UserId);
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
        public HttpResponseMessage AddNewCar(CarModel carModel)
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

        [Route("api/CartDetails/edit/{carCode}")]
        [HttpPost]
        public HttpResponseMessage Edit(long carCode, CarModel carModel)
        {
            Car car = db.Cars.Where(c => c.Id == carCode).FirstOrDefault();
            car.BrandId = carModel.Brand;
            car.Condition = CarCondition.New;
            car.IsDeleted = false;
            car.Model = carModel.Model;
            car.Name = carModel.CarName;
            car.TypeId = carModel.CarFamily;
            car.Carcategories.Select(s =>
            {

                s.IsDeleted = true;
                return s;
            }).ToList();

            car.Carcategories.Select(s => s.CarColors.Select(s1 =>
            {
                s1.IsDeleted = true; return s1;
            }).ToList()).ToList();

            foreach (var item in carModel.Options)
            {
                if (item.Id != 0)
                {
                    car.Carcategories.Where(c => c.Id == item.Id).Select(s => { s.Id = item.Id; s.CategoryId = item.Category; s.IsDeleted = false; return s; }).ToList();

                    foreach (var colorData in item.moreDetails)
                    {
                        if (car.Carcategories.Where(c => c.Id == item.Id).FirstOrDefault().CarColors.Any(c1 => c1.Id == colorData.Id))
                        {
                            var color = car.Carcategories.Where(c => c.Id == item.Id).Select(s => s.CarColors.Where(c1 => c1.Id == colorData.Id).FirstOrDefault()).FirstOrDefault();
                            color.Id = colorData.Id;
                            color.CarImages.Select(s2 => { s2.ImageURL = colorData.file; s2.IsDeleted = false; return s2; }).ToList();
                            color.ColorId = colorData.Color;
                            color.IsDeleted = false;
                            color.Price = colorData.Price;
                            color.Discount = colorData.Discount;
                            color.Quantity = colorData.Quantity;
                        }
                        else
                        {
                            CarColor CarColor;
                            CarColor = new CarColor();
                            CarColor.CarImages = new List<CarImage>()
                                {
                                    new CarImage{ ImageURL=colorData.file,IsDeleted=false}
                                };
                            CarColor.ColorId = colorData.Color;
                            CarColor.IsDeleted = false;
                            CarColor.Price = colorData.Price;
                            CarColor.Quantity = colorData.Quantity;
                            car.Carcategories.Where(c => c.Id == item.Id).FirstOrDefault().CarColors.Add(CarColor);
                        }
                    }
                }
                else
                {
                    CarCategory carCategory;
                    CarColor CarColor;

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
                    car.Carcategories.Add(carCategory);
                }
            }
            db.SaveChanges();

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/CartDetails/ChangeCurrency")]
        [HttpGet]
        public IHttpActionResult ChangeCurrency(long CCode)
        {

        var currency = db.Currencies.Where(c => c.Id == CCode).Select(s => new CurrencyDTO() { Code = s.Id, Name = s.Name }).FirstOrDefault();
            Utilities._currencyDTO = currency;

            return Ok(Utilities._currencyDTO);
        }

        [Route("api/CartDetails/ReadCurancy")]
        [HttpGet]
        public IHttpActionResult ReadCurrency()
        {
           return Ok(Utilities._currencyDTO);
        }

    }
}
