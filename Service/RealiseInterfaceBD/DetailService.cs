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
    public class DetailService : IDetailService
    {
        private DBContext context;

        public DetailService(DBContext context)
        {
            this.context = context;
        }

        public List<DetailViewModel> GetList()
        {
            List<DetailViewModel> result = context.Details
                .Select(rec => new DetailViewModel
                {
                    detail_id = rec.detail_id,
                    type = rec.type,
                    detail_name = rec.DetailName,
                    count = rec.count
                })
                .ToList();
            return result;
        }

        public DetailViewModel GetElement(int id)
        {
            Detail detail = context.Details.FirstOrDefault(rec => rec.detail_id == id);
            if (detail != null)
            {
                return new DetailViewModel
                {
                    detail_id = detail.detail_id,
                    type = detail.type,
                    count = detail.count,
                    detail_name = detail.DetailName
                };
            }
            throw new Exception("Деталь не найдена");
        }

        public void AddElement(DetailConnectingModel model)
        {
            Detail detail = context.Details.FirstOrDefault(rec => rec.DetailName == model.DetailName);
            if (detail != null)
            {
                throw new Exception("Уже есть деталь с таким названием");
            }
            context.Details.Add(new Detail
            {
                DetailName = model.DetailName,
                type = model.type,
                count = model.Count
            });
            context.SaveChanges();
        }

        public void UpdElement(DetailConnectingModel model)
        {
            Detail detail = context.Details.FirstOrDefault(rec =>
                                        rec.DetailName == model.DetailName && rec.detail_id != model.detail_id);
            if (detail != null)
            {
                throw new Exception("Уже есть деталь с таким названием");
            }
            detail = context.Details.FirstOrDefault(rec => rec.detail_id == model.detail_id);
            if (detail == null)
            {
                throw new Exception("Деталь не найдена");
            }
            detail.DetailName = model.DetailName;
            detail.type = model.type;
            detail.count = model.Count;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Detail element = context.Details.FirstOrDefault(rec => rec.detail_id == id);
            if (element != null)
            {
                context.Details.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
