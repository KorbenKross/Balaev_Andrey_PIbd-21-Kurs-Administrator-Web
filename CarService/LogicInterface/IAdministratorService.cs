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
    public interface IAdministratorService
    {
        List<AdministratorViewModel> GetList();

        AdministratorViewModel GetElement(int id);

        void AddElement(AdministratorConnectingModel model);

        void UpdElement(AdministratorConnectingModel model);

        void DelElement(int id);
    }
}
