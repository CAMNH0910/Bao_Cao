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
using System.Security.Policy;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Spreadsheet;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;
using TableStyles = OfficeOpenXml.Table.TableStyles;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace T41.Areas.Admin.Controllers
{
    public class KPI_Delivery_WageController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KPI_Delivery_Wage()
        {

            return View();

        }
        public ActionResult KPI_Detail_Wage()
        {

            return View();

        }

        public ActionResult KPI_Delivery_CTTH()
        {
            return View();
        }

        public ActionResult KPI_Delivery_CT()
        {
            return View();
        }


        public ActionResult KPI_Delivery_CTCT()
        {
            return View();
        }
        public ActionResult KPI_Delivery_KGD()
        {
            return View();
        }



        //Controller lấy dữ liệu bưu cục phát
        public JsonResult PosCode(int zone)
        {
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            return Json(qualitydeliveryRepository.GetAllPOSCODE(zone), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }


        //Controller lấy dữ liệu tuyến phát
        public JsonResult RouteCode(int endpostcode)
        {
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            return Json(qualitydeliveryRepository.GetAllROUTECODE(endpostcode), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }


        #region Tổng hợp
        [HttpPost]
        public JsonResult ListKPI_Delivery_Wage(string startdate, string enddate, int zone, int endpostcode, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;

            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Delivery_Wage_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public List<KPI_Delivery_Wage> ReturnListExcel(string startdate, string enddate, int zone, int endpostcode, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Delivery_Wage_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service);
            return returnquality.ListDetaiKPI_Delivery_Wage;
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service);
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
                workSheet.Cells[6, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<KPI_Delivery_Wage> listItems)
        {
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ SẢN LƯỢNG ĐẾN CỦA BƯU CỤC PHÁT";
            worksheet.Cells["A1:L1"].Merge = true;

            worksheet.Cells[2, 13].Value = "MÃ BÁO CÁO:BT/TKSPL_N";
            worksheet.Cells["M2:M2"].Merge = true;

            worksheet.Cells[2, 6].Value = "Từ ngày:" + " " + ViewBag.startdate + "-" + "Đến ngày" +" "+ ViewBag.endDate;
            worksheet.Cells["F2:G2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells["A4:A5"].Merge = true;
            worksheet.Cells[4, 2].Value = "Đơn Vị";
            worksheet.Cells["B4:B5"].Merge = true;
            worksheet.Cells[4, 3].Value = "Bưu Cục";
            worksheet.Cells["C4:C5"].Merge = true;
            worksheet.Cells[4, 4].Value = "Tên Bưu Cục";
            worksheet.Cells["D4:D5"].Merge = true;
            worksheet.Cells[4, 5].Value = "Dịch vụ";
            worksheet.Cells["E4:E5"].Merge = true;
            worksheet.Cells[4, 6].Value = "SL Bưu gửi đến";
            worksheet.Cells["F4:F5"].Merge = true;
            worksheet.Cells[4, 7].Value = "SL đã lập BD13";
            worksheet.Cells["G4:G5"].Merge = true;
            worksheet.Cells[4, 8].Value = "SL chưa lập BD13";
            worksheet.Cells["H4:H5"].Merge = true;

            worksheet.Cells[4, 9].Value = "Trạng thái phát bưu gửi";
            worksheet.Cells["I4:I4"].Merge = true;
            worksheet.Cells[5, 9].Value = "SL phát thành công";
            worksheet.Cells[5, 10].Value = "SL phát không thành công ";
            worksheet.Cells[5, 11].Value = "Tổng cộng";

            worksheet.Cells[4, 12].Value = "SL Phát TC 6h";
            worksheet.Cells["L4:L5"].Merge = true;
            worksheet.Cells[4, 13].Value = "SL Phát TC 72h";
            worksheet.Cells["M4:M5"].Merge = true;

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:M5"])
            using (var ranges = worksheet.Cells["A1:M1"])
            using (var Ngay = worksheet.Cells["F2:H2"])
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
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(string startdate, string enddate, int zone, int endpostcode, int service)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đến BCP.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion
        #region Chi tiết tổng hợp phát không thành công
        [HttpPost]
        public JsonResult KPI_Delivery_CTTH(string startdate, string enddate, int zone, int endpostcode, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;

            KPI_Delivery_WageRepository kpi_KPI_Delivery_CTTH = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kpi_KPI_Delivery_CTTH.KPI_Delivery_CTTH(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public List<Detail_CTTH> ReturnListExcel_CTTH(string startdate, string enddate, int zone, int endpostcode, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Delivery_CTTH(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service);
            return returnquality.ListDetail_CTTH;
        }

        public Stream CreateExcelFile_CTTH(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_CTTH(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service);
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
                BindingFormatForExcel_CTTH(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_CTTH(ExcelWorksheet worksheet, List<Detail_CTTH> listItems)
        {
            var list = ReturnListExcel_CTTH(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "Bưu cục phát";
            worksheet.Cells[1, 4].Value = "Tên bưu cục phát";
            worksheet.Cells[1, 5].Value = "Mã tuyến phát";
            worksheet.Cells[1, 6].Value = "Tên tuyến phát";
            worksheet.Cells[1, 7].Value = "Mã bưu tá";
            worksheet.Cells[1, 8].Value = "Tên bưu tá   ";
            worksheet.Cells[1, 9].Value = "Ngày phát";
            worksheet.Cells[1, 10].Value = "Dịch vụ";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:J1"])
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
                //Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
               // ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
               // ranges.Style.Font.SetFromFont(new Font("Arial", 14));
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_CTTH(string startdate, string enddate, int zone, int endpostcode, int service)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_CTTH();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đi phát.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion

        #region Chi tiết tổng hợp không giao đi
        [HttpPost]
        public JsonResult KPI_Delivery_KGD(string startdate, string enddate, int zone, int endpostcode, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;

            KPI_Delivery_WageRepository kpi_KPI_Delivery_KGD = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kpi_KPI_Delivery_KGD.KPI_Delivery_KGD(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public List<Detail_KGD> ReturnListExcel_KGD(string startdate, string enddate, int zone, int endpostcode, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Delivery_KGD(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service);
            return returnquality.ListDetail_KGD;
        }

        public Stream CreateExcelFile_KGD(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_KGD(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service);
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
                BindingFormatForExcel_KGD(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_KGD(ExcelWorksheet worksheet, List<Detail_KGD> listItems)
        {
            var list = ReturnListExcel_KGD(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "Bưu cục phát";
            worksheet.Cells[1, 4].Value = "Tên bưu cục phát";
            worksheet.Cells[1, 5].Value = "Ngày phát";
            worksheet.Cells[1, 6].Value = "Dịch vụ";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:J1"])
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
                //Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                // ranges.Style.Font.SetFromFont(new Font("Arial", 14));
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_KGD(string startdate, string enddate, int zone, int endpostcode, int service)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_KGD();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết không giao đi.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion

        #region Tổng hợp bưu tá
        [HttpPost]
        public JsonResult ListKPI_Detail_Wage(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman,int date)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;
            ViewBag.date = date;

            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Detail_Wage_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service, routecode, postman, date);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
          public List<KPI_Detail_Wage> ReturnListExcelDetail(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman,int date)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;
            ViewBag.date = date;
            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Detail_Wage_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service, routecode, postman,date);
            return returnquality.ListDetailKPI_Detail_Wage;
        }


        public Stream CreateExcelFileDetail(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service, ViewBag.routecode, ViewBag.postman, ViewBag.date);
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
                workSheet.Cells[8, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel_Detail(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_Detail(ExcelWorksheet worksheet, List<KPI_Detail_Wage> listItems)
        {
            var list = ReturnListExcelDetail(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service, ViewBag.routecode, ViewBag.postman, ViewBag.date);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO THỐNG KÊ SẢN LƯỢNG  PHÁT CỦA BƯU TÁ";
            worksheet.Cells["A1:W1"].Merge = true;

            worksheet.Cells[2, 23].Value = "MÃ BÁO CÁO:BT/TKSLBT_N";
            worksheet.Cells["W2:W2"].Merge = true;

            worksheet.Cells[2, 11].Value = "Từ ngày:" + " " + ViewBag.startdate + "-" + "Đến ngày" + " " + ViewBag.endDate;
            worksheet.Cells["K2:M2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells["A4:A7"].Merge = true;
            worksheet.Cells[4, 2].Value = "Mã Bưu Cục";
            worksheet.Cells["B4:B7"].Merge = true;
            worksheet.Cells[4, 3].Value = "Tên Bưu Cục";
            worksheet.Cells["C4:C7"].Merge = true;
            worksheet.Cells[4, 4].Value = "Mã tuyến phát";
            worksheet.Cells["D4:D7"].Merge = true;
            worksheet.Cells[4, 5].Value = "Tên tuyến phát";
            worksheet.Cells["E4:E7"].Merge = true;
            worksheet.Cells[4, 6].Value = "Mã Nhân viên";
            worksheet.Cells["F4:F7"].Merge = true;
            worksheet.Cells[4, 7].Value = "ID Bưu tá";
            worksheet.Cells["G4:G7"].Merge = true;
            worksheet.Cells[4, 8].Value = "Họ tên Bưu tá";
            worksheet.Cells["H4:H7"].Merge = true;
            worksheet.Cells[4, 9].Value = "Dịch vụ";
            worksheet.Cells["I4:I7"].Merge = true;

            worksheet.Cells[4, 11].Value = "SL Bưu gửi phát";
            worksheet.Cells["J4:L4"].Merge = true;
            worksheet.Cells[5, 10].Value = "Bưu gửi mới";
            worksheet.Cells["J5:J7"].Merge = true;
            worksheet.Cells[5, 11].Value = "Bưu gửi chuyển tiếp";
            worksheet.Cells["K5:K7"].Merge = true;
            worksheet.Cells[5, 12].Value = "Sản lượng phát thực tế";
            worksheet.Cells["L5:L7"].Merge = true;

            worksheet.Cells[4, 13].Value = "Bưu gửi phát lại";
            worksheet.Cells["M4:M7"].Merge = true;

            worksheet.Cells[4, 14].Value = "Trạng thái phát bưu gửi";
            worksheet.Cells["N4:U4"].Merge = true;

            worksheet.Cells[5, 14].Value = "SL phát thành công";
            worksheet.Cells["N5:S5"].Merge = true;
            worksheet.Cells[6, 14].Value = "Đến 2KG";
            worksheet.Cells["N6:O6"].Merge = true;
            worksheet.Cells[7, 14].Value = "Sản lượng";
            worksheet.Cells[7, 15].Value = "Khối lượng";

            worksheet.Cells[6, 16].Value = "Trên 2KG";
            worksheet.Cells["P6:Q6"].Merge = true;
            worksheet.Cells[7, 16].Value = "Sản lượng";
            worksheet.Cells[7, 17].Value = "Khối lượng";

            worksheet.Cells[6, 18].Value = "Tổng cộng";
            worksheet.Cells["R6:S6"].Merge = true;
            worksheet.Cells[7, 18].Value = "Sản lượng";
            worksheet.Cells[7, 19].Value = "Khối lượng";

            worksheet.Cells[5, 20].Value = "SL phát không thành công ";
            worksheet.Cells["T5:T7"].Merge = true;
            worksheet.Cells[5, 21].Value = "Tổng cộng";
            worksheet.Cells["U5:U7"].Merge = true;

            worksheet.Cells[4, 22].Value = "SL Phát TC 6h";
            worksheet.Cells["V4:V7"].Merge = true;
            worksheet.Cells[4, 23].Value = "SL Phát TC 72h";
            worksheet.Cells["W4:W7"].Merge = true;

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:W7"])
            using (var ranges = worksheet.Cells["A1:W1"])
            using (var Ngay = worksheet.Cells["K2:M2"])
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
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult ExportDetail(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman, int date)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;
            ViewBag.date = date;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đi phát.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion


        #region Chi tiết bưu tá phát không thành công
        [HttpPost]
        public JsonResult KPI_Delivery_CT(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;

            KPI_Delivery_WageRepository kPI_KPI_Delivery_CT = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_KPI_Delivery_CT.KPI_Delivery_CT(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service, routecode, postman);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public List<Detail_CT> ReturnListExcelDetail_CT(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;
            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Delivery_CT(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service, routecode, postman);
            return returnquality.ListDetail_CT;
        }


        public Stream CreateExcelFileDetail_CT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail_CT(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service, ViewBag.routecode, ViewBag.postman);
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
                BindingFormatForExcel_CT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_CT(ExcelWorksheet worksheet, List<Detail_CT> listItems)
        {
            var list = ReturnListExcelDetail_CT(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service, ViewBag.routecode, ViewBag.postman);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "Bưu cục phát";
            worksheet.Cells[1, 4].Value = "Tên bưu cục phát";
            worksheet.Cells[1, 5].Value = "Mã tuyến phát";
            worksheet.Cells[1, 6].Value = "Tên tuyến phát";
            worksheet.Cells[1, 7].Value = "Mã bưu tá";
            worksheet.Cells[1, 8].Value = "Tên bưu tá   ";
            worksheet.Cells[1, 9].Value = "Ngày phát";
            worksheet.Cells[1, 10].Value = "Dịch vụ";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:J1"])
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
                //Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                // ranges.Style.Font.SetFromFont(new Font("Arial", 14));
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult ExportDetail_CT(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail_CT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đi phát.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion

        #region Chi tiết bưu tá chuyển tiếp
        [HttpPost]
        public JsonResult KPI_Delivery_CTCT(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;

            KPI_Delivery_WageRepository kPI_KPI_Delivery_CT = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_KPI_Delivery_CT.KPI_Delivery_CTCT(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service, routecode, postman);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public List<Detail_CTCT> ReturnListExcelDetail_CTCT(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;
            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Delivery_CTCT(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service, routecode, postman);
            return returnquality.ListDetail_CTCT;
        }


        public Stream CreateExcelFileDetail_CTCT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail_CTCT(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service, ViewBag.routecode, ViewBag.postman);
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
                BindingFormatForExcel_CTCT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_CTCT(ExcelWorksheet worksheet, List<Detail_CTCT> listItems)
        {
            var list = ReturnListExcelDetail_CTCT(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service, ViewBag.routecode, ViewBag.postman);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã E1";
            worksheet.Cells[1, 3].Value = "Bưu cục phát";
            worksheet.Cells[1, 4].Value = "Tên bưu cục phát";
            worksheet.Cells[1, 5].Value = "Mã tuyến phát";
            worksheet.Cells[1, 6].Value = "Tên tuyến phát";
            worksheet.Cells[1, 7].Value = "Mã bưu tá";
            worksheet.Cells[1, 8].Value = "Tên bưu tá   ";
            worksheet.Cells[1, 9].Value = "Ngày phát";
            worksheet.Cells[1, 10].Value = "Dịch vụ";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:J1"])
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
                //Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                // ranges.Style.Font.SetFromFont(new Font("Arial", 14));
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult ExportDetail_CTCT(string startdate, string enddate, int zone, int endpostcode, int service, int routecode, int postman)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            ViewBag.routecode = routecode;
            ViewBag.postman = postman;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail_CTCT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đi phát.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }


        #endregion
        #region Tổng hợp theo khối lượng
        [HttpPost]
        public JsonResult ListDetail_KG(string startdate, string enddate, int zone, int endpostcode, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;

            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Delivery_KG(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public List<Detail_KG> ReturnListExcel_KG(string startdate, string enddate, int zone, int endpostcode, int service)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;
            KPI_Delivery_WageRepository kPI_CustomerRepository = new KPI_Delivery_WageRepository();
            ReturnKPI_Wage returnquality = new ReturnKPI_Wage();
            returnquality = kPI_CustomerRepository.KPI_Delivery_KG(common.DateToInt(startdate), common.DateToInt(enddate), zone, endpostcode, service);
            return returnquality.ListDetail_KG;
        }

        public Stream CreateExcelFile_KG(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_KG(ViewBag.startdate, ViewBag.enddate, ViewBag.zone, ViewBag.endpostcode, ViewBag.service);
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
                BindingFormatForExcel_KG(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_KG(ExcelWorksheet worksheet, List<Detail_KG> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Khu vực";
            worksheet.Cells[1, 3].Value = "Bưu cục ";
            worksheet.Cells[1, 4].Value = "Tên bưu cục ";
            worksheet.Cells[1, 5].Value = "Dịch vụ";
            worksheet.Cells[1, 6].Value = "Tổng sản lượng";
            worksheet.Cells[1, 7].Value = "SL Dưới 5KG";
            worksheet.Cells[1, 8].Value = "KL Dưới 5KG";
            worksheet.Cells[1, 9].Value = "SL Trên 5KG";
            worksheet.Cells[1, 10].Value = "KL Trên 5KG";
            worksheet.Cells[1, 11].Value = "PTC 6H";
            worksheet.Cells[1, 12].Value = "PTC 72H";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:L1"])
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
                //Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                // ranges.Style.Font.SetFromFont(new Font("Arial", 14));
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }
        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_KG(string startdate, string enddate, int zone, int endpostcode, int service)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.service = service;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_KG();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đến BCP.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
       
       
        //Hàm Export excel  , truyền parameter vào để export



        #endregion
    }
}