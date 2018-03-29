namespace Kurs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Car")]
    public partial class Car
    {
        [Key]
        public int car_id { get; set; }

        [StringLength(30)]
        public string brand { get; set; }

        public int? cost { get; set; }

        public int? car_kit { get; set; }

        public int stock { get; set; }

        public int Administratoradmin_id { get; set; }

        public virtual Administrator Administrator { get; set; }

        public virtual Stock Stock1 { get; set; }

        public virtual Car_kit Car_kit1 { get; set; }
    }
}
