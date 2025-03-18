using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;
using OfficeOpenXml.Table;
using System.Drawing;

namespace T41.Areas.Admin.Controllers
{
    public class KPI_Detail_PTCController : Controller
    {
        Convertion common = new Convertion();

        public ActionResult KPI_Detail_PTC()
        {
            return View();
        }
        public ActionResult KPI_Detail_PTC_BT()
        {
            return View();
        }
        public ActionResult KPI_Detail_PTC_CT()
        {
            return View();
        }
        
        #region Tổng hợp bưu cục
        [HttpPost]
        public JsonResult GET_List_KPI_Detail_PTC(string startdate, string enddate, int zone, int endpostcode)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;

            KPI_Detail_PTCRepository kPI_CustomerRepository = new KPI_Detail_PTCRepository();
            ReturnKPI_Detail_PTC returnquality = new ReturnKPI_Detail_PTC();
            returnquality = kPI_CustomerRepository.KPI_Detail_PTC(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public List<KPI_Detail_PTC> ReturnListExcel(string startdate, string enddate, int zone, int endpostcode)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            KPI_Detail_PTCRepository kPI_CustomerRepository = new KPI_Detail_PTCRepository();
            ReturnKPI_Detail_PTC returnquality = new ReturnKPI_Detail_PTC();
            returnquality = kPI_CustomerRepository.KPI_Detail_PTC(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode);
            return returnquality.ListKPI_Detail_PTC;
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
                workSheet.Cells[6, 1].LoadFromCollection(list, false, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<KPI_Detail_PTC> listItems)
        {
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ SẢN LƯỢNG ĐẾN CỦA BƯU CỤC PHÁT";
            worksheet.Cells["A1:O1"].Merge = true;
            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO:BT/TKSPL_N";
            worksheet.Cells["O2:O2"].Merge = true;
            worksheet.Cells[2, 7].Value = "Từ ngày:" + " " + ViewBag.startdate + "-" + "Đến ngày" + " " + ViewBag.endDate;
            worksheet.Cells["G2:I2"].Merge = true;
            // Header row 4
            worksheet.Cells[4, 1].Value = "TT";
            worksheet.Cells["A4:A5"].Merge = true;
            worksheet.Cells[4, 2].Value = "Đơn Vị";
            worksheet.Cells["B4:B5"].Merge = true;
            worksheet.Cells[4, 3].Value = "Mã Bưu Cục";
            worksheet.Cells["C4:C5"].Merge = true;
            worksheet.Cells[4, 4].Value = "Tên Bưu Cục";
            worksheet.Cells["D4:D5"].Merge = true;
            worksheet.Cells[4, 5].Value = "SL bưu gửi đến phát";
            worksheet.Cells["E4:E5"].Merge = true;
            worksheet.Cells[4, 6].Value = "SL Phát thành công";
            worksheet.Cells["F4:F5"].Merge = true;
            worksheet.Cells[4, 7].Value = "TL Phát thành công";
            worksheet.Cells["G4:G5"].Merge = true;
            worksheet.Cells[4, 8].Value = "SL Phát thành công lần đầu";
            worksheet.Cells["H4:H5"].Merge = true;
            worksheet.Cells[4, 9].Value = "TL Phát lần đầu";
            worksheet.Cells["I4:I5"].Merge = true;
            // Merge PTC trong ngày
            worksheet.Cells[4, 10].Value = "PTC trong ngày";
            worksheet.Cells["J4:M4"].Merge = true;
            worksheet.Cells[5, 10].Value = "SL XNĐ từ 00:00 - 16:00 PTC trong ngày";
            worksheet.Cells[5, 11].Value = "SL XNĐ từ 16:01 - 23:59 PTC <= 12:00 ngày hôm sau";
            worksheet.Cells[5, 12].Value = "Tổng phát thành công trong ngày";
            worksheet.Cells[5, 13].Value = "TL PTC trong ngày (%)";

            worksheet.Cells[4, 14].Value = "SL phát không đúng quy định";
            worksheet.Cells["N4:N5"].Merge = true;
            worksheet.Cells[4, 15].Value = "SL chưa có Thông tin phát";
            worksheet.Cells["O4:O5"].Merge = true;
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:O5"])
            using (var ranges = worksheet.Cells["A1:O1"])
            using (var Ngay = worksheet.Cells["G2:I2"])
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đến BCP.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion
        #region Tổng hợp bưu tá
        [HttpPost]
        public JsonResult GET_List_KPI_Detail_PTC_BT(string startdate, string enddate, int zone, int endpostcode,int routecode,int postman)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;

            KPI_Detail_PTCRepository kPI_CustomerRepository = new KPI_Detail_PTCRepository();
            ReturnKPI_Detail_PTC returnquality = new ReturnKPI_Detail_PTC();
            returnquality = kPI_CustomerRepository.KPI_Detail_PTC_BT(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, routecode, postman);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

        #region Chi tiết bưu cục
        [HttpPost]
        public JsonResult KPI_Detail_PTC_CT(string startdate, string enddate, int zone, int endpostcode, int types)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.types = types;

            KPI_Detail_PTCRepository kPI_CustomerRepository = new KPI_Detail_PTCRepository();
            ReturnKPI_Detail_PTC returnquality = new ReturnKPI_Detail_PTC();
            returnquality = kPI_CustomerRepository.KPI_Detail_PTC_CT(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, types);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion
    }
}