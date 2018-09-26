using ShoesProject.Areas.Admin.Models;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class SetPermissonController : Controller
    {
        Shoes db = new Shoes();
        // GET: Admin/SetPermisson
        public ActionResult Index(int id)
        {
            var listBusinesses = db.Businesses.AsEnumerable();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listBusinesses)
            {
                items.Add(new SelectListItem() { Text = item.BusinessName, Value = item.BusinessId });
            }
            ViewBag.items = items;

            var listGrantedPermissons = from a in db.Admins
                                        from p in a.Permissons
                                        where a.AdminId == id
                                        select new SelectListItem() { Text = p.PermissonDescription, Value = p.PermissonId.ToString() };
            ViewBag.listGrantedPermissons = listGrantedPermissons;
            Session["userOnSet"] = id;
            var userOnSet = db.Admins.Find(id);
            ViewBag.userOnSet = userOnSet;
            return View();
        }
    }
}