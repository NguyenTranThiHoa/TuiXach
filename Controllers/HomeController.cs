using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TuiXach.Models;
using TuiXach.ViewModel;

namespace TuiXach.Controllers
{
    public class HomeController : Controller
    {
        private TuiXachShop db = new TuiXachShop();
        public ActionResult Index()
        {
            int phanLoaiID = 2;

            var products = db.SanPhams
                .Where(p => p.PhanLoaiID == phanLoaiID)
                .GroupBy(p => p.TenSanPham) // Nhóm theo cả tên sản phẩm và SizeID
                .Select(g => g.OrderBy(p => p.SizeID).FirstOrDefault())
                .OrderByDescending(p => p.Gia) // Sắp xếp theo giá giảm dần
                .ToList();

            return View(products);
        }

        public ActionResult LienHe()
        {
            return View();
        }

        public ActionResult VeChungToi()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml", new { Layout = "~/Views/Shared/_LayoutPage1.cshtml" });
        }

        public ActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml", new { Layout = "~/Views/Shared/_LayoutPage1.cshtml" });
        }

        public PartialViewResult GetPhanLoai()
        {
            var phanLoais = db.PhanLoais.ToList();
            return PartialView("_PhanLoaiPartial", phanLoais);
        }

        public ActionResult ViewCart()
        {
            return View("~/Views/SanPham/ViewCart.cshtml", new { Layout = "~/Views/Shared/_LayoutPage.cshtml" });
        }

        public ActionResult UuDai()
        {
            int phanLoaiID = 4;

            var products = db.SanPhams
                .Where(p => p.PhanLoaiID == phanLoaiID)
                .GroupBy(p => p.TenSanPham) // Nhóm theo cả tên sản phẩm và SizeID
                .Select(g => g.OrderBy(p => p.SizeID).FirstOrDefault())
                .OrderByDescending(p => p.Gia) // Sắp xếp theo giá giảm dần
                .ToList();

            return View(products);
        }

    }
}