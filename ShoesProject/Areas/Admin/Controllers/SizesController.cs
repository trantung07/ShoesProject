using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TungTT25.Model;

namespace ShoesProject.Areas.Admin.Controllers
{
    public class SizesController : Controller
    {
        ShoesModel db = new ShoesModel();
        // GET: Admin/Sizes
        public ActionResult Index()
        {
            return View(db.Sizes.ToList());
        }

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