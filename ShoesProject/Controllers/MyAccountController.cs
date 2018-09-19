using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class MyAccountController : Controller
    {
        Shoes db = new Shoes();
        // GET: MyAccount
        public ActionResult Index()
        {
            var lstCate = from c in db.Categories
                          where c.CategoryParentId == null
                          select c.CategoryName;
            ViewBag.lstCategory = lstCate;
            return View();
        }

        public ActionResult MyPersonalInformation()
        {
            return View();
        }

    }
}