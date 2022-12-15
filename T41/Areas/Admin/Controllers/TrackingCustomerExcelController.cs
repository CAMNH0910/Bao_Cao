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
    public class TrackingCustomerExcelController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserManagementDetailReport()
        {
            var userid = Convert.ToInt32(Session["userid"]);
            //Phân quyền đăng nhập
            if (userid == 1 || userid == 4)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        //Controller lấy dữ liệu tỉnh đóng, tỉnh nhận
      
        //Phần controller xử lý để sửa dữ liệu bảng BUSINESS_PROFILE_OA dưới database
        [HttpGet]
        public ActionResult ListCustomer_Report_Detail(string customerCode, string StartDate, string EndDate)
        {
            ListDeliveryCustomerRepository CustomerRepository = new ListDeliveryCustomerRepository();
            ReturnListCustomer returnCustomer = new ReturnListCustomer();
            returnCustomer = CustomerRepository.List_Delivery(customerCode, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return View(returnCustomer);
        }


       

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<ListCustomer_Detail> ReturnListExcel(string customerCode, string StartDate, string EndDate)
        {
           
            ListDeliveryCustomerRepository CustomerRepository = new ListDeliveryCustomerRepository();
            ReturnListCustomer returnCustomer = new ReturnListCustomer();
            returnCustomer = CustomerRepository.List_Delivery(customerCode, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnCustomer.ListCustomer_Report_Detail;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.customerCode, ViewBag.startDate, ViewBag.EndDate);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<ListCustomer_Detail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã tham chiếu";
            worksheet.Cells[1, 3].Value = "Mã vận đơn";
            worksheet.Cells[1, 4].Value = "Mã BC phát";
            worksheet.Cells[1, 5].Value = "Người nhận";
            worksheet.Cells[1, 6].Value = "SĐT nhận";
            worksheet.Cells[1, 7].Value = "Địa chỉ nhận";
            worksheet.Cells[1, 8].Value = "Tỉnh nhận";
            worksheet.Cells[1, 9].Value = "Miêu tả trạng thái mới nhất";
            worksheet.Cells[1, 10].Value = "Tình trạng giao hàng lần 1";
            worksheet.Cells[1, 11].Value = "Ghi chú của bưu tá giao lần 1";
            worksheet.Cells[1, 12].Value = "Ngày giao hàng lần 1";
            worksheet.Cells[1, 13].Value = "Thời gian giao hàng lần 1";
            worksheet.Cells[1, 14].Value = "Tình trạng giao hàng lần 2";
            worksheet.Cells[1, 15].Value = "Ghi chú của bưu tá giao lần 2";
            worksheet.Cells[1, 16].Value = "Ngày giao hàng lần 2";
            worksheet.Cells[1, 17].Value = "Thời gian giao hàng lần 2";
            worksheet.Cells[1, 18].Value = "Tình trạng giao hàng lần 3";
            worksheet.Cells[1, 19].Value = "Ghi chú của bưu tá giao lần 3";
            worksheet.Cells[1, 20].Value = "Ngày giao hàng lần 3";
            worksheet.Cells[1, 21].Value = "Thời gian giao hàng lần 3";
            worksheet.Cells[1, 22].Value = "Tình trạng giao hàng lần 4";
            worksheet.Cells[1, 23].Value = "Ghi chú của bưu tá giao lần 4";
            worksheet.Cells[1, 24].Value = "Ngày giao hàng lần 4";
            worksheet.Cells[1, 25].Value = "Thời gian giao hàng lần 4";
            worksheet.Cells[1, 26].Value = "Tổng số lần giao hàng";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:Z1"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Orange);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 9));
               // range.Style.WrapText = true;

               
                // Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string customerCode,string startDate, string EndDate)
        {
            ViewBag.customerCode = customerCode;
            ViewBag.startDate = startDate;
            ViewBag.EndDate = EndDate;
           
            //ViewBag.todate = todate;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=TH_Theo_Lan_Phat.xlsx");
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