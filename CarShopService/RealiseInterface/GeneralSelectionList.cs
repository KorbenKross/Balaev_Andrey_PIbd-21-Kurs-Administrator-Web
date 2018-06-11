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
    public class GeneralSelectionList
    {
        private BaseListSingleton source;

        public GeneralSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        //public List<OrderViewModel> GetList()
        //{
        //    List<OrderViewModel> result = source.Orders
        //        .Select(rec => new OrderViewModel
        //        {
        //            order_id = rec.order_id,
        //            Administratoradmin_id = rec.Administratoradmin_id,
        //            IngredientId = rec.IngredientId,
        //            Suppliersupplier_id = rec.Suppliersupplier_id,
        //            DateCreate = rec.DateCreate.ToLongDateString(),
        //            DateCook = rec.DateImplement?.ToLongDateString(),
        //            order_status = rec.order_status.ToString(),
        //            Count = rec.Count,
        //            Sum = rec.Sum,
        //            Administrator = source.Administrators
        //                            .FirstOrDefault(recC => recC.admin_id == rec.Administratoradmin_id)?.BuyerFIO,
        //            IngredientName = source.Ingredients
        //                            .FirstOrDefault(recP => recP.Id == rec.IngredientId)?.IngredientName,
        //            KitchinerName = source.Kitcheners
        //                            .FirstOrDefault(recI => recI.Id == rec.KitchenerId)?.KitchenerFIO
        //        })
        //        .ToList();
        //    return result;
        //}

        //public void CreateOrder(OrderConnectingModel model)
        //{
        //    int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
        //    source.Orders.Add(new Order
        //    {
        //        order_id = maxId + 1,
        //        Administratoradmin_id = model.BuyerId,
        //        IngredientId = model.IngredientId,
        //        DateCreate = DateTime.Now,
        //        Count = model.Count,
        //        Sum = model.Sum,
        //        Status = OrderCondition.Принят
        //    });
        //}

        //public void TakeOrderInWork(OrderConnectingModel model)
        //{
        //    Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
        //    if (element == null)
        //    {
        //        throw new Exception("Элемент не найден");
        //    }
        //    // смотрим по количеству компонентов на складах
        //    var ingredientElements = source.IngredientElements.Where(rec => rec.IngredientId == element.IngredientId);
        //    foreach (var ingredientElement in ingredientElements)
        //    {
        //        int countOnStocks = source.ProductStorageElement
        //                                    .Where(rec => rec.ElementId == ingredientElement.ElementId)
        //                                    .Sum(rec => rec.Count);
        //        if (countOnStocks < ingredientElement.Count * element.Count)
        //        {
        //            var componentName = source.Elements
        //                            .FirstOrDefault(rec => rec.Id == ingredientElement.ElementId);
        //            throw new Exception("Не достаточно компонента " + componentName?.ElementName +
        //                " требуется " + ingredientElement.Count + ", в наличии " + countOnStocks);
        //        }
        //    }
        //    // списываем
        //    foreach (var ingredientElement in ingredientElements)
        //    {
        //        int countOnStocks = ingredientElement.Count * element.Count;
        //        var productStorageElements = source.ProductStorageElement
        //                                    .Where(rec => rec.ElementId == ingredientElement.ElementId);
        //        foreach (var productStorageElement in productStorageElements)
        //        {
        //            // компонентов на одном слкаде может не хватать
        //            if (productStorageElement.Count >= countOnStocks)
        //            {
        //                productStorageElement.Count -= countOnStocks;
        //                break;
        //            }
        //            else
        //            {
        //                countOnStocks -= productStorageElement.Count;
        //                productStorageElement.Count = 0;
        //            }
        //        }
        //    }
        //    element.KitchenerId = model.KitchenerId;
        //    element.DateImplement = DateTime.Now;
        //    element.Status = OrderCondition.Готовиться;
        //}

        //public void FinishOrder(int id)
        //{
        //    int index = -1;
        //    for (int i = 0; i < source.Orders.Count; ++i)
        //    {
        //        if (source.Buyers[i].Id == id)
        //        {
        //            index = i;
        //            break;
        //        }
        //    }
        //    if (index == -1)
        //    {
        //        throw new Exception("Элемент не найден");
        //    }
        //    source.Orders[index].order_status = OrderCondition.Готов;
        //}

        //public void PayOrder(int id)
        //{
        //    int index = -1;
        //    for (int i = 0; i < source.Orders.Count; ++i)
        //    {
        //        if (source.Buyers[i].Id == id)
        //        {
        //            index = i;
        //            break;
        //        }
        //    }
        //    if (index == -1)
        //    {
        //        throw new Exception("Элемент не найден");
        //    }
        //    source.Orders[index].order_status = OrderCondition.Оплачен;
        //}

        //public void PutComponentOnStock(ProductStorageElementConnectingModel model)
        //{
        //    int maxId = 0;
        //    for (int i = 0; i < source.ProductStorageElement.Count; ++i)
        //    {
        //        if (source.ProductStorageElement[i].ProductStorageId == model.ProductStorageId &&
        //            source.ProductStorageElement[i].ElementId == model.ElementId)
        //        {
        //            source.ProductStorageElement[i].Count += model.Count;
        //            return;
        //        }
        //        if (source.ProductStorageElement[i].Id > maxId)
        //        {
        //            maxId = source.ProductStorageElement[i].Id;
        //        }
        //    }
        //    source.ProductStorageElement.Add(new ProductStorageElement
        //    {
        //        Id = ++maxId,
        //        ProductStorageId = model.ProductStorageId,
        //        ElementId = model.ElementId,
        //        Count = model.Count
        //    });
        //}
    }
}
