using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.ConnectingModel
{
    public class StockDetailConnectingModel
    {
        public int Id { get; set; }
        
        public int DetailId { get; set; }
        
        public int StockId { get; set; }

        public int Count { get; set; }
    }
}