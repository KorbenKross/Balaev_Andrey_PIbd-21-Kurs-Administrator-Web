namespace Kurs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Detail")]
    public partial class Detail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Detail()
        {
            Stock = new HashSet<Stock>();
        }

        [Key]
        public int detail_id { get; set; }

        [StringLength(25)]
        public string detail_name { get; set; }

        [StringLength(25)]
        public string type { get; set; }

        [Column("Car-kitcarkit_id")]
        public int Car_kitcarkit_id { get; set; }

        public virtual Car_kit Car_kit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stock { get; set; }
    }
}
