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
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Controllers
{

    public class KPIHTController : Controller
    {
        Convertion common = new Convertion();      
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListProvince()

        {
            KPIHTRepository repository = new KPIHTRepository();
            return Json(repository.GetProvince(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult KPI_Total_HT()
        {
            return View();

        }
        public ActionResult ListItemDetailReport()
        {
            return View();

        }

        [HttpPost]
        public JsonResult ListKPI_Total_HT(int StartProvince,int EndProvince, string StartDate, string EndDate)
        {
          
            ViewBag.StartProvince = StartProvince;          
            ViewBag.EndProvince = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIHTRepository repository = new KPIHTRepository();
            ResponseKPI_Total_HT returnquality = new ResponseKPI_Total_HT();
            returnquality = repository.KPI_TOTAL_HT(StartProvince, EndProvince, common.DateToInt(StartDate), common.DateToInt(EndDate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ListKPI_Detail_Item_Hub_Fail(int StartProvince, int EndProvince, int AcceptFromCutOff, int AcceptToCutOff, int DeliveryCutOff, string StartDate, string EndDate)
        {

            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.AcceptFromCutOff = AcceptFromCutOff;
            ViewBag.AcceptToCutOff = AcceptToCutOff;
            ViewBag.DeliveryCutOff = DeliveryCutOff;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIHTRepository repository = new KPIHTRepository();
            ResponseKPI_Detail_Fail_Hub returnquality = new ResponseKPI_Detail_Fail_Hub();
            returnquality = repository.KPI_ITEM_HUB_FAIL(StartProvince, EndProvince, AcceptFromCutOff, AcceptToCutOff, DeliveryCutOff, common.DateToInt(StartDate), common.DateToInt(EndDate));
            //return View(returnquality.ListQualityDeliveryReport);
            // return View(returnquality);
            // return Json(returnquality, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<KPI_Total_HT> ReturnListExcel_Total_HT(int StartProvince, int EndProvince, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIHTRepository repository = new KPIHTRepository();
            ResponseKPI_Total_HT returnquality = new ResponseKPI_Total_HT();
            returnquality = repository.KPI_TOTAL_HT(StartProvince, EndProvince, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.listKPI_Total_HTReport;
        }
        [HttpGet]
        public List<KPI_Detail_Fail_Hub> ReturnListExcel_Detail_Item_Hub_Fail(int StartProvince, int EndProvince, int AcceptFromCutOff, int AcceptToCutOff, int DeliveryCutOff, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.AcceptFromCutOff = AcceptFromCutOff;
            ViewBag.AcceptToCutOff = AcceptToCutOff;
            ViewBag.DeliveryCutOff = DeliveryCutOff;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIHTRepository repository = new KPIHTRepository();
            ResponseKPI_Detail_Fail_Hub returnquality = new ResponseKPI_Detail_Fail_Hub();
            returnquality = repository.KPI_ITEM_HUB_FAIL(StartProvince, EndProvince, AcceptFromCutOff, AcceptToCutOff, DeliveryCutOff, common.DateToInt(StartDate), common.DateToInt(EndDate));
            //return View(returnquality.ListQualityDeliveryReport);
            return returnquality.listKPI_Detail_Fail_HubReport;
        }


        public Stream CreateExcelFile_Total_Hub(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_Total_HT(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcel_Total_Hub(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFile_Detail_Item_Hub_Fail(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_Detail_Item_Hub_Fail(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.AcceptFromCutOff, ViewBag.AcceptToCutOff, ViewBag.DeliveryCutOff, ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcel_Detail_Item_Fails(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Total_Hub(ExcelWorksheet worksheet, List<KPI_Total_HT> listItems)
        {
            var list = ReturnListExcel_Total_HT(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.StartDate, ViewBag.EndDate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT DỊCH VỤ HỎA TỐC";
            worksheet.Cells["A1:K1"].Merge = true;

            worksheet.Cells[2, 11].Value = "MÃ BÁO CÁO:CLDV/DVHT";
            worksheet.Cells["K2:K2"].Merge = true;

            worksheet.Cells[2, 5].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["E2:G2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã tỉnh nhận";
            worksheet.Cells[4, 3].Value = "Tên tỉnh nhận";
            worksheet.Cells[4, 4].Value = "Mã tỉnh trả";
            worksheet.Cells[4, 5].Value = "Tên tỉnh trả";
            worksheet.Cells[4, 6].Value = "Chỉ tiêu";
            worksheet.Cells[4, 7].Value = "Tổng số";
            worksheet.Cells[4, 8].Value = "Sản lượng đạt";
            worksheet.Cells[4, 9].Value = "Tỉ lệ đạt";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 10].Value = "Sản lượng trượt";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 11].Value = "Tỉ lệ trượt";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1

            using (var range = worksheet.Cells["A4:K4"])
            using (var ranges = worksheet.Cells["A1:K1"])
            using (var Ngay = worksheet.Cells["E2:G2"])
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

        private void BindingFormatForExcel_Detail_Item_Fails(ExcelWorksheet worksheet, List<KPI_Detail_Fail_Hub> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "Bưu cục nhận";
            worksheet.Cells[1, 4].Value = "Tên bưu cục nhận";
            worksheet.Cells[1, 5].Value = "Thời gian nhận";
            worksheet.Cells[1, 6].Value = "Bưu cục phát";
            worksheet.Cells[1, 7].Value = "Tên bưu cục phát";
            worksheet.Cells[1, 8].Value = "Thời gian phát";
            worksheet.Cells[1, 9].Value = "Mã tỉnh nhận";
            worksheet.Cells[1, 10].Value = "Tên tỉnh nhận";
            worksheet.Cells[1, 11].Value = "Mã tỉnh trả";
            worksheet.Cells[1, 12].Value = "Tên tỉnh trả";
            worksheet.Cells[1, 13].Value = "Chỉ tiêu";
            worksheet.Cells[1, 14].Value = "Ghi chú";
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
        public ActionResult Export_Total_HT(int StartProvince, int EndProvince, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_Total_Hub();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Danh sách Tổng hợp chất lượng dịch vụ hỏa tốc.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Export_Detail_Item_Fail_Hub(int StartProvince, int EndProvince, int AcceptFromCutOff, int AcceptToCutOff, int DeliveryCutOff, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.AcceptFromCutOff = AcceptFromCutOff;
            ViewBag.AcceptToCutOff = AcceptToCutOff;
            ViewBag.DeliveryCutOff = DeliveryCutOff;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_Detail_Item_Hub_Fail();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Danh sách chi tiết chất lượng dịch vụ hỏa tốc.xlsx");
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