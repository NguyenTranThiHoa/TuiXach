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
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ Tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được quá 10 chữ số")]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống ")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Hình ảnh")]
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Chọn vai trò")]
        [Display(Name = "Vai trò")]
        public string VaiTro { get; set; }

        public IEnumerable<SelectListItem> VaiTroList { get; set; }
    }
}
