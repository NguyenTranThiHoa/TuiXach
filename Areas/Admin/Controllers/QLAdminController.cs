using QLAdmin.Areas.Admin.Data;
using QLAdmin.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLAdminController :Controller
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

            // Lấy số lượng đơn hàng theo từng tháng
            var monthlyStats = _context.Orders
                .Where(o => o.NgayDat.HasValue) // Đảm bảo NgayDat không phải null
                .GroupBy(o => o.NgayDat.Value.Month) // Sử dụng .Value cho DateTime?
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .OrderBy(m => m.Month)
                .ToList();

            // Chuyển đổi số tháng và số lượng đơn hàng thành mảng
            ViewBag.Months = monthlyStats.Select(m => GetMonthName(m.Month)).ToArray();
            ViewBag.MonthlyOrders = monthlyStats.Select(m => m.Count).ToArray();

            return View();
        }

        private string GetMonthName(int month)
        {
            var monthNames = new[]
            {
        "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
        "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"
    };

            return monthNames[month - 1]; // month là từ 1 đến 12
        }

        private int GetTodayOrdersCount()
        {
            DateTime todayDate = DateTime.Now.Date;
            return _context.Orders.Count(o => o.NgayDat == todayDate);
        }
    }
}
