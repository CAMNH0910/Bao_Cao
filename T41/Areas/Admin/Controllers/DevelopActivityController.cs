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
//EPPLUS library
using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;



namespace T41.Areas.Admin.Controllers
{
    public class DevelopActivityController : Controller
    {
        //int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/AirwayTransport
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult DevelopActivityDetailReport()
        {
            return View();
        }


        public ActionResult ListDevelopActivity()
        {
            return View();
        }

        public ActionResult DevelopActivity_NOI_TINH_DetailReport()
        {
            return View();
        }


        public ActionResult ListDevelopActivity_NOI_TINH()
        {
            return View();
        }

        public ActionResult ListDevelopActivity_CHI_TIET_LIEN_TINH()
        {
            return View();
        }

        //Phần controller xử lý để lấy dữ liệu  LIÊN TỈNH
        [HttpGet]
        public JsonResult ListDevelopActivity_BDHN_DI_HCM(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner )
        {
            long ARRIVEQUANTITY_LK = 0;
            decimal ARRIVEWEIGHT_KG_LK = 0;

            long LEAVEQUANTITY_LK = 0;
            decimal LEAVEWEIGHT_KG_LK = 0;
            decimal DAPUNGKL = 0;
            decimal DAPUNGLUYKE = 0;

            DevelopActivityRepository developactivityrepository = new DevelopActivityRepository();
            ReturnBDHN_DI_HCM returnBDHN_DI_HCM = new ReturnBDHN_DI_HCM();
            returnBDHN_DI_HCM = developactivityrepository.BDHN_DI_HCM( workcenter,  AcceptDate,  arriveprovince,  arrivepartner, ref ARRIVEQUANTITY_LK, ref ARRIVEWEIGHT_KG_LK, ref  LEAVEQUANTITY_LK, ref  LEAVEWEIGHT_KG_LK, ref DAPUNGKL, ref DAPUNGLUYKE);
            
            return Json(returnBDHN_DI_HCM, JsonRequestBehavior.AllowGet);
            
        }

        //Phần controller xử lý để lấy dữ liệu CHI TIẾT LIÊN TỈNH
        [HttpGet]
        public JsonResult ListDevelopActivity_Action_CHI_TIET_LIEN_TINH_ACTION(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner)
        {
            DevelopActivityRepository developactivityrepository = new DevelopActivityRepository();
            ReturnCHI_TIET_LIEN_TINH returnCHI_TIET_LIEN_TINH = new ReturnCHI_TIET_LIEN_TINH();
            returnCHI_TIET_LIEN_TINH = developactivityrepository.CHI_TIET_LIEN_TINH(workcenter, AcceptDate, arriveprovince, arrivepartner);
            return Json(returnCHI_TIET_LIEN_TINH, JsonRequestBehavior.AllowGet);

        }

        //Phần controller xử lý để lấy dữ liệu NỘI TỈNH
        [HttpGet]
        public JsonResult ListDevelopActivity_Action_NOI_TINH(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner, int leaveprovince, int transitbag)
        {
            DevelopActivityRepository developactivityrepository = new DevelopActivityRepository();
            ReturnNOI_TINH returnNOI_TINH = new ReturnNOI_TINH();
            returnNOI_TINH = developactivityrepository.NOI_TINH( workcenter,  AcceptDate,  arriveprovince,  arrivepartner,  leaveprovince,  transitbag);

            return Json(returnNOI_TINH, JsonRequestBehavior.AllowGet);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<BDHN_DI_HCM> ReturnListExcel(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner)
        {
            long ARRIVEQUANTITY_LK = 0;
            decimal ARRIVEWEIGHT_KG_LK = 0;

            long LEAVEQUANTITY_LK = 0;
            decimal LEAVEWEIGHT_KG_LK = 0;
            decimal DAPUNGKL = 0;
            decimal DAPUNGLUYKE = 0;

            DevelopActivityRepository developactivityrepository = new DevelopActivityRepository();
            ReturnBDHN_DI_HCM returnBDHN_DI_HCM = new ReturnBDHN_DI_HCM();
            returnBDHN_DI_HCM = developactivityrepository.BDHN_DI_HCM(workcenter, AcceptDate, arriveprovince, arrivepartner, ref ARRIVEQUANTITY_LK, ref ARRIVEWEIGHT_KG_LK, ref LEAVEQUANTITY_LK, ref LEAVEWEIGHT_KG_LK, ref DAPUNGKL, ref DAPUNGLUYKE);

            return returnBDHN_DI_HCM.ListBDHN_DI_HCMReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel( ViewBag.workcenter, ViewBag.AcceptDate, ViewBag.arriveprovince, ViewBag.arrivepartner);
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
                workSheet.Cells[4, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel

        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<BDHN_DI_HCM> listItems)
        {
            var list = ReturnListExcel(ViewBag.workcenter, ViewBag.AcceptDate, ViewBag.arriveprovince, ViewBag.arrivepartner);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;

            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO KPI TỔNG HỢP SẢN LƯỢNG TRƯỢT CHUYẾN";
            worksheet.Cells["A1:X1"].Merge = true;

            worksheet.Cells[2, 24].Value = "MÃ BÁO CÁO:KT/THSL_QT";
            worksheet.Cells["X2:X2"].Merge = true;

            worksheet.Cells[2, 12].Value = "Từ ngày:" + " " + ViewBag.AcceptDate;
            worksheet.Cells["L2:M2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Thời gian đến";
            worksheet.Cells[4, 3].Value = "ID chuyến đến";
            worksheet.Cells[4, 4].Value = "Tên chuyến đến";
            worksheet.Cells[4, 5].Value = "Mã Đơn Vị";
            worksheet.Cells[4, 6].Value = "Tên Đơn Vị";
            worksheet.Cells[4, 7].Value = "SL đến";
            worksheet.Cells[4, 8].Value = "KL đến (kg)";
            worksheet.Cells[4, 9].Value = "SL đến lũy kế";
            worksheet.Cells[4, 10].Value = "KL đến lũy kế";
            worksheet.Cells[4, 11].Value = "SL tồn lũy kế";
            worksheet.Cells[4, 12].Value = "KLg tồn lũy kế";
            worksheet.Cells[4, 13].Value = "Thời gian";
            worksheet.Cells[4, 14].Value = "ID Tuyến đi";
            worksheet.Cells[4, 15].Value = "Tên tuyến đi";
            worksheet.Cells[4, 16].Value = "ID chuyến đi";
            worksheet.Cells[4, 17].Value = "Tên chuyến đi";
            worksheet.Cells[4, 18].Value = "SL đi";
            worksheet.Cells[4, 19].Value = "KL đi (kg)";
            worksheet.Cells[4, 20].Value = "SL đi lũy kế";
            worksheet.Cells[4, 21].Value = "KL đi lũy kế";
            worksheet.Cells[4, 22].Value = "Đáp ứng (SL)";
            worksheet.Cells[4, 23].Value = "Đáp ứng (KLg)";
            worksheet.Cells[4, 24].Value = "Tỷ lệ đáp ứng lũy kế theo KLG";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:X4"])
            using (var ranges = worksheet.Cells["A1:X1"])
            using (var Ngay = worksheet.Cells["L2:M2"])
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
                Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));
            }


            }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner)
        {
            ViewBag.workcenter = workcenter;
            ViewBag.AcceptDate = AcceptDate;
            ViewBag.arriveprovince = arriveprovince;
            ViewBag.arrivepartner = arrivepartner;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo hoạt động tại sàn khai thác" + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
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