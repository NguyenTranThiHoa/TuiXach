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
using Microsoft.SqlServer.Server;
using TuiXach.ViewModel;

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

                if (user.VaiTro == "Admin")
                {
                    return RedirectToAction("Index", "QLAdmin", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                return View();
            }
        }

        public ActionResult Logout()
        {
            // Xóa thông tin người dùng nhưng giữ giỏ hàng
            Session["Username"] = null;
            Session["UserID"] = null;

            return RedirectToAction("Index", "Home");
        }

        /********************************Đăng ký************************************/

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Register(Customer acc)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    // Kiểm tra xem tên đăng nhập đã tồn tại chưa
        //    //    var user = db.Customers.FirstOrDefault(c => c.Username == acc.Username);
        //    //    if (user != null)
        //    //    {
        //    //        ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
        //    //        return View(acc);
        //    //    }

        //    //    try
        //    //    {
        //    //        // Thêm khách hàng mới vào cơ sở dữ liệu mà không mã hóa mật khẩu
        //    //        db.Customers.Add(acc);
        //    //        db.SaveChanges();

        //    //        // Thiết lập session cho khách hàng mới sau khi lưu thành công
        //    //        Session["Username"] = acc.Username;
        //    //        Session["UserID"] = acc.CustomerID;

        //    //        return RedirectToAction("Login", "Account");
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
        //    //    }
        //    //}

        //    //// Nếu ModelState không hợp lệ, trả lại View với các lỗi
        //    //return View(acc);


        //        //var itemNew = new Customer();
        //        //itemNew.Email = acc.Email;
        //        //itemNew.Username = acc.Username;
        //        //itemNew.Password = acc.Password;

        //        //// Thêm vào
        //        //db.Customers.Add(itemNew);

        //        //db.SaveChanges(); // save đến cơ sở dữ liệu

        //        //return RedirectToAction("Login", "Account");
        //}


        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userExists = db.Customers.FirstOrDefault(c => c.Email == model.Email);
                if (userExists != null)
                {
                    ModelState.AddModelError("", "Tên Email đã tồn tại.");
                    return View(model);
                }

                var newCustomer = new Customer
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email
                };

                db.Customers.Add(newCustomer);
                db.SaveChanges();

                Session["Username"] = newCustomer.Username;
                Session["UserID"] = newCustomer.CustomerID;

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        /*****************************************************************************/

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }

            int userId = (int)Session["UserID"]; 
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
                    else
                    {
                        // Nếu không có hình ảnh mới, giữ nguyên hình ảnh cũ
                        customer.ProfileImage = customer.ProfileImage; // không thay đổi nếu không có tệp mới
                    }

                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("EditProfile", "Account");
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

