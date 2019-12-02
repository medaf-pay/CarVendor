using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarVendor.Web.Models;
using CarVendor.data.Entities;

namespace CarVendor.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConversionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Conversions
        public ActionResult Index()
        {
            var conversions = db.Conversions.Include(c => c.FromCurrency).Include(c => c.ToCurrency);
            return View(conversions.ToList());
        }

        // GET: Conversions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversion conversion = db.Conversions.Find(id);
            if (conversion == null)
            {
                return HttpNotFound();
            }
            return View(conversion);
        }

        // GET: Conversions/Create
        public ActionResult Create()
        {
            ViewBag.FromCurrencyId = new SelectList(db.Currencies, "Id", "Name");
            ViewBag.ToCurrencyId = new SelectList(db.Currencies, "Id", "Name");
            return View();
        }

        // POST: Conversions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FromCurrencyId,ToCurrencyId,Value,Plus,IsDeleted")] Conversion conversion)
        {
            if (ModelState.IsValid)
            {
                conversion.CreationDate = DateTime.Now;
                db.Conversions.Add(conversion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FromCurrencyId = new SelectList(db.Currencies, "Id", "Name", conversion.FromCurrencyId);
            ViewBag.ToCurrencyId = new SelectList(db.Currencies, "Id", "Name", conversion.ToCurrencyId);
            return View(conversion);
        }

        // GET: Conversions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversion conversion = db.Conversions.Find(id);
            if (conversion == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromCurrencyId = new SelectList(db.Currencies, "Id", "Name", conversion.FromCurrencyId);
            ViewBag.ToCurrencyId = new SelectList(db.Currencies, "Id", "Name", conversion.ToCurrencyId);
            return View(conversion);
        }

        // POST: Conversions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromCurrencyId,ToCurrencyId,CreationDate,Value,Plus,IsDeleted")] Conversion conversion)
        {
            if (ModelState.IsValid)
            {
                var oldConversion = db.Conversions.Find(conversion.Id);
                 oldConversion.FromCurrencyId= conversion.FromCurrencyId;
                 oldConversion.ToCurrencyId= conversion.ToCurrencyId;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromCurrencyId = new SelectList(db.Currencies, "Id", "Name", conversion.FromCurrencyId);
            ViewBag.ToCurrencyId = new SelectList(db.Currencies, "Id", "Name", conversion.ToCurrencyId);
            return View(conversion);
        }

        // GET: Conversions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversion conversion = db.Conversions.Find(id);
            if (conversion == null)
            {
                return HttpNotFound();
            }
            return View(conversion);
        }

        // POST: Conversions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Conversion conversion = db.Conversions.Find(id);
            db.Conversions.Remove(conversion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
