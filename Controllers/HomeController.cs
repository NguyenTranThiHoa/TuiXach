using System;
using System.Collections.Generic;
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
            return View();
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

        public ActionResult Checkout()
        {
            return View("~/Views/Checkout/Checkout.cshtml", new { Layout = "~/Views/Shared/_LayoutPage.cshtml" });
        }

    }
}