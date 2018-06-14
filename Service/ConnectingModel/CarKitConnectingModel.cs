using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnectingModel
{
    public class CarKitConnectingModel
    {
        public int carkit_id { get; set; }

        public string kit_name { get; set; }

        public int Count { get; set; }

        public int CarId { get; set; }

        public int DetailId { get; set; }

        public List<CarKitDetailConnectingModel> CarKitDetails { get; set; }
    }
}
