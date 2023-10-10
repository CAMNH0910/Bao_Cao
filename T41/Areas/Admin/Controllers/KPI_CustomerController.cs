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
    public class KPI_CustomerController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult KPI_Customer_Report()
        {
            return View();
        }

        public ActionResult KPI_Customer_Detail()
        {
            return View();
        }
        public ActionResult KPI_Customer_KDCT()
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
        public ActionResult KPI_Customer()
        {
            return View();

        }



        #region Tổng hợp chất lượng phát
        [HttpPost]
        public JsonResult ListKPI_Customer(int StartProvince, string Customer, string StartDate, string EndDate, int EndProvince, int IsService)
        {

            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;
            //ViewBag.IsCod = IsCod;          
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPI_CustomerRepository kPI_CustomerRepository = new KPI_CustomerRepository();
            ReturnKPI_Customer returnquality = new ReturnKPI_Customer();
            returnquality = kPI_CustomerRepository.KPI_Customer_DETAIL(StartProvince, EndProvince, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Customer);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public List<KPI_Customer_Excel> Excel_KPI_Customer(int StartProvince, int EndProvince, string StartDate, string EndDate, int IsService, string Customer)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            KPI_CustomerRepository qualityDetailHubRepository = new KPI_CustomerRepository();
            ReturnKPI_Customer returnquality = new ReturnKPI_Customer();
            returnquality = qualityDetailHubRepository.KPI_Customer_EXCEL_Total(StartProvince, EndProvince, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Customer);
            return returnquality.ListKPI_EXCustomerReport;
        }
        [HttpGet]

        public ActionResult ExportKPI_Customer(int StartProvince, int EndProvince, int IsService, string StartDate, string EndDate, string Customer)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelKPI_Customer();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chất lượng chuyển phát theo dịch vụ.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        [HttpGet]


        public Stream CreateExcelKPI_Customer(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = Excel_KPI_Customer(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.StartDate, ViewBag.EndDate, ViewBag.IsService, ViewBag.Customer);
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
                BindingFormatForExcelKPI_Customer(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }

        }
        private void BindingFormatForExcelKPI_Customer(ExcelWorksheet worksheet, List<KPI_Customer_Excel> listItems)
        {
            var list = Excel_KPI_Customer(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.StartDate, ViewBag.EndDate, ViewBag.IsService, ViewBag.Customer);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT LƯỢNG CHUYỂN PHÁT THEO DỊC VỤ";
            worksheet.Cells["A1:R1"].Merge = true;

            worksheet.Cells[2, 18].Value = "MÃ BÁO CÁO:CL_KHL/CL_DV";
            worksheet.Cells["R2:R2"].Merge = true;

            worksheet.Cells[2, 9].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["I2:J2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã tỉnh nhận";
            worksheet.Cells[4, 3].Value = "Tên tỉnh nhận";
            worksheet.Cells[4, 4].Value = "Mã tỉnh trả";
            worksheet.Cells[4, 5].Value = "Tên tỉnh trả";
            worksheet.Cells[4, 6].Value = "Tổng sản lượng";
            worksheet.Cells[4, 7].Value = "Phát thành công";
            worksheet.Cells[4, 8].Value = " Tỷ lệ Phát thành công";
            worksheet.Cells[4, 9].Value = "Phát không thành công";
            worksheet.Cells[4, 10].Value = "Tỷ lệ Phát không thành công";
            worksheet.Cells[4, 11].Value = "Chuyển hoàn";
            worksheet.Cells[4, 12].Value = "Tỷ lệ chuyển hoàn";
            worksheet.Cells[4, 13].Value = "Chưa có trạng thái";
            worksheet.Cells[4, 14].Value = "Tỷ lệ chưa có trạng thái";
            worksheet.Cells[4, 15].Value = "Đạt chỉ tiêu";
            worksheet.Cells[4, 16].Value = "Tỷ lệ đạt chỉ tiêu";
            worksheet.Cells[4, 17].Value = "Không đạt chỉ tiêu";
            worksheet.Cells[4, 18].Value = "Tỷ lệ không đạt chỉ tiêu";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:R4"])
            using (var ranges = worksheet.Cells["A1:R1"])
            using (var Ngay = worksheet.Cells["I2:J2"])
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
        #endregion

        #region chi tiết phát không thành công
        [HttpPost]
        public JsonResult LIST_KPI_Customer_FAIL(int StartProvince, int EndProvince, string StartDate, string EndDate, int IsService, string Customer)
        {

            ViewBag.StartProvince = StartProvince;
            ViewBag.IsSDistrict = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;


            KPI_CustomerRepository qualityDetailHubRepository = new KPI_CustomerRepository();
            ReturnKPI_Customer returnquality = new ReturnKPI_Customer();
            returnquality = qualityDetailHubRepository.LIST_KPI_Customer_FAIL(StartProvince, EndProvince, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Customer);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public List<KPI_Customer_Detail> ListExcelKPI_CustomerFail(int StartProvince, int EndProvince, string StartDate, string EndDate, int IsService, string Customer)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.IsSDistrict = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            KPI_CustomerRepository qualityDetailHubRepository = new KPI_CustomerRepository();
            ReturnKPI_Customer returnquality = new ReturnKPI_Customer();
            returnquality = qualityDetailHubRepository.LIST_KPI_Customer_FAIL(StartProvince, EndProvince, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Customer);
            return returnquality.ListKPI_Customer_DetailReport;
        }
        public Stream CreateExcelFileKPI_Customer(Stream stream = null)
        {

            //var list = CreateTestItems();
            var list = ListExcelKPI_CustomerFail(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.StartDate, ViewBag.EndDate, ViewBag.IsService, ViewBag.Customer);
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
                BindingFormatForExcelKPI_Customer_Fail(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        private void BindingFormatForExcelKPI_Customer_Fail(ExcelWorksheet worksheet, List<KPI_Customer_Detail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã Bưu Gửi";
            worksheet.Cells[1, 3].Value = "Ngày chấp nhận";
            worksheet.Cells[1, 4].Value = "Tên chấp nhận";
            worksheet.Cells[1, 5].Value = "Tỉnh chấp nhận";        // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 6].Value = "Ngày PKTC";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 7].Value = "BC nhập TTP";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 8].Value = "Lý do PKTC";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 9].Value = "Lần";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
                                                         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        [HttpGet]
        public ActionResult ExportKPI_CustomerFail(int StartProvince, int EndProvince, string StartDate, string EndDate, int IsService, string Customer)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileKPI_Customer();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chất lượng chuyển phát theo dịch vụ PKTC.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion

        #region  Chi tiết không đạt chỉ tiêu
        [HttpPost]
        public JsonResult LIST_KPI_Customer_KDCT(int StartProvince, int EndProvince, string StartDate, string EndDate, int IsService, string Customer)
        {

            ViewBag.StartProvince = StartProvince;
            ViewBag.IsSDistrict = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;


            KPI_CustomerRepository qualityDetailHubRepository = new KPI_CustomerRepository();
            ReturnKPI_Customer returnquality = new ReturnKPI_Customer();
            returnquality = qualityDetailHubRepository.LIST_KPI_Customer_KDCT(StartProvince, EndProvince, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Customer);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public List<KPI_Customer_DetailKDCT> LIST_KPI_CustomerKDCT(int StartProvince, int EndProvince, string StartDate, string EndDate, int IsService, string Customer)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.IsSDistrict = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            KPI_CustomerRepository qualityDetailHubRepository = new KPI_CustomerRepository();
            ReturnKPI_Customer returnquality = new ReturnKPI_Customer();
            returnquality = qualityDetailHubRepository.LIST_KPI_Customer_KDCT(StartProvince, EndProvince, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Customer);
            return returnquality.ListKPI_Customer_DetailKDCT;
        }
        public Stream CreateExcelFileKPI_Customer_KDCT(Stream stream = null)
        {

            //var list = CreateTestItems();
            var list = LIST_KPI_CustomerKDCT(ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.StartDate, ViewBag.EndDate, ViewBag.IsService, ViewBag.Customer);
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
                BindingFormatForExcelKPI_CustomerKDCT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        private void BindingFormatForExcelKPI_CustomerKDCT(ExcelWorksheet worksheet, List<KPI_Customer_DetailKDCT> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã Bưu Gửi";
            worksheet.Cells[1, 3].Value = "Ngày chấp nhận";
            worksheet.Cells[1, 4].Value = "BC chấp nhận";
            worksheet.Cells[1, 5].Value = "Tỉnh chấp nhận";        // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 6].Value = "Ngày phát";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 7].Value = "BC nhập TTP";
            worksheet.Cells[1, 8].Value = "Trạng thái";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 9].Value = "Chỉ tiêu thực tế";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 10].Value = "Chỉ tiêu";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
                                                               // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        [HttpGet]
        public ActionResult ExportKPI_CustomerKDCT(int StartProvince, int EndProvince, string StartDate, string EndDate, int IsService, string Customer)
        {
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.IsService = IsService;
            ViewBag.Customer = Customer;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileKPI_Customer_KDCT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chất lượng chuyển phát theo dịch vụ KDCT.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
    }


    #endregion
}