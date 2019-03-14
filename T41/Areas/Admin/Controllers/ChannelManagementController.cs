
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;

using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;

namespace T41.Areas.Admin.Controllers
{
    public class ChannelManagementController : Controller
    {
        // GET: Admin/ChannelManagement
        Convertion common = new Convertion();
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        
        public ActionResult Index()
        {
            var userid = Convert.ToInt32(Session["userid"]);
            var role = Convert.ToInt32(Session["Role"]);
            //Phân quyền đăng nhập
            if (userid == 1 || role == 3)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult ListChannelDetail(int? page, string from_date, string to_date)
        {
            var userid = Convert.ToInt32(Session["userid"]);
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            ChannelManagementRepository usermanagementRepository = new ChannelManagementRepository();
            ReturnChannelManagement return_user = new ReturnChannelManagement();
            return_user = usermanagementRepository.CHANNEL_MANAGEMENT_DETAIL( page_size, currentPageIndex, common.DateToInt(from_date), common.DateToInt(to_date), userid);
            ViewBag.total = return_user.Total;
            ViewBag.total_page = (return_user.Total + page_size - 1) / page_size;
            return View(return_user);
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<ChannelManagement_BP_Detail> ReturnListExcel(int? page, string from_date, string to_date)
        {
            var userid = Convert.ToInt32(Session["userid"]);
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ChannelManagementRepository usermanagementRepository = new ChannelManagementRepository();
            ReturnChannelManagement return_user = new ReturnChannelManagement();
            //Giá trị page_size để export excel
            int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE_EXPORT_EXCEL"]);
            return_user = usermanagementRepository.CHANNEL_MANAGEMENT_DETAIL(page_size, currentPageIndex, common.DateToInt(from_date), common.DateToInt(to_date), userid);
            ViewBag.total = return_user.Total;
            ViewBag.total_page = (return_user.Total + page_size - 1) / page_size;
            return return_user.ListChannelManagement_BP_Report;


        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.page, ViewBag.from_date,  ViewBag.to_date);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<ChannelManagement_BP_Detail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Thời Gian Kết Nối";
            worksheet.Cells[1, 3].Value = "Tên khách hàng";
            worksheet.Cells[1, 4].Value = "Địa Chỉ";
            worksheet.Cells[1, 5].Value = "Mã Quận";
            worksheet.Cells[1, 6].Value = "Mã Tỉnh";
            worksheet.Cells[1, 7].Value = "Số Điện thoại";
            worksheet.Cells[1, 8].Value = "Mã khách hàng";
            worksheet.Cells[1, 9].Value = "Email";
            

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới I1
            using (var range = worksheet.Cells["A1:I1"])
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
        public ActionResult Export(int? page, string from_date, string to_date)
        {
            ViewBag.from_date = from_date;
            ViewBag.to_date = to_date;
            ViewBag.page = page;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Quản lý kênh kết nối_" + from_date + "_đến_" + to_date + ".xlsx");
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