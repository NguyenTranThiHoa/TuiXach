using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLAdmin.Areas.Admin.Data
{
    public class ThongkeVM
    {
        public int OrderDetailID { get; set; }
        public int? OrderID { get; set; }
        public SanPham SanPham { get; set; }
        public int? SoLuong { get; set; }
        public int? Gia { get; set; }
        public int? TongTien { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Size { get; set; }

    }
}