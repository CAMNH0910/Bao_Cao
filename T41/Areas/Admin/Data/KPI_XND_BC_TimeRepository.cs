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
    public class KPI_XND_BC_TimeRepository
    {
        public ReturnKPI_XND_BC_Time KPI_XND_BC_Time_DETAIL(int startdate, int enddate, int zone, int endpostcode, int startime, int endtime)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_XND_BC_Time _ReturnKPI_XND_BC_Time = new ReturnKPI_XND_BC_Time();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_delivery_postman_new.Get_Total_SL_TIME", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_StartTime", OracleDbType.Int32).Value = startime;
                    myCommand.Parameters.Add("v_EndTime", OracleDbType.Int32).Value = endtime;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listKPI_XND_BC_Time = new List<KPI_XND_BC_Time>();
                        while (dr.Read())
                        {

                            var oKPI_XND_BC_Time = new KPI_XND_BC_Time();
                            oKPI_XND_BC_Time.STT = a++;
                            oKPI_XND_BC_Time.ZONE = dr["ZONE"].ToString();
                            oKPI_XND_BC_Time.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oKPI_XND_BC_Time.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oKPI_XND_BC_Time.Total = dr["Total"].ToString();
                            oKPI_XND_BC_Time.XeMay_HT = dr["XeMay_HT"].ToString();
                            oKPI_XND_BC_Time.OTo_HT = dr["OTo_HT"].ToString();
                            oKPI_XND_BC_Time.XeMay_QT = dr["XeMay_QT"].ToString();
                            oKPI_XND_BC_Time.OTo_QT = dr["OTo_QT"].ToString();
                            oKPI_XND_BC_Time.XeMay_VISA = dr["XeMay_VISA"].ToString();
                            oKPI_XND_BC_Time.OTo_VISA = dr["OTo_VISA"].ToString();
                            oKPI_XND_BC_Time.XeMay_DG = dr["XeMay_DG"].ToString();
                            oKPI_XND_BC_Time.OTo_DG = dr["OTo_DG"].ToString();
                            oKPI_XND_BC_Time.XeMay_COD = dr["XeMay_COD"].ToString();
                            oKPI_XND_BC_Time.OTo_COD = dr["OTo_COD"].ToString();
                            oKPI_XND_BC_Time.XeMay_KCOD = dr["XeMay_KCOD"].ToString();
                            oKPI_XND_BC_Time.OTo_KCOD = dr["OTo_KCOD"].ToString();
                            oKPI_XND_BC_Time.XeMay_DAILY = dr["XeMay_DAILY"].ToString();
                            oKPI_XND_BC_Time.OTo_DAILY = dr["OTo_DAILY"].ToString();
                            oKPI_XND_BC_Time.XeMay_TTC = dr["XeMay_TTC"].ToString();
                            oKPI_XND_BC_Time.OTo_TTC = dr["OTo_TTC"].ToString();
                            oKPI_XND_BC_Time.XeMay_TTB = dr["XeMay_TTB"].ToString();
                            oKPI_XND_BC_Time.OTo_TTB = dr["OTo_TTB"].ToString();
                            oKPI_XND_BC_Time.XeMay_TTV = dr["XeMay_TTV"].ToString();
                            oKPI_XND_BC_Time.OTo_TTV = dr["OTo_TTV"].ToString();
                            oKPI_XND_BC_Time.XeMay_HH = dr["XeMay_HH"].ToString();
                            oKPI_XND_BC_Time.OTo_HH = dr["OTo_HH"].ToString();
                            oKPI_XND_BC_Time.XeMay_TL = dr["XeMay_TL"].ToString();
                            oKPI_XND_BC_Time.OTo_TL = dr["OTo_TL"].ToString();

                            listKPI_XND_BC_Time.Add(oKPI_XND_BC_Time);
                        }
                        _ReturnKPI_XND_BC_Time.Code = "00";
                        _ReturnKPI_XND_BC_Time.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_XND_BC_Time.ListKPI_XND_BC_Time = listKPI_XND_BC_Time;
                    }
                    else
                    {
                        _ReturnKPI_XND_BC_Time.Code = "01";
                        _ReturnKPI_XND_BC_Time.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_XND_BC_Time.Code = "99";
                _ReturnKPI_XND_BC_Time.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_XND_BC_Time;
        }
    }
}