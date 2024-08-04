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
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; }
        [Display(Name = "Giá tiền ")]
        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương")]
        public int? Gia { get; set; }

        [DisplayName("Mô tả"), DataType(DataType.MultilineText)]
        public string MoTa { get; set; }
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số dương")]
        public int? SoLuong { get; set; }
        [Display(Name = "Size")]
        [Required(ErrorMessage = "Chọn kích thước")]
        public int? SizeID { get; set; }
        [Display(Name = "Phân loại")]
        [Required(ErrorMessage = "Chọn phân loại")]
        public int? PhanLoaiID { get; set; }
    }
}