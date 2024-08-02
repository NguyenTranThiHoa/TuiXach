using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLAdmin.Areas.Admin.Data
{
    public class NguoidungDisplayVM
    {
        [Display(Name = "ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Họ Tên")]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Vai trò")]
        public string VaiTro { get; set; }
    }
}