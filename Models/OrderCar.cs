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
    public class OrderCar
    {
        [Key]
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public int cost { get; set; }

        [DataMember]
        public int count { get; set; }

        [DataMember]
        public int carkit_id { get; set; }

        [DataMember]
        public string kit_name { get; set; }

        [DataMember]
        public int order_id { get; set; }

        public virtual Order Order { get; set; }

        public virtual CarKit CarKit { get; set; }
    }
}
