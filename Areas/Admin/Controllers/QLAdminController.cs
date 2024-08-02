using QLAdmin.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLAdminController : Controller
    {
        private QLTuisach _context = new QLTuisach();

        // GET: Admin/QLAdmin
        public ActionResult Index()
        {
            ViewBag.TotalCategories = _context.PhanLoais.Count();
            ViewBag.TotalProducts = _context.SanPhams.Count();
            ViewBag.TotalUsers = _context.Customers.Count();
            ViewBag.TotalOrders = _context.Orders.Count();
            ViewBag.TodayOrders = GetTodayOrdersCount(); 
            return View();
        }

        private int GetTodayOrdersCount()
        {
            DateTime todayDate = DateTime.Now.Date;
            return _context.Orders.Count(o => o.NgayDat == todayDate);
        }
    }
}
