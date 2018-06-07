using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;

namespace Service.RealiseInterfaceBD
{
    public class StockService : IStockService
    {
        private DBContext context;

        public StockService(DBContext context)
        {
            this.context = context;
        }

        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = context.Stocks
                .Select(rec => new StockViewModel
                {
                    StockId = rec.StockId,
                    StockName = rec.StockName,
                    StockDetail = context.Stocks_Details
                            .Where(recPC => recPC.Id == rec.StockId)
                            .Select(recPC => new StockDetailViewModel
                            {
                                Id = recPC.Id,
                                StockId = recPC.StockId,
                                DetailId = recPC.DetailId,
                                DetailName = recPC.Detail.DetailName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public StockViewModel GetElement(int id)
        {
            Stock stock = context.Stocks.FirstOrDefault(rec => rec.StockId == id);
            if (stock != null)
            {
                return new StockViewModel
                {
                    StockId = stock.StockId,
                    StockName = stock.StockName,
                    StockDetail = context.Stocks_Details
                            .Where(recPC => recPC.StockId == stock.StockId)
                            .Select(recPC => new StockDetailViewModel
                            {
                                Id = recPC.Id,
                                StockId = recPC.StockId,
                                DetailId = recPC.DetailId,
                                DetailName = recPC.Detail.DetailName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Склад не найден");
        }

        public void AddElement(StockConnectingModel model)
        {
            Stock stock = context.Stocks.FirstOrDefault(rec => rec.StockName == model.StockName);
            if (stock != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Stocks.Add(new Stock
            {
                StockName = model.StockName
            });
            context.SaveChanges();
        }

        public void UpdElement(StockConnectingModel model)
        {
            Stock stock = context.Stocks.FirstOrDefault(rec =>
                                        rec.StockName == model.StockName && rec.StockId != model.StockId);
            if (stock != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            stock = context.Stocks.FirstOrDefault(rec => rec.StockId == model.StockId);
            if (stock == null)
            {
                throw new Exception("Склад не найден");
            }
            stock.StockName = model.StockName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Stock stock = context.Stocks.FirstOrDefault(rec => rec.StockId == id);
                    if (stock != null)
                    {
                        // при удалении удаляем все записи о компонентах на удаляемом складе
                        context.Stocks_Details.RemoveRange(
                                            context.Stocks_Details.Where(rec => rec.StockId == id));
                        context.Stocks.Remove(stock);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Склад не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
