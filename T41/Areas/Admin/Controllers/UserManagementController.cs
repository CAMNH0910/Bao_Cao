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
    public class UserManagementController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
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
            else {
                return RedirectToAction("Index", "Home");
            }
            
        }

        //Controller lấy dữ liệu tỉnh đóng, tỉnh nhận
        public JsonResult PosCode()
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            return Json(usermanagementRepository.GETPOSCODE_GIAO_DICH(), JsonRequestBehavior.AllowGet);
        }

        //Phần controller xử lý để sửa dữ liệu bảng BUSINESS_PROFILE_OA dưới database
        [HttpGet]
        public ActionResult ListUserManagement_CRM_Report(int poscode, int unitcode)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.USER_MANAGEMENT_DETAIL(poscode, unitcode);
            return View(returnusermanagement);
        }


        //[HttpGet]
        ////Phần show ra dữ liệu của bảng BUSINESS_PROFILE_OA
        //public ActionResult ListDetailedUserManagementReport(int? page, int user_id, string user_customer_code, int user_contact_phone_work, string user_general_email)
        //{
        //    int currentPageIndex = page.HasValue ? page.Value : 1;
        //    ViewBag.currentPageIndex = currentPageIndex;
        //    ViewBag.PageSize = page_size;
        //    UserManagementRepository usermanagementRepository = new UserManagementRepository();
        //    ReturnUserManagement returnusermanagement = new ReturnUserManagement();
        //    returnusermanagement = usermanagementRepository.USER_MANAGEMENT_DETAIL(page_size, currentPageIndex,user_id , user_customer_code, user_contact_phone_work, user_general_email);
        //    ViewBag.total = returnusermanagement.Total;
        //    ViewBag.total_page = (returnusermanagement.Total + page_size - 1) / page_size;
        //    return View(returnusermanagement);

        //}


        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<UserManagement_CRM_Detail> ReturnListExcel(int poscode, int unitcode)
        {

            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.USER_MANAGEMENT_DETAIL(poscode, unitcode);
            return returnusermanagement.ListUserManagement_CRM_Report;
        }

       
        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.poscode,ViewBag.unitcode);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<UserManagement_CRM_Detail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã tài khoản";
            worksheet.Cells[1, 3].Value = "Mã hợp đồng";
            worksheet.Cells[1, 4].Value = "Mã liên hệ";
            worksheet.Cells[1, 5].Value = "Mã nhân viên tiếp thị";
            worksheet.Cells[1, 6].Value = "Bưu cục chấp nhận";
            worksheet.Cells[1, 7].Value = "Mã KH";
            worksheet.Cells[1, 8].Value = "Tên KH";
            
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:Z1"])
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
        public ActionResult Export(int poscode, int unitcode)
        {
            ViewBag.unitcode = unitcode;
            ViewBag.poscode = poscode;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Quản Lý Khách Hàng CRM.xlsx");
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