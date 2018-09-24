using ShoesProject.Areas.Admin.Util;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Text;
using ShoesProject.Common;

namespace ShoesProject.Controllers
{
    public class MyAccountController : Controller
    {
        Shoes db = new Shoes();
        // GET: MyAccount
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult ForgotPassword()
        {
                return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            var user = db.Users.SingleOrDefault(x => x.Email.Equals(Email));
            if(user != null)
            {
                user.Password = Utility.getHashedMD5("1234");
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Template/content.html"));
                content = content.Replace("{{CustomerName}}", user.UserName);
                content = content.Replace("{{Email}}", user.Email);

                new MailHelper().SendMail(user.Email,"Lấy Lại Mật Khẩu", content);
                return RedirectToAction("Index","Login");
            }else
            {
                return RedirectToAction("ForgotPassword");
            }
        }
        public ActionResult MyPersonalInformation()
        {
            if (Session["UserId"] != null)
            {

                var user = Session["UserId"];
                var u = db.Users.Find(user);
                return View(u);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult UpdateMyPersonalInformation(User user)
        {
            if (ModelState.IsValid)
            {
                if(user.Email == null)
                {
                    user.Email = Session["Email"].ToString();
                }
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Session["fullname"] = user.UserName;
                TempData["msg"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "Cập nhật không thành công";
                return RedirectToAction("MyPersonalInformation");
            }
        }
        public ActionResult ChangePassword()
        {
            if(Session["UserId"] != null)
            {
                return View();
            }
            return RedirectToAction("Index","Login");
        }
        [HttpPost]
        public ActionResult ChangePassword(string password,string passwordNew)
        {
            int id = (int)Session["UserId"];
            var user = db.Users.Find(id);
            if(Utility.getHashedMD5(password) == user.Password)
            {
                user.Password = Utility.getHashedMD5(passwordNew);
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Đổi mật khẩu thành công";
                return RedirectToAction("Index");
            }else
            {
                TempData["msg"] = "Mật Khẩu cũ không đúng";
                return View();
            }
        }
    }
}