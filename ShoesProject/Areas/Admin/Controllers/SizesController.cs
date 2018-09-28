using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoesProject.Areas.Admin.Models;
using ShoesProjectModels.Model;

namespace ShoesProject.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class SizesController : Controller
    {
        private Shoes db = new Shoes();
        // GET: Admin/Sizes
        public ActionResult Index()
        {
            var enabledSizes = db.Sizes.Where(s => s.SizeStatus ?? false);
            return View(enabledSizes.ToList());
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
            if (ModelState.IsValid)
            {
                s.SizeStatus = s.SizeStatus ?? false;
                db.Sizes.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s);

        }

        public JsonResult getProductSizes(int ProductId)
        {
            var productSizes = (from p in db.Products
                                 from c in p.Sizes
                                 where p.ProductId == ProductId && c.SizeStatus == true
                                 select new SizeViewModel { SizeId = c.SizeId, SizeValue = c.SizeValue, IsPresent = true }).ToList();
            var allSizes = from c in db.Sizes select new SizeViewModel { SizeId = c.SizeId, SizeValue = c.SizeValue, IsPresent = false };
            var productSizesId = productSizes.Select(x => x.SizeId);
            foreach (var item in allSizes)
            {
                if (!productSizesId.Contains(item.SizeId)) productSizes.Add(item);
            }
            return Json(productSizes, JsonRequestBehavior.AllowGet);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Size size = db.Sizes.Find(id);
            //db.Sizes.Remove(size);
            size.SizeStatus = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
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
    }
}
