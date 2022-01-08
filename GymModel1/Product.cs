namespace GymModel1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int IdAntrenori { get; set; }

        [StringLength(50)]
        public string Nume1 { get; set; }

        [StringLength(50)]
        public string Prenume1 { get; set; }

        [StringLength(50)]
        public string Ziua { get; set; }

        [StringLength(50)]
        public string Ora { get; set; }

        [StringLength(50)]
        public string IdCs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
