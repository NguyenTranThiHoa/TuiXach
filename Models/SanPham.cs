namespace QLAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int SanPhamID { get; set; }

        [StringLength(255)]
        public string TenSanPham { get; set; }

        public int? Gia { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        public int? SoLuong { get; set; }

        public int? SizeID { get; set; }

        public int? PhanLoaiID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual PhanLoai PhanLoai { get; set; }

        public virtual ProductSize ProductSize { get; set; }
    }
}
