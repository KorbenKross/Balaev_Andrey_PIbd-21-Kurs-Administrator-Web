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
    public class SupplierSelectionList : ISupplierService
    {
        private BaseListSingleton source;

        public SupplierSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<SupplierViewModel> GetList()
        {
            List<SupplierViewModel> result = source.Administrators
                .Select(rec => new SupplierViewModel
                {
                    supplier_id = rec.admin_id,
                    name = rec.name
                })
                .ToList();
            return result;
        }

        public SupplierViewModel GetElement(int id)
        {
            Supplier component = source.Suppliers.FirstOrDefault(rec => rec.supplier_id == id);
            if (component != null)
            {
                return new SupplierViewModel
                {
                    supplier_id = component.supplier_id,
                    name = component.name
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(SupplierConnectingModel model)
        {
            Administrator component = source.Administrators.FirstOrDefault(rec => rec.name == model.name);
            if (component != null)
            {
                throw new Exception("Уже есть поставщик с таким логином");
            }
            int maxId = source.Suppliers.Count > 0 ? source.Suppliers.Max(rec => rec.supplier_id) : 0;
            source.Suppliers.Add(new Supplier
            {
                supplier_id = maxId + 1,
                name = model.name
            });
        }

        public void UpdElement(SupplierConnectingModel model)
        {
            Supplier component = source.Suppliers.FirstOrDefault(rec =>
                                    rec.name == model.name && rec.supplier_id != model.supplier_id);
            if (component != null)
            {
                throw new Exception("Уже есть поставщик с таким логином");
            }
            component = source.Suppliers.FirstOrDefault(rec => rec.supplier_id == model.supplier_id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.name = model.name;
        }

        public void DelElement(int id)
        {
            Supplier component = source.Suppliers.FirstOrDefault(rec => rec.supplier_id == id);
            if (component != null)
            {
                source.Suppliers.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
