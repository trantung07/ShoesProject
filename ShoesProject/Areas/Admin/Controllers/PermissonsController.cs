using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoesProjectModels.Model;

namespace ShoesProject.Areas.Admin.Controllers
{
    public class PermissonsController : Controller
    {
        private Shoes db = new Shoes();

        // GET: Admin/Permissons
        public ActionResult Index(string id)
        {
            var permissons = db.Permissons.Where(p => p.BusinessId == id);
            return View(permissons.ToList());
        }

        // GET: Admin/Permissons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisson permisson = db.Permissons.Find(id);
            if (permisson == null)
            {
                return HttpNotFound();
            }
            return View(permisson);
        }

        // GET: Admin/Permissons/Create
        public ActionResult Create()
        {
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "BusinessName");
            return View();
        }

        // POST: Admin/Permissons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermissonId,PermissonName,PermissonDescription,BusinessId")] Permisson permisson)
        {
            if (ModelState.IsValid)
            {
                db.Permissons.Add(permisson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "BusinessName", permisson.BusinessId);
            return View(permisson);
        }

        // GET: Admin/Permissons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisson permisson = db.Permissons.Find(id);
            if (permisson == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "BusinessName", permisson.BusinessId);
            return View(permisson);
        }

        // POST: Admin/Permissons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermissonId,PermissonName,PermissonDescription,BusinessId")] Permisson permisson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permisson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "BusinessName", permisson.BusinessId);
            return View(permisson);
        }

        // GET: Admin/Permissons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisson permisson = db.Permissons.Find(id);
            if (permisson == null)
            {
                return HttpNotFound();
            }
            return View(permisson);
        }

        // POST: Admin/Permissons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permisson permisson = db.Permissons.Find(id);
            db.Permissons.Remove(permisson);
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
