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
    public interface IAdministratorService
    {
        List<AdministratorViewModel> GetList();

        AdministratorViewModel GetElement(int id);

        void AddElement(AdministratorConnectingModel model);

        void UpdElement(AdministratorConnectingModel model);

        void DelElement(int id);
    }
}
