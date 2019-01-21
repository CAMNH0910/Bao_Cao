using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;

using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;

namespace T41.Areas.Admin.Controllers
{
    public class DetailLadingToPostManController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LadingToPostManDetailReport()
        {
            //var userid = Convert.ToInt32(Session["userid"]);
            ////Phân quyền đăng nhập
            //if (userid == 1)
            //{
            //    return View();

            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }

        //Phần show ra dữ liệu của Bưu Cục Phát
        public JsonResult ListPostCode(string unit_code)
        {
            DetailLadingToPostmanRepository bd13Repository = new DetailLadingToPostmanRepository();
            return Json(bd13Repository.GetAllDeliveryPostCode(unit_code), JsonRequestBehavior.AllowGet);
            
        }

        //Phần show ra dữ liệu của Tuyến Phát theo bưu cục phát
        public JsonResult ListDeliveryRouteByPostCode(int delivery_post_code)
        {
            DetailLadingToPostmanRepository bd13Repository = new DetailLadingToPostmanRepository();
            return Json(bd13Repository.GetDeliveryRouteCodeByDeliveryCode(delivery_post_code), JsonRequestBehavior.AllowGet);
            
        }

        //Phần show ra dữ liệu của bảng người dùng
        public ActionResult ListDetailedLADINGTOPOSTMANReport(int? page, int mabc_kt, int mabc, string ngay, int cakt, int chthu, int tuiso)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            DetailLadingToPostmanRepository bd13Repository = new DetailLadingToPostmanRepository();
            ReturnLADING ReturnLADING = new ReturnLADING();
            ReturnLADING = bd13Repository.LADING_TO_POSTMAN_DETAIL(currentPageIndex, page_size,  mabc_kt, mabc, common.DateToInt(ngay), cakt, chthu, tuiso);
            ViewBag.total = ReturnLADING.Total;
            ViewBag.total_page = (ReturnLADING.Total + page_size - 1) / page_size;
            return View(ReturnLADING);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<LADING_TO_POSTMAN_DETAIL> ReturnListExcel(int mabc_kt, int mabc, string ngay, int cakt, int chthu, int tuiso)
        {

            DetailLadingToPostmanRepository bd13Repository = new DetailLadingToPostmanRepository();
            ReturnLADING ReturnLADING = new ReturnLADING();
            ReturnLADING = bd13Repository.LADING_TO_POSTMAN_DETAIL(1, 5000, mabc_kt, mabc, common.DateToInt(ngay), cakt, chthu, tuiso);
            return ReturnLADING.List_LADING_TO_POSTMAN_Report;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.mabc_kt, ViewBag.mabc, ViewBag.ngay, ViewBag.cakt, ViewBag.chthu, ViewBag.tuiso);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<LADING_TO_POSTMAN_DETAIL> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "Mã E1";
            worksheet.Cells[1, 2].Value = "Mã bưu cục trả";
            worksheet.Cells[1, 3].Value = "Mã bưu cục nhận";
            worksheet.Cells[1, 4].Value = "Khối lượng";
            worksheet.Cells[1, 5].Value = "Cước CS";
            worksheet.Cells[1, 6].Value = "Cước DV";
            worksheet.Cells[1, 7].Value = "Trạng thái";
            worksheet.Cells[1, 8].Value = "Địa chỉ";

            worksheet.Cells[1, 9].Value = "Mã bưu cục ";
            worksheet.Cells[1, 10].Value = "Chuyến thư";
            worksheet.Cells[1, 11].Value = "Túi số";
            worksheet.Cells[1, 12].Value = "Ngày";
            worksheet.Cells[1, 13].Value = "Mã bưu cục khai thác";
            worksheet.Cells[1, 14].Value = "Ngày hệ thống";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:N1"])
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
        public ActionResult Export(int mabc_kt, int mabc, string ngay, int cakt, int chthu, int tuiso)
        {

            ViewBag.mabc_kt = mabc_kt;
            ViewBag.mabc = mabc;
            ViewBag.ngay = ngay;
            ViewBag.cakt = cakt;
            ViewBag.chthu = chthu;
            ViewBag.tuiso = tuiso;

            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Chi tiết e1_bd13_di_PNS_" + mabc_kt + "_ngày_" + ngay + ".xlsx");
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