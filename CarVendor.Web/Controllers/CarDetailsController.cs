using CarVendor.data;
using CarVendor.data.Entities;
using CarVendor.mvc.Common;
using CarVendor.mvc.Models;
using CarVendor.mvc.ViewModels;
using CarVendor.Web.Dtos;

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarVendor.mvc.Controllers
{

    public class CarDetailsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        [HttpGet]
        [Route("api/CarDetails/IndexData")]
        public IHttpActionResult IndexData(long Brand = 0, long Family = 0, long Category = 0, long Color = 0,int currencyCode=0)
        {
           

            decimal ExchangeRate =1;
            CurrencyDTO currency = Utilities._currencyDTO.Select(s=>new CurrencyDTO {Code=s.Code,Name=s.Name }).FirstOrDefault();
            if(currencyCode!=0)
            {
                ExchangeRate = db.Conversions.Where(cc => cc.FromCurrencyId == currencyCode).OrderByDescending(o => o.CreationDate).Select(s => s.Value).FirstOrDefault();
                currency = db.Currencies.Where(c => c.Id == currencyCode).Select(s => new CurrencyDTO { Code = s.Id, Name = s.Name }).FirstOrDefault();
             

            }
            if (Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).Select(s => s.Code).FirstOrDefault() != 0 && Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).Select(s=>s.Code).FirstOrDefault()!=1)
            { var code = Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).First().Code;
                ExchangeRate = db.Conversions.Where(cc => cc.FromCurrencyId == code).OrderByDescending(o => o.CreationDate).Select(s => s.Value).FirstOrDefault();
                currency.Code = Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).First().Code;
                currency.Name = Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).First().Name;


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
                        CategoryCode = "c" + s1.CategoryId + "c",
                        Colors = s1.CarColors.Select(s2 => new ColorViewModel
                        {
                            Id = s2.ColorId,
                            Name = s2.Color.Name,
                            Price = s2.Price / ExchangeRate,
                            Images = s2.CarImages.Select(s3 => new BaseViewModel { Id = s3.Id, Name = s3.ImageURL }).ToList(),
                            Discount = s2.Discount / ExchangeRate,
                            NewPrice = ((s2.Price / ExchangeRate) - (s2.Discount / ExchangeRate))
                        }).ToList()


                    }).ToList(),
                    FirstImageView = s.Carcategories.Select(s1 => s1.CarColors.Select(s2 => s2.CarImages.Select(s3 => s3.ImageURL).FirstOrDefault()).FirstOrDefault()).FirstOrDefault(),
                    CarFamily = new CarFamilyModel { Id = s.Type.Id, Name = s.Type.Name },
                    SelectedCurrency =new CurrencyDTO() { Code = currency.Code, Name = currency.Name }
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
            var userId = User.Identity.GetUserId();
            if (Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).Select(s => s.Code).FirstOrDefault() !=0 && Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).Select(s=>s.Code).FirstOrDefault() != 1)
            {
                var code = Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).First().Code;
                ExchangeRate = db.Conversions.Where(cc => cc.FromCurrencyId == code).OrderByDescending(o => o.CreationDate).Select(s => s.Value).FirstOrDefault();
            }
            if (Utilities._shopingCarts.Count() == 0)
            {
                return Ok(new List<CartItemModel>());
            }

            var userCart = Utilities._shopingCarts.Where(s => s.UserId == userId).FirstOrDefault();

            if (userId == null || userCart == null)
            {
                return Ok(new List<CartItemModel>());
            }
            List<CartItemModel> Cars = new List<CartItemModel>();

            CartItemModel car;
            foreach (var item in userCart.CartItems)
            {
                if (Cars.Count > 0 && Cars.Any(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id))
                {
                    Cars.Where(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id).Select(s => { s.Quantity = s.Quantity + 1; return s; }).ToList();
                    continue;
                }
                CurrencyDTO currency = Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).Select(s =>new CurrencyDTO { Code = s.Code, Name = s.Name }).FirstOrDefault();


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
                         Select(s1 => new ColorModel { Id = s1.Color.Id, Text = s1.Color.Name, Price = (s1.Price / ExchangeRate), NewPrice = ((s1.Price / ExchangeRate) - (s1.Discount / ExchangeRate)) }).FirstOrDefault()).FirstOrDefault(),
                         Price = (s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.CarColors.Select(s2 => s2.Price).FirstOrDefault()).FirstOrDefault()) / ExchangeRate,
                         NewPrice = (s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.CarColors.Select(s2 => s2.Price - s2.Discount).FirstOrDefault()).FirstOrDefault()) / ExchangeRate,

                         Quantity = item.Quantity == 0 ? 1 : item.Quantity,

                        
            }).FirstOrDefault();
                car.Currency = currency;
                Cars.Add(car);
            }
            Utilities._shopingCarts.Where(c=>c.UserId==userId).FirstOrDefault().CartItems = Cars;
            return Ok(Cars);
        }

        [HttpPost]
        [Route("api/CartDetails/SetFinalItems")]
        public IHttpActionResult SetFinalItems(List<CartItemModel> Items)
        {
            Utilities._shopingCarts.Where(s=>s.UserId== User.Identity.GetUserId()).FirstOrDefault().CartItems = Items;
            return Ok();

            // return View(userCart.CartItems);
        }

        [HttpGet]
        [Route("api/CartDetails/GetFinalItems")]
        public IHttpActionResult GetFinalItems()
        {
            CartData();
            var Items = Utilities._shopingCarts.Where(s => s.UserId == User.Identity.GetUserId()).FirstOrDefault().CartItems;
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
            if (Utilities._shopingCarts.Where(s => s.UserId == User.Identity.GetUserId()).FirstOrDefault().CartItems.Sum(s => s.Quantity) > 10)
            {
                long UserId = db.Users.Where(c => c.Email == Email).Select(s => s.Id).FirstOrDefault();
                Utilities.SetOrderDetails(db, null, null, UserId);
                Utilities._shopingCarts.RemoveAll(s => s.UserId == User.Identity.GetUserId()); 
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
            {
                return NotFound();
            }

            Utilities._shopingCarts.RemoveAll(s => s.UserId == User.Identity.GetUserId());
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
            {
                return NotFound();
            }

            Utilities._shopingCarts.RemoveAll(s => s.UserId == User.Identity.GetUserId());
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
        [Route("api/EditCategory/UploadFile")]
        [HttpPost]
        public HttpResponseMessage UploadFile(long? id, int Category)
        {
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            Car car = db.Cars.Find(id);
            var CarCategory = car.Carcategories.Where(cat => cat.CategoryId == Category).FirstOrDefault();


            if (postedFile.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(postedFile.FileName).ToLower();
                string query = null;
                string connString = "";




                string[] validFileTypes = { ".xls", ".xlsx" };

                string filePath = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/Content/Uploads"), postedFile.FileName);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    postedFile.SaveAs(filePath);
                    if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";


                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    OleDbConnection oledbConn = new OleDbConnection(connString);
                    DataTable dt = new DataTable();
                    try
                    {



                        oledbConn.Open();
                        using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                        {
                            OleDbDataAdapter oleda = new OleDbDataAdapter();
                            oleda.SelectCommand = cmd;
                            DataSet ds = new DataSet();
                            oleda.Fill(ds);

                            dt = ds.Tables[0];
                        }

                        #region Region


                        CarCategory.EngineCapacity = int.Parse(dt.Rows[0]["EngineCapacity"].ToString());


                        CarCategory.TransfersNo = int.Parse(dt.Rows[0]["TransfersNo"].ToString());


                        CarCategory.CylindersNo = int.Parse(dt.Rows[0]["CylindersNo"].ToString());


                        CarCategory.CastingsNo = int.Parse(dt.Rows[0]["CastingsNo"].ToString());


                        CarCategory.ElectronicFuelInjection = dt.Rows[0]["ElectronicFuelInjection"].ToString() == "0" ? false : true;


                        CarCategory.MaximumTorque = dt.Rows[0]["MaximumTorque"].ToString();


                        CarCategory.EnginePower = dt.Rows[0]["EnginePower"].ToString();


                        CarCategory.Acceleration = int.Parse(dt.Rows[0]["Acceleration"].ToString());


                        CarCategory.TractionType = dt.Rows[0]["TractionType"].ToString();


                        CarCategory.SeatsNo = int.Parse(dt.Rows[0]["SeatsNo"].ToString());


                        CarCategory.DoorsNo = int.Parse(dt.Rows[0]["DoorsNo"].ToString());


                        CarCategory.AvgFuelConsumption = int.Parse(dt.Rows[0]["AvgFuelConsumption"].ToString());


                        CarCategory.FuelTankCapacity = int.Parse(dt.Rows[0]["FuelTankCapacity"].ToString());


                        CarCategory.GroundClearance = int.Parse(dt.Rows[0]["GroundClearance"].ToString());


                        CarCategory.MaxSpeed = int.Parse(dt.Rows[0]["MaxSpeed"].ToString());


                        CarCategory.FuelRecommended = dt.Rows[0]["FuelRecommended"].ToString();


                        CarCategory.DriverAirbags = dt.Rows[0]["DriverAirbags"].ToString() == "0" ? false : true;


                        CarCategory.FrontPassengerAirbags = dt.Rows[0]["FrontPassengerAirbags"].ToString() == "0" ? false : true;


                        CarCategory.ElectricChairs = dt.Rows[0]["ElectricChairs"].ToString() == "0" ? false : true;


                        CarCategory.BrakeSystemABS = dt.Rows[0]["BrakeSystemABS"].ToString() == "0" ? false : true;


                        CarCategory.ElectronicBrakeDistribution = dt.Rows[0]["ElectronicBrakeDistribution"].ToString() == "0" ? false : true;


                        CarCategory.ElectronicBalanceProgram = dt.Rows[0]["ElectronicBalanceProgram"].ToString() == "0" ? false : true;


                        CarCategory.AntitheftAlarmSystem = dt.Rows[0]["AntitheftAlarmSystem"].ToString() == "0" ? false : true;


                        CarCategory.ImmobilizerSystemAgainstTheft = dt.Rows[0]["ImmobilizerSystemAgainstTheft"].ToString() == "0" ? false : true;


                        CarCategory.SportRims = dt.Rows[0]["SportRims"].ToString() == "0" ? false : true;


                        CarCategory.RimSize = int.Parse(dt.Rows[0]["RimSize"].ToString());


                        CarCategory.FrontFogLanterns = dt.Rows[0]["FrontFogLanterns"].ToString() == "0" ? false : true;


                        CarCategory.BackFogLanterns = dt.Rows[0]["BackFogLanterns"].ToString() == "0" ? false : true;


                        CarCategory.BackCleaners = dt.Rows[0]["BackCleaners"].ToString() == "0" ? false : true;


                        CarCategory.ElectricSideMirrors = dt.Rows[0]["ElectricSideMirrors"].ToString() == "0" ? false : true;


                        CarCategory.ElectricallyFoldingSideMirrors = dt.Rows[0]["ElectricallyFoldingSideMirrors"].ToString() == "0" ? false : true;


                        CarCategory.SideMirrorsSignals = dt.Rows[0]["SideMirrorsSignals"].ToString() == "0" ? false : true;


                        CarCategory.XenonBulbsLighting = dt.Rows[0]["XenonBulbsLighting"].ToString() == "0" ? false : true;


                        CarCategory.HeadlampWipers = dt.Rows[0]["HeadlampWipers"].ToString() == "0" ? false : true;


                        CarCategory.SensitiveHeadlamps = dt.Rows[0]["SensitiveHeadlamps"].ToString() == "0" ? false : true;


                        CarCategory.HeadlampControl = dt.Rows[0]["HeadlampControl"].ToString() == "0" ? false : true;


                        CarCategory.HeadlampLightingLED = dt.Rows[0]["HeadlampLightingLED"].ToString() == "0" ? false : true;


                        CarCategory.TaillightsLightingLED = dt.Rows[0]["TaillightsLightingLED"].ToString() == "0" ? false : true;


                        CarCategory.BackSpoiler = dt.Rows[0]["BackSpoiler"].ToString() == "0" ? false : true;


                        CarCategory.IntelligentParkingSystem = dt.Rows[0]["IntelligentParkingSystem"].ToString() == "0" ? false : true;


                        CarCategory.SoundSystem = dt.Rows[0]["SoundSystem"].ToString() == "0" ? false : true;


                        CarCategory.CDDriver = dt.Rows[0]["CDDriver"].ToString() == "0" ? false : true;


                        CarCategory.AUXPort = dt.Rows[0]["AUXPort"].ToString() == "0" ? false : true;


                        CarCategory.USBPort = dt.Rows[0]["USBPort"].ToString() == "0" ? false : true;


                        CarCategory.Bluetooth = dt.Rows[0]["Bluetooth"].ToString() == "0" ? false : true;


                        CarCategory.FrontHeadrests = dt.Rows[0]["FrontHeadrests"].ToString() == "0" ? false : true;


                        CarCategory.RearHeadrests = dt.Rows[0]["RearHeadrests"].ToString() == "0" ? false : true;


                        CarCategory.ElectricWindshield = dt.Rows[0]["ElectricWindshield"].ToString() == "0" ? false : true;


                        CarCategory.ElectricRearGlass = dt.Rows[0]["ElectricRearGlass"].ToString() == "0" ? false : true;


                        CarCategory.OneTouchGlassControl = dt.Rows[0]["OneTouchGlassControl"].ToString() == "0" ? false : true;


                        CarCategory.RemoteControlToLockAndOpenDoors = dt.Rows[0]["RemoteControlToLockAndOpenDoors"].ToString() == "0" ? false : true;


                        CarCategory.DriverHeightControl = dt.Rows[0]["DriverHeightControl"].ToString() == "0" ? false : true;


                        CarCategory.LeatherBrushes = dt.Rows[0]["LeatherBrushes"].ToString() == "0" ? false : true;


                        CarCategory.EngineStartStopButtonSystem = dt.Rows[0]["EngineStartStopButtonSystem"].ToString() == "0" ? false : true;


                        CarCategory.Sunroof = dt.Rows[0]["Sunroof"].ToString() == "0" ? false : true;


                        CarCategory.ElectricSunroof = dt.Rows[0]["ElectricSunroof"].ToString() == "0" ? false : true;


                        CarCategory.BackCamera = dt.Rows[0]["BackCamera"].ToString() == "0" ? false : true;


                        CarCategory.ComputerTrips = dt.Rows[0]["ComputerTrips"].ToString() == "0" ? false : true;


                        CarCategory.SteeringWheelType = dt.Rows[0]["SteeringWheelType"].ToString() == "0" ? false : true;


                        CarCategory.ControllableSteeringWheel = dt.Rows[0]["ControllableSteeringWheel"].ToString() == "0" ? false : true;


                        CarCategory.ControlTheSoundSystemOfTheSteeringWheel = dt.Rows[0]["ControlTheSoundSystemOfTheSteeringWheel"].ToString() == "0" ? false : true;


                        CarCategory.CruiseControl = dt.Rows[0]["CruiseControl"].ToString() == "0" ? false : true;


                        CarCategory.LeatherSteeringWheel = dt.Rows[0]["LeatherSteeringWheel"].ToString() == "0" ? false : true;


                        CarCategory.LeatherTransmission = dt.Rows[0]["LeatherTransmission"].ToString() == "0" ? false : true;


                        CarCategory.FrontDoorStorage = dt.Rows[0]["FrontDoorStorage"].ToString() == "0" ? false : true;


                        CarCategory.BackDoorStorageAreas = dt.Rows[0]["BackDoorStorageAreas"].ToString() == "0" ? false : true;


                        CarCategory.PossibilityToFoldBackSeats = dt.Rows[0]["PossibilityToFoldBackSeats"].ToString() == "0" ? false : true;


                        CarCategory.Lighter = dt.Rows[0]["Lighter"].ToString() == "0" ? false : true;


                        CarCategory.MobileAshtray = dt.Rows[0]["MobileAshtray"].ToString() == "0" ? false : true;


                        CarCategory.CentralDoorLock = dt.Rows[0]["CentralDoorLock"].ToString() == "0" ? false : true;


                        CarCategory.AlarmSoundWhenTheCarIsNotClosed = dt.Rows[0]["AlarmSoundWhenTheCarIsNotClosed"].ToString() == "0" ? false : true;


                        CarCategory.FrontCupHolder = dt.Rows[0]["FrontCupHolder"].ToString() == "0" ? false : true;


                        CarCategory.BackCupHolder = dt.Rows[0]["BackCupHolder"].ToString() == "0" ? false : true;


                        CarCategory.FrontArmrest = dt.Rows[0]["FrontArmrest"].ToString() == "0" ? false : true;


                        CarCategory.AirConditionedFrontArmrest = dt.Rows[0]["AirConditionedFrontArmrest"].ToString() == "0" ? false : true;


                        CarCategory.BackArmrest = dt.Rows[0]["BackArmrest"].ToString() == "0" ? false : true;


                        CarCategory.BackTrunkCover = dt.Rows[0]["BackTrunkCover"].ToString() == "0" ? false : true;


                        CarCategory.FrontStorageDrawer = dt.Rows[0]["FrontStorageDrawer"].ToString() == "0" ? false : true;


                        CarCategory.PowerOutlet = dt.Rows[0]["PowerOutlet"].ToString() == "0" ? false : true;


                        CarCategory.BackOutletPowerOutlet = dt.Rows[0]["BackOutletPowerOutlet"].ToString() == "0" ? false : true;


                        CarCategory.BackWipers = dt.Rows[0]["BackWipers"].ToString() == "0" ? false : true;


                        CarCategory.RainSensitiveWindshieldWipers = dt.Rows[0]["RainSensitiveWindshieldWipers"].ToString() == "0" ? false : true;

                        CarCategory.BackLight = dt.Rows[0]["BackLight"].ToString() == "0" ? false : true;

                        CarCategory.SensitiveHeadlampsForLighting = dt.Rows[0]["SensitiveHeadlampsForLighting"].ToString() == "0" ? false : true;

                        CarCategory.BackTrunkSpace = dt.Rows[0]["BackTrunkSpace"].ToString() == "0" ? false : true;

                        CarCategory.BackSeatBelt = dt.Rows[0]["BackSeatBelt"].ToString() == "0" ? false : true;
                        #endregion

                        db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {

                        oledbConn.Close();
                    }

                }


            }


            //Send OK Response to Client.
            return Request.CreateResponse();
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
                            color.CarImages.Select(s2 => { s2.ImageURL = colorData.file == null ? s2.ImageURL : colorData.file; s2.IsDeleted = false; return s2; }).ToList();
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

            if (Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).Count()>0)
            {
                Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).First().Code = currency.Code;
                Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).First().Name = currency.Name;
            }
            else if(User.Identity.GetUserId()!=null)
            {
                currency.UserIdentity = User.Identity.GetUserId();
                Utilities._currencyDTO.Add(currency);
            }
      

            return Ok(currency);
        }

        [Route("api/CartDetails/ReadCurancy")]
        [HttpGet]
        public IHttpActionResult ReadCurrency()
        {
           return Ok(Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).FirstOrDefault());
        }

    }
}
