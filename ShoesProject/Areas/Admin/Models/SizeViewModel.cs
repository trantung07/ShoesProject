using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesProject.Areas.Admin.Models
{
    public class SizeViewModel
    {
        public int SizeId { get; set; }

        public string SizeValue { get; set; }

        public bool IsPresent { get; set; }

    }
}