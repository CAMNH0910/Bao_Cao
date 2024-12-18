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
    public class TMS_TICKETController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult TMS_TICKET()
        {
            return View();

        }
        public ActionResult Report_TMS_TICKET()
        {
            return View();

        }

        public JsonResult Get_User(int user)
        {
            TMS_TICKETRepository PostManDeliveryRepository = new TMS_TICKETRepository();
            return Json(PostManDeliveryRepository.Get_User(user), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_Nhom_Tinh(int nhomtinh)
        {
            TMS_TICKETRepository PostManDeliveryRepository = new TMS_TICKETRepository();
            return Json(PostManDeliveryRepository.Get_Nhom_Tinh(nhomtinh), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Nhan_Vien(int id)
        {
            TMS_TICKETRepository PostManDeliveryRepository = new TMS_TICKETRepository();
            return Json(PostManDeliveryRepository.Get_Nhan_Vien(id), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListTMS_TICKET(string tennv)
        {
            ViewBag.tennv = tennv;

            TMS_TICKETRepository KPI_Thu_GomRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = KPI_Thu_GomRepository.TMS_TICKET_DETAIL(tennv);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult UpdateTMS_TICKET(string ID, string Ten_NV, string Phan_User, string Phan_Tinh, string Nhom_Tinh, string Ghi_Chu, int isdelete)
        {

            TMS_TICKETRepository TMS_TICKETRepository = new TMS_TICKETRepository();
            return Json(TMS_TICKETRepository.Update_TMS_TICKET(ID, Ten_NV, Phan_User, Phan_Tinh, Nhom_Tinh, Ghi_Chu, isdelete), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertTMS_TICKET(string Ten_NV, string Phan_User, string Phan_Tinh, string Nhom_Tinh, string Ghi_Chu)
        {

            TMS_TICKETRepository TMS_TICKETRepository = new TMS_TICKETRepository();
            return Json(TMS_TICKETRepository.Insert_TMS_TICKET(Ten_NV, Phan_User, Phan_Tinh, Nhom_Tinh, Ghi_Chu), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public List<TMS_TICKET> ReturnListExcel(string tennv)
        {

            ViewBag.tennv = tennv;

            TMS_TICKETRepository KPIKTRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnKPIKT = new ReturnTMS_TICKET();
            returnKPIKT = KPIKTRepository.TMS_TICKET_DETAIL(tennv);
            return returnKPIKT.List_TMS_TICKET;
        }
        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.tennv);
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
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<TMS_TICKET> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "DANH MỤC NHÂN VIÊN";
            worksheet.Cells["A1:G1"].Merge = true;

            worksheet.Cells[2, 7].Value = "MÃ BÁO CÁO:CSKH/DMNV";
            worksheet.Cells["G2:G2"].Merge = true;

            worksheet.Cells[3, 1].Value = "STT";
            worksheet.Cells[3, 2].Value = "Id";
            worksheet.Cells[3, 3].Value = "Tên nhân viên";
            worksheet.Cells[3, 4].Value = "Phân User";
            worksheet.Cells[3, 5].Value = "Phân tỉnh";
            worksheet.Cells[3, 6].Value = "Ghi chú";
            worksheet.Cells[3, 7].Value = "Trạng thái";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A3:G3"])
            using (var ranges = worksheet.Cells["A1:G1"])

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
        public ActionResult Export(string tennv)
        {

            ViewBag.tennv = tennv;
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


        #region import

        public JsonResult ImportTrackingItems(HttpPostedFileBase file,string user)
        {
            TMS_TICKETRepository _ticketRepository = new TMS_TICKETRepository();

            // Kiểm tra tính hợp lệ của file
            if (file == null || file.ContentLength == 0)
            {
                return Json(new { Code = "01", Message = "File không hợp lệ." });
            }

            // Kiểm tra định dạng file
            if (Path.GetExtension(file.FileName).ToLower() != ".xlsx")
            {
                return Json(new { Code = "01", Message = "Vui lòng chọn file Excel (.xlsx)." });
            }

            var sessionId = DateTime.Now.ToString("yyyyMMddHHmmss");
            XElement documentElement = new XElement("DOCUMENTELEMENT");

            try
            {
                using (var package = new ExcelPackage(file.InputStream))
                {
                    // Kiểm tra số lượng worksheet
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        return Json(new { Code = "01", Message = "File không có worksheet nào." });
                    }
                    // Lấy worksheet đầu tiên mà không cần kiểm tra tên
                    var worksheet = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension?.Rows;

                    // Kiểm tra số dòng trong worksheet
                    if (rowCount == null || rowCount < 2) // Bỏ qua dòng tiêu đề
                    {
                        return Json(new { Code = "01", Message = "Worksheet không có dữ liệu." });
                    }

                    // Lặp qua từng hàng dữ liệu, bỏ qua dòng tiêu đề
                    for (int row = 2; row <= rowCount; row++)
                    {
                        string soHoSo = worksheet.Cells[row, 1]?.Text;

                        if (string.IsNullOrWhiteSpace(soHoSo))
                        {
                            continue; // Bỏ qua hàng nếu TTK_CODE rỗng
                        }
                       // object column8Value = worksheet.Cells[row, 8]?.Value;
                        // Tạo trackElement với các trường có thể null
                        XElement trackElement = new XElement("TRACK",
                                            new XElement("Collum1", soHoSo),
                                            new XElement("Collum2", worksheet.Cells[row, 2]?.Text),
                                            new XElement("Collum3", worksheet.Cells[row, 3]?.Text),
                                            new XElement("Collum4", worksheet.Cells[row, 4]?.Text),
                                            new XElement("Collum5", worksheet.Cells[row, 5]?.Text),
                                            new XElement("Collum6", worksheet.Cells[row, 6]?.Text),
                                            new XElement("Collum7", worksheet.Cells[row, 7]?.Text),
                                            new XElement("Collum8", worksheet.Cells[row, 8]?.Value),
                                            new XElement("Collum9", worksheet.Cells[row, 9]?.Text),
                                            new XElement("Collum10", worksheet.Cells[row, 10]?.Text),
                                            new XElement("Collum11", worksheet.Cells[row, 11]?.Text),
                                            new XElement("Collum12", worksheet.Cells[row, 12]?.Text),
                                            new XElement("Collum13", worksheet.Cells[row, 13]?.Text),
                                            new XElement("Collum14", worksheet.Cells[row, 14]?.Text),
                                            new XElement("Collum15", worksheet.Cells[row, 15]?.Text),
                                            new XElement("Collum16", worksheet.Cells[row, 16]?.Text),
                                            new XElement("Collum17", worksheet.Cells[row, 17]?.Value),
                                            new XElement("Collum18", worksheet.Cells[row, 18]?.Value),
                                            new XElement("Collum19", worksheet.Cells[row, 19]?.Text),
                                            new XElement("Collum20", worksheet.Cells[row, 20]?.Text),
                                            new XElement("Collum21", worksheet.Cells[row, 21]?.Text),
                                            new XElement("Collum22", worksheet.Cells[row, 22]?.Text),
                                            new XElement("Collum23", worksheet.Cells[row, 23]?.Value),
                                            new XElement("Collum24", worksheet.Cells[row, 24]?.Text),
                                            new XElement("IDSESSION", sessionId),
                                            new XElement("user", user)
);

                        documentElement.Add(trackElement); // Thêm từng TRACK vào DOCUMENTELEMENT
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Code = "01", Message = "Đã xảy ra lỗi: " + ex.Message });
            }

            string xmlString = new XDocument(documentElement).ToString(); // Chuyển đổi sang chuỗi XML

            // Gọi phương thức import từ repository
            var response = _ticketRepository.ImportTickets(xmlString, sessionId, user);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region List Ticket
        [HttpPost]
        public JsonResult List_TICKET(string startdate, string enddate, int time, int user, int nhomtinh, int id)
        {

            TMS_TICKETRepository KPI_Thu_GomRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = KPI_Thu_GomRepository.TMS_TICKET_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), time, user, nhomtinh, id);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public List<LIST_TICKET> ReturnListExcel(string startdate, string enddate, int time, int user, int nhomtinh, int id)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.time = time;
            ViewBag.user = user;
            ViewBag.nhomtinh = nhomtinh;
            ViewBag.id = id;

            TMS_TICKETRepository KPIKTRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnKPIKT = new ReturnTMS_TICKET();
            returnKPIKT = KPIKTRepository.TMS_TICKET_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate), time, user, nhomtinh, id);
            return returnKPIKT.ListTICKET;
        }
        public Stream CreateExcelFile_TICKET(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.time, ViewBag.user, ViewBag.nhomtinh, ViewBag.id);
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
                var range = workSheet.Cells[4, 1, workSheet.Dimension.End.Row, workSheet.Dimension.End.Column];
                // Áp dụng filter cho tất cả các cột
                range.AutoFilter = true;

                BindingFormatForExcel_TICKET(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel
        private void BindingFormatForExcel_TICKET(ExcelWorksheet worksheet, List<LIST_TICKET> listItems)
        {
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.enddate, ViewBag.time, ViewBag.user, ViewBag.nhomtinh, ViewBag.id);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "QUẢN LÝ YÊU CẦU";
            worksheet.Cells["A1:AB1"].Merge = true;

            worksheet.Cells[2, 28].Value = "MÃ BÁO CÁO:CSKH/TK";
            worksheet.Cells["AB2:AB2"].Merge = true;
            worksheet.Cells[2, 14].Value = "Từ ngày:" + " " + ViewBag.startdate + "-" + "Đến ngày" + " " + ViewBag.endDate;
            worksheet.Cells["N2:O2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Số hồ sơ";
            worksheet.Cells[4, 3].Value = "Số phiếu khiếu nại";
            worksheet.Cells[4, 4].Value = "Mã GD/BG";
            worksheet.Cells[4, 5].Value = "Dịch Vụ Sử Dụng";
            worksheet.Cells[4, 6].Value = "Mã  ĐV Tiếp nhận";
            worksheet.Cells[4, 7].Value = "ĐV Tiếp nhận";
            worksheet.Cells[4, 8].Value = "Loại khiếu nại";
            worksheet.Cells[4, 9].Value = "Ngày tạo";
            worksheet.Cells[4, 10].Value = "Tình trạng xử lý";
            worksheet.Cells[4, 11].Value = "Nội dung khiếu nại";
            worksheet.Cells[4, 12].Value = "Người khiếu nại";
            worksheet.Cells[4, 13].Value = "Địa chỉ người khiếu nại";
            worksheet.Cells[4, 14].Value = "Điện thoại người khiếu nại";
            worksheet.Cells[4, 15].Value = "Email người khiếu nại";
            worksheet.Cells[4, 16].Value = "Mã ĐV chủ trì";
            worksheet.Cells[4, 17].Value = "Đơn vị chủ trì";
            worksheet.Cells[4, 18].Value = "Thời Gian Xử Lý Cuối Cùng";
            worksheet.Cells[4, 19].Value = "Ngày hết hạn";
            worksheet.Cells[4, 20].Value = "Dịch vụ";
            worksheet.Cells[4, 21].Value = "Lý do khiếu nại";
            worksheet.Cells[4, 22].Value = "Hình thức KN";
            worksheet.Cells[4, 23].Value = "Kết Quả Khiếu Nại";
            worksheet.Cells[4, 24].Value = "Ngày Đóng Khiếu Nại";
            worksheet.Cells[4, 25].Value = "Số Tiền Bồi Thường";
            worksheet.Cells[4, 26].Value = "Tinh_Nhan";
            worksheet.Cells[4, 27].Value = "Tinh_Tra";
            worksheet.Cells[4, 28].Value = "Tên nhân viên";
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:AB4"])
            using (var ranges = worksheet.Cells["A1:AB1"])
            using (var Ngay = worksheet.Cells["N2:O2"])
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
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_TICKET(string startdate, string enddate, int time, int user, int nhomtinh, int id)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.time = time;
            ViewBag.user = user;
            ViewBag.nhomtinh = nhomtinh;
            ViewBag.id = id;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_TICKET();
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
