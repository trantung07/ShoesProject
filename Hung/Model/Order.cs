namespace ShoesProjectModels.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int? VoucherId { get; set; }

        [Column(TypeName = "text")]
        public string Address { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int? ProgressStatus { get; set; }

        public bool? OrderStatus { get; set; }

        public virtual User User { get; set; }

        public virtual Voucher Voucher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
