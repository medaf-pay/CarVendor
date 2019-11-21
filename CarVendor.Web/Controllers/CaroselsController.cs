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
using System.IO;

namespace CarVendor.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CaroselsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carosels
        public ActionResult Index()
        {
            return View(db.Carosels.ToList());
        }

        // GET: Carosels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carosel carosel = db.Carosels.Find(id);
            if (carosel == null)
            {
                return HttpNotFound();
            }
            return View(carosel);
        }

        // GET: Carosels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carosels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Model,Description,Price,Mileage,VolumeCapacity")] Carosel carosel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                string fileName = Path.GetFileName(path);
                file.SaveAs(path);
                carosel.ImagePath = fileName;
            }
            if (ModelState.IsValid)
            {
                db.Carosels.Add(carosel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carosel);
        }

        // GET: Carosels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carosel carosel = db.Carosels.Find(id);
            if (carosel == null)
            {
                return HttpNotFound();
            }
            return View(carosel);
        }

        // POST: Carosels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Model,Description,Price,Mileage,VolumeCapacity,ImagePath")] Carosel carosel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                string fileName = Path.GetFileName(path);
                file.SaveAs(path);
                carosel.ImagePath = fileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(carosel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carosel);
        }

        // GET: Carosels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carosel carosel = db.Carosels.Find(id);
            if (carosel == null)
            {
                return HttpNotFound();
            }
            return View(carosel);
        }

        // POST: Carosels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carosel carosel = db.Carosels.Find(id);
            db.Carosels.Remove(carosel);
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
