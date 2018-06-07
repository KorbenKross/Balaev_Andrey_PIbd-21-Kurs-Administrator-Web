using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.UserViewModel;
using Service.ConnectingModel;

namespace Service.LogicInterface
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
