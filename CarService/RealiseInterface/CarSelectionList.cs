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
    public class CarSelectionList : ICarService
    {
        private BaseListSingleton source;

        public CarSelectionList()
        {
            source = BaseListSingleton.GetInstance();
        }

        public List<CarViewModel> GetList()
        {
            List<CarViewModel> result = source.Cars
                .Select(rec => new CarViewModel
                {
                    car_id = rec.car_id,
                    brand = rec.brand,
                    cost = rec.cost,
                    Car_kit1 = source.Car_kits
                            .Where(recPC => recPC.carkit_id == rec.car_id)
                            .Select(recPC => new CarKitViewModel
                            {
                                carkit_id = recPC.carkit_id,
                                carId = recPC.carkit_id,
                                DetailName = source.Details
                                    .FirstOrDefault(recC => recC.detail_id == recPC.DetailId)?.detail_name,
                                cost = recPC.cost
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public CarViewModel GetElement(int id)
        {
            Car component = source.Cars.FirstOrDefault(rec => rec.car_id == id);
            if (component != null)
            {
                return new CarViewModel
                {
                    car_id = component.car_id,
                    brand = component.brand,
                    cost = component.cost,
                    Car_kit1 = source.Car_kits
                            .Where(recPC => recPC.Car.ElementAt(id).car_id == component.car_id)
                            .Select(recPC => new CarKitViewModel
                            {
                                carkit_id = recPC.carkit_id,
                                carId = recPC.Car.ElementAt(id).car_id,
                                date = recPC.date,
                                DetailId = recPC.DetailId,
                                DetailName = source.Details
                                    .FirstOrDefault(recC => recC.detail_id == recPC.DetailId)?.detail_name,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CarConnectingModel model)
        {
            Car component = source.Cars.FirstOrDefault(rec => rec.brand == model.brand);
            if (component != null)
            {
                throw new Exception("Уже есть автомобиль с таким названием");
            }
            int maxId = source.Cars.Count > 0 ? source.Cars.Max(rec => rec.car_id) : 0;
            source.Cars.Add(new Car
            {
                car_id = maxId + 1,
                brand = model.brand,
                cost = model.cost
            });
            // компоненты для изделия
            int maxPCId = source.Car_kits.Count > 0 ?
                                    source.Car_kits.Max(rec => rec.carkit_id) : 0;
            // убираем дубли по компонентам
            var groupComponents = model.Car_kit1
                                        .GroupBy(rec => rec.DetailId)
                                        .Select(rec => new
                                        {
                                            ElementId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            // добавляем компоненты
            foreach (var groupComponent in groupComponents)
            {
                source.Car_kits.Add(new Car_kit
                {
                    carkit_id = ++maxPCId,
                    CarId = maxId + 1,
                    DetailId = groupComponent.ElementId,
                    Count = groupComponent.Count
                });
            }
        }

        public void UpdElement(CarConnectingModel model)
        {
            Car component = source.Cars.FirstOrDefault(rec =>
                                        rec.brand == model.brand && rec.car_id != model.car_id);
            if (component != null)
            {
                throw new Exception("Уже есть автомобиль с таким названием");
            }
            component = source.Cars.FirstOrDefault(rec => rec.car_id == model.car_id);
            if (component == null)
            {
                throw new Exception("Компонент не найден");
            }
            component.brand = model.brand;
            component.cost = model.cost;

            int maxPCId = source.Car_kits.Count > 0 ? source.Car_kits.Max(rec => rec.carkit_id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.Car_kit1.Select(rec => rec.DetailId).Distinct();
            var updateComponents = source.Car_kits
                                            .Where(rec => rec.CarId == model.car_id &&
                                           compIds.Contains(rec.DetailId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count = model.Car_kit1
                                                .FirstOrDefault(rec => rec.carkit_id == updateComponent.carkit_id).Count;
            }
            source.Car_kits.RemoveAll(rec => rec.CarId == model.car_id &&
                                       !compIds.Contains(rec.DetailId));
            // новые записи
            var groupComponents = model.Car_kit1
                                        .Where(rec => rec.carkit_id == 0)
                                        .GroupBy(rec => rec.DetailId)
                                        .Select(rec => new
                                        {
                                            ElementId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            foreach (var groupComponent in groupComponents)
            {
                Car_kit elementPC = source.Car_kits
                                        .FirstOrDefault(rec => rec.CarId == model.car_id &&
                                                        rec.DetailId == groupComponent.ElementId);
                if (elementPC != null)
                {
                    elementPC.Count += groupComponent.Count;
                }
                else
                {
                    source.Car_kits.Add(new Car_kit
                    {
                        carkit_id = ++maxPCId,
                        CarId = model.car_id,
                        DetailId = groupComponent.ElementId,
                        Count = groupComponent.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Car component = source.Cars.FirstOrDefault(rec => rec.car_id == id);
            if (component != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.Car_kits.RemoveAll(rec => rec.Car.ElementAt(id).car_id == id);
                source.Cars.Remove(component);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
