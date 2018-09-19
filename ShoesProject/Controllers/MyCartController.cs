using ShoesProject.Models;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesProject.Controllers
{
    public class MyCartController : Controller
    {
       Shoes db = new Shoes();
        // GET: MyCart
        public ActionResult Index()
        {
            var lstCate = from c in db.Categories
                          select c;
            ViewBag.lstCategory = lstCate;
            List<CartItem> lst = (List<CartItem>)Session["Cart"];
            return View(lst);
        }
        public ActionResult AddCart(int id)
        {
            var product = db.Products.Find(id);
            if(Session["Cart"] != null)
            {
                List<CartItem> lst = new List<CartItem>();
                lst = (List<CartItem>)Session["Cart"];
                bool check = false;
                foreach (var item in lst)
                {
                    if(item.Product.ProductId == product.ProductId)
                    {
                        item.Quantity += 1;
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    lst.Add(new CartItem { Product = product, Quantity = 1 });
                }
                Session["Cart"] = lst;
            }else
            {
                Session["Cart"] = new List<CartItem>() { new CartItem { Product = product, Quantity = 1 } }; 
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string UpdateCart(int id,int quantity)
        {
            var lst = (List<CartItem>)Session["Cart"];
            foreach (var item in lst)
            {
                if(item.Product.ProductId == id)
                {
                    item.Quantity = quantity;
                    break;
                }
            }
            Session["Cart"] = lst;
            return "Ok";
        }
        [HttpPost]
        public string DeleteCart(int id)
        {
            var lst = (List<CartItem>)Session["Cart"];
            foreach (var item in lst)
            {
                if (item.Product.ProductId == id)
                {
                    lst.Remove(item);
                    break;
                }
            }
            Session["Cart"] = lst;
            return "Ok";
        }

        public int SumPriceCart()
        {
            var lst = (List<CartItem>)Session["Cart"];
            var total = 0;
            foreach (var item in lst)
            {
                total += (item.Product.ProductPrice * item.Quantity);
            }
            var totalSum = total / lst.Count();
            return totalSum;
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