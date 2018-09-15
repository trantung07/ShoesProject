using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoesProjectModels.Model;

namespace ShoesProject.Areas.Admin.Controllers
{
    public class SizesController : Controller
    {
        private Shoes db = new Shoes();
        // GET: Admin/Sizes
        public ActionResult Index()
        {
            return View(db.Sizes.ToList());
        }

        // GET: Admin/Sizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        // GET: Admin/Sizes/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Size s)
        {
            s.SizeStatus = s.SizeStatus ?? false;

            db.Sizes.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var size = db.Sizes.Find(id);

            return View(size);
        }

        [HttpPost]
        public ActionResult Edit(Size s)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s);
        }
    }
}
