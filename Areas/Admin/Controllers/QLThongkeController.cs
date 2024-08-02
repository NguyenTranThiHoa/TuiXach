using Newtonsoft.Json;
using QLAdmin.Areas.Admin.Data;
using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLThongkeController : Controller
    {
        private QLTuisach _context = new QLTuisach();

        public ActionResult Index(DateTime? start_date, DateTime? end_date)
        {
            var orders = from o in _context.OrderDetails
                         join sp in _context.SanPhams on o.SanPhamID equals sp.SanPhamID
                         select new ThongkeVM
                         {
                             OrderDetailID = o.OrderDetailID,
                             OrderID = o.OrderID,
                             SanPham = sp,
                             SoLuong = o.SoLuong,
                             Gia = o.Gia,
                             TongTien = o.TongTien,
                             OrderDate = o.OrderDate,
                             Size = o.Size
                         };

            var orderList = orders.ToList();

            if (start_date.HasValue && end_date.HasValue)
            {
                orderList = orderList.Where(o => o.OrderDate.HasValue &&
                                                  o.OrderDate.Value.Date >= start_date.Value.Date &&
                                                  o.OrderDate.Value.Date <= end_date.Value.Date).ToList();
            }

            ViewBag.TotalRevenue = orderList.Sum(o => o.TongTien);
            return View(orderList);
           
        }
    }
}