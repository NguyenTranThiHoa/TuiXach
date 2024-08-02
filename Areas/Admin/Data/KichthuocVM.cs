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

        [Display(Name = "Kích thước")]
        public string Size { get; set; }

    }
}
