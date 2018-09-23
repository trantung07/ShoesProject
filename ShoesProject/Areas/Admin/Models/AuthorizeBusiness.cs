using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Areas.Admin.Models
{
    public class AuthorizeBusiness : ActionFilterAttribute
    {
        Shoes db = new Shoes();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(HttpContext.Current.Session["adminid"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Home/Login");
                return;
            }
            int userId = int.Parse(HttpContext.Current.Session["adminid"].ToString());
            string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller-" + filterContext.ActionDescriptor.ActionName;
            var admin = db.Admins.Where(a => a.AdminId == userId && a.IsSuper).FirstOrDefault();
            if (admin != null) return;
            var listPermissons = from a in db.Admins
                                 from p in a.Permissons
                                 where a.AdminId == userId
                                 select p.PermissonName;
            if (!listPermissons.Contains(actionName))
            {
                filterContext.Result = new RedirectResult("/Admin/Home/401");
                return;
            }

        }
    }
}