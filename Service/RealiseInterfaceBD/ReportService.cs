using System;
using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.util;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Service.RealiseInterfaceBD
{
    public class ReportService : IReportService
    {
        private DBContext context;

        public ReportService(DBContext context)
        {
            this.context = context;
        }

        public List<OrderViewModel> GetOrdersList(ReportConnectingModel model)
        {
            return context.Orders
                .Select(
                    rec => new OrderViewModel
                    {
                        AdministratorName = rec.administrator.name,
                        order_date = rec.order_date.ToString(),
                        order_status = rec.order_status.ToString()
                    }
                )
                .ToList();
        }

        public List<RequestViewModel> GetRequestsList(ReportConnectingModel model)
        {
            return context.Requests
                .Select(rec => new RequestViewModel
                {
                    RequestId = rec.RequestId,
                    Price = rec.Price,
                    Date = rec.Date.ToString()
                    
                })
                .ToList();
        }

        public void SaveReportToFile(ReportConnectingModel model)
        {
            //из ресрусов получаем шрифт для кирилицы
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }

            //открываем файл для работы
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);

            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //вставляем заголовок
            var phraseTitle = new Phrase("Отчеты",
                new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString() +
                                    " по " + model.DateTo.Value.ToShortDateString(),
                                    new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);


            var phraseOrders = new Phrase("Отчет по заказам",
                                    new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phraseOrders)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            PdfPTable table = new PdfPTable(3)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160 });

            //вставляем шапку
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("Дата", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Имя администратора", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Имя поставщика", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Цена", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            var list = GetOrdersList(model);
            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            for (int i = 0; i < list.Count; i++)
            {
                cell = new PdfPCell(new Phrase(list[i].order_date, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].AdministratorName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].AdministratorName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].Sum.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
            }

            var phraseOrdersSum = new Phrase("Итого: " + list.Sum(rec => rec.Sum).ToString(),
                                    new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phraseOrdersSum)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            doc.Add(table);

            //Отчет по заявкам
            var phraseRequests = new Phrase("Отчет по заявкам",
                                    new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phraseRequests)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            //вставляем таблицу, задаем количество столбцов, и ширину колонок
            PdfPTable tableRequests = new PdfPTable(2)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160 });

            //вставляем шапку
            PdfPCell cellRequest = new PdfPCell();
            tableRequests.AddCell(new PdfPCell(new Phrase("Дата", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            tableRequests.AddCell(new PdfPCell(new Phrase("Цена", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            //заполняем таблицу
            var listRequest = GetRequestsList(model);
            for (int i = 3; i < listRequest.Count; i++)
            {
                cellRequest = new PdfPCell(new Phrase(listRequest[i].Date, fontForCells));
                tableRequests.AddCell(cellRequest);
                cellRequest = new PdfPCell(new Phrase(listRequest[i].Price.ToString(), fontForCells));
                tableRequests.AddCell(cellRequest);
            }

            //вставляем итого
            var phraseRequestsSum = new Phrase("Итого: " + listRequest.Sum(rec => rec.Price).ToString(),
                                    new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phraseRequestsSum)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            
            //вставляем таблицу
            doc.Add(tableRequests);

            doc.Close();
        }
    }
}
