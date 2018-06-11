using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs;

namespace CarShopService.UserViewModel
{
    public class CarViewModel
    {
        public int car_id { get; set; }

        public string brand { get; set; }

        public int? cost { get; set; }

        public int? car_kit { get; set; }

        public int stock { get; set; }

        public int Administratoradmin_id { get; set; }

        public virtual Administrator Administrator { get; set; }

        public virtual Stock Stock1 { get; set; }

        public virtual Car_kit Car_kit1 { get; set; }
    }
}
