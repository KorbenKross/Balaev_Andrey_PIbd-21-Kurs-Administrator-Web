using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserViewModel
{
    public class StocksLoadViewModel
    {
        public string ResourceName { get; set; }
        
        public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Ingridients { get; set; }
    }
}
