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
using OfficeOpenXml.Table;
using System.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;


namespace T41.Areas.Admin.Controllers
{
    public class List_Item_BCCPController : Controller
    {
        Convertion common = new Convertion();

        public ActionResult List_Item_BCCP()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DetailList_Item_BCCP(string startdate, int endpostcode, string search)
        {
            ViewBag.startdate = startdate;
            ViewBag.endpostcode = endpostcode;
            ViewBag.search = search;

            var kPI_CustomerRepository = new List_Item_BCCPRepository();
            var returnquality = new ReturnList_Item_BCCP();

            try
            {
                // Kiểm tra giá trị của endpostcode để gọi phương thức phù hợp
                if (endpostcode == 100916 || endpostcode == 101000)
                {
                    returnquality = kPI_CustomerRepository.List_Item_BCCP_HN(common.DateToInt(startdate), endpostcode, search);
                }
                else if (endpostcode == 700916 || endpostcode == 701000)
                {
                    returnquality = kPI_CustomerRepository.List_Item_BCCP_HCM(common.DateToInt(startdate), endpostcode, search);
                }
                else
                {
                    returnquality.Code = "01";
                    returnquality.Message = "endpostcode không hợp lệ.";
                }
            }
            catch (Exception ex)
            {
                returnquality.Code = "99";
                returnquality.Message = $"Lỗi: {ex.Message}";
            }

            // Trả về JSON kết quả
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public List<List_Item_BCCP> ReturnListExcel(string startdate, int endpostcode, string search)
        {

            ViewBag.startdate = startdate;
            ViewBag.endpostcode = endpostcode;
            ViewBag.search = search;
            var kPI_CustomerRepository = new List_Item_BCCPRepository();
            var returnquality = new ReturnList_Item_BCCP();
            try
            {
                // Kiểm tra giá trị của endpostcode để gọi phương thức phù hợp
                if (endpostcode == 100916 || endpostcode == 101000)
                {
                    returnquality = kPI_CustomerRepository.List_Item_BCCP_HN(common.DateToInt(startdate), endpostcode, search);
                }
                else if (endpostcode == 700916 || endpostcode == 701000)
                {
                    returnquality = kPI_CustomerRepository.List_Item_BCCP_HCM(common.DateToInt(startdate), endpostcode, search);
                }
                else
                {
                    returnquality.Code = "01";
                    returnquality.Message = "endpostcode không hợp lệ.";
                }
            }
            catch (Exception ex)
            {
                returnquality.Code = "99";
                returnquality.Message = $"Lỗi: {ex.Message}";
            }
            return returnquality.ListList_Item_BCCP;
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.endpostcode, ViewBag.search);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<List_Item_BCCP> listItems)
        {
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.endpostcode, ViewBag.search);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO HOẠT ĐỘNG CÁC CHUYẾN THU";
            worksheet.Cells["A1:I1"].Merge = true;

            worksheet.Cells[2, 8].Value = "MÃ BÁO CÁO:BT/HDCT";
            worksheet.Cells["I2:I2"].Merge = true;

            worksheet.Cells[2, 4].Value = "Ngày:" + " " + ViewBag.startdate;
            worksheet.Cells["D2:F2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã bưu cục";
            worksheet.Cells[4, 3].Value = "Id đường thư";
            worksheet.Cells[4, 4].Value = "Tên chuyến thư";
            worksheet.Cells[4, 5].Value = "Số hiệu BĐ 10 đi";
            worksheet.Cells[4, 6].Value = "Số túi";
            worksheet.Cells[4, 7].Value = "Khối lượng";
            worksheet.Cells[4, 8].Value = "Thời gian BĐ 10 đóng đi TTKT Hub Vùng";
            worksheet.Cells[4, 9].Value = "Thời gian nhận BĐ 10 tại khai thác đến";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:I5"])
            using (var ranges = worksheet.Cells["A1:I1"])
            using (var Ngay = worksheet.Cells["D2:F2"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 11));

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
        public ActionResult Export(string startdate, int endpostcode, string search)
        {
            ViewBag.startdate = startdate;
            ViewBag.endpostcode = endpostcode;
            ViewBag.search = search;
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo hoạt động các chuyến thư.xlsx");
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