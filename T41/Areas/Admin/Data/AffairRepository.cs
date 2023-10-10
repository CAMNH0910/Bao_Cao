using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;


namespace T41.Areas.Admin.Data
{
    public class AffairRepository
    {

        #region GETLISTBUUCUC
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public string GetPosCode()
        {
            List<GETPOSCODEGD> listBC = null;
            GETPOSCODEGD oGetBC = null;
            string LISTBC = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "Affair.GetListPosCode";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        // LISTTINH += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTBC += "<option value='" + dr["Ma_BC"].ToString() + "'>" + dr["Ma_BC"].ToString() + '-' + dr["Ten_BC"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetBC" + ex.Message);
                LISTBC = null;
            }

            return LISTBC;
        }

        #endregion
        //Phần TONG HOP
        #region AFFAIR_DETAIL          
        public ReturnAffair AFFAIR_DETAIL(int PosCode,int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataAffair _metadataAffair = new MetaDataAffair();
            Convertion common = new Convertion();
            ReturnAffair _returnAffair = new ReturnAffair();
            _metadataAffair.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            _returnAffair.MetaDataAffair = _metadataAffair;
            List<AffairDetail> listAffairDetail = null;
            AffairDetail oCustomerDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("Affair.GetListAffair", Helper.OraDCOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                  
                    myCommand.Parameters.Add("v_PosCode", OracleDbType.Int32).Value = PosCode;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAffairDetail = new List<AffairDetail>();
                        while (dr.Read())
                        {
                            oCustomerDetail = new AffairDetail();
                            oCustomerDetail.STT = a++;
                            oCustomerDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oCustomerDetail.AcceptancePosCode = dr["ACCEPTANCEPOSCODE"].ToString();
                            oCustomerDetail.AffairType = dr["AFFAIRTYPE"].ToString();
                            oCustomerDetail.AmountCOD = dr["AMOUNTCOD"].ToString();
                            oCustomerDetail.AmountCODOld = dr["AMOUNTCODOLD"].ToString();
                            oCustomerDetail.ReceiverFullName = dr["RECEIVERFULLNAME"].ToString();
                            oCustomerDetail.ReceiverAddress = dr["RECEIVERADDRESS"].ToString();
                            oCustomerDetail.ReceiverTel = dr["RECEIVERTEL"].ToString();
                            oCustomerDetail.PushBccp = dr["PUSHBCCP"].ToString();
                            oCustomerDetail.PushBccpDateLog = dr["PUSHBCCPDATELOG"].ToString();
                            oCustomerDetail.AffairDate = dr["AFFAIRDATE"].ToString();
                            listAffairDetail.Add(oCustomerDetail);
                        }
                        _returnAffair.Code = "00";
                        _returnAffair.Message = "Lấy dữ liệu thành công.";
                        _returnAffair.ListAffairReport = listAffairDetail;
                    }
                    else
                    {
                        _returnAffair.Code = "01";
                        _returnAffair.Message = "Không có dữ liệu";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _returnAffair.Code = "99";
                _returnAffair.Message = "Lỗi xử lý dữ liệu";
               
            }
            return _returnAffair;
        }
        #endregion     
    }

}

