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

namespace CarShopService.RealiseInterface
{
    public class AdministratorSelectionList : IAdministratorService
    {
        private BaseListSingleton source;

        public AdministratorSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<AdministratorViewModel> GetList()
        {
            List<AdministratorViewModel> result = source.Administrators
                .Select(rec => new AdministratorViewModel
                {
                    admin_id = rec.admin_id,
                    login = rec.login,
                    password = rec.password,
                    name = rec.name
                })
                .ToList();
            return result;
        }

        public AdministratorViewModel GetElement(int id)
        {
            Administrator component = source.Administrators.FirstOrDefault(rec => rec.admin_id == id);
            if (component != null)
            {
                return new AdministratorViewModel
                {
                    admin_id = component.admin_id,
                    login = component.login,
                    password = component.password,
                    name =  component.name                
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(AdministratorConnectingModel model)
        {
            Administrator component = source.Administrators.FirstOrDefault(rec => rec.name == model.name);
            if (component != null)
            {
                throw new Exception("Уже есть администратор с таким именем");
            }
            int maxId = source.Administrators.Count > 0 ? source.Administrators.Max(rec => rec.admin_id) : 0;
            source.Administrators.Add(new Administrator
            {
                admin_id = maxId + 1,
                login = model.login,
                password = model.password,
                name = model.name
            });
        }

        public void UpdElement(AdministratorConnectingModel model)
        {
            Administrator component = source.Administrators.FirstOrDefault(rec =>
                                    rec.name == model.name && rec.admin_id != model.admin_id);
            if (component != null)
            {
                throw new Exception("Уже есть администратор с таким именем");
            }
            component = source.Administrators.FirstOrDefault(rec => rec.admin_id == model.admin_id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.name = model.name;
        }

        public void DelElement(int id)
        {
            Administrator component = source.Administrators.FirstOrDefault(rec => rec.admin_id == id);
            if (component != null)
            {
                source.Administrators.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
