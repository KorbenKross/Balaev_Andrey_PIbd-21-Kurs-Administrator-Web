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
    public interface ICarService
    {
        List<CarViewModel> GetList();

        CarViewModel GetElement(int id);

        void AddElement(CarConnectingModel model);

        void UpdElement(CarConnectingModel model);

        void DelElement(int id);
    }
}
