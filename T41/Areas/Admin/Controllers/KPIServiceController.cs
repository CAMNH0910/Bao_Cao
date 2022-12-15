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

    public class KPIServiceController : Controller
    {
        Convertion common = new Convertion();      
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult PosCode()

        {
            KPIServiceRepository kpiserviceRepository = new KPIServiceRepository();
            return Json(kpiserviceRepository.GetProvince(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult KPIService()
        {
            return View();

        }
        [HttpPost]
        public JsonResult ListDetailTotalKPIService(Int32 StartProvince,Int32 EndProvince, Int32 isService, Int32 IsCod, string StartDate, string EndDate)
        {
          
            ViewBag.StartProvince = StartProvince;          
            ViewBag.EndProvince = EndProvince;
            ViewBag.isService = isService;
            ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIServiceRepository qualityKPIServiceRepository = new KPIServiceRepository();
            ReturnKPIService returnquality = new ReturnKPIService();
            returnquality = qualityKPIServiceRepository.KPIService_DETAIL(StartProvince,EndProvince,isService,IsCod, common.DateToInt(StartDate), common.DateToInt(EndDate));       
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListDetailItemKPIService(Int32 StartProvince, Int32 EndProvince, Int32 isService, Int32 IsCod, string StartDate, string EndDate)
        {

            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.isService = isService;
            ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIServiceRepository qualityKPIServiceRepository = new KPIServiceRepository();
            ReturnKPIServiceItem returnquality = new ReturnKPIServiceItem();
            returnquality = qualityKPIServiceRepository.KPIServiceItem_DETAIL(StartProvince, EndProvince, isService, IsCod, common.DateToInt(StartDate), common.DateToInt(EndDate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<KPIServiceDetail> ReturnListExcel(Int32 StartProvince, Int32 EndProvince, Int32 isService, Int32 IsCod, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.isService = isService;
            ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIServiceRepository qualityKPIServiceRepository = new KPIServiceRepository();
            ReturnKPIService returnquality = new ReturnKPIService();
            returnquality = qualityKPIServiceRepository.KPIService_DETAIL(StartProvince, EndProvince, isService, IsCod, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListKPIServiceReport;
        }

        public List<KPIServiceItemDetail> ReturnListItemExcel(Int32 StartProvince, Int32 EndProvince, Int32 isService, Int32 IsCod, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.isService = isService;
            ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIServiceRepository qualityKPIServiceRepository = new KPIServiceRepository();
            ReturnKPIServiceItem returnqualityItem = new ReturnKPIServiceItem();
            returnqualityItem = qualityKPIServiceRepository.KPIServiceItem_DETAIL(StartProvince, EndProvince, isService, IsCod, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnqualityItem.ListKPIServiceItemReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.isService, ViewBag.IsCod, ViewBag.StartDate, ViewBag.EndDate);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<AffairDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã tỉnh đi";
            worksheet.Cells[1, 3].Value = "Tên tỉnh";
            worksheet.Cells[1, 4].Value = "Mã tỉnh đến";
            worksheet.Cells[1, 5].Value = "Tên tỉnh đến";
            worksheet.Cells[1, 6].Value = "Tổng cộng";
            worksheet.Cells[1, 7].Value = "J0";
            worksheet.Cells[1, 8].Value = "TLJ0";
            worksheet.Cells[1, 9].Value = "Ngày sự vụ";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult Export(Int32 poscode, string startdate, string enddate)
        {
            ViewBag.poscode = poscode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Danh sách bưu gửi sự vụ "+ poscode + ".xlsx");
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