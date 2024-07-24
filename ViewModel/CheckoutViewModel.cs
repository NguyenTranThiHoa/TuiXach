using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuiXach.ViewModel
{
    public class CheckoutViewModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public List<SanPhamViewModel> CartItems { get; set; }
        public decimal TongTien { get; set; }
    }
}