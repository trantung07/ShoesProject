
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
            return View(lst);
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