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

    public class ReportTHCHController : Controller
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
        //Controller lấy dữ liệu tuyến phát
      

        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult TH_CH()
        {
            return View();

        }

        



        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        [HttpPost]
        public JsonResult ListDetailedQualityTHCHReport(int donvi, int tinhnhan, int tinhtra, string service, string startdate, string enddate)
        {
            ViewBag.service = service;
            ViewBag.donvi = donvi;
            ViewBag.tinhtra = tinhtra;
            ViewBag.tinhnhan = tinhnhan;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportTHCH returnquality = new ReturnReportTHCH();
            returnquality = qualityTHDVRepository.QUALITY_THCH_DETAIL(donvi,tinhnhan, tinhtra, service, common.DateToInt(startdate), common.DateToInt(enddate));
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        //[HttpPost]
        //public JsonResult ListDetailedItemQualityTINHTINHReport(string donvi, int tinhnhan, int tinhtra,string Madvchinh, int nackl, string startdate, string enddate)
        //{
        //    int iDonvi = 0;
        //    int iPLDV = 0;
        //    ViewBag.donvi = donvi;
        //    ViewBag.Madvchinh = Madvchinh;
        //    ViewBag.tinhnhan = tinhnhan;
        //    ViewBag.tinhtra = tinhtra;
        //    ViewBag.nackl = nackl;
        //    ViewBag.startdate = startdate;

        //    ViewBag.enddate = enddate;

        //    if (donvi == "EMS")
        //    {
        //        iDonvi = 1;
        //    }
        //    else if (donvi == "BDT")
        //    {
        //        iDonvi = 2;
        //    }

          
        //    ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
        //    ReturnReportTHDV returnquality = new ReturnReportTHDV();
        //    returnquality = qualityTHDVRepository.QUALITY_THTINHTINH_DETAIL_ITEM(iDonvi, tinhnhan, tinhtra, Madvchinh, common.DateToInt(startdate), common.DateToInt(enddate),nackl);
        //    //return View(returnquality.ListQualityDeliveryReport);
        //    // return View(returnquality);
        //    // return Json(returnquality, JsonRequestBehavior.AllowGet);

        //    var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}


        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<QualityReportTHCHDetail> ReturnListExcel(int donvi, int tinhnhan, int tinhtra, string service, string startdate, string enddate)
        {
            ViewBag.service = service;
            ViewBag.donvi = donvi;
            ViewBag.tinhtra = tinhtra;
            ViewBag.tinhnhan = tinhnhan;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
            ReturnReportTHCH returnquality = new ReturnReportTHCH();
            returnquality = qualityTHDVRepository.QUALITY_THCH_DETAIL(donvi, tinhnhan, tinhtra, service, common.DateToInt(startdate), common.DateToInt(enddate));
            return returnquality.ListDetailedQualityTHCHReport;
        }
        //[HttpGet]
        ////public List<QualityReportKDDetailItem> ReturnListExcelCT(string donvi, int tinhnhan, int tinhtra, string Madvchinh, string startdate, string enddate,int nackl)
        ////{

        ////    int iDonvi = 0;
            


        ////    if (donvi == "EMS")
        ////    {
        ////        iDonvi = 1;
        ////    }
        ////    else if (donvi == "BDT")
        ////    {
        ////        iDonvi = 2;
        ////    }

        ////    ViewBag.Madvchinh = Madvchinh;
        ////    ViewBag.donvi = donvi;
        ////    ViewBag.tinhtra = tinhtra;
        ////    ViewBag.tinhnhan = tinhnhan;
        ////    ViewBag.startdate = startdate;
        ////    ViewBag.enddate = enddate;
        ////    ViewBag.nackl = nackl;
        ////    ReportlTHDVRepository qualityTHDVRepository = new ReportlTHDVRepository();
        ////    ReturnReportTHDV returnquality = new ReturnReportTHDV();
        ////    returnquality = qualityTHDVRepository.QUALITY_THTINHTINH_DETAIL_ITEM(iDonvi, tinhnhan, tinhtra, Madvchinh, common.DateToInt(startdate), common.DateToInt(enddate),nackl);
        ////    return returnquality.ListDetailedItemQualityTHDVReport;
        ////}

        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.donvi, ViewBag.tinhnhan, ViewBag.tinhtra, ViewBag.service,  ViewBag.startdate,ViewBag.enddate);
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

        //public Stream CreateExcelFileCT(Stream stream = null)
        //{
        //    //var list = CreateTestItems();
        //    var list = ReturnListExcelCT(ViewBag.donvi, ViewBag.tinhnhan, ViewBag.tinhtra, ViewBag.Madvchinh, ViewBag.startdate, ViewBag.enddate, ViewBag.nackl);
        //    using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
        //    {
        //        // Tạo author cho file Excel
        //        excelPackage.Workbook.Properties.Author = "Window.User";
        //        // Tạo title cho file Excel
        //        excelPackage.Workbook.Properties.Title = "Export Excel";
        //        // thêm tí comments vào làm màu 
        //        excelPackage.Workbook.Properties.Comments = "Export Excel Success !";
        //        // Add Sheet vào file Excel
        //        excelPackage.Workbook.Worksheets.Add("First Sheet");
        //        // Lấy Sheet bạn vừa mới tạo ra để thao tác 
        //        var workSheet = excelPackage.Workbook.Worksheets[1];
        //        // Đổ data vào Excel file
        //        workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);
        //        BindingFormatForExcelCT(workSheet, list);
        //        excelPackage.Save();
        //        return excelPackage.Stream;
        //    }
        //}
        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<QualityReportTHCHDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.donvi, ViewBag.tinhnhan, ViewBag.tinhtra, ViewBag.service, ViewBag.startdate, ViewBag.enddate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO TỔNG HỢP CHUYỂN HOÀN";
            worksheet.Cells["A1:R1"].Merge = true;

            worksheet.Cells[2, 18].Value = "MÃ BÁO CÁO:KD/THCH";
            worksheet.Cells["R2:R2"].Merge = true;

            worksheet.Cells[2, 9].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["I2:J2"].Merge = true;

            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Đơn Vị";
            worksheet.Cells[1, 3].Value = "Mã tỉnh nhận";
            worksheet.Cells[1, 4].Value = "Tên tỉnh nhận";
            worksheet.Cells[1, 5].Value = "Mã tỉnh trả";
            worksheet.Cells[1, 6].Value = "Tên tỉnh trả";           
            worksheet.Cells[1, 7].Value = "Mã Dịch vụ";
            worksheet.Cells[1, 8].Value = "Tên Dịch vụ";
            worksheet.Cells[1, 9].Value = "Nấc KL tính cước";
            worksheet.Cells[1, 10].Value = "SL";
            worksheet.Cells[1, 11].Value = "KL";
            worksheet.Cells[1, 12].Value = "KL QD";
            worksheet.Cells[1, 13].Value = "Cước chính";
            worksheet.Cells[1, 14].Value = "PPXD";
            worksheet.Cells[1, 15].Value = "PPVX";
            worksheet.Cells[1, 16].Value = "PPMD";
            worksheet.Cells[1, 17].Value = "Cước DVCT";
            worksheet.Cells[1, 18].Value = "Tổng cước";

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


        //private void BindingFormatForExcelCT(ExcelWorksheet worksheet, List<QualityReportKDDetailItem> listItems)
        //{
        //    // Set default width cho tất cả column
        //    worksheet.DefaultColWidth = 30;
        //    worksheet.DefaultRowHeight = 20;
        //    // Tự động xuống hàng khi text quá dài
        //    worksheet.Cells.Style.WrapText = true;
        //    // Tạo header
        //    worksheet.Cells[1, 1].Value = "STT";
        //    worksheet.Cells[1, 2].Value = "Đơn Vị";
        //    worksheet.Cells[1, 3].Value = "Mã tỉnh nhận";
        //    worksheet.Cells[1, 4].Value = "Tên tỉnh nhận";
        //    worksheet.Cells[1, 5].Value = "Ngày phát hành";
        //    worksheet.Cells[1, 6].Value = "Mã DV chính";
        //    worksheet.Cells[1, 7].Value = "Tên DV";
        //    worksheet.Cells[1, 8].Value = "Mã E1";
        //    worksheet.Cells[1, 9].Value = "Mã KH";
        //    worksheet.Cells[1, 10].Value = "Người gửi";
        //    worksheet.Cells[1, 11].Value = "Địa chỉ gửi";
        //    worksheet.Cells[1, 12].Value = "Người nhận";
        //    worksheet.Cells[1, 13].Value = "Địa chỉ nhận";
        //    worksheet.Cells[1, 14].Value = "Mã tỉnh trả";
        //    worksheet.Cells[1, 15].Value = "Tên tỉnh trả";
        //    worksheet.Cells[1, 16].Value = "Trạng thái";
        //    worksheet.Cells[1, 17].Value = "KL";
        //    worksheet.Cells[1, 18].Value = "KLQD";
        //    worksheet.Cells[1, 19].Value = "Cước chính";
        //    worksheet.Cells[1, 20].Value = "Cước DVCT";
        //    worksheet.Cells[1, 21].Value = "PPXD";
        //    worksheet.Cells[1, 22].Value = "PPVX";
        //    worksheet.Cells[1, 23].Value = "PP khác";
        //    worksheet.Cells[1, 24].Value = "Tổng cước";

        //    // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
        //    using (var range = worksheet.Cells["A1:Z1"])
        //    {
        //        // Set PatternType
        //        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        // Set Màu cho Background
        //        range.Style.Fill.BackgroundColor.SetColor(Color.Orange);
        //        // Canh giữa cho các text
        //        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        // Set Font cho text  trong Range hiện tại
        //        range.Style.Font.SetFromFont(new Font("Arial", 11));
        //        // Set Border
        //        //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
        //        // Set màu ch Border
        //        //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
        //    }


        //}
        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(int donvi,int tinhnhan, int tinhtra,string service, string startdate, string enddate)
        {
            ViewBag.service = service;
            ViewBag.donvi = donvi;
            ViewBag.tinhtra = tinhtra;
            ViewBag.tinhnhan = tinhnhan;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp CH.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult ExportCT(string donvi, int tinhnhan, int tinhtra, string Madvchinh, string startdate, string enddate,int nackl)
        //{

        //     int iDonvi = 0;
        //    if (donvi == "EMS")
        //    {
        //        iDonvi = 1;
        //    }
        //    else if (donvi == "BDT")
        //    {
        //        iDonvi = 2;
        //    }
        //    ViewBag.Madvchinh = Madvchinh;
        //    ViewBag.donvi = donvi;
        //    ViewBag.tinhtra = tinhtra;
        //    ViewBag.tinhnhan = tinhnhan;
        //    ViewBag.startdate = startdate;
        //    ViewBag.enddate = enddate;
        //    ViewBag.nackl = nackl;
        //    //ViewBag.receptacle_id = receptacle_id;
        //    // Gọi lại hàm để tạo file excel
        //    var stream = CreateExcelFileCT();
        //    // Tạo buffer memory strean để hứng file excel
        //    var buffer = stream as MemoryStream;
        //    // Đây là content Type dành cho file excel
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
        //    // File name của Excel này là ExcelDemo
        //    Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo chi tiết tỉnh_tỉnh EMS TN.xlsx");
        //    // Lưu file excel của chúng ta như 1 mảng byte để trả về response
        //    Response.BinaryWrite(buffer.ToArray());
        //    // Send tất cả ouput bytes về phía clients
        //    Response.Flush();
        //    Response.End();
        //    // Redirect về luôn trang index >
        //    return RedirectToAction("Index");
        //}
    }
}