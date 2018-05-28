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
    public class Administrator
    {
        [Key]
        [DataMember]
        public int admin_id { get; set; }

        [DataMember]
        [Required]
        public string name { get; set; }

        [DataMember]
        [Required]
        public string login { get; set; }

        [DataMember]
        [Required]
        public string password { get; set; }

        [ForeignKey("AdministratorId")]
        public virtual List<Order> Orders { get; set; }
    }
}
