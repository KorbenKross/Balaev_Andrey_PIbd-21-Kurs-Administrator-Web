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
    public class GeneralSelectionList : IGeneralService
    {
        private BaseListSingleton source;

        public GeneralSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = source.Orders
                .Select(rec => new OrderViewModel
                {
                    order_id = rec.order_id,
                    Administratoradmin_id = rec.Administratoradmin_id,
                    Suppliersupplier_id = rec.Suppliersupplier_id,
                    order_date = rec.order_date,
                    order_status = rec.order_status,
                    Administrator = source.Administrators
                                    .FirstOrDefault(recC => recC.admin_id == rec.Administratoradmin_id),
                    Supplier = source.Suppliers
                                    .FirstOrDefault(recP => recP.supplier_id == rec.Suppliersupplier_id)
                })
                .ToList();
            return result;
        }

        public void CreateOrder(OrderConnectingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.order_id) : 0;
            source.Orders.Add(new Order
            {
                order_id = maxId + 1,
                Administratoradmin_id = model.order_id,
                Suppliersupplier_id = model.order_id,
                order_date = DateTime.Now,
                Sum = model.Sum,
                order_status = OrderCondition.Принят
            });
        }

        public void TakeOrderInWork(OrderConnectingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.order_id == model.order_id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            var ingredientElements = source.Car_kits.Where(rec => rec.CarId == element.CarId);
            foreach (var ingredientElement in ingredientElements)
            {
                int countOnStocks = source.Car_kits
                                            .Where(rec => rec.DetailId == ingredientElement.DetailId)
                                            .Sum(rec => rec.Count);
                if (countOnStocks < ingredientElement.Count * element.Count)
                {
                    var componentName = source.Details
                                    .FirstOrDefault(rec => rec.detail_id == ingredientElement.DetailId);
                    throw new Exception("Не достаточно компонента " + componentName?.detail_name +
                        " требуется " + ingredientElement.Count + ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var ingredientElement in ingredientElements)
            {
                int countOnStocks = ingredientElement.Count * element.Count;
                var productStorageElements = source.Car_kits
                                            .Where(rec => rec.DetailId == ingredientElement.DetailId);
                foreach (var productStorageElement in productStorageElements)
                {
                    // компонентов на одном слкаде может не хватать
                    if (productStorageElement.Count >= countOnStocks)
                    {
                        productStorageElement.Count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= productStorageElement.Count;
                        productStorageElement.Count = 0;
                    }
                }
            }
            element.Suppliersupplier_id = model.Suppliersupplier_id;
            element.order_date = DateTime.Now;
            element.order_status = OrderCondition.Готовиться;
        }

        public void FinishOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Administrators[i].admin_id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Orders[index].order_status = OrderCondition.Готов;
        }

        public void PayOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Administrators[i].admin_id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Orders[index].order_status = OrderCondition.Оплачен;
        }

        public void PutComponentOnStock(StockDetailConnectingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Stock_Details.Count; ++i)
            {
                if (source.Stock_Details[i].Stockstock_id == model.Stockstock_id &&
                    source.Stock_Details[i].Detaildetail_id == model.Detaildetail_id)
                {
                    source.Stock_Details[i].Count += model.Count;
                    return;
                }
                if (source.Stock_Details[i].stock_detail_id > maxId)
                {
                    maxId = source.Stock_Details[i].stock_detail_id;
                }
            }
            source.Stock_Details.Add(new Stock_Detail
            {
                stock_detail_id = ++maxId,
                Stockstock_id = model.Stockstock_id,
                Detaildetail_id = model.Detaildetail_id,
                Count = model.Count
            });
        }
    }
}
