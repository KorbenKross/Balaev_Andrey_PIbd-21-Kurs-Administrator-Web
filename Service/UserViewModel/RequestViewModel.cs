using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserViewModel
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }

        public double Price { get; set; }

        public string Date { get; set; }

        public List<RequestDetailViewModel> RequestDetails { get; set; }
    }
}
