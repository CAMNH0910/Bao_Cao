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
    public class DealAndAccountFalseController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckDealAndAccountReport()
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


        #region DEAL
        //Type : JSON
        [HttpPost]
        public JsonResult CheckDeal_Report()
        {
            DealAndAccountFalseRepository dealandaccountfalseRepository = new DealAndAccountFalseRepository();
            ReturnDealAndAccountFalse returndealandaccountfalse = new ReturnDealAndAccountFalse();
            returndealandaccountfalse = dealandaccountfalseRepository.DEAL_FALSE_DETAIL();
            return Json(returndealandaccountfalse, JsonRequestBehavior.AllowGet);
        }

        //Phần trả về data theo list để xuất excel DEAL
        [HttpGet]
        public List<DealFalse_Detail> ReturnListExcelDeal()
        {

            DealAndAccountFalseRepository dealandaccountfalseRepository = new DealAndAccountFalseRepository();
            ReturnDealAndAccountFalse returndealandaccountfalse = new ReturnDealAndAccountFalse();
            returndealandaccountfalse = dealandaccountfalseRepository.DEAL_FALSE_DETAIL();
            return returndealandaccountfalse.ListDealFalse_Report;
        }



        public Stream CreateExcelFileDeal(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDeal();
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
                BindingFormatForExcelDeal(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcelDeal(ExcelWorksheet worksheet, List<DealFalse_Detail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "CustomerCode";
            worksheet.Cells[1, 3].Value = "AccountName";
            worksheet.Cells[1, 4].Value = "CountID";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới C1
            using (var range = worksheet.Cells["A1:D1"])
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
        public ActionResult ExportDeal()
        {
            //ViewBag.todate = todate;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDeal();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=CheckDealReport_.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion

        #region ACCOUNT
        //Phần controller New Tab ID_CT 
        //Type : JSON
        [HttpPost]
        public JsonResult CheckAccount_Report()
        {
            DealAndAccountFalseRepository dealandaccountfalseRepository = new DealAndAccountFalseRepository();
            ReturnDealAndAccountFalse returndealandaccountfalse = new ReturnDealAndAccountFalse();
            returndealandaccountfalse = dealandaccountfalseRepository.ACCOUNT_FALSE_DETAIL();
            return Json(returndealandaccountfalse, JsonRequestBehavior.AllowGet);
        }

        //Phần trả về data theo list để xuất excel DEAL
        [HttpGet]
        public List<AccountFalse_Detail> ReturnListExcelAccount()
        {

            DealAndAccountFalseRepository dealandaccountfalseRepository = new DealAndAccountFalseRepository();
            ReturnDealAndAccountFalse returndealandaccountfalse = new ReturnDealAndAccountFalse();
            returndealandaccountfalse = dealandaccountfalseRepository.ACCOUNT_FALSE_DETAIL();
            return returndealandaccountfalse.ListAccountFalse_Report;
        }

        public Stream CreateExcelFileAccount(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelAccount();
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
                BindingFormatForExcelAccount(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcelAccount(ExcelWorksheet worksheet, List<AccountFalse_Detail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "REFERENT_ACCOUNT";
            worksheet.Cells[1, 3].Value = "CountID";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới C1
            using (var range = worksheet.Cells["A1:C1"])
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
        public ActionResult ExportAccount()
        {
            //ViewBag.todate = todate;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileAccount();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=CheckAccountReport_.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion









    }
}