using QLAdmin.Areas.Admin.Data;
using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLDonhangController : Controller
    {
        // GET: Admin/QLDonhang
        private QLTuisach _context = new QLTuisach();
        public ActionResult Index()
        {

            var orders = (from o in _context.Orders
                          join c in _context.Customers on o.CustomerID equals c.CustomerID
                          select new DonhangVM
                          {
                              OrderID = o.OrderID,
                              CustomerID = c.FullName,
                              TrangThaiDonHang = o.TrangThaiDonHang,
                              NgayDat = o.NgayDat,
                              Customer = c
                          }).ToList();
            return View(orders);
        }

      
        public ActionResult Details(int id)
        {
            var order = (from o in _context.Orders
                         join c in _context.Customers on o.CustomerID equals c.CustomerID
                         where o.OrderID == id
                         select new DonhangVM
                         {
                             OrderID = o.OrderID,
                             CustomerID= c.FullName,
                             TrangThaiDonHang = o.TrangThaiDonHang,
                             NgayDat = o.NgayDat,
                             Customer = o.Customer
                         }).FirstOrDefault();

            if (order == null)
                return HttpNotFound();

            var orderItems = (from oi in _context.OrderDetails
                              where oi.OrderID == id
                              select new CTDonhangVM
                              {
                                  OrderDetailID = oi.OrderDetailID,
                                  OrderID = oi.OrderID,
                                  SanPhamID = oi.SanPhamID,
                                  SoLuong = oi.SoLuong,
                                  Gia = oi.Gia,
                                  TongTien = oi.TongTien,
                                  HinhAnh = oi.HinhAnh,
                                  OrderDate = oi.OrderDate,
                                  Size = oi.Size,
                                  SanPham = oi.SanPham
                              }).ToList();

            ViewBag.OrderItems = orderItems;
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id, string status)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderID == id);

            if (order == null)
                return HttpNotFound();

            order.TrangThaiDonHang = status;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
    }