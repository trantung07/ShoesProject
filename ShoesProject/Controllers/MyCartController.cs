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
            List<CartItem> lst = (List<CartItem>)Session["Cart"];
            var lstCart = new List<CartItem>();
            if (lst != null)
            {
                lstCart = lst;
            }
            return View(lst);
        }
        public ActionResult AddCart(int id)
        {
            var product = db.Products.Find(id);
            var brand = db.Brands.Find(product.BrandId);
            var category = db.Categories.Find(product.CategoryId);
            //var color = db.Colors.Find(db.Products.Find(id));
            //var size = db.Sizes.Find(db.Products.Find(id));
            if (Session["Cart"] != null)
            {
                List<CartItem> lst = new List<CartItem>();
                lst = (List<CartItem>)Session["Cart"];
                bool check = false;
                foreach (var item in lst)
                {
                    if (item.Product.ProductId == product.ProductId)
                    {
                        item.Quantity += 1;
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    lst.Add(new CartItem { Product = product, Quantity = 1, Brand = brand, Category = category });
                }
                Session["Cart"] = lst;
            }
            else
            {

                Session["Cart"] = new List<CartItem>() { new CartItem { Product = product, Quantity = 1, Brand = brand, Category = category } };
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string UpdateCart(int id, int quantity)
        {
            var lst = (List<CartItem>)Session["Cart"];
            foreach (var item in lst)
            {
                if (item.Product.ProductId == id)
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
                if (item.Product.ProductDiscount == null)
                {
                    total += (item.Product.ProductPrice * item.Quantity);
                }
                else
                {
                    var price = ((item.Product.ProductPrice * (int)item.Product.ProductDiscount) / 100);
                    total += (price * item.Quantity);
                }
            }
            var totalSum = total;
            return totalSum;
        }
        public ActionResult CartSignin()
        {
            if (Session["fullname"] != null)
            {
                return RedirectToAction("CartAddress");
            }
            else
            {

                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult CartAddress()
        {
            List<CartItem> lst = (List<CartItem>)Session["Cart"];
            var lstCart = new List<CartItem>();
            if (lst != null)
            {
                lstCart = lst;
            }
            return View(lst);
        }
        //[HttpPost]
        //public ActionResult CartAddress(string Address, string PhoneNumber, string code)
        //{
        //    try
        //    {
        //        User u = (User)Session["fullName"];
        //        var voucher = db.Vouchers.SingleOrDefault(x => x.Code.Equals(code));
        //        Order oder = new Order();
        //        if (voucher != null)
        //        {
        //            oder = new Order
        //            {
        //                UserId = u.UserId,
        //                VoucherId = voucher.VoucherId,
        //                Address = Address,
        //                PhoneNumber = PhoneNumber,
        //                ProgressStatus = 1,
        //                OrderStatus = true,
        //            };
        //        }
        //        else
        //        {
        //            oder = new Order
        //            {
        //                UserId = u.UserId,
        //                VoucherId = 0,
        //                Address = Address,
        //                PhoneNumber = PhoneNumber,
        //                ProgressStatus = 1,
        //                OrderStatus = true,
        //            };
        //        }
        //        db.Orders.Add(oder);
        //        db.SaveChanges();
        //        List<CartItem> lst = (List<CartItem>)Session["Cart"];
        //        foreach (var item in lst)
        //        {
        //            OrdersDetail orderdetail = new OrdersDetail();
        //            orderdetail.OrderId = oder.OrderId;
                    

        //        }
        //        return RedirectToAction("CartPayment");
        //    }
        //    catch (Exception)
        //    {
        //        return View();
        //        throw;
        //    }
        //}
        //public ActionResult CartShipping()
        //{

        //    return View();
        //}
        public ActionResult CartPayment()
        {
            //if (Session["fullName"] != null)
            //{

            //    List<CartItem> lst = (List<CartItem>)Session["Cart"];
            //    var lstCart = new List<CartItem>();
            //    if (lst != null)
            //    {
            //        lstCart = lst;
            //    }
            //    return View(lst);
            //}else
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            return View(); 
        }
    }
}