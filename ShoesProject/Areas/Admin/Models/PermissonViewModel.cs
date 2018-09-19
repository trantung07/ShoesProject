using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesProject.Areas.Admin.Models
{
    public class PermissonViewModel
    {
        public int PermissonId { get; set; }
        public string PermissonName { get; set; }
        public string Description { get; set; }
        public bool isGranted { get; set; }
    }
}