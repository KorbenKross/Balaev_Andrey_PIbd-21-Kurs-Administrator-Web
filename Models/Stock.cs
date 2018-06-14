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
    public class Stock
    {
        [Key]
        [DataMember]
        public int StockId { get; set; }

        [DataMember]
        [Required]
        public string StockName { get; set; }

        [ForeignKey("StockId")]
        public virtual List<StockDetail> StockDetail { get; set; }
    }
}
