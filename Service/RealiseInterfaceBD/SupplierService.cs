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
   public class SupplierService : ISupplierService
    {
        private DBContext context;

        public SupplierService(DBContext context)
        {
            this.context = context;
        }

        public List<SupplierViewModel> GetList()
        {
            List<SupplierViewModel> result = context.Suppliers
                .Select(rec => new SupplierViewModel
                {
                    supplier_id = rec.supplier_id,
                    name = rec.name
                })
                .ToList();
            return result;
        }

        public SupplierViewModel GetElement(int id)
        {
            Supplier element = context.Suppliers.FirstOrDefault(rec => rec.supplier_id == id);
            if (element != null)
            {
                return new SupplierViewModel
                {
                    supplier_id = element.supplier_id,
                    name = element.name
                };
            }
            throw new Exception("Сотрудник не найден");
        }

        public void AddElement(SupplierConnectingModel model)
        {
            Supplier element = context.Suppliers.FirstOrDefault(rec => rec.name == model.name);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.Suppliers.Add(new Supplier
            {
                name = model.name,
                login = model.login,
                password = model.password
            });
            context.SaveChanges();
        }

        public void UpdElement(SupplierConnectingModel model)
        {
            Supplier element = context.Suppliers.FirstOrDefault(rec =>
                                        rec.name == model.name && rec.supplier_id != model.supplier_id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = context.Suppliers.FirstOrDefault(rec => rec.supplier_id == model.supplier_id);
            if (element == null)
            {
                throw new Exception("Сотрудник не найден");
            }
            element.name = model.name;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Supplier element = context.Suppliers.FirstOrDefault(rec => rec.supplier_id == id);
            if (element != null)
            {
                context.Suppliers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Сотрудник не найден");
            }
        }

        public void LogIn(SupplierConnectingModel model)
        {
            Supplier element = context.Suppliers.FirstOrDefault(rec =>
                                    rec.login == model.login && rec.password == model.password);
            if (element == null)
            {
                throw new Exception("Ошибка");
            }
        }
    }
}
