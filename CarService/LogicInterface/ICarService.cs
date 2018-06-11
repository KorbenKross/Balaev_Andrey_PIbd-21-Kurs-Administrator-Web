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
    public interface ICarService
    {
        List<CarViewModel> GetList();

        CarViewModel GetElement(int id);

        void AddElement(CarConnectingModel model);

        void UpdElement(CarConnectingModel model);

        void DelElement(int id);
    }
}
