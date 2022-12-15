using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class InsertStatusInternationalRepository
    {
        #region GetCountryCode
        //Lấy mã nước nhận qua thủ tục edi_monitor.GET_LIST_MA_NC
        public String GetCountryCode()
        {
            string LISTMANC = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "edi_monitor.GET_LIST_MA_NC";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("p_ResultSet", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTMANC += "<option value='" + dr["MA_NC"].ToString() + "'>" + dr["MA_NC"].ToString() + '-' + dr["TENNC"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "InsertStatusInternationalRepository.GetCountryCode: " + ex.Message);
            }

            return LISTMANC;
        }

        public String GetCountryCodeForPartner()
        {
            string LISTMANC = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems_tracking_partner.GET_LIST_MA_NC";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("p_ResultSet", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTMANC += "<option value='" + dr["MA_NC"].ToString() + "'>" + dr["MA_NC"].ToString() + '-' + dr["TENNC"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "InsertStatusInternationalRepository.GetCountryCode: " + ex.Message);
            }

            return LISTMANC;
        }
        #endregion

        #region GetListEventCode
        //Lấy mã nước nhận qua thủ tục edi_monitor.Get_LIST_EVENT_CODE
        public String GetListEventCode()
        {
            string LISTEVENTCODE = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "edi_monitor.Get_LIST_EVENT_CODE";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("p_ResultSet", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTEVENTCODE += "<option value='" + dr["EVENT_CODE"].ToString() + "'>" + dr["EVENT_CODE"].ToString() + '-' + dr["EVENT_NAME"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "InsertStatusInternationalRepository.GetListEventCode: " + ex.Message);
            }

            return LISTEVENTCODE;
        }


        #endregion

        #region GetListEventCode
        //Lấy mã nước nhận qua thủ tục edi_monitor.Get_LIST_EVENT_CODE
        public String GetListEventCodeForPartner()
        {
            string LISTEVENTCODE = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems_tracking_partner.Get_LIST_EVENT_CODE";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("p_ResultSet", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTEVENTCODE += "<option value='" + dr["EVENT_CODE"].ToString() + "'>" + dr["EVENT_CODE"].ToString() + '-' + dr["EVENT_NAME"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "InsertStatusInternationalRepository.GetListEventCode: " + ex.Message);
            }

            return LISTEVENTCODE;
        }
        #endregion


        #region Get_UPU_ACTION_CODE
        //Lấy mã nước nhận qua thủ tục edi_monitor.Get_UPU_ACTION_CODE
        public String Get_UPU_ACTION_CODE()
        {
            string LISTUPUACTIONCODE = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "edi_monitor.Get_UPU_ACTION_CODE";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("p_ResultSet", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTUPUACTIONCODE += "<option value='" + dr["ACTION_CODE"].ToString() + "'>" + dr["ACTION_CODE"].ToString() + '-' + dr["ACTION_NAME_EN"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "InsertStatusInternationalRepository.Get_UPU_ACTION_CODE: " + ex.Message);
            }

            return LISTUPUACTIONCODE;
        }
        #endregion

        #region Get_UPU_REASON_CODE
        //Lấy mã nước nhận qua thủ tục edi_monitor.Get_UPU_ACTION_CODE
        public String Get_UPU_REASON_CODE()
        {
            string LISTUPUACTIONCODE = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "edi_monitor.Get_UPU_REASON_CODE";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("p_ResultSet", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTUPUACTIONCODE += "<option value='" + dr["REASON_CODE"].ToString() + "'>" + dr["REASON_CODE"].ToString() + '-' + dr["REASON_NAME_EN"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "InsertStatusInternationalRepository.Get_UPU_REASON_CODE: " + ex.Message);
            }

            return LISTUPUACTIONCODE;
        }
        #endregion

        #region StatusInternational_Create

        public ReponseEntity StatusInternational_Create(ParameterInsertStatusInternational param)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            string substring_gio = "";
            string substring_ngay = param.NGAY.Substring(0, 8);
            if (param.NGAY.Length == 13)
            {
                 substring_gio = param.GIO.Substring(9, 4);
            }
            if (param.NGAY.Length == 12)
            {
                 substring_gio = param.GIO.Substring(9, 3);
            }
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "edi_monitor.INSERT_EMSEVTv3";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 20000;
                    cmd.Parameters.Add("p_result", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("p_event", OracleDbType.Varchar2, ParameterDirection.Input).Value = param.EVENT.Trim();
                    cmd.Parameters.Add("p_mae1", OracleDbType.Varchar2, ParameterDirection.Input).Value = param.MAE1.Trim();
                    cmd.Parameters.Add("p_manc", OracleDbType.Varchar2, ParameterDirection.Input).Value = param.MANC.Trim();
                    cmd.Parameters.Add("p_ngay", OracleDbType.Int32, ParameterDirection.Input).Value = Convert.ToInt32(substring_ngay);
                    cmd.Parameters.Add("p_gio", OracleDbType.Int32, ParameterDirection.Input).Value = Convert.ToInt32(substring_gio);
                    cmd.Parameters.Add("p_lydo", OracleDbType.Varchar2, ParameterDirection.Input).Value = param.LYDO.Trim();
                    cmd.Parameters.Add("p_huongxuly", OracleDbType.Varchar2, ParameterDirection.Input).Value = param.HUONGXULY.Trim();
                    cmd.Parameters.Add("p_oe", OracleDbType.Varchar2, ParameterDirection.Input).Value = param.OE.Trim();
                    cmd.Parameters.Add("p_mabc_kt", OracleDbType.Varchar2, ParameterDirection.Input).Value = param.MABC_KT.Trim();
                    cmd.ExecuteNonQuery();
                    //OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    oReponseEntity = new ReponseEntity();
                    if (int.Parse(cmd.Parameters["p_result"].Value.ToString()) == 0)
                    {
                        oReponseEntity.Code = "00";
                        oReponseEntity.Message = "Thêm mã " + param.MAE1.Trim() + " thành công";
                    }
                    else
                    {
                        oReponseEntity.Code = "01";
                        oReponseEntity.Message = "Thêm mã " + param.MAE1.Trim() + " không thành công";
                    }
                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "----------------------------");
                LogAPI.LogToFile(LogFileType.EXCEPTION, "region InsertStatusInternationalRepository.StatusInternational_Create: " + ex.Message);
                LogAPI.LogToFile(LogFileType.EXCEPTION, "MAE1 : " + param.MAE1.Trim());
                LogAPI.LogToFile(LogFileType.EXCEPTION, "----------------------------");
                oReponseEntity.Code = "99";
                oReponseEntity.Message = "Lỗi xử lý dữ liệu";
                oReponseEntity.Value = string.Empty;

            }
            return oReponseEntity;
        }




        public ReponseEntity StatusInternationalForPartner_Create(string MAE1, string STATUS, string MANC, string NGAY, string GIO, string LYDO, string HANHDONG)
        {
            ReponseEntity oReponseEntity = new ReponseEntity();
            string substring_gio = "";
            string substring_ngay = NGAY.Substring(0, 8);
            if (NGAY.Length == 13)
            {
                substring_gio = GIO.Substring(9, 4);
            }
            if (NGAY.Length == 12)
            {
                substring_gio = GIO.Substring(9, 3);
            }

            try
            {

                OracleCommand myCommand = new OracleCommand("ems_tracking_partner.Update_Tracking_Outbound_Items", Helper.OraDCOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_mae1", OracleDbType.Varchar2).Value = MAE1;
                myCommand.Parameters.Add("v_Trang_Thai", OracleDbType.Varchar2).Value = STATUS;
                myCommand.Parameters.Add("v_Ma_Nuoc", OracleDbType.Varchar2).Value = MANC;
                myCommand.Parameters.Add("v_Ngay", OracleDbType.Int32).Value = Convert.ToInt32(substring_ngay);
                myCommand.Parameters.Add("v_Gio", OracleDbType.Int32).Value = Convert.ToInt32(substring_gio);
                myCommand.Parameters.Add("v_Ly_Do", OracleDbType.Varchar2).Value = LYDO;
                myCommand.Parameters.Add("v_Hanh_Dong", OracleDbType.Varchar2).Value = HANHDONG;
                myCommand.ExecuteNonQuery();
                oReponseEntity.Code = "00";
                oReponseEntity.Message = "Thêm mới dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                oReponseEntity.Code = "01";
                oReponseEntity.Message = ex.ToString();

            }

            return oReponseEntity;
        }

        #endregion


        public ReturnStatusInternational DisplayStatusInternational(string E1_Code)
        {

            DataTable da = new DataTable();
            ReturnStatusInternational _returnStatusInternational = new ReturnStatusInternational();
            List<StatusInternational> listStatusInternational = null;
            StatusInternational oStatusInternationalDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems_tracking_partner.Getlist_Tracking_Outb_Item", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_mae1", OracleDbType.Varchar2).Value = E1_Code.Trim();


                    myCommand.Parameters.Add(new OracleParameter("v_List", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listStatusInternational = new List<StatusInternational>();
                        while (dr.Read())
                        {
                            oStatusInternationalDetail = new StatusInternational();
                            oStatusInternationalDetail.STT = a++;
                            oStatusInternationalDetail.NGAY = dr["NGAY"].ToString();
                            oStatusInternationalDetail.GIO = dr["GIO"].ToString();
                            oStatusInternationalDetail.STATUS = dr["GHI_CHU"].ToString();
                            listStatusInternational.Add(oStatusInternationalDetail);
                        }
                        _returnStatusInternational.Code = "00";
                        _returnStatusInternational.Message = "Lấy dữ liệu thành công.";
                        _returnStatusInternational.ListStatusInternationalReport = listStatusInternational;
                    }
                    else
                    {
                        _returnStatusInternational.Code = "01";
                        _returnStatusInternational.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnStatusInternational.Code = "99";
                _returnStatusInternational.Message = ex.ToString();

            }
            return _returnStatusInternational;
        }
    }
}