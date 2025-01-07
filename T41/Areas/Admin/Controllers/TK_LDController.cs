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

    public class TK_LDController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult Detail_TK_LD()
        {
            return View();

        }

        public ActionResult Detail_TK_CT_LD()
        {
            return View();

        }

        [HttpPost]
        public JsonResult LisDetail_TK_LD(int endpostcode, int routecode,string startdate, string enddate)
        {
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TK_LDRepository Repository = new TK_LDRepository();
            ResponseDetail_TK_LD returnquality = new ResponseDetail_TK_LD();
            returnquality = Repository.DETAIL_TK_LD(endpostcode, routecode,common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult LisDetail_TK_CT_LD(int startpostcode, int routecode, string startdate, string enddate)
        {
            ViewBag.startpostcode = startpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TK_LDRepository Repository = new TK_LDRepository();
            ResponseDetail_TK_CT_LD returnquality = new ResponseDetail_TK_CT_LD();
            returnquality = Repository.DETAIL_TK_CT_LD(startpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_TK_LD> ReturnListExcel_Detail_TK_LD(int endpostcode, int routecode, string startdate, string enddate)
        {
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TK_LDRepository Repository = new TK_LDRepository();
            ResponseDetail_TK_LD returnquality = new ResponseDetail_TK_LD();
            returnquality = Repository.DETAIL_TK_LD(endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.listDetail_TK_LDReport;
        }


        public Stream CreateExcelFile_Detail_TK_LD(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_Detail_TK_LD(ViewBag.endpostcode, ViewBag.routecode,ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel_Detail_TK_LD(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Detail_TK_LD(ExcelWorksheet worksheet, List<Detail_TK_LD> listItems)
        {
            var list = ReturnListExcel_Detail_TK_LD(ViewBag.endpostcode, ViewBag.routecode, ViewBag.startdate, ViewBag.enddate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ LÝ DO PHÁT THẤT BẠI";
            worksheet.Cells["A1:K1"].Merge = true;

            worksheet.Cells[2, 11].Value = "MÃ BÁO CÁO:BT/TK_PTB";
            worksheet.Cells["K2:K2"].Merge = true;

            worksheet.Cells[2, 5].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["E2:G2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
                                       
            worksheet.Cells[4, 2].Value = "Mã BC Phát";
                                       
            worksheet.Cells[4, 3].Value = "Tên BC Phát";
                                       
            worksheet.Cells[4, 4].Value = "Mã tuyến";
                                       
            worksheet.Cells[4, 5].Value = "Tên tuyến phát";
                                       
            worksheet.Cells[4, 6].Value = "Mã nhân viên";
                                       
            worksheet.Cells[4, 7].Value = "ID Bưu tá";
                                       
            worksheet.Cells[4, 8].Value = "Họ tên";
            worksheet.Cells[4, 9].Value = "Tổng cộng";
            worksheet.Cells[4, 10].Value = "Mã lý do";
            worksheet.Cells[4, 11].Value = "Tên lý do";



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

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_Detail_TK_LD(int endpostcode, int routecode,string startdate, string enddate)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_Detail_TK_LD();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê lý do phát không thành công từ "+startdate+" - "+enddate+".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_TK_CT_LD> ReturnListExcel_Detail_TK_CT_LD(int startpostcode, int routecode, string startdate, string enddate)
        {
            ViewBag.startpostcode = startpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            TK_LDRepository Repository = new TK_LDRepository();
            ResponseDetail_TK_CT_LD returnquality = new ResponseDetail_TK_CT_LD();
            returnquality = Repository.DETAIL_TK_CT_LD(startpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.Data;
        }


        public Stream CreateExcelFile_Detail_TK_CT_LD(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_Detail_TK_CT_LD(ViewBag.startpostcode, ViewBag.routecode, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel_Detail_TK_CT_LD(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Detail_TK_CT_LD(ExcelWorksheet worksheet, List<Detail_TK_CT_LD> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";

            worksheet.Cells[1, 2].Value = "Mã E1";

            worksheet.Cells[1, 3].Value = "Mã BC Phát";

            worksheet.Cells[1, 4].Value = "Tên BC Phát";

            worksheet.Cells[1, 5].Value = "Mã tuyến";

            worksheet.Cells[1, 6].Value = "Tên tuyến phát";

            worksheet.Cells[1, 7].Value = "Mã nhân viên";

            worksheet.Cells[1, 8].Value = "ID Bưu tá";

            worksheet.Cells[1, 9].Value = "Họ tên";
            worksheet.Cells[1, 10].Value = "Thời gian";
            worksheet.Cells[1, 11].Value = "Mã lý do";
            worksheet.Cells[1, 12].Value = "Tên lý do";



            using (var range = worksheet.Cells["A1:X3"])
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
        public ActionResult Export_Detail_TK_CT_LD(int startpostcode, int routecode, string startdate, string enddate)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.startpostcode = startpostcode;
            ViewBag.routecode = routecode;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_Detail_TK_CT_LD();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Thống kê chi tiết lý do phát không thành công từ " + startdate + " - " + enddate + ".xlsx");
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