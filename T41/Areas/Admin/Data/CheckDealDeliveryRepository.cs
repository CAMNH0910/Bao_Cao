using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class CheckDealDeliveryRepository
    {
        Convertion common = new Convertion();

        // Phần lấy dữ liệu CHECK_DEAL_DELIVERY_DETAIL
        #region CHECK_DEAL_DELIVERY_DETAIL          
        public ReturnCheckDealDelivery CHECK_DEAL_DELIVERY_DETAIL(int tu_ngay, int den_ngay, int ma_bc_khai_thac)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnCheckDealDelivery _ReturnCheckDealDelivery = new ReturnCheckDealDelivery();
            List<CheckDealDelivery_Detail> listCheckDealDeliveryDetail = null;
            
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("pk_telepost.V2_BAO_PHAT_DSOAT_WEB_BYNGAY", Helper.OraEVComDevOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_TU_NGAY", OracleDbType.Varchar2).Value = tu_ngay;
                    myCommand.Parameters.Add("v_DEN_NGAY", OracleDbType.Varchar2).Value = den_ngay;
                    myCommand.Parameters.Add("v_MA_BC_KHAI_THAC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add(new OracleParameter("v_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    listCheckDealDeliveryDetail = ConvertListToDataTable.DataTableToList<CheckDealDelivery_Detail>(da);
                    if (listCheckDealDeliveryDetail != null && listCheckDealDeliveryDetail.Count != 0)
                    {
                        _ReturnCheckDealDelivery.Code = "00";
                        _ReturnCheckDealDelivery.Message = "Lấy dữ liệu thành công.";
                        _ReturnCheckDealDelivery.CheckDealDelivery_Total = checkdealdeliverytotalTotal(tu_ngay,den_ngay,ma_bc_khai_thac);
                        _ReturnCheckDealDelivery.ListCheckDealDelivery_Report = listCheckDealDeliveryDetail;

                    }
                    
                    else
                    {
                        _ReturnCheckDealDelivery.Code = "01";
                        _ReturnCheckDealDelivery.Message = "Không có dữ liệu";
                        _ReturnCheckDealDelivery.CheckDealDelivery_Total = checkdealdeliverytotalTotal(tu_ngay, den_ngay, ma_bc_khai_thac);
                        _ReturnCheckDealDelivery.ListCheckDealDelivery_Report = listCheckDealDeliveryDetail;
                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnCheckDealDelivery.Code = "99";
                _ReturnCheckDealDelivery.Message = "Lỗi xử lý dữ liệu";
                _ReturnCheckDealDelivery.CheckDealDelivery_Total = checkdealdeliverytotalTotal(tu_ngay, den_ngay, ma_bc_khai_thac);
                _ReturnCheckDealDelivery.ListCheckDealDelivery_Report = listCheckDealDeliveryDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CheckDealDeliveryRepository.CHECK_DEAL_DELIVERY_DETAIL: " + ex.Message);
            }
            return _ReturnCheckDealDelivery;
        }

        #endregion

        #region CHECK_DEAL_DELIVERY_TOTAL
        protected CheckDealDelivery_TOTAL checkdealdeliverytotalTotal(int tu_ngay, int den_ngay, int ma_bc_khai_thac)
        {
            CheckDealDelivery_TOTAL checkdealdeliverytotal = null;
            using (OracleCommand cmd = new OracleCommand())
            
            {
                try
                {
                    cmd.Connection = Helper.OraEVComDevOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "pk_telepost.V2_BAO_PHAT_DSOAT_WEB_TONG";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_TU_NGAY", OracleDbType.Int32)).Value = tu_ngay;
                    cmd.Parameters.Add(new OracleParameter("v_DEN_NGAY", OracleDbType.Int32)).Value = den_ngay;
                    cmd.Parameters.Add(new OracleParameter("v_MA_BC_KHAI_THAC", OracleDbType.Int32)).Value = ma_bc_khai_thac;
                    cmd.Parameters.Add(new OracleParameter("v_TONG_SO_BAO_PHAT_TRUYEN", OracleDbType.Int32)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new OracleParameter("v_TONG_SO_MA_E1_TRUYEN", OracleDbType.Int32)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new OracleParameter("v_TONG_SO_BAO_PHAT_DOI_SOAT", OracleDbType.Int32)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new OracleParameter("v_TONG_SO_MA_E1_DOI_SOAT", OracleDbType.Int32)).Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add("v_TONG_SO_BAO_PHAT_TRUYEN", OracleDbType.Int32, 0, ParameterDirection.Output);
                    //cmd.Parameters.Add("v_TONG_SO_MA_E1_TRUYEN", OracleDbType.Int32, 0, ParameterDirection.Output);
                    //cmd.Parameters.Add("v_TONG_SO_BAO_PHAT_DOI_SOAT", OracleDbType.Int32, ParameterDirection.Output);
                    //cmd.Parameters.Add("v_TONG_SO_MA_E1_DOI_SOAT", OracleDbType.Int32, 0, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraEVComDevOracleConnection);

                    checkdealdeliverytotal = new CheckDealDelivery_TOTAL();

                    
                    checkdealdeliverytotal.TONG_SO_BAO_PHAT_TRUYEN = common.ConvertToInt(cmd.Parameters["v_TONG_SO_BAO_PHAT_TRUYEN"].Value);
                    checkdealdeliverytotal.TONG_SO_MA_E1_TRUYEN = common.ConvertToInt(cmd.Parameters["v_TONG_SO_MA_E1_TRUYEN"].Value);
                    checkdealdeliverytotal.TONG_SO_BAO_PHAT_DOI_SOAT = common.ConvertToInt(cmd.Parameters["v_TONG_SO_BAO_PHAT_DOI_SOAT"].Value);
                    checkdealdeliverytotal.TONG_SO_MA_E1_DOI_SOAT = common.ConvertToInt(cmd.Parameters["v_TONG_SO_MA_E1_DOI_SOAT"].Value);



                }
                catch (Exception ex)
                {
                    checkdealdeliverytotal.TONG_SO_BAO_PHAT_TRUYEN = 0;
                    checkdealdeliverytotal.TONG_SO_MA_E1_TRUYEN = 0;
                    checkdealdeliverytotal.TONG_SO_BAO_PHAT_DOI_SOAT = 0;
                    checkdealdeliverytotal.TONG_SO_MA_E1_DOI_SOAT = 0;

                    LogAPI.LogToFile(LogFileType.EXCEPTION, "CheckDealDeliveryRepository.CheckDealDelivery_TOTAL: " + ex.Message);
                }
                return checkdealdeliverytotal;
            }
            
        }
       
        #endregion
    }



}

