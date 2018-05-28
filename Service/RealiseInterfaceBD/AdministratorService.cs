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
   public class CustomerServiceBD : IAdministratorService
    {
        private DBContext context;

        public CustomerServiceBD(DBContext context)
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
                    password = rec.password
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
            throw new Exception("Элемент не найден");
        }

        public void AddElement(AdministratorConnectingModel model)
        {
            Administrator element = context.Administrators.FirstOrDefault(rec => rec.name == model.name);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Administrators.Add(new Administrator
            {
                name = model.name
            });
            context.SaveChanges();
        }

        public void UpdElement(AdministratorConnectingModel model)
        {
            Administrator element = context.Administrators.FirstOrDefault(rec =>
                                    rec.name == model.name && rec.admin_id != model.admin_id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Administrators.FirstOrDefault(rec => rec.admin_id == model.admin_id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
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
                throw new Exception("Элемент не найден");
            }
        }
    }
}
