using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TuiXach.ViewModel
{
    public class SanPhamViewModel
    {
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public int? Gia { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public string TenPhanLoai { get; set; }
        public int SizeID { get; set; }
        public string Size { get; set; } 
        public int SoLuong { get; set; } 
        public List<SelectListItem> SizeList { get; set; }
    }
}