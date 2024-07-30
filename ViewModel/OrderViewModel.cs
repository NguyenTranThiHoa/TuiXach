using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuiXach.ViewModel
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public string FullName { get; set; }

        [StringLength(255)]
        public string TrangThaiDonHang { get; set; }
        public DateTime? NgayDat { get; set; }
    }
}