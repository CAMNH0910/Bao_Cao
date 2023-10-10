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

    public class THHubController : Controller
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
        public ActionResult THHub()
        {
            return View();

        }





        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        [HttpPost]
        public JsonResult ListTHHubReport(string startdate, string enddate)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPITransportRepository KPITransportRepository = new KPITransportRepository();
            ReturnKPITransportTotal returnKPITransportTotal = new ReturnKPITransportTotal();
            returnKPITransportTotal = KPITransportRepository.THHub(common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnKPITransportTotal, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<THHubDetail> ReturnListExcel(string startdate, string enddate)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            KPITransportRepository KPITransportRepository = new KPITransportRepository();
            ReturnKPITransportTotal returnKPITransportTotal = new ReturnKPITransportTotal();
            returnKPITransportTotal = KPITransportRepository.THHub(common.DateToInt(startdate), common.DateToInt(enddate));
            return returnKPITransportTotal.ListTHHubDetailReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate);
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
                workSheet.Cells[6, 1].LoadFromCollection(list);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<THHubDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO TỔNG HỢP CÁC HUB";
            worksheet.Cells["A1:R1"].Merge = true;

            worksheet.Cells[2, 18].Value = "MÃ BÁO CÁO:KTVC/HUB";
            worksheet.Cells["R2:R2"].Merge = true;

            worksheet.Cells[2, 9].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.endDate;
            worksheet.Cells["I2:J2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[5, 1].Value = "";
            worksheet.Cells[4, 2].Value = "HUB 1";
            worksheet.Cells[5, 2].Value = "";
            worksheet.Cells[4, 3].Value = "Tỉnh phát";
            worksheet.Cells[5, 3].Value = "";
            worksheet.Cells[4, 4].Value = "Tổng số";
            worksheet.Cells[5, 4].Value = "";


            worksheet.Cells[4, 5].Value = "Đạt chỉ tiêu";
            worksheet.Cells["E4:F4"].Merge = true;
            worksheet.Cells[5, 5].Value = "Số lượng";
            worksheet.Cells[5, 6].Value = "Tỷ lệ";

            worksheet.Cells[4, 7].Value = "Chậm chỉ tiêu";
            worksheet.Cells["G4:H4"].Merge = true;
            worksheet.Cells[5, 7].Value = "Số lượng";
            worksheet.Cells[5, 8].Value = "Tỷ lệ";

            worksheet.Cells[4, 9].Value = "Tổng số (0-16)";
            worksheet.Cells[5, 9].Value = "";


            worksheet.Cells[4, 10].Value = "Đạt chỉ tiêu (0-16)";
            worksheet.Cells["J4:K4"].Merge = true;
            worksheet.Cells[5, 10].Value = "Sản lượng";
            worksheet.Cells[5, 11].Value = "Tỷ lệ";

            worksheet.Cells[4, 12].Value = "Chậm chỉ tiêu (0-16)";
            worksheet.Cells["L4:M4"].Merge = true;
            worksheet.Cells[5, 12].Value = "Sản lượng";
            worksheet.Cells[5, 13].Value = "Tỷ lệ";

            worksheet.Cells[4, 14].Value = "Tổng số (16-24)";
            worksheet.Cells[5, 14].Value = "";


            worksheet.Cells[4, 15].Value = "Đạt chỉ tiêu (16-24)";
            worksheet.Cells["O4:P4"].Merge = true;
            worksheet.Cells[5, 15].Value = "Sản lượng";
            worksheet.Cells[5, 16].Value = "Tỷ lệ";

            worksheet.Cells[4, 17].Value = "Chậm chỉ tiêu (16-24)";
            worksheet.Cells["Q4:R4"].Merge = true;
            worksheet.Cells[5, 17].Value = "Sản lượng";
            worksheet.Cells[5, 18].Value = "Tỷ lệ";




            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:R5"])
            using (var ranges = worksheet.Cells["A1:R1"])
            using (var Ngay = worksheet.Cells["I2:J2"])
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
                
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string startdate, string enddate)
        {

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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp các Hub.xlsx");
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