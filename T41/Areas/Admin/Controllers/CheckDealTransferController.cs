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
    public class CheckDealTransferController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckDealTransferDetailReport()
        {
            var userid = Convert.ToInt32(Session["userid"]);
            //Phân quyền đăng nhập
            if (userid == 1 || userid == 2)
            {
                return View();
                
            }
            else {
                return RedirectToAction("Index", "Home");
            }
            
        }

        //Phần controller view ID_E2 
        public ActionResult ListCheckDealTransfer_ID_E2_Report()
        {
            return View();
        }

        //Phần controller view ID_CT
        public ActionResult ListCheckDealTransfer_ID_CT_Report()
        {
            return View();
        }

        //Controller lấy dữ liệu tỉnh đóng, tỉnh nhận
        public JsonResult PosCode(int id_don_vi)
        {
            CheckDealTransferRepository checkdealtransferRepository = new CheckDealTransferRepository();
            return Json(checkdealtransferRepository.GETPOSCODE_GIAO_DICH(id_don_vi), JsonRequestBehavior.AllowGet);
        }


        //Phần controller Detail
        //Type : ViewHtml
        public ActionResult ListCheckDealTransfer_Report(string fromdate, string todate, int ma_bc_khai_thac)
        {
            CheckDealTransferRepository checkdealtransferRepository = new CheckDealTransferRepository();
            ReturnCheckDealTransfer returncheckdealtransfer = new ReturnCheckDealTransfer();
            returncheckdealtransfer = checkdealtransferRepository.CHECK_DEAL_TRANSFER_DETAIL(common.DateToInt(fromdate), common.DateToInt(todate), ma_bc_khai_thac);
            return View(returncheckdealtransfer);
        }



        //Phần controller Modal 
        //Type : ViewHtml
        [HttpPost]
        public ActionResult CheckDeal_Modal_Report(string id_chuyen_thu, int? ma_bc_khai_thac, Int64? mailtrip_key)
        {
            CheckDealTransferRepository checkdealtransferRepository = new CheckDealTransferRepository();
            ReturnCheckDealTransfer returncheckdealtransfer = new ReturnCheckDealTransfer();
            returncheckdealtransfer = checkdealtransferRepository.CHECK_DEAL_TRANSFER_MODAL_DETAIL(id_chuyen_thu, ma_bc_khai_thac, mailtrip_key);
            return Json(returncheckdealtransfer,JsonRequestBehavior.AllowGet);
        }

        //Phần controller New Tab ID_E2 
        //Type : JSON
        [HttpPost]
        public JsonResult CheckDeal_NewTab_ID_E2_Report(string id_chuyen_thu, int? ma_bc_khai_thac, Int64? mailtrip_key)
        {
            CheckDealTransferRepository checkdealtransferRepository = new CheckDealTransferRepository();
            ReturnCheckDealTransfer returncheckdealtransfer = new ReturnCheckDealTransfer();
            returncheckdealtransfer = checkdealtransferRepository.CHECK_DEAL_TRANSFER_BY_ID_E2_DETAIL(id_chuyen_thu, ma_bc_khai_thac, mailtrip_key);
            return Json(returncheckdealtransfer, JsonRequestBehavior.AllowGet);
        }

        //Phần controller New Tab ID_CT 
        //Type : JSON
        [HttpPost]
        public JsonResult CheckDeal_NewTab_ID_CT_Report(string id_chuyen_thu, int? ma_bc_khai_thac, Int64? mailtrip_key)
        {
            CheckDealTransferRepository checkdealtransferRepository = new CheckDealTransferRepository();
            ReturnCheckDealTransfer returncheckdealtransfer = new ReturnCheckDealTransfer();
            returncheckdealtransfer = checkdealtransferRepository.CHECK_DEAL_TRANSFER_BY_ID_CT_DETAIL(id_chuyen_thu, ma_bc_khai_thac, mailtrip_key);
            return Json(returncheckdealtransfer, JsonRequestBehavior.AllowGet);
        }


        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<CheckDealTransfer_Excel_Detail> ReturnListExcel(string fromdate, string todate, int ma_bc_khai_thac)
        {

            CheckDealTransferRepository checkdealtransferRepository = new CheckDealTransferRepository();
            ReturnCheckDealTransfer returncheckdealtransfer = new ReturnCheckDealTransfer();
            returncheckdealtransfer = checkdealtransferRepository.CHECK_DEAL_TRANSFER_EXCEL_DETAIL(common.DateToInt(fromdate), common.DateToInt(todate), ma_bc_khai_thac);
            return returncheckdealtransfer.ListCheckDealTransfer_Excel_Report;
        }

       
        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.fromdate,ViewBag.todate,ViewBag.ma_bc_khai_thac);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<CheckDealTransfer_Excel_Detail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "ID chuyến thư";
            worksheet.Cells[1, 3].Value = "Mã bưu cục khai thác";
            worksheet.Cells[1, 4].Value = "Số chuyến thư";
            worksheet.Cells[1, 5].Value = "Ngày đóng";
            worksheet.Cells[1, 6].Value = "Giờ đóng";
            worksheet.Cells[1, 7].Value = "Tổng số túi";
            
            
            worksheet.Cells[1, 8].Value = "Tổng khối lượng";
            worksheet.Cells[1, 9].Value = "Tổng khối lượng bưu phẩm";
            worksheet.Cells[1, 10].Value = "Tổng cước COD";
            worksheet.Cells[1, 11].Value = "Tổng cước dịch vụ";
            worksheet.Cells[1, 12].Value = "Tổng cước";
            worksheet.Cells[1, 13].Value = "HH phát hành";
            worksheet.Cells[1, 14].Value = "HH phát trả";
            worksheet.Cells[1, 15].Value = "IP máy chủ";
            worksheet.Cells[1, 16].Value = "Tổng số bưu phẩm";
            worksheet.Cells[1, 17].Value = "Tổng số bưu phẩm đối soát";




            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới O1
            using (var range = worksheet.Cells["A1:O1"])
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

            using (var range1 = worksheet.Cells["P1:Q1"])
            {
                // Set PatternType
                range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                range1.Style.Fill.BackgroundColor.SetColor(Color.Red);
                // Canh giữa cho các text
                range1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range1.Style.Font.SetFromFont(new Font("Arial", 11));
                // Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string fromdate, string todate, int ma_bc_khai_thac)
        {
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            ViewBag.ma_bc_khai_thac = ma_bc_khai_thac;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Đối soát truyền nhận giao dịch_" + fromdate + "_DenNgay_" + todate +  ".xlsx");
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