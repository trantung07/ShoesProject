namespace ShoesProjectModels.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductImage
    {
        public int ProductId { get; set; }

        public int ProductImageId { get; set; }

        [Column(TypeName = "text")]
        public string Image { get; set; }

        public virtual Product Product { get; set; }
    }
}
