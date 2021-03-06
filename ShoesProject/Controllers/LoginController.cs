﻿using ShoesProject.Areas.Admin.Util;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class LoginController : Controller
    {
        Shoes db = new Shoes();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["fullName"] != null)
            {
                if(Session["Cart"] != null)
                {
                    return RedirectToAction("CartAddress", "MyCart");
                }else
                {
                    return RedirectToAction("Index", "MyAccount");
                }
                
            }else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Register(User u)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult LoginUser(string Email, string Password)
        {

            var pass = Utility.getHashedMD5(Password);
            var user = db.Users.SingleOrDefault(x => x.Email.Equals(Email) && x.Password.StartsWith(pass) && x.Password.EndsWith(pass));
            if (user != null && user.UserStatus == true)
            {
                Session["fullname"] = user.UserName;
                Session["UserId"] = user.UserId;
                Session["Email"] = user.Email;
                if (Session["Cart"] != null)
                {
                    return RedirectToAction("CartAddress", "MyCart");
                }
                else
                {
                    return RedirectToAction("Index", "MyAccount");
                }
            }
            else
            {
                TempData["msg"] = "Đăng nhập thất bại";
                return RedirectToAction("Index");
            }
        }
        public ActionResult LogOut()
        {
            Session.Remove("fullname");
            return RedirectToAction("Index");
        }


    }
}