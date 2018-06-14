using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnectingModel
{
    public class CarConnectingModel
    {
        public int car_id { get; set; }

        public string brand { get; set; }

        public int? cost { get; set; }

        public int? car_kit { get; set; }

        public virtual List<CarKitConnectingModel> Car_kit1 { get; set; }
    }
}
