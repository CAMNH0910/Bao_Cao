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

    public class KPITransportController : Controller
    {
        Convertion common = new Convertion();      
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Province()

        {
            KPITransportRepository kpiTransportRepository = new KPITransportRepository();
            return Json(kpiTransportRepository.GetProvince(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        //Controller gọi đến phần view
        public ActionResult KPITransport()
        {
            return View();

        }
        public ActionResult ListDetailKPITransportNotKPI()
        {
            return View();

        }
        public ActionResult ListDetailKPITransportFail()
        {
            return View();

        }

        [HttpPost]
        public JsonResult ListDetailTotalKPITransport(int hub, int endprovince, int service, string StartDate, string EndDate)
        {
          
            ViewBag.hub = hub;          
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPITransportRepository kpiTransportRepository = new KPITransportRepository();
            ReturnKPITransportTotal returnKPITransport = new ReturnKPITransportTotal();
            returnKPITransport = kpiTransportRepository.KPITRANSPORT_DETAIL(hub, endprovince, service, common.DateToInt(StartDate), common.DateToInt(EndDate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnKPITransport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListDetailKPITransportNotKpi(int hub, int endprovince, int service, string startdate, string enddate)
        {

            ViewBag.hub = hub;
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPITransportRepository kpiTransportRepository = new KPITransportRepository();
            ReturnKPITransportNotKPI returnKPITransportNotKpi = new ReturnKPITransportNotKPI();
            returnKPITransportNotKpi = kpiTransportRepository.KPITransportNotKPI(hub, endprovince, service, common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnKPITransportNotKpi, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListDetailKPITransportFail(int hub, int endprovince, int service, string startdate, string enddate)
        {

            ViewBag.hub = hub;
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPITransportRepository kpiTransportRepository = new KPITransportRepository();
            ReturnKPITransportFail returnKPITransportFail = new ReturnKPITransportFail();
            returnKPITransportFail = kpiTransportRepository.KPITransportFail(hub, endprovince, service, common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnKPITransportFail, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<KPITransportTotal> ReturnListKpiTransportTotalExcel(int hub, int endprovince, int service, string StartDate, string EndDate)
        {
            ViewBag.hub = hub;
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPITransportRepository kpiTransportRepository = new KPITransportRepository();
            ReturnKPITransportTotal returnKPITransport = new ReturnKPITransportTotal();
            returnKPITransport = kpiTransportRepository.KPITRANSPORT_DETAIL(hub, endprovince, service, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnKPITransport.ListKpiTransportReport;
        }

       
        [HttpGet]
        public List<KPITransportFail> ReturnListKpiTransportFailExcel(int hub, int endprovince, int service, string startdate, string enddate)
        {
            ViewBag.hub = hub;
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPITransportRepository kpiTransportRepository = new KPITransportRepository();
            ReturnKPITransportFail returnKPITransportFail = new ReturnKPITransportFail();
            returnKPITransportFail = kpiTransportRepository.KPITransportFail(hub, endprovince, service, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnKPITransportFail.ListKpiTransportFailReport;
        }
        [HttpGet]
        public List<KPITransportNotKPI> ReturnListKpiTransportNotKpiExcel(int hub, int endprovince, int service, string startdate, string enddate)
        {
            ViewBag.hub = hub;
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPITransportRepository kpiTransportRepository = new KPITransportRepository();
            ReturnKPITransportNotKPI returnKPITransportNotKpi = new ReturnKPITransportNotKPI();
            returnKPITransportNotKpi = kpiTransportRepository.KPITransportNotKPI(hub, endprovince, service, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnKPITransportNotKpi.ListKpiTransportNotKpiReport;
        }


        public Stream CreateExcelFileKPITransportTotal(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListKpiTransportTotalExcel(ViewBag.hub, ViewBag.endprovince, ViewBag.service, ViewBag.StartDate, ViewBag.EndDate);
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

       
        public Stream CreateExcelFileKPITransportFail(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListKpiTransportFailExcel(ViewBag.hub, ViewBag.endprovince, ViewBag.service, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcelFail(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFileKPITransportNotKpi(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListKpiTransportNotKpiExcel(ViewBag.hub, ViewBag.endprovince, ViewBag.service, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcelNotKpi(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<KPITransportTotal> listItems)
        {
            var list = ReturnListKpiTransportTotalExcel(ViewBag.hub, ViewBag.endprovince, ViewBag.service, ViewBag.StartDate, ViewBag.EndDate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO KHAI THÁC VẬN CHUYỂN EMS";
            worksheet.Cells["A1:K1"].Merge = true;

            worksheet.Cells[2, 11].Value = "MÃ BÁO CÁO:KTVC/VC_EMS";
            worksheet.Cells["K2:K2"].Merge = true;

            worksheet.Cells[2, 5].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.endDate;
            worksheet.Cells["E2:G2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "HUB";
            worksheet.Cells[4, 3].Value = "Tên HUB";
            worksheet.Cells[4, 4].Value = "Mã tỉnh nhận";
            worksheet.Cells[4, 5].Value = "Tên tỉnh nhận";          
            worksheet.Cells[4, 6].Value = "Tổng số";
            worksheet.Cells[4, 7].Value = "Tỉ lệ đúng quy định";
            worksheet.Cells[4, 8].Value = "SL quá chỉ tiêu";
            worksheet.Cells[4, 9].Value = "TL quá chỉ tiêu";
            worksheet.Cells[4, 10].Value = "SL không KPI";
            worksheet.Cells[4, 11].Value = "TL không KPI";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:K4"])
            using (var ranges = worksheet.Cells["A1:K1"])
            using (var Ngay = worksheet.Cells["E2:G2"])
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

        private void BindingFormatForExcelFail(ExcelWorksheet worksheet, List<KPITransportFail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "Dịch vụ";
            worksheet.Cells[1, 4].Value = "Hub";
            worksheet.Cells[1, 5].Value = "Tên Hub";
            worksheet.Cells[1, 6].Value = "BC đóng chuyến thư";
            worksheet.Cells[1, 7].Value = "BC nhận chuyến thư";
            worksheet.Cells[1, 8].Value = "Tỉnh đóng";
            worksheet.Cells[1, 9].Value = "Tỉnh nhận";
            worksheet.Cells[1, 10].Value = "TG chấp nhận";
            worksheet.Cells[1, 11].Value = "BC HUB 1";
            worksheet.Cells[1, 12].Value = "TG Đến HUB 1";
            worksheet.Cells[1, 13].Value = "TG đi HUB 1";
            worksheet.Cells[1, 14].Value = "TG KT HUB 1";
            worksheet.Cells[1, 16].Value = "BC HUB 2";
            worksheet.Cells[1, 17].Value = "TG Đến HUB 2";
            worksheet.Cells[1, 18].Value = "TG đi HUB 2";
            worksheet.Cells[1, 19].Value = "TG KT HUB 2";
            worksheet.Cells[1, 20].Value = "BC KTT";
            worksheet.Cells[1, 21].Value = "TG đến KTT";
            worksheet.Cells[1, 22].Value = "Tổng thời gian";
            worksheet.Cells[1, 23].Value = "Ghi chú";


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
        private void BindingFormatForExcelNotKpi(ExcelWorksheet worksheet, List<KPITransportNotKPI> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "HUB";
            worksheet.Cells[1, 4].Value = "Tên HUB";
            worksheet.Cells[1, 5].Value = "Mã tỉnh đóng";
            worksheet.Cells[1, 6].Value = "Tên tỉnh đóng";
            worksheet.Cells[1, 7].Value = "Mã tỉnh nhận";
            worksheet.Cells[1, 8].Value = "Tên tỉnh nhận";
            worksheet.Cells[1, 9].Value = "Thời gian đi từ HUB";
            worksheet.Cells[1, 10].Value = "Dịch vụ";
            worksheet.Cells[1, 11].Value = "Loại Túi";          

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
        public ActionResult ExportKpiTransportTotal(int hub, int endprovince, int service, string startdate, string enddate)
        {
            ViewBag.hub = hub;
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileKPITransportTotal();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp KTVC "+hub+".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExportKpiTransportNotKpi(int hub, int endprovince, int service, string startdate, string enddate)
        {
            ViewBag.hub = hub;
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;


            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileKPITransportNotKpi();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Chỉ tiết bưu gửi loại từ không đo kiểm " + hub + ".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExportKpiTransportFail(int hub, int endprovince, int service, string startdate, string enddate)
        {
            ViewBag.hub = hub;
            ViewBag.endprovince = endprovince;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileKPITransportFail();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Chỉ tiết bưu gửi KPI khai thác vận chuyển quá chỉ tiêu " + hub + ".xlsx");
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