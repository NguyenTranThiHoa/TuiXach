using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TuiXach.Models;
using TuiXach.ViewModel;

namespace TuiXach.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        private TuiXachShop db = new TuiXachShop();

        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult SanPham(int id)
        //{
        //    var products = db.SanPhams.Where(p => p.PhanLoaiID == id).OrderByDescending(p => p.Gia).ToList();
        //    ViewBag.soluong = products.Count();
        //    return View(products);
        //}

        public ActionResult SanPham(int id)
        {
            var products = db.SanPhams
                .Where(p => p.PhanLoaiID == id)
                .GroupBy(p => p.TenSanPham)
                .Select(g => g.OrderBy(p => p.SizeID).FirstOrDefault()) // Chọn sản phẩm đầu tiên theo SizeID trong mỗi nhóm
                .OrderByDescending(p => p.Gia)
                .ToList();

            return View(products);
        }

        public ActionResult ChiTietSanPham(int maSanPham)
        {
            var sp = (from s in db.SanPhams
                      join cd in db.PhanLoais on s.PhanLoaiID equals cd.PhanLoaiID
                      join a in db.ProductSizes on s.SizeID equals a.SizeID
                      where s.SanPhamID == maSanPham
                      select new SanPhamViewModel()
                      {
                          SanPhamID = s.SanPhamID,
                          TenSanPham = s.TenSanPham,
                          Gia = s.Gia,
                          MoTa = s.MoTa,
                          HinhAnh = s.HinhAnh,
                          TenPhanLoai = cd.TenPhanLoai,
                          SizeID = a.SizeID,
                          Size = a.Size,
                          SizeList = db.ProductSizes.Select(ps => new SelectListItem
                          {
                              Value = ps.SizeID.ToString(),
                              Text = ps.Size
                          }).ToList()
                      }).FirstOrDefault();

            if (sp == null)
            {
                ViewBag.message = "Sản phẩm này không tồn tại";
                return RedirectToAction("SanPham", "SanPham");
            }

            return View(sp);
        }

        /***********************************Giỏ hàng**********************************/

        [HttpPost]
        public ActionResult AddToCart(int sanPhamID, int sizeID, int quantity)
        {
            var product = db.SanPhams.Find(sanPhamID);
            var size = db.ProductSizes.Find(sizeID);

            if (product != null && size != null)
            {
                var cart = Session["Cart"] as List<SanPhamViewModel> ?? new List<SanPhamViewModel>();

                var existingItem = cart.FirstOrDefault(c => c.SanPhamID == sanPhamID && c.SizeID == sizeID);

                if (existingItem != null)
                {
                    existingItem.SoLuong += quantity;
                }
                else
                {
                    var cartItem = new SanPhamViewModel
                    {
                        SanPhamID = product.SanPhamID,
                        TenSanPham = product.TenSanPham,
                        Gia = product.Gia,
                        HinhAnh = product.HinhAnh,
                        SizeID = size.SizeID,
                        Size = size.Size,
                        SoLuong = quantity
                    };
                    cart.Add(cartItem);
                }
                // Update the session cart
                Session["Cart"] = cart;
            }

            return RedirectToAction("ViewCart", "SanPham");
        }

        //[HttpPost]
        //public ActionResult AddToCart(int sanPhamID, int sizeID, int quantity)
        //{
        //    var product = db.SanPhams.Find(sanPhamID);
        //    var size = db.ProductSizes.Find(sizeID);

        //    if (product != null && size != null)
        //    {
        //        var cartItem = new SanPhamViewModel
        //        {
        //            SanPhamID = product.SanPhamID,
        //            TenSanPham = product.TenSanPham,
        //            Gia = product.Gia,
        //            HinhAnh = product.HinhAnh,
        //            SizeID = size.SizeID,
        //            Size = size.Size,
        //            SoLuong = quantity
        //        };

        //        var cart = Session["Cart"] as List<SanPhamViewModel>;
        //        if (cart == null)
        //        {
        //            cart = new List<SanPhamViewModel>();
        //        }
        //        cart.Add(cartItem);
        //        Session["Cart"] = cart;
        //    }

        //    return RedirectToAction("ViewCart", "SanPham");
        //}


        public ActionResult ViewCart()
        {
            var cart = Session["Cart"] as List<SanPhamViewModel>;
            if (cart == null)
            {
                cart = new List<SanPhamViewModel>();
            }

            return View(cart);
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int SanPhamID, int SizeID)
        {
            var cart = Session["Cart"] as List<SanPhamViewModel>;
            if (cart != null)
            {
                var cartItem = cart.FirstOrDefault(c => c.SanPhamID == SanPhamID && c.SizeID == SizeID);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    Session["Cart"] = cart;
                }
            }
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public ActionResult UpdateCart(int sanPhamID, int sizeID, int quantity)
        {
            var cart = Session["Cart"] as List<SanPhamViewModel>;
            if (cart == null)
            {
                cart = new List<SanPhamViewModel>();
            }

            var existingItem = cart.FirstOrDefault(c => c.SanPhamID == sanPhamID && c.SizeID == sizeID);

            if (existingItem != null)
            {
                existingItem.SoLuong = quantity; // Cập nhật số lượng
            }

            Session["Cart"] = cart;

            return Json(new { success = true });
        }

        /***************************************************************/
        public ActionResult Search(string searchTerm, int id) 
        {
            var products = db.SanPhams
                .Where(p => p.TenSanPham.Contains(searchTerm))
                .Where(p => p.PhanLoaiID == id)
                .GroupBy(p => p.TenSanPham)
                .Select(g => g.OrderBy(p => p.SizeID).FirstOrDefault()) // Chọn sản phẩm đầu tiên theo SizeID trong mỗi nhóm
                .OrderByDescending(p => p.Gia)
                .ToList();

            return View(products);
        }

        // GET: SanPham/Suggestions
        public JsonResult Suggestions(string term)
        {
            var suggestions = db.SanPhams
                .Where(p => p.TenSanPham.Contains(term))
                .GroupBy(p => p.TenSanPham)
                .Select(g => g.FirstOrDefault()) 
                .ToList()
                .Select(p => new
                {
                    TenSanPham = p.TenSanPham,
                    SizeID = p.SizeID,
                    p.Gia,
                    p.HinhAnh
                });

            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }

    }
}