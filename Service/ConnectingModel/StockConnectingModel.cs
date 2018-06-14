using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnectingModel
{
    public class StockConnectingModel
    {
        public int StockId { get; set; }

        public string StockName { get; set; }

        public List<StockDetail> Stock_Details { get; set; }
    }
}
