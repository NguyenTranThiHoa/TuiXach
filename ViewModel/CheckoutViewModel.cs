using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuiXach.ViewModel
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Địa chỉ nhận hàng là bắt buộc.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public List<SanPhamViewModel> CartItems { get; set; }
        public decimal TongTien { get; set; }
    }
}