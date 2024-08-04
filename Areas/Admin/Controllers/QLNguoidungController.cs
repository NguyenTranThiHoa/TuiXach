using QLAdmin.Areas.Admin.Data;
using QLAdmin.Areas.Admin.Helpers;
using QLAdmin.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLNguoidungController : Controller
    {
        private QLTuisach _context = new QLTuisach();

        [CustomAuthorize("Admin", "User")]
        public ActionResult QLNguoidung()
        {
            var lstnguoidung = (from s in _context.Customers
                                orderby s.CustomerID ascending
                                select new NguoidungDisplayVM()
                                {
                                    CustomerID = s.CustomerID,
                                    FullName = s.FullName,
                                    SoDienThoai = s.SoDienThoai,
                                    Email = s.Email,
                                    VaiTro = s.VaiTro
                                }).ToList();
            ViewBag.message = lstnguoidung.Count();
            return View(lstnguoidung);
        }

        [CustomAuthorize("Admin", "User")]
        public ActionResult DetailNguoidung(int id)
        {
            var item = _context.Customers
                .Where(x => x.CustomerID == id)
                .Select(t => new NguoiDungVM
                {
                    CustomerID = t.CustomerID,
                    FullName = t.FullName,
                    NgaySinh = t.NgaySinh,
                    SoDienThoai = t.SoDienThoai,
                    DiaChi = t.DiaChi,
                    Email = t.Email,
                    ProfileImage = t.ProfileImage,
                    Username = t.Username,
                    Password = t.Password,
                    VaiTro = t.VaiTro
                })
                .FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult AddND()
        {
            var model = new NguoiDungVM
            {
                VaiTroList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "User", Text = "User" },
                    new SelectListItem { Value = "Admin", Text = "Admin" }
                }
            };
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult AddND(NguoiDungVM formData, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var item = new Customer
                {
                    FullName = formData.FullName,
                    SoDienThoai = formData.SoDienThoai,
                    NgaySinh = formData.NgaySinh,
                    DiaChi = formData.DiaChi,
                    Email = formData.Email,
                    Username = formData.Username,
                    Password = formData.Password,
                    VaiTro = formData.VaiTro
                };

                // Xử lý ảnh nếu có
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/img/"), fileName);
                    fileUpload.SaveAs(path);
                    item.ProfileImage = fileName; // Lưu tên file vào thuộc tính ProfileImage
                }
                else
                {
                    item.ProfileImage = null; // Nếu không có ảnh, đặt giá trị null
                }

                _context.Customers.Add(item);
                _context.SaveChanges();
                return RedirectToAction("QLNguoidung");
            }

            // Cung cấp danh sách vai trò để hiển thị lại trên form nếu có lỗi
            formData.VaiTroList = new List<SelectListItem>
    {
        new SelectListItem { Value = "User", Text = "User" },
        new SelectListItem { Value = "Admin", Text = "Admin" }
    };
            return View(formData);
        }


        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult EditNguoidung(int id)
        {
            var nd = (from item in _context.Customers
                      where item.CustomerID == id
                      select new NguoiDungVM
                      {
                          CustomerID = item.CustomerID,
                          FullName = item.FullName,
                          SoDienThoai = item.SoDienThoai,
                          NgaySinh = item.NgaySinh,
                          DiaChi = item.DiaChi,
                          Email = item.Email,
                          ProfileImage = item.ProfileImage,
                          Username = item.Username,
                          Password = item.Password,
                          VaiTro = item.VaiTro,
                          VaiTroList = new List<SelectListItem>
                          {
                              new SelectListItem { Value = "User", Text = "User" },
                              new SelectListItem { Value = "Admin", Text = "Admin" }
                          }
                      }).FirstOrDefault();
            if (nd == null)
            {
                return RedirectToAction("QLNguoidung");
            }
            return View(nd);
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult EditNguoidung(NguoiDungVM formData, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var item = _context.Customers.Where(x => x.CustomerID == formData.CustomerID).FirstOrDefault();
                if (item != null)
                {
                    item.FullName = formData.FullName;
                    item.SoDienThoai = formData.SoDienThoai;
                    item.NgaySinh = formData.NgaySinh;
                    item.DiaChi = formData.DiaChi;
                    item.Email = formData.Email;
                    item.Username = formData.Username;
                    item.Password = formData.Password;
                    item.VaiTro = formData.VaiTro;

                    // Xử lý ảnh nếu có
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        var fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Areas/img/"), fileName);
                        fileUpload.SaveAs(path);

                        // Kiểm tra nếu ProfileImage không rỗng hoặc null trước khi xóa ảnh cũ
                        if (!string.IsNullOrEmpty(item.ProfileImage))
                        {
                            var oldImagePath = Path.Combine(Server.MapPath("~/Areas/img/"), item.ProfileImage);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Cập nhật đường dẫn ảnh mới
                        item.ProfileImage = fileName;
                    }


                    _context.SaveChanges();
                    return RedirectToAction("QLNguoidung");
                }
            }

            formData.VaiTroList = new List<SelectListItem>
    {
        new SelectListItem { Value = "User", Text = "User" },
        new SelectListItem { Value = "Admin", Text = "Admin" }
    };
            return View(formData);
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteND(int id)
        {
            var item = _context.Customers.Where(x => x.CustomerID == id).Select(x => new NguoiDungVM()
            {
                CustomerID = x.CustomerID,
                FullName = x.FullName,
                SoDienThoai = x.SoDienThoai,
                NgaySinh = x.NgaySinh,
                DiaChi = x.DiaChi,
                Email = x.Email,
                ProfileImage = x.ProfileImage,
                Username = x.Username,
                Password = x.Password,
                VaiTro = x.VaiTro
            }).FirstOrDefault();
            return View(item);
        }

        [HttpPost, ActionName("DeleteND")]
        [CustomAuthorize("Admin")]
        public ActionResult ConfirmDeleteND(int id)
        {
            var item = _context.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
            if (item != null)
            {
                _context.Customers.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("QLNguoidung");
        }
    }
}
