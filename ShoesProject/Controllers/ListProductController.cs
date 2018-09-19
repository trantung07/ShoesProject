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
        Shoes db = new Shoes();
        // GET: ShopGirl
        public ActionResult Index()
        {
            var lstCate = from c in db.Categories
                          select c;
            ViewBag.lstCategory = lstCate;
            return View();
        }
    }
}