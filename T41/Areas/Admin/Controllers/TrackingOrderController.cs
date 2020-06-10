using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;

using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Configuration;
using System.Net.Http;


namespace T41.Areas.Admin.Controllers
{

    public class TrackingOrderController : Controller
    {
        Convertion common = new Convertion();
        
        public ActionResult Index()
        {
            return View();
        }
        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult TrackingOrderDetailReport()
        {
            return View();
        }

        public ActionResult ListTrackingOrderDetailReport()
        {
            return View();
        }

        //Controller gọi đến chi tiết của bảng tra cứu đơn hàng
        public ActionResult ListDetailedTrackingOrderReport(string startdate, string enddate, string customercode, int type)
        {

            TrackingOrderRepository trackingorderRepository = new TrackingOrderRepository();
            ReturnTrackingOrder returntrackingorder = new ReturnTrackingOrder();
            returntrackingorder = trackingorderRepository.TRACKING_ORDER_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate) , customercode, type);
            return View(returntrackingorder);

        }

        //Controller gọi đến Header của bảng tra cứu đơn hàng
        public ActionResult ListDetailedHeaderTrackingOrderReport(string startdate, string enddate, string customercode)
        {

            TrackingOrderRepository trackingorderRepository = new TrackingOrderRepository();
            ReturnTrackingOrder returntrackingorder = new ReturnTrackingOrder();
            returntrackingorder = trackingorderRepository.HEADER_TRACKING_ORDER_DETAIL(startdate, enddate, customercode);
            return View(returntrackingorder);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<TrackingOrderDetail> ReturnListExcel(string startdate, string enddate, string customercode, int type)
        {
            TrackingOrderRepository trackingorderRepository = new TrackingOrderRepository();
            ReturnTrackingOrder returntrackingorder = new ReturnTrackingOrder();
            returntrackingorder = trackingorderRepository.TRACKING_ORDER_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), customercode, type);
            return returntrackingorder.ListTrackingOrderReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.customercode, ViewBag.type);
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "Window.User";
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "Export Excel";
                // thêm tí comments vào làm màu 
                excelPackage.Workbook.Properties.Comments = "Export Excel Success !";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("Sheet 1");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đổ data vào Excel file
                workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<TrackingOrderDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header

            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã KH";
            worksheet.Cells[1, 3].Value = "Mã E1";
            worksheet.Cells[1, 4].Value = "Mã đối tác";
            worksheet.Cells[1, 5].Value = "Ngày gửi";
            worksheet.Cells[1, 6].Value = "Điện thoại nhận";
            worksheet.Cells[1, 7].Value = "Địa chỉ nhận";
            worksheet.Cells[1, 8].Value = "Tỉnh nhận";
            worksheet.Cells[1, 9].Value = "Khối lượng";
            worksheet.Cells[1, 10].Value = "Cước E1";
            worksheet.Cells[1, 11].Value = "Số tiền thu hộ";
            worksheet.Cells[1, 12].Value = "Người nhận/ lý do chưa phát";
            worksheet.Cells[1, 13].Value = "Ngày phát";
            worksheet.Cells[1, 14].Value = "Giờ phát";
            worksheet.Cells[1, 15].Value = "Trạng thái";
            worksheet.Cells[1, 16].Value = "Ghi chú";
            
            
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới V1
            using (var range = worksheet.Cells["A1:P1"])
            {
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Roboto", 14, FontStyle.Bold));
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }

            using (var range1 = worksheet.Cells["A2:P" + (listItems.Count + 1)])
            {
                // Canh giữa cho các text
                range1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range1.Style.Font.SetFromFont(new Font("Roboto", 12, FontStyle.Regular));
                range1.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range1.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range1.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range1.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }



        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string startdate, string enddate, string customercode, int type)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.customercode = customercode;
            ViewBag.type = type;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel 19977-2003
            //Response.ContentType = "application/vnd.ms-excel";

            // Đây là content Type dành cho file excel 2007-2010
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Quản Lý Vận Đơn_" + customercode + "_" + startdate + "_đến_" + enddate + ".xlsx");
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