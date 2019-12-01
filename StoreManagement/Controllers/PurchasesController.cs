using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Models;

namespace StoreManagement.Controllers
{
    [Authorize]
    public class PurchasesController : Controller
    {
        private StoreManagement_MVCEntities db = new StoreManagement_MVCEntities();

        // GET: Purchases
        public ActionResult Index()
        {
            var tblPurchases = db.tblPurchases.Include(t => t.tblProduct);
            return View(tblPurchases.ToList());
        }


        // GET: Purchases/Create
        public ActionResult Create(int? id)
        {
            ViewBag.pId = id;
            ViewBag.Product_ID = new SelectList(db.tblProducts, "Product_ID", "Product_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Purchase_ID,Product_ID,Quantity,Price,Purchase_Date,Dealer_Name,Dealer_Address")] tblPurchase tblPurchase)
        {
            if (ModelState.IsValid)
            {
                db.tblPurchases.Add(tblPurchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_ID = new SelectList(db.tblProducts, "Product_ID", "Product_Name", tblPurchase.Product_ID);
            return View(tblPurchase);
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
