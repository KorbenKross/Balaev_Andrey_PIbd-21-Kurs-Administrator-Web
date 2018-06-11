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
    public class CarSelectionList
    {
        private BaseListSingleton source;

        public CarSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        //public List<CarViewModel> GetList()
        //{
        //    List<CarViewModel> result = source.Cars
        //        .Select(rec => new CarViewModel
        //        {
        //            car_id = rec.car_id,
        //            brand = rec.brand,
        //            cost = rec.cost,
        //            Car_kit1 = source.Car_kits
        //                    .Where(recPC => recPC.carkit_id == rec.car_id)
        //                    .Select(recPC => new CarKitViewModel
        //                    {
        //                        carkit_id = recPC.carkit_id,
        //                        Car = recPC.Car,
        //                        ElementId = recPC.ElementId,
        //                        ElementName = source.Elements
        //                            .FirstOrDefault(recC => recC.Id == recPC.carkit_id)?.ElementName,
        //                        cost = recPC.cost
        //                    })
        //                    .ToList()
        //        })
        //        .ToList();
        //    return result;
        //}

        //public CarViewModel GetElement(int id)
        //{
        //    Car component = source.Cars.FirstOrDefault(rec => rec.car_id == id);
        //    if (component != null)
        //    {
        //        return new CarViewModel
        //        {
        //            car_id = component.car_id,
        //            car_kit = component.car_kit,
        //            cost = component.cost,
        //            Car_kit1 = source.Car_kits
        //                    .Where(recPC => recPC.carkit_id == component.car_id)
        //                    .Select(recPC => new CarKitViewModel
        //                    {
        //                        carkit_id = recPC.carkit_id,
        //                        IngredientId = recPC.IngredientId,
        //                        ElementId = recPC.ElementId,
        //                        ElementName = source.Elements
        //                                .FirstOrDefault(recC => recC.Id == recPC.ElementId)?.ElementName,
        //                        Count = recPC.Count
        //                    })
        //                    .ToList()
        //        };
        //    }
        //    throw new Exception("Элемент не найден");
        //}

        //public void AddElement(CarConnectingModel model)
        //{
        //    Car component = source.Cars.FirstOrDefault(rec => rec.brand == model.brand);
        //    if (component != null)
        //    {
        //        throw new Exception("Уже есть изделие с таким названием");
        //    }
        //    int maxId = source.Cars.Count > 0 ? source.Cars.Max(rec => rec.car_id) : 0;
        //    source.Cars.Add(new Car
        //    {
        //        car_id = maxId + 1,
        //        brand = model.brand,
        //        cost = model.cost
        //    });
        //    // компоненты для изделия
        //    int maxPCId = source.Cars.Count > 0 ?
        //                            source.Car_kits.Max(rec => rec.carkit_id) : 0;
        //    // убираем дубли по компонентам
        //    var groupComponents = model.Car_kit
        //                                .GroupBy(rec => rec.ElementId)
        //                                .Select(rec => new
        //                                {
        //                                    ElementId = rec.Key,
        //                                    Count = rec.Sum(r => r.Count)
        //                                });
        //    // добавляем компоненты
        //    foreach (var groupComponent in groupComponents)
        //    {
        //        source.Car_kits.Add(new Car_kit
        //        {
        //            Id = ++maxPCId,
        //            IngredientId = maxId + 1,
        //            ElementId = groupComponent.ElementId,
        //            Count = groupComponent.Count
        //        });
        //    }
        //}

        //public void UpdElement(CarConnectingModel model)
        //{
        //    Car component = source.Cars.FirstOrDefault(rec =>
        //                                rec.brand == model.brand && rec.Id != model.Id);
        //    if (component != null)
        //    {
        //        throw new Exception("Уже есть суши с таким названием");
        //    }
        //    component = source.Cars.FirstOrDefault(rec => rec.Id == model.Id);
        //    if (component == null)
        //    {
        //        throw new Exception("Компонент не найден");
        //    }
        //    component.brand = model.brand;
        //    component.Cost = model.Value;

        //    int maxPCId = source.Car_kits.Count > 0 ? source.Car_kits.Max(rec => rec.Id) : 0;
        //    // обновляем существуюущие компоненты
        //    var compIds = model.Car_kits.Select(rec => rec.ElementId).Distinct();
        //    var updateComponents = source.Car_kits
        //                                    .Where(rec => rec.IngredientId == model.Id &&
        //                                   compIds.Contains(rec.ElementId));
        //    foreach (var updateComponent in updateComponents)
        //    {
        //        updateComponent.Count = model.Car_kit
        //                                        .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
        //    }
        //    source.Car_kits.RemoveAll(rec => rec.IngredientId == model.Id &&
        //                               !compIds.Contains(rec.ElementId));
        //    // новые записи
        //    var groupComponents = model.Car_kits
        //                                .Where(rec => rec.Id == 0)
        //                                .GroupBy(rec => rec.ElementId)
        //                                .Select(rec => new
        //                                {
        //                                    ElementId = rec.Key,
        //                                    Count = rec.Sum(r => r.Count)
        //                                });
        //    foreach (var groupComponent in groupComponents)
        //    {
        //        Car_kit elementPC = source.Car_kits
        //                                .FirstOrDefault(rec => rec.IngredientId == model.Id &&
        //                                                rec.ElementId == groupComponent.ElementId);
        //        if (elementPC != null)
        //        {
        //            elementPC.Count += groupComponent.Count;
        //        }
        //        else
        //        {
        //            source.Car_kits.Add(new Car_kit
        //            {
        //                Id = ++maxPCId,
        //                IngredientId = model.Id,
        //                ElementId = groupComponent.ElementId,
        //                Count = groupComponent.Count
        //            });
        //        }
        //    }
        //}

        //public void DelElement(int id)
        //{
        //    Car component = source.Cars.FirstOrDefault(rec => rec.car_id == id);
        //    if (component != null)
        //    {
        //        // удаяем записи по компонентам при удалении изделия
        //        source.Car_kits.RemoveAll(rec => rec.carkit_id == id);
        //        source.Cars.Remove(component);
        //    }
        //    else
        //    {
        //        throw new Exception("Элемент не найден");
        //    }
        //}
    }
}
