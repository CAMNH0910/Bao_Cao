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
    public class KPI_SL_Du_BaoController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult KPI_SL_Du_Bao_Report()
        {
            return View();

        }
        #region Tổng hợp chất lượng phát
        [HttpPost]
        public JsonResult ListKPI_SL_Du_Bao(string StartDate, int TT_KT)
        {

            ViewBag.StartDate = StartDate;

            ViewBag.TT_KT = TT_KT;

            KPI_SL_Du_BaoRepository KPI_SL_Du_BaoRepository = new KPI_SL_Du_BaoRepository();
            ReturnKPI_SL_Du_Bao returnquality = new ReturnKPI_SL_Du_Bao();
            returnquality = KPI_SL_Du_BaoRepository.KPI_SL_Du_Bao(common.DateToInt(StartDate), TT_KT);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
            public JsonResult ListKPI_SL_Du_BaoTime(string StartDate, int TT_KT)
            {

                ViewBag.StartDate = StartDate;

                ViewBag.TT_KT = TT_KT;

                KPI_SL_Du_BaoRepository KPI_SL_Du_BaoRepository = new KPI_SL_Du_BaoRepository();
                ReturnKPI_SL_Du_Bao returnquality = new ReturnKPI_SL_Du_Bao();
                returnquality = KPI_SL_Du_BaoRepository.KPI_SL_Du_BaoTime(common.DateToInt(StartDate), TT_KT);

                var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
        [HttpGet]
        public List<KPI_SL_Du_Bao> Excel_KPI_SL_Du_Bao(string StartDate, int TT_KT)
        {
            ViewBag.StartDate = StartDate;

            ViewBag.TT_KT = TT_KT;

            KPI_SL_Du_BaoRepository qualityDetailHubRepository = new KPI_SL_Du_BaoRepository();
            ReturnKPI_SL_Du_Bao returnquality = new ReturnKPI_SL_Du_Bao();
            returnquality = qualityDetailHubRepository.KPI_SL_Du_Bao(common.DateToInt(StartDate), TT_KT);
            return returnquality.ListKPI_SL_Du_Bao;
        }
        [HttpGet]

        public ActionResult ExportKPI_SL_Du_Bao(string StartDate, int TT_KT)
        {

            ViewBag.StartDate = StartDate;

            ViewBag.TT_KT = TT_KT;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelKPI_SL_Du_Bao();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo dự báo hàng đến ktv " + StartDate + " - " + ".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        [HttpGet]


        public Stream CreateExcelKPI_SL_Du_Bao(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = Excel_KPI_SL_Du_Bao(ViewBag.StartDate, ViewBag.TT_KT);
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
                workSheet.Cells[8, 1].LoadFromCollection(list, false, TableStyles.Dark9);
                BindingFormatForExcelKPI_SL_Du_Bao(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }

        }
        private void BindingFormatForExcelKPI_SL_Du_Bao(ExcelWorksheet worksheet, List<KPI_SL_Du_Bao> listItems)
        {
            var list = Excel_KPI_SL_Du_Bao(ViewBag.StartDate, ViewBag.TT_KT);


            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO DỰ BÁO SẢN LƯỢNG ĐẾN KHAI THÁC VÙNG";
            worksheet.Cells["A1:AB1"].Merge = true;

            worksheet.Cells[2, 28].Value = "MÃ BÁO CÁO:NVCL/DBD_KTV";
            worksheet.Cells["AA2:AA2"].Merge = true;

            worksheet.Cells[2, 14].Value = "Từ ngày:" + " " + ViewBag.StartDate;
            worksheet.Cells["N2:O2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells["A4:A7"].Merge = true;
            worksheet.Cells[4, 2].Value = "Mã tỉnh";
            worksheet.Cells["B4:B7"].Merge = true;
            worksheet.Cells[4, 3].Value = "Tên tỉnh";
            worksheet.Cells["C4:C7"].Merge = true;

            worksheet.Cells[4, 4].Value = "Tổng số túi";
            worksheet.Cells["D4:K4"].Merge = true;

            worksheet.Cells[5, 4].Value = "Tổng số túi giao từ BĐT";
            worksheet.Cells["D5:F6"].Merge = true;
            worksheet.Cells[7, 4].Value = "Tổng số Túi";
            worksheet.Cells[7, 5].Value = "Tổng số túi KT lại";
            worksheet.Cells[7, 6].Value = "Tổng số túi QG";

            worksheet.Cells[5, 7].Value = "Tổng số túi đến KT";
            worksheet.Cells["G5:J5"].Merge = true;

            worksheet.Cells[6, 7].Value = "Túi KT lại";
            worksheet.Cells["G6:H6"].Merge = true;
            worksheet.Cells[7, 7].Value = "Tổng số túi đã đến";
            worksheet.Cells[7, 8].Value = "Tổng số túi đã KT";

            worksheet.Cells[6, 9].Value = "Tổng số túi đến KT";
            worksheet.Cells["I6:J6"].Merge = true;
            worksheet.Cells[7, 9].Value = "Tổng số túi QG đã đến chưa giao";
            worksheet.Cells[7, 10].Value = "Tổng số túi QG đã giao";

            worksheet.Cells[5, 11].Value = "Tổng số túi đi đường";
            worksheet.Cells["K5:K6"].Merge = true;
            worksheet.Cells[7, 11].Value = "Tổng số túi đi đường";


            worksheet.Cells[4, 12].Value = "Tổng số bưu phẩm";
            worksheet.Cells["L4:S4"].Merge = true;

            worksheet.Cells[5, 12].Value = "Tổng số bưu phẩm giao từ BĐT";
            worksheet.Cells["L5:N6"].Merge = true;
            worksheet.Cells[7, 12].Value = "Tổng số bưu phẩm";
            worksheet.Cells[7, 13].Value = "Tổng số BP đã KT";
            worksheet.Cells[7, 14].Value = "Tổng số BP QG đã đến";


            worksheet.Cells[5, 15].Value = "Tổng số bưu phẩm đến KT";
            worksheet.Cells["O5:R5"].Merge = true;

            worksheet.Cells[6, 15].Value = "Bưu phẩm KT lại";
            worksheet.Cells["O6:P6"].Merge = true;
            worksheet.Cells[7, 15].Value = "Tổng số BP đã đến";
            worksheet.Cells[7, 16].Value = "Tổng số BP đã KT";

            worksheet.Cells[6, 17].Value = "Bưu phẩm Quá Giang";
            worksheet.Cells["Q6:R6"].Merge = true;
            worksheet.Cells[7, 17].Value = "Tổng số BP QG đã đến chưa giao";
            worksheet.Cells[7, 18].Value = "Tổng số BP QG đã giao";

            worksheet.Cells[5, 19].Value = "Bưu phẩm đi đường";
            worksheet.Cells["S5:S6"].Merge = true;
            worksheet.Cells[7, 19].Value = "Tổng số BP đi đường";




            worksheet.Cells[4, 20].Value = "Tổng số khối lượng";
            worksheet.Cells["T4:AA4"].Merge = true;

            worksheet.Cells[5, 20].Value = "Tổng số khối lượng giao từ BĐT";
            worksheet.Cells["T5:V6"].Merge = true;
            worksheet.Cells[7, 20].Value = "Tổng KL";
            worksheet.Cells[7, 21].Value = "Tổng KL KT lại";
            worksheet.Cells[7, 22].Value = "Tổng KL QG";

            worksheet.Cells[5, 23].Value = "Tổng số khối lượng đến KT";
            worksheet.Cells["W5:Z5"].Merge = true;

            worksheet.Cells[6, 23].Value = "Khối lượng KT lại";
            worksheet.Cells["W6:X6"].Merge = true;
            worksheet.Cells[7, 23].Value = "Tổng KL đã đến";
            worksheet.Cells[7, 24].Value = "Tổng KL đã KT";

            worksheet.Cells[6, 25].Value = "Khối lượng Quá Giang";
            worksheet.Cells["Y6:Z6"].Merge = true;
            worksheet.Cells[7, 25].Value = "Tổng KL QG đã đến chưa giao";
            worksheet.Cells[7, 26].Value = "Tổng KL QG đã giao";

            worksheet.Cells[5, 27].Value = "Tổng KL đi đường";
            worksheet.Cells["AA5:AA6"].Merge = true;
            worksheet.Cells[7, 27].Value = "Tổng KL đi đường";

            worksheet.Cells[4, 1].Value = "Thời gian cập nhật";

            worksheet.Cells["AB4:AB7"].Merge = true;
            //Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            //Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var ranges = worksheet.Cells["A1:AB1"])
            using (var Ngay = worksheet.Cells[" N2:O2"])
            {
                // Set PatternType
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

            using (var range = worksheet.Cells["A4:AB4"])
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
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
            using (var rangse = worksheet.Cells["D5:AB7"])
            {
                // Set PatternType
                rangse.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                rangse.Style.Fill.BackgroundColor.SetColor(Color.Orange);
                // Canh giữa cho các text
                rangse.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                rangse.Style.Font.SetFromFont(new Font("Arial", 11));
                // Set Border
                rangse.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rangse.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rangse.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rangse.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }
        //Hàm Export excel  , truyền parameter vào để export
        #endregion
    }
}