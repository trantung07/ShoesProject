using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesProject.Models
{
    public class Brands
    {
        public int id { get; set; }

        public string image { get; set; }

        public string name { get; set; }

        public int product { get; set; }

        public Brands()
        {

        }

        public Brands(int id, string image, string name, int product)
        {
            this.id = id;
            this.image = image;
            this.name = name;
            this.product = product;
        }
    }
}