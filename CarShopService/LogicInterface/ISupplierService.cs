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
    public interface ISupplierService
    {
        List<SupplierViewModel> GetList();

        SupplierViewModel GetElement(int id);

        void AddElement(SupplierConnectingModel model);

        void UpdElement(SupplierConnectingModel model);

        void DelElement(int id);
    }
}
