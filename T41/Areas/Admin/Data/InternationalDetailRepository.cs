using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class InternationalDetailRepository
    {
        #region GETMANC
        //Lấy mã nước nhận qua bảng manc
        public String GetCountryCode()
        {
            string LISTMANC = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM MANC ORDER BY MANC ASC");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTMANC += "<option value='" + dr["MANC"].ToString() + "'>" + dr["MANC"].ToString() + '-' + dr["TENNC"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "InternationalDetailRepository.GETMANC" + ex.Message);
            }

            return LISTMANC;
        }
        #endregion

        // Phần lấy dữ liệu CHECK_DEAL_TRANSFER_DETAIL
        #region INTERNATIONAL_DETAIL          
        public ReturnInternationalDetail INTERNATIONAL_DETAIL(int fromdate, int todate, string countrycode)
        {
            int STT = 1;
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnInternationalDetail _ReturnInternationalDetail = new ReturnInternationalDetail();
            List<INTERNATIONAL_DETAIL> listInternationalDetail = null;
            INTERNATIONAL_DETAIL oInternationalDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("edi_monitor.Get_EDI_Outbound_Overview_Day", Helper.OraDCOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("p_TuNgay", OracleDbType.Int32).Value = fromdate;
                    myCommand.Parameters.Add("p_DenNgay", OracleDbType.Int32).Value = todate;
                    myCommand.Parameters.Add("v_mancnhan", OracleDbType.Varchar2).Value = countrycode;
                    myCommand.Parameters.Add(new OracleParameter("p_ResultSet", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listInternationalDetail = new List<INTERNATIONAL_DETAIL>();
                        while (dr.Read())
                        {
                            oInternationalDetail = new INTERNATIONAL_DETAIL();
                            oInternationalDetail.STT = STT++;
                            oInternationalDetail.MAE1 = dr["MAE1"].ToString();
                            oInternationalDetail.EDI_CODE = dr["EDI_CODE"].ToString();
                            oInternationalDetail.D_OE = dr["D_OE"].ToString();
                            oInternationalDetail.EMD = dr["EMD"].ToString();
                            oInternationalDetail.EMD_TRAN = dr["EMD_TRAN"].ToString();
                            oInternationalDetail.DELI = dr["DELI"].ToString();
                            oInternationalDetail.EME = dr["EME"].ToString();

                            oInternationalDetail.EME_TRAN = dr["EME_TRAN"].ToString();
                            oInternationalDetail.EDB = dr["EDB"].ToString();
                            oInternationalDetail.EDB_TRAN = dr["EDB_TRAN"].ToString();
                            oInternationalDetail.EDC = dr["EDC"].ToString();
                            oInternationalDetail.EDC_TRAN = dr["EDC_TRAN"].ToString();
                            oInternationalDetail.EMF = dr["EMF"].ToString();
                            oInternationalDetail.EMF_TRAN = dr["EMF_TRAN"].ToString();

                            oInternationalDetail.EMH = dr["EMH"].ToString();

                            oInternationalDetail.EMH_TRAN = dr["EMH_TRAN"].ToString();
                            oInternationalDetail.EMI = dr["EMI"].ToString();
                            oInternationalDetail.EMI_TRAN = dr["EMI_TRAN"].ToString();

                            listInternationalDetail.Add(oInternationalDetail);

                        }
                        _ReturnInternationalDetail.Code = "00";
                        _ReturnInternationalDetail.Message = "Lấy dữ liệu thành công.";
                        _ReturnInternationalDetail.ListInternational_Detail_Report = listInternationalDetail;
                    }
                    else
                    {
                        _ReturnInternationalDetail.Code = "01";
                        _ReturnInternationalDetail.Message = "Không có dữ liệu";
                        _ReturnInternationalDetail.ListInternational_Detail_Report = listInternationalDetail;
                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnInternationalDetail.Code = "99";
                _ReturnInternationalDetail.Message = "Lỗi xử lý dữ liệu";
                _ReturnInternationalDetail.ListInternational_Detail_Report = listInternationalDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "InternationalDetailRepository.INTERNATIONAL_DETAIL" + ex.Message);
            }
            return _ReturnInternationalDetail;
        }

        #endregion
    }
}