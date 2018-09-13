using Hung.Model;
using ShoesProject.Areas.Admin.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class LoginController : Controller
    {
        HungModel db = new HungModel();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User u)
        {

            var user = db.Users.SingleOrDefault(x => x.Email.Equals(u.Email));
            if (user == null)
            {
                u.Password = Utility.getHashedMD5(u.Password);
                db.Users.Add(u);
                db.SaveChanges();
                TempData["smg"] = "Đăng ký Thành Công";
                return RedirectToAction("Index");
            }
            else
            {

                TempData["smg"] = "Email đã đc đăng ký ";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult LoginUser(string Email, string Password)
        {

            var pass = Utility.getHashedMD5(Password);
            var user = db.Users.SingleOrDefault(x => x.Email.Equals(Email) && x.Password.StartsWith(pass) && x.Password.EndsWith(pass));
            if (user != null)
            {
                Session["user"] = user.UserName;
                return RedirectToAction("Index", "MyAccount");
            }
            else
            {
                TempData["msg"] = "Đăng nhập thất bại";
                return RedirectToAction("Index");
            }
        }

    }
}