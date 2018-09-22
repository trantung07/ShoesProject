using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoesProject.Models
{
	public class Products
	{
        public int id { get; set; }

        public string name { get; set; }

        public int cateId { get; set; }

        public string cateName { get; set; }

        public int instock { get; set; }

        public int brandId { get; set; }

        public string brandName { get; set; }

        public int price { get; set; }

        public string description { get; set; }

        public int? discount { get; set; }

        public string imagesFeature { get; set; }

        public Products()
        {

        }

        public Products(int id, string name, int cateId, string cateName, int instock, int brandId, string brandName, int price, string description, int? discount, string imagesFeature)
        {
            this.id = id;
            this.name = name;
            this.cateId = cateId;
            this.cateName = cateName;
            this.instock = instock;
            this.brandId = brandId;
            this.brandName = brandName;
            this.price = price;
            this.description = description;
            this.discount = discount;
            this.imagesFeature = imagesFeature;
        }
    }
}