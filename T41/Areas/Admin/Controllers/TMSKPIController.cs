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

    public class TMSKPIController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery
        
        //Controller lấy dữ liệu bưu cục phát
       
        //Controller lấy dữ liệu tuyến phát
      

        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult TMSKPI()
        {
            return View();

        }


        //public ActionResult Delete(int id)
        //{
        //    KPIKTRepository KPIKTRepository = new KPIKTRepository();
        //    try
        //    {
                
        //        if (KPIKTRepository.UpdateNotKPI(id))
        //        {
        //            ViewBag.AlertMsg = "Student Deleted Successfully";
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }

        //}

        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        [HttpPost]
        public JsonResult ListTMSKPIReport( string buucuc, string Transporttype)
        {           
            ViewBag.buucuc = buucuc;
            ViewBag.Transporttype = Transporttype;

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnKPIKT = new ReturnKPIKT();
            returnKPIKT = KPIKTRepository.TMSKPI(buucuc,Transporttype);
            //return View(returntrackingorder);
            return Json(returnKPIKT, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateMailRouteTMSKPIReport(int IdMailRoute, int WorkCenter, string Cellvalue, int TransportypeInt, string NameCutOff, string Totime,int NotKpi)
        {

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            return Json(KPIKTRepository.UpdateMailRoute(IdMailRoute, WorkCenter, Cellvalue, TransportypeInt, NameCutOff, Totime, NotKpi), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertMailRouteTMSKPIReport(int IdMailRoute, int WorkCenter, int Cellvalue, int TransportypeInt,int NotKpi, string NameCutOff, string Totime)
        {

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            return Json(KPIKTRepository.InsertMailRoute(IdMailRoute, WorkCenter, Cellvalue, TransportypeInt, NotKpi, NameCutOff, Totime), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FindIdMailRouteTMSKPIReport(string ID6Number)
        {

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            ReturnIdMailRoute returnIdMailRoute = new ReturnIdMailRoute();
            returnIdMailRoute = KPIKTRepository.FindIdMailRoute(ID6Number);
            //return View(returntrackingorder);
            return Json(returnIdMailRoute, JsonRequestBehavior.AllowGet);
        }

        


        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<TMSKPIDetail> ReturnListExcel(string buucuc, string Transporttype)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.Transporttype = Transporttype;

            KPIKTRepository KPIKTRepository = new KPIKTRepository();
            ReturnKPIKT returnKPIKT = new ReturnKPIKT();
            returnKPIKT = KPIKTRepository.TMSKPI(buucuc,Transporttype);
            return returnKPIKT.ListTMSKPIReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.buucuc, ViewBag.Transporttype);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<TMSKPIDetail> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "CutOff";
            worksheet.Cells[1, 3].Value = "ID hành trình";
            worksheet.Cells[1, 4].Value = "Tên hành trình";
            worksheet.Cells[1, 5].Value = "Thời gian";
            worksheet.Cells[1, 6].Value = "Bưu cục đóng";
            worksheet.Cells[1, 7].Value = "Bưu cục nhận";
            worksheet.Cells[1, 8].Value = "Tên bưu cục nhận";
            worksheet.Cells[1, 9].Value = "Dịch vụ";
            worksheet.Cells[1, 10].Value = "Phương tiện";
            worksheet.Cells[1, 11].Value = "Đo kiểm";

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
        public ActionResult Export(string buucuc,string Transporttype)
        {

            ViewBag.buucuc = buucuc;
            ViewBag.Transporttype = Transporttype;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Lược Đồ khai thác bưu cục "+ buucuc + ".xlsx");
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