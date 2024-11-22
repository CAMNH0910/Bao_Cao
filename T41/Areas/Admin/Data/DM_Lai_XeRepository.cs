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
    public class DM_Lai_XeRepository
    {

        public ReturnDM_Lai_Xe DM_Lai_Xe_DETAIL(int zone,int endpostcode, string trangthai)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnDM_Lai_Xe _ReturnDM_Lai_Xe = new ReturnDM_Lai_Xe();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("DM_Lai_Xe.List_DM_Lai_Xe", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_buucuc", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_trangthai", OracleDbType.Varchar2).Value = trangthai;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDM_Lai_Xe = new List<DM_Lai_Xe>();
                        while (dr.Read())
                        {

                            var oDM_Lai_Xe = new DM_Lai_Xe();
                            oDM_Lai_Xe.STT = a++;
                            oDM_Lai_Xe.Id = dr["Id"].ToString();
                            oDM_Lai_Xe.Zone = dr["Zone"].ToString();
                            oDM_Lai_Xe.Buu_Cuc = dr["Buu_Cuc"].ToString();
                            oDM_Lai_Xe.Tuyen_Phat = dr["Tuyen_Phat"].ToString();
                            oDM_Lai_Xe.Buu_Ta = dr["Buu_Ta"].ToString();
                            oDM_Lai_Xe.Ma_NV = dr["Ma_NV"].ToString();
                            oDM_Lai_Xe.Ten_NV = dr["Ten_NV"].ToString();
                            oDM_Lai_Xe.Trang_Thai = dr["Trang_Thai"].ToString();
                            listDM_Lai_Xe.Add(oDM_Lai_Xe);
                        }
                        _ReturnDM_Lai_Xe.Code = "00";
                        _ReturnDM_Lai_Xe.Message = "Lấy dữ liệu thành công.";
                        _ReturnDM_Lai_Xe.ListDM_Lai_Xe = listDM_Lai_Xe;
                    }
                    else
                    {
                        _ReturnDM_Lai_Xe.Code = "01";
                        _ReturnDM_Lai_Xe.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDM_Lai_Xe.Code = "99";
                _ReturnDM_Lai_Xe.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDM_Lai_Xe;
        }


        public ReturnResponseUpdate Khoa(int id)
        {

            ReturnResponseUpdate R_update = new ReturnResponseUpdate();
            try
            {

                OracleCommand myCommand = new OracleCommand("DM_LAI_XE.Khoa_Lai_Xe", Helper.OraDCOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_id", OracleDbType.Int32).Value = id;
                myCommand.ExecuteNonQuery();
                R_update.Code = "00";
                R_update.Message = "Cập nhật dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_update.Code = "01";
                R_update.Message = ex.ToString();

            }

            return R_update;
        }

        public ReturnDM_Lai_Xe CapNhat_Id_Sua(int id)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnDM_Lai_Xe _ReturnDM_Lai_Xe = new ReturnDM_Lai_Xe();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("DM_Lai_Xe.Lay_Lai_Xe", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_id", OracleDbType.Int32).Value = id;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDM_Lai_Xe = new List<DM_Lai_Xe_Sua>();
                        while (dr.Read())
                        {

                            var oDM_Lai_Xe = new DM_Lai_Xe_Sua();
                            oDM_Lai_Xe.Id = dr["Id"].ToString();
                            oDM_Lai_Xe.Zone = dr["Zone"].ToString();
                            oDM_Lai_Xe.Buu_Cuc = dr["Buu_Cuc"].ToString();
                            oDM_Lai_Xe.Tuyen_Phat = dr["Tuyen_Phat"].ToString();
                            oDM_Lai_Xe.Buu_Ta = dr["Buu_Ta"].ToString();
                            oDM_Lai_Xe.Ma_NV = dr["Ma_NV"].ToString();
                            oDM_Lai_Xe.Ten_NV = dr["Ten_NV"].ToString();
                            listDM_Lai_Xe.Add(oDM_Lai_Xe);
                        }
                        _ReturnDM_Lai_Xe.Code = "00";
                        _ReturnDM_Lai_Xe.Message = "Lấy dữ liệu thành công.";
                        _ReturnDM_Lai_Xe.ListDM_Lai_Xe_Sua = listDM_Lai_Xe;
                    }
                    else
                    {
                        _ReturnDM_Lai_Xe.Code = "01";
                        _ReturnDM_Lai_Xe.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDM_Lai_Xe.Code = "99";
                _ReturnDM_Lai_Xe.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDM_Lai_Xe;
        }

        public bool Add(DM_Lai_Xe_Sua model)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("DM_Lai_Xe.Insert_DM_LAI_XE", Helper.OraDCOracleConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_ten_nv", OracleDbType.Varchar2).Value = model.Ten_NV;
                    cmd.Parameters.Add("v_ma_nv", OracleDbType.Varchar2).Value = model.Ma_NV;
                    cmd.Parameters.Add("v_zone", OracleDbType.Int32).Value = model.Zone;
                    cmd.Parameters.Add("v_buu_cuc", OracleDbType.Varchar2).Value = model.Buu_Cuc;
                    cmd.Parameters.Add("v_tuyen_phat", OracleDbType.Varchar2).Value = model.Tuyen_Phat;
                    cmd.Parameters.Add("v_buu_ta", OracleDbType.Varchar2).Value = model.Buu_Ta;
                   
                    cmd.ExecuteNonQuery();
                }

                return true; // Thêm thành công
            }
            catch
            {
                return false; // Thêm thất bại
            }
        }
        public bool Edit(DM_Lai_Xe_Sua model)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("DM_Lai_Xe.Update_DM_Lai_Xe_NEW", Helper.OraDCOracleConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = model.Id;
                    cmd.Parameters.Add("v_ma_nv", OracleDbType.Varchar2).Value = model.Ma_NV;
                    cmd.Parameters.Add("v_ten_nv", OracleDbType.Varchar2).Value = model.Ten_NV;
                    cmd.Parameters.Add("v_zone", OracleDbType.Int32).Value = model.Zone;
                    cmd.Parameters.Add("v_buu_cuc", OracleDbType.Varchar2).Value = model.Buu_Cuc;
                    cmd.Parameters.Add("v_tuyen_phat", OracleDbType.Varchar2).Value = model.Tuyen_Phat;
                    cmd.Parameters.Add("v_buu_ta", OracleDbType.Varchar2).Value = model.Buu_Ta;
                    
                    cmd.ExecuteNonQuery();
                }

                return true; // Thêm thành công
            }
            catch
            {
                return false; // Thêm thất bại
            }
        }
    }
}