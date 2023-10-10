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

    public class KPIPickupLogController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();      
        public ActionResult Index()
        {
            return View();
        }

      
        public JsonResult PosCode()
        {
            KPIPickupRepository kpipickupRepository = new KPIPickupRepository();
            return Json(kpipickupRepository.GetAllPOSCODELOG(), JsonRequestBehavior.AllowGet);
          
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
        public ActionResult KPIPickupLog()
        {
            return View();

        }

        public ActionResult ListItemDetailReport()
        {
            return View();

        }
     

      

        [HttpPost]
        public JsonResult ListDetailTotalKPIPickup(int poscode, string StartDate, string EndDate)
        {
          
           // ViewBag.zone = zone;          
            ViewBag.poscode = poscode;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPIPickup returnquality = new ReturnKPIPickup();
            returnquality = qualityKPIServiceRepository.KPIPickupLOG( poscode, common.DateToInt(StartDate), common.DateToInt(EndDate));       
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListDetailItemKPIPickup(int poscode, string startDate, string endDate)
        {

            ViewBag.poscode = poscode;           
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;

            KPIPickupRepository qualityKPIPickupRepository = new KPIPickupRepository();
            ReturnKPIPickupItem returnquality = new ReturnKPIPickupItem();
            returnquality = qualityKPIPickupRepository.KPIPickupItemLog(poscode, common.DateToInt(startDate), common.DateToInt(endDate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
            //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<KPIPickupTotal> ReturnListExcel(int poscode, string StartDate, string EndDate)
        {
           
            ViewBag.poscode = poscode;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIPickupRepository qualityKPIServiceRepository = new KPIPickupRepository();
            ReturnKPIPickup returnquality = new ReturnKPIPickup();
            returnquality = qualityKPIServiceRepository.KPIPickupLOG( poscode, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListKPIPickupReport;
        }

        [HttpGet]
        public List<KPIPickupItem> ReturnListItemExcelCT( int poscode, string startDate, string endDate)
        {
            ViewBag.poscode = poscode;
          
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;

            KPIPickupRepository qualityKPIPickupRepository = new KPIPickupRepository();
            ReturnKPIPickupItem returnqualityItem = new ReturnKPIPickupItem();
            returnqualityItem = qualityKPIPickupRepository.KPIPickupItemLog(poscode, common.DateToInt(startDate), common.DateToInt(endDate));
            return returnqualityItem.ListKPIPickupItemReport;
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
         
          
            var list = ReturnListExcel( ViewBag.poscode, ViewBag.startDate, ViewBag.endDate);
            List<KPIPickupTotalExcel> excelList = new List<KPIPickupTotalExcel>();
            foreach (KPIPickupTotal totalItem in list)
            {
                KPIPickupTotalExcel excelItem = new KPIPickupTotalExcel
                {
                    STT = totalItem.STT,
                    StartPostCode = totalItem.StartPostCode,
                    StartPostCodeName = totalItem.StartPostCodeName,
                    Total = totalItem.Total,
                    TimeintervalUpTo6 = totalItem.TimeintervalUpTo6,
                    TLTimeintervalUpTo6 = totalItem.TLTimeintervalUpTo6
                };
                excelList.Add(excelItem);
            }

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
                workSheet.Cells[4, 1].LoadFromCollection(excelList, true, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFileCT(Stream stream = null)
        {
            //var list = CreateTestItems();


            var list = ReturnListItemExcelCT(ViewBag.poscode, ViewBag.startDate, ViewBag.endDate);
            List<KPIPickupItemExcel> excelList = new List<KPIPickupItemExcel>();
            foreach (KPIPickupItem item in list)
            {
                KPIPickupItemExcel excelItem = new KPIPickupItemExcel
                {
                    STT = item.STT,
                    ItemCode = item.ItemCode,
                    StartPostCode = item.StartPostCode,
                    StartPostCodeName = item.StartPostCodeName,
                    EndProvince = item.EndProvince,
                    EndProvinceName = item.EndProvinceName,
                    A1StatusDate = item.A1StatusDate,
                    A1StatusTime = item.A1StatusTime,
                    A2StatusDate = item.A2StatusDate,
                    A2StatusTime = item.A2StatusTime
                };
                excelList.Add(excelItem);
            }
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
                workSheet.Cells[1, 1].LoadFromCollection(excelList, true, TableStyles.Dark9);
                BindingFormatForExcelCT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<KPIPickupTotal> listItems)
        {
            var list = ReturnListExcel(ViewBag.poscode, ViewBag.startDate, ViewBag.endDate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT LƯỢNG THU GOM KHL";
            worksheet.Cells["A1:F1"].Merge = true;

            worksheet.Cells[2, 6].Value = "MÃ BÁO CÁO:TG_CLTG_KHL";
            worksheet.Cells["F2:F2"].Merge = true;

            worksheet.Cells[2, 3].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.endDate;
            worksheet.Cells["C2:D2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Bưu cục";
            worksheet.Cells[4, 3].Value = "Tên bưu cục";
            worksheet.Cells[4, 4].Value = "Tổng sản lượng";
            worksheet.Cells[4, 5].Value = "Sản lượng trượt";
            worksheet.Cells[4, 6].Value = "Tỉ Lệ trượt";

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

        private void BindingFormatForExcelCT(ExcelWorksheet worksheet, List<KPIPickupItem> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
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
            worksheet.Cells[1, 9].Value = "Ngày Giao đi";
            worksheet.Cells[1, 10].Value = "Giờ Giao đi";
          
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
        public ActionResult Export(int poscode, string startdate, string enddate)
        {
           
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
        public ActionResult ExportCT( int poscode, string startdate, string enddate)
        {
           
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết thu gom '"+ poscode + "'.xlsx");
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