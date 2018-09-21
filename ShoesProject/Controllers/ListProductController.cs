<<<<<<< HEAD
﻿using ShoesProjectModels.Model;
=======
﻿using ShoesProject.Models;
using ShoesProjectModels.Model;
>>>>>>> 25601fb91a4b59e2bce1d061b0a8b423ba9cc410
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class ListProductController : Controller
    {
        private Shoes db = new Shoes();
        // GET: ShopGirl
        public ActionResult Index()
        {
            var lstCate = from c in db.Categories
                          select c;
            ViewBag.lstCategory = lstCate;
            var lst =
                //from co in db.Colors
                //from s in db.Sizes
                from p in db.Products
                join c in db.Categories on p.CategoryId equals c.CategoryId
                join b in db.Brands on p.BrandId equals b.BrandId
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
            return View(lst);
        }
    }
}