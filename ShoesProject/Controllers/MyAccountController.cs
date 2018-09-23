using ShoesProject.Areas.Admin.Util;
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
            return View();
        }

        public ActionResult MyPersonalInformation()
        {
            var user = Session["UserId"];
            var u = db.Users.Find(user); 
            return View(u);
        }
        [HttpPost]
        public ActionResult UpdateMyPersonalInformation(string fullname, string email, string password,string passwordnew)
        {
            var user = Session["UserId"];
            var u = db.Users.Find(user);
            var us = db.Users.SingleOrDefault(x =>x.Password.StartsWith(password) && x.Password.EndsWith(password));
            if (us != null)
            {
                User u1 = new User();
                u1.UserName = fullname;
                u1.Password = Utility.getHashedMD5(passwordnew);
                u1.Email = email;
                u1.UserStatus = true;
                db.Entry(u1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyPersonalInformation");
            }
            else
            {
                TempData["smg"] = "Cập nhật không thành công";
                return RedirectToAction("MyPersonalInformation");
            }
        }
    }
}