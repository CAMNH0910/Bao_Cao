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

    public class ReportTHQTDIController : Controller
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



        public ActionResult Index()
        {
            return View();
        }

        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult QTDI()
        {
            return View();

        }





        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        //[HttpGet]
        public ActionResult ListDetailedQualityQTDIReport(string MADV, int DONVI, string nuocnhan, int PHANLOAI, string startdate, string enddate, string MaKH)
        {

            ViewBag.donvi = DONVI;
            ViewBag.madv = MADV;
            ViewBag.phanloai = PHANLOAI;
            ViewBag.nuocnhan = nuocnhan;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.MaKH = MaKH;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportQTDI returnquality = new ReturnReportQTDI();
            returnquality = qualityTHDVRepository.QUALITY_THQTDI_DETAIL(MADV, DONVI, nuocnhan, PHANLOAI, common.DateToInt(startdate), common.DateToInt(enddate), MaKH);
            return View(returnquality);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<QualityReportTHQTDIDetail> ReturnListExcel(string MADV, int DONVI, string nuocnhan, int PHANLOAI, string startdate, string enddate, string MaKH)
        {
            ViewBag.donvi = DONVI;
            ViewBag.madv = MADV;
            ViewBag.phanloai = PHANLOAI;
            ViewBag.nuocnhan = nuocnhan;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.MaKH = MaKH;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportQTDI returnquality = new ReturnReportQTDI();
            returnquality = qualityTHDVRepository.QUALITY_THQTDI_DETAIL(MADV, DONVI, nuocnhan, PHANLOAI, common.DateToInt(startdate), common.DateToInt(enddate), MaKH);
            return returnquality.ListDetailedQualityTHQTDIReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.madv, ViewBag.donvi, ViewBag.nuocnhan, ViewBag.phanloai, ViewBag.startdate, ViewBag.enddate, ViewBag.MaKH);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<QualityReportTHQTDIDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.madv, ViewBag.donvi, ViewBag.nuocnhan, ViewBag.phanloai, ViewBag.startdate, ViewBag.enddate, ViewBag.MaKH);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHI TIẾT THEO MÃ E1 QUỐC TẾ ĐI";
            worksheet.Cells["A1:U1"].Merge = true;

            worksheet.Cells[2, 21].Value = "MÃ BÁO CÁO:KD/EQTDi";
            worksheet.Cells["U2:U2"].Merge = true;

            worksheet.Cells[2, 10].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["J2:L2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Đơn Vị";
            worksheet.Cells[4, 3].Value = "Mã tỉnh nhận";
            worksheet.Cells[4, 4].Value = "Tên tỉnh nhận";
            worksheet.Cells[4, 5].Value = "Mã E1";
            worksheet.Cells[4, 6].Value = "Mã khách hàng";
            worksheet.Cells[4, 7].Value = "Mã nước nhận";
            worksheet.Cells[4, 8].Value = "Tên nước nhận";
            worksheet.Cells[4, 9].Value = "Ngày phát hành";
            worksheet.Cells[4, 10].Value = "Mã dịch vụ";
            // worksheet.Cell4[1, 10].Value = "Tên dịch vụ";           
            worksheet.Cells[4, 11].Value = "Phân loại";
            worksheet.Cells[4, 12].Value = "KL";
            worksheet.Cells[4, 13].Value = "KLQD";
            worksheet.Cells[4, 14].Value = "Cước chính";
            worksheet.Cells[4, 15].Value = "PPXD";
            worksheet.Cells[4, 16].Value = "PPMD";
            worksheet.Cells[4, 17].Value = "PPHK";
            worksheet.Cells[4, 18].Value = "PPKC";
            worksheet.Cells[4, 19].Value = "PP khác";
            worksheet.Cells[4, 20].Value = "Tổng cước";
            worksheet.Cells[4, 21].Value = "Trạng thái";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:U4"])
            using (var ranges = worksheet.Cells["A1:U1"])
            using (var Ngay = worksheet.Cells["J2:L2"])
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
        public ActionResult Export(string MADV, int DONVI, string nuocnhan, int PHANLOAI, string startdate, string enddate, string MaKH)
        {
            ViewBag.donvi = DONVI;
            ViewBag.madv = MADV;
            ViewBag.phanloai = PHANLOAI;
            ViewBag.nuocnhan = nuocnhan;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.MaKH = MaKH;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết E1 QT ĐI.xlsx");
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