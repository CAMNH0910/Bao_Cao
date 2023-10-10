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

    public class ReportTHE1TNController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery
        
        //Controller lấy dữ liệu bưu cục phát
       
        public JsonResult tinhnhan()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetAllTINH(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult dichvu()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetAllDV(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult E1TN()
        {
            return View();

        }

     



        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
         //[HttpGet]
        public ActionResult ListDetailedQualityE1TNReport( int donvi, int tinhnhan, string dichvu,string khachhang, string startdate, string enddate)
        {
            
            ViewBag.donvi = donvi;            
            ViewBag.tinhnhan = tinhnhan;
            ViewBag.dichvu = dichvu;
            ViewBag.khachhang = khachhang;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportE1TN returnquality = new ReturnReportE1TN();
            returnquality = qualityTHDVRepository.QUALITY_THE1TN_DETAIL(donvi,tinhnhan,dichvu,khachhang, common.DateToInt(startdate), common.DateToInt(enddate));            
            return View(returnquality);
          
        }
            
        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<QualityReportTHE1TNDetail> ReturnListExcel(int donvi, int tinhnhan, string dichvu, string khachhang, string startdate, string enddate)
        {
            ViewBag.donvi = donvi;
            ViewBag.tinhnhan = tinhnhan;
            ViewBag.dichvu = dichvu;
            ViewBag.khachhang = khachhang;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportE1TN returnquality = new ReturnReportE1TN();
            returnquality = qualityTHDVRepository.QUALITY_THE1TN_DETAIL(donvi, tinhnhan, dichvu, khachhang, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListDetailedQualityTHE1TNReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.donvi, ViewBag.tinhnhan, ViewBag.dichvu, ViewBag.khachhang, ViewBag.startdate,ViewBag.enddate);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<QualityReportTHE1TNDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.donvi, ViewBag.tinhnhan, ViewBag.dichvu, ViewBag.khachhang, ViewBag.startdate, ViewBag.enddate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHI TIẾT THEO MÃ E1 TRONG NƯỚC";
            worksheet.Cells["A1:X1"].Merge = true;

            worksheet.Cells[2, 24].Value = "MÃ BÁO CÁO:KD/ETN";
            worksheet.Cells["X2:X2"].Merge = true;

            worksheet.Cells[2, 12].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["K2:L2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Đơn vị (BĐT/EMS";
            worksheet.Cells[4, 3].Value = "Mã tỉnh nhận";
            worksheet.Cells[4, 4].Value = "Tên tỉnh nhận";
            worksheet.Cells[4, 5].Value = "Ngày phát hành";
            worksheet.Cells[4, 6].Value = "Mã dịch vụ";
            worksheet.Cells[4, 7].Value = "Tên dịch vụ";
            worksheet.Cells[4, 8].Value = "Mã E1";
            worksheet.Cells[4, 9].Value = "Mã khách hàng";
            worksheet.Cells[4, 10].Value = "Tên người gửi";
            worksheet.Cells[4, 11].Value = "Địa chỉ người gửi";
            worksheet.Cells[4, 12].Value = "Tên người nhận";
            worksheet.Cells[4, 13].Value = "Địa chỉ người nhận";
            worksheet.Cells[4, 14].Value = "Mã tỉnh trả";
            worksheet.Cells[4, 15].Value = "Tỉnh trả";
            worksheet.Cells[4, 16].Value = "Trạng thái";
            worksheet.Cells[4, 17].Value = "Khối lượng";
            worksheet.Cells[4, 18].Value = "Khối lượng quy đổi";
            worksheet.Cells[4, 19].Value = "Cước chính";
            worksheet.Cells[4, 20].Value = "Dịch vụ công thêm";
            worksheet.Cells[4, 21].Value = "Phụ phí xăng dầu";
            worksheet.Cells[4, 22].Value = "Phụ phí vùng xa";
            worksheet.Cells[4, 23].Value = "Phụ phí khác";
            worksheet.Cells[4, 24].Value = "Tổng cước";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:X4"])
            using (var ranges = worksheet.Cells["A1:Y1"])
            using (var Ngay = worksheet.Cells["K2:L2"])
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
        public ActionResult Export(int donvi,int tinhnhan, string dichvu, string khachhang, string startdate, string enddate)
        {
            ViewBag.donvi = donvi;
            ViewBag.tinhnhan = tinhnhan;
            ViewBag.dichvu = dichvu;
            ViewBag.khachhang = khachhang;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết E1 TN.xlsx");
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