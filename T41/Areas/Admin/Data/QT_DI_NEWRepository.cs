using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;
using System.Security.Policy;
using System.Web.Services.Description;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;

namespace T41.Areas.Admin.Data
{
    public class QT_DI_NEWRepository
    {
        public ReturnQT_DI_NEW QT_DI_NEW_DETAIL( int startdate, int enddate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnQT_DI_NEW _QT_DI_NEW = new ReturnQT_DI_NEW();
            var test = Helper.OraDSOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_E1_QT_Di_NEW", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listQT_DI_NEW = new List<QT_DI_NEW>();
                        while (dr.Read())
                        {

                            var oQT_DI_NEW = new QT_DI_NEW();
                            oQT_DI_NEW.STT = a++;
                            oQT_DI_NEW.Donvi = dr["DONVI"].ToString();
                            oQT_DI_NEW.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQT_DI_NEW.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQT_DI_NEW.Mae1 = dr["MAE1"].ToString();
                            oQT_DI_NEW.MaKH = dr["MaKH"].ToString();
                            oQT_DI_NEW.Manuoctra = dr["MANUOCTRA"].ToString();
                            oQT_DI_NEW.Ten_Nuoc = dr["TEN_NUOC"].ToString();
                            oQT_DI_NEW.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();
                            oQT_DI_NEW.Madvchinh = dr["MADVCHINH"].ToString();
                            // oQT_DI_NEW.TenDV = dr["TENDV"].ToString();
                            oQT_DI_NEW.PhanLoai = dr["PHANLOAI"].ToString();
                            oQT_DI_NEW.khoiluong = dr["KL"].ToString();
                            oQT_DI_NEW.KLQD = dr["KLQD"].ToString();
                            oQT_DI_NEW.Tongcuoc = dr["TONGCUOC"].ToString();

                            listQT_DI_NEW.Add(oQT_DI_NEW);
                        }
                        _QT_DI_NEW.Code = "00";
                        _QT_DI_NEW.Message = "Lấy dữ liệu thành công.";
                        _QT_DI_NEW.ListDetaiQT_DI_NEW = listQT_DI_NEW;
                    }
                    else
                    {
                        _QT_DI_NEW.Code = "01";
                        _QT_DI_NEW.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _QT_DI_NEW.Code = "99";
                _QT_DI_NEW.Message = "Lỗi xử lý dữ liệu";

            }
            return _QT_DI_NEW;
        }
    }
}