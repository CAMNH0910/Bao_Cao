using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Entity.Validation;
using LinqToExcel;
using Oracle.ManagedDataAccess.Client;

namespace T41.Areas.Admin.Controllers
{

    public class DeliveryImportController : Controller
    {

        public JsonResult PosCode(int status)
        {
            DeliveryImportRepository DeliveryImportRepository = new DeliveryImportRepository();
            return Json(DeliveryImportRepository.GetAllPOSCODE(status), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertE1NH(string Itemcode, int Poscode, int Status, int DeliveryDate, string DeliveryTime, string Consignee, string Note)
        {

            DeliveryImportRepository DeliveryImportRepository = new DeliveryImportRepository();
            return Json(DeliveryImportRepository.InsertE1NHRepsitory(Itemcode, Poscode, Status, DeliveryDate, DeliveryTime, Consignee, Note), JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }

        public ActionResult DeliveryImport()
        {
            return View();

        }

        public ActionResult DeliveryInsert()
        {
            return View();

        }

        public class Delivery
        {
            public string NGAY { get; set; }
            public string GIO { get; set; }
            public string MAE1 { get; set; }
            public string BUUCUC { get; set; }
            public string NGUOINHAN { get; set; }
            public string GHICHU { get; set; }


        }
        [HttpPost]
        public JsonResult UploadExcel(Delivery delivery, HttpPostedFileBase FileUpload)
        {
            int i = 0;
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Areas/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<Delivery>(sheetName) select a;
                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.NGAY != null && a.MAE1 != null && a.BUUCUC != null && a.NGUOINHAN != null && a.GIO != null)
                            {
                                Delivery TU = new Delivery();
                                TU.NGAY = a.NGAY;
                                TU.GIO = a.GIO;
                                TU.MAE1 = a.MAE1;
                                TU.BUUCUC = a.BUUCUC;
                                TU.NGUOINHAN = a.NGUOINHAN;
                                TU.GHICHU = a.GHICHU;

                                using (OracleCommand cmd = new OracleCommand())
                                {
                                    //cmd.Connection = Helper.OraDCOracleConnection;
                                    //cmd.CommandText = Helper.SchemaName + "Ems_Bccp_Delivery.Insert_E1NH";
                                    //cmd.CommandType = CommandType.StoredProcedure;
                                    //cmd.Parameters.Add(new OracleParameter("v_postcode", OracleDbType.Int32)).Value = Int32.Parse(a.BUUCUC);
                                    //cmd.Parameters.Add(new OracleParameter("v_itemcode", OracleDbType.NVarchar2)).Value = a.MAE1;
                                    //cmd.Parameters.Add(new OracleParameter("v_ngaynhan", OracleDbType.Int32)).Value = Int32.Parse(a.NGAY);
                                    //cmd.Parameters.Add(new OracleParameter("v_ngnhan", OracleDbType.NVarchar2)).Value = a.NGUOINHAN;
                                    //cmd.ExecuteNonQuery();
                                    string UDP_Detail = " INSERT INTO e1nh_2015(mae1, mabctra,ngaynhan,gionhan,ngnhan,bccp,ghi_chu) VALUES('"+a.MAE1+"','"+a.BUUCUC+"','"+a.NGAY+"','"+a.GIO+"','"+a.NGUOINHAN+"',0,'"+a.GHICHU+"') ";
                                    OracleCommand myCommand_up = new OracleCommand(UDP_Detail, Helper.OraDCOracleConnection);
                                    myCommand_up.ExecuteNonQuery();
                                    i++;
                                }
                                }
                            else
                            {
                                //data.Add("Dữ liệu không đúng");                                                            
                                //return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }
                    //deleting excel file from folder
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("Thành công: "+ i, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format
                 
                    data.Add("Only Excel file format is allowed");                
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

    }
}