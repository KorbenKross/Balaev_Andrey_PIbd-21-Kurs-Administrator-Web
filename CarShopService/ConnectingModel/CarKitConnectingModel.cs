using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShopService.ConnectingModel
{
    public class CarKitConnectingModel
    {
        public int carkit_id { get; set; }

        public string kit_name { get; set; }

        public int? cost { get; set; }

        public DateTime date { get; set; }

        public List<DetailConnectingModel> Detail { get; set; }

    }
}
