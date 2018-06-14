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
    public class CarKit
    {
        [Key]
        [DataMember]
        public int carkit_id { get; set; }

        [DataMember]
        public string kit_name { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public int car_id { get; set; }

        [DataMember]
        public int DetailId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Car Car { get; set; }

        public virtual CarKitDetail CarKitDetail { get; set; }

        [ForeignKey("carkit_id")]
        public virtual List<OrderCar> CarKitOrders { get; set; }

        [ForeignKey("CarKitId")]
        public virtual List<CarKitDetail> CarKitDetails { get; set; }

    }
}
