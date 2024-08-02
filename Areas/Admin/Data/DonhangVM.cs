using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLAdmin.Areas.Admin.Data
{
    public class DonhangVM
    {
        [Display(Name = "ID")]
        public int OrderID { get; set; }
        [Display(Name = "Tên khách hàng")]
        public string CustomerID { get; set; }
        [Display(Name = "Trạng thái đơn  hàng")]
        public string TrangThaiDonHang { get; set; }
        [Display(Name = " Ngày đặt")]
        public DateTime? NgayDat { get; set; }
        public Customer Customer { get; set; }

    }
}