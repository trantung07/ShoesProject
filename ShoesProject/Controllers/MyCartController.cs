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
        public ActionResult AddCart(int id, int colorId, int sizeId)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                var brand = db.Brands.Find(product.BrandId);
                var category = db.Categories.Find(product.CategoryId);
                var color = db.Colors.Find(colorId);
                var size = db.Sizes.Find(sizeId);
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
                        lst.Add(new CartItem { Product = product, Quantity = 1, Brand = brand, Category = category, Color = color, Size = size });
                    }
                    Session["Cart"] = lst;
                }
                else
                {

                    Session["Cart"] = new List<CartItem>() { new CartItem { Product = product, Quantity = 1, Brand = brand, Category = category, Color = color, Size = size } };
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

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
                if (item.Product.ProductDiscount == null || item.Product.ProductDiscount <= 0)
                {
                    total += (item.Product.ProductPrice * item.Quantity);
                }
                else
                {
                    var price = item.Product.ProductPrice - ((item.Product.ProductPrice * (int)item.Product.ProductDiscount) / 100);
                    total += (price * item.Quantity);
                }
            }
            var totalSum = total;
            return totalSum;
        }
        public int SumPriceCartPayment()
        {

            int id = (int)Session["OrderId"];
            var lst = from ordetail in db.OrdersDetails
                      where ordetail.OrderId == id
                      select ordetail;
            var total = 0;
            foreach (var item in lst)
            {

                total += ((int)item.OldPrice * item.Quantity);

            }
            if (Session["OrderId"] != null)
            {
                int id1 = (int)Session["OrderId"];
                var order = db.Orders.Find(id1);
                var voucher = 0;
                if (order != null)
                {
                    var v = db.Vouchers.Find(order.VoucherId);
                    voucher = (int)v.DiscountPercent;
                }
                if (voucher > 0)
                {
                    var totalSum = total - ((total * voucher) / 100);
                    return totalSum;
                }
                else
                {
                    var totalSum = total;
                    return totalSum;
                }
            }
            else
            {
                var totalSum = total;
                return totalSum;
            }

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
            if (Session["UserId"] != null)
            {
                List<CartItem> lst = (List<CartItem>)Session["Cart"];
                var lstCart = new List<CartItem>();
                if (lst != null)
                {
                    lstCart = lst;
                }
                return View(lst);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult CartAddress(Order or, string code, OrdersDetail ordetail)
        {

            try
            {
                if (Session["Cart"] != null)
                {
                    int u = (int)Session["UserId"];
                    var voucher = db.Vouchers.SingleOrDefault(x => x.Code.Equals(code));
                    User us = db.Users.SingleOrDefault(x => x.UserId == u);
                    if (voucher != null)
                    {
                        or.UserId = us.UserId;
                        or.VoucherId = voucher.VoucherId;
                        or.CreatedAt = DateTime.Now;
                        db.Orders.Add(or);
                        db.SaveChanges();
                        Session["OrderId"] = or.OrderId;

                        var lst = (List<CartItem>)Session["Cart"];
                        foreach (var item in lst)
                        {
                            ordetail.OrderId = or.OrderId;
                            ordetail.ProductId = item.Product.ProductId;
                            ordetail.ColorId = item.Color.ColorId;
                            ordetail.SizeId = item.Size.SizeId;
                            ordetail.Quantity = item.Quantity;
                            if (item.Product.ProductDiscount != null)
                            {
                                ordetail.OldPrice = item.Product.ProductPrice - ((item.Product.ProductPrice * (int)item.Product.ProductDiscount) / 100);
                            }
                            else
                            {
                                ordetail.OldPrice = item.Product.ProductPrice;
                            }
                            db.OrdersDetails.Add(ordetail);
                            db.SaveChanges();
                        }
                        Session["Cart"] = null;
                    }
                    else
                    {
                        or.UserId = us.UserId;
                        or.VoucherId = 1;
                        or.CreatedAt = DateTime.Now;
                        db.Orders.Add(or);
                        db.SaveChanges();
                        Session["OrderId"] = or.OrderId;

                        var lst = (List<CartItem>)Session["Cart"];
                        foreach (var item in lst)
                        {
                            ordetail.OrderId = or.OrderId;
                            ordetail.ProductId = item.Product.ProductId;
                            ordetail.ColorId = item.Color.ColorId;
                            ordetail.SizeId = item.Size.SizeId;
                            ordetail.Quantity = item.Quantity;
                            if (item.Product.ProductDiscount != null)
                            {
                                ordetail.OldPrice = item.Product.ProductPrice - ((item.Product.ProductPrice * (int)item.Product.ProductDiscount) / 100);
                            }
                            else
                            {
                                ordetail.OldPrice = item.Product.ProductPrice;
                            }
                            db.OrdersDetails.Add(ordetail);
                            db.SaveChanges();
                        }
                        Session["Cart"] = null;
                    }
                    return RedirectToAction("CartPayment");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {

                ViewBag.aaa = " loi " + ex;
                return View();
                throw;
            }

        }
        //public ActionResult CartShipping()
        //{

        //    return View();
        //}
        public ActionResult CartPayment()
        {
            if (Session["Cart"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["UserId"] != null)
                {
                    if (Session["OrderId"] != null)
                    {
                        int id = (int)Session["OrderId"];
                        var lst = from ordetail in db.OrdersDetails where ordetail.OrderId == id
                                  select ordetail;
                        if (lst != null)
                        {
                            var order = db.Orders.Find(id);
                            if (order != null)
                            {
                                ViewBag.Address = order.Address;
                                ViewBag.Phone = order.PhoneNumber;
                                var voucher = db.Vouchers.Find(order.VoucherId);
                                if (voucher != null)
                                {

                                    ViewBag.Vouch = voucher.DiscountPercent;
                                    return View(lst);
                                }
                                else
                                {
                                    return View(lst);
                                }
                            }
                            else
                            {
                                return View();
                            }

                        }
                        else
                        {
                            return RedirectToAction("Index", "Login");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            //return View();
        }
        public ActionResult ContinueShopping()
        {
            Session["Cart"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}