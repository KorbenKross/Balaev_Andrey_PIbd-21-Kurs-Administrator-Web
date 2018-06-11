using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs;

namespace CarShopService.ConnectingModel
{
    public class CarConnectingModel
    {
        public int car_id { get; set; }

        public string brand { get; set; }

        public int? cost { get; set; }

        public int? car_kit { get; set; }

        public int stock { get; set; }

        public virtual List<Administrator> Administrator { get; set; }

        public virtual List<Stock> Stock1 { get; set; }

        public virtual List<Car_kit> Car_kit1 { get; set; }
    }
}
