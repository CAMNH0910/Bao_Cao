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

namespace T41.Areas.Admin.Controllers
{

    public class ReportCTDVCTController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery

        //Controller lấy dữ liệu bưu cục phát

        public JsonResult TINHNHAN()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetAllTINH(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TINHTRA()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetAllTINH(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DICHVU()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetAllDV(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DICHVUCT()
        {
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            return Json(qualityTHDVRepository.GetAllDVCT(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu tuyến phát


        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult CTDVCT()
        {
            return View();

        }





        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        [HttpPost]
        public JsonResult ListDetailedQualityCTDVCTReport(int donvi,int tinhnhan,int tinhtra, string service,string servicect ,string startdate, string enddate)
        {           
            ViewBag.donvi = donvi;
            ViewBag.tinhnhan = tinhnhan;
            ViewBag.tinhtra = tinhtra;
            ViewBag.service = service;
            ViewBag.servicect = servicect;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportCTDVCT returnquality = new ReturnReportCTDVCT();
            returnquality = qualityTHDVRepository.QUALITY_CTDVCT_DETAIL(donvi,tinhnhan,tinhtra,  service,servicect, common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
            
        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<QualityReportCTDVCTDetail> ReturnListExcel(int donvi, int tinhnhan, int tinhtra, string service, string servicect, string startdate, string enddate)
        {

            ViewBag.donvi = donvi;
            ViewBag.tinhnhan = tinhnhan;
            ViewBag.tinhtra = tinhtra;
            ViewBag.service = service;
            ViewBag.servicect = servicect;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportCTDVCT returnquality = new ReturnReportCTDVCT();
            returnquality = qualityTHDVRepository.QUALITY_CTDVCT_DETAIL(donvi, tinhnhan, tinhtra, service, servicect, common.DateToInt(startdate), common.DateToInt(enddate));
            //return View(returnquality.ListQualityDeliveryReport);
            return returnquality.ListDetailedQualityCTDVCTReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.donvi, ViewBag.tinhnhan, ViewBag.tinhtra, ViewBag.service, ViewBag.servicect, ViewBag.startdate,ViewBag.enddate);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<QualityReportCTDVCTDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Đơn vị (BĐT/EMS)";
            worksheet.Cells[1, 3].Value = "Mã tỉnh nhận";
            worksheet.Cells[1, 4].Value = "Tên tỉnh nhận";
            worksheet.Cells[1, 5].Value = "Mã tỉnh trả";
            worksheet.Cells[1, 6].Value = "Tên tỉnh trả";
            worksheet.Cells[1, 7].Value = "Mã dịch vụ cộng thêm";
            worksheet.Cells[1, 8].Value = "Tên dịch vụ cộng thêm";
            worksheet.Cells[1, 9].Value = "Mã dịch vụ chính";
            worksheet.Cells[1, 10].Value = "Tên dịch vụ chính";
            worksheet.Cells[1, 11].Value = "SL";
            worksheet.Cells[1, 12].Value = "KL";
            worksheet.Cells[1, 13].Value = "Khối lượng quy đổi";
            worksheet.Cells[1, 14].Value = "Cước chính";
            worksheet.Cells[1, 15].Value = "PPXD";
            worksheet.Cells[1, 16].Value = "PPVX";
            worksheet.Cells[1, 17].Value = "PPMD";
            worksheet.Cells[1, 18].Value = "Cước DVCT";
            worksheet.Cells[1, 19].Value = "Tổng cước";
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
        public ActionResult Export(int donvi,int tinhnhan,int tinhtra, string service,string servicect, string startdate, string enddate)
        {

            ViewBag.donvi = donvi;
            ViewBag.tinhnhan = tinhnhan;
            ViewBag.tinhtra = tinhtra;
            ViewBag.service = service;
            ViewBag.servicect = servicect;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết theo dịch vụ cộng thêm.xlsx");
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