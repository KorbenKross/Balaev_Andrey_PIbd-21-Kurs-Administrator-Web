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
        public int count { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<CarKit> CarKit { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<StockDetail> StockDetails { get; set; }
    }
}
