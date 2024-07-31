using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuiXach.Models;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;

namespace TuiXach.Controllers
{
    public class AccountController : Controller
    {
        private TuiXachShop db = new TuiXachShop();

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer acc)
        {
            var user = db.Customers.FirstOrDefault(a => a.Username == acc.Username && a.Password == acc.Password);

            if (user != null)
            {
                Session["Username"] = user.Username;
                Session["UserID"] = user.CustomerID;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                return View();
            }
        }

        //public ActionResult Logout()
        //{
        //    Session.Clear(); // Clear all session data
        //    return RedirectToAction("Index", "Home");
        //}


        public ActionResult Logout()
        {
            // Xóa thông tin người dùng nhưng giữ giỏ hàng
            Session["Username"] = null;
            Session["UserID"] = null;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer acc)
        {
            if (ModelState.IsValid)
            {
                var user = db.Customers.FirstOrDefault(c => c.Username == acc.Username);
                if (user != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                    return View(acc);
                }

                Session["Username"] = user.Username;
                Session["UserID"] = user.CustomerID;

                try
                {
                    db.Customers.Add(acc);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
                }
            }
            return View(acc);
        }

        /******************************Chỉnh sửa thông tin người dùng**************************************/
        //[HttpGet]
        //public ActionResult EditProfile()
        //{
        //    if (Session["Username"] == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    string username = Session["Username"].ToString();
        //    var customer = db.Customers.FirstOrDefault(c => c.Username == username);

        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(customer);
        //}


        //[HttpPost]
        //public ActionResult EditProfile(Customer updatedCustomer, HttpPostedFileBase ProfilePicture)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var customer = db.Customers.FirstOrDefault(c => c.CustomerID == updatedCustomer.CustomerID);
        //        if (customer != null)
        //        {
        //            customer.FullName = updatedCustomer.FullName;
        //            customer.Email = updatedCustomer.Email;
        //            customer.SoDienThoai = updatedCustomer.SoDienThoai;
        //            customer.DiaChi = updatedCustomer.DiaChi;
        //            customer.NgaySinh = updatedCustomer.NgaySinh;
        //            customer.Username = updatedCustomer.Username;
        //            customer.Password = updatedCustomer.Password;

        //            if (ProfilePicture != null && ProfilePicture.ContentLength > 0)
        //            {
        //                string path = Path.Combine(Server.MapPath("~/image/Anh_nguoi_dung/"), ProfilePicture.FileName);
        //                ProfilePicture.SaveAs(path);
        //                customer.ProfileImage = ProfilePicture.FileName;
        //            }

        //            try
        //            {
        //                db.SaveChanges();
        //                return RedirectToAction("Index", "Home");
        //            }
        //            catch (Exception ex)
        //            {
        //                ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
        //            }
        //        }
        //    }
        //    return View(updatedCustomer);
        //}

        /*****************************************************************************/

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }

            int userId = (int)Session["UserID"]; // Get user ID from session
            var customer = db.Customers.FirstOrDefault(c => c.CustomerID == userId);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult EditProfile(Customer updatedCustomer, HttpPostedFileBase ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == updatedCustomer.CustomerID);
                if (customer != null)
                {
                    customer.FullName = updatedCustomer.FullName;
                    customer.Email = updatedCustomer.Email;
                    customer.SoDienThoai = updatedCustomer.SoDienThoai;
                    customer.DiaChi = updatedCustomer.DiaChi;
                    customer.NgaySinh = updatedCustomer.NgaySinh;
                    customer.Username = updatedCustomer.Username;

                    if (!string.IsNullOrEmpty(updatedCustomer.Password))
                    {
                        customer.Password = updatedCustomer.Password;
                    }

                    if (ProfilePicture != null && ProfilePicture.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(ProfilePicture.FileName);
                        string path = Path.Combine(Server.MapPath("~/image/Anh_thihoa"), fileName);
                        ProfilePicture.SaveAs(path);
                        customer.ProfileImage = fileName;
                    }

                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
                    }
                }
            }
            return View(updatedCustomer);
        }
    }
}

