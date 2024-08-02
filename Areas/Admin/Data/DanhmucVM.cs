using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLAdmin.Areas.Admin.Data
{
    public class DanhmucVM
    {
        [Display(Name = "ID")]
        public int PhanLoaiID { get; set; }

        [Display(Name = "Tên phân loại")]
        public string TenPhanLoai { get; set; }
    }
}