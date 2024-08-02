using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLAdmin.Areas.Admin.Data
{
    public class SanphamVM
    {
        [Display(Name = "ID")]
        public int SanPhamID { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; }
        [Display(Name = "Giá tiền ")]
        public int? Gia { get; set; }

        [DisplayName("Mô tả"), DataType(DataType.MultilineText)]
        public string MoTa { get; set; }

        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }
        [Display(Name = "Size")]
        public int? SizeID { get; set; }
        [Display(Name = "Phân loại")]
        public int? PhanLoaiID { get; set; }
    }
}