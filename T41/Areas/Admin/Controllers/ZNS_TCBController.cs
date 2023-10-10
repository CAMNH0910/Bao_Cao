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
using System.Security.Policy;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Spreadsheet;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;
using TableStyles = OfficeOpenXml.Table.TableStyles;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace T41.Areas.Admin.Controllers
{
    public class ZNS_TCBController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ZNS_TCBReport()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListZNS_TCB(string Customer, string startdate, string enddate)
        {
            ViewBag.Customer = Customer;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            ZNS_TCB_Repository kPI_CustomerRepository = new ZNS_TCB_Repository();
            ReturnZNS_TCB returnquality = new ReturnZNS_TCB();
            returnquality = kPI_CustomerRepository.ZNS_TCB_DETAIL(Customer, common.DateToInt(startdate), common.DateToInt(enddate));

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public List<ZNS_TCB> ReturnListExcel(string Customer, string startdate, string enddate)
        {

            ViewBag.Customer = Customer;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            ZNS_TCB_Repository kPI_CustomerRepository = new ZNS_TCB_Repository();
            ReturnZNS_TCB returnquality = new ReturnZNS_TCB();
            returnquality = kPI_CustomerRepository.ZNS_TCB_DETAIL(Customer, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListZNS_TCB;
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.Customer, ViewBag.startdate, ViewBag.enddate);
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
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<ZNS_TCB> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Số điện thoại";
            worksheet.Cells[1, 3].Value = "Mã Khách hàng";
            worksheet.Cells[1, 4].Value = "Mã bưu gửi";
            worksheet.Cells[1, 5].Value = "T/G giao";
            worksheet.Cells[1, 6].Value = "T/G KTC 1";
            worksheet.Cells[1, 7].Value = "T/G KTC 2";
            worksheet.Cells[1, 8].Value = "T/G KTC 3";
            worksheet.Cells[1, 9].Value = "T/G PTC";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:I1"])
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
                //Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                // ranges.Style.Font.SetFromFont(new Font("Arial", 14));
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string Customer, string startdate, string enddate)
        {
            ViewBag.Customer = Customer;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đến BCP.xlsx");
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