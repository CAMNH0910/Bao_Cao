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
    public class ItemSendController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ItemSendReport()
        {
            return View();
        }

        //Phần show ra dữ liệu của bảng người dùng
        public ActionResult ListItemSendReport(int? page, int Type, string Startdate, string Enddate, string CustomerCode,string ItemCode)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            ItemSendRepository itemSendRepository = new ItemSendRepository();
            ResponseItemSend returnItemSend = new ResponseItemSend();
            returnItemSend = itemSendRepository.ITEM_SEND_DETAIL(currentPageIndex, page_size, Type, common.DateToInt(Startdate), common.DateToInt(Enddate), CustomerCode,ItemCode);
            ViewBag.total = returnItemSend.Total;
            ViewBag.total_page = (returnItemSend.Total + page_size - 1) / page_size;
            return View(returnItemSend);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<ItemSend> ReturnListExcel(int Type, string Startdate, string Enddate, string CustomerCode, string ItemCode)
        {

            ItemSendRepository itemSendRepository = new ItemSendRepository();
            ResponseItemSend returnItemSend = new ResponseItemSend();
            returnItemSend = itemSendRepository.ITEM_SEND_DETAIL(1, 100000, Type, common.DateToInt(Startdate), common.DateToInt(Enddate), CustomerCode, ItemCode);
            return returnItemSend.listItemSendReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.Type, ViewBag.Startdate, ViewBag.Enddate, ViewBag.CustomerCode, ViewBag.ItemCode);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<ItemSend> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "Mã Khách Hàng";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "Mã bưu cục";
            worksheet.Cells[1, 4].Value = "Kênh GTGT";
            worksheet.Cells[1, 5].Value = "Điện thoại gửi";
            worksheet.Cells[1, 6].Value = "Điện thoại nhận";
            worksheet.Cells[1, 7].Value = "Email nhận";
            worksheet.Cells[1, 8].Value = "Trạng thái";
            worksheet.Cells[1, 9].Value = "Gửi tin";
            worksheet.Cells[1, 10].Value = "Thời gian gửi";

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
        public ActionResult Export(int Type, string Startdate, string Enddate, string CustomerCode, string ItemCode)
        {
            
            ViewBag.Type = Type;
            ViewBag.Startdate = Startdate;
            ViewBag.Enddate = Enddate;
            ViewBag.CustomerCode = CustomerCode;
            ViewBag.ItemCode = ItemCode;
           
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo thông kê dịch vụ GTGT từ "+ Startdate + " đến" + Enddate + ".xlsx");
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