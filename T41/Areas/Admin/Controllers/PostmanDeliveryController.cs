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

    public class PostmanDeliveryController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/KpiBD13Delivery
        
        //Controller lấy dữ liệu bưu cục phát
        public JsonResult PosCode(int zone)
        {
            KpiBD13DeliveryRepository PostManDeliveryRepository = new KpiBD13DeliveryRepository();
            return Json(PostManDeliveryRepository.GetAllPOSCODE(zone), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        //Controller lấy dữ liệu tuyến phát
        public JsonResult RouteCode(int endpostcode)
        {
            PostmanDeliveryRepository PostmanDeliveryRepository = new PostmanDeliveryRepository();
            return Json(PostmanDeliveryRepository.GetAllROUTECODE(endpostcode), JsonRequestBehavior.AllowGet);
            //return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Postman(int routecode)
        {
            PostmanDeliveryRepository PostmanDeliveryRepository = new PostmanDeliveryRepository();
            return Json(PostmanDeliveryRepository.GetAllPOSTMAN(routecode), JsonRequestBehavior.AllowGet);
            //return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult KpipostmanDeliveryDetailReport()
        {
            return View();

        }        
        [HttpPost]
        //Controller gọi đến chi tiết theo từng mã bưu cục của sản lượng phát thành công trong 6H
        public ActionResult PostmanDeliveryDetailReport_Success5H(int startpostcode, int routecode,int PostmanID, string service, string startdate, string enddate)
        {
            PostmanDeliveryRepository PostmanDeliveryRepository = new PostmanDeliveryRepository();
            ReturnPostman returnPostman = new ReturnPostman();
            returnPostman = PostmanDeliveryRepository.Postman_Delivery_Success5H_Detail(startpostcode, routecode, PostmanID, service,common.DateToInt(startdate), common.DateToInt(enddate));
            return Json(returnPostman, JsonRequestBehavior.AllowGet);            
        }

        public ActionResult PostmanDeliveryDetailNotPostmanID(int RouteCode, int PostmanID, string servicetypenumber, string startdate, string enddate)
        {
            PostmanDeliveryRepository PostmanDeliveryRepository = new PostmanDeliveryRepository();
            ReturnPostman returnPostman = new ReturnPostman();
            returnPostman = PostmanDeliveryRepository.NotPostmanIdKPIDetail(RouteCode, PostmanID, servicetypenumber, common.DateToInt(startdate), common.DateToInt(enddate));
            return Json(returnPostman, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PostmanDeliveryDetailNotPOD(int RouteCode, int PostmanID, string servicetypenumber, string startdate, string enddate)
        {
            PostmanDeliveryRepository PostmanDeliveryRepository = new PostmanDeliveryRepository();
            ReturnPostman returnPostman = new ReturnPostman();
            returnPostman = PostmanDeliveryRepository.NotPODKPIDetail(RouteCode, PostmanID, servicetypenumber, common.DateToInt(startdate), common.DateToInt(enddate));
            return Json(returnPostman, JsonRequestBehavior.AllowGet);
        }


        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        public ActionResult ListDetailedpostmanDeliveryReport(int Postman,int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            ViewBag.postman = Postman;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            PostmanDeliveryRepository PostmanDeliveryRepository = new PostmanDeliveryRepository();
            ReturnPostman returnPostman = new ReturnPostman();
            returnPostman = PostmanDeliveryRepository.Postman_DELIVERY_DETAIL(Postman, endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            //return View(returnKpiBD13.ListKpiBD13DeliveryReport);
            return View(returnPostman);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<PostmanDeliveryDetail> ReturnListExcel(int postman, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            ViewBag.postman = postman;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.service = service;
            PostmanDeliveryRepository PostmanDeliveryRepository = new PostmanDeliveryRepository();
            ReturnPostman returnPostman = new ReturnPostman();
            returnPostman = PostmanDeliveryRepository.Postman_DELIVERY_DETAIL(postman,endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            return returnPostman.ListPostmanDeliveryReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.postman , ViewBag.endpostcode,ViewBag.routecode,ViewBag.startdate,ViewBag.enddate,ViewBag.service);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<PostmanDeliveryDetail> listItems)
        {

            var list = ReturnListExcel(ViewBag.postman, ViewBag.endpostcode, ViewBag.routecode, ViewBag.startdate, ViewBag.enddate, ViewBag.service);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO KPI CHẤT LƯỢNG BƯU TÁ";
            worksheet.Cells["A1:Q1"].Merge = true;

            worksheet.Cells[2, 17].Value = "MÃ BÁO CÁO:BT/CLBT";
            worksheet.Cells["Q2:Q2"].Merge = true;

            worksheet.Cells[2, 8].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["H2:J2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";            
            worksheet.Cells[4, 2].Value = "Bưu Cục";
            worksheet.Cells[4, 3].Value = "Tên Bưu Cục";
            worksheet.Cells[4, 4].Value = "Tuyến Phát";
            worksheet.Cells[4, 5].Value = "Tên Tuyến phát";
            worksheet.Cells[4, 6].Value = "Mã Bưu tá";
            worksheet.Cells[4, 7].Value = "Tên Bưu tá";
            worksheet.Cells[4, 8].Value = "Dịch vụ";
            worksheet.Cells[4, 9].Value = "SL Bưu Gửi Đến";
            worksheet.Cells[4, 10].Value = "SL Phát Thành Công";          
            worksheet.Cells[4, 11].Value = "SL PTC Đúng Quy Định";
            worksheet.Cells[4, 12].Value = "SL >5KG PTC Đúng Quy Định";
            worksheet.Cells[4, 13].Value = "Tỉ Lệ TC Đạt Đúng Quy Định";
            worksheet.Cells[4, 14].Value = "SL PTC Không Đúng Quy Định";
            worksheet.Cells[4, 15].Value = "SL >5G PTC Không Đúng Quy Định";
            worksheet.Cells[4, 16].Value = "Tỉ Lệ TC Không Đúng Quy Định";
            worksheet.Cells[4, 17].Value = "SL PTC chưa có thông tin";


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:Q4"])
            using (var ranges = worksheet.Cells["A1:Q1"])
            using (var Ngay = worksheet.Cells["H2:J2"])
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
        public ActionResult Export(int postman, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            ViewBag.postman = postman;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo tổng hợp chất lượng KPI bưu tá.xlsx");
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