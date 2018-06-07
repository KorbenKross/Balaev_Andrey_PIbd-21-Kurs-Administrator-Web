using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserViewModel
{
    public class StockViewModel
    {
        public int StockId { get; set; }

        public string StockName { get; set; }       

        public List<StockDetailViewModel> StockDetail { get; set; }
    }
}
