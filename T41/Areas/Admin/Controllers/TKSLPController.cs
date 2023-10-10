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

    public class TKSLPController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult Detail_TKSLP()
        {
            return View();

        }

        public ActionResult Detail_CTKTT_TKSLP()
        {
            return View();

        }
        [HttpPost]
        public JsonResult LisDetail_TKSLP(int Postman, int endpostcode, int routecode, string startdate, string enddate, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TKSLPRepository Repository = new TKSLPRepository();
            ResponseDetail_TKSLP returnquality = new ResponseDetail_TKSLP();
            returnquality = Repository.DETAIL_TKSLP(Postman, endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_TKSLP> ReturnListExcel_Detail_TKSLP(int postman, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            ViewBag.postman = postman;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.service = service;

            TKSLPRepository Repository = new TKSLPRepository();
            ResponseDetail_TKSLP returnquality = new ResponseDetail_TKSLP();
            returnquality = Repository.DETAIL_TKSLP(postman, endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            return returnquality.listDetail_TKSLPReport;
        }


        public Stream CreateExcelFile_Detail_TKSLP(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_Detail_TKSLP(ViewBag.postman, ViewBag.endpostcode, ViewBag.routecode, ViewBag.startdate, ViewBag.enddate, ViewBag.service);
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
                workSheet.Cells[7, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel_Detail_TKSLP(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Detail_TKSLP(ExcelWorksheet worksheet, List<Detail_TKSLP> listItems)
        {
            var list = ReturnListExcel_Detail_TKSLP(ViewBag.postman, ViewBag.endpostcode, ViewBag.routecode, ViewBag.startdate, ViewBag.enddate, ViewBag.service);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ SẢN LƯỢNG PHÁT";
            worksheet.Cells["A1:Y1"].Merge = true;

            worksheet.Cells[2, 25].Value = "MÃ BÁO CÁO:BT/TKSLP";
            worksheet.Cells["Y2:Y2"].Merge = true;

            worksheet.Cells[2, 12].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["L2:N2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells["A4:A6"].Merge = true;
            worksheet.Cells[4, 2].Value = "Mã BC Phát";
            worksheet.Cells["B4:B6"].Merge = true;
            worksheet.Cells[4, 3].Value = "Tên BC Phát";
            worksheet.Cells["C4:C6"].Merge = true;
            worksheet.Cells[4, 4].Value = "Mã tuyến";
            worksheet.Cells["D4:D6"].Merge = true;
            worksheet.Cells[4, 5].Value = "Tên tuyến phát";
            worksheet.Cells["E4:E6"].Merge = true;
            worksheet.Cells[4, 6].Value = "Mã nhân viên";
            worksheet.Cells["F4:F6"].Merge = true;
            worksheet.Cells[4, 7].Value = "ID Bưu tá";
            worksheet.Cells["G4:G6"].Merge = true;
            worksheet.Cells[4, 8].Value = "Họ tên";
            worksheet.Cells["H4:H6"].Merge = true;
            worksheet.Cells[4, 9].Value = "Dịch vụ";
            worksheet.Cells["I4:I6"].Merge = true;
            worksheet.Cells[4, 10].Value = "SL đến";
            worksheet.Cells["J4:J6"].Merge = true;
            worksheet.Cells[4, 11].Value = "SL phát thành công";
            worksheet.Cells["K4:K6"].Merge = true;
            worksheet.Cells[4, 12].Value = "SL phát thành công 72H";
            worksheet.Cells["L4:L6"].Merge = true;
            worksheet.Cells[4, 13].Value = "SL phát TC đúng quy định";
            worksheet.Cells["M4:P4"].Merge = true;
            worksheet.Cells[5, 13].Value = "Đến 2kg";
            worksheet.Cells["M5:N5"].Merge = true;
            worksheet.Cells[6, 13].Value = "Sản lượng";
            worksheet.Cells[6, 14].Value = "Khối lượng";
            worksheet.Cells[5, 15].Value = "Trên 2kg";
            worksheet.Cells["O5:P5"].Merge = true;
            worksheet.Cells[6, 15].Value = "Sản lượng";
            worksheet.Cells[6, 16].Value = "Khối lượng";
            worksheet.Cells[5, 17].Value = "Tổng cộng";
            worksheet.Cells["Q5:R5"].Merge = true;
            worksheet.Cells[6, 17].Value = "Sản lượng";
            worksheet.Cells[6, 18].Value = "Khối lượng";
            worksheet.Cells[4, 19].Value = "SL phát TC không đúng quy định";
            worksheet.Cells["S4:X4"].Merge = true;
            worksheet.Cells[5, 19].Value = "Đến 2kg";
            worksheet.Cells["S5:T5"].Merge = true;
            worksheet.Cells[6, 19].Value = "Sản lượng";
            worksheet.Cells[6, 20].Value = "Khối lượng";
            worksheet.Cells[5, 21].Value = "Trên 2kg";
            worksheet.Cells["U5:V5"].Merge = true;
            worksheet.Cells[6, 21].Value = "Sản lượng";
            worksheet.Cells[6, 22].Value = "Khối lượng";
            worksheet.Cells[5, 23].Value = "Tổng cộng";
            worksheet.Cells["W5:X5"].Merge = true;
            worksheet.Cells[6, 23].Value = "Sản lượng";
            worksheet.Cells[6, 24].Value = "Khối lượng";
            worksheet.Cells[4, 25].Value = "Sản phát không thành công";
            worksheet.Cells["Y4:Y6"].Merge = true;

            using (var range = worksheet.Cells["A4:Y6"])
            using (var ranges = worksheet.Cells["A1:I1"])
            using (var Ngay = worksheet.Cells["L2:N2"])
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
        public ActionResult Export_Detail_TKSLP(int postman, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            ViewBag.postman = postman;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.service = service;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_Detail_TKSLP();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê sản lượng phát từ " + startdate + " - " + enddate + ".xlsx");
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