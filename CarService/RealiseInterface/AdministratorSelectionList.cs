using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurs.LogicInterface;
using Kurs.ConnectingModel;
using Kurs.UserViewModel;
using Kurs;

namespace Kurs.RealiseInterface
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
                    name = rec.name,
                    login = rec.login,
                    password = rec.password                   
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
                    name = component.name,
                    login = component.login,
                    password = component.password
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(AdministratorConnectingModel model)
        {
            Administrator component = source.Administrators.FirstOrDefault(rec => rec.login == model.login);
            if (component != null)
            {
                throw new Exception("Уже есть администратор с таким логином");
            }
            int maxId = source.Administrators.Count > 0 ? source.Administrators.Max(rec => rec.admin_id) : 0;
            source.Administrators.Add(new Administrator
            {
                admin_id = maxId + 1,
                name = model.name,
                login = model.login,
                password = model.password,
            });
        }

        public void UpdElement(AdministratorConnectingModel model)
        {
            Administrator component = source.Administrators.FirstOrDefault(rec =>
                                    rec.login == model.login && rec.admin_id != model.admin_id);
            if (component != null)
            {
                throw new Exception("Уже есть администратор с таким логином");
            }
            component = source.Administrators.FirstOrDefault(rec => rec.admin_id == model.admin_id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.login = model.login;
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
