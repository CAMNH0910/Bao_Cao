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
    public class TKSLGDRepository
    {
        public ResponseTKSLGD DETAIL_TKSLGD(int startdate, int enddate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ResponseTKSLGD _return = new ResponseTKSLGD();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("GD_PKG.GetListTKSLGD", Helper.OraDCOracleConnection);
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
                        var listDetail_TKSLGD = new List<TKSLGD>();
                        while (dr.Read())
                        {

                            var oDetail_TKSLGD = new TKSLGD();
                            oDetail_TKSLGD.STT = a++;
                            oDetail_TKSLGD.ROLE = dr["ROLE"].ToString();
                            oDetail_TKSLGD.POSCODE = dr["POSCODE"].ToString();
                            oDetail_TKSLGD.POSNAME = dr["POSNAME"].ToString();
                            oDetail_TKSLGD.SLTOTAL = dr["SLTOTAL"].ToString();
                            oDetail_TKSLGD.TLTOTAL = dr["TLTOTAL"].ToString();
                            oDetail_TKSLGD.SLTHUCONG = dr["SLTHUCONG"].ToString();
                            oDetail_TKSLGD.TLTHUCONG = dr["TLTHUCONG"].ToString();
                            oDetail_TKSLGD.SLEXCEL = dr["SLEXCEL"].ToString();
                            oDetail_TKSLGD.TLEXCEL = dr["TLEXCEL"].ToString();
                            oDetail_TKSLGD.SLONE = dr["SLONE"].ToString();
                            oDetail_TKSLGD.TLONE = dr["TLONE"].ToString();
                            oDetail_TKSLGD.SLVDDT = dr["SLVDDT"].ToString();
                            oDetail_TKSLGD.TLVDDT = dr["TLVDDT"].ToString();
                            oDetail_TKSLGD.SLMCS = dr["SLMCS"].ToString();
                            oDetail_TKSLGD.TLMCS = dr["TLMCS"].ToString();
                            oDetail_TKSLGD.SLKhac = dr["SLKhac"].ToString();
                            oDetail_TKSLGD.TLKhac = dr["TLKhac"].ToString();
                            listDetail_TKSLGD.Add(oDetail_TKSLGD);
                        }
                        _return.Code = "00";
                        _return.Message = "Lấy dữ liệu thành công.";
                        _return.listDetail_TKSLGDReport = listDetail_TKSLGD;
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