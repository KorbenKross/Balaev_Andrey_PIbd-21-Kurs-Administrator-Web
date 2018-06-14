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
    public class CarService : ICarService
    {
        private DBContext context;

        public CarService(DBContext context)
        {
            this.context = context;
        }

        public List<CarViewModel> GetList()
        {
            return context.Cars.Select(
                rec => new CarViewModel
                {
                    car_id = rec.car_id,
                    brand = rec.brand,
                    cost = rec.cost,
                    Car_kit1 = context.Car_kits
                        .Where(recPC => recPC.car_id == rec.car_id)
                        .Select(
                            recPC => new CarKitViewModel
                            {
                                carkit_id = recPC.carkit_id,
                                carId = recPC.car_id,
                                Count = recPC.Count
                            }
                        )
                        .ToList()
                }
            )
            .ToList();
        }

        public CarViewModel GetElement(int id)
        {
            Car car = context.Cars.FirstOrDefault(rec => rec.car_id == id);
            if (car != null)
            {
                return new CarViewModel
                {
                    car_id = car.car_id,
                    brand = car.brand,
                    cost = car.cost,
                    Car_kit1 = context.Car_kits
                        .Where(recDP => recDP.car_id == car.car_id)
                        .Select(
                            recPC => new CarKitViewModel
                            {
                                carkit_id = recPC.carkit_id,
                                carId = recPC.car_id,
                                Count = recPC.Count
                            }
                        )
                        .ToList()
                };
            }
            throw new Exception("Автомобиль не найден");
        }

        public void AddElement(CarConnectingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Car car = context.Cars.FirstOrDefault(rec => rec.brand == model.brand);
                    if (car != null)
                        throw new Exception("Такой автомобиль существует");
                    car = new Car
                    {
                        brand = model.brand,
                        cost = model.cost
                    };
                    context.Cars.Add(car);
                    context.SaveChanges();

                    var groupProducts = model.Car_kit1
                        .GroupBy(rec => rec.DetailId)
                        .Select(rec => new
                        {
                            ProductId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        }
                        );
                    foreach (var groupProduct in groupProducts)
                    {
                        context.Car_kits.Add(
                            new CarKit
                            {
                                car_id = car.car_id,
                                DetailId = groupProduct.ProductId,
                                Count = groupProduct.Count
                            }
                        );
                        context.SaveChanges();
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

        public void UpdElement(CarConnectingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Car car = context.Cars.FirstOrDefault(rec => rec.brand == model.brand && rec.car_id != model.car_id);
                    if (car != null)
                        throw new Exception("Такой автомобиль существует");

                    car = context.Cars.FirstOrDefault(rec => rec.car_id == model.car_id);
                    if (car == null)
                        throw new Exception("Автомобиль не найден");

                    car.brand = model.brand;
                    car.cost = model.cost;
                    context.SaveChanges();

                    var compIds = model.Car_kit1.Select(rec => rec.carkit_id).Distinct();
                    var updateProducts = context.Car_kits.Where(rec => rec.car_id == model.car_id && compIds.Contains(rec.DetailId));
                    foreach (var updateProduct in updateProducts)
                    {
                        updateProduct.Count = model.Car_kit1.FirstOrDefault(rec => rec.carkit_id == updateProduct.carkit_id).Count;
                    }
                    context.SaveChanges();
                    context.Car_kits.RemoveRange(context.Car_kits.Where(rec => rec.car_id == model.car_id && !compIds.Contains(rec.DetailId)));
                    context.SaveChanges();

                    var groupProducts = model.Car_kit1
                        .Where(rec => rec.carkit_id == 0)
                        .GroupBy(rec => rec.DetailId)
                        .Select(rec => new
                        {
                            ProductId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        }
                        );

                    foreach (var groupProduct in groupProducts)
                    {
                        CarKit car_Kit = context.Car_kits.FirstOrDefault(rec => rec.car_id == model.car_id && rec.DetailId == groupProduct.ProductId);
                        if (car_Kit != null)
                        {
                            car_Kit.Count += groupProduct.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.Car_kits.Add(
                                new CarKit
                                {
                                    carkit_id = model.car_id,
                                    DetailId = groupProduct.ProductId,
                                    Count = groupProduct.Count
                                }
                            );
                            context.SaveChanges();
                        }
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

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Car car = context.Cars.FirstOrDefault(rec => rec.car_id == id);
                    if (car != null)
                    {
                        context.Car_kits.RemoveRange(context.Car_kits.Where(rec => rec.car_id == id));
                        context.Cars.Remove(car);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Автомобиль не найден");
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
