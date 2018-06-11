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
    public class DetailSelectionList : IDetailService
    {
        private BaseListSingleton source;

        public DetailSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<DetailViewModel> GetList()
        {
            List<DetailViewModel> result = source.Details
                .Select(rec => new DetailViewModel
                {
                    detail_id = rec.detail_id,
                    detail_name = rec.detail_name
                })
                .ToList();
            return result;
        }

        public DetailViewModel GetElement(int id)
        {
            Detail component = source.Details.FirstOrDefault(rec => rec.detail_id == id);
            if (component != null)
            {
                return new DetailViewModel
                {
                    detail_id = component.detail_id,
                    detail_name = component.detail_name
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(DetailConnectingModel model)
        {
            Detail component = source.Details.FirstOrDefault(rec => rec.detail_name == model.detail_name);
            if (component != null)
            {
                throw new Exception("Уже есть деталь с таким названием");
            }
            int maxId = source.Details.Count > 0 ? source.Details.Max(rec => rec.detail_id) : 0;
            source.Details.Add(new Detail
            {
                detail_id = maxId + 1,
                detail_name = model.detail_name
            });
        }

        public void UpdElement(DetailConnectingModel model)
        {
            Detail component = source.Details.FirstOrDefault(rec =>
                                        rec.detail_name == model.detail_name && rec.detail_id != model.detail_id);
            if (component != null)
            {
                throw new Exception("Уже есть деталь с таким названием");
            }
            component = source.Details.FirstOrDefault(rec => rec.detail_id == model.detail_id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.detail_name = model.detail_name;
        }

        public void DelElement(int id)
        {
            Detail component = source.Details.FirstOrDefault(rec => rec.detail_id == id);
            if (component != null)
            {
                source.Details.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
