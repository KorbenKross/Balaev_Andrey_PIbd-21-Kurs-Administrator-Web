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
    public class Car
    {
        [Key]
        [DataMember]
        public int car_id { get; set; }

        [DataMember]
        [Required]
        public string brand { get; set; }

        [DataMember]
        [Required]
        public int? cost { get; set; }

        [ForeignKey("car_id")]
        public virtual List<OrderCar> CarOrders { get; set; }

        [ForeignKey("car_id")]
        public virtual List<Car_kit> Car_kit1 { get; set; }


    }
}
