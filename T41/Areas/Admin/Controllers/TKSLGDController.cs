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
                workSheet.Cells[2, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel_Detail_TKSLGD(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Detail_TKSLGD(ExcelWorksheet worksheet, List<TKSLGD> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Chi nhánh";
            worksheet.Cells[1, 3].Value = "Mã BC";
            worksheet.Cells[1, 4].Value = "Tên BC";
            worksheet.Cells[1, 5].Value = "Tổng số BG chấp nhận";
            worksheet.Cells[1, 6].Value = "Tỷ lệ";
            worksheet.Cells[1, 7].Value = "Số BG chấp nhận TC";
            worksheet.Cells[1, 8].Value = "Tỷ lệ";
            worksheet.Cells[1, 9].Value = "Số BG chấp nhận qua import Excel";
            worksheet.Cells[1, 10].Value = "Tỷ lệ";
            worksheet.Cells[1, 11].Value = "Số BG chấp nhận qua EMS ONE";
            worksheet.Cells[1, 12].Value = "Tỷ lệ";
            worksheet.Cells[1, 13].Value = "Số BG chấp nhận qua web vận đơn ĐT";
            worksheet.Cells[1, 14].Value = "Tỷ lệ";
            worksheet.Cells[1, 15].Value = "Số BG chấp nhận qua API,MCS";
            worksheet.Cells[1, 16].Value = "Tỷ lệ";

            using (var range = worksheet.Cells["A1:Q1"])
            {
                range.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Top.Color.SetColor(Color.Black);
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Bottom.Color.SetColor(Color.Black);
                range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Left.Color.SetColor(Color.Black);
                range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Right.Color.SetColor(Color.Black);
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê chấp nhận từ BC GD "+startdate+" - "+enddate+".xlsx");
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