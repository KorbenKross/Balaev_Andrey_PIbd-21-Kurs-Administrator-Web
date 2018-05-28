using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.LogicInterface;
using Service.ConnectingModel;
using Service;
using Service.UserViewModel;
using Service.ConnectingModel;

namespace Service.LogicInterface
{
    public interface IReportService
    {
        List<OrderViewModel> GetOrdersList(ReportConnectingModel model);

        List<RequestViewModel> GetRequestsList(ReportConnectingModel model);

        void SaveReportToFile(ReportConnectingModel model);
    }
}
