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

    public class QualityKHDeliveryController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery
        
        //Controller lấy dữ liệu bưu cục phát
        public JsonResult Provincenhan()
        {
            QualityKHDeliveryRepository qualitydeliveryRepository = new QualityKHDeliveryRepository();
            return Json(qualitydeliveryRepository.GetallProvinceNhan(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Provincetra()
        {
            QualityKHDeliveryRepository qualitydeliveryRepository = new QualityKHDeliveryRepository();
            return Json(qualitydeliveryRepository.GetallProvinceTra(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult QualityDeliveryKHDetailReport()
        {
            return View();

        }
        
        [HttpPost]
        //Controller gọi đến chi tiết theo từng mã buu gui chua co thông tin
        public ActionResult QualityDeliveryDetailReport_Success(int type,int StartProvince, int EndProvince  , int Isservice, string startdate, string enddate, string CustomerCode)
        {
            QualityKHDeliveryRepository qualitydeliveryRepository = new QualityKHDeliveryRepository();
            ReturnQualityKH returnquality = new ReturnQualityKH();
            if (type == 0)
            {
                returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL_PTC(StartProvince, EndProvince, Isservice, common.DateToInt(startdate), common.DateToInt(enddate), CustomerCode);
            }
            else
            {
                returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL_PLD(StartProvince, EndProvince, Isservice, common.DateToInt(startdate), common.DateToInt(enddate), CustomerCode);
            }
            return Json(returnquality, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        //Controller gọi đến chi tiết theo từng mã bưu cục của sản lượng phát thành công trong 6H
        public ActionResult DeliveryDetailReport_NotSuccess(int type,int StartProvince, int EndProvince, int Isservice, string startdate, string enddate, string CustomerCode)
        {

            QualityKHDeliveryRepository qualitydeliveryRepository = new QualityKHDeliveryRepository();
            ReturnQualityKH returnquality = new ReturnQualityKH();
            if (type == 0)
            {
                returnquality = qualitydeliveryRepository.Quality_Delivery_Item_Detail_PTC(StartProvince, EndProvince, Isservice, common.DateToInt(startdate), common.DateToInt(enddate), CustomerCode);
            }
            else
            {
                returnquality = qualitydeliveryRepository.Quality_Delivery_Item_Detail_PLD(StartProvince, EndProvince, Isservice, common.DateToInt(startdate), common.DateToInt(enddate), CustomerCode);
            }
            return Json(returnquality, JsonRequestBehavior.AllowGet);
        }



        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        public ActionResult ListDetailedQualityDeliveryKHReport(int type,int StartProvince, int EndProvince, int Isservice, string startdate, string enddate, string CustomerCode)
        {
            //ViewBag.zone = zone;

            ViewBag.type = type;
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.Isservice = Isservice;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.CustomerCode = CustomerCode;
            QualityKHDeliveryRepository qualitydeliveryRepository = new QualityKHDeliveryRepository();
            ReturnQualityKH returnquality = new ReturnQualityKH();
            if (type == 0)
            {
                returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL_PTC(StartProvince, EndProvince, Isservice, common.DateToInt(startdate), common.DateToInt(enddate), CustomerCode);
            }else
            {
                returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL_PLD(StartProvince, EndProvince, Isservice, common.DateToInt(startdate), common.DateToInt(enddate), CustomerCode);

            }
            //return View(returnquality.ListQualityDeliveryReport);
            return View(returnquality);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<QualityDeliveryDetailKH> ReturnListExcel(int type,int StartProvince, int EndProvince, int Isservice, string startdate, string enddate, string CustomerCode)
        {

            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince;
            ViewBag.Isservice = Isservice;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.CustomerCode = CustomerCode;
            QualityKHDeliveryRepository qualitydeliveryRepository = new QualityKHDeliveryRepository();
            ReturnQualityKH returnquality = new ReturnQualityKH();
            if (type == 0)
            {
                returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL_PTC(StartProvince, EndProvince, Isservice, common.DateToInt(startdate), common.DateToInt(enddate), CustomerCode);
            }else
            {
                returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL_PLD(StartProvince, EndProvince, Isservice, common.DateToInt(startdate), common.DateToInt(enddate), CustomerCode);

            }
            return returnquality.ListQualityDeliveryKHReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.type,ViewBag.StartProvince, ViewBag.EndProvince, ViewBag.Isservice, ViewBag.startdate, ViewBag.enddate, ViewBag.CustomerCode);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<QualityDeliveryDetailKH> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã tỉnh nhận";
            worksheet.Cells[1, 3].Value = "Mã tỉnh trả";
            worksheet.Cells[1, 4].Value = "Tên tỉnh nhận";           
            worksheet.Cells[1, 5].Value = "Tên tỉnh trả";
            worksheet.Cells[1, 6].Value = "Tổng SL";           
            worksheet.Cells[1, 7].Value = "SL J1";
            worksheet.Cells[1, 8].Value = "TL J1";
            worksheet.Cells[1, 9].Value = "SL J2";
            worksheet.Cells[1, 10].Value = "TL J2";
            worksheet.Cells[1, 11].Value = "SL J25";
            worksheet.Cells[1, 12].Value = "TL J25";
            worksheet.Cells[1, 13].Value = "SL J3";
            worksheet.Cells[1, 14].Value = "TL J3";
            worksheet.Cells[1, 15].Value = "SL J35";
            worksheet.Cells[1, 16].Value = "TL J35";
            worksheet.Cells[1, 17].Value = "SL J4";
            worksheet.Cells[1, 18].Value = "TL J4";
            worksheet.Cells[1, 19].Value = ">SL J4";
            worksheet.Cells[1, 20].Value = ">TL J4";
            worksheet.Cells[1, 21].Value = "SL KTT";
            worksheet.Cells[1, 22].Value = "TL KTT";



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
        public ActionResult Export(int type,int StartProvince, int EndProvince, int Isservice, string startdate, string enddate, string CustomerCode)
        {
            ViewBag.type = type;
            ViewBag.StartProvince = StartProvince;
            ViewBag.EndProvince = EndProvince ;
            ViewBag.Isservice = Isservice;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.CustomerCode = CustomerCode;

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