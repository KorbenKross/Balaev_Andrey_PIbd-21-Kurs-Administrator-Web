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
    public class RequestDetails
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public int DetailId { get; set; }

        [DataMember]
        public int RequestId { get; set; }

        public virtual Detail Detail { get; set; }

        public virtual Request Request { get; set; }
    }
}
