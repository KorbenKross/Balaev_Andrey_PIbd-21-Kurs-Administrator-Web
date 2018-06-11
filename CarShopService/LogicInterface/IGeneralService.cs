using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarShopService.LogicInterface;
using CarShopService.ConnectingModel;
using CarShopService.UserViewModel;
using CarShopService;
using Kurs;

namespace CarShopService.LogicInterface
{
    public interface IGeneralService
    {
        List<OrderViewModel> GetList();

        void CreateOrder(Order model);

        void TakeOrderInWork(Order model);

        void FinishOrder(int id);

        void PayOrder(int id);

        void PutComponentOnStock(StockConnectingModel model);
    }
}
