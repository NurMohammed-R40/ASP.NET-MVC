using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using StoreManagement.Models;
using StoreManagement.Reports;

namespace StoreManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        StoreManagement_MVCEntities db = new StoreManagement_MVCEntities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CatNav()
        {
            return PartialView("_CatNav", db.tblCategories.ToList());
        }
        [AllowAnonymous]
        public ActionResult Product(int id, int page = 1)
        {
            ViewBag.CategoryName = db.tblCategories.First(c => c.Category_ID == id).Category_Name;

            int perPage = 3;
            int count = db.tblProducts.Where(p => p.Category_ID == id).Count();
            int totalPage = (int)Math.Ceiling((double)count / perPage);
            ViewBag.TotalPages = totalPage;
            ViewBag.CategoryID = id;
            ViewBag.CurrentPage = page;
            return View(db.tblProducts.OrderBy(p => p.Product_ID).Where(p => p.Category_ID == id).Skip(perPage * (page - 1)).Take(perPage).ToList());
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Report";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "IDB-BISEW";

            return View();
        }

        public ActionResult Purchase()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/"), "Purchase.rpt"));
            var PurchaseReport = (from pr in db.tblPurchases
                                  join p in db.tblProducts on pr.Product_ID equals p.Product_ID
                                  join c in db.tblCategories on p.Category_ID equals c.Category_ID
                                  select new { p.Product_Name, pr.Price, pr.Quantity, pr.Purchase_Date, pr.Dealer_Name, c.Category_Name }).ToList();
            rd.SetDataSource(PurchaseReport);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream,"application/pdf","PurchaseReport.pdf");
        }

        public ActionResult Sales()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/"), "Sales.rpt"));
            var SalesReport = (from s in db.tblSales
                                  join p in db.tblProducts on s.Product_ID equals p.Product_ID
                                  join c in db.tblCategories on p.Category_ID equals c.Category_ID
                                  join cs in db.tblCustomers on s.Customer_ID equals cs.Customer_ID
                                  select new { p.Product_Name, s.Quantity, s.Sales_Date, cs.Customer_Name, c.Category_Name, p.Selling_Price }).ToList();
            rd.SetDataSource(SalesReport);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream,"application/pdf","SalesReport.pdf");
        }

    }
}