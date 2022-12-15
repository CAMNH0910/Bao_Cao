using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;


namespace T41.Areas.Admin.Data
{
    public class ReportZNSRepository
    {

        

        #region REPORT_ZNS_TOTAL         
        public ReturnZNS REPORTZNSTotal(int startdate, int enddate, int donvi)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnZNS _returnZNSTotal = new ReturnZNS();
            List<ZNSTotal> listZNSTotal = null;
            ZNSTotal oZNSTotal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems_GateWay.GET_LIST_DATA_SMS_BRANDNAME";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add(new OracleParameter("v_Startdate", OracleDbType.Int32)).Value = startdate;
                    cm.Parameters.Add(new OracleParameter("v_Enddate", OracleDbType.Int32)).Value = enddate;
                    cm.Parameters.Add(new OracleParameter("v_Donvi", OracleDbType.Int32)).Value = donvi;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            listZNSTotal = new List<ZNSTotal>();
                            while (dr.Read())
                            {
                                oZNSTotal = new ZNSTotal();
                                oZNSTotal.STT = a++;
                                oZNSTotal.AcceptDate = dr["AcceptDate"].ToString();
                                oZNSTotal.TotalSend = dr["TotalSend"].ToString();
                                oZNSTotal.SuccessMessagers = dr["SuccessMessagers"].ToString();
                                oZNSTotal.FailMessagers = dr["FailMessagers"].ToString();
                                oZNSTotal.ServiceFeeNotVAT = dr["ServiceFeeNotVAT"].ToString();
                                oZNSTotal.Note = dr["Note"].ToString();
                                listZNSTotal.Add(oZNSTotal);

                            }
                            _returnZNSTotal.Code = "00";
                            _returnZNSTotal.Message = "Lấy dữ liệu thành công.";
                            _returnZNSTotal.ListZNSTotalReport = listZNSTotal;
                        }
                        else
                        {
                            _returnZNSTotal.Code = "01";
                            _returnZNSTotal.Message = "Không có dữ liệu";

                        }
                    }    
                    


                }
            }
            catch (Exception ex)
            {
                _returnZNSTotal.Code = "99";
                _returnZNSTotal.Message = ex.ToString();
                
            }
            return _returnZNSTotal;
        }
        #endregion

        #region REPORT_ZNS_DETAIL         
        public ReturnZNS REPORTZNSDetail(int startdate, int enddate, int donvi, string Makh, int loaitin, int trangthai)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnZNS _returnZNSDetail = new ReturnZNS();
            List<ZNSDetail> listZNSDetail = null;
            ZNSDetail oZNSDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems_GateWay.GET_LIST_DATA_SMS_BRANDNAME";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add(new OracleParameter("v_Startdate", OracleDbType.Int32)).Value = startdate;
                    cm.Parameters.Add(new OracleParameter("v_Enddate", OracleDbType.Int32)).Value = enddate;
                    cm.Parameters.Add(new OracleParameter("v_Donvi", OracleDbType.Int32)).Value = donvi;
                    cm.Parameters.Add(new OracleParameter("v_Makh", OracleDbType.Varchar2)).Value = Makh;
                    cm.Parameters.Add(new OracleParameter("v_Loaitin", OracleDbType.Int32)).Value = loaitin;
                    cm.Parameters.Add(new OracleParameter("v_Status", OracleDbType.Int32)).Value = trangthai;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            listZNSDetail = new List<ZNSDetail>();
                            while (dr.Read())
                            {
                                oZNSDetail = new ZNSDetail();
                                oZNSDetail.STT = a++;
                                oZNSDetail.AcceptDate = dr["AcceptDate"].ToString();
                                oZNSDetail.E1Code = dr["E1Code"].ToString();
                                oZNSDetail.PosCode = dr["PosCode"].ToString();
                                oZNSDetail.CustomerCode = dr["CustomerCode"].ToString();
                                oZNSDetail.Phone = dr["Phone"].ToString();
                                oZNSDetail.StatusDate = dr["StatusDate"].ToString();
                                oZNSDetail.StatusDelivery = dr["StatusDelivery"].ToString();
                                oZNSDetail.SendDate = dr["SendDate"].ToString();
                                oZNSDetail.TypeMessage = dr["TypeMessage"].ToString();
                                oZNSDetail.TotalSend = dr["TotalSend"].ToString();
                                oZNSDetail.SuccessMessagers = dr["SuccessMessagers"].ToString();
                                oZNSDetail.FailMessagers = dr["FailMessagers"].ToString();
                                oZNSDetail.ServiceFeeNotVAT = dr["ServiceFeeNotVAT"].ToString();
                                oZNSDetail.Note = dr["Note"].ToString();
                                listZNSDetail.Add(oZNSDetail);

                            }
                            _returnZNSDetail.Code = "00";
                            _returnZNSDetail.Message = "Lấy dữ liệu thành công.";
                            _returnZNSDetail.ListZNSDetailReport = listZNSDetail;
                        }
                        else
                        {
                            _returnZNSDetail.Code = "01";
                            _returnZNSDetail.Message = "Không có dữ liệu";

                        }
                    }



                }
            }
            catch (Exception ex)
            {
                _returnZNSDetail.Code = "99";
                _returnZNSDetail.Message = ex.ToString();

            }
            return _returnZNSDetail;
        }
        #endregion
    }

}

