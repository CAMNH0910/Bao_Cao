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
    public class Kien_Di_Ngoai_TTKTVController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Kien_Di_Ngoai_TTKTVReport()
        {
            return View();
        }
        public ActionResult ListKien_Di_Ngoai_TTKTV( string startdate, string enddate, int zone)
        {
            
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            Kien_Di_Ngoai_TTKTVRepository qualitydeliveryRepository = new Kien_Di_Ngoai_TTKTVRepository();
            ReturnKien_Di_Ngoai_TTKTV returnquality = new ReturnKien_Di_Ngoai_TTKTV();
            returnquality = qualitydeliveryRepository.Kien_Di_Ngoai_TTKTV(common.DateToInt(startdate), common.DateToInt(enddate), zone);
            return Json(returnquality, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public List<Kien_Di_Ngoai_TTKTV> EXKien_Di_Ngoai_TTKTV(string startdate, string enddate,  int zone)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.Zone = zone;
            Kien_Di_Ngoai_TTKTVRepository qualitydeliveryRepository = new Kien_Di_Ngoai_TTKTVRepository();
            ReturnKien_Di_Ngoai_TTKTV returnquality = new ReturnKien_Di_Ngoai_TTKTV();
            returnquality = qualitydeliveryRepository.Kien_Di_Ngoai_TTKTV(common.DateToInt(startdate), common.DateToInt(enddate), zone);
            return returnquality.ListKien_Di_Ngoai_TTKTV;
        }
        [HttpGet]

        public ActionResult ExportKien_Di_Ngoai_TTKTV(string startdate, string enddate, int zone)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.Zone = zone;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelKien_Di_Ngoai_TTKTV();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo kiểm soát bưu gửi Cod.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        [HttpGet]


        public Stream CreateExcelKien_Di_Ngoai_TTKTV(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = EXKien_Di_Ngoai_TTKTV(ViewBag.startdate,ViewBag.enddate,ViewBag.Zone);
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
                BindingFormatForExcelKien_Di_Ngoai_TTKTV(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }

        }
        private void BindingFormatForExcelKien_Di_Ngoai_TTKTV(ExcelWorksheet worksheet, List<Kien_Di_Ngoai_TTKTV> listItems)
        {
            var list = EXKien_Di_Ngoai_TTKTV(ViewBag.startdate, ViewBag.enddate, ViewBag.Zone);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "Báo cáo thống kê kiện đi ngoài tại các TTKTV";
            worksheet.Cells["A1:H1"].Merge = true;

            worksheet.Cells[2, 8].Value = "MÃ BÁO CÁO:NVCL/TK_KDN";
            worksheet.Cells["H2:H2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["D2:E2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Đơn vị";
            worksheet.Cells[4, 3].Value = "Tổng SL";
            worksheet.Cells[4, 4].Value = "SL KT EMS";       
            worksheet.Cells[4, 5].Value = "Tỷ lệ SL ";        
            worksheet.Cells[4, 6].Value = "Tổng KL";       
            worksheet.Cells[4, 7].Value = "KL KT EMS";
            worksheet.Cells[4, 8].Value = "Tỷ lệ KL";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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

    }
}