using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;

namespace T41.Areas.Admin.Data
{
    public class KPI_SL_Du_BaoRepository
    {
        public ReturnKPI_SL_Du_Bao KPI_SL_Du_Bao(int StartDate, int TT_KT)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_SL_Du_Bao _ReturnKPI_SL_Du_Bao = new ReturnKPI_SL_Du_Bao();
            List<KPI_SL_Du_Bao> listKPI_SL_Du_Bao = null;
            KPI_SL_Du_Bao oKPI_SL_Du_Bao = null;
            int a = 1;
            var test = Helper.OraDCDevOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DELIVERY_STATE_FORECAST.Detail_STATE_FORECAST_ToTal", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_TT_KT", OracleDbType.Int32).Value = TT_KT;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_SL_Du_Bao = new List<KPI_SL_Du_Bao>();
                        while (dr.Read())
                        {
                            oKPI_SL_Du_Bao = new KPI_SL_Du_Bao();
                            oKPI_SL_Du_Bao.STT = a++;
                            oKPI_SL_Du_Bao.Province = dr["Province"].ToString();
                            oKPI_SL_Du_Bao.ProvinceName = dr["ProvinceName"].ToString();
                            oKPI_SL_Du_Bao.TStui = dr["TStui"].ToString();
                            oKPI_SL_Du_Bao.TStui_KTlai = dr["TStui_KTlai"].ToString();
                            oKPI_SL_Du_Bao.TStui_QG = dr["TStui_QG"].ToString();
                            oKPI_SL_Du_Bao.TStui_DaDen = dr["TStui_DaDen"].ToString();
                            oKPI_SL_Du_Bao.TStui_DaKT = dr["TStui_DaKT"].ToString();
                            oKPI_SL_Du_Bao.TSTui_QG_DaDen = dr["TSTui_QG_DaDen"].ToString();
                            oKPI_SL_Du_Bao.TStui_QG_DaGiao = dr["TStui_QG_DaGiao"].ToString();
                            oKPI_SL_Du_Bao.TStui_DiDuong = dr["TStui_DiDuong"].ToString();
                            oKPI_SL_Du_Bao.TSbp = dr["TSbp"].ToString();
                            oKPI_SL_Du_Bao.TSbp_KTlai = dr["TSbp_KTlai"].ToString();
                            oKPI_SL_Du_Bao.TSbp_QG = dr["TSbp_QG"].ToString();
                            oKPI_SL_Du_Bao.TSbp_DaDen = dr["TSbp_DaDen"].ToString();
                            oKPI_SL_Du_Bao.TSbp_DaKT = dr["TSbp_DaKT"].ToString();
                            oKPI_SL_Du_Bao.TSbp_QG_DaKT = dr["TSbp_QG_DaKT"].ToString();
                            oKPI_SL_Du_Bao.TSbp_QG_DaGiao = dr["TSbp_QG_DaGiao"].ToString();
                            oKPI_SL_Du_Bao.TSbp_DiDuong = dr["TSbp_DiDuong"].ToString();
                            oKPI_SL_Du_Bao.Tkl = dr["Tkl"].ToString();
                            oKPI_SL_Du_Bao.Tkl_KTlai = dr["Tkl_KTlai"].ToString();
                            oKPI_SL_Du_Bao.Tkl_QG = dr["Tkl_QG"].ToString();
                            oKPI_SL_Du_Bao.Tkl_DaDen = dr["Tkl_DaDen"].ToString();
                            oKPI_SL_Du_Bao.Tkl_DaKT = dr["Tkl_DaKT"].ToString();
                            oKPI_SL_Du_Bao.Tkl_QG_DaDen = dr["Tkl_QG_DaDen"].ToString();
                            oKPI_SL_Du_Bao.Tkl_QG_DaGiao = dr["Tkl_QG_DaGiao"].ToString();
                            oKPI_SL_Du_Bao.Tkl_DiDuong = dr["Tkl_DiDuong"].ToString();
                            oKPI_SL_Du_Bao.Tkl_DiDuong = dr["Tkl_DiDuong"].ToString();
                            oKPI_SL_Du_Bao.DATE_LOG = dr["DATE_LOG"].ToString();
                            listKPI_SL_Du_Bao.Add(oKPI_SL_Du_Bao);
                        }
                        _ReturnKPI_SL_Du_Bao.Code = "00";
                        _ReturnKPI_SL_Du_Bao.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_SL_Du_Bao.ListKPI_SL_Du_Bao = listKPI_SL_Du_Bao;
                    }
                    else
                    {
                        _ReturnKPI_SL_Du_Bao.Code = "01";
                        _ReturnKPI_SL_Du_Bao.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_SL_Du_Bao.Code = "99";
                _ReturnKPI_SL_Du_Bao.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_SL_Du_Bao;
        }


        public ReturnKPI_SL_Du_Bao KPI_SL_Du_BaoTime(int StartDate, int TT_KT)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_SL_Du_Bao _ReturnKPI_SL_Du_Bao = new ReturnKPI_SL_Du_Bao();
            List<KPI_SL_Du_BaoTime> listKPI_SL_Du_Bao = null;
            KPI_SL_Du_BaoTime oKPI_SL_Du_Bao = null;
            int a = 1;
            var test = Helper.OraDCDevOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DELIVERY_STATE_FORECAST.TimeUpdate", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_TT_KT", OracleDbType.Int32).Value = TT_KT;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_SL_Du_Bao = new List<KPI_SL_Du_BaoTime>();
                        while (dr.Read())
                        {
                            oKPI_SL_Du_Bao = new KPI_SL_Du_BaoTime();
                           
                            oKPI_SL_Du_Bao.DATE_LOG = dr["DATE_LOG"].ToString();
                            listKPI_SL_Du_Bao.Add(oKPI_SL_Du_Bao);
                        }
                        _ReturnKPI_SL_Du_Bao.Code = "00";
                        _ReturnKPI_SL_Du_Bao.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_SL_Du_Bao.ListKPI_SL_Du_BaoTime = listKPI_SL_Du_Bao;
                    }
                    else
                    {
                        _ReturnKPI_SL_Du_Bao.Code = "01";
                        _ReturnKPI_SL_Du_Bao.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_SL_Du_Bao.Code = "99";
                _ReturnKPI_SL_Du_Bao.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_SL_Du_Bao;
        }


    }
}