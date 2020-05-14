using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector.Entities;
using OfficeOpenXml;
using System.IO;

namespace DataCollector.Services.BLL
{
    interface IReportService<T>
    {
        (bool IsError, string ErrorMessage) MakeReport(List<T> DataList);
    }
    public class ExcelReportService : IReportService<Dose>
    {
        private int START_ROW_DATA = 5;

        private string FILE_NAME = @"\" + "Курганинский ХК" + " " + DateTime.Now.ToLocalTime().ToString().Replace(':', '-') + ".xlsx";

        private string _savePath = null;
        private ExcelPackage _ex=null;
        private FileInfo _file = null;

        public ExcelReportService(string savePath)
        {
            _savePath = savePath + FILE_NAME;
            _file = new FileInfo(_savePath);
            _ex = new ExcelPackage(_file);
        }

        public (bool IsError, string ErrorMessage) MakeReport(List<Dose> doses)
        {
            ExcelWorksheet ws = _ex.Workbook.Worksheets.Add("Статистика");

            var result1=MakeHeading(ws);

            if (result1.IsError)
            {
                return (true, result1.ErrorMessage);
            }

            var result2 = DataRecordToExcelFile(ws, doses);

            if (result2.IsError)
            {
                return (true, result2.ErrorMessage);
            }
            _ex.SaveAs(new FileInfo(_savePath));
            return (false, "");
        }

        private (bool IsError, string ErrorMessage) MakeHeading(ExcelWorksheet ws)
        {
            try
            {
                ws.Cells[1, 2].Value = "Курганинский ХК";
                ws.Cells[1, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ws.Column(2).Width = 20;


                ws.Cells[2, 2].Value = "PLC1";
                ws.Cells[2, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ws.Cells[1, 9].Value = DateTime.Now.ToShortDateString();
                ws.Cells[1, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ws.Column(9).Width = 20;

                ws.Cells[2, 9].Value = DateTime.Now.ToShortTimeString();
                ws.Cells[2, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ws.Cells[1, 1].Value = "Имя объекта";
                ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ws.Cells[2, 1].Value = "Имя устройства";
                ws.Cells[2, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

               // ws.Cells[1, 7, 1, 8].Merge = true;
                ws.Cells[1, 8].Value = "Дата формирования";
                ws.Cells[1, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

               // ws.Cells[2, 7, 2, 8].Merge = true;
                ws.Cells[2, 8].Value = "Время формирования";
                ws.Cells[2, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ws.Column(8).Width = 20;

                ws.Cells[4, 1].Value = "Дата";// "Дата";
                ws.Cells[4, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ws.Cells[4, 2].Value = "Время";// "Время";
                ws.Cells[4, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ws.Cells[4, 3].Value = "Номер дозатора";// "Номер дозатора";
                ws.Cells[4, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ws.Cells[4, 4].Value = "Номер компонента"; //"Номер компоннета";
                ws.Cells[4, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ws.Cells[4, 5].Value = "Заданный вес,кг";// "Заданный вес";
                ws.Cells[4, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ws.Cells[4, 6].Value = "Фактический вес,кг"; //"Фактический вес";
                ws.Cells[4, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ws.Cells[4, 7].Value = "Погрешность,кг";// "Погрешность";
                ws.Cells[4, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                return (false, "");
            }

            catch (Exception err)
            {
                return (true, err.Message);
            }

        }

        private (bool IsError, string ErrorMessage) DataRecordToExcelFile (ExcelWorksheet ws, List<Dose> doses)
        {
            int row = 0;

            try
            {
                foreach (var list in doses)
                {

                    ws.Cells[START_ROW_DATA + row, 1].Value = Convert.ToString(list.datedose).TrimEnd('0', ':');
                    ws.Cells[START_ROW_DATA + row, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    ws.Column(1).Width = 20;

                    ws.Cells[START_ROW_DATA + row, 2].Value =Convert.ToString(list.timedose);
                    ws.Cells[START_ROW_DATA + row, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    ws.Column(2).Width = 20;

                    ws.Cells[START_ROW_DATA + row, 3].Value = list.dosiernumber;
                    ws.Cells[START_ROW_DATA + row, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    ws.Column(3).Width = 20;

                    ws.Cells[START_ROW_DATA + row, 4].Value = list.componentnumber;
                    ws.Cells[START_ROW_DATA + row, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    ws.Column(4).Width = 20;

                    ws.Cells[START_ROW_DATA + row, 5].Value = (double)list.setweight/100;
                    ws.Cells[START_ROW_DATA + row, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    ws.Column(5).Width = 20;

                    ws.Cells[START_ROW_DATA + row, 6].Value = (double)list.realweight/100;
                    ws.Cells[START_ROW_DATA + row, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    ws.Column(6).Width = 20;

                    ws.Cells[START_ROW_DATA + row, 7].Value = (double)list.errorweight/100;
                    ws.Cells[START_ROW_DATA + row, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    ws.Column(7).Width = 20;

                    row += 1;

                }
                return (false, "");
            }

            catch(Exception err)
            {
                return (true, err.Message);
            }
        }
    }
}
