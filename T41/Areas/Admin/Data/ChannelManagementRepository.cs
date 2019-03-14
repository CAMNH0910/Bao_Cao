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
    public class ChannelManagementRepository
    {
        // Phần lấy dữ liệu từ bảng business_profile
        #region CHANNEL_MANAGEMENT_DETAIL          
        public ReturnChannelManagement CHANNEL_MANAGEMENT_DETAIL(int page_size, int page_index, int from_date, int to_date, int userid)
        {
            Convertion common = new Convertion();
            DataTable da = new DataTable();
            List<ChannelManagement_BP_Detail> listBusinessProfile = null;
            ChannelManagement_BP_Detail bpo = null;
            ReturnChannelManagement return_user = new ReturnChannelManagement();
            int a = 1;
            int type;
            switch (userid)
            {
                //UserID admin
                case 1:
                    type = 0;
                    break;
                //UserID kinhdoanhems
                case 4:
                    type = 0;
                    break;
                //UserID kinhdoanhhn
                case 12:
                    type = 10;
                    break;
                //UserID kinhdoanhdn
                case 13:
                    type = 55;
                    break;
                //UserID kinhdoanhhcm
                case 14:
                    type = 70;
                    break;
                default:
                    type = 55;
                    break;
            }
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("USER_CHANNEL_MANAGEMENT.Page_Detail_User_Channel", Helper.OraDCOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_PAGE_INDEX", OracleDbType.Int32).Value = page_index;
                    myCommand.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = page_size;
                    myCommand.Parameters.Add("P_FROM_DATE", OracleDbType.Int32).Value = from_date;
                    myCommand.Parameters.Add("P_TO_DATE", OracleDbType.NVarchar2).Value = to_date;
                    myCommand.Parameters.Add("P_TYPE", OracleDbType.Int32).Value = type;
                    myCommand.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    //myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listBusinessProfile = new List<ChannelManagement_BP_Detail>();
                        while (dr.Read())
                        {
                            bpo = new ChannelManagement_BP_Detail();
                            bpo.STT = a++;
                            bpo.AMND_DATE = dr["AMND_DATE"].ToString();
                            bpo.CONTACT_NAME = dr["CONTACT_NAME"].ToString();
                            bpo.CONTACT_ADDRESS = dr["CONTACT_ADDRESS"].ToString();
                            bpo.CONTACT_DISTRICT = dr["CONTACT_DISTRICT"].ToString();
                            bpo.CONTACT_PROVINCE = dr["CONTACT_PROVINCE"].ToString();
                            bpo.CONTACT_PHONE_WORK = dr["CONTACT_PHONE_WORK"].ToString();
                            bpo.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            bpo.GENERAL_EMAIL = dr["GENERAL_EMAIL"].ToString();
                            listBusinessProfile.Add(bpo);
                        }
                        return_user.Code = "00";
                        return_user.Message = "Lấy dữ liệu thành công.";
                        return_user.ListChannelManagement_BP_Report = listBusinessProfile;
                        return_user.Total = Convert.ToInt32(myCommand.Parameters["P_TOTAL"].Value.ToString());

                    }
                    else
                    {
                        return_user.Code = "01";
                        return_user.Message = "Không có dữ liệu.";
                        return_user.ListChannelManagement_BP_Report = null;
                        return_user.Total = 0;

                    }

                    
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "region ChannelManagementRepository // CHANNEL_MANAGEMENT_DETAIL " + ex.Message);
                return_user.Code = "99";
                return_user.Message = "Lỗi xử lý dữ liệu" + ex.Message;
            }
            return return_user;
        }



        #endregion
    }
}