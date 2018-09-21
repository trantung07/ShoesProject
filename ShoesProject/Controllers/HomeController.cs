using ShoesProject.Models;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class HomeController : Controller
    {

        private Shoes db = new Shoes();
        public ActionResult Index()
        {
            List<CartItem> lstCart = (List<CartItem>)Session["Cart"];
            var lst = db.Products.AsEnumerable();
            ViewBag.lstProduct = lst;
            var lstCate = from c in db.Categories
                          select c;
            ViewBag.lstCategory = lstCate;
            int LIMIT = 100;
            var lstNewProducts =
                   //from co in db.Colors
                   //from s in db.Sizes
                   from p in db.Products
                   join c in db.Categories on p.CategoryId equals c.CategoryId
                   join b in db.Brands on p.BrandId equals b.BrandId
                   orderby p.ProductId descending
                   select new Products
                   {
                       id = p.ProductId,
                       name = p.ProductName,
                       cateId = p.CategoryId,
                       cateName = c.CategoryName,
                       instock = p.Instock,
                       brandId = p.BrandId,
                       brandName = b.BrandName,
                       price = p.ProductPrice,
                       discount = p.ProductDiscount,
                       imagesFeature = p.ProductFeatureImage
                   };
            ViewBag.lstNewProducts = lstNewProducts.Take(LIMIT);
            var lstSaleProduct =
                   //from co in db.Colors
                   //from s in db.Sizes
                   from p in db.Products
                   join c in db.Categories on p.CategoryId equals c.CategoryId
                   join b in db.Brands on p.BrandId equals b.BrandId
                   orderby p.ProductId descending
                   where p.ProductDiscount > 0
                   && p.ProductDiscount != null
                   select new Products
                   {
                       id = p.ProductId,
                       name = p.ProductName,
                       cateId = p.CategoryId,
                       cateName = c.CategoryName,
                       instock = p.Instock,
                       brandId = p.BrandId,
                       brandName = b.BrandName,
                       price = p.ProductPrice,
                       discount = p.ProductDiscount,
                       imagesFeature = p.ProductFeatureImage
                   };
            ViewBag.lstSaleProduct = lstSaleProduct.Take(LIMIT);
            return View(ViewBag);
        }
        public ActionResult About()
        {
            var lstCate = from c in db.Categories
                          select c;
            ViewBag.lstCategory = lstCate;
            return View();
        }

        public ActionResult Contact()
        {
            var lstCate = from c in db.Categories
                          select c;
            ViewBag.lstCategory = lstCate;
            return View();
        }
    }
}