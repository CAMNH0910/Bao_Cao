using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;
using System.Data.Odbc;
using System.Data.SqlClient;
using Remotion;

namespace T41.Areas.Admin.Data
{
    public class Tms_Kpi_HTRepository
    {

        public ReturnTms_Kpi_HT Tms_Kpi_HT_DETAIL( int endpostcode, int? hanh_trinh, int isdelete)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnTms_Kpi_HT _ReturnTms_Kpi_HT = new ReturnTms_Kpi_HT();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_HT_Report.List_Tms_Kpi_HT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_HT", OracleDbType.Int32).Value = hanh_trinh;
                    myCommand.Parameters.Add("v_isdelete", OracleDbType.Int32).Value = isdelete;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTms_Kpi_HT = new List<Tms_Kpi_HT>();
                        while (dr.Read())
                        {

                            var oTms_Kpi_HT = new Tms_Kpi_HT();
                            oTms_Kpi_HT.STT = a++;
                            oTms_Kpi_HT.HUB = dr["HUB"].ToString();
                            //oTms_Kpi_HT.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oTms_Kpi_HT.TINH_PHAT = dr["TINH_PHAT"].ToString();
                            oTms_Kpi_HT.HUYEN_PHAT = dr["HUYEN_PHAT"].ToString();
                            oTms_Kpi_HT.TEN_HUYEN = dr["TEN_HUYEN"].ToString();
                            oTms_Kpi_HT.ID_HANH_TRINH = dr["ID_HANH_TRINH"].ToString();
                            oTms_Kpi_HT.TEN_HANH_TRINH = dr["TEN_HANH_TRINH"].ToString();
                            oTms_Kpi_HT.THOI_GIAN_DI = dr["THOI_GIAN_DI"].ToString();
                            oTms_Kpi_HT.Trang_Thai = dr["Trang_Thai"].ToString();
                            listTms_Kpi_HT.Add(oTms_Kpi_HT);
                        }
                        _ReturnTms_Kpi_HT.Code = "00";
                        _ReturnTms_Kpi_HT.Message = "Lấy dữ liệu thành công.";
                        _ReturnTms_Kpi_HT.List_Tms_Kpi_HT = listTms_Kpi_HT;
                    }
                    else
                    {
                        _ReturnTms_Kpi_HT.Code = "01";
                        _ReturnTms_Kpi_HT.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnTms_Kpi_HT.Code = "99";
                _ReturnTms_Kpi_HT.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnTms_Kpi_HT;
        }


        public ReturnResponseUpdate Insert_Tms_Kpi_HT(int HUB, int TINH_PHAT, int HUYEN_PHAT, string TEN_HUYEN, int ID_HANH_TRINH, string TEN_HANH_TRINH, string THOI_GIAN_DI, int Trang_thai)
        {

            ReturnResponseUpdate R_update = new ReturnResponseUpdate();
            try
            {

                OracleCommand myCommand = new OracleCommand("KPI_HT_Report.Insert_Tms_Kpi_HT", Helper.OraDCOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_HUB", OracleDbType.Int32).Value = HUB;
                myCommand.Parameters.Add("v_TINH_PHAT", OracleDbType.Int32).Value = TINH_PHAT;
                myCommand.Parameters.Add("v_HUYEN_PHAT", OracleDbType.Int32).Value = HUYEN_PHAT;
                myCommand.Parameters.Add("v_HUYEN_PHAT", OracleDbType.NVarchar2).Value = TEN_HUYEN;
                myCommand.Parameters.Add("v_ID_HANH_TRINH", OracleDbType.Int32).Value = ID_HANH_TRINH;
                myCommand.Parameters.Add("v_TEN_HANH_TRINH", OracleDbType.NVarchar2).Value = TEN_HANH_TRINH;
                myCommand.Parameters.Add("v_THOI_GIAN_DI", OracleDbType.NVarchar2).Value = THOI_GIAN_DI;
                myCommand.Parameters.Add("v_Trang_Thai", OracleDbType.Int32).Value = Trang_thai;
                myCommand.ExecuteNonQuery();
                R_update.Code = "00";
                R_update.Message = "Thêm mới dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_update.Code = "01";
                R_update.Message = ex.ToString();

            }

            return R_update;
        }

        public ReturnResponseUpdate Update_Tms_Kpi_HT(int HUB, int TINH_PHAT, int HUYEN_PHAT, string TEN_HUYEN, int ID_HANH_TRINH, string TEN_HANH_TRINH, string THOI_GIAN_DI, int Trang_thai)
        {

            ReturnResponseUpdate R_update = new ReturnResponseUpdate();
            try
            {

                OracleCommand myCommand = new OracleCommand("KPI_HT_Report.UpdateTms_Kpi_HT", Helper.OraDCOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_HUB", OracleDbType.Int32).Value = HUB;
                myCommand.Parameters.Add("v_TINH_PHAT", OracleDbType.Int32).Value = TINH_PHAT;
                myCommand.Parameters.Add("v_HUYEN_PHAT", OracleDbType.Int32).Value = HUYEN_PHAT;
                myCommand.Parameters.Add("v_HUYEN_PHAT", OracleDbType.NVarchar2).Value = TEN_HUYEN;
                myCommand.Parameters.Add("v_ID_HANH_TRINH", OracleDbType.Int32).Value = ID_HANH_TRINH;
                myCommand.Parameters.Add("v_TEN_HANH_TRINH", OracleDbType.NVarchar2).Value = TEN_HANH_TRINH;
                myCommand.Parameters.Add("v_THOI_GIAN_DI", OracleDbType.NVarchar2).Value = THOI_GIAN_DI;
                myCommand.Parameters.Add("v_Trang_Thai", OracleDbType.Int32).Value = Trang_thai;
                myCommand.ExecuteNonQuery();
                R_update.Code = "00";
                R_update.Message = "Cập nhật dữ liệu thành công  !";
            }
            catch (Exception ex)
            {
                R_update.Code = "01";
                R_update.Message = ex.ToString();

            }

            return R_update;
        }
    }
}