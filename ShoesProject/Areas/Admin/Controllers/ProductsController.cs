using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoesProjectModels.Model;
using FineUploader;
using System.IO;

namespace ShoesProject.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private Shoes db = new Shoes();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public int Create([Bind(Include = "ProductId,ProductName,CategoryId,Instock,BrandId,ProductPrice,ProductDescription,ProductDiscount,ProductFeatureImage,ProductStatus")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return product.ProductId;
            }
            return 0;
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,CategoryId,Instock,BrandId,ProductPrice,ProductDescription,ProductDiscount,ProductFeatureImage, ProductStatus")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public FineUploaderResult CreateProductImages(FineUpload upload, int ProductId)
        {
            try
            {
                ProductImage p = new ProductImage();
                p.ProductId = ProductId;
                p.Image = upload.Filename;
                db.ProductImages.Add(p);
                db.SaveChanges();
                var dir = Server.MapPath("../Content/ProductImages/" + p.ProductId + "/");
                var filePath = Path.Combine(dir, upload.Filename);
                upload.SaveAs(filePath);
                return new FineUploaderResult(true, new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }
        }

        public JsonResult GetProductImages(int ProductId)
        {
            var imagesList = (from p in db.ProductImages
                             where p.ProductId == ProductId
                             select new {Image = p.Image, ProductImageId = p.ProductImageId}
                             ).ToList();
            return Json(imagesList.OrderBy(x => x.ProductImageId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int DeleteProductImages(int ImgId)
        {
            try
            {
                var img = db.ProductImages.SingleOrDefault(x => x.ProductImageId == ImgId);
                db.ProductImages.Remove(img);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public JsonResult GetProductsByName(string name, int page)
        {
            var products = db.Products.Where(x => x.ProductName.ToLower().Contains(name.ToLower())).Select(x => new { ProductName = x.ProductName, ProductId = x.ProductId, ProductFeatureImage = x.ProductFeatureImage,
                BrandName = x.Brand.BrandName, CategoryName = x.Category.CategoryName, InStock = x.Instock, 
                ProductPrice = x.ProductPrice, ProductDescription = x.ProductDescription, ProductDiscount = x.ProductDiscount
            });
            var pagedProducts = products.OrderByDescending(x => x.ProductName).Skip(10*(page-1)).Take(10);
            return Json(new { products= pagedProducts, page = 1, count= products.Count()}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllProducts(int page)
        {
            var products = db.Products.Select(x => new { ProductName = x.ProductName, ProductId = x.ProductId, ProductFeatureImage = x.ProductFeatureImage });
            var pagedProducts = products.OrderByDescending(x => x.ProductName).Skip(10 * (page - 1)).Take(10);
            return Json(new { products = pagedProducts, page = page, count = products.Count() }, JsonRequestBehavior.AllowGet);
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
