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
using System.Data.OleDb;
using Oracle.ManagedDataAccess.Client;
using System.Data.Entity.Validation;
using LinqToExcel;
using System.Windows.Input;
using System.Data.SqlClient;

namespace T41.Areas.Admin.Controllers
{

    public class PrintBarcode_CGIController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery

        //Controller lấy dữ liệu bưu cục phát

        //Controller lấy dữ liệu tuyến phát


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrintBarcode(string E1Code)
        {
            PrintBarcode_CGIRepository Barcode_CGIRepository = new PrintBarcode_CGIRepository();
            ReturnCustomerInfor returnCustomer = new ReturnCustomerInfor();
            returnCustomer = Barcode_CGIRepository.Customer_DETAIL(E1Code);
            return View(returnCustomer);
        }


        public ActionResult GetCustomerInforByDate(string date)
        {
            PrintBarcode_CGIRepository Barcode_CGIRepository = new PrintBarcode_CGIRepository();
            ReturnCustomerInfor returnCustomer = new ReturnCustomerInfor();
            returnCustomer = Barcode_CGIRepository.Customer_By_Date(date);
            return View(returnCustomer);
        }



        public ActionResult BarcodeInsert()
        {
            return View();
        }





        [HttpPost]
        public JsonResult ExcelToDatabase()
        {
            List<string> data = new List<string>();
            PrintBarcode_CGIRepository Barcode_CGIRepository = new PrintBarcode_CGIRepository();
            ResponseImportExcel R_Import = new ResponseImportExcel();
            if (Request.Files.Count > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files[0].FileName).ToLower();
                string query = null;
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Areas/Doc/"), Request.Files[0].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Areas/Doc/"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files[0].SaveAs(path1);

                    //Connection String to Excel Workbook
                    if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        
                    }
                }
                else
                {
                    data.Add("Only Excel file format is allowed");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                return Json(Barcode_CGIRepository.ImportExceltoDatabase(path1, connString), JsonRequestBehavior.AllowGet);
            }
            else
            {
                data.Add("Bạn chưa thêm file excel import vào");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

    }

}