using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using System.Drawing;
using OfficeOpenXml.Table;

namespace T41.Areas.Admin.Controllers
{
    public class KPI_BD10_Hub_TimeController : Controller
    {
        Convertion common = new Convertion();

        public ActionResult KPI_BD10_Hub_Report()
        {
            return View();

        }
        [HttpPost]
        public JsonResult KPI_BD10_Hub(string startdate, string enddate, string mailrouteScheduleName)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.mailrouteScheduleName = mailrouteScheduleName;

            KPI_BD10_Hub_TimeRepository kPI_BD10_Hub_Time = new KPI_BD10_Hub_TimeRepository();
            ReturnKPI_BD10_Hub_Time returnKPI_BD10_Hub_ = new ReturnKPI_BD10_Hub_Time();
            returnKPI_BD10_Hub_ = kPI_BD10_Hub_Time.KPI_BD10_Hub_Time(common.DateToInt(startdate), common.DateToInt(enddate), mailrouteScheduleName);
            //return View(returntrackingorder);
            return Json(returnKPI_BD10_Hub_, JsonRequestBehavior.AllowGet);
        }


        public List<KPI_BD10_Hub_TimeEX> Excel_KPI_BD10_Hub_TimeEX(string StartDate, string EndDate, string mailrouteScheduleName)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.mailrouteScheduleName = mailrouteScheduleName;

            KPI_BD10_Hub_TimeRepository qualityDetailHubRepository = new KPI_BD10_Hub_TimeRepository();
            ReturnKPI_BD10_Hub_Time returnquality = new ReturnKPI_BD10_Hub_Time();
            returnquality = qualityDetailHubRepository.KPI_BD10_Hub_TimeEX(common.DateToInt(StartDate), common.DateToInt(EndDate), mailrouteScheduleName);
            return returnquality.ListKPI_BD10_Hub_TimeEX;
        }
        [HttpGet]

        public ActionResult ExportKPI_BD10_Hub_TimeEX(string StartDate, string EndDate, string mailrouteScheduleName)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.mailrouteScheduleName = mailrouteScheduleName;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelKPI_Customer();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo thời gian đến hub.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        [HttpGet]


        public Stream CreateExcelKPI_Customer(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = Excel_KPI_BD10_Hub_TimeEX(ViewBag.StartDate, ViewBag.EndDate, ViewBag.mailrouteScheduleName);
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
                workSheet.Cells[4, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcelKPI_BD10_Hub_TimeEX(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }

        }
        private void BindingFormatForExcelKPI_BD10_Hub_TimeEX(ExcelWorksheet worksheet, List<KPI_BD10_Hub_TimeEX> listItems)
        {
            var list = Excel_KPI_BD10_Hub_TimeEX(ViewBag.StartDate, ViewBag.EndDate, ViewBag.mailrouteScheduleName);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO BƯU GỬI XÁC NHẬN ĐẾN HUB";
            worksheet.Cells["A1:G1"].Merge = true;

            worksheet.Cells[2, 7].Value = "MÃ BÁO CÁO:NVCL/HUB";
            worksheet.Cells["G2:G2"].Merge = true;

            worksheet.Cells[2, 3].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["C2:E2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Đường thư";
            worksheet.Cells[4, 3].Value = "Mã đường thư";
            worksheet.Cells[4, 4].Value = "BD10";
            worksheet.Cells[4, 5].Value = "Mã bưu gửi";
            worksheet.Cells[4, 6].Value = "Bưu cục đến";
            worksheet.Cells[4, 7].Value = "Ngày giờ đến";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:G4"])
            using (var ranges = worksheet.Cells["A1:G1"])
            using (var Ngay = worksheet.Cells["C2:E2"])
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
                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));
            }


        }
    }
}