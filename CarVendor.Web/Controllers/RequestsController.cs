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
            if(role == "Admin")
            {
                if (EndDate < StartDate)
                {
                    ViewBag.ErrorMsg = "End Date Must be After Start Date";
                    List<Order> _orders = db.Orders.ToList();
                    return View(_orders.ToList());
                }
                else if ((EndDate == null || StartDate == null) && EndDate != StartDate)
                {
                    ViewBag.ErrorMsg = "Make sure to enter both Start and End Dates";
                    List<Order> _orders = db.Orders.ToList();
                    return View(_orders.ToList());
                }
                else if(EndDate==null&&StartDate==null)
                {
                    ViewBag.ErrorMsg = "";
                    List<Order> _orders = db.Orders.ToList();
                    return View(_orders.ToList());
                }
                else
                {
                    List<Order> _orders = db.Orders.Where(o=>o.OrderDate>StartDate&&o.OrderDate<EndDate).ToList();
                    return View(_orders.ToList());
                }

                
            }
            var aspUser = User.Identity;
            var userId =  UserManager.FindById(aspUser.GetUserId()).user.Id;
            var orders = db.Orders.Where(s=>s.User.Id==userId).ToList();
            return View(orders.ToList());

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
