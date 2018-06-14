using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnectingModel
{
    public class OrderConnectingModel
    {
        public int order_id { get; set; }
        
        public int Administratoradmin_id { get; set; }

        public int Suppliersupplier_id { get; set; }

        public int CarKitId { get; set; }

        public DateTime order_date { get; set; }

        public int Sum { get; set; }

        public int CarId { get; set; }

        public int Count { get; set; }

        public OrderCondition order_status { get; set; }

        public virtual Administrator Administrator { get; set; }

        public virtual Supplier Supplier { get; set; }

    }
}
