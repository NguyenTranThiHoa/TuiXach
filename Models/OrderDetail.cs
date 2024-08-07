namespace TuiXach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int OrderDetailID { get; set; }

        public int? OrderID { get; set; }

        public int? SanPhamID { get; set; }

        public int? SoLuong { get; set; }

        public int? Gia { get; set; }

        public int? TongTien { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        public int? SizeID { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual Order Order { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual ProductSize ProductSize { get; set; }  // Add this line
    }
}
