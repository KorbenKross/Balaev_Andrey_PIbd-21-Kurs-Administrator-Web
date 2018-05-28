using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Service.UserViewModel
{
    public class OrderViewModel
    {
        public int order_id { get; set; }

        public int AdministratorId { get; set; }

        public int SupplierId { get; set; }

        public string AdministratorName { get; set; }

        public string SupplierName { get; set; }

        public string order_date { get; set; }

        public int Sum { get; set; }

        public OrderCondition order_status { get; set; }

    }
}
