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
    public class KPI_Thu_GomController :  Controller
    {
        Convertion common = new Convertion();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KPI_Thu_Gom()
        {

            return View();

        }
        public JsonResult PosCode(int zone)
        {
            KPI_Thu_GomRepository PostManDeliveryRepository = new KPI_Thu_GomRepository();
            return Json(PostManDeliveryRepository.GetAllPOSCODE(zone), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        //Controller lấy dữ liệu tuyến phát
        public JsonResult RouteCode(int endpostcode)
        {
            KPI_Thu_GomRepository PostmanDeliveryRepository = new KPI_Thu_GomRepository();
            return Json(PostmanDeliveryRepository.GetAllROUTECODE(endpostcode), JsonRequestBehavior.AllowGet);
            //return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Postman(int routecode)
        {
            KPI_Thu_GomRepository PostmanDeliveryRepository = new KPI_Thu_GomRepository();
            return Json(PostmanDeliveryRepository.GetAllPOSTMAN(routecode), JsonRequestBehavior.AllowGet);
            //return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        #region Tổng hợp
        [HttpPost]
        public JsonResult ListKPI_Thu_Gom(string startdate, string enddate, int zone, int endpostcode)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            KPI_Thu_GomRepository KPI_Thu_GomRepository = new KPI_Thu_GomRepository();
            ReturnKPI_Thu_Gom returnquality = new ReturnKPI_Thu_Gom();
            returnquality = KPI_Thu_GomRepository.KPI_Thu_Gom(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public List<KPI_Thu_Gom> ReturnListExcel(string startdate, string enddate, int zone, int endpostcode)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            KPI_Thu_GomRepository KPI_Thu_GomRepository = new KPI_Thu_GomRepository();
            ReturnKPI_Thu_Gom returnquality = new ReturnKPI_Thu_Gom();
            returnquality = KPI_Thu_GomRepository.KPI_Thu_Gom(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode);
            return returnquality.ListKPI_Thu_Gom;
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode);
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
                workSheet.Cells[5, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<KPI_Thu_Gom> listItems)
        {
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ SẢN LƯỢNG THU GOM";
            worksheet.Cells["A1:Q1"].Merge = true;

            worksheet.Cells[2, 17].Value = "MÃ BÁO CÁO:TG/TKSLTG";
            worksheet.Cells["Q2:Q2"].Merge = true;

            worksheet.Cells[2, 8].Value = "Từ ngày:" + " " + ViewBag.startdate + "-" + "Đến ngày" + " " + ViewBag.endDate;
            worksheet.Cells["H2:J2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells["A4:A5"].Merge = true;
            worksheet.Cells[4, 2].Value = "Mã BCGG";
            worksheet.Cells["B4:B5"].Merge = true;
            worksheet.Cells[4, 3].Value = "Tên BCGD";
            worksheet.Cells["C4:C5"].Merge = true;
            worksheet.Cells[4, 4].Value = "Mã tuyến thu gom";
            worksheet.Cells["D4:D5"].Merge = true;
            worksheet.Cells[4, 5].Value = "Tên tuyến thu gom";
            worksheet.Cells["E4:E5"].Merge = true;
            worksheet.Cells[4, 6].Value = "Mã nhân viên";
            worksheet.Cells["F4:F5"].Merge = true;
            worksheet.Cells[4, 7].Value = "Tên nhân viên";
            worksheet.Cells["G4:G5"].Merge = true;
            worksheet.Cells[4, 8].Value = "Dịch vụ";
            worksheet.Cells["H4:H5"].Merge = true;

            worksheet.Cells[4, 9].Value = "Sản lượng";
            worksheet.Cells["I4:K4"].Merge = true;
            worksheet.Cells[5, 9].Value = "Thu gom thành công";
            worksheet.Cells[5, 10].Value = "Phát hành";
            worksheet.Cells[5, 11].Value = "Trả lại/ không phát hành";

            worksheet.Cells[4, 12].Value = "Sản lượng thu gom thành công đã phát hành theo khối lượng";
            worksheet.Cells["L4:N4"].Merge = true;
            worksheet.Cells[5, 12].Value = "SL đến 2kg";
            worksheet.Cells[5, 13].Value = "KL đến 2kg";
            worksheet.Cells[5, 14].Value = "SL trên 2kg";
            worksheet.Cells[5, 15].Value = "KL trên 2kg";
            worksheet.Cells[5, 16].Value = "Tổng cộng SL";
            worksheet.Cells[5, 17].Value = "Tổng cộng KL";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:Q5"])
            using (var ranges = worksheet.Cells["A1:Q1"])
            using (var Ngay = worksheet.Cells["H2:J2"])
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
        public ActionResult Export(string startdate, string enddate, int zone, int endpostcode)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng thu gom.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
    }
    #endregion
}