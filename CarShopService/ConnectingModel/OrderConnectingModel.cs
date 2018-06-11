using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShopService.ConnectingModel
{
    public class OrderConnectingModel
    {
        public int order_id { get; set; }

        public DateTime order_date { get; set; }

        public string order_status { get; set; }
    }
}
