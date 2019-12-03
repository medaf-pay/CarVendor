using CarVendor.data;
using CarVendor.data.Entities;
using CarVendor.mvc.Common;
using CarVendor.mvc.Models;
using CarVendor.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarVendor.mvc.Controllers
{
    public class HomeController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public HomeController() { }
        [Route("home/Index")]
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.role = UserManager.GetRoles(user.GetUserId())[0];
            }
            decimal ExchangeRate = 1;
            if (Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).Select(s => s.Code).FirstOrDefault()!=0 && Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).Select(s=>s.Code).FirstOrDefault() != 1)
            {
                var Code = Utilities._currencyDTO.Where(c => c.UserIdentity == User.Identity.GetUserId()).First().Code;
                ExchangeRate = db.Conversions.Where(cc => cc.FromCurrencyId == Code).OrderByDescending(o => o.CreationDate).Select(s => s.Value).FirstOrDefault();
            }
            var carosels = db.Carosels.ToList();
            ViewBag.Slides = carosels.Select(s => { s.Price =(decimal) s.Price/ ExchangeRate; return s; }).ToList();
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
        [Authorize(Roles = "Admin")]
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