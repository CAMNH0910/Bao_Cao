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

    public class QualityDeliveryController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery

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

        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult QualityDeliveryDetailReport()
        {

            return View();

        }



        [HttpPost]
        //Controller gọi đến chi tiết theo từng mã bưu cục của sản lượng phát thành công trong 6H
        public ActionResult QualityDeliveryDetailReport_Success6H(int zone,int endpostcode, int routecode, string startdate, string enddate, int service, int type)
        {
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            ReturnQuality returnquality = new ReturnQuality();
            returnquality = qualitydeliveryRepository.Quality_Delivery_Success6H_Detail(zone,endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service, type);
            return Json(returnquality, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        //Controller gọi đến chi tiết theo từng mã bưu cục của sản lượng phát thành công không có thông tin 
        public ActionResult QualityDeliveryDetailReport_NoInformation(int zone ,int endpostcode, int routecode, string startdate, string enddate, int service, int type)
        {
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            ReturnQuality returnquality = new ReturnQuality();
            returnquality = qualitydeliveryRepository.Quality_Delivery_NoInformation_Detail(zone,endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service, type);
            return Json(returnquality, JsonRequestBehavior.AllowGet);
        }

        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        public ActionResult ListDetailedQualityDeliveryReport(int zone, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            ReturnQuality returnquality = new ReturnQuality();
            returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL(zone, endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            //return View(returnquality.ListQualityDeliveryReport);
            return View(returnquality);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<QualityDeliveryDetail> ReturnListExcel(int zone, int endpostcode, int routecode, string startdate, string enddate, int service)
        {

            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.service = service;
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            ReturnQuality returnquality = new ReturnQuality();
            returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL(zone, endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            return returnquality.ListQualityDeliveryReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.zone, ViewBag.endpostcode, ViewBag.routecode, ViewBag.startdate, ViewBag.enddate, ViewBag.service);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<QualityDeliveryDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.zone, ViewBag.endpostcode, ViewBag.routecode, ViewBag.startdate, ViewBag.enddate, ViewBag.service);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT LƯỢNG PHÁT";
            worksheet.Cells["A1:S1"].Merge = true;

            worksheet.Cells[2, 19].Value = "MÃ BÁO CÁO:P/CLP";
            worksheet.Cells["S2:S2"].Merge = true;

            worksheet.Cells[2, 9].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.endDate;
            worksheet.Cells["I2:K2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Đơn Vị";
            worksheet.Cells[4, 3].Value = "Bưu Cục";
            worksheet.Cells[4, 4].Value = "Tên Bưu Cục";
            worksheet.Cells[4, 5].Value = "SL Bưu Gửi Đến";
            worksheet.Cells[4, 6].Value = "SL Phát Thành Công";
            worksheet.Cells[4, 7].Value = "TL Phát Thành Công";
            worksheet.Cells[4, 8].Value = "SL Phát Thành Công 72H";
            worksheet.Cells[4, 9].Value = "TL Phát Thành Công 72H";
            worksheet.Cells[4, 10].Value = "SL Phát Thành Công 48H";
            worksheet.Cells[4, 11].Value = "TL Phát Thành Công 48H";
            worksheet.Cells[4, 12].Value = "SL Phát Thành Công 24H";
            worksheet.Cells[4, 13].Value = "TL Phát Thành Công 24H";
            worksheet.Cells[4, 14].Value = "SL Phát Chưa Có Thông Tin";
            worksheet.Cells[4, 15].Value = "SL PTC Đúng Quy Định";
            worksheet.Cells[4, 16].Value = "SL PTC Không Đúng Quy Định";
            worksheet.Cells[4, 17].Value = "Tỉ Lệ TC Đạt Đúng Quy Định";
            worksheet.Cells[4, 18].Value = "Tỉ Lệ TC Không Đúng Quy Định";
            worksheet.Cells[4, 19].Value = "SL PTC Không Xác Định";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:Q4"])
            using (var ranges = worksheet.Cells["A1:Q1"])
            using (var Ngay = worksheet.Cells["I2:K2"])
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
        public ActionResult Export(int zone, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng đi phát.xlsx");
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