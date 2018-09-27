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
            int LIMIT = 10;
            var lstNewProducts =
                   //from co in db.Colors
                   //from s in db.Sizes
                   from p in db.Products
                   join c in db.Categories on p.CategoryId equals c.CategoryId
                   join b in db.Brands on p.BrandId equals b.BrandId
                   where p.ProductStatus == true
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
                       imagesFeature = p.ProductFeatureImage,
                       status = p.ProductStatus
                   };
            ViewBag.lstNewProducts = lstNewProducts.Take(LIMIT);
            var lstSaleProduct =
                   //from co in db.Colors
                   //from s in db.Sizes
                   from p in db.Products
                   join c in db.Categories on p.CategoryId equals c.CategoryId
                   join b in db.Brands on p.BrandId equals b.BrandId
                   where p.ProductStatus == true
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
                       imagesFeature = p.ProductFeatureImage,
                       status = p.ProductStatus
                   };
            ViewBag.lstSaleProduct = lstSaleProduct.Take(LIMIT);
            var lstFeatureProduct =
                   //from co in db.Colors
                   //from s in db.Sizes
                   from p in db.Products
                   join c in db.Categories on p.CategoryId equals c.CategoryId
                   join b in db.Brands on p.BrandId equals b.BrandId
                   where p.ProductStatus == true
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
                       imagesFeature = p.ProductFeatureImage,
                       status = p.ProductStatus
                   };
            ViewBag.lstFeatureProduct = lstFeatureProduct.OrderBy(r => Guid.NewGuid()).Take(LIMIT);

            var lstCategory =
                from c in db.Categories
                where c.CategoryStatus == true
                && c.CategoryParentId == 0
                select c;

            var lstBestSale =
                from p in db.Products
                join c in db.Categories on p.CategoryId equals c.CategoryId
                join b in db.Brands on p.BrandId equals b.BrandId
                where p.ProductStatus == true
                orderby p.ProductDiscount ascending
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
                    imagesFeature = p.ProductFeatureImage,
                    status = p.ProductStatus
                };
            ViewBag.lstBestSale = lstBestSale.Take(LIMIT);

            return View(lstCategory);
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}