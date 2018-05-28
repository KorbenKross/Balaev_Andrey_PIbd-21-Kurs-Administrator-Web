using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserViewModel
{
    public class CarViewModel
    {
        public int car_id { get; set; }

        public string brand { get; set; }

        public int? cost { get; set; }

        public virtual List<CarKitViewModel> Car_kit1 { get; set; }
    }
}
