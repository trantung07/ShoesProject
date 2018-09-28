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
                   from p in db.Products
                   from co in p.Colors
                   from s in p.Sizes
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
                   from p in db.Products
                   from co in p.Colors
                   from s in p.Sizes
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
                   from p in db.Products
                   from co in p.Colors
                   from s in p.Sizes
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
            ViewBag.lstFeatureProduct = lstFeatureProduct.OrderBy(r => Guid.NewGuid()).Take(15);
            ViewBag.lstFeatureProductFirst = lstFeatureProduct.OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefault();
            var lstBannerProduct =
                   from p in db.Products
                   select p;
            ViewBag.lstBannerProduct = lstBannerProduct.OrderBy(r => Guid.NewGuid()).Take(2);

            var lstBrand =
                from b in db.Brands
                select b;
            ViewBag.lstBrand = lstBrand;
            ViewBag.lstBrandFirst = lstBrand.Take(1).FirstOrDefault();
            var lstBestSale =
                from p in db.Products
                from co in p.Colors
                from s in p.Sizes
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
            return View();
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