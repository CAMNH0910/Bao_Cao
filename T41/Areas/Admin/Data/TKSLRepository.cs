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
    public class TKSLRepository
    {
        public ResponseDetail_TKSL DETAIL_TKSL(string StartDate,string EndDate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ResponseDetail_TKSL _return = new ResponseDetail_TKSL();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_TKSL", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = common.DateToInt(StartDate);
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = common.DateToInt(EndDate); ;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        var listDetail_TKSL = new List<Detail_TKSL>();
                        while (dr.Read())
                        {
                            var oDetail_TKSL = new Detail_TKSL();
                            oDetail_TKSL.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetail_TKSL.StartPostName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_TKSL.EndPostCode = dr["ENDPOSTCODE"].ToString();
                            oDetail_TKSL.EndPostName = dr["ENDPOSTCODENAME"].ToString();
                            oDetail_TKSL.Tong_SL = dr["TONG_SL"].ToString();
                            oDetail_TKSL.TS_SMP = dr["TS_SMP"].ToString();
                            oDetail_TKSL.TL_SMP = dr["TL_SMP"].ToString();
                            oDetail_TKSL.TS_NOTSMP = dr["TS_NOTSMP"].ToString();
                            oDetail_TKSL.TL_NOTSMP = dr["TL_NOTSMP"].ToString();
                            
                            listDetail_TKSL.Add(oDetail_TKSL);
                        }
                        _return.Code = "00";
                        _return.Message = "Lấy dữ liệu thành công.";
                        _return.listDetail_TKSLReport = listDetail_TKSL;
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
        public ResponseDetail_CT_TKSL DETAIL_CT_TKSL(int StartPostCode, int EndPostCode,string StartDate, string EndDate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ResponseDetail_CT_TKSL _return = new ResponseDetail_CT_TKSL();
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_CT_TKSL", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = StartPostCode;
                    myCommand.Parameters.Add("v_EndPostCode", OracleDbType.Int32).Value = EndPostCode;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = common.DateToInt(StartDate);
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = common.DateToInt(EndDate); ;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        var listDetail_CT_TKSL = new List<Detail_CT_TKSL>();
                        while (dr.Read())
                        {
                            var oDetail_CT_TKSL = new Detail_CT_TKSL();
                            oDetail_CT_TKSL.ItemCode = dr["ITEMCODE"].ToString();
                            oDetail_CT_TKSL.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetail_CT_TKSL.StartPostName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_CT_TKSL.EndPostCode = dr["ENDPOSTCODE"].ToString();
                            oDetail_CT_TKSL.EndPostName = dr["ENDPOSTCODENAME"].ToString();
                            oDetail_CT_TKSL.TG = dr["TG"].ToString();
    

                            listDetail_CT_TKSL.Add(oDetail_CT_TKSL);
                        }
                        _return.Code = "00";
                        _return.Message = "Lấy dữ liệu thành công.";
                        _return.listDetail_CT_TKSLReport = listDetail_CT_TKSL;
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