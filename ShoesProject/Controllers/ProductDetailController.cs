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
        // GET: ProductDetail
        public ActionResult Index(int id)
        {
            var lstCate = from c in db.Categories
                          where c.CategoryParentId == null
                          select c.CategoryName;
            ViewBag.lstCategory = lstCate;
            Product p = db.Products.Find(id);
            //ViewBag.Size = from s in db.Sizes
            //               where s.Products.Contains(p)
            //               select s.SizeValue;
            return View(p);
        }
        
    }
}