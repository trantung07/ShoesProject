using ShoesProject.Areas.Admin.Util;
using ShoesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        Shoes db = new Shoes();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string hashedPassword = Utility.getHashedMD5(username + password);
            var admin = db.Admins.SingleOrDefault(x => x.AdminUsername == username && x.AdminPassword.StartsWith(hashedPassword)
                && x.AdminPassword.EndsWith(hashedPassword) && !x.AdminIsDeleted && !x.AdminIsDisabled
            );
            if(admin != null)
            {
                Session["adminId"] = admin.AdminId;
                Session["adminUsername"] = admin.AdminUsername;
                return RedirectToAction("Index");
            }
            ViewBag.error = "Login Failed!";
            return View();
        }
    }
}