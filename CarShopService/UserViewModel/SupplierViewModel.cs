using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs;

namespace CarShopService.UserViewModel
{
    public class SupplierViewModel
    {
        public int supplier_id { get; set; }

        public string name { get; set; }

        public int Stockstock_id { get; set; }

        public virtual ICollection<Car_kit> Car_kit { get; set; }

        public virtual ICollection<Order> Order { get; set; }

        public virtual ICollection<Stock> Stock { get; set; }

        public virtual Stock Stock1 { get; set; }
    }
}
