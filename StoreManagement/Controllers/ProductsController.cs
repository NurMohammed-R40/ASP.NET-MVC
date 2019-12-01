using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Models;
using System.IO;

namespace StoreManagement.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private StoreManagement_MVCEntities db = new StoreManagement_MVCEntities();

        // GET: Products
        public ActionResult Index()
        {
            var tblProducts = db.tblProducts.Include(t => t.tblCategory);
            return View(tblProducts.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.tblCategories, "Category_ID", "Category_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Product_Name,Category_ID,Mesureing_Unit,Available_Quantity,Selling_Price,Picture,Product_Description")] tblProduct tblProduct,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    image.SaveAs(Path.Combine(Server.MapPath("~/Picture/"), Path.GetFileName(image.FileName)));
                    tblProduct.Picture = new byte[image.ContentLength];
                    image.InputStream.Read(tblProduct.Picture, 0, image.ContentLength);
                }
                db.tblProducts.Add(tblProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_ID = new SelectList(db.tblCategories, "Category_ID", "Category_Name", tblProduct.Category_ID);
            return View(tblProduct);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_ID = new SelectList(db.tblCategories, "Category_ID", "Category_Name", tblProduct.Category_ID);
            return View(tblProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_ID,Product_Name,Category_ID,Mesureing_Unit,Available_Quantity,Selling_Price,Picture,Product_Description")] tblProduct tblProduct, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    image.SaveAs(Path.Combine(Server.MapPath("~/Picture/"), Path.GetFileName(image.FileName)));
                    tblProduct.Picture = new byte[image.ContentLength];
                    image.InputStream.Read(tblProduct.Picture, 0, image.ContentLength);
                }
                db.Entry(tblProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_ID = new SelectList(db.tblCategories, "Category_ID", "Category_Name", tblProduct.Category_ID);
            return View(tblProduct);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct tblProduct = db.tblProducts.Find(id);
            db.tblProducts.Remove(tblProduct);
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
