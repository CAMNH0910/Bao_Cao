using Oracle.ManagedDataAccess.Client;
using Remotion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class KPI_Detail_PTCRepository
    {

        public ReturnKPI_Detail_PTC KPI_Detail_PTC(int startdate, int enddate, int zone, int endpostcode)
        {
            ReturnKPI_Detail_PTC _ReturnDetail_CTTH = new ReturnKPI_Detail_PTC();
            List<KPI_Detail_PTC> listDetail_CTTH = new List<KPI_Detail_PTC>();

            try
            {
                OracleConnection conn = Helper.OraDCOracleConnection;

                using (OracleCommand cmd = new OracleCommand("camnh.KPI_Detail_Phat.GET_Bao_Cao_BC", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 20000;

                    // Input params
                    cmd.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    cmd.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    cmd.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    cmd.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;

                    // Output ref cursor
                    cmd.Parameters.Add("v_ListStage", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        int stt = 0;
                        while (dr.Read())
                        {
                            KPI_Detail_PTC detail = new KPI_Detail_PTC
                            {
                                STT = stt++,
                                zone = dr["ZONE"].ToString(),
                                STARTPOSTCODE = dr["STARTPOSTCODE"].ToString(),
                                STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString(),
                                SLD = Convert.ToInt32(dr["SLD"]),
                                PTC = Convert.ToInt32(dr["PTC"]),
                                TL_PTC = dr["TL_PTC"].ToString(),
                                PTC_LD = Convert.ToInt32(dr["PTC_LD"]),
                                TLPTC_LD = dr["TLPTC_LD"].ToString(),
                                PTC_TN_1 = Convert.ToInt32(dr["PTC_TN_1"]),
                                PTC_TN_2 = Convert.ToInt32(dr["PTC_TN_2"]),
                                Tong_PTC_TN = Convert.ToInt32(dr["Tong_PTC_TN"]),
                                TLTong_PTC_TN = dr["TLTong_PTC_TN"].ToString(),
                                KDQD = Convert.ToInt32(dr["KDQD"]),
                                PKTC = Convert.ToInt32(dr["PKTC"])
                            };
                            listDetail_CTTH.Add(detail);
                        }
                    }
                }

                if (listDetail_CTTH.Count > 0)
                {
                    _ReturnDetail_CTTH.Code = "00";
                    _ReturnDetail_CTTH.Message = "Lấy dữ liệu thành công.";
                    _ReturnDetail_CTTH.ListKPI_Detail_PTC = listDetail_CTTH;
                }
                else
                {
                    _ReturnDetail_CTTH.Code = "01";
                    _ReturnDetail_CTTH.Message = "Không có dữ liệu.";
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_CTTH.Code = "99";
                _ReturnDetail_CTTH.Message = $"Lỗi xử lý dữ liệu: {ex.Message}";
            }

            return _ReturnDetail_CTTH;
        }


        public ReturnKPI_Detail_PTC KPI_Detail_PTC_CT(int startdate, int enddate, int zone, int endpostcode,int types)
        {
            ReturnKPI_Detail_PTC _ReturnDetail_CTTH = new ReturnKPI_Detail_PTC();
            List<KPI_Detail_PTC_CT> listDetail_CTTH = new List<KPI_Detail_PTC_CT>();

            try
            {
                OracleConnection conn = Helper.OraDCOracleConnection;

                using (OracleCommand cmd = new OracleCommand("camnh.KPI_Detail_Phat.GET_Bao_Cao_CT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 20000;

                    // Input params
                    cmd.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    cmd.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    cmd.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    cmd.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    cmd.Parameters.Add("v_types", OracleDbType.Int32).Value = types;
                    // Output ref cursor
                    cmd.Parameters.Add("v_ListStage", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        int stt = 0;
                        while (dr.Read())
                        {
                            KPI_Detail_PTC_CT detail = new KPI_Detail_PTC_CT
                            {
                                STT = stt++,
                                Itemcode = dr["Itemcode"].ToString(),
                                STARTPOSTCODE = dr["STARTPOSTCODE"].ToString(),
                                STATUSDATETIME = dr["STATUSDATETIME"].ToString(),
                                C19 = dr["C19"].ToString(),
                                TIMEINTERVAL = dr["TIMEINTERVAL"].ToString(),

                            };
                            listDetail_CTTH.Add(detail);
                        }
                    }
                }

                if (listDetail_CTTH.Count > 0)
                {
                    _ReturnDetail_CTTH.Code = "00";
                    _ReturnDetail_CTTH.Message = "Lấy dữ liệu thành công.";
                    _ReturnDetail_CTTH.ListKPI_Detail_PTC_CT = listDetail_CTTH;
                }
                else
                {
                    _ReturnDetail_CTTH.Code = "01";
                    _ReturnDetail_CTTH.Message = "Không có dữ liệu.";
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_CTTH.Code = "99";
                _ReturnDetail_CTTH.Message = $"Lỗi xử lý dữ liệu: {ex.Message}";
            }

            return _ReturnDetail_CTTH;
        }
        public ReturnKPI_Detail_PTC KPI_Detail_PTC_BT(int startdate, int enddate, int zone, int endpostcode, int routecode, int postman)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Detail_PTC _ReturnDetail_CTTH = new ReturnKPI_Detail_PTC();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("camnh.KPI_Detail_Phat.GET_Bao_Cao_BT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Routecode", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("v_Postman", OracleDbType.Int32).Value = postman;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_CTTH = new List<KPI_Detail_PTC_BT>();
                        while (dr.Read())
                        {

                            var oDetail_CTTH = new KPI_Detail_PTC_BT();
                            oDetail_CTTH.STT = a++;
                            oDetail_CTTH.zone = dr["zone"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oDetail_CTTH.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oDetail_CTTH.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_CTTH.ROUTECODE = dr["ROUTECODE"].ToString();
                            oDetail_CTTH.ROUTECODENAME = dr["ROUTECODENAME"].ToString();
                            oDetail_CTTH.POSTMAN_HRM = dr["POSTMAN_HRM"].ToString();
                            oDetail_CTTH.POSTMAN_ID = dr["POSTMAN_ID"].ToString();
                            oDetail_CTTH.POSTMANName = dr["POSTMANName"].ToString();
                            oDetail_CTTH.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            oDetail_CTTH.SLD_TC_1 = dr["SLD_TC_1"].ToString();
                            oDetail_CTTH.SL_PTC_1 = dr["SL_PTC_1"].ToString();
                            oDetail_CTTH.TL_PTC_1 = dr["TL_PTC_1"].ToString();
                            oDetail_CTTH.SLD_TC_2 = dr["SLD_TC_2"].ToString();
                            oDetail_CTTH.SL_PTC_2 = dr["SL_PTC_2"].ToString();
                            oDetail_CTTH.TL_PTC_2 = dr["TL_PTC_2"].ToString();
                            oDetail_CTTH.SLD_TC_3 = dr["SLD_TC_3"].ToString();
                            oDetail_CTTH.SL_PTC_3 = dr["SL_PTC_3"].ToString();
                            oDetail_CTTH.TL_PTC_3 = dr["TL_PTC_3"].ToString();
                            listDetail_CTTH.Add(oDetail_CTTH);
                        }
                        _ReturnDetail_CTTH.Code = "00";
                        _ReturnDetail_CTTH.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetail_CTTH.ListKPI_Detail_PTC_BT = listDetail_CTTH;
                    }
                    else
                    {
                        _ReturnDetail_CTTH.Code = "01";
                        _ReturnDetail_CTTH.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_CTTH.Code = "99";
                _ReturnDetail_CTTH.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDetail_CTTH;
        }
    }
}