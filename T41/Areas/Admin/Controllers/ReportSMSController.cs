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

    public class ReportSMSController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery
        
        //Controller lấy dữ liệu bưu cục phát
       
        //Controller lấy dữ liệu tuyến phát
      

        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult ReportSMS()
        {
            return View();

        }

        public ActionResult ReportZNSTotal()
        {
            return View();

        }

        public ActionResult ReportZNSDetail()
        {
            return View();

        }

        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        [HttpPost]
        public JsonResult ListDetailedQualitySMSReport( string customercode, string startdate, string enddate)
        {           
            ViewBag.customercode = customercode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TrackingOrderRepository trackingorderRepository = new TrackingOrderRepository();
            ReturnTrackingOrderzalo returntrackingorder = new ReturnTrackingOrderzalo();
            returntrackingorder = trackingorderRepository.TRACKING_ORDER_DETAIL_ZALO(common.DateToInt(startdate), common.DateToInt(enddate), customercode);
            //return View(returntrackingorder);
            return Json(returntrackingorder, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListZNSTotal(int donvi, string startdate, string enddate)
        {
            ViewBag.donvi = donvi;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            ReportZNSRepository ZNSReportRepository = new ReportZNSRepository();
            ReturnZNS returnZNSTotal = new ReturnZNS();
            returnZNSTotal = ZNSReportRepository.REPORTZNSTotal(common.DateToInt(startdate), common.DateToInt(enddate), donvi);
            //return View(returntrackingorder);
            return Json(returnZNSTotal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListZNSDetail(string startdate, string enddate, int donvi, string Makh, int loaitin, int trangthai)
        {
            ViewBag.donvi = donvi;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.Makh = Makh;
            ViewBag.loaitin = loaitin;
            ViewBag.trangthai = trangthai;

            ReportZNSRepository ZNSReportRepository = new ReportZNSRepository();
            ReturnZNS returnZNSDetail = new ReturnZNS();
            returnZNSDetail = ZNSReportRepository.REPORTZNSDetail(common.DateToInt(startdate), common.DateToInt(enddate), donvi, Makh, loaitin, trangthai);
            //return View(returntrackingorder);
            return Json(returnZNSDetail, JsonRequestBehavior.AllowGet);
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<TrackingOrderDetailzalo> ReturnListExcel(string customercode, string startdate, string enddate)
        {

            ViewBag.customercode = customercode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            TrackingOrderRepository trackingorderRepository = new TrackingOrderRepository();
            ReturnTrackingOrderzalo returntrackingorder = new ReturnTrackingOrderzalo();
            returntrackingorder = trackingorderRepository.TRACKING_ORDER_DETAIL_ZALO(common.DateToInt(startdate), common.DateToInt(enddate), customercode);
            return returntrackingorder.ListDetailedQualitySMSReport;
        }

        [HttpGet]
        public List<ZNSTotal> ReturnListExcelZNSTotal(string startdate, string enddate, int donvi)
        {

            ViewBag.donvi = donvi;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportZNSRepository ZNSReportRepository = new ReportZNSRepository();
            ReturnZNS returnZNSTotal = new ReturnZNS();
            returnZNSTotal = ZNSReportRepository.REPORTZNSTotal(common.DateToInt(startdate), common.DateToInt(enddate), donvi);
            return returnZNSTotal.ListZNSTotalReport;
        }

        [HttpGet]
        public List<ZNSDetail> ReturnListExcelZNSDetail(string startdate, string enddate, int donvi, string Makh, int loaitin, int trangthai)
        {

            ViewBag.donvi = donvi;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.Makh = Makh;
            ViewBag.loaitin = loaitin;
            ViewBag.trangthai = trangthai;

            ReportZNSRepository ZNSReportRepository = new ReportZNSRepository();
            ReturnZNS returnZNSDetail = new ReturnZNS();
            returnZNSDetail = ZNSReportRepository.REPORTZNSDetail(common.DateToInt(startdate), common.DateToInt(enddate), donvi, Makh, loaitin, trangthai);
            return returnZNSDetail.ListZNSDetailReport;
        }



        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.customercode, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFileZNSTotal(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelZNSTotal(ViewBag.startdate, ViewBag.enddate, ViewBag.donvi);
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
                BindingFormatForExcelZNSTotal(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFileZNSDetail(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelZNSDetail(ViewBag.startdate, ViewBag.enddate, ViewBag.donvi, ViewBag.Makh, ViewBag.loaitin, ViewBag.trangthai);
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
                BindingFormatForExcelZNSDetail(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<TrackingOrderDetailzalo> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã e1";
            worksheet.Cells[1, 3].Value = "Mã khách hàng";
            worksheet.Cells[1, 4].Value = "Điện thoại";
            worksheet.Cells[1, 5].Value = "Ngày trạng thái";
            worksheet.Cells[1, 6].Value = "Trạng thái phát";
            worksheet.Cells[1, 7].Value = "Ngày gửi tin";
            worksheet.Cells[1, 8].Value = "Loại tin";
          

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

        //Phần sửa excel
        private void BindingFormatForExcelZNSTotal(ExcelWorksheet worksheet, List<ZNSTotal> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Ngày chấp nhận";
            worksheet.Cells[1, 3].Value = "Tổng số tin đã nhắn";
            worksheet.Cells[1, 4].Value = "Số tin nhắn thành công";
            worksheet.Cells[1, 5].Value = "Số tin nhắn thất bại";
            worksheet.Cells[1, 6].Value = "Phí dịch vụ tin nhắn (chưa bao gồm VAT)";
            worksheet.Cells[1, 7].Value = "Ghi chú";



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

        private void BindingFormatForExcelZNSDetail(ExcelWorksheet worksheet, List<ZNSDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Ngày chấp nhận";
            worksheet.Cells[1, 3].Value = "Mã E1";
            worksheet.Cells[1, 4].Value = "Bưu cục chấp nhận";
            worksheet.Cells[1, 5].Value = "Mã Khánh Hàng";
            worksheet.Cells[1, 6].Value = "Số điện thoại người gửi";
            worksheet.Cells[1, 7].Value = "Ngày trạng thái";
            worksheet.Cells[1, 8].Value = "Trạng thái phát";
            worksheet.Cells[1, 9].Value = "Ngày gửi tin";
            worksheet.Cells[1, 10].Value = "Loại tin";
            worksheet.Cells[1, 11].Value = "Tổng số tin đã nhắn";
            worksheet.Cells[1, 12].Value = "Số tin nhắn thành công";
            worksheet.Cells[1, 13].Value = "Số tin nhắn thất bại";
            worksheet.Cells[1, 14].Value = "Phí dịch vụ tin nhắn (chưa bao gồm VAT)";
            worksheet.Cells[1, 15].Value = "Ghi chú";



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

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult ExportZNSTotal(string startdate, string enddate, int donvi)
        {

            ViewBag.donvi = donvi;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileZNSTotal();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp ZNS.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExportZNSDetail(string startdate, string enddate, int donvi, string Makh, int loaitin, int trangthai)
        {

            ViewBag.donvi = donvi;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.Makh = Makh;
            ViewBag.loaitin = loaitin;
            ViewBag.trangthai = trangthai;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileZNSDetail();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp ZNS.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Export(string customercode, string startdate, string enddate)
        {

            ViewBag.customercode = customercode;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp ZNS.xlsx");
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