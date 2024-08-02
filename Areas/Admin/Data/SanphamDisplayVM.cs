using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLAdmin.Areas.Admin.Data
{
    public class SanphamDisplayVM
    {
        [Display(Name = "ID")]
        public int SanPhamID { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; }
        [Display(Name = "Giá tiền ")]
        public int? Gia { get; set; }

        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }
        [Display(Name = "Size")]
        public string Size { get; set; }
        [Display(Name = "Phân loại")]
        public string PhanLoai { get; set; }
    }
}