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
        Shoes db = new Shoes();
        public ActionResult Index()
        {
            var lst = db.Products.ToList();
            ViewBag.lstProduct = lst;
            var lstCate = from c in db.Categories          
                          select c;
            ViewBag.lstCategory = lstCate;
            return View();
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