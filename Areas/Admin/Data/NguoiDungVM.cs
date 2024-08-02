using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Data
{
    public class NguoiDungVM
    {
        [Display(Name = "ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Họ Tên")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Hình ảnh")]
        public string ProfileImage { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Vai trò")]
        public string VaiTro { get; set; }

        public IEnumerable<SelectListItem> VaiTroList { get; set; }
    }
}
