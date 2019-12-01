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
    public class SalesController : Controller
    {
        private StoreManagement_MVCEntities db = new StoreManagement_MVCEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var tblSales = db.tblSales.Include(t => t.tblCustomer).Include(t => t.tblProduct);
            return View(tblSales.ToList());
        }

        // GET: Sales/Create
        public ActionResult Create(int? id)
        {
            ViewBag.Customer_ID = new SelectList(db.tblCustomers, "Customer_ID", "Customer_Name");
            ViewBag.Product_ID = new SelectList(db.tblProducts, "Product_ID", "Product_Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sales_ID,Product_ID,Quantity,Sales_Date,Customer_ID")] tblSale tblSale)
        {
            if (ModelState.IsValid)
            {
                db.tblSales.Add(tblSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_ID = new SelectList(db.tblCustomers, "Customer_ID", "Customer_Name", tblSale.Customer_ID);
            ViewBag.Product_ID = new SelectList(db.tblProducts, "Product_ID", "Product_Name", tblSale.Product_ID);
            return View(tblSale);
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
