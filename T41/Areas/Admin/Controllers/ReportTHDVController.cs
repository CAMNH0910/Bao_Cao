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

    public class ReportTHDVController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery

        //Controller lấy dữ liệu bưu cục phát
        public JsonResult DICHVU()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetAllDV(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu tuyến phát


        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult TH_DV()
        {
            return View();

        }
        public ActionResult ListDetailedQualityTHDVReport()
        {
            return View();

        }
        [HttpPost]
        public JsonResult ListDetailedItemQualityTHDVReport(string donvi, string PhanloaiDV, string Madvchinh, string startdate, string enddate)
        {
            int iDonvi = 0;
            int iPLDV = 0;

            ViewBag.donvi = donvi;
            ViewBag.PhanloaiDV = PhanloaiDV;
            ViewBag.Madvchinh = Madvchinh;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            if (donvi == "EMS")
            {
                iDonvi = 1;
            }
            else if (donvi == "BDT")
            {
                iDonvi = 2;
            }

            if (PhanloaiDV == "TN")
            {
                iPLDV = 1;
            }
            else if (PhanloaiDV == "QT")
            {
                iPLDV = 2;
            }
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportTHDV returnquality = new ReturnReportTHDV();
            returnquality = qualityTHDVRepository.QUALITY_THDV_DETAIL_ITEM(iDonvi, iPLDV, Madvchinh, common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
           // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        [HttpPost]
        public JsonResult ListDetailedQualityTHDVReport(int donvi, int phanloaidichvu, string service, string startdate, string enddate)
        {

            ViewBag.donvi = donvi;
            ViewBag.phanloaidichvu = phanloaidichvu;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportTHDV returnquality = new ReturnReportTHDV();


            returnquality = qualityTHDVRepository.QUALITY_THDV_DETAIL(donvi, phanloaidichvu, service, common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            return Json(returnquality, JsonRequestBehavior.AllowGet);
        }






        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<QualityReportKDDetail> ReturnListExcel(int donvi, int phanloaidichvu, string service, string startdate, string enddate)
        {

            ViewBag.donvi = donvi;
            ViewBag.phanloaidichvu = phanloaidichvu;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportTHDV returnquality = new ReturnReportTHDV();
            returnquality = qualityTHDVRepository.QUALITY_THDV_DETAIL(donvi, phanloaidichvu, service, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListQualityReportKDReport;
        }


        [HttpGet]
        public List<QualityReportKDDetailItem> ReturnListExcelCT(string donvi, string PhanloaiDV, string Madvchinh, string startdate, string enddate)
        {

            int iDonvi = 0;
            int iPLDV = 0;

            ViewBag.donvi = donvi;
            ViewBag.PhanloaiDV = PhanloaiDV;
            ViewBag.Madvchinh = Madvchinh;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            if (donvi == "EMS")
            {
                iDonvi = 1;
            }
            else if (donvi == "BDT")
            {
                iDonvi = 2;
            }

            if (PhanloaiDV == "TN")
            {
                iPLDV = 1;
            }
            else if (PhanloaiDV == "QT")
            {
                iPLDV = 2;
            }

            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportTHDV returnquality = new ReturnReportTHDV();
            returnquality = qualityTHDVRepository.QUALITY_THDV_DETAIL_ITEM(iDonvi, iPLDV, Madvchinh, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListDetailedItemQualityTHDVReport;
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();

            var list = ReturnListExcel(ViewBag.donvi, ViewBag.phanloaidichvu, ViewBag.service, ViewBag.startdate, ViewBag.enddate);
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

        public Stream CreateExcelFileCT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelCT(ViewBag.donvi, ViewBag.PhanloaiDV, ViewBag.Madvchinh, ViewBag.startdate, ViewBag.enddate);
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "Window.User";
                // Tạo title cho file Excel
              //  excelPackage.Workbook.Properties.Title = "Export Excel";
                // thêm tí comments vào làm màu 
               // excelPackage.Workbook.Properties.Comments = "Export Excel Success !";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("First Sheet");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đổ data vào Excel file
                workSheet.Cells[1, 1].LoadFromCollection(list, true);
                BindingFormatForExcelCT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<QualityReportKDDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Đơn Vị";
            worksheet.Cells[1, 3].Value = "Phân loại DV";
            worksheet.Cells[1, 4].Value = "Mã dịch vụ";
            worksheet.Cells[1, 5].Value = "Tên dịch vụ";
            worksheet.Cells[1, 6].Value = "SL";
            worksheet.Cells[1, 7].Value = "KL";
            worksheet.Cells[1, 8].Value = "Cước chính";
            worksheet.Cells[1, 9].Value = "PPXD";
            worksheet.Cells[1, 10].Value = "PPVX";
            worksheet.Cells[1, 11].Value = "PPMD";
            worksheet.Cells[1, 12].Value = "Cước DVCT";
            worksheet.Cells[1, 13].Value = "Tổng cước";

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


        private void BindingFormatForExcelCT(ExcelWorksheet worksheet, List<QualityReportKDDetailItem> listItems)
        {
            // Set default width cho tất cả column
           // worksheet.DefaultColWidth = 30;
           // worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
           // worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Đơn Vị";
            worksheet.Cells[1, 3].Value = "Mã tỉnh nhận";
            worksheet.Cells[1, 4].Value = "Tên tỉnh nhận";
            worksheet.Cells[1, 5].Value = "Ngày phát hành";
            worksheet.Cells[1, 6].Value = "Mã DV chính";
            worksheet.Cells[1, 7].Value = "Tên DV";
            worksheet.Cells[1, 8].Value = "Mã E1";
            worksheet.Cells[1, 9].Value = "Mã KH";
            worksheet.Cells[1, 10].Value = "Người gửi";
            worksheet.Cells[1, 11].Value = "Địa chỉ gửi";
            worksheet.Cells[1, 12].Value = "Người nhận";
            worksheet.Cells[1, 13].Value = "Địa chỉ nhận";
            worksheet.Cells[1, 14].Value = "Mã tỉnh trả";
            worksheet.Cells[1, 15].Value = "Tên tỉnh trả";
            worksheet.Cells[1, 16].Value = "Trạng thái";
            worksheet.Cells[1, 17].Value = "KL";
            worksheet.Cells[1, 18].Value = "KLQD";
            worksheet.Cells[1, 19].Value = "Cước chính";
            worksheet.Cells[1, 20].Value = "Cước DVCT";
            worksheet.Cells[1, 21].Value = "PPXD";
            worksheet.Cells[1, 22].Value = "PPVX";
            worksheet.Cells[1, 23].Value = "PP khác";
            worksheet.Cells[1, 24].Value = "Tổng cước";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:Z1"])
            {
                // Set PatternType
               // range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
               // range.Style.Fill.BackgroundColor.SetColor(Color.Orange);
                // Canh giữa cho các text
               // range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 9));
                // Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }


        }
        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(int donvi, int phanloaidichvu, string service, string startdate, string enddate)
        {

            ViewBag.donvi = donvi;
            ViewBag.phanloaidichvu = phanloaidichvu;
            ViewBag.service = service;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp theo dịch vụ.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ExportCT(string donvi, string PhanloaiDV, string Madvchinh, string startdate, string enddate)
        {

            int iDonvi = 0;
            int iPLDV = 0;

            ViewBag.donvi = donvi;
            ViewBag.PhanloaiDV = PhanloaiDV;
            ViewBag.Madvchinh = Madvchinh;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            if (donvi == "EMS")
            {
                iDonvi = 1;
            }
            else if (donvi == "BDT")
            {
                iDonvi = 2;
            }

            if (PhanloaiDV == "TN")
            {
                iPLDV = 1;
            }
            else if (PhanloaiDV == "QT")
            {
                iPLDV = 2;
            }

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileCT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết theo dịch vụ.xlsx");
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