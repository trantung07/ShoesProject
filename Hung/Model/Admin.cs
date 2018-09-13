namespace Hung.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            Permissons = new HashSet<Permisson>();
        }

        public int AdminId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string AdminPassword { get; set; }

        [Required]
        [StringLength(14)]
        public string AdminUsername { get; set; }

        public bool AdminIsDeleted { get; set; }

        public bool AdminIsDisabled { get; set; }

        public bool IsSuper { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permisson> Permissons { get; set; }
    }
}
