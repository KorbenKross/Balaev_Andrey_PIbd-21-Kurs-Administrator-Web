using Models;
using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.RealiseInterfaceBD
{
    public class GeneralService : IGeneralService
    {
        private DBContext context;

        public GeneralService(DBContext context)
        {
            this.context = context;
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders
                .Select(rec => new OrderViewModel
                {
                    order_id = rec.order_id,
                    AdministratorId = rec.AdministratorId,
                    CarKitId = rec.CarKitId,
                    order_date = SqlFunctions.DateName("dd", rec.order_date) + " " +
                                SqlFunctions.DateName("mm", rec.order_date) + " " +
                                SqlFunctions.DateName("yyyy", rec.order_date),
                    order_status = rec.order_status,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    AdministratorName = rec.administrator.name,
                    kit_name = rec.CarKit.kit_name,
                })
                .ToList();
            return result;
        }

        public void CreateOrder(OrderConnectingModel model)
        {
            context.Orders.Add(new Order
            {
                AdministratorId = model.Administratoradmin_id,
                CarKitId = model.CarKitId,
                order_date = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                order_status = OrderCondition.Принят
            });
            context.SaveChanges();
        }

        public void TakeOrderInWork(OrderConnectingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Order element = context.Orders.FirstOrDefault(rec => rec.order_id == model.order_id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var productComponents = context.CarKitDetails
                                                .Include(rec => rec.Detail)
                                                .Where(rec => rec.CarKitId == element.CarKitId);
                    // списываем
                    foreach (var productComponent in productComponents)
                    {
                        int countOnStocks = productComponent.Count * element.Count;
                        var stockComponents = context.Stocks_Details
                                                    .Where(rec => rec.DetailId == productComponent.DetailId);
                        foreach (var stockComponent in stockComponents)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockComponent.Count >= countOnStocks)
                            {
                                stockComponent.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockComponent.Count;
                                stockComponent.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                productComponent.Detail.DetailName + " требуется " +
                                productComponent.Count + ", не хватает " + countOnStocks);
                        }
                    }
                    element.order_date = DateTime.Now;
                    element.order_status = OrderCondition.Готовиться;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishOrder(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.order_id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.order_status = OrderCondition.Готов;
            context.SaveChanges();
        }

        public void PayOrder(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.order_id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.order_status = OrderCondition.Оплачен;
            context.SaveChanges();
        }

        public void PutComponentOnStock(StockDetailConnectingModel model)
        {
            StockDetail element = context.Stocks_Details
                                                .FirstOrDefault(rec => rec.StockId == model.StockId &&
                                                                    rec.DetailId == model.DetailId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.Stocks_Details.Add(new StockDetail
                {
                    StockId = model.StockId,
                    DetailId = model.DetailId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
