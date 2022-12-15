using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace T41.Areas.Admin.Data
{
    public class PrintBarcode_CGIRepository
    {
          //Phần chi tiết 
        #region Customer_DETAIL          
        public ReturnCustomerInfor Customer_DETAIL(string E1Code)
        {
            CustomerInfor  oCustomerInfor =  new CustomerInfor();
            ReturnCustomerInfor R_Infor = new ReturnCustomerInfor();
            try
            {

                SqlCommand myCommand = new SqlCommand(@"dbo.[Get_E1_GSS]", Helper.SqlEMSConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = E1Code;
                SqlDataReader dr = myCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        oCustomerInfor = new CustomerInfor();
                        oCustomerInfor.STT = 1;
                        oCustomerInfor.E1Code = dr["Ma_E1"].ToString();
                        oCustomerInfor.DeliveryDate = dr["Ngay_Gui"].ToString();
                        oCustomerInfor.CustomerName = dr["Nguoi_Nhan"].ToString();
                        oCustomerInfor.Phone = dr["Dien_Thoai_Nhan"].ToString();
                        oCustomerInfor.Company = dr["Cong_Ty"].ToString();
                        oCustomerInfor.Province = dr["Tinh_Thanh"].ToString();
                        oCustomerInfor.VisaClass = dr["Visa_Class"].ToString();
                        oCustomerInfor.EntryTime = dr["Schedule_Entry_Time"].ToString();
                        oCustomerInfor.DS160 = dr["Contact_DS160"].ToString();
                        oCustomerInfor.PriorityName = dr["Priority_Name"].ToString();
                        oCustomerInfor.DeliveryOption = dr["DeliveryOption"].ToString();
                        R_Infor.CustomerInfor = oCustomerInfor;                
                        R_Infor.Code = "00";
                        R_Infor.Message = "Thành công";
                    }
                }
                else
                {
                    R_Infor.Code = "01";
                    R_Infor.Message = "Không có dữ liệu";
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                R_Infor.Code = "99";
                R_Infor.Message = ex.ToString();

            }

            return R_Infor;
        }



        #endregion


        #region Customer_BY_DATE          
        public ReturnCustomerInfor Customer_By_Date(string date)
        {

            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnCustomerInfor R_Infor = new ReturnCustomerInfor();
            List<CustomerInfor> listCustomerInfor = null;
            CustomerInfor oCustomerInfor = null;
            try
            {

                SqlCommand myCommand = new SqlCommand(@"dbo.[Get_E1_GSS_BY_DATE]", Helper.SqlEMSConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter mAdapter = new SqlDataAdapter();
                myCommand.Parameters.Add("@Ngay_Gui", SqlDbType.VarChar).Value = common.DateToInt(date);
                mAdapter = new SqlDataAdapter(myCommand);
                mAdapter.Fill(da);
                DataTableReader dr = da.CreateDataReader();
                int i = 1;
                if (dr.HasRows)
                {
                    listCustomerInfor = new List<CustomerInfor>();
                    while (dr.Read())
                    {
                        oCustomerInfor = new CustomerInfor();
                        oCustomerInfor.STT = i++;
                        oCustomerInfor.E1Code = dr["Ma_E1"].ToString();
                        oCustomerInfor.DeliveryDate = dr["Ngay_Gui"].ToString();
                        oCustomerInfor.CustomerName = dr["Nguoi_Nhan"].ToString();
                        oCustomerInfor.Phone = dr["Dien_Thoai_Nhan"].ToString();
                        oCustomerInfor.Company = dr["Cong_Ty"].ToString();
                        oCustomerInfor.Province = dr["Tinh_Thanh"].ToString();
                        oCustomerInfor.VisaClass = dr["Visa_Class"].ToString();
                        oCustomerInfor.EntryTime = dr["Schedule_Entry_Time"].ToString();
                        oCustomerInfor.DS160 = dr["Contact_DS160"].ToString();
                        oCustomerInfor.PriorityName = dr["Priority_Name"].ToString();
                        oCustomerInfor.DeliveryOption = dr["DeliveryOption"].ToString();
                        listCustomerInfor.Add(oCustomerInfor);
                    }
                    R_Infor.Code = "00";
                    R_Infor.Message = "Lấy dữ liệu thành công";
                    R_Infor.listCustomerInfor = listCustomerInfor;
                }
                else
                {
                    R_Infor.Code = "01";
                    R_Infor.Message = "Không có dữ liệu";
                    R_Infor.listCustomerInfor = null;
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                R_Infor.Code = "99";
                R_Infor.Message = ex.ToString();

            }

            return R_Infor;
        }



        #endregion

        #region ImportExcel

        public ResponseImportExcel ImportExceltoDatabase(string strFilePath, string connString)
        {
            int i = 0;
            bool result = false;
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            oledbConn.Open();
            dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            oledbConn.Close();
            ResponseImportExcel R_Import = new ResponseImportExcel();
            try
            {
                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + dt.Rows[0]["TABLE_NAME"].ToString() + "]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                    R_Import.Total = ds.Tables[0].Rows.Count;

                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            var Entrytime = row["Schedule Entry: Schedule Entry Time"].ToString().Replace(":","");
                            SqlCommand myCommand = new SqlCommand(@"dbo.[E1_GSS_STICKER]", Helper.SqlEMSConnection);
                            myCommand.CommandType = CommandType.StoredProcedure;
                            myCommand.Parameters.Add("@Ma_E1", SqlDbType.VarChar,13).Value = row["Contact: Passport Number"].ToString();
                            myCommand.Parameters.Add("@Cong_Ty", SqlDbType.VarChar,200).Value = row["Contact: UID"].ToString();
                            myCommand.Parameters.Add("@Visa_Class", SqlDbType.VarChar,50).Value = row["Visa Class: Code"].ToString();
                            myCommand.Parameters.Add("@Schedule_Entry_Time", SqlDbType.Int).Value = Int32.Parse(Entrytime);
                            myCommand.Parameters.Add("@Contact_DS160", SqlDbType.VarChar,50).Value = row["Contact: DS 160 Confirmation Number"].ToString();
                            myCommand.Parameters.Add("@Priority_Name", SqlDbType.VarChar,50).Value = row["Priority Name"].ToString();
                            myCommand.Parameters.Add("@Dia_Chi_Nhan", SqlDbType.VarChar,200).Value = row["Contact: Mailing Address"].ToString();
                            myCommand.Parameters.Add("@Tinh_Thanh", SqlDbType.VarChar,100).Value = row["Contact: Mailing City"].ToString();
                            myCommand.Parameters.Add("@Nguoi_Nhan", SqlDbType.VarChar,100).Value = row["Contact: Full Name"].ToString();
                            myCommand.Parameters.Add("@Dien_Thoai_Nhan", SqlDbType.VarChar,100).Value = row["Contact: Mobile"].ToString();
                            myCommand.ExecuteNonQuery();
                            i++;
                            
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                R_Import.Code = "99";
                R_Import.Message = ex.ToString();
            }
            finally
            {
                R_Import.Code = "00";
                R_Import.Success = i;
                R_Import.Message = "Import dữ liệu thành công " + i + "/" + R_Import.Total + "";
                oledbConn.Close();
            }
            return R_Import;
        }
        #endregion
    }

}

