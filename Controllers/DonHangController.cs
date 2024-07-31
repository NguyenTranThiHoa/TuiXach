using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuiXach.Models;
using TuiXach.ViewModel;

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

        public ActionResult ListOrder()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not logged in
            }

            int userId = (int)Session["UserID"]; // Get the logged-in user's ID

            var orders = (from s in db.Orders
                          join cd in db.Customers on s.CustomerID equals cd.CustomerID into cdtemp
                          from cdf in cdtemp.DefaultIfEmpty()
                          where s.CustomerID == userId 
                          orderby s.NgayDat descending
                          select new OrderViewModel()
                          {
                              OrderID = s.OrderID,
                              FullName = cdf != null ? cdf.FullName : "Chưa có thông tin",
                              TrangThaiDonHang = s.TrangThaiDonHang,
                              NgayDat = s.NgayDat
                          }).ToList();

            return View(orders);
        }


        //public ActionResult ListOrder()
        //{
        //    var orders = (from s in db.Orders
        //                  join cd in db.Customers on s.CustomerID equals cd.CustomerID into cdtemp
        //                  from cdf in cdtemp.DefaultIfEmpty()
        //                  orderby s.NgayDat descending
        //                  select new OrderViewModel()
        //                  {
        //                      OrderID = s.OrderID,
        //                      FullName = cdf != null ? cdf.FullName : "Chưa có thông tin",
        //                      TrangThaiDonHang = s.TrangThaiDonHang,
        //                      NgayDat = s.NgayDat
        //                  }).ToList();

        //    return View(orders);
        //}
        public ActionResult ViewOrders(int id)
        {
            try
            {
                var order = db.Orders
                              .Include("Customer")
                              .Include("OrderDetails.SanPham")
                              .FirstOrDefault(o => o.OrderID == id);

                if (order == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new CheckoutViewModel
                {
                    FullName = order.Customer?.FullName,
                    Address = order.Customer?.DiaChi,
                    Phone = order.Customer?.SoDienThoai,
                    Email = order.Customer?.Email,
                    CartItems = order.OrderDetails.Select(od => new SanPhamViewModel
                    {
                        SanPhamID = od.SanPhamID ?? 0,
                        TenSanPham = od.SanPham?.TenSanPham,
                        Gia = od.Gia,
                        HinhAnh = od.HinhAnh,
                        //Size = od.Size
                        SoLuong = od.SoLuong ?? 0
                    }).ToList(),
                    TongTien = order.OrderDetails.Sum(od => (od.Gia ?? 0) * (od.SoLuong ?? 0))
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        public ActionResult DeleteOrder(int id)
        {
            var order = db.Orders.Include("OrderDetails").FirstOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            db.OrderDetails.RemoveRange(order.OrderDetails);
            db.Orders.Remove(order);

            db.SaveChanges();

            return RedirectToAction("ListOrder");
        }

    }
}