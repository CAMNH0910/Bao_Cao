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
    public class KPI_BD10_Hub_SLController : Controller
    {
        Convertion common = new Convertion();

        public ActionResult KPI_BD10_Hub_Report()
        {
            return View();

        }
        [HttpPost]
        public JsonResult KPI_BD10_Hub(string startdate, string enddate, string bc37)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.bc37 = bc37;

            KPI_BD10_Hub_SLRepository KPI_BD10_Hub_SL = new KPI_BD10_Hub_SLRepository();
            ReturnKPI_BD10_Hub_SL returnKPI_BD10_Hub_ = new ReturnKPI_BD10_Hub_SL();
            returnKPI_BD10_Hub_ = KPI_BD10_Hub_SL.KPI_BD10_Hub_SL(common.DateToInt(startdate), common.DateToInt(enddate), bc37);
            //return View(returntrackingorder);
            return Json(returnKPI_BD10_Hub_, JsonRequestBehavior.AllowGet);
        }


        public List<KPI_BD10_Hub_SLEX> Excel_KPI_BD10_Hub_SLEX(string startdate, string enddate, string bc37)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.bc37 = bc37;

            KPI_BD10_Hub_SLRepository qualityDetailHubRepository = new KPI_BD10_Hub_SLRepository();
            ReturnKPI_BD10_Hub_SL returnquality = new ReturnKPI_BD10_Hub_SL();
            returnquality = qualityDetailHubRepository.KPI_BD10_Hub_SLEX(common.DateToInt(startdate), common.DateToInt(enddate), bc37);
            return returnquality.ListKPI_BD10_Hub_SLEX;
        }
        [HttpGet]

        public ActionResult ExportKPI_BD10_Hub_SLEX(string startdate, string enddate, string bc37)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.bc37 = bc37;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelKPI_BD10_Hub_SLEX();
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


        public Stream CreateExcelKPI_BD10_Hub_SLEX(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = Excel_KPI_BD10_Hub_SLEX(ViewBag.startDate, ViewBag.endDate, ViewBag.bc37);
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
                BindingFormatForExcelKPI_BD10_Hub_SLEX(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }

        }
        private void BindingFormatForExcelKPI_BD10_Hub_SLEX(ExcelWorksheet worksheet, List<KPI_BD10_Hub_SLEX> listItems)
        {
            var list = Excel_KPI_BD10_Hub_SLEX(ViewBag.startDate, ViewBag.endDate, ViewBag.bc37);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO SẢN LƯỢNG KHỐI LƯỢNG ĐẾN ĐƯỜNG THƯ";
            worksheet.Cells["A1:I1"].Merge = true;

            worksheet.Cells[2, 9].Value = "MÃ BÁO CÁO:NVCL/SLKL_DT";
            worksheet.Cells["I2:I2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["D2:F2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Đường thư";
            worksheet.Cells[4, 3].Value = "Tên đường thư";
            worksheet.Cells[4, 4].Value = "BD10";
            worksheet.Cells[4, 5].Value = "Bưu cục gửi";
            worksheet.Cells[4, 6].Value = "Bưu cục nhận";
            worksheet.Cells[4, 7].Value = "Ngày ";
            worksheet.Cells[4, 8].Value = "Sản lượng ";
            worksheet.Cells[4, 9].Value = "Khối lượng ";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:I4"])
            using (var ranges = worksheet.Cells["A1:I1"])
            using (var Ngay = worksheet.Cells["D2:F2"])
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