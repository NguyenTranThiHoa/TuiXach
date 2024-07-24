using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuiXach.Models;

namespace TuiXach.Controllers
{
    public class DonHangController : Controller
    {
        private TuiXachShop db = new TuiXachShop();
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListOrders()
        {
            var orders = db.Orders.ToList();
            return View(orders);
        }
    }
}