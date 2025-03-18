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
    public class List_Item_BCCPRepository
    {

        public ReturnList_Item_BCCP List_Item_BCCP_HN(int startdate, int endpostcode, string search)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnList_Item_BCCP _ReturnList_Item_BCCP = new ReturnList_Item_BCCP();
            var test = Helper.OraDCComOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu. KPI_delivery_PICKUP.REPORT_List_Item_BCCP_TEST
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("List_Item_BCCP_HN", Helper.OraDCComOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_bckt", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_search", OracleDbType.Varchar2).Value = search;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listList_Item_BCCP = new List<List_Item_BCCP>();
                        while (dr.Read())
                        {
                            var oList_Item_BCCP = new List_Item_BCCP();
                            oList_Item_BCCP.STT = a++;
                            oList_Item_BCCP.Ma_BCKT = dr["Ma_BCKT"].ToString();
                            //oList_Item_BCCP.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oList_Item_BCCP.IDVNPOST = dr["IDVNPOST"].ToString();
                            oList_Item_BCCP.Ten_HT = dr["Ten_HT"].ToString();
                            oList_Item_BCCP.BC37CODE = dr["BC37CODE"].ToString();
                            oList_Item_BCCP.So_Tui = dr["So_Tui"].ToString();
                            oList_Item_BCCP.Khoi_Luong = dr["Khoi_Luong"].ToString();
                            oList_Item_BCCP.BC37CREATETIME = dr["BC37CREATETIME"].ToString();
                            oList_Item_BCCP.CONFIRMDATE = dr["CONFIRMDATE"].ToString();
                            listList_Item_BCCP.Add(oList_Item_BCCP);
                        }
                        _ReturnList_Item_BCCP.Code = "00";
                        _ReturnList_Item_BCCP.Message = "Lấy dữ liệu thành công.";
                        _ReturnList_Item_BCCP.ListList_Item_BCCP = listList_Item_BCCP;
                    }
                    else
                    {
                        _ReturnList_Item_BCCP.Code = "01";
                        _ReturnList_Item_BCCP.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnList_Item_BCCP.Code = "99";
                _ReturnList_Item_BCCP.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnList_Item_BCCP;
        }
        public ReturnList_Item_BCCP List_Item_BCCP_HCM(int startdate, int endpostcode, string search)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnList_Item_BCCP _ReturnList_Item_BCCP = new ReturnList_Item_BCCP();
            var test = Helper.OraDCComOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu. KPI_delivery_PICKUP.REPORT_List_Item_BCCP_TEST
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("List_Item_BCCP_HCM", Helper.OraDCComOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_bckt", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_search", OracleDbType.Varchar2).Value = search;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listList_Item_BCCP = new List<List_Item_BCCP>();
                        while (dr.Read())
                        {
                            var oList_Item_BCCP = new List_Item_BCCP();
                            oList_Item_BCCP.STT = a++;
                            oList_Item_BCCP.Ma_BCKT = dr["Ma_BCKT"].ToString();
                            //oList_Item_BCCP.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oList_Item_BCCP.IDVNPOST = dr["IDVNPOST"].ToString();
                            oList_Item_BCCP.Ten_HT = dr["Ten_HT"].ToString();
                            oList_Item_BCCP.BC37CODE = dr["BC37CODE"].ToString();
                            oList_Item_BCCP.So_Tui = dr["So_Tui"].ToString();
                            oList_Item_BCCP.Khoi_Luong = dr["Khoi_Luong"].ToString();
                            oList_Item_BCCP.BC37CREATETIME = dr["BC37CREATETIME"].ToString();
                            oList_Item_BCCP.CONFIRMDATE = dr["CONFIRMDATE"].ToString();
                            listList_Item_BCCP.Add(oList_Item_BCCP);
                        }
                        _ReturnList_Item_BCCP.Code = "00";
                        _ReturnList_Item_BCCP.Message = "Lấy dữ liệu thành công.";
                        _ReturnList_Item_BCCP.ListList_Item_BCCP = listList_Item_BCCP;
                    }
                    else
                    {
                        _ReturnList_Item_BCCP.Code = "01";
                        _ReturnList_Item_BCCP.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnList_Item_BCCP.Code = "99";
                _ReturnList_Item_BCCP.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnList_Item_BCCP;
        }
    }
}