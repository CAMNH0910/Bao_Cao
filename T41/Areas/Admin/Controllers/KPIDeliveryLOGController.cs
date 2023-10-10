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

    public class KPIDeliveryLOGController : Controller
    {
        Convertion common = new Convertion();      


        public JsonResult PoscodeEms()
        {
            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            return Json(qualityKPIDeliveryLOGRepository.GetPoscodeEms(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Detail_Item_Ems_TRUOT(int StartPoscode, int PostmanID, string StartDate, string EndDate, string IsService, int Istype)
        {
            if(PostmanID == null)
            {
                PostmanID = 0;
            }
            KPIDeliveryLOGRepository KPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnDetail_Item_Ems_TRUOT ReturnDetail_Item_Ems_TRUOT = new ReturnDetail_Item_Ems_TRUOT();
            ReturnDetail_Item_Ems_TRUOT = KPIDeliveryLOGRepository.Detail_Item_Ems_TRUOT(StartPoscode, PostmanID, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Istype);
            return View(ReturnDetail_Item_Ems_TRUOT);
        }

        public ActionResult Detail_Item_Ems_KTT(int StartPoscode, int PostmanID, string StartDate, string EndDate, string IsService, int Istype)
        {
            if (PostmanID == null)
            {
                PostmanID = 0;
            }
            KPIDeliveryLOGRepository KPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnDetail_Item_Ems_KTT ReturnDetail_Item_Ems_KTT = new ReturnDetail_Item_Ems_KTT();
            ReturnDetail_Item_Ems_KTT = KPIDeliveryLOGRepository.Detail_Item_Ems_KTT(StartPoscode, PostmanID, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Istype);
            return View(ReturnDetail_Item_Ems_KTT);
        }

        #region ExcelDetail_Item_Ems_KTT
        [HttpGet]
        public List<Detail_Item_Ems_KTT> ReturnListExcelDetail_Item_Ems_KTT(int StartPoscode, int PostmanID, string StartDate, string EndDate, string IsService, int Istype)
        {
            ViewBag.StartPoscode = StartPoscode;
            ViewBag.PostmanID = PostmanID;
            // ViewBag.IsCod = IsCod;
            ViewBag.IsService = IsService;
            ViewBag.Istype = Istype;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnDetail_Item_Ems_KTT returnquality = new ReturnDetail_Item_Ems_KTT();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_Item_Ems_KTT(StartPoscode, PostmanID, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Istype);
            return returnquality.ListDetail_Item_Ems_KTT;
        }

        public Stream CreateExcelFileDetail_Item_Ems_KTT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail_Item_Ems_KTT(ViewBag.StartPoscode,ViewBag.PostmanID, ViewBag.StartDate, ViewBag.EndDate, ViewBag.IsService, ViewBag.Istype);
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
                BindingFormatForExcelDetail_Item_Ems_KTT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }




        //Phần sửa excel
        private void BindingFormatForExcelDetail_Item_Ems_KTT(ExcelWorksheet worksheet, List<Detail_Item_Ems_KTT> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "Mã E1";
            worksheet.Cells[1, 2].Value = "Bưu cục";
            worksheet.Cells[1, 3].Value = "Tên bưu cục";
            worksheet.Cells[1, 4].Value = "Mã bưu tá";
            worksheet.Cells[1, 5].Value = "Tên bưu tá";
            worksheet.Cells[1, 6].Value = "Ngày XND/Giao";
            worksheet.Cells[1, 7].Value = "Giờ XND/Giao";
            worksheet.Cells[1, 8].Value = "Dịch vụ";   // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1     // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult ExportDetail_Item_Ems_KTT(int StartPoscode, int PostmanID, string StartDate, string EndDate, string IsService, int Istype)
        {
            ViewBag.StartPoscode = StartPoscode;
            ViewBag.PostmanID = PostmanID;
            // ViewBag.IsCod = IsCod;
            ViewBag.IsService = IsService;
            ViewBag.Istype = Istype;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            var nameBC = "";
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail_Item_Ems_KTT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            switch (Istype)
            {
                case 0:
                    nameBC = "Báo cáo sản lượng trượt phát lần đầu giao bưu tá";
                    break;
                case 1:
                    nameBC = "Báo cáo sản lượng trượt phát thành công giao bưu tá ";
                    break;
                case 2:
                    nameBC = "Báo cáo sản lượng trượt phát lần đầu theo thời gian XND tại bưu cục ";
                    break;
                case 3:
                    nameBC = "Báo cáo sản lượng trượt phát thành công theo thời gian XND tại bưu cục ";
                    break;

            }
            Response.AddHeader("Content-Disposition", "attachment; filename="+ nameBC + " KTT"+ ".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion

        #region ExcelDetail_Item_Ems_TRUOT
        [HttpGet]
        public List<Detail_Item_Ems_TRUOT> ReturnListExcelDetail_Item_Ems_TRUOT(int StartPoscode, int PostmanID, string StartDate, string EndDate, string IsService, int Istype)
        {
            ViewBag.StartProvince = StartPoscode;
            ViewBag.IsService = IsService;
            // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnDetail_Item_Ems_TRUOT returnquality = new ReturnDetail_Item_Ems_TRUOT();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_Item_Ems_TRUOT(StartPoscode, PostmanID, common.DateToInt(StartDate), common.DateToInt(EndDate), IsService, Istype);
            return returnquality.ListDetail_Item_Ems_TRUOT;
        }

        public Stream CreateExcelFileDetail_Item_Ems_TRUOT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail_Item_Ems_TRUOT(ViewBag.StartPoscode, ViewBag.PostmanID, ViewBag.StartDate, ViewBag.EndDate, ViewBag.IsService, ViewBag.Istype);
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
                BindingFormatForExcelDetail_Item_Ems_TRUOT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }




        //Phần sửa excel
        private void BindingFormatForExcelDetail_Item_Ems_TRUOT(ExcelWorksheet worksheet, List<Detail_Item_Ems_TRUOT> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "Mã E1";
            worksheet.Cells[1, 2].Value = "Bưu cục";
            worksheet.Cells[1, 3].Value = "Tên bưu cục";
            worksheet.Cells[1, 4].Value = "Mã bưu tá";
            worksheet.Cells[1, 5].Value = "Tên bưu tá";
            worksheet.Cells[1, 6].Value = "Ngày XND/Giao";
            worksheet.Cells[1, 7].Value = "Giờ XND/Giao";
            worksheet.Cells[1, 8].Value = "Ngày phát";   // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1     // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 9].Value = "Giờ phát";   // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1     // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[1, 10].Value = "Dịch vụ";   // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1     // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult ExportDetail_Item_Ems_TRUOT(int StartPoscode, int PostmanID, string StartDate, string EndDate, string IsService, int Istype)
        {
            ViewBag.StartPoscode = StartPoscode;
            ViewBag.PostmanID = PostmanID;
            // ViewBag.IsCod = IsCod;
            ViewBag.IsService = IsService;
            ViewBag.Istype = Istype;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            var nameBC = "";
            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail_Item_Ems_TRUOT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            switch (Istype)
            {
                case 0:
                    nameBC = "Báo cáo sản lượng trượt phát lần đầu giao bưu tá";
                    break;
                case 1:
                    nameBC = "Báo cáo sản lượng trượt phát thành công giao bưu tá ";
                    break;
                case 2:
                    nameBC = "Báo cáo sản lượng trượt phát lần đầu theo thời gian XND tại bưu cục ";
                    break;
                case 3:
                    nameBC = "Báo cáo sản lượng trượt phát thành công theo thời gian XND tại bưu cục ";
                    break;

            }
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameBC + " TRUOT" + ".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult Detail_PLD_XND()
        {
            return View();

        }

        public ActionResult Detail_PTC_XND()
        {
            return View();

        }

        public ActionResult Detail_PLD_BT()
        {
            return View();

        }

        public ActionResult Detail_PTC_BT()
        {
            return View();

        }



        [HttpPost]
        public JsonResult ListDetail_PLD_XND(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
          
            ViewBag.StartPoscode = StartPoscode;                  
            ViewBag.IsService = IsService;                
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnKPIDeliveryLOG returnquality = new ReturnKPIDeliveryLOG();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_PLD_XND(StartPoscode, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_PLD_XND> ReturnListExcelDetail_PLD_XND(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartPoscode;
            ViewBag.IsService = IsService;
           // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnKPIDeliveryLOG returnquality = new ReturnKPIDeliveryLOG();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_PLD_XND(StartPoscode, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListDetail_PLD_XNDReport;
        }

        public Stream CreateExcelFileDetail_PLD_XND(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail_PLD_XND(ViewBag.StartPoscode, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcelDetail_PLD_XND(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        


        //Phần sửa excel
        private void BindingFormatForExcelDetail_PLD_XND(ExcelWorksheet worksheet, List<Detail_PLD_XND> listItems)
        {
            var list = ReturnListExcelDetail_PLD_XND(ViewBag.StartPoscode, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header

            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT PLD BƯU CỤC";
            worksheet.Cells["A1:O1"].Merge = true;

            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO:CL_BCKHL/PLD_BC";
            worksheet.Cells["O2:O2"].Merge = true;

            worksheet.Cells[2, 7].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["G2:I2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Bưu cục";
            worksheet.Cells[4, 3].Value = "Tên bưu cục";
            worksheet.Cells[4, 4].Value = "Mã bưu tá";
            worksheet.Cells[4, 5].Value = "Tên bưu tá";
            worksheet.Cells[4, 6].Value = "Dịch vụ";
            worksheet.Cells[4, 7].Value = "Tổng số";
            worksheet.Cells[4, 8].Value = "Sản lượng phát thành công";
            worksheet.Cells[4, 9].Value = "Tỉ lệ phát thành công";
            worksheet.Cells[4, 10].Value = "Sản lượng đúng quy định";
            worksheet.Cells[4, 11].Value = "Tỷ lệ đúng quy định";
            worksheet.Cells[4, 12].Value = "Sản lượng không đúng quy định";
            worksheet.Cells[4, 13].Value = "Tỷ lệ không đúng quy định";
            worksheet.Cells[4, 14].Value = "Sản lượng KTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 15].Value = "Tỷ lệ KTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1     // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult ExportDetail_PLD_XND(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartPoscode = StartPoscode;
            ViewBag.IsService = IsService;
           // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail_PLD_XND();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo phát lần đầu XND.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        //----------------------------------------------------Detail_PTC_XND
        [HttpPost]
        public JsonResult ListDetail_PTC_XND(int StartPoscode, int IsService, string StartDate, string EndDate)
        {

            ViewBag.StartPoscode = StartPoscode;
            ViewBag.IsService = IsService;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnKPIDeliveryLOG returnquality = new ReturnKPIDeliveryLOG();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_PTC_XND(StartPoscode, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_PTC_XND> ReturnListExcelDetail_PTC_XND(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartPoscode;
            ViewBag.IsService = IsService;
            // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnKPIDeliveryLOG returnquality = new ReturnKPIDeliveryLOG();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_PTC_XND(StartPoscode, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListDetail_PTC_XNDReport;
        }

        public Stream CreateExcelFileDetail_PTC_XND(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail_PTC_XND(ViewBag.StartPoscode, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcelDetail_PTC_XND(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }




        //Phần sửa excel
        private void BindingFormatForExcelDetail_PTC_XND(ExcelWorksheet worksheet, List<Detail_PTC_XND> listItems)
        {
            var list = ReturnListExcelDetail_PTC_XND(ViewBag.StartPoscode, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT PTC BƯU CỤC";
            worksheet.Cells["A1:O1"].Merge = true;

            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO:CL_BCKHL/PTC_BC";
            worksheet.Cells["O2:O2"].Merge = true;

            worksheet.Cells[2, 7].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["G2:I2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Bưu cục";
            worksheet.Cells[4, 3].Value = "Tên bưu cục";
            worksheet.Cells[4, 4].Value = "Mã bưu tá";
            worksheet.Cells[4, 5].Value = "Tên bưu tá";
            worksheet.Cells[4, 6].Value = "Dịch vụ";
            worksheet.Cells[4, 7].Value = "Tổng số";
            worksheet.Cells[4, 8].Value = "Sản lượng phát thành công";
            worksheet.Cells[4, 9].Value = "Tỉ lệ phát thành công";
            worksheet.Cells[4, 10].Value = "Sản lượng đúng quy định";
            worksheet.Cells[4, 11].Value = "Tỷ lệ đúng quy định";
            worksheet.Cells[4, 12].Value = "Sản lượng không đúng quy định";
            worksheet.Cells[4, 13].Value = "Tỷ lệ không đúng quy định";
            worksheet.Cells[4, 14].Value = "Sản lượng KTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 15].Value = "Tỷ lệ KTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1     // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult ExportDetail_PTC_XND(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartPoscode = StartPoscode;
            ViewBag.IsService = IsService;
            // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail_PTC_XND();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo phát thành công XND.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

        //---------------------------------------------------
        [HttpPost]
        public JsonResult ListDetail_PLD_BT(int StartPoscode, int IsService, string StartDate, string EndDate)
        {

            ViewBag.StartPoscode = StartPoscode;
            ViewBag.IsService = IsService;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnKPIDeliveryLOG returnquality = new ReturnKPIDeliveryLOG();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_PLD_BT(StartPoscode, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_PLD_BT> ReturnListExcelDetail_PLD_BT(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartPoscode;
            ViewBag.IsService = IsService;
            // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnKPIDeliveryLOG returnquality = new ReturnKPIDeliveryLOG();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_PLD_BT(StartPoscode, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListDetail_PLD_BTReport;
        }

        public Stream CreateExcelFileDetail_PLD_BT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail_PLD_BT(ViewBag.StartPoscode, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcelDetail_PLD_BT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }




        //Phần sửa excel
        private void BindingFormatForExcelDetail_PLD_BT(ExcelWorksheet worksheet, List<Detail_PLD_BT> listItems)
        {
            var list = ReturnListExcelDetail_PLD_BT(ViewBag.StartPoscode, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);

            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT PLD BƯU TÁ";
            worksheet.Cells["A1:O1"].Merge = true;

            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO:CL_BCKHL/PLD_BT";
            worksheet.Cells["O2:O2"].Merge = true;

            worksheet.Cells[2, 7].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["G2:I2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Bưu cục";
            worksheet.Cells[4, 3].Value = "Tên bưu cục";
            worksheet.Cells[4, 4].Value = "Mã bưu tá";
            worksheet.Cells[4, 5].Value = "Tên bưu tá";
            worksheet.Cells[4, 6].Value = "Dịch vụ";
            worksheet.Cells[4, 7].Value = "Tổng số";
            worksheet.Cells[4, 8].Value = "Sản lượng phát thành công";
            worksheet.Cells[4, 9].Value = "Tỉ lệ phát thành công";
            worksheet.Cells[4, 10].Value = "Sản lượng đúng quy định";
            worksheet.Cells[4, 11].Value = "Tỷ lệ đúng quy định";
            worksheet.Cells[4, 12].Value = "Sản lượng không đúng quy định";
            worksheet.Cells[4, 13].Value = "Tỷ lệ không đúng quy định";
            worksheet.Cells[4, 14].Value = "Sản lượng KTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 15].Value = "Tỷ lệ KTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1     // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult ExportDetail_PLD_BT(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartPoscode = StartPoscode;
            ViewBag.IsService = IsService;
            // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail_PLD_BT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo phát lần đầu bưu tá.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }


        //-------------------------------------------------------
        [HttpPost]
        public JsonResult ListDetail_PTC_BT(int StartPoscode, int IsService, string StartDate, string EndDate)
        {

            ViewBag.StartPoscode = StartPoscode;
            ViewBag.IsService = IsService;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnKPIDeliveryLOG returnquality = new ReturnKPIDeliveryLOG();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_PTC_BT(StartPoscode, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<Detail_PTC_BT> ReturnListExcelDetail_PTC_BT(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartProvince = StartPoscode;
            ViewBag.IsService = IsService;
            // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            KPIDeliveryLOGRepository qualityKPIDeliveryLOGRepository = new KPIDeliveryLOGRepository();
            ReturnKPIDeliveryLOG returnquality = new ReturnKPIDeliveryLOG();
            returnquality = qualityKPIDeliveryLOGRepository.Detail_PTC_BT(StartPoscode, IsService, common.DateToInt(StartDate), common.DateToInt(EndDate));
            return returnquality.ListDetail_PTC_BTReport;
        }

        public Stream CreateExcelFileDetail_PTC_BT(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcelDetail_PTC_BT(ViewBag.StartPoscode, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);
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
                BindingFormatForExcelDetail_PTC_BT(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }




        //Phần sửa excel
        private void BindingFormatForExcelDetail_PTC_BT(ExcelWorksheet worksheet, List<Detail_PTC_BT> listItems)
        {
            var list = ReturnListExcelDetail_PTC_BT(ViewBag.StartPoscode, ViewBag.IsService, ViewBag.StartDate, ViewBag.EndDate);
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "BÁO CÁO CHẤT PTC BƯU TÁ";
            worksheet.Cells["A1:O1"].Merge = true;

            worksheet.Cells[2, 15].Value = "MÃ BÁO CÁO:CL_BCKHL/PTC_BT";
            worksheet.Cells["O2:O2"].Merge = true;

            worksheet.Cells[2, 7].Value = "Từ ngày:" + " " + ViewBag.StartDate + " " + "Đến ngày" + ViewBag.EndDate;
            worksheet.Cells["G2:I2"].Merge = true;

            worksheet.Cells[4, 1].Value = "STT";
            worksheet.Cells[4, 2].Value = "Bưu cục";
            worksheet.Cells[4, 3].Value = "Tên bưu cục";
            worksheet.Cells[4, 4].Value = "Mã bưu tá";
            worksheet.Cells[4, 5].Value = "Tên bưu tá";
            worksheet.Cells[4, 6].Value = "Dịch vụ";
            worksheet.Cells[4, 7].Value = "Tổng số";
            worksheet.Cells[4, 8].Value = "Sản lượng phát thành công";
            worksheet.Cells[4, 9].Value = "Tỉ lệ phát thành công";
            worksheet.Cells[4, 10].Value = "Sản lượng đúng quy định";
            worksheet.Cells[4, 11].Value = "Tỷ lệ đúng quy định";
            worksheet.Cells[4, 12].Value = "Sản lượng không đúng quy định";
            worksheet.Cells[4, 13].Value = "Tỷ lệ không đúng quy định";
            worksheet.Cells[4, 14].Value = "Sản lượng KTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            worksheet.Cells[4, 15].Value = "Tỷ lệ KTT";         // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1     // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
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
        public ActionResult ExportDetail_PTC_BT(int StartPoscode, int IsService, string StartDate, string EndDate)
        {
            ViewBag.StartPoscode = StartPoscode;
            ViewBag.IsService = IsService;
            // ViewBag.IsCod = IsCod;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            //ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFileDetail_PTC_BT();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo phát thành công bưu tá.xlsx");
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