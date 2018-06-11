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
    public interface IDetailService
    {
        List<DetailViewModel> GetList();

        DetailViewModel GetElement(int id);

        void AddElement(DetailConnectingModel model);

        void UpdElement(DetailConnectingModel model);

        void DelElement(int id);
    }
}
