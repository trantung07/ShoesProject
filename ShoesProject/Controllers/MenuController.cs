using ShoesProject.Models;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class MenuController : Controller
    {
        Shoes db = new Shoes();

        // GET: Menu
        public ActionResult Index()
        {
            var lstCate = from c in db.Categories
                          select c;
            return View(lstCate);
        }
        public ActionResult MenuCart()
        {
            List<CartItem> Cart = (List<CartItem>)Session["Cart"];
            return View(Cart);
        }
        public ActionResult MenuMobile()
        {
            var lstCate = from c in db.Categories
                          select c;
            return View(lstCate);
        }
    }
}