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

    public class KPIKTController : Controller
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
        public ActionResult THSL()
        {
            return View();

        }

        public ActionResult THSLQT()
        {
            return View();

        }




        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        [HttpPost]
        public JsonResult ListTHSLReport(string buucuc, int dichvu, string startdate, string enddate)
        {
            ViewBag.buucuc = buucuc;

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnKPIKT = new ReturnKPIKT();
            returnKPIKT = KPIKTRepository.THSL(buucuc, dichvu, common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returntrackingorder);
            return Json(returnKPIKT, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]


        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<THSLDetail> ReturnListExcel(string buucuc, int dichvu, string startdate, string enddate)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnKPIKT = new ReturnKPIKT();
            returnKPIKT = KPIKTRepository.THSL(buucuc, dichvu, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnKPIKT.ListTHSLReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
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
                workSheet.Cells[4, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<THSLDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.buucuc, ViewBag.dichvu, ViewBag.startdate, ViewBag.enddate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT LƯỢNG KPI KHAI THÁC";
            worksheet.Cells["A1:H1"].Merge = true;

            worksheet.Cells[2, 8].Value = "MÃ BÁO CÁO:KT/THSL";
            worksheet.Cells["H2:H2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Từ ngày:" +" "+ViewBag.startdate + " " + "Đến ngày" +ViewBag.enddate;
            worksheet.Cells["D2:E2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã bưu cục";
            worksheet.Cells[4, 3].Value = "Sản lượng";
            worksheet.Cells[4, 4].Value = "Khối lượng";
            worksheet.Cells[4, 5].Value = "Sản lượng trượt chuyến";
            worksheet.Cells[4, 6].Value = "Khối lượng trượt chuyến";
            worksheet.Cells[4, 7].Value = "Tỷ lệ sản lượng trượt chuyến";
            worksheet.Cells[4, 8].Value = "Tỷ lệ khối lượng trượt chuyến";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string buucuc, int dichvu, string startdate, string enddate)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.dichvu = dichvu;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng trượt chuyến.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }





        public JsonResult ListTHSLQTReport(string buucuc, string startdate, string enddate)
        {
            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnKPIKT = new ReturnKPIKT();
            returnKPIKT = KPIKTRepository.THSLQT(buucuc, common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returntrackingorder);
            return Json(returnKPIKT, JsonRequestBehavior.AllowGet);
        }


        public List<THSLDetail> ReturnListExcelQT(string buucuc, string startdate, string enddate)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnKPIKT = new ReturnKPIKT();
            returnKPIKT = KPIKTRepository.THSLQT(buucuc, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnKPIKT.ListTHSLReport;
        }


        public Stream CreateExcelFileQT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelQT(ViewBag.buucuc, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcelQT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcelQT(ExcelWorksheet worksheet, List<THSLDetail> listItems)
        {
            var list = ReturnListExcelQT(ViewBag.buucuc, ViewBag.startdate, ViewBag.enddate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO TỔNG HỢP SẢN LƯỢNG QUỐC TẾ";
            worksheet.Cells["A1:H1"].Merge = true;

            worksheet.Cells[2, 8].Value = "MÃ BÁO CÁO:KT/THSL_QT";
            worksheet.Cells["H2:H2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.endDate;
            worksheet.Cells["D2:E2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã bưu cục";
            worksheet.Cells[4, 3].Value = "Sản lượng";
            worksheet.Cells[4, 4].Value = "Khối lượng";
            worksheet.Cells[4, 5].Value = "Sản lượng trượt chuyến";
            worksheet.Cells[4, 6].Value = "Khối lượng trượt chuyến";
            worksheet.Cells[4, 7].Value = "Tỷ lệ sản lượng trượt chuyến";
            worksheet.Cells[4, 8].Value = "Tỷ lệ khối lượng trượt chuyến";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:H4"])
            using (var ranges = worksheet.Cells["A1:F1"])
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

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng trượt chuyến.xlsx");
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