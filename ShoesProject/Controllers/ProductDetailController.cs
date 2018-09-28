using ShoesProject.Models;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class ProductDetailController : Controller
    {
        Shoes db = new Shoes();
        //GET: ProductDetail
        [HttpGet]
        public ActionResult Index(int id)
        {
            Product pro = db.Products.Find(id);

            ViewBag.Size = from p in db.Products
                           from s in p.Sizes
                           where p.ProductId == id
                           && s.SizeStatus == true
                           select s.SizeValue;

            var Color = from p in db.Products
                            from c in p.Colors
                            where p.ProductId == id
                            && c.ColorStatus == true
                            select c;
            ViewBag.Color = Color;
            ViewBag.ColorFirst = Color.Take(1).FirstOrDefault();

            var Images = from i in db.ProductImages
                             from p in db.Products
                             where p.ProductId == id
                             select i;
            ViewBag.Images = Images;
            ViewBag.ImagesFirst = Images.Take(1).FirstOrDefault();

            var relatedProduct = from p in db.Products
                                 where p.BrandId == pro.BrandId
                                 && p.ProductId != pro.ProductId
                                 select p;
            ViewBag.relatedProduct = relatedProduct.OrderBy(r => Guid.NewGuid()).Take(10);

            var otherProduct = from p in db.Products
                               where p.BrandId != pro.BrandId
                               && p.ProductId != pro.ProductId
                               select p;
            ViewBag.otherProduct = otherProduct.OrderBy(r => Guid.NewGuid()).Take(10);
            return View(pro);
        }
        
    }
}