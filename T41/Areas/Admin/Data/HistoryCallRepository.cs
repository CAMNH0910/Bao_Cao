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
    public class HistoryCallRepository
    {
        public ReturnTHHistoryCall DETAIL_TH_HISTORYCALL(int Endpostcode, int routecode, int isservice, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnTHHistoryCall _return = new ReturnTHHistoryCall();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("GTGT_GateWay.ListTHCallHistory", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_PosCode", OracleDbType.Int32).Value = Endpostcode;
                    myCommand.Parameters.Add("v_RouterCode", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = isservice;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTHHistoryCall = new List<HistoryCall>();
                        while (dr.Read())
                        {

                            var oTHHistoryCall = new HistoryCall();
                            oTHHistoryCall.STT = a++;
                            oTHHistoryCall.TINHTP = dr["TINHTP"].ToString();
                            oTHHistoryCall.NGAY = dr["NGAY"].ToString();
                            oTHHistoryCall.MABC = dr["MABC"].ToString();
                            oTHHistoryCall.MATUYEN = dr["MATUYEN"].ToString();
                            oTHHistoryCall.TENBUUTA = dr["TENBUUTA"].ToString();
                            oTHHistoryCall.TONGSOBUUGUIDIPHAT = dr["TONGSOBUUGUIDIPHAT"].ToString();
                            oTHHistoryCall.SUMBGDINGDONGCOE1 = dr["SUMBGDINGDONGCOE1"].ToString();
                            oTHHistoryCall.TYLEGOIQUADINGDONG = dr["TYLEGOIQUADINGDONG"].ToString();
                            listTHHistoryCall.Add(oTHHistoryCall);
                        }
                        _return.Code = "00";
                        _return.Message = "Lấy dữ liệu thành công.";
                        _return.listTHHistoryCallReport = listTHHistoryCall;
                    }
                    else
                    {
                        _return.Code = "01";
                        _return.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _return.Code = "99";
                _return.Message = "Lỗi xử lý dữ liệu";

            }
            return _return;
        }

        public ReturnCTHistoryCall DETAIL_CT_HISTORYCALL(int Endpostcode, int routecode, int isservice, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnCTHistoryCall _return = new ReturnCTHistoryCall();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("GTGT_GateWay.ListCTCallHistory", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_PosCode", OracleDbType.Int32).Value = Endpostcode;
                    myCommand.Parameters.Add("v_RouterCode", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = isservice;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listCTHistoryCall = new List<DetailHistoryCall>();
                        while (dr.Read())
                        {

                            var oTHHistoryCall = new DetailHistoryCall();
                            oTHHistoryCall.STT = a++;
                            oTHHistoryCall.TINHTP = dr["TINHTP"].ToString();
                            oTHHistoryCall.MABC = dr["MABC"].ToString();
                            oTHHistoryCall.MATUYEN = dr["MATUYEN"].ToString();
                            oTHHistoryCall.MABT = dr["EMPLOYEE_ID"].ToString();
                            oTHHistoryCall.TENBT = dr["TENBT"].ToString();
                            oTHHistoryCall.MAE1 = dr["MAE1"].ToString();
                            oTHHistoryCall.SODTNGUOINHAN = dr["SODTNGUOINHAN"].ToString();
                            oTHHistoryCall.LUOTGOITRONGNGAY = dr["LUOTGOITRONGNGAY"].ToString();
                            oTHHistoryCall.TGBATDAUGOI = dr["THOIGIANBATDAUGOI"].ToString();
                            oTHHistoryCall.TGKETTHUCGOI = dr["THOIGIANKETTHUCGOI"].ToString();
                            oTHHistoryCall.TRANGTHAIGOI = dr["TRANGTHAIGOI"].ToString();
                            listCTHistoryCall.Add(oTHHistoryCall);
                        }
                        _return.Code = "00";
                        _return.Message = "Lấy dữ liệu thành công.";
                        _return.listCTHistoryCallReport = listCTHistoryCall;
                    }
                    else
                    {
                        _return.Code = "01";
                        _return.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _return.Code = "99";
                _return.Message = "Lỗi xử lý dữ liệu";

            }
            return _return;
        }
    }
}