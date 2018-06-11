using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs.LogicInterface;
using Kurs.ConnectingModel;
using Kurs.UserViewModel;
using Kurs;

namespace Kurs.LogicInterface
{
    public interface IGeneralService
    {
        List<OrderViewModel> GetList();

        void CreateOrder(OrderConnectingModel model);

        void TakeOrderInWork(OrderConnectingModel model);

        void FinishOrder(int id);

        void PayOrder(int id);

        void PutComponentOnStock(StockDetailConnectingModel model);
    }
}
