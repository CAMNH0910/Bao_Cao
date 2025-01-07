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

    public class TKSLGDController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult Detail_TKSLGD()
        {
            return View();

        }

        [HttpPost]
        public JsonResult LisDetail_TKSLGD(string startdate, string enddate)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TKSLGDRepository Repository = new TKSLGDRepository();
            ResponseTKSLGD returnquality = new ResponseTKSLGD();
            returnquality = Repository.DETAIL_TKSLGD(common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<TKSLGD> ReturnListExcel_Detail_TKSLGD(string startdate, string enddate)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TKSLGDRepository Repository = new TKSLGDRepository();
            ResponseTKSLGD returnquality = new ResponseTKSLGD();
            returnquality = Repository.DETAIL_TKSLGD(common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.listDetail_TKSLGDReport;
        }


        public Stream CreateExcelFile_Detail_TKSLGD(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_Detail_TKSLGD(ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel_Detail_TKSLGD(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Detail_TKSLGD(ExcelWorksheet worksheet, List<TKSLGD> listItems)
        {
            var list = ReturnListExcel_Detail_TKSLGD(ViewBag.startdate, ViewBag.enddate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ SẢN LƯỢNG CHẤP NHẬN TẠI BƯU CỤC";
            worksheet.Cells["A1:R1"].Merge = true;

            worksheet.Cells[2, 18].Value = "MÃ BÁO CÁO:QLTN/CNTGD";
            worksheet.Cells["R2:R2"].Merge = true;

            worksheet.Cells[2, 8].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["H2:I2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Chi nhánh";
            worksheet.Cells[4, 3].Value = "Mã bưu cục";
            worksheet.Cells[4, 4].Value = "Tên bưu cục";
            worksheet.Cells[4, 5].Value = "Tổng SL";
            worksheet.Cells[4, 6].Value = "Tỷ lệ";
            worksheet.Cells[4, 7].Value = "SL thủ công";
            worksheet.Cells[4, 8].Value = "Tỷ lệ thủ công";

            worksheet.Cells[4, 9].Value = "SL import Excel";
            worksheet.Cells[4, 10].Value = "Tỷ lệ import Excel";
            worksheet.Cells[4, 11].Value = "SL EMSONE";
            worksheet.Cells[4, 12].Value = "Tỷ lệ  EMSONE";
            worksheet.Cells[4, 13].Value = "SL VDDT";
            worksheet.Cells[4, 14].Value = "Tỷ lệ VDDT";
            worksheet.Cells[4, 15].Value = "SL MCS";
            worksheet.Cells[4, 16].Value = "Tỷ lệ MCS";
            worksheet.Cells[4, 17].Value = "SL Khác";
            worksheet.Cells[4, 18].Value = "Tỷ lệ Khác";


            using (var range = worksheet.Cells["A4:R4"])
            using (var ranges = worksheet.Cells["A1:R1"])
            using (var Ngay = worksheet.Cells["H2:I2"])
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
        public ActionResult Export_Detail_TKSLGD(string startdate, string enddate)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_Detail_TKSLGD();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê chấp nhận từ BC GD " + startdate + " - " + enddate + ".xlsx");
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