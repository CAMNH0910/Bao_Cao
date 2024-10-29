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
    public class Tms_Kpi_HTController : Controller

    {
        Convertion common = new Convertion();
        public ActionResult Tms_Kpi_HT()
        {
            return View();

        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListTms_Kpi_HT(int endpostcode, int? hanh_trinh,int isdelete)
        {
            ViewBag.endpostcode = endpostcode;
            ViewBag.hanh_trinh = hanh_trinh;
            ViewBag.isdelete = isdelete;

            Tms_Kpi_HTRepository KPI_Thu_GomRepository = new Tms_Kpi_HTRepository();
            ReturnTms_Kpi_HT returnquality = new ReturnTms_Kpi_HT();
            returnquality = KPI_Thu_GomRepository.Tms_Kpi_HT_DETAIL(endpostcode, hanh_trinh, isdelete);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult UpdateTms_Kpi_HT(int HUB, int TINH_PHAT, int HUYEN_PHAT, string TEN_HUYEN, int ID_HANH_TRINH, string TEN_HANH_TRINH, string THOI_GIAN_DI, int Trang_thai)
        {
             
            Tms_Kpi_HTRepository Tms_Kpi_HTRepository = new Tms_Kpi_HTRepository();
            return Json(Tms_Kpi_HTRepository.Update_Tms_Kpi_HT(HUB, TINH_PHAT, HUYEN_PHAT, TEN_HUYEN, ID_HANH_TRINH, TEN_HANH_TRINH, THOI_GIAN_DI, Trang_thai), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertTms_Kpi_HT  (int HUB, int TINH_PHAT, int HUYEN_PHAT, string TEN_HUYEN, int ID_HANH_TRINH, string TEN_HANH_TRINH, string THOI_GIAN_DI, int Trang_thai)
        {

            Tms_Kpi_HTRepository Tms_Kpi_HTRepository = new Tms_Kpi_HTRepository();
            return Json(Tms_Kpi_HTRepository.Insert_Tms_Kpi_HT(HUB, TINH_PHAT, HUYEN_PHAT, TEN_HUYEN, ID_HANH_TRINH, TEN_HANH_TRINH, THOI_GIAN_DI, Trang_thai), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public List<Tms_Kpi_HT> ReturnListExcel(int endpostcode, int? hanh_trinh, int isdelete)
        {

            ViewBag.endpostcode = endpostcode;
            ViewBag.hanh_trinh = hanh_trinh;
            ViewBag.isdelete = isdelete;

            Tms_Kpi_HTRepository KPIKTRepository = new Tms_Kpi_HTRepository();
            ReturnTms_Kpi_HT returnKPIKT = new ReturnTms_Kpi_HT();
            returnKPIKT = KPIKTRepository.Tms_Kpi_HT_DETAIL(endpostcode, hanh_trinh, isdelete);
            return returnKPIKT.List_Tms_Kpi_HT;
        }
        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.endpostcode, ViewBag.hanh_trinh, ViewBag.isdelete);
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
                workSheet.Cells[4, 1].LoadFromCollection(list, false, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<Tms_Kpi_HT> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "LƯỢC ĐỒ KHAI THÁC";
            worksheet.Cells["A1:I1"].Merge = true;

            worksheet.Cells[2, 9].Value = "MÃ BÁO CÁO:KT/LDKTHT";
            worksheet.Cells["I2:I2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "HUB";
            worksheet.Cells[4, 3].Value = "Tỉnh phát";
            worksheet.Cells[4, 4].Value = "Huyện phát";
            worksheet.Cells[4, 5].Value = "Tên huyện";
            worksheet.Cells[4, 6].Value = "ID hành trình";
            worksheet.Cells[4, 7].Value = "Tên hành trình";
            worksheet.Cells[4, 8].Value = "Thời gian xuất phát";
            worksheet.Cells[4, 9].Value = "Trạng thái";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:I4"])
            using (var ranges = worksheet.Cells["A1:I1"])

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

                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(int endpostcode, int? hanh_trinh, int isdelete)
        {

            ViewBag.endpostcode = endpostcode;
            ViewBag.hanh_trinh = hanh_trinh;
            ViewBag.isdelete = isdelete;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Lược Đồ khai thác bưu cục " + endpostcode + ".xlsx");
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