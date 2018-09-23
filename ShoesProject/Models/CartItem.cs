using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ShoesProject.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        //public Color Color { get; set; }
        //public Size Size { get; set; }
    }
}