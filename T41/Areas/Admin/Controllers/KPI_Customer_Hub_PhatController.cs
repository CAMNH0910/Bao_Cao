﻿using System;
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
using System.Web.Services.Description;

namespace T41.Areas.Admin.Controllers
{
    public class KPI_Customer_Hub_PhatController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult KPI_Customer_Hub_Phat_Report()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListKPI_Customer_Hub_Phat(int EndPostCode, string StartDate, string EndDate, int CaKT, int IsService, int Routecode, int zone, int Status_PP)
        {

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.EndProvince = EndPostCode;
            ViewBag.CaKT = CaKT;
            ViewBag.IsService = IsService;
            ViewBag.Routecode = Routecode;
            ViewBag.Status_PP = Status_PP;

            KPI_Customer_Hub_Phat_Repository kPI_CustomerRepository = new KPI_Customer_Hub_Phat_Repository();
            ReturnKPI_Customer_Hub_Phat returnquality = new ReturnKPI_Customer_Hub_Phat();
            returnquality = kPI_CustomerRepository.KPI_Customer_Hub_Phat(common.DateToInt(StartDate), common.DateToInt(EndDate), EndPostCode, CaKT, IsService, Routecode, zone, Status_PP);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public List<EXKPI_Customer_Hub_Phat> EXKPI_Customer_Hub_Phat(string StartDate, string EndDate, int EndPostCode, int CaKT, int IsService, int Routecode, int zone, int Status_PP)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.EndProvince = EndPostCode;
            ViewBag.CaKT = CaKT;
            ViewBag.IsService = IsService;
            ViewBag.Routecode = Routecode;
            ViewBag.Zone = zone;
            ViewBag.Status_PP = Status_PP;

            KPI_Customer_Hub_Phat_Repository qualityDetailHubRepository = new KPI_Customer_Hub_Phat_Repository();
            ReturnKPI_Customer_Hub_Phat returnquality = new ReturnKPI_Customer_Hub_Phat();
            returnquality = qualityDetailHubRepository.EXKPI_Customer_Hub_Phat(common.DateToInt(StartDate), common.DateToInt(EndDate), EndPostCode, CaKT, IsService, Routecode, zone, Status_PP);
            return returnquality.ListKPI_EXCustomerReport;
        }
        [HttpGet]

        public ActionResult ExportKPI_Customer(string StartDate, string EndDate, int EndPostCode, int CaKT, int IsService, int Routecode, int zone, int Status_PP)
        {
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.EndProvince = EndPostCode;
            ViewBag.CaKT = CaKT;
            ViewBag.IsService = IsService;
            ViewBag.Routecode = Routecode;
            ViewBag.Zone = zone;
            ViewBag.Status_PP = Status_PP;


            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelKPI_Customer();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo kiểm soát bưu gửi Cod.xlsx");
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
            var list = EXKPI_Customer_Hub_Phat(
            ViewBag.StartDate,
            ViewBag.EndDate,
            ViewBag.EndProvince,
            ViewBag.CaKT,
            ViewBag.IsService,
            ViewBag.Routecode,
            ViewBag.Zone,
             ViewBag.Status_PP
            );
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
        private void BindingFormatForExcelKPI_Customer(ExcelWorksheet worksheet, List<EXKPI_Customer_Hub_Phat> listItems)
        {
            var list = EXKPI_Customer_Hub_Phat(
            ViewBag.StartDate,
            ViewBag.EndDate,
            ViewBag.EndProvince,
            ViewBag.CaKT,
            ViewBag.IsService,
            ViewBag.Routecode,
            ViewBag.Zone,
             ViewBag.Status_PP
            );
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO KIỂM SOÁT NỘP TIỀN COD";
            worksheet.Cells["A1:M1"].Merge = true;

            worksheet.Cells[2, 13].Value = "MÃ BÁO CÁO:P/KS_COD";
            worksheet.Cells["M2:M2"].Merge = true;

            worksheet.Cells[2, 6].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["F2:H2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Mã bưu Gửi";
            worksheet.Cells[4, 3].Value = "Mã khách hàng";
            worksheet.Cells[4, 4].Value = "Ngày chấp nhận";
            worksheet.Cells[4, 5].Value = "Bưu cục chấp nhận";
            worksheet.Cells[4, 6].Value = "Cod trên PNS";        // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 7].Value = "Cod trên Paypost";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 8].Value = "Trạng thái hiện tại BCCP";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 9].Value = "Ngày cập nhật trạng thái BCCP";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 10].Value = "Trạng thái Paypost";
            worksheet.Cells[4, 11].Value = "Ngày cập nhật trạng thái Paypost";
            worksheet.Cells[4, 12].Value = "Bưu cục phát";
            worksheet.Cells[4, 13].Value = "Tuyến phát";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:M4"])
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
            }


        }

    }
}