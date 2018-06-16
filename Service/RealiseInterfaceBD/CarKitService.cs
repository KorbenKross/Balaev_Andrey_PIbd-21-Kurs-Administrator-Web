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
    public class CarKitService : ICarKitService
    {
        private DBContext context;

        public CarKitService(DBContext context)
        {
            this.context = context;
        }

        public List<CarKitViewModel> GetList()
        {
            List<CarKitViewModel> result = context.Car_kits
                .Select(rec => new CarKitViewModel
                {
                    carkit_id = rec.carkit_id,
                    kit_name = rec.kit_name,
                    Count = rec.Count,
                    Details = context.Details
                            .Where(recPC => recPC.detail_id == rec.DetailId)
                            .Select(recPC => new DetailViewModel
                            {
                                detail_id = recPC.detail_id,
                                detail_name = recPC.DetailName,
                                count = recPC.count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public CarKitViewModel GetElement(int id)
        {
            CarKit element = context.Car_kits.FirstOrDefault(rec => rec.carkit_id == id);
            if (element != null)
            {
                return new CarKitViewModel
               {
                    carkit_id = element.carkit_id,
                    kit_name = element.kit_name,
                    Count = element.Count,
                    Details = context.Details
                            .Where(recPC => recPC.detail_id == element.DetailId)
                            .Select(recPC => new DetailViewModel
                            {
                                detail_id = recPC.detail_id,
                                detail_name = recPC.DetailName,
                                count = recPC.count,
                                type = recPC.type
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CarConnectingModel model, CarKitConnectingModel model1)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
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
                                kit_name = model1.kit_name,
                                car_id = model1.CarId,
                                DetailId = groupProduct.ProductId,
                                Count = groupProduct.Count
                            }
                        );
                        context.SaveChanges();
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        //public void UpdElement(CarKitConnectingModel model)
        //{
        //    using (var transaction = context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            CarKit element = context.Car_kits.FirstOrDefault(rec =>
        //                                rec.kit_name == model.kit_name && rec.carkit_id != model.carkit_id);
        //            if (element != null)
        //            {
        //                throw new Exception("Уже есть изделие с таким названием");
        //            }
        //            element = context.Car_kits.FirstOrDefault(rec => rec.carkit_id == model.carkit_id);
        //            if (element == null)
        //            {
        //                throw new Exception("Элемент не найден");
        //            }
        //            element.kit_name = model.kit_name;
        //            element.Count = model.Count;
        //            context.SaveChanges();

        //            // обновляем существуюущие компоненты
        //            var compIds = model.CarKitDetails.Select(rec => rec.DetailId).Distinct();
        //            var updateComponents = context.CarKitDetails
        //                                            .Where(rec => rec.CarKitId == model.carkit_id &&
        //                                                compIds.Contains(rec.DetailId));
        //            foreach (var updateComponent in updateComponents)
        //            {
        //                updateComponent.Count = model.CarKitDetails
        //                                                .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
        //            }
        //            context.SaveChanges();
        //            context.CarKitDetails.RemoveRange(
        //                                context.CarKitDetails.Where(rec => rec.CarKitId == model.carkit_id &&
        //                                                                    !compIds.Contains(rec.DetailId)));
        //            context.SaveChanges();
        //            // новые записи
        //            var groupComponents = model.CarKitDetails
        //                                        .Where(rec => rec.Id == 0)
        //                                        .GroupBy(rec => rec.DetailId)
        //                                        .Select(rec => new
        //                                        {
        //                                            DetailId = rec.Key,
        //                                            Count = rec.Sum(r => r.Count)
        //                                        });
        //            foreach (var groupComponent in groupComponents)
        //            {
        //                CarKitDetail elementPC = context.CarKitDetails
        //                                        .FirstOrDefault(rec => rec.CarKitId == model.carkit_id &&
        //                                                        rec.DetailId == groupComponent.DetailId);
        //                if (elementPC != null)
        //                {
        //                    elementPC.Count += groupComponent.Count;
        //                    context.SaveChanges();
        //                }
        //                else
        //                {
        //                    context.CarKitDetails.Add(new CarKitDetail
        //                    {
        //                        CarKitId = model.carkit_id,
        //                        DetailId = groupComponent.DetailId,
        //                        Count = groupComponent.Count
        //                    });
        //                    context.SaveChanges();
        //                }
        //            }
        //            transaction.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }
        //}

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    CarKit element = context.Car_kits.FirstOrDefault(rec => rec.carkit_id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.Car_kits.RemoveRange(
                                            context.Car_kits.Where(rec => rec.carkit_id == id));
                        context.Car_kits.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
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
