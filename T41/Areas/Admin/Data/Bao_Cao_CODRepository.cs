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
    public class Bao_Cao_CODRepository
    {
        public ReturnBao_Cao_COD Bao_Cao_COD(int StartDate, int EndDate, string Ma_kh, string So_hieu, string Trang_thai)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnBao_Cao_COD _ReturnBao_Cao_COD = new ReturnBao_Cao_COD();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("Bao_Cao_COD.Get_List_Bao_cao", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_Ma_kh", OracleDbType.Varchar2).Value = Ma_kh;
                    myCommand.Parameters.Add("v_So_hieu", OracleDbType.Varchar2).Value = So_hieu;
                    myCommand.Parameters.Add("v_Trang_thai", OracleDbType.Int32).Value = Trang_thai;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listBao_Cao_COD = new List<Bao_Cao_COD>();
                        while (dr.Read())
                        {

                            var oBao_Cao_COD = new Bao_Cao_COD();
                            oBao_Cao_COD.STT = a++;
                            oBao_Cao_COD.Ngay_CN = dr["Ngay_CN"].ToString();
                            //oBao_Cao_COD.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oBao_Cao_COD.Ngay_COD = dr["Ngay_COD"].ToString();
                            oBao_Cao_COD.Makh = dr["Makh"].ToString(); 
                            oBao_Cao_COD.Ma_Tong = dr["Ma_Tong"].ToString();
                            oBao_Cao_COD.NguoiGui = dr["NguoiGui"].ToString();
                            oBao_Cao_COD.Chi_Nhanh = dr["Chi_Nhanh"].ToString();
                            oBao_Cao_COD.SO_THAM_CHIEU = dr["SO_THAM_CHIEU"].ToString();
                            oBao_Cao_COD.Mae1 = dr["Mae1"].ToString();
                            oBao_Cao_COD.COD = dr["COD"].ToString();
                            oBao_Cao_COD.Trang_Thai = dr["Trang_Thai"].ToString();

                            listBao_Cao_COD.Add(oBao_Cao_COD);
                        }
                        _ReturnBao_Cao_COD.Code = "00";
                        _ReturnBao_Cao_COD.Message = "Lấy dữ liệu thành công.";
                        _ReturnBao_Cao_COD.ListBao_Cao_COD = listBao_Cao_COD;
                    }
                    else
                    {
                        _ReturnBao_Cao_COD.Code = "01";
                        _ReturnBao_Cao_COD.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnBao_Cao_COD.Code = "99";
                _ReturnBao_Cao_COD.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnBao_Cao_COD;
        }
    }
}