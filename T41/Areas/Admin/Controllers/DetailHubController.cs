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

namespace T41.Areas.Admin.Controllers
{

    public class DetailHubController : Controller
    {
        Convertion common = new Convertion();      
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ProvinceCode()
        {
            DetailHubRepository qualityDetailHubRepository = new DetailHubRepository();
            return Json(qualityDetailHubRepository.GetProvinceEms(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProvinceAcceptCode()
        {
            DetailHubRepository qualityDetailHubRepository = new DetailHubRepository();
            return Json(qualityDetailHubRepository.GetProvinceAcceptEms(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
 

        public ActionResult TotalHub()
        {
            return View();

        }

        public ActionResult ListDetailedHubFailReport()
        {
            return View();

        }

        [HttpPost]
        public JsonResult ListTotalHub(int StartProvince, int EndProvince, int IsService, string StartDate, string EndDate)
        {
          
            ViewBag.StartProvince = StartProvince;          
            ViewBag.EndProvince = EndProvince;          
            ViewBag.IsService = IsService;          
            //ViewBag.IsCod = IsCod;          
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            DetailHubRepository qualityDetailHubRepository = new DetailHubRepository();
            ReturnDetailHub returnquality = new ReturnDetailHub();
            returnquality = qualityDetailHubRepository.TOTAL_HUB_DETAIL(StartProvince, EndProvince, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListDetailHubFail(int StartProvince, int EndProvince,int IsSDistrict, int IsEDistrict, int IsserviceNumber, string StartDate, string EndDate)
        {

            ViewBag.StartProvince = StartProvince;
            ViewBag.IsSDistrict = EndProvince;

           // ViewBag.IsEDistrict = IsEndDistrict;
          //  ViewBag.IsEDistrict = IsEndDistrict;

          //  ViewBag.IsService = IsService;
          
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            DetailHubRepository qualityDetailHubRepository = new DetailHubRepository();
            ReturnDetailHub returnquality = new ReturnDetailHub();
            returnquality = qualityDetailHubRepository.DETAIL_HUB_FAIL(StartProvince, EndProvince, IsSDistrict, IsEDistrict, IsserviceNumber, common.DateToInt(StartDate), common.DateToInt(EndDate));

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<TotalHub_Excel> ReturnListExcelTotalHub(int StartProvince, int EndProvince, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.IsService = IsService;
           // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            DetailHubRepository qualityDetailHubRepository = new DetailHubRepository();
            ReturnDetailHub returnquality = new ReturnDetailHub();
            returnquality = qualityDetailHubRepository.TOTAL_HUB_DETAIL_EXCEL(StartProvince, EndProvince, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListTotalHub_ExcelReport;
        }

        [HttpGet]
        public List<DetailHubFail> ReturnListExcelDetailHubFail(int StartProvince, int EndProvince, int IsSDistrict, int IsEDistrict, int IsserviceNumber, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.IsSDistrict = IsSDistrict;
            ViewBag.IsEDistrict = IsEDistrict;
            ViewBag.IsserviceNumber = IsserviceNumber;
            // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            DetailHubRepository qualityDetailHubRepository = new DetailHubRepository();
            ReturnDetailHub returnquality = new ReturnDetailHub();
            returnquality = qualityDetailHubRepository.DETAIL_HUB_FAIL(StartProvince, EndProvince, IsSDistrict, IsEDistrict, IsserviceNumber, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListDetailHubFailReport;
        }


        public Stream CreateExcelFileTotalHub(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelTotalHub(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcelTotalHub(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFileDetailHubFail(Stream stream = null)
        {

            //var list = CreateTestItems();
            var list = ReturnListExcelDetailHubFail(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.IsSDistrict, ViewBag.IsEDistrict, ViewBag.IsserviceNumber,  ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcelDetailHubFail(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcelTotalHub(ExcelWorksheet worksheet, List<TotalHub_Excel> listItems)
        {
            var list = ReturnListExcelTotalHub(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT LƯỢNG KPI HƯỚNG TRỌNG ĐIỂM";
            worksheet.Cells["A1:P1"].Merge = true;

            worksheet.Cells[2, 16].Value = "MÃ BÁO CÁO:TD/KPI_TD";
            worksheet.Cells["P2:P2"].Merge = true;

            worksheet.Cells[2, 8].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.endDate;
            worksheet.Cells["H2:I2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã tỉnh nhận";
            worksheet.Cells[4, 3].Value = "Tên tỉnh nhận";
            worksheet.Cells[4, 4].Value = "Phân tuyến tỉnh nhận";
            worksheet.Cells[4, 5].Value = "Mã tỉnh phát";
            worksheet.Cells[4, 6].Value = "Tên tỉnh phát";
            worksheet.Cells[4, 7].Value = "Phân tuyến tỉnh phát";
            worksheet.Cells[4, 8].Value = "Dịch vụ";
            worksheet.Cells[4, 9].Value = "Chỉ tiêu";
            worksheet.Cells[4, 10].Value = "Tổng số bưu gửi phát thành công";
            worksheet.Cells[4, 11].Value = "Tỉ lệ bưu gửi phát thành công";
            worksheet.Cells[4, 12].Value = "Tổng số bưu gửi";
            worksheet.Cells[4, 13].Value = "Số bưu gửi đặt chỉ tiêu";
            worksheet.Cells[4, 14].Value = "Tỉ lệ đặt chỉ tiêu";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 15].Value = "Số bưu gửi không đạt chỉ tiêu";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 16].Value = "Tỉ lệ không đạt chỉ tiêu";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
                                                                               // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:P4"])
            using (var ranges = worksheet.Cells["A1:P1"])
            using (var Ngay = worksheet.Cells["H2:I2"])
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

        private void BindingFormatForExcelDetailHubFail(ExcelWorksheet worksheet, List<DetailHubFail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã Bưu Gửi";
            worksheet.Cells[1, 3].Value = "Dịch Vụ";
            worksheet.Cells[1, 4].Value = "BC nhận";
            worksheet.Cells[1, 5].Value = "Tên BC nhận";
            worksheet.Cells[1, 6].Value = "Q/H nhận";
            worksheet.Cells[1, 7].Value = "Tên Q/H nhận";
            worksheet.Cells[1, 8].Value = "Tỉnh nhận";
            worksheet.Cells[1, 9].Value = "Tên tỉnh nhận";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 10].Value = "T/G chấp nhận";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 11].Value = "BC phát";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 12].Value = "Tên BC phát";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 13].Value = "Q/H Phát";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 14].Value = "Tên Q/H Phát";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 15].Value = "Tỉnh phát";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 16].Value = "Tên tỉnh phát";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 17].Value = "T/G phát";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 18].Value = "CTCB";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 19].Value = "CTTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 20].Value = "Trạng thái";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult ExportTotalHub(int StartProvince, int EndProvince, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.IsService = IsService;
           // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileTotalHub();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp KPI hướng trọng điểm.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExportDetailHubFail(int StartProvince, int EndProvince, int IsSDistrict, int IsEDistrict, int IsserviceNumber, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.IsSDistrict = IsSDistrict;
            ViewBag.IsEDistrict = IsEDistrict;
            ViewBag.IsserviceNumber = IsserviceNumber;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetailHubFail();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết KPI hướng trọng điểm.xlsx");
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