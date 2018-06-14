using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;

namespace Service.LogicInterface
{
    public interface IAdministratorService
    {
        List<AdministratorViewModel> GetList();

        AdministratorViewModel GetElement(int id);

        void AddElement(AdministratorConnectingModel model);

        void UpdElement(AdministratorConnectingModel model);

        void DelElement(int id);

        void LogIn(AdministratorConnectingModel model);
    }
}
