using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FineUploader;
using ShoesProject.Areas.Admin.Models;
using ShoesProjectModels.Model;

namespace ShoesProject.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class BrandsController : Controller
    {
        private Shoes db = new Shoes();

        // GET: Admin/Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: Admin/Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandId,BrandName,BrandImage")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: Admin/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandId,BrandName,BrandImage")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Admin/Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public FineUploaderResult CreateBrandWithImage(FineUpload upload, string BrandName)
        {          
            try
            {
                Brand b = new Brand();
                b.BrandName = BrandName;
                b.BrandImage = upload.Filename;
                db.Brands.Add(b);
                db.SaveChanges();
                var dir = Server.MapPath("../Content/brandImages/" + b.BrandId + "/");
                var filePath = Path.Combine(dir, upload.Filename);
                upload.SaveAs(filePath);
                return new FineUploaderResult(true, new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }

        }
        [HttpPost]
        public void UpdateBrand(int BrandId, string BrandName)
        {
            Brand b = db.Brands.FirstOrDefault(x => x.BrandId == BrandId);
            var currentFileName = b.BrandImage;
            b.BrandName = BrandName;
            db.SaveChanges();
        }
        [HttpPost]
        public FineUploaderResult UpdateBrandWithImage(FineUpload upload, int BrandId, string BrandName)
        {
            try
            {
                    Brand b = db.Brands.FirstOrDefault(x => x.BrandId == BrandId);
                    var currentFileName = b.BrandImage;
                    b.BrandName = BrandName;
                    b.BrandImage = upload.Filename;
                    db.SaveChanges();
                    var dir = Server.MapPath("../Content/brandImages/" + b.BrandId + "/");
                    var filePath = Path.Combine(dir, upload.Filename);
                    upload.SaveAs(filePath);
                    var currentFilePath = Path.Combine(dir, currentFileName);
                    System.IO.File.Delete(currentFilePath);
                    return new FineUploaderResult(true, new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }

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
