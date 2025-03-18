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
    public class KPI_Delivery_TimeController : Controller
    {
        Convertion common = new Convertion();

        public ActionResult KPI_Delivery_Time_Report()
        {
            return View();

        }
        [HttpPost]
        public JsonResult KPI_Delivery_Time(string startdate, string enddate)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPI_Delivery_Time_Repository KPI_Delivery_Time_SL = new KPI_Delivery_Time_Repository();
            ReturnKPI_Delivery_Time returnKPI_Delivery_Time_ = new ReturnKPI_Delivery_Time();
            returnKPI_Delivery_Time_ = KPI_Delivery_Time_SL.KPI_Delivery_Time(common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returntrackingorder);
            return Json(returnKPI_Delivery_Time_, JsonRequestBehavior.AllowGet);
        }

        // Sửa
        public List<KPI_Delivery_Time_EX> Excel_KPI_Delivery_Time_EX(string startdate, string enddate)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPI_Delivery_Time_Repository qualityDetailHubRepository = new KPI_Delivery_Time_Repository();
            ReturnKPI_Delivery_Time returnquality = new ReturnKPI_Delivery_Time();
            returnquality = qualityDetailHubRepository.KPI_Delivery_Time(common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListKPI_Delivery_Time_EX;
        }
        [HttpGet]

        public ActionResult ExportKPI_Delivery_Time_EX(string startdate, string enddate)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelKPI_Delivery_Time_EX();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo dự kiến thời gian phát bưu gửi.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        [HttpGet]


        public Stream CreateExcelKPI_Delivery_Time_EX(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = Excel_KPI_Delivery_Time_EX(ViewBag.startDate, ViewBag.endDate);
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
                workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcelKPI_Delivery_Time_EX(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }

        }
        private void BindingFormatForExcelKPI_Delivery_Time_EX(ExcelWorksheet worksheet, List<KPI_Delivery_Time_EX> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã tỉnh";
            worksheet.Cells[1, 4].Value = "Tên tỉnh";
            worksheet.Cells[1, 5].Value = "SL xác đến BĐT nhưng chưa phát";
            worksheet.Cells[1, 7].Value = "SL phát sinh chưa xác nhận đến BĐT phát";
            worksheet.Cells[1, 8].Value = "Sản lượng phát thành công";
            worksheet.Cells[1, 9].Value = "Ngày ";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:Z1"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Orange);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 11));
                // Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }


        }
    }
}