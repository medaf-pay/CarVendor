using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarVendor.Web.Controllers
{
    public class MainInfoController : Controller
    {
        // GET: MainInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Privacypolicy()
        {
            return View();
        }
        public ActionResult Termsofservice()
        {
            return View();
        }
        public ActionResult Refundpolicy()
        {
            return View();
        }
    }
}