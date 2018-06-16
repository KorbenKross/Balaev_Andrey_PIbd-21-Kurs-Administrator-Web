using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.UserViewModel;
using Service.ConnectingModel;

namespace Service.LogicInterface
{
    public interface IGeneralService
    {
        List<OrderViewModel> GetList();

        void CreateOrder(OrderConnectingModel model);

        void TakeOrderInWork(int id);

        void FinishOrder(int id);

        void PayOrder(int id);

        void PutComponentOnStock(StockDetailConnectingModel model);

        void SendPdf(string mail);
    }
}
