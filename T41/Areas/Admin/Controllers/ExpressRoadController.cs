using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using OfficeOpenXml;
using System.IO;
using System.Data;



namespace T41.Areas.Admin.Controllers
{
    public class ExpressRoadController : Controller
    {
        // GET: Admin/Collect
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpressRoadDetailReport()
        {
            return View();
        }

        //Controller gọi đến chi tiết của bảng CHI TIET
        [HttpGet]
        public ActionResult ListDetailedExpressRoadReport()
        {
            ExpressRoadRepository expressroadRepository = new ExpressRoadRepository();
            ReturnExpressRoad returnexpressroad = new ReturnExpressRoad();
            returnexpressroad = expressroadRepository.EXPRESS_ROAD_DETAIL();
            return View(returnexpressroad);
        }

        private DataTable ReadFromExcelfile(string path, string sheetName)
        {
            // Khởi tạo data table
            DataTable dt = new DataTable();
            // Load file excel và các setting ban đầu
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                if (package.Workbook.Worksheets.Count < 1)
                {
                    // Log - Không có sheet nào tồn tại trong file excel của bạn
                    return null;
                }
                // Khởi Lấy Sheet đầu tiện trong file Excel để truy vấn, truyền vào name của Sheet để lấy ra sheet cần, nếu name = null thì lấy sheet đầu tiên
                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName) ?? package.Workbook.Worksheets.FirstOrDefault();
                // Đọc tất cả các header
                foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }
                // Đọc tất cả data bắt đầu từ row thứ 2
                for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                {
                    // Lấy 1 row trong excel để truy vấn
                    var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                    // tạo 1 row trong data table
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;

                    }
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }

        public ActionResult Hello()
        {
            var data = ReadFromExcelfile(@"D:\PROJECT_EMS\DemoImportExcel\ExcelDemo.xlsx", "First Sheet");
            List<ExpressRoadDetail> listExpressRoad = null;
            listExpressRoad = ConvertListToDataTable.DataTableToList<ExpressRoadDetail>(data);
            return View(listExpressRoad);
        }

        [HttpGet]
        public ActionResult ReadFromExcel()
        {
            var data = ReadFromExcelfile(@"D:\PROJECT_EMS\DemoImportExcel\SKU_5K.xlsx", "First Sheet");
            return View(data);
        }
    }
}