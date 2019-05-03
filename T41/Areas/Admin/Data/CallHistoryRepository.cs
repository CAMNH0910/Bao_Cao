using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class CallHistoryRepository
    {
        #region GET_CallHistory
        public ReturnCallHistory GET_CallHistory(string code)
        {
            ReturnCallHistory return_callhistory = new ReturnCallHistory();
            List<CallHistory> list_CallHistory = new List<CallHistory>();
            CallHistory CallHistory = null;
            int a = 1;
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraPNSOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_LADING_TO_POSTMAN_PKG.GET_DETAIL_CALL_HISTORY";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_CODE", OracleDbType.Varchar2).Value = code;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);

                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraPNSOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            list_CallHistory = new List<CallHistory>();
                            while (dr.Read())
                            {
                                CallHistory = new CallHistory();
                                CallHistory.ID = a++;
                                CallHistory.PO_CODE = dr["PO_CODE"].ToString();
                                CallHistory.CODE = dr["CODE"].ToString();
                                CallHistory.NAME = dr["NAME"].ToString();
                                CallHistory.FULL_NAME = dr["FULL_NAME"].ToString();
                                CallHistory.MOBILE_NUMBER = dr["MOBILE_NUMBER"].ToString();
                                CallHistory.LADING_CODE = dr["LADING_CODE"].ToString();
                                CallHistory.CALLER = dr["CALLER"].ToString();
                                CallHistory.START_TIME = dr["START_TIME"].ToString();
                                CallHistory.TALK_TIME = dr["TALK_TIME"].ToString();
                                CallHistory.PATH = dr["PATH"].ToString();
                                list_CallHistory.Add(CallHistory);
                            }
                            return_callhistory.Code = "00";
                            return_callhistory.Message = "Lấy dữ liệu thành công.";
                            return_callhistory.List_Call_History_Report = list_CallHistory;
                        }
                        else
                        {
                            return_callhistory.Code = "01";
                            return_callhistory.Message = "Không tồn tại bản ghi nào.";
                            return_callhistory.List_Call_History_Report = null;
                        }

                    }
                    else
                    {
                        return_callhistory.Code = "98";
                        return_callhistory.Message = "Lỗi xử lý hệ thống..";
                        return_callhistory.List_Call_History_Report = null;
                    }

                }

            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CallHistoryRepository.GET_CallHistory: " + ex.Message);
                return_callhistory.Code = "99";
                return_callhistory.Message = ex.Message;
                return_callhistory.List_Call_History_Report = null;
            }
            return return_callhistory;
        }

        #endregion

    }



}

