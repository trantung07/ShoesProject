using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoesProjectModels.Model;

namespace ShoesProject.Areas.Admin.Controllers
{
    public class OrdersDetailsController : Controller
    {
        private Shoes db = new Shoes();

        // GET: Admin/OrdersDetails
        public ActionResult Index(int id)
        {
            var ordersDetails = db.OrdersDetails.Include(o => o.Color).Include(o => o.Order).Include(o => o.Product).Include(o => o.Size);
            var orderByID = ordersDetails.Where(order => order.OrderId == id);
                            
            return View(orderByID.ToList());
        }

        // GET: Admin/OrdersDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail ordersDetail = db.OrdersDetails.Find(id);
            if (ordersDetail == null)
            {
                return HttpNotFound();
            }
            return View(ordersDetail);
        }

        // GET: Admin/OrdersDetails/Create
        public ActionResult Create()
        {
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorValue");
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Address");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeValue");
            return View();
        }

        // POST: Admin/OrdersDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,ProductId,ColorId,SizeId,Quantity,OldPrice")] OrdersDetail ordersDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrdersDetails.Add(ordersDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorValue", ordersDetail.ColorId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Address", ordersDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", ordersDetail.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeValue", ordersDetail.SizeId);
            return View(ordersDetail);
        }

        // GET: Admin/OrdersDetails/Edit/5
        public ActionResult Edit(int oId, int pId, int cId, int sId)
        {
            var ss = (from details in db.OrdersDetails
                     where details.OrderId == oId && details.ProductId == pId && details.ColorId == cId && details.SizeId == sId
                     select details).ToList();
            var lstOD = db.OrdersDetails.Where(x => x.OrderId == oId && x.ProductId == pId && x.ColorId == cId && x.SizeId == sId);
            //OrdersDetail ordersDetail = db.OrdersDetails.Find(oId);

            List<OrdersDetail> y = new List<OrdersDetail>();

            //OrdersDetail ordersDetail = db.OrdersDetails.Find(;
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorValue", cId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Address", oId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", pId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeValue", sId);
            
            
            return View(y);
        }

        // POST: Admin/OrdersDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,ProductId,ColorId,SizeId,Quantity,OldPrice")] OrdersDetail ordersDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordersDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorValue", ordersDetail.ColorId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Address", ordersDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", ordersDetail.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeValue", ordersDetail.SizeId);
            return View(ordersDetail);
        }

        // GET: Admin/OrdersDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail ordersDetail = db.OrdersDetails.Find(id);
            if (ordersDetail == null)
            {
                return HttpNotFound();
            }
            return View(ordersDetail);
        }

        // POST: Admin/OrdersDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdersDetail ordersDetail = db.OrdersDetails.Find(id);
            db.OrdersDetails.Remove(ordersDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
