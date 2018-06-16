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
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace Service.RealiseInterfaceBD
{
    public class GeneralService : IGeneralService
    {
        private DBContext context;
        private string reportMessage = "Отчёт по движению: " + "\r\n";

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
                    order_status = rec.order_status.ToString(),
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
                SupplierId = model.Suppliersupplier_id,
                CarKitId = model.CarKitId,
                CarKitName = model.kit_name,
                order_date = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                order_status = OrderCondition.Принят
            });
            context.SaveChanges();
            reportMessage = reportMessage + "Ваш заказ был создан" + "\r\n" + DateTime.Now;
        }

        public void TakeOrderInWork(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.order_id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.order_status = OrderCondition.Выгружен;
            reportMessage = reportMessage + "Ваш заказ был выгружен" + "\r\n" + DateTime.Now;
            context.SaveChanges();
        }

        public void FinishOrder(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.order_id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.order_status = OrderCondition.Готов;
            reportMessage = reportMessage + "Ваш заказ был доставлен" + "\r\n" + DateTime.Now ;
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
            reportMessage = reportMessage + "Ваш заказ оплачен" + "\r\n" + DateTime.Now;
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

        public void SendPdf(string path)
        {
            //открываем файл для работы
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();

            BaseFont baseFont = BaseFont.CreateFont(@"TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
              writer.DirectContent.BeginText();
              writer.DirectContent.SetFontAndSize(baseFont, 12f);
              writer.DirectContent.ShowTextAligned(Element.ALIGN_LEFT, reportMessage, 35, 766, 0);
              writer.DirectContent.EndText();
              doc.Close();
              writer.Close();
        }

        



    }
}
