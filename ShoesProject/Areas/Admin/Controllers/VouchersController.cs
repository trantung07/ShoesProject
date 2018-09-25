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
    public class VouchersController : Controller
    {
        private Shoes db = new Shoes();

        // GET: Admin/Vouchers
        public ActionResult Index()
        {
            return View(db.Vouchers.ToList());
        }

        // GET: Admin/Vouchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // GET: Admin/Vouchers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Vouchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoucherId,VoucherName,Code,DiscountPercent,Remain,ExpiredAt")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                voucher.Code = voucher.Code.ToUpper().ToString();
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voucher);
        }

        // GET: Admin/Vouchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Admin/Vouchers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoucherId,VoucherName,Code,DiscountPercent,Remain,ExpiredAt")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                voucher.Code = voucher.Code.ToUpper().ToString();
                db.Entry(voucher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucher);
        }

        // GET: Admin/Vouchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Admin/Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voucher voucher = db.Vouchers.Find(id);
            db.Vouchers.Remove(voucher);
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
