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
    public class DealAndAccountFalseRepository
    {
        Convertion common = new Convertion();

        // Phần lấy dữ liệu DEAL_FALSE_DETAIL
        #region DEAL_FALSE_DETAIL          
        public ReturnDealAndAccountFalse DEAL_FALSE_DETAIL()
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnDealAndAccountFalse _ReturnDealAndAccountFalse = new ReturnDealAndAccountFalse();
            List<DealFalse_Detail> listDealFalseDetail = null;
            DealFalse_Detail oDealFalse_Detail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("management_sale.get_list_Crm_Deals", Helper.OraEVComOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDealFalseDetail = new List<DealFalse_Detail>();
                        while (dr.Read())
                        {
                            oDealFalse_Detail = new DealFalse_Detail();
                            oDealFalse_Detail.STT = a++;
                            oDealFalse_Detail.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            oDealFalse_Detail.account_Name = dr["account_Name"].ToString();
                            oDealFalse_Detail.COUNTID = dr["COUNTID"].ToString();
                            listDealFalseDetail.Add(oDealFalse_Detail);

                        }
                        _ReturnDealAndAccountFalse.Code = "00";
                        _ReturnDealAndAccountFalse.Message = "Lấy dữ liệu thành công.";
                        _ReturnDealAndAccountFalse.Total = listDealFalseDetail.Count();
                        _ReturnDealAndAccountFalse.ListDealFalse_Report = listDealFalseDetail;
                    }
                    else
                    {
                        _ReturnDealAndAccountFalse.Code = "01";
                        _ReturnDealAndAccountFalse.Message = "Không có dữ liệu";

                    }
                    

                }
            }
            catch (Exception ex)
            {
                _ReturnDealAndAccountFalse.Code = "99";
                _ReturnDealAndAccountFalse.Message = "Lỗi xử lý dữ liệu";
                _ReturnDealAndAccountFalse.Total = 0;
                _ReturnDealAndAccountFalse.ListDealFalse_Report = listDealFalseDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DealAndAccountFalseRepository.ACCOUNT_FALSE_DETAIL: " + ex.Message);
            }
            return _ReturnDealAndAccountFalse;
        }

        #endregion

        // Phần lấy dữ liệu ACCOUNT_FALSE_DETAIL
        #region ACCOUNT_FALSE_DETAIL          
        public ReturnDealAndAccountFalse ACCOUNT_FALSE_DETAIL()
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnDealAndAccountFalse _ReturnDealAndAccountFalse = new ReturnDealAndAccountFalse();
            List<AccountFalse_Detail> listAccountFalseDetail = null;
            AccountFalse_Detail oAccountFalse_Detail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("management_sale.get_list_Crm_Account", Helper.OraEVComOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAccountFalseDetail = new List<AccountFalse_Detail>();
                        while (dr.Read())
                        {
                            oAccountFalse_Detail = new AccountFalse_Detail();
                            oAccountFalse_Detail.STT = a++;
                            oAccountFalse_Detail.REFERENT_ACCOUNT = dr["REFERENT_ACCOUNT"].ToString();
                            oAccountFalse_Detail.COUNTID = dr["COUNTID"].ToString();
                            listAccountFalseDetail.Add(oAccountFalse_Detail);

                        }
                        _ReturnDealAndAccountFalse.Code = "00";
                        _ReturnDealAndAccountFalse.Message = "Lấy dữ liệu thành công.";
                        _ReturnDealAndAccountFalse.Total = listAccountFalseDetail.Count();
                        _ReturnDealAndAccountFalse.ListAccountFalse_Report = listAccountFalseDetail;
                    }
                    else
                    {
                        _ReturnDealAndAccountFalse.Code = "01";
                        _ReturnDealAndAccountFalse.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnDealAndAccountFalse.Code = "99";
                _ReturnDealAndAccountFalse.Message = "Lỗi xử lý dữ liệu";
                _ReturnDealAndAccountFalse.Total = 0;
                _ReturnDealAndAccountFalse.ListAccountFalse_Report = listAccountFalseDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DealAndAccountFalseRepository.ACCOUNT_FALSE_DETAIL: " + ex.Message);
            }
            return _ReturnDealAndAccountFalse;
        }

        #endregion
    }



}

