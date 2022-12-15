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
    public class ReportSMSBrandRepository
    {

        //Phần chi tiết của bảng tổng hợp 

        #region REPORT_SMS_BRANDNAME_DETAIL          
        public ReturnSMSBrandDetail REPORTSMSBRANDNAME(int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnSMSBrandDetail _returnSMSBrandDetail = new ReturnSMSBrandDetail();
            List<SMSBrandDetail> listSMSBrandDetail = null;
            SMSBrandDetail oSMSBrandDetail = null;
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
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            listSMSBrandDetail = new List<SMSBrandDetail>();
                            while (dr.Read())
                            {
                                oSMSBrandDetail = new SMSBrandDetail();
                                oSMSBrandDetail.STT = a++;
                                oSMSBrandDetail.BrandName = dr["BRANDNAME"].ToString();
                                oSMSBrandDetail.Network = dr["NETWORK"].ToString();
                                oSMSBrandDetail.Status = dr["TRANGTHAI"].ToString();
                                oSMSBrandDetail.EMSPro1 = dr["EMSPRO1"].ToString();
                                oSMSBrandDetail.EMSTest1 = dr["EMSTEST1"].ToString();
                                oSMSBrandDetail.Total1 = dr["TC1"].ToString();
                                oSMSBrandDetail.EMSPro2 = dr["EMSPRO2"].ToString();
                                oSMSBrandDetail.EMSTest2 = dr["EMSTEST2"].ToString();
                                oSMSBrandDetail.Total2 = dr["TC2"].ToString();

                                listSMSBrandDetail.Add(oSMSBrandDetail);

                            }
                            _returnSMSBrandDetail.Code = "00";
                            _returnSMSBrandDetail.Message = "Lấy dữ liệu thành công.";
                            _returnSMSBrandDetail.ListSMSBrandDetailReport = listSMSBrandDetail;
                        }
                        else
                        {
                            _returnSMSBrandDetail.Code = "01";
                            _returnSMSBrandDetail.Message = "Không có dữ liệu";

                        }
                    }    
                    


                }
            }
            catch (Exception ex)
            {
                _returnSMSBrandDetail.Code = "99";
                _returnSMSBrandDetail.Message = ex.ToString();
                
            }
            return _returnSMSBrandDetail;
        }
        #endregion

    }

}

