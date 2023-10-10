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
    public class TK_LDRepository
    {
        public ResponseDetail_TK_LD DETAIL_TK_LD(int endpostcode, int routecode, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ResponseDetail_TK_LD _return = new ResponseDetail_TK_LD();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_TK_LD", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_RouteCode", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_TK_LD = new List<Detail_TK_LD>();
                        while (dr.Read())
                        {

                            var oDetail_TK_LD = new Detail_TK_LD();
                            oDetail_TK_LD.STT = a++;
                            oDetail_TK_LD.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetail_TK_LD.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_TK_LD.RouteCode = dr["ROUTECODE"].ToString();
                            oDetail_TK_LD.RouteName = dr["ROUTENAME"].ToString();
                            oDetail_TK_LD.Manv = dr["MANV"].ToString();
                            oDetail_TK_LD.Postmanid = dr["POSTMANID"].ToString();
                            oDetail_TK_LD.PostmanName = dr["POSTMANNAME"].ToString();
                            oDetail_TK_LD.Total = dr["TOTAL"].ToString();


                            oDetail_TK_LD.Reason_Code = dr["REASON_CODE"].ToString();
                            oDetail_TK_LD.Reason_Name = dr["REASON_NAME"].ToString();
                            listDetail_TK_LD.Add(oDetail_TK_LD);
                        }
                        _return.Code = "00";
                        _return.Message = "Lấy dữ liệu thành công.";
                        _return.listDetail_TK_LDReport = listDetail_TK_LD;
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

        public ResponseDetail_TK_CT_LD DETAIL_TK_CT_LD(int startpostcode, int routecode, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ResponseDetail_TK_CT_LD _return = new ResponseDetail_TK_CT_LD();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_TK_CT_LD", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = startpostcode;
                    myCommand.Parameters.Add("v_RouteCode", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_TK_CT_LD = new List<Detail_TK_CT_LD>();
                        while (dr.Read())
                        {

                            var oDetail_TK_CT_LD = new Detail_TK_CT_LD();
                            oDetail_TK_CT_LD.STT = a++;
                            oDetail_TK_CT_LD.ItemCode = dr["ItemCode"].ToString();
                            oDetail_TK_CT_LD.StartPostCode = dr["StartPostCode"].ToString();
                            oDetail_TK_CT_LD.StartPostcodeName = dr["StartPostcodeName"].ToString();
                            oDetail_TK_CT_LD.RouteCode = dr["RouteCode"].ToString();
                            oDetail_TK_CT_LD.RouteName = dr["RouteName"].ToString();
                            oDetail_TK_CT_LD.POSTMANID = dr["POSTMANID"].ToString();
                            oDetail_TK_CT_LD.PostmanName = dr["PostmanName"].ToString();
                            oDetail_TK_CT_LD.StatusDateTime = dr["StatusDateTime"].ToString();
                            oDetail_TK_CT_LD.ReceiverPhone = dr["SenderPhone"].ToString();
                            oDetail_TK_CT_LD.ReceiverPhone = dr["RECEIVERPHONE"].ToString();
                            oDetail_TK_CT_LD.Reason_Code = dr["REASON_CODE"].ToString();
                            oDetail_TK_CT_LD.Reason_Name = dr["REASON_NAME"].ToString();
                            listDetail_TK_CT_LD.Add(oDetail_TK_CT_LD);
                        }
                        _return.Code = "00";
                        _return.Message = "Lấy dữ liệu thành công.";
                        _return.Data = listDetail_TK_CT_LD;
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