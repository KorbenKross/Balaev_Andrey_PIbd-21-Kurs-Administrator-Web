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
    public class Order
    {
        [Key]
        [DataMember]
        public int order_id { get; set; }

        [DataMember]
        public int AdministratorId { get; set; }

        [DataMember]
        public int SupplierId { get; set; }

        public virtual Administrator administrator { get; set; }

        public virtual Supplier supplier { get; set; }

        [DataMember]
        [Required]
        public DateTime order_date { get; set; }

        [DataMember]
        [Required]
        public int Sum { get; set; }

        [DataMember]
        [Required]
        public int CarId { get; set; }

        [DataMember]
        [Required]
        public int Count { get; set; }

        [DataMember]
        public OrderCondition order_status { get; set; }

        [ForeignKey("car_id")]
        public virtual List<OrderCar> OrderCar { get; set; }
    }
}
