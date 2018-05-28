using Service.ConnectingModel;
using Service.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LogicInterface
{
    public interface IRequestService
    {
        List<RequestViewModel> GetList();

        RequestViewModel GetElement(int id);

        void AddElement(RequestConnectingModel model);

        void SaveRequestExcelFile(RequestViewModel model, string filename);

        void SaveRequestWordFile(RequestViewModel model, string filename);
    }
}
