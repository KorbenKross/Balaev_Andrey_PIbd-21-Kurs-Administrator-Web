using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs;

namespace Kurs.UserViewModel
{
    public class StockViewModel
    {
        public int stock_id { get; set; }

        public int? capacity { get; set; }

        public int supplier { get; set; }

        public virtual ICollection<Car> Car { get; set; }

        public virtual Supplier Supplier1 { get; set; }

        public virtual ICollection<Supplier> Supplier2 { get; set; }

        public virtual ICollection<Detail> Detail { get; set; }

        public List<StockDetailViewModel> StockDetail { get; set; }
    }
}
