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

    public class THTCKTController : Controller
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
        public ActionResult THTCKT()
        {
            return View();

        }
        public ActionResult THTCKTQT()
        {
            return View();

        }
        public ActionResult THSLTC()
        {
            return View();

        }

        public ActionResult THSLTCQT()
        {
            return View();

        }
        public ActionResult CTSLTC()
        {
            return View();

        }

        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        // [HttpPost]
        //public ActionResult ListTHTCReport( string buucuc, string startdate, string enddate)
        //{           
        //    ViewBag.buucuc = buucuc;          
        //    ViewBag.startdate = startdate;
        //    ViewBag.enddate = enddate;

        //    KPIKTRepository KPIKTRepository = new KPIKTRepository();
        //    ReturnKPIKT returnKPIKT = new ReturnKPIKT();
        //    returnKPIKT = KPIKTRepository.THTC(buucuc,common.DateToInt(startdate), common.DateToInt(enddate) );
        //    return View(returnKPIKT);
        //   // return Json(returnKPIKT, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult ListTHTCReport(string buucuc, int dichvu, string startdate, string enddate)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.THTC(buucuc, dichvu, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListTHTCQTReport(string buucuc, string startdate, string enddate)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.THTCQT(buucuc, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Controller gọi đến tổng hợp sản lượng trượt chuyến
        [HttpPost]
        public JsonResult ListTHSLTCReport(int WorkCenter, int IDVnpost, int dichvu, string startdate, string enddate)
        {

            ViewBag.WorkCenter = WorkCenter;
            ViewBag.IDVnpost = IDVnpost;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.THSLTC(WorkCenter, IDVnpost, dichvu, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListTHSLTCQTReport(int WorkCenter, int IDVnpost, string startdate, string enddate)
        {

            ViewBag.WorkCenter = WorkCenter;
            ViewBag.IDVnpost = IDVnpost;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.THSLTCQT(WorkCenter, IDVnpost, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListCTSLTCReport(string ItemCode, string WorkCenter)
        {
            ViewBag.ItemCode = ItemCode;
            ViewBag.WorkCenter = WorkCenter;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.CTSLTC(ItemCode, WorkCenter);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //Phần trả về data theo list để xuất excel

        [HttpGet]
        public List<THTCDetail> ReturnListExcel(string buucuc, int dichvu, string startdate, string enddate)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.THTC(buucuc, dichvu, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListTHTCReport;
        }
        [HttpGet]
        public List<THSLTCDetail> ReturnListExcel(int WorkCenter, int IDVnpost, int dichvu, string startdate, string enddate)
        {

            ViewBag.WorkCenter = WorkCenter;
            ViewBag.IDVnpost = IDVnpost;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.THSLTC(WorkCenter, IDVnpost, dichvu, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListTHSLTCDetailReport;
        }



        [HttpGet]
        public List<THSLTCDetail> ReturnListExcelQT(int WorkCenter, int IDVnpost, string startdate, string enddate)
        {

            ViewBag.WorkCenter = WorkCenter;
            ViewBag.IDVnpost = IDVnpost;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.THSLTCQT(WorkCenter, IDVnpost, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListTHSLTCDetailReport;
        }

        [HttpGet]
        public List<CTSLTCDetail> ReturnListExcel(string ItemCode, string WorkCenter)
        {
            ViewBag.ItemCode = ItemCode;
            ViewBag.WorkCenter = WorkCenter;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.CTSLTC(ItemCode, WorkCenter);
            return returnquality.ListCTSLTCDetailReport;
        }



        public List<THTCDetail> ReturnListExcelTHQT(string buucuc, string startdate, string enddate)
        {
            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPIKTRepository qualityKPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnquality = new ReturnKPIKT();
            returnquality = qualityKPIKTRepository.THTCQT(buucuc, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListTHTCReport;
        }



        public Stream CreateExcelFileTHTCKT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.buucuc, ViewBag.dichvu, ViewBag.startdate, ViewBag.enddate);
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
                workSheet.Cells[6, 1].LoadFromCollection(list);
                BindingFormatForExcelTHTCKT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFileTHSLTC(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.WorkCenter, ViewBag.IDVnpost, ViewBag.dichvu, ViewBag.startdate, ViewBag.enddate);
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
                workSheet.Cells[3, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcelTHSLTC(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        public Stream CreateExcelFileTHSLTCQT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelQT(ViewBag.WorkCenter, ViewBag.IDVnpost, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcelTHSLTCQT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFileCTSLTC(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.ItemCode, ViewBag.WorkCenter);
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
                workSheet.Cells[3, 1].LoadFromCollection(list);
                BindingFormatForExcelCTSLTC(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        public Stream CreateExcelFileQT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelTHQT(ViewBag.buucuc, ViewBag.startdate, ViewBag.enddate);
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
                workSheet.Cells[6, 1].LoadFromCollection(list);
                BindingFormatForExceQT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

     

        //Phần sửa excel
        private void BindingFormatForExcelTHTCKT(ExcelWorksheet worksheet, List<THTCDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.buucuc, ViewBag.dichvu, ViewBag.startdate, ViewBag.enddate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO TỔNG HỢP SẢN LƯỢNG THEO HÀNH TRÌNH";
            worksheet.Cells["A1:O1"].Merge = true;

            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO:KT/THSL_HT";
            worksheet.Cells["O2:O2"].Merge = true;

            worksheet.Cells[2, 7].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.endDate;
            worksheet.Cells["G2:I2"].Merge = true;


            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Id hành trình";
            worksheet.Cells[4, 3].Value = "Tên hành trình";

            worksheet.Cells[4, 4].Value = "Tồn mang sang";
            worksheet.Cells["D4:E4"].Merge = true;
            worksheet.Cells[5, 4].Value = "Sản lượng";
            worksheet.Cells[5, 5].Value = "Khối lượng";

            worksheet.Cells[4, 6].Value = "Đến trong kỳ";
            worksheet.Cells["F4:G4"].Merge = true;

            worksheet.Cells[5, 6].Value = "Sản lượng";
            worksheet.Cells[5, 7].Value = "Khối lượng";

            worksheet.Cells[4, 8].Value = "Phải khai thác";
            worksheet.Cells["H4:I4"].Merge = true;
            worksheet.Cells[5, 8].Value = "Sản lượng";
            worksheet.Cells[5, 9].Value = "Khối lượng";


            worksheet.Cells[4, 10].Value = "Peak";
            worksheet.Cells["J4:K4"].Merge = true;
            worksheet.Cells[5, 10].Value = "Sản lượng";
            worksheet.Cells[5, 11].Value = "Khối lượng";


            worksheet.Cells[4, 12].Value = "Đã khai thác";
            worksheet.Cells["L4:M4"].Merge = true;
            worksheet.Cells[5, 12].Value = "Sản lượng";
            worksheet.Cells[5, 13].Value = "Khối lượng";

            worksheet.Cells[4, 14].Value = "Trượt chuyến";
            worksheet.Cells["N4:O4"].Merge = true;
            worksheet.Cells[5, 14].Value = "Sản lượng";
            worksheet.Cells[5, 15].Value = "Khối lượng";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:O5"])
            using (var ranges = worksheet.Cells["A1:O1"])
            using (var Ngay = worksheet.Cells["G2:I2"])
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
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }
        private void BindingFormatForExcelTHSLTC(ExcelWorksheet worksheet, List<THSLTCDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Trạng thái";
            worksheet.Cells[1, 3].Value = "Thời gian KT";
            worksheet.Cells[1, 4].Value = "Mã E1";
            worksheet.Cells[1, 5].Value = "Khối lượng (g)";
            worksheet.Cells[1, 6].Value = "ID VNPost";
            worksheet.Cells[1, 7].Value = "Tên hành trình";
            worksheet.Cells[1, 8].Value = "Bc lập BD10";
            worksheet.Cells[1, 9].Value = "Bc nhận BD10";
            worksheet.Cells[1, 10].Value = "Ngày lập BD10";
            worksheet.Cells[1, 11].Value = "T/G lập BD10";
            worksheet.Cells[1, 12].Value = "STT BD10";
            worksheet.Cells[1, 13].Value = "ID BD10";
            worksheet.Cells[1, 14].Value = "T/G quét túi";
            worksheet.Cells[1, 15].Value = "Tỉnh đóng CT";
            worksheet.Cells[1, 16].Value = "BC đóng CT";
            worksheet.Cells[1, 17].Value = "Tỉnh nhận CT";
            worksheet.Cells[1, 18].Value = "BC nhận CT";
            worksheet.Cells[1, 19].Value = "Số CT";
            worksheet.Cells[1, 20].Value = "Túi số";
            worksheet.Cells[1, 21].Value = "Ngày đóng CT";
            worksheet.Cells[1, 22].Value = "T/G đóng túi";
            worksheet.Cells[1, 23].Value = "Mã cổ túi";
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

        private void BindingFormatForExcelTHSLTCQT(ExcelWorksheet worksheet, List<THSLTCDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Trạng thái";
            worksheet.Cells[1, 3].Value = "Thời gian KT";
            worksheet.Cells[1, 4].Value = "Mã E1";
            worksheet.Cells[1, 5].Value = "Phân loại";
            worksheet.Cells[1, 6].Value = "Mã BC đến";
            worksheet.Cells[1, 7].Value = "Mã kho";
            worksheet.Cells[1, 8].Value = "Ngày đến";
            worksheet.Cells[1, 9].Value = "Giờ đến";
            worksheet.Cells[1, 10].Value = "Ngày đi";
            worksheet.Cells[1, 11].Value = "Giờ đi";
            worksheet.Cells[1, 12].Value = "Hướng";
            worksheet.Cells[1, 13].Value = "Khối lượng (g)";
            worksheet.Cells[1, 14].Value = "ID VNPost";
            worksheet.Cells[1, 15].Value = "Tên hành trình";
            worksheet.Cells[1, 16].Value = "Bc lập BD10";
            worksheet.Cells[1, 17].Value = "Bc nhận BD10";
            worksheet.Cells[1, 18].Value = "Ngày lập BD10";
            worksheet.Cells[1, 19].Value = "T/G lập BD10";
            worksheet.Cells[1, 20].Value = "STT BD10";
            worksheet.Cells[1, 21].Value = "ID BD10";
            worksheet.Cells[1, 22].Value = "T/G quét túi";
            worksheet.Cells[1, 23].Value = "Tỉnh đóng CT";
            worksheet.Cells[1, 24].Value = "BC đóng CT";
            worksheet.Cells[1, 25].Value = "Tỉnh nhận CT";
            worksheet.Cells[1, 26].Value = "BC nhận CT";
            worksheet.Cells[1, 27].Value = "Số CT";
            worksheet.Cells[1, 28].Value = "Túi số";
            worksheet.Cells[1, 29].Value = "Ngày đóng CT";
            worksheet.Cells[1, 30].Value = "T/G đóng túi";
            worksheet.Cells[1, 31].Value = "Mã cổ túi";
            using (var range = worksheet.Cells["A1:AF1"])
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



        private void BindingFormatForExcelCTSLTC(ExcelWorksheet worksheet, List<CTSLTCDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "BC đóng";
            worksheet.Cells[1, 4].Value = "Tên BC đóng";
            worksheet.Cells[1, 5].Value = "BC nhận";
            worksheet.Cells[1, 6].Value = "Tên BC nhận";
            worksheet.Cells[1, 7].Value = "Mã cổ túi";
            worksheet.Cells[1, 8].Value = "Mã BD10";
            worksheet.Cells[1, 9].Value = "ID Hành trình";
            worksheet.Cells[1, 10].Value = "Tên hành trình";
            worksheet.Cells[1, 11].Value = "Thời gian";
            worksheet.Cells[1, 12].Value = "Sự kiện";
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



        private void BindingFormatForExceQT(ExcelWorksheet worksheet, List<THTCDetail> listItems)
        {
            var list = ReturnListExcelTHQT(ViewBag.buucuc, ViewBag.startdate, ViewBag.enddate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO TỔNG HỢP SẢN LƯỢNG THEO HÀNH TRÌNH QUỐC TẾ";
            worksheet.Cells["A1:M1"].Merge = true;

            worksheet.Cells[2, 13].Value = "MÃ BÁO CÁO:KT/THSL_HTQT";
            worksheet.Cells["M2:M2"].Merge = true;

            worksheet.Cells[2, 6].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.endDate;
            worksheet.Cells["F2:H2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Id hành trình";
            worksheet.Cells[4, 3].Value = "Tên hành trình";

            worksheet.Cells[4, 4].Value = "Tồn mang sang";
            worksheet.Cells["D4:E4"].Merge = true;
            worksheet.Cells[5, 4].Value = "Sản lượng";
            worksheet.Cells[5, 5].Value = "Khối lượng";

            worksheet.Cells[4, 6].Value = "Đến trong kỳ";
            worksheet.Cells["F4:G4"].Merge = true;

            worksheet.Cells[5, 6].Value = "Sản lượng";
            worksheet.Cells[5, 7].Value = "Khối lượng";

            worksheet.Cells[4, 8].Value = "Phải khai thác";
            worksheet.Cells["H4:I4"].Merge = true;
            worksheet.Cells[5, 8].Value = "Sản lượng";
            worksheet.Cells[5, 9].Value = "Khối lượng";

            worksheet.Cells[4, 10].Value = "Đã khai thác";
            worksheet.Cells["J4:K4"].Merge = true;
            worksheet.Cells[5, 10].Value = "Sản lượng";
            worksheet.Cells[5, 11].Value = "Khối lượng";

            worksheet.Cells[4, 12].Value = "Trượt chuyến";
            worksheet.Cells["L4:M4"].Merge = true;
            worksheet.Cells[5, 12].Value = "Sản lượng";
            worksheet.Cells[5, 13].Value = "Khối lượng";


            using (var range = worksheet.Cells["A4:M5"])
            using (var ranges = worksheet.Cells["A1:M1"])
            using (var Ngay = worksheet.Cells["F2:H2"])
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
                 range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult ExportTHTCKT(string buucuc, int dichvu, string startdate, string enddate)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileTHTCKT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp trượt chuyến theo hành trình.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExportTHSLTC(int WorkCenter, int IDVnpost, int dichvu, string startdate, string enddate)
        {

            ViewBag.WorkCenter = WorkCenter;
            ViewBag.IDVnpost = IDVnpost;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileTHSLTC();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng trượt chuyến theo hành trình.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }




        [HttpGet]
        public ActionResult ExportCTSLTC(string ItemCode, string WorkCenter)
        {

            ViewBag.ItemCode = ItemCode;
            ViewBag.WorkCenter = WorkCenter;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileCTSLTC();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết sản lượng trượt chuyến theo hành trình.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }



        public ActionResult ExportQT(string buucuc, string startdate, string enddate)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileQT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết sản lượng trượt chuyến theo hành trình.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }



        public ActionResult ExportCTQT(int WorkCenter, int IDVnpost, string startdate, string enddate)
        {

            ViewBag.WorkCenter = WorkCenter;
            ViewBag.IDVnpost = IDVnpost;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileTHSLTCQT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết sản lượng trượt chuyến theo hành trình.xlsx");
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