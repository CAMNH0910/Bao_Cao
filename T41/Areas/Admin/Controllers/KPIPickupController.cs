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
using System.Configuration;

namespace T41.Areas.Admin.Controllers
{

    public class KPIPickupController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult PosCode(int zone)
        {
            KPIPickupRepository kpipickupRepository = new KPIPickupRepository();
            return Json(kpipickupRepository.GetAllPOSCODE(zone), JsonRequestBehavior.AllowGet);

        }
        public JsonResult PosCodePickup(string unit)
        {
            KPIPickupRepository kpipickupRepository = new KPIPickupRepository();
            return Json(kpipickupRepository.GetAllPOSCODEPickup(unit), JsonRequestBehavior.AllowGet);

        }
        public JsonResult RouteCodePickup(string Poscode)
        {
            KPIPickupRepository kpipickupRepository = new KPIPickupRepository();
            return Json(kpipickupRepository.GetAllROUTECODEPickup(Poscode), JsonRequestBehavior.AllowGet);

        }
        public ActionResult KPIPickup()
        {
            return View();

        }

        public ActionResult KPIPickupV2()
        {
            return View();

        }
        public ActionResult ListDetailKPIPickUpV2Report()
        {
            return View();

        }
        public ActionResult DetailPickup()
        {
            return View();

        }

        public ActionResult KPIPickupSuccess()
        {
            return View();

        }

        public ActionResult KPIPickupSuccessDQD()
        {
            return View();

        }
        public ActionResult ListItemDetailReport()
        {
            return View();

        }
        public ActionResult ListDetailPickupReport(string fromdate, string todate, string fromtime, string totime, string poscode, string unit, int routeid, string status, int? page, int datatype, int collectquality)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnDetailPickup returnquality = new ReturnDetailPickup();
            returnquality = qualityKPIServiceRepository.DETAIL_PICKUP(fromdate, todate, fromtime, totime, poscode, unit, routeid, status, currentPageIndex, page_size, datatype, collectquality);
            ViewBag.total = returnquality.Total;
            ViewBag.total_page = (returnquality.Total + page_size - 1) / page_size;
            return View(returnquality);

        }

        [HttpPost]
        public JsonResult ListKPIPickupSuccessReport(string StartDate, string EndDate, string district, string poscode, int routecode)
        {
            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPIPickupSuccess returnquality = new ReturnKPIPickupSuccess();
            returnquality = qualityKPIServiceRepository.KPIPickupSuccess(common.DateToInt(StartDate), common.DateToInt(EndDate), district, poscode, routecode);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListKPIPickupSuccessDQDReport(string StartDate, string EndDate, string district, string poscode, int routecode)
        {
            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPIPickupSuccessDQD returnquality = new ReturnKPIPickupSuccessDQD();
            returnquality = qualityKPIServiceRepository.KPIPickupSuccessDQD(common.DateToInt(StartDate), common.DateToInt(EndDate), district, poscode, routecode);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListDetailTotalKPIPickup(int zone, int poscode, string StartDate, string EndDate)
        {

            ViewBag.zone = zone;
            ViewBag.poscode = poscode;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPIPickup returnquality = new ReturnKPIPickup();
            returnquality = qualityKPIServiceRepository.KPIPickup(zone, poscode, common.DateToInt(StartDate), common.DateToInt(EndDate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public JsonResult ListTotalKPIPickupV2(int Zone, int Poscode, string StartDate, string EndDate)
        {

            ViewBag.zone = Zone;
            ViewBag.poscode = Poscode;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPITotalPickUpV2 returnquality = new ReturnKPITotalPickUpV2();
            returnquality = qualityKPIServiceRepository.KPIPickupTotalV2(Zone, Poscode, common.DateToInt(StartDate), common.DateToInt(EndDate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult ListDetailKPIPickupV2(int Poscode, string StartDate, string EndDate)
        {
            ViewBag.poscode = Poscode;
            ViewBag.StartDate = StartDate; 
            ViewBag.EndDate = EndDate;

            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnDetailPickUpV2 returnquality = new ReturnDetailPickUpV2();
            returnquality = qualityKPIServiceRepository.DetailKPIPickUpV2(Poscode, common.DateToInt(StartDate), common.DateToInt(EndDate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public List<TotalKPIPickUpV2> ReturnListExcelTotalPickupV2(int Zone, int Poscode, string StartDate, string EndDate)
        {
            KPIPickupRepository qualityKPIPickupRepository = new KPIPickupRepository();
            ReturnKPITotalPickUpV2 returnquality = new ReturnKPITotalPickUpV2();
            returnquality = qualityKPIPickupRepository.KPIPickupTotalV2(Zone, Poscode, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListTotalKPIPickUpV2;
        }

        [HttpPost]
        public JsonResult ListDetailItemKPIPickup(int poscode, int timeinterval, string startDate, string endDate)
        {

            ViewBag.poscode = poscode;
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;

            KPIPickupRepository qualityKPIPickupRepository = new KPIPickupRepository();
            ReturnKPIPickupItem returnquality = new ReturnKPIPickupItem();
            returnquality = qualityKPIPickupRepository.KPIPickupItem(poscode, timeinterval, common.DateToInt(startDate), common.DateToInt(endDate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #region PICKUPSUCESS
        [HttpGet]
        public List<KPIPickupSuccess> ReturnListExcelPickupSuccess(string StartDate, string EndDate, string district, string poscode, int routecode)
        {
            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPIPickupSuccess returnquality = new ReturnKPIPickupSuccess();
            returnquality = qualityKPIServiceRepository.KPIPickupSuccess(common.DateToInt(StartDate), common.DateToInt(EndDate), district, poscode, routecode);
            return returnquality.ListKPIPickupSuccessReport;
        }
        public Stream CreateExcelFilePickupSuccess(Stream stream = null)
        {
            //var list = CreateTestItems();


            var list = ReturnListExcelPickupSuccess(ViewBag.StartDate, ViewBag.EndDate, ViewBag.district, ViewBag.poscode, ViewBag.routecode);
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
                BindingFormatForExcelPickupupSuccess(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        private void BindingFormatForExcelPickupupSuccess(ExcelWorksheet worksheet, List<KPIPickupSuccess> listItems)
        {
            var list = ReturnListExcelPickupSuccess(ViewBag.StartDate, ViewBag.EndDate, ViewBag.district, ViewBag.poscode, ViewBag.routecode);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THU GOM THÀNH CÔNG";
            worksheet.Cells["A1:N1"].Merge = true;

            worksheet.Cells[2, 14].Value = "MÃ BÁO CÁO:TG/TGTC";
            worksheet.Cells["N2:N2"].Merge = true;

            worksheet.Cells[2, 7].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["G2:H2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Bưu cục";
            worksheet.Cells[4, 3].Value = "Tuyến thu gom";
            worksheet.Cells[4, 4].Value = "Tổng số lượng tin";
            worksheet.Cells[4, 5].Value = "Số lượng tin thu gom thành công";
            worksheet.Cells[4, 6].Value = "Sản lượng thu gom thành công";
            worksheet.Cells[4, 7].Value = "Tỷ lệ thu gom thành công";
            worksheet.Cells[4, 8].Value = "Số lượng tin thu gom không thành công";
            worksheet.Cells[4, 9].Value = "R0";
            worksheet.Cells[4, 10].Value = "R1";
            worksheet.Cells[4, 11].Value = "R2";
            worksheet.Cells[4, 12].Value = "R3";
            worksheet.Cells[4, 13].Value = "R4";
            worksheet.Cells[4, 14].Value = "R5";
            using (var range = worksheet.Cells["A4:N4"])
            using (var ranges = worksheet.Cells["A1:N1"])
            using (var Ngay = worksheet.Cells["G2:H2"])
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

        [HttpGet]
        public ActionResult ExportPickupSuccess(string StartDate, string EndDate, string district, string poscode, int routecode)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.district = district;
            ViewBag.poscode = poscode;
            ViewBag.routecode = routecode;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFilePickupSuccess();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp thu gom.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        #endregion

        #region DetailPickup
        [HttpGet]
        public List<DetailPickup> ReturnListExcelDetailPickup(string fromdate, string todate, string fromtime, string totime, string poscode, string unit, int routeid, string status, int? page, int datatype, int collectquality)
        {
            KPIPickupRepository qualityKPIPickupRepository = new KPIPickupRepository();
            ReturnDetailPickup returnquality = new ReturnDetailPickup();
            returnquality = qualityKPIPickupRepository.DETAIL_PICKUP(fromdate, todate, fromtime, totime, poscode, unit, routeid, status, 1, 10000, datatype, collectquality);
            return returnquality.listDetailPickup_Report;
        }
        public Stream CreateExcelFileDetailPickup(Stream stream = null)
        {
            //var list = CreateTestItems();


            var list = ReturnListExcelDetailPickup(ViewBag.fromdate, ViewBag.todate, ViewBag.fromtime, ViewBag.totime, ViewBag.poscode, ViewBag.unit, ViewBag.routeid, ViewBag.status, ViewBag.page, ViewBag.datatype, ViewBag.collectquality);
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
                BindingFormatForExcelDetailPickup(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        public Stream CreateExcelFileTotalPickupV2(Stream stream = null)
        {
            //var list = CreateTestItems();


            var list = ReturnListExcelTotalPickupV2(ViewBag.Zone, ViewBag.PosCode, ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcelTotalPickupV2(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        private void BindingFormatForExcelDetailPickup(ExcelWorksheet worksheet, List<DetailPickup> listItems)
        {
            var list = ReturnListExcelDetailPickup(ViewBag.fromdate, ViewBag.todate, ViewBag.fromtime, ViewBag.totime, ViewBag.poscode, ViewBag.unit, ViewBag.routeid, ViewBag.status, ViewBag.page, ViewBag.datatype, ViewBag.collectquality);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT LƯỢNG CT THU GOM";
            worksheet.Cells["A1:W1"].Merge = true;

            worksheet.Cells[2, 23].Value = "MÃ BÁO CÁO:TG/CTTG";
            worksheet.Cells["W2:W2"].Merge = true;

            worksheet.Cells[2, 11].Value = "Từ ngày:" + " " + ViewBag.fromdate + " " + "Đến ngày" + ViewBag.todate;
            worksheet.Cells["K2:M2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Ngày";
            worksheet.Cells[4, 3].Value = "Bưu cục";
            worksheet.Cells[4, 4].Value = "Mã tin";
            worksheet.Cells[4, 5].Value = "Tuyến thu gom";
            worksheet.Cells[4, 6].Value = "Tên khách hàng";
            worksheet.Cells[4, 7].Value = "Mã khách hàng";
            worksheet.Cells[4, 8].Value = "Địa chỉ thu gom";
            worksheet.Cells[4, 9].Value = "Số lượng";
            worksheet.Cells[4, 10].Value = "Khối lượng";
            worksheet.Cells[4, 11].Value = "Giờ điều tin";
            worksheet.Cells[4, 12].Value = "Giờ yêu cầu thu gom";
            worksheet.Cells[4, 13].Value = "Giờ nhận tin";
            worksheet.Cells[4, 14].Value = "Giờ xác nhận";
            worksheet.Cells[4, 15].Value = "Giờ thu gom";
            worksheet.Cells[4, 16].Value = "Giờ xác nhận đến";
            worksheet.Cells[4, 17].Value = "Giờ thu KTC";
            worksheet.Cells[4, 18].Value = "Tin bưu cục xác nhận";
            worksheet.Cells[4, 19].Value = "Sản lượng bưu cục xác nhận";
            worksheet.Cells[4, 20].Value = "Lý do";
            worksheet.Cells[4, 21].Value = "Người tạo";
            worksheet.Cells[4, 22].Value = "Người điều";
            worksheet.Cells[4, 23].Value = "Bưu tá gom";

            using (var range = worksheet.Cells["A4:W4"])
            using (var ranges = worksheet.Cells["A1:W1"])
            using (var Ngay = worksheet.Cells["K2:M2"])
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
        private void BindingFormatForExcelTotalPickupV2(ExcelWorksheet worksheet, List<TotalKPIPickUpV2> listItems)
        {
            var list = ReturnListExcelTotalPickupV2(ViewBag.Zone, ViewBag.PosCode, ViewBag.StartDate, ViewBag.EndDate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT LƯỢNG THU GOM V2";
            worksheet.Cells["A1:H1"].Merge = true;

            worksheet.Cells[2, 8].Value = "MÃ BÁO CÁO:TG/CLTG_V2";
            worksheet.Cells["H2:H2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["D2:E2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã bưu cục";
            worksheet.Cells[4, 3].Value = "Tên Bưu cục";
            worksheet.Cells[4, 4].Value = "Tổng Sản lượng";
            worksheet.Cells[4, 5].Value = "Sản lượng trượt";
            worksheet.Cells[4, 6].Value = "Tỉ lệ trượt";
            worksheet.Cells[4, 7].Value = "Sản lượng đúng";
            worksheet.Cells[4, 8].Value = "Tỉ lệ đúng";

            using (var range = worksheet.Cells["A4:H4"])
            using (var ranges = worksheet.Cells["A1:H1"])
            using (var Ngay = worksheet.Cells["D2:E2"])
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

        [HttpGet]
        public ActionResult ExportDetailPickup(string fromdate, string todate, string fromtime, string totime, string poscode, string unit, int routeid, string status, int? page, int datatype, int collectquality)
        {
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            ViewBag.fromtime = fromtime;
            ViewBag.totime = totime;
            ViewBag.poscode = poscode;
            ViewBag.unit = unit;
            ViewBag.routeid = routeid;
            ViewBag.status = status;
            ViewBag.page = page;
            ViewBag.datatype = datatype;
            ViewBag.collectquality = collectquality;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetailPickup();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chất lượng chi tiết thu gom.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        #endregion

        #region PICKUPSUCESSDQD
        [HttpGet]
        public List<KPIPickupSuccessDQD> ReturnListExcelPickupSuccessDQD(string StartDate, string EndDate, string district, string poscode, int routecode)
        {
            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPIPickupSuccessDQD returnquality = new ReturnKPIPickupSuccessDQD();
            returnquality = qualityKPIServiceRepository.KPIPickupSuccessDQD(common.DateToInt(StartDate), common.DateToInt(EndDate), district, poscode, routecode);
            return returnquality.ListKPIPickupSuccessDQDReport;
        }
        public Stream CreateExcelFilePickupSuccessDQD(Stream stream = null)
        {
            //var list = CreateTestItems();


            var list = ReturnListExcelPickupSuccessDQD(ViewBag.StartDate, ViewBag.EndDate, ViewBag.district, ViewBag.poscode, ViewBag.routecode);
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
                BindingFormatForExcelPickupupSuccess(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        private void BindingFormatForExcelPickupupSuccess(ExcelWorksheet worksheet, List<KPIPickupSuccessDQD> listItems)
        {
            var list = ReturnListExcelPickupSuccessDQD(ViewBag.StartDate, ViewBag.EndDate, ViewBag.district, ViewBag.poscode, ViewBag.routecode);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THU GOM THÀNH CÔNG ĐÚNG QUY ĐỊNH";
            worksheet.Cells["A1:J1"].Merge = true;

            worksheet.Cells[2, 10].Value = "MÃ BÁO CÁO:KD/TG/TGDQD";
            worksheet.Cells["J2:J2"].Merge = true;

            worksheet.Cells[2, 5].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["E2:F2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Bưu cục";
            worksheet.Cells[4, 3].Value = "Tuyến thu gom";
            worksheet.Cells[4, 4].Value = "Tổng số lượng tin";
            worksheet.Cells[4, 5].Value = "Số lượng tin ";
            worksheet.Cells[4, 6].Value = "Sản lượng ";
            worksheet.Cells[4, 7].Value = "Tỷ lệ ";
            worksheet.Cells[4, 8].Value = "Số lượng tin";
            worksheet.Cells[4, 9].Value = "Sản lượng ";
            worksheet.Cells[4, 10].Value = "Tỷ lệ ";

            using (var range = worksheet.Cells["A4:J4"])
            using (var ranges = worksheet.Cells["A1:J1"])
            using (var Ngay = worksheet.Cells["E2:F2"])
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

        [HttpGet]
        public ActionResult ExportPickupSuccessDQD(string StartDate, string EndDate, string district, string poscode, int routecode)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.district = district;
            ViewBag.poscode = poscode;
            ViewBag.routecode = routecode;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFilePickupSuccessDQD();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp thu gom.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        #endregion
        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<KPIPickupTotal> ReturnListExcel(int zone, int poscode, string StartDate, string EndDate)
        {
            ViewBag.zone = zone;
            ViewBag.poscode = poscode;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPIPickup returnquality = new ReturnKPIPickup();
            returnquality = qualityKPIServiceRepository.KPIPickup(zone, poscode, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListKPIPickupReport;
        }

        [HttpGet]
        public List<KPIPickupItem> ReturnListItemExcelCT(int timeinterval, int poscode, string startDate, string endDate)
        {
            ViewBag.poscode = poscode;
            ViewBag.Timeinterval = timeinterval;
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;

            KPIPickupRepository qualityKPIPickupRepository = new KPIPickupRepository();
            ReturnKPIPickupItem returnqualityItem = new ReturnKPIPickupItem();
            returnqualityItem = qualityKPIPickupRepository.KPIPickupItem(poscode, timeinterval, common.DateToInt(startDate), common.DateToInt(endDate));
            return returnqualityItem.ListKPIPickupItemReport;
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();


            var list = ReturnListExcel(ViewBag.zone, ViewBag.poscode, ViewBag.startDate, ViewBag.endDate);
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

        public Stream CreateExcelFileCT(Stream stream = null)
        {
            //var list = CreateTestItems();


            var list = ReturnListItemExcelCT(ViewBag.timeinterval, ViewBag.poscode, ViewBag.startDate, ViewBag.endDate);
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
                BindingFormatForExcelCT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<KPIPickupTotal> listItems)
        {
            var list = ReturnListExcel(ViewBag.zone, ViewBag.poscode, ViewBag.startDate, ViewBag.endDate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT LƯỢNG  THU GOM";
            worksheet.Cells["A1:L1"].Merge = true;

            worksheet.Cells[2, 12].Value = "MÃ BÁO CÁO:TTG/CLTG";
            worksheet.Cells["L2:L2"].Merge = true;

            worksheet.Cells[2, 6].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["F2:G2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Bưu cục";
            worksheet.Cells[4, 3].Value = "Tên bưu cục";
            worksheet.Cells[4, 4].Value = "Tổng sản lượng";
            worksheet.Cells[4, 5].Value = "Sản lượng < 5H";
            worksheet.Cells[4, 6].Value = "Tỉ Lệ< 5H";
            worksheet.Cells[4, 7].Value = "Sản Lượng từ 5H-10H";
            worksheet.Cells[4, 8].Value = "Tỉ Lệ từ 5H-10H";
            worksheet.Cells[4, 9].Value = "Sản Lượng > 10H";
            worksheet.Cells[4, 10].Value = "Tỉ Lệ > 10H";


            using (var range = worksheet.Cells["A4:L4"])
            using (var ranges = worksheet.Cells["A1:L1"])
            using (var Ngay = worksheet.Cells["F2:G2"])
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

        private void BindingFormatForExcelCT(ExcelWorksheet worksheet, List<KPIPickupItem> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "Bưu cục";
            worksheet.Cells[1, 4].Value = "Tên bưu cục";
            worksheet.Cells[1, 5].Value = "Mã tỉnh nhận";
            worksheet.Cells[1, 6].Value = "Tên tỉnh nhận";
            worksheet.Cells[1, 7].Value = "Ngày chấp nhận";
            worksheet.Cells[1, 8].Value = "Giờ chấp nhận";
            worksheet.Cells[1, 9].Value = "Ngày đến khai thác";
            worksheet.Cells[1, 10].Value = "Giờ đến khai thác";
            worksheet.Cells[1, 11].Value = "Tổng thời gian";
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

        [HttpGet]
        public ActionResult ExportTotalKPIPickUpV2(int zone, int poscode, string startdate, string enddate)
        {
            ViewBag.zone = zone;
            ViewBag.poscode = poscode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileTotalPickupV2();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp thu gom V2.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(int zone, int poscode, string startdate, string enddate)
        {
            ViewBag.zone = zone;
            ViewBag.poscode = poscode;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp thu gom.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ExportCT(int timeinterval, int poscode, string startdate, string enddate)
        {
            ViewBag.timeinterval = timeinterval;
            ViewBag.poscode = poscode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileCT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết thu gom '" + poscode + "'.xlsx");
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