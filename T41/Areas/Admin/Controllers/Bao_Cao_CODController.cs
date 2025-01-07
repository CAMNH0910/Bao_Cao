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
using DocumentFormat.OpenXml.Drawing;
using System.Security.Policy;


namespace T41.Areas.Admin.Controllers
{
    public class Bao_Cao_CODController : Controller
    {
        Convertion common = new Convertion();

        public ActionResult Bao_Cao_COD()
        {
            return View();

        }
        [HttpPost]
        public JsonResult ListBao_Cao_CODReport(string StartDate, string EndDate, string Ma_kh, string So_hieu, string Trang_thai)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.Ma_kh = Ma_kh;
            ViewBag.So_hieu = So_hieu;
            ViewBag.Trang_thai = Trang_thai;

            Bao_Cao_CODRepository baocao = new Bao_Cao_CODRepository();
            ReturnBao_Cao_COD ReturnBao_Cao_COD = new ReturnBao_Cao_COD();
            ReturnBao_Cao_COD = baocao.Bao_Cao_COD( common.DateToInt(StartDate), common.DateToInt(EndDate), Ma_kh, So_hieu, Trang_thai);
            //return View(returntrackingorder);
            return Json(ReturnBao_Cao_COD, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public List<Bao_Cao_COD> ReturnBao_Cao_CODExcel(string StartDate, string EndDate, string Ma_kh, string So_hieu, string Trang_thai)
        {

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.Ma_kh = Ma_kh;
            ViewBag.So_hieu = So_hieu;
            ViewBag.Trang_thai = Trang_thai;

            Bao_Cao_CODRepository baocao = new Bao_Cao_CODRepository();
            ReturnBao_Cao_COD ReturnBao_Cao_COD = new ReturnBao_Cao_COD();
            ReturnBao_Cao_COD = baocao.Bao_Cao_COD(common.DateToInt(StartDate), common.DateToInt(EndDate), Ma_kh, So_hieu, Trang_thai);
            return ReturnBao_Cao_COD.ListBao_Cao_COD;
        }

        public Stream CreateBao_Cao_CODExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnBao_Cao_CODExcel(ViewBag.StartDate, ViewBag.EndDate, ViewBag.Ma_kh, ViewBag.So_hieu, ViewBag.Trang_thai);
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
                BindingFormatForBao_Cao_CODExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        //Phần sửa excel
        private void BindingFormatForBao_Cao_CODExcel(ExcelWorksheet worksheet, List<Bao_Cao_COD> listItems)
        {
            var list = ReturnBao_Cao_CODExcel(ViewBag.StartDate, ViewBag.EndDate, ViewBag.Ma_kh, ViewBag.So_hieu, ViewBag.Trang_thai);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header Cảnh báo trượt chuyến bưu gửi hỏa tốc
            worksheet.Cells[1, 1].Value = " BÁO CÁO TRẠNG THÁI CHI HỘ COD";
            worksheet.Cells["A1:K1"].Merge = true;

            worksheet.Cells[2, 11].Value = "MÃ BÁO CÁO:KD/TTCH_COD";
            worksheet.Cells["K2:K2"].Merge = true;

            worksheet.Cells[2, 5].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["E2:G2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Thời gian chấp nhận";
            worksheet.Cells[4, 3].Value = "Thời gian trả tiền";
            worksheet.Cells[4, 4].Value = "Mã khách hàng";
            worksheet.Cells[4, 5].Value = "Mã khách hàng tổng";
            worksheet.Cells[4, 6].Value = "Tên khách hàng";
            worksheet.Cells[4, 7].Value = "Chi nhánh";
            worksheet.Cells[4, 8].Value = "Số hiệu khách hàng";
            worksheet.Cells[4, 9].Value = "Mã vận đơn";
            worksheet.Cells[4, 10].Value = "Số tiền COD";
            worksheet.Cells[4, 11].Value = "Trạng thái";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult CreateBao_Cao_CODExcelFile(string StartDate, string EndDate, string Ma_kh, string So_hieu, string Trang_thai)
        {

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.Ma_kh = Ma_kh;
            ViewBag.So_hieu = So_hieu;
            ViewBag.Trang_thai = Trang_thai;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateBao_Cao_CODExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo .xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Export_Bao_Cao_COD(string StartDate, string EndDate, string Ma_kh, string So_hieu, string Trang_thai)
        {

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.Ma_kh = Ma_kh;
            ViewBag.So_hieu = So_hieu;
            ViewBag.Trang_thai = Trang_thai;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateBao_Cao_CODExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo trạng thái chi hộ COD.xlsx");
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