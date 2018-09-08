using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class MyCartController : Controller
    {
        // GET: MyCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CartSignin()
        {
            return View();
        }

        public ActionResult CartAddress()
        {
            return View();
        }
        public ActionResult CartShipping()
        {
            return View();
        }
        public ActionResult CartPayment()
        {
            return View();
        }
    }
}