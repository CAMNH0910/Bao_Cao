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
    public class Kien_Di_Ngoai_TTKTVRepository
    {
        public ReturnKien_Di_Ngoai_TTKTV Kien_Di_Ngoai_TTKTV(int StartDate, int EndDate, int zone)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKien_Di_Ngoai_TTKTV _ReturnKien_Di_Ngoai_TTKTV = new ReturnKien_Di_Ngoai_TTKTV();
            var test = Helper.OraDSOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu. KPI_delivery_PICKUP.REPORT_Kien_Di_Ngoai_TTKTV
                using (OracleCommand cmd = new OracleCommand())
                {
                    // Thiết lập tên stored procedure dựa trên giá trị zone
                    if (zone == 1)
                    {
                        cmd.CommandText = "EMS.Get_List_Report_TTKTV_HN";
                    }
                    else if (zone == 2)
                    {
                        cmd.CommandText = "EMS.Get_List_Report_TTKTV_DN";
                    }
                    else if (zone == 3)
                    {
                        cmd.CommandText = "EMS.Get_List_Report_TTKTV_HCM";
                    }
                    else
                    {
                        throw new ArgumentException("Zone không hợp lệ");
                    }

                    // Thiết lập kết nối và kiểu command
                    cmd.Connection = Helper.OraDSOracleConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 20000;

                    // Thêm tham số vào command
                    cmd.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    cmd.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    cmd.Parameters.Add(new OracleParameter("v_Cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                    // Sử dụng OracleDataAdapter để fill dữ liệu vào DataTable
                    using (OracleDataAdapter mAdapter = new OracleDataAdapter(cmd))
                    {
                        mAdapter.Fill(da);
                    }
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listKien_Di_Ngoai_TTKTV = new List<Kien_Di_Ngoai_TTKTV>();
                        while (dr.Read())
                        {
                            var oKien_Di_Ngoai_TTKTV = new Kien_Di_Ngoai_TTKTV();
                            oKien_Di_Ngoai_TTKTV.STT = a++;
                            oKien_Di_Ngoai_TTKTV.Don_Vi = dr["Don_Vi"].ToString();
                            oKien_Di_Ngoai_TTKTV.SL_TONG = dr["SL_TONG"].ToString();
                            oKien_Di_Ngoai_TTKTV.SL_KTEMS = dr["SL_KTEMS"].ToString();
                            oKien_Di_Ngoai_TTKTV.TY_LE_SL = dr["TY_LE_SL"].ToString();
                            oKien_Di_Ngoai_TTKTV.KL_TONG =dr["KL_TONG"].ToString();
                            oKien_Di_Ngoai_TTKTV.KL_KTEMS = dr["KL_KTEMS"].ToString();
                            oKien_Di_Ngoai_TTKTV.TY_LE_KL = dr["TY_LE_KL"].ToString();
                            listKien_Di_Ngoai_TTKTV.Add(oKien_Di_Ngoai_TTKTV);
                        }
                        _ReturnKien_Di_Ngoai_TTKTV.Code = "00";
                        _ReturnKien_Di_Ngoai_TTKTV.Message = "Lấy dữ liệu thành công.";
                        _ReturnKien_Di_Ngoai_TTKTV.ListKien_Di_Ngoai_TTKTV = listKien_Di_Ngoai_TTKTV;
                    }
                    else
                    {
                        _ReturnKien_Di_Ngoai_TTKTV.Code = "01";
                        _ReturnKien_Di_Ngoai_TTKTV.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKien_Di_Ngoai_TTKTV.Code = "99";
                _ReturnKien_Di_Ngoai_TTKTV.Message = "Lỗi xử lý dữ liệu";
            }
            return _ReturnKien_Di_Ngoai_TTKTV;
        }
    }

}