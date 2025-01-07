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

    public class TKSLController : Controller
    {

        public ActionResult Detail_TKSL()
        {
            return View();

        }
        public ActionResult Detail_CT_TKSL()
        {
            return View();

        }

        [HttpPost]
        public JsonResult LisDetail_TKSL(string startdate, string enddate)
        {
                  
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TKSLRepository Repository = new TKSLRepository();
            ResponseDetail_TKSL returnquality = new ResponseDetail_TKSL();
            returnquality = Repository.DETAIL_TKSL(startdate, enddate);
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_TKSL> ReturnListExcel_Detail_TKSL(string startdate, string enddate)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TKSLRepository Repository = new TKSLRepository();
            ResponseDetail_TKSL returnquality = new ResponseDetail_TKSL();
            returnquality = Repository.DETAIL_TKSL(startdate, enddate);
            return returnquality.listDetail_TKSLReport;
        }


        public Stream CreateExcelFile_Detail_TKSL(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_Detail_TKSL(ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel_Detail_TKSL(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Detail_TKSL(ExcelWorksheet worksheet, List<Detail_TKSL> listItems)
        {
            var list = ReturnListExcel_Detail_TKSL(ViewBag.startdate, ViewBag.enddate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ SỬ DỤNG TMP";
            worksheet.Cells["A1:I1"].Merge = true;

            worksheet.Cells[2, 9].Value = "MÃ BÁO CÁO:BT/TMP";
            worksheet.Cells["I2:I2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["D2:F2"].Merge = true;

            worksheet.Cells[4, 1].Value = "Bưu cục phát";
            worksheet.Cells[4, 2].Value = "Tên BC phát";
            worksheet.Cells[4, 3].Value = "Mã tuyến";
            worksheet.Cells[4, 4].Value = "Tên tuyến";
            worksheet.Cells[4, 5].Value = "Tổng SL";
            worksheet.Cells[4, 6].Value = "SL nhập SMP";
            worksheet.Cells[4, 7].Value = "TL nhập SMP";
            worksheet.Cells[4, 8].Value = "SL không nhập SMP"; 
            worksheet.Cells[4, 9].Value = "TL không nhập SMP";

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

                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            }
        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_Detail_TKSL(string startdate, string enddate)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_Detail_TKSL();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê sử dụng SMP từ "+startdate+" - "+enddate+".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }



        [HttpPost]
        public JsonResult ListDetail_CT_TKSL(int startposcode,int endposcode,string startdate, string enddate)
        {
                  
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TKSLRepository Repository = new TKSLRepository();
            ResponseDetail_CT_TKSL returnquality = new ResponseDetail_CT_TKSL();
            returnquality = Repository.DETAIL_CT_TKSL(startposcode, endposcode,startdate, enddate);
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_CT_TKSL> ReturnListExcel_Detail_CT_TKSL(int startposcode, int endposcode, string startdate, string enddate)
        {
            ViewBag.startposcode = startposcode;
            ViewBag.endposcode = endposcode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TKSLRepository Repository = new TKSLRepository();
            ResponseDetail_CT_TKSL returnquality = new ResponseDetail_CT_TKSL();
            returnquality = Repository.DETAIL_CT_TKSL(startposcode, endposcode, startdate, enddate);
            return returnquality.listDetail_CT_TKSLReport;
        }


        public Stream CreateExcelFile_Detail_CT_TKSL(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_Detail_CT_TKSL(ViewBag.startposcode, ViewBag.endposcode,ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel_Detail_CT_TKSL(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Detail_CT_TKSL(ExcelWorksheet worksheet, List<Detail_CT_TKSL> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "Mã bưu gửi";
            worksheet.Cells[1, 2].Value = "Bưu cục phát";
            worksheet.Cells[1, 3].Value = "Tên BC phát";
            worksheet.Cells[1, 4].Value = "Mã tuyến";
            worksheet.Cells[1, 5].Value = "Tên tuyến";
            worksheet.Cells[1, 6].Value = "Thời gian lập bản kê"; 
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
        public ActionResult Export_Detail_CT_TKSL(int startposcode,int endposcode,string startdate, string enddate)
        {
            ViewBag.startposcode = startposcode;
            ViewBag.endposcode = endposcode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_Detail_CT_TKSL();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê chi tiết không sử dụng SMP từ "+startdate+" - "+enddate+".xlsx");
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