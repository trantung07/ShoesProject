using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesProject.Areas.Admin.Models
{
    public class ColorViewModel
    {
        public int ColorId { get; set; }

        public string ColorValue { get; set; }

        public string ColorCode { get; set; }

        public bool isPresent { get; set; }
    }
}