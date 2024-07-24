using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuiXach.Models;
using TuiXach.ViewModel;

namespace TuiXach.Controllers
{
    public class CheckoutController : Controller
    {
        private TuiXachShop db = new TuiXachShop();
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            var cart = Session["Cart"] as List<SanPhamViewModel>;
            if (cart == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Lấy thông tin khách hàng dựa trên Username lưu trong Session
            var username = Session["Username"]?.ToString();
            var customer = db.Customers.FirstOrDefault(c => c.Username == username);

            if (customer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new CheckoutViewModel
            {
                CartItems = cart,
                FullName = customer.FullName,
                Address = customer.DiaChi,
                Phone = customer.SoDienThoai,
                Email = customer.Email,
                TongTien = (int)cart.Sum(item => item.Gia * item.SoLuong)
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ProcessCheckout(CheckoutViewModel model)
        {
            var cart = Session["Cart"] as List<SanPhamViewModel>;
            var username = Session["Username"]?.ToString();
            var customer = db.Customers.FirstOrDefault(c => c.Username == username);

            if (cart == null || customer == null)
            {
                return RedirectToAction("Checkout");
            }

            customer.FullName = model.FullName;
            customer.DiaChi = model.Address;
            customer.SoDienThoai = model.Phone;
            customer.Email = model.Email;
            db.SaveChanges();

            var order = new Order
            {
                CustomerID = customer.CustomerID,
                TrangThaiDonHang = "Pending",
                NgayDat = DateTime.Now
            };
            db.Orders.Add(order);
            db.SaveChanges();

            foreach (var item in cart)
            {
                var orderDetail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    SanPhamID = item.SanPhamID,
                    SoLuong = item.SoLuong,
                    Gia = item.Gia,
                    TongTien = item.Gia * item.SoLuong,
                    HinhAnh = item.HinhAnh,
                    OrderDate = DateTime.Now
                };
                db.OrderDetails.Add(orderDetail);
            }
            db.SaveChanges();

            // Xóa giỏ hàng sau khi thanh toán
            Session["Cart"] = null;

            return RedirectToAction("OrderSuccess", new { orderID = order.OrderID });
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}