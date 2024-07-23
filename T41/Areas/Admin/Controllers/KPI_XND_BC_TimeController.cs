using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Models.DataModel;

using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using T41.Areas.Admin.Model.DataModel;
using System.Security.Policy;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Spreadsheet;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;
using TableStyles = OfficeOpenXml.Table.TableStyles;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace T41.Areas.Admin.Controllers
{
    public class KPI_XND_BC_TimeController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult KPI_XND_BC_Time()
        {
            return View();
        }
        #region Tổng hợp
        [HttpPost]
        public JsonResult ListKPI_XND_BC_Time(string startdate, string enddate, int zone, int endpostcode, string startime, string endtime)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.startime = startime;
            ViewBag.endtime = endtime;

            KPI_XND_BC_TimeRepository  KPI_XND_BC_Time = new KPI_XND_BC_TimeRepository();
            ReturnKPI_XND_BC_Time returnquality = new ReturnKPI_XND_BC_Time();
            returnquality =  KPI_XND_BC_Time.KPI_XND_BC_Time_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, common.ConvertTimeToInt_NEW(startime), common.ConvertTimeToInt_NEW(endtime) );

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue; 
            return jsonResult;

        }
        public List<KPI_XND_BC_Time> ReturnListExcel(string startdate, string enddate, int zone, int endpostcode, string startime, string endtime)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.startime = startime;
            ViewBag.endtime = endtime;
            KPI_XND_BC_TimeRepository  KPI_XND_BC_Time = new KPI_XND_BC_TimeRepository();
            ReturnKPI_XND_BC_Time returnquality = new ReturnKPI_XND_BC_Time();
            returnquality =  KPI_XND_BC_Time.KPI_XND_BC_Time_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, common.ConvertTimeToInt_NEW(startime), common.ConvertTimeToInt_NEW(endtime));
            return returnquality.ListKPI_XND_BC_Time;
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.startime, ViewBag.endtime);
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "Window.User";
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "Export Excel";
                // thêm tí comments vào làm màu 
                excelPackage.Workbook.Properties.Comments = "Export Excel Success !";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("First Sheet");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đổ data vào Excel file
                workSheet.Cells[6, 1].LoadFromCollection(list, false, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<KPI_XND_BC_Time> listItems)
        {
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.startime, ViewBag.endtime);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO SẢN LƯỢNG ĐẾN PHÁT THEO KHUNG GIỜ";
            worksheet.Cells["A1:AC1"].Merge = true;

            worksheet.Cells[2, 29].Value = "MÃ BÁO CÁO:P/SLPTG";
            worksheet.Cells["AC2:AC2"].Merge = true;

            worksheet.Cells[2, 14].Value = "Từ ngày:" + " " + ViewBag.startdate + " - " + "Đến ngày:" + " " + ViewBag.endDate;
            worksheet.Cells["N2:P2"].Merge = true;

            worksheet.Cells[3, 14].Value = "Từ giờ:" + " " + ViewBag.startime +"H"+ " - " + "Đến giờ: " + " " + ViewBag.endtime + "H";
            worksheet.Cells["N3:P3"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells["A4:A5"].Merge = true;
            worksheet.Cells[4, 2].Value = "Đơn Vị";
            worksheet.Cells["B4:B5"].Merge = true;
            worksheet.Cells[4, 3].Value = "Bưu Cục";
            worksheet.Cells["C4:C5"].Merge = true;
            worksheet.Cells[4, 4].Value = "Tên Bưu Cục";
            worksheet.Cells["D4:D5"].Merge = true;
            worksheet.Cells[4, 5].Value = "Tổng số";
            worksheet.Cells["E4:E5"].Merge = true;

            worksheet.Cells[4, 6].Value = "Hỏa tốc";
            worksheet.Cells["F4:G4"].Merge = true;
            worksheet.Cells[5, 6].Value = "Ô tô";
            worksheet.Cells[5, 7].Value = "Xe máy";
            worksheet.Cells[4, 8].Value = "Quốc tế";
            worksheet.Cells["H4:I4"].Merge = true;
            worksheet.Cells[5, 8].Value = "Ô tô";
            worksheet.Cells[5, 9].Value = "Xe máy";
            worksheet.Cells[4, 10].Value = "Visa";
            worksheet.Cells["J4:K4"].Merge = true;
            worksheet.Cells[5, 10].Value = "Ô tô";
            worksheet.Cells[5, 11].Value = "Xe máy";
            worksheet.Cells[4, 12].Value = "TMDT Đồng giá";
            worksheet.Cells["L4:M4"].Merge = true;
            worksheet.Cells[5, 12].Value = "Ô tô";
            worksheet.Cells[5, 13].Value = "Xe máy";
            worksheet.Cells[4, 14].Value = "TMDT COD";
            worksheet.Cells["N4:O4"].Merge = true;
            worksheet.Cells[5, 14].Value = "Ô tô";
            worksheet.Cells[5, 15].Value = "Xe máy";
            worksheet.Cells[4, 16].Value = "TMDT không COD";
            worksheet.Cells["P4:Q4"].Merge = true;
            worksheet.Cells[5, 16].Value = "Ô tô";
            worksheet.Cells[5, 17].Value = "Xe máy";

            worksheet.Cells[4, 18].Value = "Đại lý";
            worksheet.Cells["R4:S4"].Merge = true;
            worksheet.Cells[5, 18].Value = "Ô tô";
            worksheet.Cells[5, 19].Value = "Xe máy";

            worksheet.Cells[4, 20].Value = "TTC";
            worksheet.Cells["T4:U4"].Merge = true;
            worksheet.Cells[5, 20].Value = "Ô tô";
            worksheet.Cells[5, 21].Value = "Xe máy";

            worksheet.Cells[4, 22].Value = "TTB";
            worksheet.Cells["V4:W4"].Merge = true;
            worksheet.Cells[5, 22].Value = "Ô tô";
            worksheet.Cells[5, 23].Value = "Xe máy";

            worksheet.Cells[4, 24].Value = "TTV";
            worksheet.Cells["X4:Y4"].Merge = true;
            worksheet.Cells[5, 24].Value = "Ô tô";
            worksheet.Cells[5, 25].Value = "Xe máy";

            worksheet.Cells[4, 26].Value = "Hàng hóa";
            worksheet.Cells["Z4:AA4"].Merge = true;
            worksheet.Cells[5, 26].Value = "Ô tô";
            worksheet.Cells[5, 27].Value = "Xe máy";

            worksheet.Cells[4, 28].Value = "Tài liệu";
            worksheet.Cells["AB4:AC4"].Merge = true;
            worksheet.Cells[5, 28].Value = "Ô tô";
            worksheet.Cells[5, 29].Value = "Xe máy";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:AC5"])
            using (var ranges = worksheet.Cells["A1:AC1"])
            using (var Ngay = worksheet.Cells["N2:P2"])
            using (var Gio = worksheet.Cells["N3:P3"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 11));

                // Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
                //ranges.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //Set Màu cho Background
                //ranges.Style.Fill.BackgroundColor.SetColor(Color.none);
                // Canh giữa cho các text
                Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                Gio.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string startdate, string enddate, int zone, int endpostcode, string startime, string endtime)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.startime = startime;
            ViewBag.endtime = endtime;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Sản lượng đến phát theo khung giờ.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion
    }
}