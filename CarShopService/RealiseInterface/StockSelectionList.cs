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
    public class StockSelectionList
    {
        private BaseListSingleton source;

        public StockSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        //public List<StockViewModel> GetList()
        //{
        //    List<StockViewModel> result = source.Stocks
        //        .Select(rec => new StockViewModel
        //        {
        //            stock_id = rec.stock_id,
        //            capacity = rec.capacity,
        //            ProductStorageElements = source.ProductStorageElement
        //                    .Where(recPC => recPC.ProductStorageId == rec.stock_id)
        //                    .Select(recPC => new StockViewModel
        //                    {
        //                        Id = recPC.Id,
        //                        ProductStorageId = recPC.ProductStorageId,
        //                        ElementId = recPC.ElementId,
        //                        ElementName = source.Elements
        //                            .FirstOrDefault(recC => recC.Id == recPC.ElementId)?.ElementName,
        //                        Count = recPC.Count
        //                    })
        //                    .ToList()
        //        })
        //        .ToList();
        //    return result;
        //}


        //public StockViewModel GetElement(int id)
        //{
        //    Stock component = source.Stocks.FirstOrDefault(rec => rec.stock_id == id);
        //    if (component != null)
        //    {
        //        return new StockViewModel
        //        {
        //            Id = component.Id,
        //            ProductStorageName = component.ProductStorageName,
        //            ProductStorageElements = source.ProductStorageElement
        //                    .Where(recPC => recPC.ProductStorageId == component.Id)
        //                    .Select(recPC => new ProductStorageElementViewModel
        //                    {
        //                        Id = recPC.Id,
        //                        ProductStorageId = recPC.ProductStorageId,
        //                        ElementId = recPC.ElementId,
        //                        ElementName = source.Elements
        //                            .FirstOrDefault(recC => recC.Id == recPC.ElementId)?.ElementName,
        //                        Count = recPC.Count
        //                    })
        //                    .ToList()
        //        };
        //    }
        //    throw new Exception("Элемент не найден");
        //}


        //public void AddElement(StockConnectingModel model)
        //{
        //    Stock compinent = source.Stocks.FirstOrDefault(rec => rec.ProductStorageName == model.StockName);
        //    if (compinent != null)
        //    {
        //        throw new Exception("Уже есть склад с таким названием");
        //    }
        //    int maxId = source.Stocks.Count > 0 ? source.Stocks.Max(rec => rec.Id) : 0;
        //    source.Stocks.Add(new Stock
        //    {
        //        Id = maxId + 1,
        //        ProductStorageName = model.StockName
        //    });
        //}

        //public void UpdElement(StockConnectingModel model)
        //{
        //    Stock component = source.Stocks.FirstOrDefault(rec =>
        //                                rec.ProductStorageName == model.StockName && rec.Id != model.Id);
        //    if (component != null)
        //    {
        //        throw new Exception("Уже есть склад с таким названием");
        //    }
        //    component = source.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
        //    if (component == null)
        //    {
        //        throw new Exception("Элемент не найден");
        //    }
        //    component.ProductStorageName = model.StockName;
        //}

        //public void DelElement(int id)
        //{
        //    Stock component = source.Stocks.FirstOrDefault(rec => rec.Id == id);
        //    if (component != null)
        //    {
        //        // при удалении удаляем все записи о компонентах на удаляемом складе
        //        source.ProductStorageElement.RemoveAll(rec => rec.ProductStorageId == id);
        //        source.Stocks.Remove(component);
        //    }
        //    else
        //    {
        //        throw new Exception("Элемент не найден");
        //    }
        //}
    }
}
