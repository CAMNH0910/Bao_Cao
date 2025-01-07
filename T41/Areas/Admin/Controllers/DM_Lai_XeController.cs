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
using DocumentFormat.OpenXml.Drawing;
using System.Security.Policy;
using System.Text;
using System.Xml.Linq;
using Path = System.IO.Path;
using System.Globalization;
using DocumentFormat.OpenXml.Office2010.Excel;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Color = System.Drawing.Color;


namespace T41.Areas.Admin.Controllers
{
    public class DM_Lai_XeController : Controller
    {

        Convertion common = new Convertion();

        public ActionResult DM_Lai_Xe()
        {
            return View();
        }

        public JsonResult RouteCode(int endpostcode)
        {
            DM_Lai_XeRepository PostmanDeliveryRepository = new DM_Lai_XeRepository();
            return Json(PostmanDeliveryRepository.GetAllROUTECODE(endpostcode), JsonRequestBehavior.AllowGet);
            //return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        #region Danh mục lái xe
        [HttpPost]
        public JsonResult ListDM_Lai_Xe( int zone, int endpostcode,string trangthai)
        {
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.trangthai = trangthai;

            DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();
            ReturnDM_Lai_Xe returnquality = new ReturnDM_Lai_Xe();
            returnquality = lai_XeRepository.DM_Lai_Xe_DETAIL( zone, endpostcode, trangthai);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Khoa(int id)
        {
            // Gọi phương thức Khoa trong repository để thực hiện thao tác khóa
            DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();
            ReturnResponseUpdate result = lai_XeRepository.Khoa(id);

            // Trả về kết quả dưới dạng JSON
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetLaiXeInfo(int id)
        {
            DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();
            ReturnDM_Lai_Xe returnquality = lai_XeRepository.CapNhat_Id_Sua(id); // Lấy thông tin lái xe từ cơ sở dữ liệu
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;  // Đảm bảo có thể truyền lượng dữ liệu lớn
            return jsonResult;
        }
        [HttpPost]
        public JsonResult AddLaiXe(DM_Lai_Xe_Sua model)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (model == null)
                {
                    return Json(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });
                }

                // Tạo repository để thao tác dữ liệu
                DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();

                // Gọi phương thức thêm mới
                var result = lai_XeRepository.Add(model);

                if (result)
                {
                    return Json(new { success = true, message = "Thêm mới lái xe thành công.", data = model });
                }
                else
                {
                    return Json(new { success = false, message = "Thêm mới lái xe thất bại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết và trả về lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateLaiXe(DM_Lai_Xe_Sua model)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (model == null)
                {
                    return Json(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });
                }

                // Tạo repository để thao tác dữ liệu
                DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();

                // Gọi phương thức thêm mới
                var result = lai_XeRepository.Edit(model);

                if (result)
                {
                    return Json(new { success = true, message = "Sửa lái xe thành công.", data = model });
                }
                else
                {
                    return Json(new { success = false, message = "Sửa lái xe thất bại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết và trả về lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }



        [HttpGet]
        public List<DM_Lai_Xe> ReturnListExcel(int zone, int endpostcode, string trangthai)
        {

            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.trangthai = trangthai;

            DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();
            ReturnDM_Lai_Xe returnquality = new ReturnDM_Lai_Xe();
            returnquality = lai_XeRepository.DM_Lai_Xe_DETAIL(zone, endpostcode, trangthai);
            return returnquality.ListDM_Lai_Xe;
        }
        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.zone ,ViewBag.endpostcode ,ViewBag.trangthai);
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
                workSheet.Cells[4, 1].LoadFromCollection(list, false, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<DM_Lai_Xe> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "DANH MỤC LÁI XE";
            worksheet.Cells["A1:J1"].Merge = true;

            worksheet.Cells[2, 10].Value = "MÃ BÁO CÁO:BT/DMLX";
            worksheet.Cells["J2:J2"].Merge = true;

            worksheet.Cells[3, 1].Value = "STT";
            worksheet.Cells[3, 2].Value = "Id";
            worksheet.Cells[3, 3].Value = "Chi nhánh";
            worksheet.Cells[3, 4].Value = "Tên nhân viên";
            worksheet.Cells[3, 5].Value = "Bưu cục";
            worksheet.Cells[3, 6].Value = "Tuyến phát";
            worksheet.Cells[3, 7].Value = "Mã nhân viên";
            worksheet.Cells[3, 8].Value = "Tên lái xe";
            worksheet.Cells[3, 9].Value = "Bưu tá";
            worksheet.Cells[3, 10].Value = "Trạng thái";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A3:J3"])
            using (var ranges = worksheet.Cells["A1:J1"])

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

                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(int zone, int endpostcode, string trangthai) {

            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.trangthai = trangthai;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            // Response.AddHeader("Content-Disposition", "attachment; filename=Lược Đồ khai thác bưu cục " + endpostcode + ".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        #endregion
    }
}