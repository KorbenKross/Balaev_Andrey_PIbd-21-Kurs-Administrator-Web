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
    public class StockSelectionList : IStockService
    {
        private BaseListSingleton source;

        public StockSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = source.Stocks
                .Select(rec => new StockViewModel
                {
                    stock_id = rec.stock_id,
                    capacity = rec.capacity,
                    StockDetail = source.Stock_Details
                            .Where(recPC => recPC.Stockstock_id == rec.stock_id)
                            .Select(recPC => new StockDetailViewModel
                            {
                                stock_detail_id = recPC.stock_detail_id,
                                Stockstock_id = recPC.Stockstock_id,
                                Detaildetail_id = recPC.Detaildetail_id,
                                detail_name = source.Details
                                    .FirstOrDefault(recC => recC.detail_id == recPC.Detaildetail_id)?.detail_name,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }


        public StockViewModel GetElement(int id)
        {
            Stock component = source.Stocks.FirstOrDefault(rec => rec.stock_id == id);
            if (component != null)
            {
                return new StockViewModel
                {
                    stock_id = component.stock_id,
                    capacity = component.capacity,
                    StockDetail = source.Stock_Details
                            .Where(recPC => recPC.Stockstock_id == component.stock_id)
                            .Select(recPC => new StockDetailViewModel
                            {
                                stock_detail_id = recPC.stock_detail_id,
                                Detaildetail_id = recPC.Detaildetail_id,
                                Stockstock_id = recPC.Stockstock_id,
                                detail_name = source.Details
                                    .FirstOrDefault(recC => recC.detail_id == recPC.Stockstock_id)?.detail_name,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }


        public void AddElement(StockConnectingModel model)
        {
            Stock compinent = source.Stocks.FirstOrDefault(rec => rec.stock_id == model.stock_id);
            if (compinent != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Stocks.Count > 0 ? source.Stocks.Max(rec => rec.stock_id) : 0;
            source.Stocks.Add(new Stock
            {
                stock_id = maxId + 1,
                capacity = model.capacity
            });
        }

        public void UpdElement(StockConnectingModel model)
        {
            Stock component = source.Stocks.FirstOrDefault(rec =>
                                        rec.stock_id != model.stock_id);
            if (component != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            component = source.Stocks.FirstOrDefault(rec => rec.stock_id == model.stock_id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.capacity = model.capacity;
        }

        public void DelElement(int id)
        {
            Stock component = source.Stocks.FirstOrDefault(rec => rec.stock_id == id);
            if (component != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе
                source.Stock_Details.RemoveAll(rec => rec.Stockstock_id == id);
                source.Stocks.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
