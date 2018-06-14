using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserViewModel
{
    public class CarKitDetailViewModel
    {
        public int Id { get; set; }

        public int CarKitId { get; set; }

        public int DetailId { get; set; }

        public string DetailName { get; set; }

        public int Count { get; set; }
    }
}
