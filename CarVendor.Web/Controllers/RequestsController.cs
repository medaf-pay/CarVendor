using CarVendor.data;
using CarVendor.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using CarVendor.data.Entities;
using System.Collections.Generic;
using System;

namespace CarVendor.mvc.Controllers
{
    public class RequestsController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

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


        // GET: Requests
        public ActionResult Index(DateTime? StartDate, DateTime? EndDate)
        {
           
            var user = User.Identity;
            string role = UserManager.GetRoles(user.GetUserId())[0];
            List<Order> _orders = db.Orders.ToList();

          


            if (role != "Admin")
            {
                var aspUser = User.Identity;
                var userId = UserManager.FindById(aspUser.GetUserId()).user.Id;
                _orders = _orders?.Where(s => s.User.Id == userId).ToList();
            }

            if (EndDate < StartDate)
            {
                ViewBag.ErrorMsg = "End Date Must be After Start Date";

                return View(_orders.ToList());
            }
            //else if ((EndDate == null || StartDate == null) && EndDate != StartDate)
            //{
            //    ViewBag.ErrorMsg = "Make sure to enter both Start and End Dates";
               
            //    return View(_orders.ToList());
            //}
            else if (EndDate == null && StartDate == null)
            {
                ViewBag.ErrorMsg = "";
               
                return View(_orders.ToList());
            }
            else if ( StartDate == null)
            {
                ViewBag.ErrorMsg = "";
                _orders = _orders.Where(o =>  o.OrderDate < EndDate).ToList();


                return View(_orders.ToList());
            }
            else if (EndDate == null)
            {
                ViewBag.ErrorMsg = "";
                _orders = _orders.Where(o => o.OrderDate > StartDate).ToList();

                return View(_orders.ToList());
            }
            else if (StartDate== EndDate)
            {
                ViewBag.ErrorMsg = "";
                _orders = _orders.Where(o => o.OrderDate.Date == StartDate).ToList();

                return View(_orders.ToList());
            }
            else
            {
                 _orders = _orders.Where(o => o.OrderDate > StartDate && o.OrderDate < EndDate).ToList();
                return View(_orders.ToList());
            }

    
      

        }



        // GET: Requests/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Confirmation()
        {
            return View();
        }

    }
}
