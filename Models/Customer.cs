namespace TuiXach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Họ tên.")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Tên chỉ được chứa chữ cái và khoảng trắng.")]
        [StringLength(255)]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại không được nhập chữ.")]
        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [RegularExpression(@"^[^@\s]+@gmail\.com$", ErrorMessage = "Email phải có định dạng @gmail.com.")]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(255)]
        public string ProfileImage { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        [StringLength(255)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
