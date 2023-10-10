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

    public class KpiBD13DeliveryController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/KpiBD13Delivery

        //Controller lấy dữ liệu bưu cục phát
        public JsonResult PosCode(int zone)
        {
            KpiBD13DeliveryRepository KpiBD13DeliveryRepository = new KpiBD13DeliveryRepository();
            return Json(KpiBD13DeliveryRepository.GetAllPOSCODE(zone), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }


        //Controller lấy dữ liệu tuyến phát
        public JsonResult RouteCode(int endpostcode)
        {
            KpiBD13DeliveryRepository KpiBD13DeliveryRepository = new KpiBD13DeliveryRepository();
            return Json(KpiBD13DeliveryRepository.GetAllROUTECODE(endpostcode), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult KpiBD13DeliveryDetailReport()
        {
            return View();

        }



        [HttpPost]
        //Controller gọi đến chi tiết theo từng mã bưu cục của sản lượng phát thành công trong 6H
        public ActionResult KpiBD13DeliveryDetailReport_Success6H(int endpostcode, int routecode, string startdate, string enddate, int service, int type)
        {
            KpiBD13DeliveryRepository KpiBD13DeliveryRepository = new KpiBD13DeliveryRepository();
            ReturnKpiBD13 returnKpiBD13 = new ReturnKpiBD13();
            returnKpiBD13 = KpiBD13DeliveryRepository.KpiBD13_Delivery_Success6H_Detail(endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service, type);
            return Json(returnKpiBD13, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        //Controller gọi đến chi tiết theo từng mã bưu cục của sản lượng phát thành công không có thông tin 
        public ActionResult KpiBD13DeliveryDetailReport_NoInformation(int endpostcode, int routecode, string startdate, string enddate, int service, int type)
        {
            KpiBD13DeliveryRepository KpiBD13DeliveryRepository = new KpiBD13DeliveryRepository();
            ReturnKpiBD13 returnKpiBD13 = new ReturnKpiBD13();
            returnKpiBD13 = KpiBD13DeliveryRepository.KpiBD13_Delivery_NoInformation_Detail(endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service, type);
            return Json(returnKpiBD13, JsonRequestBehavior.AllowGet);
        }

        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        public ActionResult ListDetailedKpiBD13DeliveryReport(int zone, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            //ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            KpiBD13DeliveryRepository KpiBD13DeliveryRepository = new KpiBD13DeliveryRepository();
            ReturnKpiBD13 returnKpiBD13 = new ReturnKpiBD13();
            returnKpiBD13 = KpiBD13DeliveryRepository.KpiBD13_DELIVERY_DETAIL(zone, endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            //return View(returnKpiBD13.ListKpiBD13DeliveryReport);
            return View(returnKpiBD13);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<KpiBD13DeliveryDetail> ReturnListExcel(int zone, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.service = service;
            KpiBD13DeliveryRepository KpiBD13DeliveryRepository = new KpiBD13DeliveryRepository();
            ReturnKpiBD13 returnKpiBD13 = new ReturnKpiBD13();
            returnKpiBD13 = KpiBD13DeliveryRepository.KpiBD13_DELIVERY_DETAIL(zone, endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            return returnKpiBD13.ListKpiBD13DeliveryReport;
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<KpiBD13DeliveryDetail> listItems)
        {
            var list = ReturnListExcel(ViewBag.zone, ViewBag.endpostcode, ViewBag.routecode, ViewBag.startdate, ViewBag.enddate, ViewBag.service);


            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHI TIẾT DỊCH VỤ CỘNG THÊM";
            worksheet.Cells["A1:O1"].Merge = true;

            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO:KD/CT_DVCT_CT";
            worksheet.Cells["O2:O2"].Merge = true;

            worksheet.Cells[2, 7].Value = "Từ ngày:" + " " + ViewBag.startdate + " " + "Đến ngày" + ViewBag.enddate;
            worksheet.Cells["G2:I2"].Merge = true;

            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Đơn Vị";
            worksheet.Cells[1, 3].Value = "Bưu Cục";
            worksheet.Cells[1, 4].Value = "Tên Bưu Cục";
            worksheet.Cells[1, 5].Value = "SL Bưu Gửi Đến";
            worksheet.Cells[1, 6].Value = "SL Phát Thành Công";
            worksheet.Cells[1, 7].Value = "TL Phát Thành Công";
            worksheet.Cells[1, 8].Value = "SL Phát Thành Công 72H";
            worksheet.Cells[1, 9].Value = "TL Phát Thành Công 72H";
            worksheet.Cells[1, 10].Value = "SL Phát Chưa Có Thông Tin";
            worksheet.Cells[1, 11].Value = "SL PTC Đúng Quy Định";
            worksheet.Cells[1, 12].Value = "SL PTC Không Đúng Quy Định";
            worksheet.Cells[1, 13].Value = "Tỉ Lệ TC Đạt Đúng Quy Định";
            worksheet.Cells[1, 14].Value = "Tỉ Lệ TC Không Đúng Quy Định";
            worksheet.Cells[1, 15].Value = "SL PTC Không Xác Định";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:O4"])
            using (var ranges = worksheet.Cells["A1:O1"])
            using (var Ngay = worksheet.Cells["G2:I2"])
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
            //ViewBag.todate = todate;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp sản lượng KPI BD13.xlsx");
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