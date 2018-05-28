using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Models;
using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;

namespace Service.RealiseInterfaceBD
{
    public class RequestService : IRequestService
    {
        private DBContext context;

        public RequestService(DBContext context)
        {
            this.context = context;
        }

        public void AddElement(RequestConnectingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Request request = context.Requests.FirstOrDefault(rec => rec.Date == model.Date);
                    if (request != null)
                        throw new Exception("Такой запрос уже существует");

                    request = new Request
                    {
                        Price = model.Price,
                        Date = model.Date
                    };
                    context.Requests.Add(request);
                    context.SaveChanges();

                    var groupDetails = model.RequestDetails
                        .GroupBy(rec => rec.DetailId)
                        .Select(rec => new
                        {
                            ProductId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        }
                        );

                    foreach (var groupDetail in groupDetails)
                    {
                        context.RequestDetailses.Add(
                            new RequestDetails
                            {
                                RequestId = request.RequestId,
                                DetailId = groupDetail.ProductId,
                                Count = groupDetail.Count
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

        public RequestViewModel GetElement(int id)
        {
            Request request = context.Requests.FirstOrDefault(rec => rec.RequestId == id);
            if (request != null)
            {
                return new RequestViewModel
                {
                    RequestId = request.RequestId,
                    Price = request.Price,
                    Date = request.Date.ToString(),
                    RequestDetails = context.RequestDetailses
                            .Where(recPC => recPC.RequestId == request.RequestId)
                            .Select(recPC => new RequestDetailViewModel
                            {
                                Id = recPC.Id,
                                RequestId = recPC.RequestId,
                                DetailId = recPC.DetailId,
                                DetailName = recPC.Detail.DetailName,
                                Count = recPC.Count
                            }
                            )
                            .ToList()
                };
            }
            throw new Exception("Заявка не найдена");
        }

        public List<RequestViewModel> GetList()
        {
            return context.Requests
                .Select(rec => new RequestViewModel
                {
                    RequestId = rec.RequestId,
                    Price = rec.Price,
                    Date = rec.Date.ToString(),
                    RequestDetails = context.RequestDetailses
                            .Where(recPR => recPR.RequestId == rec.RequestId)
                            .Select(
                                recPC => new RequestDetailViewModel
                                {
                                    Id = recPC.Id,
                                    RequestId = recPC.RequestId,
                                    DetailId = recPC.DetailId,
                                    DetailName = recPC.Detail.DetailName,
                                    Count = recPC.Count
                                }
                            )
                            .ToList()
                })
                .ToList();
        }

        public void SaveRequestExcelFile(RequestViewModel model, string filename)
        {
            var excel = new Application();
            try
            {
                if (File.Exists(filename))
                {
                    excel.Workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = 1;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(filename, XlFileFormat.xlExcel8, Type.Missing,
                        Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                Sheets excelsheets = excel.Workbooks[1].Worksheets;

                var excelworksheet = (Worksheet)excelsheets.get_Item(1);

                excelworksheet.Cells.Clear();

                excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                excelworksheet.PageSetup.CenterHorizontally = true;
                excelworksheet.PageSetup.CenterVertically = true;

                Range excelcells = excelworksheet.get_Range("A1", "B1");
                excelcells.Merge(Type.Missing);

                excelcells.Font.Bold = true;
                excelcells.Value2 = "Запрос на детали";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;

                excelcells = excelworksheet.get_Range("A2", "B2");
                excelcells.Merge(Type.Missing);

                excelcells.Font.Bold = true;
                excelcells.Value2 = "на " + model.Date;
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;

                excelcells = excelworksheet.get_Range("B1", "B1");
                excelcells = excelcells.get_Offset(3, -1);
                excelcells.ColumnWidth = 15;
                excelcells.Value2 = "Деталь";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                excelcells = excelcells.get_Offset(0, 1);
                excelcells.ColumnWidth = 15;
                excelcells.Value2 = "Количество";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                excelcells = excelcells.get_Offset(0, 1);
                excelcells.ColumnWidth = 15;
                excelcells.Value2 = "Цена";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                double totalSum = 0;

                var dict = model.RequestDetails;
                if (dict != null)
                {
                    excelcells = excelworksheet.get_Range("A4", "A4");


                    foreach (var elem in dict)
                    {
                        double detailSum = elem.Count * elem.DetailPrice;
                        totalSum += detailSum;

                        var excelBorder =
                            excelworksheet.get_Range(excelcells, excelcells.get_Offset(dict.Count, 2));
                        excelBorder.Borders.LineStyle = XlLineStyle.xlContinuous;
                        excelBorder.Borders.Weight = XlBorderWeight.xlThin;
                        excelBorder.HorizontalAlignment = Constants.xlCenter;
                        excelBorder.VerticalAlignment = Constants.xlCenter;
                        excelBorder.BorderAround(XlLineStyle.xlContinuous,
                                                XlBorderWeight.xlMedium,
                                                XlColorIndex.xlColorIndexAutomatic, 1);

                        excelcells = excelcells.get_Offset(1, 0);
                        excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        excelcells.Font.Name = "Times New Roman";
                        excelcells.Font.Size = 12;
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = elem.DetailName;

                        excelcells = excelcells.get_Offset(0, 1);
                        excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        excelcells.Font.Name = "Times New Roman";
                        excelcells.Font.Size = 12;
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = elem.Count;

                        excelcells = excelcells.get_Offset(0, 1);
                        excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        excelcells.Font.Name = "Times New Roman";
                        excelcells.Font.Size = 12;
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = elem.DetailPrice;
                        excelcells = excelcells.get_Offset(1, -2);

                        excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        excelcells.Font.Name = "Times New Roman";
                        excelcells.Font.Size = 12;
                        excelcells.Font.Bold = true;
                        excelcells.Value2 = "Итого";

                        excelcells = excelcells.get_Offset(0, 2);
                        excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        excelcells.Font.Name = "Times New Roman";
                        excelcells.Font.Size = 12;
                        excelcells.Font.Bold = true;
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = detailSum.ToString();

                        excelcells = excelcells.get_Offset(1, -2);

                    }
                }
                excelcells = excelcells.get_Offset(1, 0);
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Итого";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                excelcells = excelcells.get_Offset(0, 2);
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Bold = true;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;
                excelcells.Value2 = totalSum;

                excel.Workbooks[1].Save();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                excel.Quit();
            }
        }

        public void SaveRequestWordFile(RequestViewModel model, string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            var winword = new Microsoft.Office.Interop.Word.Application();
            try
            {
                object missing = System.Reflection.Missing.Value;

                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;

                var font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;

                range.Text = "Запрос на детали на " + model.Date;

                range.InsertParagraphAfter();

                var requestDetails = model.RequestDetails;
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, requestDetails.Count + 1, 4, ref missing, ref missing);

                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";

                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;

                table.Cell(1, 1).Range.Text = "Деталь";
                table.Cell(1, 2).Range.Text = "Количество";
                table.Cell(1, 3).Range.Text = "Цена";
                table.Cell(1, 4).Range.Text = "Цена за деталь";

                double totalSum = 0;

                for (int i = 0; i < requestDetails.Count; ++i)
                {
                    double detailSum = requestDetails[i].Count * requestDetails[i].DetailPrice;
                    totalSum += detailSum;

                    table.Cell(i + 2, 1).Range.Text = requestDetails[i].DetailName;
                    table.Cell(i + 2, 2).Range.Text = requestDetails[i].Count.ToString();
                    table.Cell(i + 2, 3).Range.Text = requestDetails[i].DetailPrice.ToString();
                    table.Cell(i + 2, 4).Range.Text = detailSum.ToString();
                }

                table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;

                range.Text = "Итого: " + totalSum.ToString();

                font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;

                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;

                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;

                range.InsertParagraphAfter();

                object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(filename, ref fileFormat, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing);
                document.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                winword.Quit();
            }
        }
    }
}
