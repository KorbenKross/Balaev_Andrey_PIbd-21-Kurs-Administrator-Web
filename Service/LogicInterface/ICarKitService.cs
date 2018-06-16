using Service.ConnectingModel;
using Service.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LogicInterface
{
    public interface ICarKitService
    {
        List<CarKitViewModel> GetList();

        CarKitViewModel GetElement(int id);

        void AddElement(CarConnectingModel model, CarKitConnectingModel model1);

        //void UpdElement(CarKitConnectingModel model);

        void DelElement(int id);
    }
}
