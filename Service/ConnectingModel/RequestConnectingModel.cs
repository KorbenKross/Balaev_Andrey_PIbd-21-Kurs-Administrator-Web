using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnectingModel
{
    public class RequestConnectingModel
    {
        public int RequestId { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }

        public List<RequestDetailsConnectingModel> RequestDetails { get; set; }
    }
}
