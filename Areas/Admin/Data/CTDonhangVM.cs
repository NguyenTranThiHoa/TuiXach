using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLAdmin.Areas.Admin.Data
{
    public class CTDonhangVM
    {
        public int OrderDetailID { get; set; }

        public int? OrderID { get; set; }

        public int? SanPhamID { get; set; }

        public int? SoLuong { get; set; }

        public int? Gia { get; set; }

        public int? TongTien { get; set; }
        public string HinhAnh { get; set; }

        public DateTime? OrderDate { get; set; }

        public string Size { get; set; }

        public virtual Order Order { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}