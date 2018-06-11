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
    public interface IStockService
    {
        List<StockViewModel> GetList();

        StockViewModel GetElement(int id);

        void AddElement(StockConnectingModel model);

        void UpdElement(StockConnectingModel model);

        void DelElement(int id);
    }
}
