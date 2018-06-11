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
    public interface ISupplierService
    {
        List<SupplierViewModel> GetList();

        SupplierViewModel GetElement(int id);

        void AddElement(SupplierConnectingModel model);

        void UpdElement(SupplierConnectingModel model);

        void DelElement(int id);
    }
}
