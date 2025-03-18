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
using DocumentFormat.OpenXml.EMMA;
using Remotion.FunctionalProgramming;
using DocumentFormat.OpenXml.Wordprocessing;
using Font = System.Drawing.Font;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Spreadsheet;
using TableStyles = OfficeOpenXml.Table.TableStyles;

namespace T41.Areas.Admin.Controllers
{
    public class TMS_TICKETController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult TMS_TICKET()
        {
            return View();

        }
        public ActionResult TMS_DM_Ra_Soat()
        {
            return View();

        }
        
        public ActionResult Report_TMS_TICKET()
        {
            return View();

        }
        public ActionResult Report_TMS_TICKET_CT()
        {
            return View();

        }
        
        public ActionResult Report_TMS_TICKET_TH()
        {
            return View();

        }
        public ActionResult Report_TMS_TICKET_CTND()
        {
            return View();

        }
        public ActionResult QL_PhanCong_HT()
        {
            return View();

        }
        public ActionResult THPhanCong()
        {
            return View();

        }
        public ActionResult CTPhanCong()
        {
            return View();

        }
        public ActionResult PhanCong_HT()
        {
            return View();

        }
        public ActionResult DM_Nhan_Vien_TICKET()
        {
            return View();

        }
        public ActionResult TICKET_List_Ra_Soat()
        {
            return View();

        }
        public ActionResult TK_TICKET_UpdaDate()
        {
            return View();

        }
        public ActionResult TH_TICKET_TK()
        {
            return View();

        }
        public ActionResult CT_TICKET_TK()
        {
            return View();

        }
        #region Check user
      [HttpGet]
        public ActionResult GetUserIdsByUserName(string userName)
        {
            try
            {
                // Kiểm tra nếu UserName không hợp lệ
                if (string.IsNullOrEmpty(userName))
                {
                    return Json(new { success = false, message = "UserName không hợp lệ." }, JsonRequestBehavior.AllowGet);
                }

                // Gọi Repository để lấy danh sách userIds
                TMS_TICKETRepository _TICKETRepository = new TMS_TICKETRepository();
                var userIds = _TICKETRepository.GetUserIdsByUserName(userName);  // Gọi hàm GetUserIdsByUserName

                // Kiểm tra xem có userIds không
                if (userIds != null && userIds.Any())
                {
                    // Chuyển danh sách userIds thành chuỗi phân tách dấu ","
                    string userIdsString = string.Join(",", userIds);
                    return Json(new { success = true, userIds = userIdsString }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy userIds cho UserName: " + userName }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo chi tiết
                return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public JsonResult Get_Nhom_Tinh(int nhomtinh, string types)
        {
            TMS_TICKETRepository PostManDeliveryRepository = new TMS_TICKETRepository();
            return Json(PostManDeliveryRepository.Get_Nhom_Tinh(nhomtinh, types), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Nhan_Vien(string id)
        {
            TMS_TICKETRepository PostManDeliveryRepository = new TMS_TICKETRepository();
            return Json(PostManDeliveryRepository.Get_Nhan_Vien(id), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        #region Danh mục nhân viên
        [HttpPost]
        public JsonResult ListTMS_TICKET(string tennv, int  zone)
        {
            ViewBag.tennv = tennv;
            ViewBag.zone = zone;
            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.TMS_TICKET_DETAIL(tennv, zone);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult DM_Nhan_Vien(string tennv, int zone)
        {
            ViewBag.tennv = tennv;

            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.DM_Nhan_Vien(tennv, zone);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult Khoa(int id, string types)
        {
            // Gọi phương thức Khoa trong repository để thực hiện thao tác khóa
            TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();
            ReturnResponseUpdate result = lai_XeRepository.Khoa(id,types);

            // Trả về kết quả dưới dạng JSON
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLaiXeInfo(int id)
        {
            TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = lai_XeRepository.TMS_TICKET_DETAIL_SUA(id); // Lấy thông tin lái xe từ cơ sở dữ liệu
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;  // Đảm bảo có thể truyền lượng dữ liệu lớn
            return jsonResult;
        }

        [HttpPost]
        public JsonResult AddLaiXe(TMS_TICKET_Sua model)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (model == null)
                {
                    return Json(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });
                }

                // Tạo repository để thao tác dữ liệu
                TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();

                // Gọi phương thức thêm mới
                var result = lai_XeRepository.Add(model);

                if (result)
                {
                    return Json(new { success = true, message = "Thêm mới thành công.", data = model });
                }
                else
                {
                    return Json(new { success = false, message = "Thêm mới thất bại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết và trả về lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateLaiXe(TMS_TICKET_Sua model)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (model == null)
                {
                    return Json(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });
                }

                // Tạo repository để thao tác dữ liệu
                TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();

                // Gọi phương thức thêm mới
                var result = lai_XeRepository.Edit(model);

                if (result)
                {
                    return Json(new { success = true, message = "Sửa thành công.", data = model });
                }
                else
                {
                    return Json(new { success = false, message = "Sửa thất bại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết và trả về lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpGet]
        public List<TMS_TICKET> ReturnListExcel(string tennv,int zone)
        {

            ViewBag.tennv = tennv;
            ViewBag.zone = zone;

            TMS_TICKETRepository KPIKTRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnKPIKT = new ReturnTMS_TICKET();
            returnKPIKT = KPIKTRepository.TMS_TICKET_DETAIL(tennv,zone);
            return returnKPIKT.List_TMS_TICKET;
        }
        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.tennv, ViewBag.zone);
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
            worksheet.DefaultColWidth = 20;
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
        public ActionResult Export(string tennv,int zone)
        {

            ViewBag.tennv = tennv;
            ViewBag.zone = zone;
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

        #region Danh mục lượt rà soát
        [HttpPost]
        public JsonResult List_Ra_Soat(string startdate, string enddate, string user)
        {
            ViewBag.startdate = startdate;
            ViewBag.user = user;

            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.List_Ra_Soat(common.DateToInt(startdate), common.DateToInt(enddate), user);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult DM_Ra_Soat(int luot)
        {
            ViewBag.luot = luot;

            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.DM_Ra_Soat( luot);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

        #region Quản lý phân công hỗ trợ
        #region Quản lý xin nghỉ
        [HttpPost]
        public JsonResult QL_PhanCong_HT(string startdate, string enddate ,string seach)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.seach = seach;

            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.QL_PhanCong_HT(common.DateToInt(startdate), common.DateToInt(enddate), seach);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult Duyet(string ids)
        {
            try
            {
                // Chuyển đổi chuỗi ids thành danh sách các ID
                var idList = ids.Split(',').Select(id => Convert.ToInt32(id)).ToList();

                // Xử lý duyệt các mục với các ID này (Ví dụ gọi repository để cập nhật trạng thái)
                var users = Convert.ToString(Session["UserName"]);
                TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();

                var result = lai_XeRepository.Duyet(string.Join(",", idList), users); // Truyền danh sách ID vào repository

                return Json(new { success = true, message = "Cập nhật thành công.", data = ids });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult Xin_Nghi(PhanCong_Xin_Nghi model)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (model == null)
                {
                    return Json(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });
                }
                var NGUOI_TAO = Convert.ToString(Session["UserName"]);
                // Tạo repository để thao tác dữ liệu
                TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();

                // Gọi phương thức thêm mới
                var result = lai_XeRepository.Xin_Nghi(model, NGUOI_TAO);

                if (result)
                {
                    return Json(new { success = true, message = "Thêm mới thành công.", data = model });
                }
                else
                {
                    return Json(new { success = false, message = "Thêm mới thất bại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết và trả về lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        #endregion
        #region Tổng hợp xin nghỉ
        [HttpPost]
        public JsonResult THPhanCong_HT(string startdate, string enddate, string seach)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.seach = seach;

            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.THPhanCong_HT(common.DateToInt(startdate), common.DateToInt(enddate), seach);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

        #region Phân công tạm thời
        [HttpPost]
        public JsonResult PhanCong_HT(string startdate, string enddate, string seach, string mabg)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.seach = seach;
            ViewBag.mabg = mabg;

            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.PhanCong_HT(common.DateToInt(startdate), common.DateToInt(enddate), seach, mabg);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Assign(string ids, int newEmployeeId)
        {
            try
            {
                // Tách chuỗi ids thành mảng các SO_HS
                var soHsList = ids.Split(',');  // Tách chuỗi ngăn cách bằng dấu phẩy

                // Khởi tạo repository và các đối tượng cần thiết
                TMS_TICKETRepository tMS_TICKET = new TMS_TICKETRepository();

                // Lặp qua các SO_HS và thực hiện phân công nhân viên
                foreach (var soHS in soHsList)
                {
                    // Cập nhật phân công nhân viên cho từng SO_HS
                    var result = tMS_TICKET.Phan_Cong(soHS, newEmployeeId);

                    // Kiểm tra kết quả của từng phân công
                    if (!result)
                    {
                        return Json(new { success = false, message = "Có lỗi xảy ra khi phân công!" });
                    }
                }

                // Nếu tất cả phân công thành công
                return Json(new { success = true, message = "Phân công thành công!" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return Json(new { success = false, message = "Có lỗi xảy ra khi phân công!" });
            }
        }


        #endregion
        #endregion
        #region import

        public JsonResult ImportTrackingItems(HttpPostedFileBase file,string user, string luot)
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
            var USERS_IMPORT = @Convert.ToString(Session["UserName"]);
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
                        string collum9Value = worksheet.Cells[row, 9]?.Text;
                        int Ma_TT = 0;

                        if (!string.IsNullOrWhiteSpace(collum9Value))
                        {
                            if (collum9Value.Contains("Đang xử lý"))
                                Ma_TT = 1;
                            else if (collum9Value.Contains("Đã có kết luận cuối cùng"))
                                Ma_TT = 2;
                            else if (collum9Value.Contains("Đóng"))
                                Ma_TT = 3;
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
                                            new XElement("user", user),
                                            new XElement("Ma_TT", Ma_TT),
                                            new XElement("Luot", luot),
                                            new XElement("USERS_IMPORT", USERS_IMPORT)
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
            var importResponse = _ticketRepository.ImportTickets(xmlString, sessionId, user, luot);

            // Nếu import thành công, trả về phản hồi thành công với thông tin ngày, lượt và người dùng
            var response = new
            {
                Code = "00",  // Thành công
                Message = "Nhập dữ liệu thành công!",
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm"), // Ngày nhập

                Luot = luot,  // Lượt nhập, có thể thay đổi tùy theo logic của bạn
                User = user // Tên người dùng
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Save_TICKET(string startdate,string user)
{
    try
    {
                // Gọi logic xử lý lưu dữ liệu
                TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();
            var response = lai_XeRepository.SaceTickets(common.DateToInt(startdate), user); // Gọi hàm lưu dữ liệu

        // Trả về kết quả JSON thành công
        return Json(response, JsonRequestBehavior.AllowGet);
    }
    catch (Exception ex)
    {
        // Trả về lỗi nếu có
        return Json(new { status = "error", message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
    }
}
        #endregion
        [HttpPost]
        public JsonResult UpdateTicketTrangThai(string itemcode, string Item, string Ly_Do, string Ly_Do_Khac, string types)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (itemcode == null)
                {
                    return Json(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });
                }
                var USERS_UPDATE = @Convert.ToString(Session["UserName"]);
                // Tạo repository để thao tác dữ liệu
                TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();

                // Gọi phương thức thêm mới
                var result = lai_XeRepository.UpdateTicketTrangThai(USERS_UPDATE, itemcode, Item, Ly_Do,Ly_Do_Khac, types);

                if (result)
                {
                    return Json(new { success = true, message = "Cập nhật thành công.", data = itemcode });
                }
                else
                {
                    return Json(new { success = false, message = "Cập nhật thất bại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết và trả về lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetTicketDetails(string So_HS, string types)
        {
            TMS_TICKETRepository lai_XeRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = lai_XeRepository.GetTicketDetails(So_HS, types);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;  // Đảm bảo có thể truyền lượng dữ liệu lớn
            return jsonResult;
        }
        #region Báo cáo dành cho người quản lý
        #region List Ticket
        [HttpPost]
        public JsonResult List_TICKET(string startdate, int Id, string luot,string types)
        {

            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.TICKET_DETAIL(common.DateToInt(startdate), Id, luot, types);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public List<LIST_TICKET> ReturnListExcel(string startdate, int id, string luot, string types)
        {
            ViewBag.startdate = startdate;
            ViewBag.id = id;
            ViewBag.luot = luot;
            ViewBag.types = types;

            TMS_TICKETRepository KPIKTRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnKPIKT = new ReturnTMS_TICKET();
            returnKPIKT = KPIKTRepository.TICKET_DETAIL(common.DateToInt(startdate), id,  luot, types);
            return returnKPIKT.ListTICKET;
        }
        public Stream CreateExcelFile_TICKET(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.id, ViewBag.luot, ViewBag.types);
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
            var list = ReturnListExcel(ViewBag.startdate, ViewBag.id, ViewBag.luot, ViewBag.types);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;

            // Tạo header
            worksheet.Cells[1, 1].Value = "CHI TIẾT PHÂN CÔNG HỒ SƠ KHIẾU NẠI";
            worksheet.Cells["A1:O1"].Merge = true;
            worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Arial", 14, FontStyle.Bold));

            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO: CSKH/CTHSKN";
            worksheet.Cells[2, 7].Value = "Ngày: " + ViewBag.startdate;
            worksheet.Cells["G2:I2"].Merge = true;

            // Tiêu đề các cột
            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Số hồ sơ";
            worksheet.Cells[4, 3].Value = "Mã GD/BG";
            worksheet.Cells[4, 4].Value = "Ngày tạo";
            worksheet.Cells[4, 5].Value = "Tình trạng xử lý";
            worksheet.Cells[4, 6].Value = "Mã ĐV chủ trì";
            worksheet.Cells[4, 7].Value = "Đơn vị chủ trì";
            worksheet.Cells[4, 8].Value = "Ngày hết hạn";
            worksheet.Cells[4, 9].Value = "Tinh_Nhan";
            worksheet.Cells[4, 10].Value = "Tinh_Tra";
            worksheet.Cells[4, 11].Value = "Tên nhân viên";
            worksheet.Cells[4, 12].Value = "Hồ sơ mới";
            worksheet.Cells[4, 13].Value = "Trạng thái phân công";
            worksheet.Cells[4, 14].Value = "Lượt rà soát";
            // Định dạng cho header
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
                // Set Font cho text trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 11));
                // Set Border
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                // Định dạng cho tiêu đề
                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));

                // Định dạng cho ngày
                Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
            int row = 5;
            foreach (var item in listItems)
            {
                worksheet.Cells[row, 1].Value = item.STT;
                worksheet.Cells[row, 2].Value = item.So_HS;
                worksheet.Cells[row, 3].Value = item.Ma_BG;
                worksheet.Cells[row, 4].Value = item.Ngay_Tao;
                worksheet.Cells[row, 5].Value = item.Trang_Thai == "1"? "Đang xử lý": item.Trang_Thai == "2"? "Đã có kết luận cuối cùng" : "Đóng";
                worksheet.Cells[row, 6].Value = item.Ma_DV_Chu_Tri;
                worksheet.Cells[row, 7].Value = item.Ten_DV_Chu_Tri;
                worksheet.Cells[row, 8].Value = item.Ngay_Het_han;
                worksheet.Cells[row, 9].Value = item.Tinh_Nhan;
                worksheet.Cells[row, 10].Value = item.Tinh_Tra;
                worksheet.Cells[row, 11].Value = item.TEN_NV;
                worksheet.Cells[row, 12].Value = item.STATUS == "1" ? "Cũ" : "Mới"; // Hồ sơ mới
                worksheet.Cells[row, 13].Value = item.STATUS_HS == "1"? "Thủ công" : "Tự động";
                worksheet.Cells[row, 14].Value = item.Luot;
                row++;
            }
        }
        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_TICKET(string startdate,int id, string luot,string types)
        {

            ViewBag.startdate = startdate;
            ViewBag.id = id;
            ViewBag.luot = luot;
            ViewBag.types = types;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_TICKET();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
             Response.AddHeader("Content-Disposition", "attachment; filename=Chi tiết phân công hồ sơ khiếu nại.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion
        #region Tổng Hợp Ticket

        [HttpPost]
        public JsonResult List_TICKET_TH(string startdate, int luot, string user)
        {

            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.TMS_TICKET_DETAIL_TH(common.DateToInt(startdate), luot, user);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion
        #endregion

        #region Báo cáo dành cho người dùng

        [HttpPost]
        public JsonResult List_TICKET_ND(string startdate, string enddate, int thoigian,string seach, int trangthai, string mabg, int donvi,string searchtinh)
        {
           // var UserName = Convert.ToString(Session["UserName"]);
            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.TICKET_DETAIL_ND(common.DateToInt(startdate), common.DateToInt(enddate), thoigian,seach, trangthai, mabg, donvi, searchtinh);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public List<LIST_TICKET_ND> ReturnListExcel_ND(string startdate, string enddate, int thoigian,string seach, int trangthai, string mabg, int donvi,string searchtinh)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.thoigian = thoigian;
            ViewBag.seach = seach;
            ViewBag.trangthai = trangthai;
            ViewBag.mabg = mabg;
            ViewBag.donvi = donvi;
            ViewBag.searchtinh = searchtinh;

            TMS_TICKETRepository KPIKTRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnKPIKT = new ReturnTMS_TICKET();
            returnKPIKT = KPIKTRepository.TICKET_DETAIL_ND(common.DateToInt(startdate), common.DateToInt(enddate), thoigian,seach, trangthai, mabg, donvi, searchtinh);
            return returnKPIKT.List_TICKET_ND;
        }
        public Stream CreateExcelFile_TICKET_ND(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel_ND(ViewBag.startdate, ViewBag.enddate, ViewBag.thoigian, ViewBag.seach, ViewBag.trangthai, ViewBag.mabg, ViewBag.donvi, ViewBag.searchtinh);
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

                BindingFormatForExcel_TICKET_ND(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        //Phần sửa excel
        private void BindingFormatForExcel_TICKET_ND(ExcelWorksheet worksheet, List<LIST_TICKET_ND> listItems)
        {
            var list = ReturnListExcel_ND(ViewBag.startdate, ViewBag.enddate, ViewBag.thoigian,ViewBag.seach,ViewBag.trangthai,ViewBag.mabg, ViewBag.donvi, ViewBag.searchtinh);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;

            // Tạo header
            worksheet.Cells[1, 1].Value = "QUẢN LÝ HỒ SƠ KHIẾU NẠI";
            worksheet.Cells["A1:O1"].Merge = true;
            worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Arial", 14, FontStyle.Bold));

            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO: CSKH/QLKN";
            worksheet.Cells[2, 6].Value = "Từ ngày:" + " " + ViewBag.startdate + "-" + "Đến ngày" + " " + ViewBag.enddate;
            worksheet.Cells["G2:I2"].Merge = true;
            // Tiêu đề các cột
            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Số hồ sơ";
            worksheet.Cells[4, 3].Value = "Mã GD/BG";
            worksheet.Cells[4, 4].Value = "Tỉnh nhận";
            worksheet.Cells[4, 5].Value = "Tỉnh trả";
            worksheet.Cells[4, 6].Value = "Ngày tạo";
            worksheet.Cells[4, 7].Value = "Tình trạng xử lý";
            worksheet.Cells[4, 8].Value = "Mã ĐV chủ trì";
            worksheet.Cells[4, 9].Value = "Ngày hết hạn";
            worksheet.Cells[4, 10].Value = "Hạn xử lý cuối";
            worksheet.Cells[4, 11].Value = "Tên nhân viên";
            worksheet.Cells[4, 12].Value = "Trạng thái";
            worksheet.Cells[4, 13].Value = "Trạng thái";
            worksheet.Cells[4, 14].Value = "Trạng thái hồ sơ";
            worksheet.Cells[4, 15].Value = "Trạng thái phân công";
            // Định dạng cho header
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
                // Set Font cho text trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 11));
                // Set Border
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                // Định dạng cho tiêu đề
                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));

                // Định dạng cho ngày
                Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
            int row = 5;
            int stt = 1;
            foreach (var item in listItems)
            {
                worksheet.Cells[row, 1].Value = stt;
                worksheet.Cells[row, 2].Value = item.So_HS;
                worksheet.Cells[row, 3].Value = item.Tinh_Nhan;
                worksheet.Cells[row, 4].Value = item.Tinh_Tra;
                worksheet.Cells[row, 5].Value = item.Ma_BG;
                worksheet.Cells[row, 6].Value = item.Ngay_Tao;
                worksheet.Cells[row, 7].Value = item.Trang_Thai == "1" ? "Đang xử lý" : item.Trang_Thai == "2" ? "Đã có kết luận cuối cùng" : "Đóng";
                worksheet.Cells[row, 8].Value = item.Ma_DV_Chu_Tri;
                worksheet.Cells[row, 9].Value = item.Ngay_Het_han;
                worksheet.Cells[row, 10].Value = item.NGAY_XL_CUOI;
                worksheet.Cells[row, 11].Value = item.TEN_NV;
                worksheet.Cells[row, 12].Value = item.UPDATE_TT;
                worksheet.Cells[row, 13].Value = item.UPDATE_DV;
                worksheet.Cells[row, 14].Value = item.STATUS == "1" ? "Cũ" : "Mới";
                worksheet.Cells[row, 15].Value = item.STATUS_HS == "1" ? "Thủ công" : "Tự động";
                row++;
                stt++;
            }
        }
        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export_TICKET_ND(string startdate, string enddate, int thoigian,string seach, int trangthai, string mabg, int donvi,string searchtinh)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.thoigian = thoigian;
            ViewBag.seach = seach;
            ViewBag.trangthai = trangthai;
            ViewBag.mabg = mabg;
            ViewBag.donvi = donvi;
            ViewBag.searchtinh = searchtinh; 
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_TICKET_ND();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=Quản lý hồ sơ khiếu nại.xlsx");
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

        #region Thống kê báo cáo
        [HttpPost]
        public JsonResult TK_TICKET_UpdaDate(string startdate, string enddate, string seach, string mabg, int donvi)
        {
            // var UserName = Convert.ToString(Session["UserName"]);
            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.TK_TICKET_UpdaDate(common.DateToInt(startdate), common.DateToInt(enddate), seach, mabg, donvi);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        #region Báo cáo tổng hợp
        [HttpPost]
        public JsonResult TH_TICKET_TK(string startdate, string enddate, int thoigian, string seach, int donvi,string searchtinh)
        {
            // var UserName = Convert.ToString(Session["UserName"]);
            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.TH_TICKET_TK(common.DateToInt(startdate), common.DateToInt(enddate), thoigian, seach, donvi, searchtinh);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public List<TH_TICKET_TK> ReturnTH_TICKET_TK(string startdate, string enddate, int thoigian, string seach, int donvi, string searchtinh)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.thoigian = thoigian;
            ViewBag.seach = seach;
            ViewBag.donvi = donvi;
            ViewBag.searchtinh = searchtinh;

            TMS_TICKETRepository KPIKTRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnKPIKT = new ReturnTMS_TICKET();
            returnKPIKT = KPIKTRepository.TH_TICKET_TK(common.DateToInt(startdate), common.DateToInt(enddate), thoigian, seach, donvi, searchtinh);
            return returnKPIKT.ListTH_TICKET_TK;
        }
        public Stream CreateExcelFileTH_TICKET_TK(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnTH_TICKET_TK(ViewBag.startdate, ViewBag.enddate, ViewBag.thoigian, ViewBag.seach, ViewBag.donvi, ViewBag.searchtinh);
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

                BindingFormatForExcelTH_TICKET_TK(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        //Phần sửa excel
        private void BindingFormatForExcelTH_TICKET_TK(ExcelWorksheet worksheet, List<TH_TICKET_TK> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "Tổng hợp hồ sơ khiếu nại";
            worksheet.Cells["A1:J1"].Merge = true;

            worksheet.Cells[2, 9].Value = "MÃ BÁO CÁO:TKBC/THHS";
            worksheet.Cells["J2:J2"].Merge = true;

            worksheet.Cells[3, 1].Value = "STT";
            worksheet.Cells[3, 2].Value = "Khu vực";
            worksheet.Cells[3, 3].Value = "Nhóm";
            worksheet.Cells[3, 4].Value = "Id nhân viên";
            worksheet.Cells[3, 5].Value = "Tên nhân viên";
            worksheet.Cells[3, 6].Value = "Tổng số";
            worksheet.Cells[3, 7].Value = "Đang xử lý";
            worksheet.Cells[3, 8].Value = "Đã có kết luận";
            worksheet.Cells[3, 9].Value = "Đóng";
            worksheet.Cells[3, 10].Value = "Đã hoàn thành";
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
                // Set Font cho text trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 11));
                // Set Border
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                // Định dạng cho tiêu đề
                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));

            }
            int row = 4;
            int stt = 1;
            foreach (var item in listItems)
            {
                worksheet.Cells[row, 1].Value = stt;
                worksheet.Cells[row, 2].Value = item.Khu_Vuc == "1" ? "Hà Nội" : item.Khu_Vuc == "2" ? "Hồ Chí Minh" : "";
                worksheet.Cells[row, 3].Value = item.Nhom_Tinh;
                worksheet.Cells[row, 4].Value = item.Id;
                worksheet.Cells[row, 5].Value = item.Ten_NV;
                worksheet.Cells[row, 6].Value = item.Tong_So;
                worksheet.Cells[row, 7].Value = item.DXL;
                worksheet.Cells[row, 8].Value = item.DCKQ;
                worksheet.Cells[row, 9].Value = item.Dong;
                worksheet.Cells[row, 10].Value = item.DHT;
                row++;
                stt++;
            }
        }
      

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult ExportTH_TICKET_TK(string startdate, string enddate, int thoigian, string seach, int donvi, string searchtinh)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.thoigian = thoigian;
            ViewBag.seach = seach;
            ViewBag.donvi = donvi;
            ViewBag.searchtinh = searchtinh;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileTH_TICKET_TK();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=Tổng hợp hồ sơ khiếu nại.xlsx");
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion
        #region Báo cáo chi tiết
        [HttpPost]
        public JsonResult CT_TICKET_TK(string startdate, string enddate, int thoigian, string seach, int donvi,string searchtinh, string Id, string types)
        {
            // var UserName = Convert.ToString(Session["UserName"]);
            TMS_TICKETRepository tICKETRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnquality = new ReturnTMS_TICKET();
            returnquality = tICKETRepository.CT_TICKET_TK(common.DateToInt(startdate), common.DateToInt(enddate), thoigian, seach, donvi, searchtinh, Id, types);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public List<CT_TICKET_TK> ReturnCT_TICKET_TK(string startdate, string enddate, int thoigian, string seach, int donvi, string searchtinh, string Id, string types)
        {
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.thoigian = thoigian;
            ViewBag.seach = seach;
            ViewBag.donvi = donvi;
            ViewBag.searchtinh = searchtinh;
            ViewBag.Id = Id;
            ViewBag.types = types;
            TMS_TICKETRepository KPIKTRepository = new TMS_TICKETRepository();
            ReturnTMS_TICKET returnKPIKT = new ReturnTMS_TICKET();
            returnKPIKT = KPIKTRepository.CT_TICKET_TK(common.DateToInt(startdate), common.DateToInt(enddate), thoigian, seach, donvi, searchtinh, Id, types);
            return returnKPIKT.ListCT_TICKET_TK;
        }
        public Stream CreateExcelFileCT_TICKET_TK(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnCT_TICKET_TK(ViewBag.startdate, ViewBag.enddate, ViewBag.thoigian, ViewBag.seach, ViewBag.donvi, ViewBag.searchtinh, ViewBag.Id, ViewBag.types);
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

                BindingFormatForExcelCT_TICKET_TK(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        //Phần sửa excel
        private void BindingFormatForExcelCT_TICKET_TK(ExcelWorksheet worksheet, List<CT_TICKET_TK> listItems)
        {
            worksheet.DefaultColWidth = 20;
            worksheet.DefaultRowHeight = 30;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;

            // Tạo header
            worksheet.Cells[1, 1].Value = "QUẢN LÝ HỒ SƠ KHIẾU NẠI";
            worksheet.Cells["A1:J1"].Merge = true;
            worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Arial", 14, FontStyle.Bold));

            worksheet.Cells[2, 10].Value = "MÃ BÁO CÁO: CSKH/QLKN";
            worksheet.Cells[2, 5].Value = "Từ ngày:" + " " + ViewBag.startdate + "-" + "Đến ngày" + " " + ViewBag.enddate;
            worksheet.Cells["D2:E2"].Merge = true;
            // Tiêu đề các cột
            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Số hồ sơ";
            worksheet.Cells[4, 3].Value = "Mã GD/BG";
            worksheet.Cells[4, 4].Value = "Ngày tạo";
            worksheet.Cells[4, 5].Value = "Tình trạng xử lý";
            worksheet.Cells[4, 6].Value = "Mã ĐV chủ trì";
            worksheet.Cells[4, 7].Value = "Ngày hết hạn";
            worksheet.Cells[4, 8].Value = "Tỉnh nhận";
            worksheet.Cells[4, 9].Value = "Tỉnh trả";
            worksheet.Cells[4, 10].Value = "Tên nhân viên";
            // Định dạng cho header
            using (var range = worksheet.Cells["A4:J4"])
            using (var ranges = worksheet.Cells["A1:J1"])
            using (var Ngay = worksheet.Cells["D2:E2"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 11));
                // Set Border
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                // Định dạng cho tiêu đề
                ranges.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ranges.Style.Font.SetFromFont(new Font("Arial", 14));

                // Định dạng cho ngày
                Ngay.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
            int row = 5;
            int stt = 1;
            foreach (var item in listItems)
            {
                worksheet.Cells[row, 1].Value = stt;
                worksheet.Cells[row, 2].Value = item.So_HS;
                worksheet.Cells[row, 3].Value = item.Ma_BG;
                worksheet.Cells[row, 4].Value = item.Ngay_Tao;
                worksheet.Cells[row, 5].Value = item.Trang_Thai == "1" ? "Đang xử lý" : item.Trang_Thai == "2" ? "Đã có kết luận cuối cùng" : item.Trang_Thai == "3" ? "Đóng" :item.Trang_Thai == "4" ? "Đã hoàn thành" : "";
                worksheet.Cells[row, 6].Value = item.Ma_DV_Chu_Tri;
                worksheet.Cells[row, 7].Value = item.Ngay_Het_han;
                worksheet.Cells[row, 8].Value = item.Tinh_Nhan;
                worksheet.Cells[row, 9].Value = item.Tinh_Tra;
                worksheet.Cells[row, 10].Value = item.TEN_NV;
                row++;
                stt++;
            }
        }
      

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult ExportCT_TICKET_TK(string startdate, string enddate, int thoigian, string seach, int donvi, string searchtinh, string Id, string types)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            ViewBag.thoigian = thoigian;
            ViewBag.seach = seach;
            ViewBag.donvi = donvi;
            ViewBag.searchtinh = searchtinh;
            ViewBag.Id = Id;
            ViewBag.types = types;
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileCT_TICKET_TK();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=Chi tiết hồ sơ khiếu nại.xlsx");
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion
        #endregion
    }
}
