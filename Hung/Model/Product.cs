namespace ShoesProjectModels.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
            ProductImages = new HashSet<ProductImage>();
            Colors = new HashSet<Color>();
            Sizes = new HashSet<Size>();
        }

        public int ProductId { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Instock { get; set; }

        public int BrandId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int ProductPrice { get; set; }

        [Column(TypeName = "ntext")]
        public string ProductDescription { get; set; }

        public bool? ProductStatus { get; set; }

        [Range(1, 100)]
        public int? ProductDiscount { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ProductFeatureImage { get; set; }


        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Color> Colors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Size> Sizes { get; set; }
    }
}
