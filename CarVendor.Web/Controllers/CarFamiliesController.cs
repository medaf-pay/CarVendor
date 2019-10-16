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
    public class CarFamiliesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarFamilies
        public ActionResult Index()
        {
            return View(db.CarFamilies.ToList());
        }

        // GET: CarFamilies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarFamily carFamily = db.CarFamilies.Find(id);
            if (carFamily == null)
            {
                return HttpNotFound();
            }
            return View(carFamily);
        }

        // GET: CarFamilies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarFamilies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsDeleted")] CarFamily carFamily)
        {
            if (ModelState.IsValid)
            {
                db.CarFamilies.Add(carFamily);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carFamily);
        }

        // GET: CarFamilies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarFamily carFamily = db.CarFamilies.Find(id);
            if (carFamily == null)
            {
                return HttpNotFound();
            }
            return View(carFamily);
        }

        // POST: CarFamilies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsDeleted")] CarFamily carFamily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carFamily).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carFamily);
        }

        // GET: CarFamilies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarFamily carFamily = db.CarFamilies.Find(id);
            if (carFamily == null)
            {
                return HttpNotFound();
            }
            return View(carFamily);
        }

        // POST: CarFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarFamily carFamily = db.CarFamilies.Find(id);
            db.CarFamilies.Remove(carFamily);
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
