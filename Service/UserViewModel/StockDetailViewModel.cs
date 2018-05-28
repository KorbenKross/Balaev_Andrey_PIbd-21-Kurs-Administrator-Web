using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.UserViewModel
{
    public class StockDetailViewModel
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int DetailId { get; set; }

        public string DetailName { get; set; }

        public int Count { get; set; }
    }
}