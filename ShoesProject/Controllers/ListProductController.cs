
using ShoesProject.Models;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //var products = db.Products.ToList();
            var lst = 
                from c in db.Colors
                from s in db.Sizes
                from p in db.Products 
                join ca in db.Categories on p.CategoryId equals ca.CategoryId
                join br in db.Brands on p.BrandId equals br.BrandId
                      select new Products
                      {
                          productId = p.ProductId,
                          productName = p.ProductName,
                          categoryId = p.CategoryId,
                          categoryName = ca.CategoryName,
                          instock = p.Instock,
                          brandId = p.BrandId,
                          brandName = br.BrandName,
                          productPrice = p.ProductPrice,
                          productDescription = p.ProductDescription,
                          productDiscount = p.ProductDiscount
                      };
            return View(lst);
        }
    }
}