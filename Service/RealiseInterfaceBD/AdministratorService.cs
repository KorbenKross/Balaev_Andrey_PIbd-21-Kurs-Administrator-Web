using Models;
using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RealiseInterfaceBD
{
    public class AdministratorService : IAdministratorService
    {
        private DBContext context;

        public AdministratorService(DBContext context)
        {
            this.context = context;
        }

        public List<AdministratorViewModel> GetList()
        {
            List<AdministratorViewModel> result = context.Administrators
                .Select(rec => new AdministratorViewModel
                {
                    admin_id = rec.admin_id,
                    name = rec.name,
                    login = rec.login,
                    password = rec.password, 
                    mail = rec.mail
                })
                .ToList();
            return result;
        }

        public AdministratorViewModel GetElement(int id)
        {
            Administrator element = context.Administrators.FirstOrDefault(rec => rec.admin_id == id);
            if (element != null)
            {
                return new AdministratorViewModel
                {
                    admin_id = element.admin_id,
                    name = element.name,
                };
            }
            throw new Exception("Сотрудник не именем");
        }

        public void AddElement(AdministratorConnectingModel model)
        {
            Administrator element = context.Administrators.FirstOrDefault(rec => rec.name == model.name);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким именем");
            }
            context.Administrators.Add(new Administrator
            {
                name = model.name,
                login = model.login,
                password = model.password,
                mail = model.mail
            });
            context.SaveChanges();
        }

        public void UpdElement(AdministratorConnectingModel model)
        {
            Administrator element = context.Administrators.FirstOrDefault(rec =>
                                    rec.name == model.name && rec.admin_id != model.admin_id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким именем");
            }
            element = context.Administrators.FirstOrDefault(rec => rec.admin_id == model.admin_id);
            if (element == null)
            {
                throw new Exception("Сотрудник не найден");
            }
            element.name = model.name;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Administrator element = context.Administrators.FirstOrDefault(rec => rec.admin_id == id);
            if (element != null)
            {
                context.Administrators.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Сотрудник не найден");
            }
        }

        public void LogIn(AdministratorConnectingModel model)
        {
            Administrator element = context.Administrators.FirstOrDefault(rec =>
                                    rec.login == model.login && rec.password == model.password);
            if (element == null)
            {
                throw new Exception("Ошибка");
            }
        }
    }
}
