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

namespace T41.Areas.Admin.Controllers
{
    public class KPI_Customer_HPTCController : Controller
    {
        Convertion common = new Convertion();


        public ActionResult KPI_Customer_HPTC_Report()
        {
            return View();
        }


        #region Phát không thành công
        [HttpPost]
        public JsonResult ListKPI_Customer_HPTC(string StartDate, string EndDate, int IsService, string Customer)
        {

            //ViewBag.IsCod = IsCod;          
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            KPI_Customer_HPTCRepository kPI_CustomerRepository = new KPI_Customer_HPTCRepository();
            ReturnKPI_Customer_HPTC returnquality = new ReturnKPI_Customer_HPTC();
            returnquality = kPI_CustomerRepository.KPI_Customer_HPTC(common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Customer);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public List<KPI_Customer_HPTC> Excel_KPI_Customer_HPTC(string StartDate, string EndDate, int IsService, string Customer)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            KPI_Customer_HPTCRepository qualityDetailHubRepository = new KPI_Customer_HPTCRepository();
            ReturnKPI_Customer_HPTC returnquality = new ReturnKPI_Customer_HPTC();
            returnquality = qualityDetailHubRepository.KPI_Customer_HPTC(common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Customer);
            return returnquality.ListKPI_Customer_HPTCReport;
        }
        [HttpGet]

        public ActionResult ExportKPI_Customer_HPTC(string StartDate, string EndDate, int IsService, string Customer)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelKPI_Customer();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo bưu gửi hủy phát thành công.xlsx");
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
            var list = Excel_KPI_Customer_HPTC(ViewBag.StartDate, ViewBag.EndDate, ViewBag.IsService, ViewBag.Customer);
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
                BindingFormatForExcelKPI_Customer_HPTC(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }

        }
        private void BindingFormatForExcelKPI_Customer_HPTC(ExcelWorksheet worksheet, List<KPI_Customer_HPTC> listItems)
        {
            var list = Excel_KPI_Customer_HPTC(ViewBag.StartDate, ViewBag.EndDate, ViewBag.IsService, ViewBag.Customer);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO HỦY PHÁT THÀNH CÔNG";
            worksheet.Cells["A1:F1"].Merge = true;

            worksheet.Cells[2, 6].Value = "MÃ BÁO CÁO:P/HPTC";
            worksheet.Cells["F2:F2"].Merge = true;

            worksheet.Cells[2, 3].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["C2:D2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã bưu gửi";
            worksheet.Cells[4, 3].Value = "Bưu cục chấp nhận";
            worksheet.Cells[4, 4].Value = "Tỉnh chấp nhận";
            worksheet.Cells[4, 5].Value = "Trạng thái hiện tại";
            worksheet.Cells[4, 6].Value = "Thời gian cập nhật HPTC";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:F4"])
            using (var ranges = worksheet.Cells["A1:F1"])
            using (var Ngay = worksheet.Cells["C2:D2"])
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
        //Hàm Export excel  , truyền parameter vào để export
        #endregion

    }
}