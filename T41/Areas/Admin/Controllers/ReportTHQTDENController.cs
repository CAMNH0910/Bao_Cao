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

namespace T41.Areas.Admin.Controllers
{

    public class ReportTHQTDENController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery
        
        //Controller lấy dữ liệu bưu cục phát
       
        public JsonResult MANUOC()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetMANC(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MANUOCOE()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetMANCOE(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult QTDEN()
        {
            return View();

        }

     



        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
         //[HttpGet]
        public ActionResult ListDetailedQualityQTDENReport( int nuocoe, string nuocnhan, int PHANLOAI, string startdate, string enddate)
        {
            
            ViewBag.nuocoe = nuocoe;
            
            ViewBag.phanloai = PHANLOAI;
            ViewBag.nuocnhan = nuocnhan;            
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportQTDEN returnquality = new ReturnReportQTDEN();
            returnquality = qualityTHDVRepository.QUALITY_THQTDEN_DETAIL(nuocoe, nuocnhan, PHANLOAI, common.DateToInt(startdate), common.DateToInt(enddate));            
            return View(returnquality);
          
        }
            
        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<QualityReportTHQTDENDetail> ReturnListExcel(int nuocoe, string nuocnhan, int PHANLOAI, string startdate, string enddate)
        {
            ViewBag.nuocoe = nuocoe;
            ViewBag.phanloai = PHANLOAI;
            ViewBag.nuocnhan = nuocnhan;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportQTDEN returnquality = new ReturnReportQTDEN();
            returnquality = qualityTHDVRepository.QUALITY_THQTDEN_DETAIL(nuocoe, nuocnhan, PHANLOAI, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListDetailedQualityTHQTDENReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.nuocoe, ViewBag.nuocnhan, ViewBag.phanloai, ViewBag.startdate,ViewBag.enddate);
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
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<QualityReportTHQTDENDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.nuocoe, ViewBag.nuocnhan, ViewBag.phanloai, ViewBag.startdate, ViewBag.enddate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHI TIẾT THEO MÃ E1 QUỐC TẾ ĐẾN";
            worksheet.Cells["A1:I1"].Merge = true;

            worksheet.Cells[2, 9].Value = "MÃ BÁO CÁO:KD/EQTDen";
            worksheet.Cells["I2:I2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["D2:F2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã E1";
            worksheet.Cells[4, 3].Value = "Mã nước gốc";
            worksheet.Cells[4, 4].Value = "Tên nước gốc";
            worksheet.Cells[4, 5].Value = "Ngày đến OE nhận";
            worksheet.Cells[4, 6].Value = "Mã OE nhận";
            worksheet.Cells[4, 7].Value = "Phân loại D/M";
            worksheet.Cells[4, 8].Value = "Khối lượng";
            worksheet.Cells[4, 9].Value = "Trạng thái phát hàng";

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

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(int nuocoe, string nuocnhan, int PHANLOAI, string startdate, string enddate)
        {
            ViewBag.nuocoe = nuocoe;          
            ViewBag.phanloai = PHANLOAI;
            ViewBag.nuocnhan = nuocnhan;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết E1 QT Đến.xlsx");
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