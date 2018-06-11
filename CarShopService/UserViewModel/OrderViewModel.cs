using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs;

namespace CarShopService.UserViewModel
{
    public class OrderViewModel
    { 
        public int order_id { get; set; }
        
        public int Administratoradmin_id { get; set; }
        
        public int Suppliersupplier_id { get; set; }

        public DateTime order_date { get; set; }

        public OrderCondition order_status { get; set; }

        public virtual Administrator Administrator { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
