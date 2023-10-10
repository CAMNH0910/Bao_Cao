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
    public class TKSLPRepository
    {
        public ResponseDetail_TKSLP DETAIL_TKSLP(int postmanid, int endpostcode, int routecode, int startdate, int enddate, int service)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ResponseDetail_TKSLP _return = new ResponseDetail_TKSLP();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_TKSLP", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_EndPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_routecode", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_PostmanID", OracleDbType.Int32).Value = postmanid;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_TKSLP = new List<Detail_TKSLP>();
                        while (dr.Read())
                        {

                            var oDetail_TKSLP = new Detail_TKSLP();
                            oDetail_TKSLP.STT = a++;
                            oDetail_TKSLP.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetail_TKSLP.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_TKSLP.RouteCode = dr["ROUTECODE"].ToString();
                            oDetail_TKSLP.RouteName = dr["ROUTENAME"].ToString();
                            oDetail_TKSLP.Manv = dr["MANV"].ToString();
                            oDetail_TKSLP.Postmanid = dr["POSTMANID"].ToString();
                            oDetail_TKSLP.PostmanName = dr["POSTMANNAME"].ToString();
                            oDetail_TKSLP.ServiceType = dr["SERVICETYPE"].ToString();
                            oDetail_TKSLP.SLD = dr["SLD"].ToString();
                            oDetail_TKSLP.SLPTC = dr["SLPTC"].ToString();
                            oDetail_TKSLP.SLPTC72H = dr["SLPTC72H"].ToString();
                            oDetail_TKSLP.SLDQD2KG = dr["SLDQD2KG"].ToString();
                            oDetail_TKSLP.KLDQD2KG = dr["KLDQD2KG"].ToString();
                            oDetail_TKSLP.SLDQDT2KG = dr["SLDQDT2KG"].ToString();
                            oDetail_TKSLP.KLDQDT2KG = dr["KLDQDT2KG"].ToString();
                            oDetail_TKSLP.SLDQD = dr["SLDQD"].ToString();
                            oDetail_TKSLP.KLDQD = dr["KLDQD"].ToString();
                            oDetail_TKSLP.SLKTC2KG = dr["SLKTC2KG"].ToString();
                            oDetail_TKSLP.KLKTC2KG = dr["KLKTC2KG"].ToString();
                            oDetail_TKSLP.SLKTCT2KG = dr["SLKTCT2KG"].ToString();
                            oDetail_TKSLP.KLKTCT2KG = dr["KLKTCT2KG"].ToString();
                            oDetail_TKSLP.SLKTC = dr["SLKTC"].ToString();
                            oDetail_TKSLP.KLKTC = dr["KLKTC"].ToString();
                            oDetail_TKSLP.SLCHUAPHAT = dr["SLCHUAPHAT"].ToString();
                            listDetail_TKSLP.Add(oDetail_TKSLP);
                        }
                        _return.Code = "00";
                        _return.Message = "Lấy dữ liệu thành công.";
                        _return.listDetail_TKSLPReport = listDetail_TKSLP;
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