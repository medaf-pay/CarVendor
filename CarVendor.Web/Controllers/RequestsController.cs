using CarVendor.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarVendor.mvc.Controllers
{
    public class RequestsController : Controller
    {
        private readonly DataBaseContext _db = new DataBaseContext();
        // GET: Requests
        public ActionResult Index()
        {
            var dbOrders = _db.Orders.Include("OrderItems").Include("DeliveryDetails").ToList();

            return View(dbOrders);
        }

        // GET: Requests/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


    }
}
