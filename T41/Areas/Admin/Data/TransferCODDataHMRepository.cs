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
using Oracle.ManagedDataAccess.Types;

namespace T41.Areas.Admin.Data
{
    public class TransferCODDataHMRepository
    {
        //Phần chi tiết 
        #region Payment_Total_HM_BY_DATE         
        public ReturnPaymentHMByDate Payment_Total_By_Date(string Date)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnPaymentHMByDate _Return = new ReturnPaymentHMByDate();
            int a = 0;
            List<PaymentHMTotalByDate> listPaymentHMTotalByDate = null;
            PaymentHMTotalByDate oPaymentHMTotalByDate = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("sync_status_partner_V2.GET_PAYMENT_HM_FROM_DATE", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_PaymentDate", OracleDbType.Int32).Value = common.DateToInt(Date);
                    myCommand.Parameters.Add(new OracleParameter("v_List", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listPaymentHMTotalByDate = new List<PaymentHMTotalByDate>();
                        while (dr.Read())
                        {
                            oPaymentHMTotalByDate = new PaymentHMTotalByDate();
                            oPaymentHMTotalByDate.StatementNumber = dr["STATEMENTNUMBER"].ToString();
                            oPaymentHMTotalByDate.TotalItem = dr["TOTALITEM"].ToString();
                            oPaymentHMTotalByDate.TransactionType = dr["TRANSACTIONTYPE"].ToString();
                            oPaymentHMTotalByDate.SettlementAmount = dr["SETTLEMENTAMOUNT"].ToString();
                            oPaymentHMTotalByDate.SettlementCurrency = dr["SETTLEMENTCURRENCY"].ToString();
                            oPaymentHMTotalByDate.PaymentDate = dr["PAYMENTDATE"].ToString();
                            oPaymentHMTotalByDate.Posted = dr["POSTED"].ToString();
                            oPaymentHMTotalByDate.Status = dr["STATUS"].ToString();
                            listPaymentHMTotalByDate.Add(oPaymentHMTotalByDate);

                        }
                        _Return.Code = "00";
                        _Return.Message = "Lấy dữ liệu thành công.";
                        _Return.listPaymentHMTotalByDate = listPaymentHMTotalByDate;
                    }
                    else
                    {
                        _Return.Code = "01";
                        _Return.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _Return.Code = "99";
                _Return.Message = "Lỗi xử lý dữ liệu";
            }
            return _Return;
        }



        #endregion


        #region Customer_BY_DATE          
        public ReturnCustomerInforHM Payment_Detail(int ID)
        {
            DataSet dataSet = new DataSet();
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnCustomerInforHM R_Infor = new ReturnCustomerInforHM();
            List<CustomerInforExcelHM> listCustomerInforHM = null;
            CustomerInforExcelHM oCustomerInforHM = null;
            int a = 0;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("sync_status_partner_V2.GET_LIST_DATA_PAYMENT_HM", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Id", OracleDbType.Int32).Value = ID;
                    myCommand.Parameters.Add(new OracleParameter("v_ListHeader", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    myCommand.Parameters.Add(new OracleParameter("v_ListItem", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    myCommand.Parameters.Add(new OracleParameter("v_Listtrailer", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    using (OracleDataAdapter adapter = new OracleDataAdapter(myCommand))
                    {

                        adapter.Fill(dataSet);
                        DataTable table1 = dataSet.Tables[0]; // first result set
                        DataTable table2 = dataSet.Tables[1]; // second result set
                        DataTable table3 = dataSet.Tables[2]; // third result set
                        DataTableReader dr = table2.CreateDataReader();
                        if (dr.HasRows)
                        {
                            listCustomerInforHM = new List<CustomerInforExcelHM>();
                            while (dr.Read())
                            {
                                oCustomerInforHM = new CustomerInforExcelHM();
                                oCustomerInforHM.StatementNumber = dr["STATEMENTNUMBER"].ToString();
                                oCustomerInforHM.SerialNumber = dr["SERIALNUMBER"].ToString();
                                oCustomerInforHM.transactionType = dr["TRANSACTIONTYPE"].ToString();
                                oCustomerInforHM.pspReference = dr["PSPREFERENCE"].ToString();
                                oCustomerInforHM.amountCaptured = dr["AMOUNTCAPTURED"].ToString();
                                oCustomerInforHM.captureTransactionCurrency = dr["CAPTURETRANSACTIONCURRENCY"].ToString();
                                oCustomerInforHM.settlementAmount = dr["SETTLEMENTAMOUNT"].ToString();
                                oCustomerInforHM.settlementCurrency = dr["SETTLEMENTCURRENCY"].ToString();
                                oCustomerInforHM.paymentDate = dr["PAYMENTDATE"].ToString();
                                oCustomerInforHM.uniqueReferenceNumber = dr["UNIQUEREFERENCENUMBER"].ToString();
                                oCustomerInforHM.manualMatchingReference = dr["MANUALMATCHINGREFERENCE"].ToString();
                                listCustomerInforHM.Add(oCustomerInforHM);
                            }
                            R_Infor.Code = "00";
                            R_Infor.Message = "Lấy dữ liệu thành công.";
                            R_Infor.listCustomerInforHM = listCustomerInforHM;
                        }else
                        {
                            R_Infor.Code = "01";
                            R_Infor.Message = "Không có dữ liệu";
                        }

                    }
                    
                }
            }
            catch (Exception ex)
            {
                R_Infor.Code = "99";
                R_Infor.Message = "Lỗi xử lý dữ liệu";

            }
            return R_Infor;
        }

        public ReturnConfirmCOD ConfirmCod(string ID)
        {

            ReturnConfirmCOD R_Update = new ReturnConfirmCOD();
            try
            {

                OracleCommand myCommand = new OracleCommand("sync_status_partner_V2.UPDATE_DATA_PAYMNET_HM", Helper.OraDCOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_ID", OracleDbType.Int32).Value = Convert.ToInt32(ID);
                myCommand.ExecuteNonQuery();
                R_Update.Code = "00";
                R_Update.Message = "Cập nhật dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_Update.Code = "01";
                R_Update.Message = ex.ToString();

            }

            return R_Update;


        }

        public ReturnConfirmCOD DeleteCod(string ID)
        {

            ReturnConfirmCOD R_Update = new ReturnConfirmCOD();
            try
            {

                OracleCommand myCommand = new OracleCommand("sync_status_partner_V2.UPDATE_DATA_PAYMENT_HM_DELETE", Helper.OraDCOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_ID", OracleDbType.Int32).Value = Convert.ToInt32(ID);
                myCommand.ExecuteNonQuery();
                R_Update.Code = "00";
                R_Update.Message = "Cập nhật dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_Update.Code = "01";
                R_Update.Message = ex.ToString();

            }

            return R_Update;


        }



        #endregion

        #region ImportExcel

        public ResponseImportExcelHM ImportExceltoDatabase(string strFilePath, string connString)
        {
            int Id = 0;
            Convertion cv = new Convertion();
            try
            {

                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("sync_status_partner_V2.Get_ID_Seq_HM", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    myCommand.Parameters.Add(new OracleParameter("p_output", OracleDbType.Int32)).Direction = ParameterDirection.Output;
                    myCommand.ExecuteNonQuery();
                    Id = Convert.ToInt32(myCommand.Parameters["p_output"].Value.ToString());
                }
            }
            catch(Exception ex)
            {

            }
            int i = 0;
            bool result = false;
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            oledbConn.Open();
            dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            oledbConn.Close();
            ResponseImportExcelHM R_Import = new ResponseImportExcelHM();
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
                    R_Import.Total = 0;
                    if (dt.Rows.Count > 0)
                    {
                        int SerialNumber = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            var test = row["SỐ BƯU GỬI/ Tracking ID"].ToString();
                            if (row["SỐ BƯU GỬI/ Tracking ID"].ToString().Length > 11)
                            {
                                R_Import.Total++;
                                try
                                {
                                    SerialNumber++;
                                    OracleCommand myCommand = new OracleCommand("sync_status_partner_V2.INSERT_DATA_KH_PAYMENT_HM", Helper.OraDCOracleConnection);
                                    myCommand.CommandType = CommandType.StoredProcedure;
                                    myCommand.CommandTimeout = 1200;
                                    myCommand.Parameters.Add("v_id", OracleDbType.Int32).Value = Id;
                                    myCommand.Parameters.Add("v_SerialNumber", OracleDbType.Int32).Value = SerialNumber;
                                    myCommand.Parameters.Add("v_PspReference", OracleDbType.Varchar2).Value = "";
                                    myCommand.Parameters.Add("v_AmountCaptured", OracleDbType.Varchar2).Value = "";
                                    myCommand.Parameters.Add("v_CaptureTransactionCurrency", OracleDbType.Varchar2).Value = "";
                                    myCommand.Parameters.Add("v_SettlementAmount", OracleDbType.Varchar2).Value = row["SỐ TIỀN / Amount according to tracking ID"].ToString().Trim();
                                    myCommand.Parameters.Add("v_PaymentDate", OracleDbType.Int32).Value = cv.DateToInt(row["NGÀY THU TIỀN/ Collection date"].ToString());
                                    myCommand.Parameters.Add("v_PostingDate", OracleDbType.Int32).Value = cv.DateToInt(row["NGÀY THU TIỀN/ Collection date"].ToString());
                                    myCommand.Parameters.Add("v_UniqueReferenceNumber", OracleDbType.Varchar2).Value = row["SỐ BƯU GỬI/ Tracking ID"].ToString().Trim();
                                    myCommand.Parameters.Add("v_ManualMatchingReference", OracleDbType.Varchar2).Value = row["Số tham chiếu/ Reference number"].ToString().Trim();
                                    myCommand.ExecuteNonQuery();
                                    i++;
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            
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

