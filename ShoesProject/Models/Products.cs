using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesProject.Models
{
    public class Products
    {
        public int productId { get; set; }

        public string productName { get; set; }

        public int categoryId { get; set; }

        public string categoryName { get; set; }

        public int instock { get; set; }

        public int brandId { get; set; }

        public string brandName { get; set; }

        public int productPrice { get; set; }

        public string productDescription { get; set; }

        public int? productDiscount { get; set; }

        public Products()
        {

        }

        public Products(int productId, string productName, int categoryId, string categoryName, int instock, int brandId, string brandName, int productPrice, string productDescription, int? productDiscount)
        {
            this.productId = productId;
            this.productName = productName;
            this.categoryId = categoryId;
            this.categoryName = categoryName;
            this.instock = instock;
            this.brandId = brandId;
            this.brandName = brandName;
            this.productPrice = productPrice;
            this.productDescription = productDescription;
            this.productDiscount = productDiscount;
        }
    }
}