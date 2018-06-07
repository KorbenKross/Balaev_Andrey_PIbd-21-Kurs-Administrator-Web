using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class Detail
    {
        [Key]
        [DataMember]
        public int detail_id { get; set; }

        [DataMember]
        [Required]
        public string DetailName { get; set; }

        [DataMember]
        [Required]
        public string type { get; set; }

        [DataMember]
        [Required]
        public double price { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<Car_kit> Car_kit { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<Stock_Detail> Stock_Detail { get; set; }
    }
}
