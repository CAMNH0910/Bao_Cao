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
                workSheet.Cells[3, 1].LoadFromCollection(list);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<THHubDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[2, 1].Value = "";
            worksheet.Cells[1, 2].Value = "HUB 1";
            worksheet.Cells[2, 2].Value = "";
            worksheet.Cells[1, 3].Value = "Tỉnh phát";
            worksheet.Cells[2, 3].Value = "";
            worksheet.Cells[1, 4].Value = "Tổng số";
            worksheet.Cells[2, 4].Value = "";


            worksheet.Cells[1, 5].Value = "Đạt chỉ tiêu";
            worksheet.Cells["E1:F1"].Merge = true;
            worksheet.Cells[2, 5].Value = "Số lượng";
            worksheet.Cells[2, 6].Value = "Tỷ lệ";

            worksheet.Cells[1, 7].Value = "Chậm chỉ tiêu";
            worksheet.Cells["G1:H1"].Merge = true;
            worksheet.Cells[2, 7].Value = "Số lượng";
            worksheet.Cells[2, 8].Value = "Tỷ lệ";

            worksheet.Cells[1, 9].Value = "Tổng số (0-16)";
            worksheet.Cells[2, 9].Value = "";


            worksheet.Cells[1, 10].Value = "Đạt chỉ tiêu (0-16)";
            worksheet.Cells["J1:K1"].Merge = true;
            worksheet.Cells[2, 10].Value = "Sản lượng";
            worksheet.Cells[2, 11].Value = "Tỷ lệ";

            worksheet.Cells[1, 12].Value = "Chậm chỉ tiêu (0-16)";
            worksheet.Cells["L1:M1"].Merge = true;
            worksheet.Cells[2, 12].Value = "Sản lượng";
            worksheet.Cells[2, 13].Value = "Tỷ lệ";

            worksheet.Cells[1, 14].Value = "Tổng số (16-24)";
            worksheet.Cells[2, 14].Value = "";


            worksheet.Cells[1, 15].Value = "Đạt chỉ tiêu (16-24)";
            worksheet.Cells["O1:P1"].Merge = true;
            worksheet.Cells[2, 15].Value = "Sản lượng";
            worksheet.Cells[2, 16].Value = "Tỷ lệ";

            worksheet.Cells[1, 17].Value = "Chậm chỉ tiêu (16-24)";
            worksheet.Cells["Q1:R1"].Merge = true;
            worksheet.Cells[2, 17].Value = "Sản lượng";
            worksheet.Cells[2, 18].Value = "Tỷ lệ";




            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:S1"])
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