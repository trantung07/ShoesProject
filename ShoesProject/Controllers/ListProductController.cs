using ShoesProject.Models;
using ShoesProjectModels.Model;
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
        [HttpGet]
        public ActionResult Index(int id)
        {
            var lstProductInCategory =
                //from co in db.Colors
                //from s in db.Sizes
                from p in db.Products
                join c in db.Categories on p.CategoryId equals c.CategoryId
                join b in db.Brands on p.BrandId equals b.BrandId
                where c.CategoryId == id
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
            return View(lstProductInCategory);
        }
    }
}