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
    public class StockDetail
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int DetailId { get; set; }

        [DataMember]
        public int StockId { get; set; }

        [DataMember]
        public int Count { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual Detail Detail { get; set; }
    }
}
