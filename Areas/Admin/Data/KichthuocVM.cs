using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLAdmin.Areas.Admin.Data
{
    public class KichthuocVM
    {
        [Display(Name = "ID")]
        public int SizeID { get; set; }
        [Required(ErrorMessage = "Kích thước không được để trống.")]
        [StringLength(5, ErrorMessage = "Kích thước không được vượt quá 5 ký tự.")]
        [Display(Name = "Kích thước")]
        public string Size { get; set; }

    }
}
