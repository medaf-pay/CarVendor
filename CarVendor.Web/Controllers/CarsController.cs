﻿using CarVendor.data.Entities;
using CarVendor.Web.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarVendor.Web.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.Brand).Include(c => c.Type);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(long? id, string Category)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null || car.IsDeleted == true)
            {
                return HttpNotFound();
            }
            var CarCategory = car.Carcategories.Where(cat => cat.CategoryId == int.Parse(Category.Trim('c'))).FirstOrDefault();
            ViewBag.carId = id;
            ViewBag.categoryId = int.Parse(Category.Trim('c'));
            return View(CarCategory);
        }

        public ActionResult EditCategory(long? carId, int categoryId)
        {
          
            ViewBag.carId = carId;
            ViewBag.categoryId = categoryId;
            return View();
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(long CarCode)
        {

            Car car = db.Cars.Find(CarCode);
            if (car == null || car.IsDeleted == true)
            {
                return HttpNotFound();
            }
            //ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", car.BrandId);
            //ViewBag.TypeId = new SelectList(db.CarFamilies, "Id", "Name", car.TypeId);
            return View();
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Model,Condition,TypeId,BrandId,IsDeleted")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", car.BrandId);
            ViewBag.TypeId = new SelectList(db.CarFamilies, "Id", "Name", car.TypeId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null || car.IsDeleted==true)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Car car = db.Cars.Find(id);
            car.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
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
