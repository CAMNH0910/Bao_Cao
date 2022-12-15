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

    public class ReportSMSBrandController : Controller
    {
        Convertion common = new Convertion();      
        public ActionResult Index()
        {
            return View();
        }

        
        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult ReportSMSBrand()
        {
            return View();

        }

        [HttpPost]
        public JsonResult ListReportSMSBrand( string startdate, string enddate)
        {
          
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            ReportSMSBrandRepository qualitySMSBrand = new ReportSMSBrandRepository();
            ReturnSMSBrandDetail returnquality = new ReturnSMSBrandDetail();
            returnquality = qualitySMSBrand.REPORTSMSBRANDNAME(common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<SMSBrandDetail> ReturnListExcel( string startdate, string enddate)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            ReportSMSBrandRepository qualitySMSBrand = new ReportSMSBrandRepository();
            ReturnSMSBrandDetail returnquality = new ReturnSMSBrandDetail();
            returnquality = qualitySMSBrand.REPORTSMSBRANDNAME(common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListSMSBrandDetailReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate);
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
                workSheet.Cells[13, 1].LoadFromCollection(list, false, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<SMSBrandDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "TỔNG CÔNG TY CỔ PHẦN BƯU ĐIỆN";
            worksheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14));
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells["A1:D3"].Merge = true;
            worksheet.Cells["A1:D3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["A1:D3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheet.Cells[1, 7].Value = "Hợp đồng cung cấp dịch vụ nhắn tin số 071213/HĐ-VINATI-EMS";
            worksheet.Cells[1, 7].Style.Font.SetFromFont(new Font("Times New Roman", 14));
            worksheet.Cells[1, 7].Style.Font.Italic = true;
            worksheet.Cells["G1:J3"].Merge = true;
            worksheet.Cells["G1:J3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["G1:J3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


            worksheet.Cells[11, 1].Value = "STT";
            worksheet.Cells["A11:A12"].Merge = true;
            worksheet.Cells["A13:A16"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            worksheet.Cells[11, 2].Value = "BrandName";
            worksheet.Cells["B11:B12"].Merge = true;
            worksheet.Cells[11, 3].Value = "Mạng";
            worksheet.Cells["C11:C12"].Merge = true;
            worksheet.Cells[11, 4].Value = "Trạng thái";
            worksheet.Cells["D11:D12"].Merge = true;
            worksheet.Cells["A11:J12"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            worksheet.Cells["A11:J12"].Style.Border.Top.Color.SetColor(Color.Black);
            worksheet.Cells["A11:J12"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            worksheet.Cells["A11:J12"].Style.Border.Bottom.Color.SetColor(Color.Black);
            worksheet.Cells["A11:J12"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            worksheet.Cells["A11:J12"].Style.Border.Left.Color.SetColor(Color.Black);
            worksheet.Cells["A11:J12"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            worksheet.Cells["A11:J12"].Style.Border.Right.Color.SetColor(Color.Black);


            worksheet.Cells[5, 4].Value = "BÁO CÁO SẢN LƯỢNG DỊCH VỤ SMS BRANDNAME";
            worksheet.Cells[5, 4].Style.Font.Bold = true;
            worksheet.Cells[5, 4].Style.Font.SetFromFont(new Font("Times New Roman", 22));
            worksheet.Cells["D5:G7"].Merge = true;
            worksheet.Cells["D5:G7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["D5:G7"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheet.Cells[9, 5].Value = ViewBag.startdate + " - " + ViewBag.enddate;
            worksheet.Cells[9, 5].RichText.Insert(0, "Thời gian: ").Bold = true;
            worksheet.Cells["E9:F9"].Merge = true;
            worksheet.Cells["E9:F9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[9, 5].Style.Font.SetFromFont(new Font("Times New Roman", 11));

            worksheet.Cells[11, 5].Value = "Tổng bản tin";
            worksheet.Cells["E11:G11"].Merge = true;
            worksheet.Cells[12, 5].Value = "EMSPro";
            worksheet.Cells[12, 6].Value = "EMSTest";
            worksheet.Cells[12, 7].Value = "Tổng cộng";


            worksheet.Cells[11, 8].Value = "Tổng số điện thoại";
            worksheet.Cells["H11:J11"].Merge = true;
            worksheet.Cells[12, 8].Value = "EMSPro";
            worksheet.Cells[12, 9].Value = "EMSTest";
            worksheet.Cells[12, 10].Value = "Tổng cộng";

           
            
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A11:J12"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Orange);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Times New Roman", 11));
                // Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string startdate, string enddate)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết SMS Brand Name.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

    }
}