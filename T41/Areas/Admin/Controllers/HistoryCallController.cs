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

    public class HistoryCallController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult THHistoryCall()
        {
            return View();

        }

        public ActionResult CTHistoryCall()
        {
            return View();

        }

        [HttpPost]
        public JsonResult ListTHHistoryCall(int postcode, int routecode, int isservice, string startdate, string enddate)
        {
            ViewBag.postcode = postcode;
            ViewBag.isservice = isservice;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            HistoryCallRepository Repository = new HistoryCallRepository();
            ReturnTHHistoryCall returnquality = new ReturnTHHistoryCall();
            returnquality = Repository.DETAIL_TH_HISTORYCALL(postcode, routecode, isservice, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<HistoryCall> ReturnListExcel_TH_HISTORYCALL(int postcode, int routecode, int isservice, string startdate, string enddate)
        {
            ViewBag.postcode = postcode;
            ViewBag.isservice = isservice;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            HistoryCallRepository Repository = new HistoryCallRepository();
            ReturnTHHistoryCall returnquality = new ReturnTHHistoryCall();
            returnquality = Repository.DETAIL_TH_HISTORYCALL(postcode, routecode, isservice, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            return returnquality.listTHHistoryCallReport;
        }


        public Stream CreateExcelFile_TH_HISTORYCALL(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_TH_HISTORYCALL(ViewBag.postcode, ViewBag.routecode, ViewBag.isservice, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel_TH_HISTORYCALL(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_TH_HISTORYCALL(ExcelWorksheet worksheet, List<HistoryCall> listItems)
        {
            var list = ReturnListExcel_TH_HISTORYCALL(ViewBag.postcode, ViewBag.routecode, ViewBag.isservice, ViewBag.startdate, ViewBag.enddate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ TỔNG HỢP CUỘC GỌI";
            worksheet.Cells["A1:I1"].Merge = true;

            worksheet.Cells[2, 9].Value = "MÃ BÁO CÁO:BT/THCG";
            worksheet.Cells["I2:I2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["D2:F2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Khu vực";
            worksheet.Cells[4, 3].Value = "Ngày";
            worksheet.Cells[4, 4].Value = "Mã BC Phát";
            worksheet.Cells[4, 5].Value = "Tên bưu tá";
            worksheet.Cells[4, 6].Value = "Tổng SL đi phát ";
            worksheet.Cells[4, 7].Value = "Bưu gửi DingDong có E1";
            worksheet.Cells[4, 8].Value = "Tỷ lệ DingDong có E1";
            worksheet.Cells[4, 9].Value = "Tỷ lệ gọi qua DingDong";

            using (var range = worksheet.Cells["A4:I4"])
            using (var ranges = worksheet.Cells["A1:I1"])
            using (var Ngay = worksheet.Cells["D2:F2"])
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
        public ActionResult Export_TH_HISTORYCALL(int postcode, int routecode, int isservice, string startdate, string enddate)
        {

            ViewBag.postcode = postcode;
            ViewBag.routecode = routecode;
            ViewBag.isservice = isservice;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_TH_HISTORYCALL();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê tổng hợp lịch sử cuộc gọi từ " + startdate + " - " + enddate + ".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ListCTHistoryCall(int postcode, int routecode, int isservice, string startdate, string enddate)
        {
            ViewBag.postcode = postcode;
            ViewBag.isservice = isservice;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            HistoryCallRepository Repository = new HistoryCallRepository();
            ReturnCTHistoryCall returnquality = new ReturnCTHistoryCall();
            returnquality = Repository.DETAIL_CT_HISTORYCALL(postcode, routecode, isservice, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<DetailHistoryCall> ReturnListExcel_CT_HISTORYCALL(int postcode, int routecode, int isservice, string startdate, string enddate)
        {
            ViewBag.postcode = postcode;
            ViewBag.routecode = routecode;
            ViewBag.isservice = isservice;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            HistoryCallRepository Repository = new HistoryCallRepository();
            ReturnCTHistoryCall returnquality = new ReturnCTHistoryCall();
            returnquality = Repository.DETAIL_CT_HISTORYCALL(postcode, routecode, isservice, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            return returnquality.listCTHistoryCallReport;
        }


        public Stream CreateExcelFile_CT_HISTORYCALL(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_CT_HISTORYCALL(ViewBag.postcode, ViewBag.routecode, ViewBag.isservice, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel_CT_HISTORYCALL(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_CT_HISTORYCALL(ExcelWorksheet worksheet, List<DetailHistoryCall> listItems)
        {
            var list = ReturnListExcel_CT_HISTORYCALL(ViewBag.postcode, ViewBag.routecode, ViewBag.isservice, ViewBag.startdate, ViewBag.enddate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ CHI TIẾT CUỘC GỌI";
            worksheet.Cells["A1:L1"].Merge = true;

            worksheet.Cells[2, 12].Value = "MÃ BÁO CÁO:BT/CTCG";
            worksheet.Cells["L2:L2"].Merge = true;

            worksheet.Cells[2, 6].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["F2:G2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Tỉnh phát";
            worksheet.Cells[4, 3].Value = "Mã BC Phát";
            worksheet.Cells[4, 4].Value = "Mã tuyến";
            worksheet.Cells[4, 5].Value = "Mã bưu tá";
            worksheet.Cells[4, 6].Value = "Tên bưu tá";
            worksheet.Cells[4, 7].Value = "Mã bưu gửi";
            worksheet.Cells[4, 8].Value = "SDT Người nhận";
            worksheet.Cells[4, 9].Value = "Lượt gọi trong ngày";
            worksheet.Cells[4, 10].Value = "TG bắt đầu gọi";
            worksheet.Cells[4, 11].Value = "TG kết thúc gọi";
            worksheet.Cells[4, 12].Value = "Trang thái gọi";



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

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_CT_HISTORYCALL(int postcode, int routecode, int isservice, string startdate, string enddate)
        {

            ViewBag.postcode = postcode;
            ViewBag.routecode = routecode;
            ViewBag.isservice = isservice;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_CT_HISTORYCALL();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê chi tiết lịch sử cuộc gọi từ " + startdate + " - " + enddate + ".xlsx");
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