using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoesProject.Areas.Admin.Models;
using ShoesProject.Areas.Admin.Util;
using ShoesProjectModels.Model;

namespace ShoesProject.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class BusinessesController : Controller
    {
        private Shoes db = new Shoes();

        // GET: Admin/Businesses
        public ActionResult Index()
        {
            return View(db.Businesses.ToList());
        }

        // GET: Admin/Businesses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: Admin/Businesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessId,BusinessName")] Business business)
        {
            if (ModelState.IsValid)
            {
                db.Businesses.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business);
        }

        // GET: Admin/Businesses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Admin/Businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessId,BusinessName")] Business business)
        {
            if (ModelState.IsValid)
            {
                db.Entry(business).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(business);
        }

        // GET: Admin/Businesses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Admin/Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Business business = db.Businesses.Find(id);
            db.Businesses.Remove(business);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SyncBusiness()
        {
            ReflectionController rc = new ReflectionController();
            List<Type> listControllerType = rc.GetControllers("ShoesProject.Areas.Admin.Controllers");
            List<string> listCurrentController = db.Businesses.Select(c => c.BusinessId).ToList();
            List<string> listCurrentPermisson = db.Permissons.Select(p => p.PermissonName).ToList();
            foreach (var c in listControllerType)
            {
                if (!listCurrentController.Contains(c.Name))
                {
                    Debug.WriteLine(c.Name);
                    Business b = new Business
                    {
                        BusinessId = c.Name,
                        BusinessName = "No Description"
                    };
                    db.Businesses.Add(b);
                }
                List<String> listPermisson = rc.GetActions(c);
                foreach (var p in listPermisson)
                {
                    if (!listCurrentPermisson.Contains(c.Name + "-" + p))
                    {
                        Permisson permisson = new Permisson();
                        permisson.PermissonName = c.Name + "-" + p;
                        permisson.BusinessId = c.Name;
                        db.Permissons.Add(permisson);
                    }
                }
            }
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
