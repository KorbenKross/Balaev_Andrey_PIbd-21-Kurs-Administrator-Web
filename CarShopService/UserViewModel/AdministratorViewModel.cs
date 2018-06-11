using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs;

namespace CarShopService.UserViewModel
{
    public class AdministratorViewModel
    {
        public int admin_id { get; set; }

        public string name { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public virtual ICollection<Car> Car { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
