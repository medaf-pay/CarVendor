using CarVendor.data;
using CarVendor.data.Entities;
using CarVendor.mvc.Common;
using CarVendor.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarVendor.mvc.Controllers
{
    public class AccountController : Controller
    {
        private DataBaseContext Context = new DataBaseContext();
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Context.Users.FirstOrDefault(s => s.Email == model.Email && s.Password == model.Password);
            if (user == null)
                return HttpNotFound();


            JWTManager.GenerateToken(user);

            //switch (_role.Role)
            //{
            //    case "user":
            //        return RedirectToAction("index", "chatbot");
            //    case "admin":
            //        return RedirectToAction("index", "scenarios");

            //    case "telleSales":
            //        return RedirectToAction("index", "Matrix");

            //    case "telleSalesadmin":
            //        return RedirectToAction("index", "TelleSalesAdmin");

            //    case "leader":
            //        return RedirectToAction("index", "home");
            //}
            return RedirectToAction("index", "Home");

        }


    }
}