using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs;

namespace Kurs.UserViewModel
{
    public class DetailViewModel
    {
        public int detail_id { get; set; }

        public string detail_name { get; set; }

        public string type { get; set; }

        public int Car_kitcarkit_id { get; set; }

        public virtual Car_kit Car_kit { get; set; }

        public virtual ICollection<Stock> Stock { get; set; }
    }
}
