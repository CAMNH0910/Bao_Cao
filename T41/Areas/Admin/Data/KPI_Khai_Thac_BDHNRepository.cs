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
    public class KPI_Khai_Thac_BDHNRepository
    {
        public ReturnKhai_Thac_BDHN Chap_nhan_TH(int startdate, int enddate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKhai_Thac_BDHN _ReturnKPI_Khai_Thac_BDHN = new ReturnKhai_Thac_BDHN();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("camnh.Report_Khai_Thac_BDHN.Report_KPI_Chan_Nhan_TH", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listChap_Nhan_TH = new List<Chap_Nhan_TH>();
                        while (dr.Read())
                        {

                            var oChap_Nhan_TH = new Chap_Nhan_TH();
                            oChap_Nhan_TH.STT = a++;
                            oChap_Nhan_TH.MA_BC_CN = dr["MA_BC_CN"].ToString();
                            oChap_Nhan_TH.TIME_GROUP = dr["TIME_GROUP"].ToString();
                            oChap_Nhan_TH.SL = Convert.ToInt32(dr["SL"].ToString());
                            oChap_Nhan_TH.KPI_RESULT = Convert.ToInt32(dr["KPI_RESULT"].ToString());
                            oChap_Nhan_TH.TL = dr["TL"].ToString();
                            oChap_Nhan_TH.KDQD = Convert.ToInt32(dr["KDQD"].ToString());
                            oChap_Nhan_TH.TL_KDQD = dr["TL_KDQD"].ToString();
                            listChap_Nhan_TH.Add(oChap_Nhan_TH);
                        }
                        _ReturnKPI_Khai_Thac_BDHN.Code = "00";
                        _ReturnKPI_Khai_Thac_BDHN.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Khai_Thac_BDHN.ListChap_Nhan_TH = listChap_Nhan_TH;
                    }
                    else
                    {
                        _ReturnKPI_Khai_Thac_BDHN.Code = "01";
                        _ReturnKPI_Khai_Thac_BDHN.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Khai_Thac_BDHN.Code = "99";
                _ReturnKPI_Khai_Thac_BDHN.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Khai_Thac_BDHN;
        }
        public ReturnKhai_Thac_BDHN Chap_Nhan_CT(int startdate, int enddate,int postCode)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKhai_Thac_BDHN _ReturnKPI_Khai_Thac_BDHN = new ReturnKhai_Thac_BDHN();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("camnh.Report_Khai_Thac_BDHN.Report_KPI_Chan_Nhan_CT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.Int32).Value = postCode;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listChap_Nhan_TH = new List<Chap_Nhan_CT>();
                        while (dr.Read())
                        {

                            var oChap_Nhan_TH = new Chap_Nhan_CT();
                            oChap_Nhan_TH.STT = a++;
                            oChap_Nhan_TH.ITEMCODE = dr["ITEMCODE"].ToString();
                            oChap_Nhan_TH.MA_BC_CN = dr["MA_BC_CN"].ToString();
                            oChap_Nhan_TH.THOI_GIAN_CN = dr["THOI_GIAN_CN"].ToString();
                            oChap_Nhan_TH.TGD_100916 = dr["TGD_100916"].ToString();
                            oChap_Nhan_TH.TGD_101000 = dr["TGD_101000"].ToString();
                            oChap_Nhan_TH.MA_BC_DEN = dr["MA_BC_DEN"].ToString();
                            oChap_Nhan_TH.Danh_Gia = dr["Danh_Gia"].ToString();
                            listChap_Nhan_TH.Add(oChap_Nhan_TH);
                        }
                        _ReturnKPI_Khai_Thac_BDHN.Code = "00";
                        _ReturnKPI_Khai_Thac_BDHN.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Khai_Thac_BDHN.ListChap_Nhan_CT = listChap_Nhan_TH;
                    }
                    else
                    {
                        _ReturnKPI_Khai_Thac_BDHN.Code = "01";
                        _ReturnKPI_Khai_Thac_BDHN.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Khai_Thac_BDHN.Code = "99";
                _ReturnKPI_Khai_Thac_BDHN.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Khai_Thac_BDHN;
        }
    }
}