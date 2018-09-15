namespace ShoesProjectModels.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permisson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permisson()
        {
            Admins = new HashSet<Admin>();
        }

        public int PermissonId { get; set; }

        [StringLength(256)]
        public string PermissonName { get; set; }

        [Column(TypeName = "ntext")]
        public string PermissonDescription { get; set; }

        [Required]
        [StringLength(64)]
        public string BusinessId { get; set; }

        public virtual Business Business { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin> Admins { get; set; }
    }
}
