using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kurs.UserViewModel
{
    public class StockDetailViewModel
    {
        public int Stockstock_id { get; set; }

        public int Detaildetail_id { get; set; }

        public int stock_detail_id { get; set; }

        public string detail_name { get; set; }

        public int? Count { get; set; }
    }
}