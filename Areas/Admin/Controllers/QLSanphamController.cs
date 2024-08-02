using PagedList;
using QLAdmin.Areas.Admin.Data;
using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLSanphamController : Controller
    {
        // GET: Admin/QLSanphamDefault
        private QLTuisach _context = new QLTuisach();
        public ActionResult Index(string search, int page = 1)
        {
            int pageSize = 8;

            // Lấy dữ liệu sản phẩm từ cơ sở dữ liệu với liên kết các bảng khác
            var lstsanpham = (from s in _context.SanPhams
                              join sz in _context.ProductSizes on s.SizeID equals sz.SizeID
                              join pl in _context.PhanLoais on s.PhanLoaiID equals pl.PhanLoaiID
                              orderby s.SanPhamID ascending
                              select new SanphamDisplayVM
                              {
                                  SanPhamID = s.SanPhamID,
                                  TenSanPham = s.TenSanPham,
                                  Gia = s.Gia,
                                  HinhAnh = s.HinhAnh,
                                  SoLuong = s.SoLuong,
                                  Size = sz.Size,
                                  PhanLoai = pl.TenPhanLoai
                              });

            // Log giá trị tìm kiếm
            System.Diagnostics.Debug.WriteLine($"Tìm kiếm: {search}");

            // Áp dụng tìm kiếm nếu có từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                lstsanpham = lstsanpham.Where(p => p.TenSanPham.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Log số lượng sản phẩm sau khi lọc
            System.Diagnostics.Debug.WriteLine($"Số lượng sản phẩm sau lọc: {lstsanpham.Count()}");

            // Phân trang dữ liệu
            var pagedProducts = lstsanpham.ToPagedList(page, pageSize);

            // Đưa từ khóa tìm kiếm về view
            ViewBag.SearchTerm = search;

            // Trả về view với dữ liệu phân trang
            return View(pagedProducts);
        }
        public ActionResult DetailSP(int id)
        {
            var item = _context.SanPhams.Where(x => x.SanPhamID == id).Select(t => new SanphamVM()
            {
                SanPhamID = t.SanPhamID,
                TenSanPham = t.TenSanPham,
                Gia = t.Gia,
                MoTa = t.MoTa,
                SoLuong = t.SoLuong,
                HinhAnh = t.HinhAnh,
                SizeID= t.SizeID,
                PhanLoaiID = t.PhanLoaiID,
            }).FirstOrDefault();
            if (item != null)
            {
                var size = _context.ProductSizes.FirstOrDefault(n => n.SizeID == item.SizeID);
                ViewBag.Tensize = size != null ? size.Size : "Không xác định";
                var pl = _context.PhanLoais.FirstOrDefault(d => d.PhanLoaiID == item.PhanLoaiID);
                ViewBag.TenPL = pl != null ? pl.TenPhanLoai : "Không xác định";
            }

            return View(item);
        }

        [HttpGet]
        public ActionResult AddSP()
        {
            var lstSize = _context.ProductSizes.OrderBy(x => x.Size).ToList();
            var lstPL = _context.PhanLoais.OrderBy(x => x.TenPhanLoai).ToList();

            ViewBag.SizeID = new SelectList(lstSize, "SizeID", "Size");
            ViewBag.PhanLoaiID = new SelectList(lstPL, "PhanLoaiID", "TenPhanLoai");
            return View();

        }
        [HttpPost]
        public ActionResult AddSP(SanphamVM formData, HttpPostedFileBase fileUpload)
        {
            var item = new SanPham();
            item.TenSanPham = formData.TenSanPham;
            item.Gia = formData.Gia;
            item.MoTa = formData.MoTa;
            item.SoLuong = formData.SoLuong;
            item.SizeID = formData.SizeID;
            item.PhanLoaiID = formData.PhanLoaiID;
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                //get filename
                var fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                //get path
                var path = Path.Combine(Server.MapPath("~/Areas/img/"), fileName);

                //ktra image exist
                if (System.IO.File.Exists(path))
                {
                    ViewBag.mesage = "Ảnh này đã tồn tại!";
                }
                else
                {
                    fileUpload.SaveAs(path);
                    //lưu file name vào DB ảnh bìa
                    item.HinhAnh = fileName;
                }
            }

            _context.SanPhams.Add(item);
            _context.SaveChanges();// save to DB
            return RedirectToAction("Index", "QLSanpham");
        }
        [HttpGet]
        public ActionResult DeleteSP(int id)
        {
            var item = _context.SanPhams.Where(x => x.SanPhamID == id).Select(x => new SanphamVM()
            {
                SanPhamID = x.SanPhamID,
                TenSanPham = x.TenSanPham,
                Gia = x.Gia,
                SoLuong =x.SoLuong,
                MoTa = x.MoTa,  
                SizeID = x.SizeID,  
                PhanLoaiID = x.PhanLoaiID,  

            }).FirstOrDefault();
            return View(item);
        }
        [HttpPost, ActionName("DeleteSP")]
        public ActionResult ConfirmDeleteSP(int id)
        {
            var item = _context.SanPhams.Where(x => x.SanPhamID == id).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index", "QLSanpham");
            }
            _context.SanPhams.Remove(item);

            _context.SaveChanges();

            return RedirectToAction("Index", "QLSanpham");
        }
        [HttpGet]
        public ActionResult EditSP(int id)
        {
            var lstSize = _context.ProductSizes.OrderBy(x => x.Size).ToList();
            var lstPL = _context.PhanLoais.OrderBy(x => x.TenPhanLoai).ToList();

            ViewBag.SizeID = new SelectList(lstSize, "SizeID", "Size");
            ViewBag.PhanLoaiID = new SelectList(lstPL, "PhanLoaiID", "TenPhanLoai");
            var sp = (from item in _context.SanPhams
                      where item.SanPhamID == id
                      select new SanphamVM()
                      {
                          SanPhamID = item.SanPhamID,
                          TenSanPham = item.TenSanPham,
                          Gia = item.Gia,
                          MoTa = item.MoTa,
                          SoLuong = item.SoLuong,
                          HinhAnh = item.HinhAnh,
                          SizeID = item.SizeID,
                          PhanLoaiID = item.PhanLoaiID
                      }).FirstOrDefault();
            if (sp == null)
            {
                return RedirectToAction("Index", "QlSanpham");
            }
            return View(sp);
        }


        [HttpPost]
        public ActionResult EditSP(SanphamVM formData, HttpPostedFileBase fileUpload)
        {
            var item = _context.SanPhams.Where(x => x.SanPhamID == formData.SanPhamID).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index", "QLSanpham");
            }

            item.TenSanPham = formData.TenSanPham;
            item.Gia = formData.Gia;
            item.MoTa = formData.MoTa;
            item.SoLuong = formData.SoLuong;
            item.SizeID = formData.SizeID;
            item.PhanLoaiID = formData.PhanLoaiID;

            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                // Get filename
                var fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                // Get path
                var path = Path.Combine(Server.MapPath("~/Areas/img/"), fileName);

                // Check if image already exists

                // Delete old image if it exists
                var oldImagePath = Path.Combine(Server.MapPath("~/Areas/img/"), item.HinhAnh);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                // Save new image
                fileUpload.SaveAs(path);
                // Update image name in the database
                item.HinhAnh = fileName;

            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges(); // Save to DB
            return RedirectToAction("Index", "QLSanpham");
        }
        
    }

}