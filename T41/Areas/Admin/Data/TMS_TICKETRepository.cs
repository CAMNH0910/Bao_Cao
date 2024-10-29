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
    public class TMS_TICKETRepository
    {



        #region Get_User
        //Lấy mã bưu cục phát dưới DB Procedure Detail_DeliveryPosCode_Ems
        public IEnumerable<Get_User> Get_User(int user)
        {
            List<Get_User> listGetPosCode = null;
            Get_User oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCDevOracleConnection;
                    cm.CommandText = Helper.SchemaName + "TMS_TICKET.Get_User";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_user", OracleDbType.Int32)).Value = user;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<Get_User>();
                        while (dr.Read())
                        {
                            oGetPosCode = new Get_User();
                            oGetPosCode.Nhom_Tinh = (dr["Nhom_Tinh"].ToString());
                            oGetPosCode.Ten_Nhom_Tinh = dr["Ten_Nhom_Tinh"].ToString();
                            listGetPosCode.Add(oGetPosCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "KpiBD13DeliveryRepository.GetAllPOSCODE" + ex.Message);
                listGetPosCode = null;
            }

            return listGetPosCode;
        }


        public IEnumerable<Get_Nhom_Tinh> Get_Nhom_Tinh(int id)
        {
            List<Get_Nhom_Tinh> listGetPosCode = null;
            Get_Nhom_Tinh oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCDevOracleConnection;
                    cm.CommandText = Helper.SchemaName + "TMS_TICKET.Get_Nhom_Tinh";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32)).Value = id;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<Get_Nhom_Tinh>();
                        while (dr.Read())
                        {
                            oGetPosCode = new Get_Nhom_Tinh();
                            oGetPosCode.ID = (dr["ID"].ToString());
                            oGetPosCode.TEN_NV = dr["TEN_NV"].ToString();
                            listGetPosCode.Add(oGetPosCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "KpiBD13DeliveryRepository.GetAllPOSCODE" + ex.Message);
                listGetPosCode = null;
            }

            return listGetPosCode;
        }

        public IEnumerable<Get_Nhan_Vien> Get_Nhan_Vien(int nhomtinh)
        {
            List<Get_Nhan_Vien> listGetPosCode = null;
            Get_Nhan_Vien oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCDevOracleConnection;
                    cm.CommandText = Helper.SchemaName + "TMS_TICKET.Get_Nhom_Tinh";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_nhomtinh", OracleDbType.Int32)).Value = nhomtinh;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<Get_Nhan_Vien>();
                        while (dr.Read())
                        {
                            oGetPosCode = new Get_Nhan_Vien();
                            oGetPosCode.ID = (dr["Nhom_Tinh"].ToString());
                            oGetPosCode.TEN_NV = dr["Ten_Nhom_Tinh"].ToString();
                            listGetPosCode.Add(oGetPosCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "KpiBD13DeliveryRepository.GetAllPOSCODE" + ex.Message);
                listGetPosCode = null;
            }

            return listGetPosCode;
        }
        #endregion
        public ReturnTMS_TICKET TMS_TICKET_DETAIL(string tennv)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnTMS_TICKET _ReturnTMS_TICKET = new ReturnTMS_TICKET();
            var test = Helper.OraDCDevOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.List_DM_Nhan_Vien_CMS", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Ten_NV", OracleDbType.NVarchar2).Value = tennv;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<TMS_TICKET>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new TMS_TICKET();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.ID = dr["ID"].ToString();
                            oTMS_TICKET.Ten_NV = dr["Ten_NV"].ToString();
                            oTMS_TICKET.Phan_User = dr["Phan_User"].ToString();
                            oTMS_TICKET.Phan_Tinh = dr["Phan_Tinh"].ToString();
                            oTMS_TICKET.Nhom_Tinh = dr["Nhom_Tinh"].ToString();
                            oTMS_TICKET.Ghi_Chu = dr["Ghi_Chu"].ToString();
                            oTMS_TICKET.isdelete = dr["isdelete"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.List_TMS_TICKET = listTMS_TICKET;
                    }
                    else
                    {
                        _ReturnTMS_TICKET.Code = "01";
                        _ReturnTMS_TICKET.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnTMS_TICKET.Code = "99";
                _ReturnTMS_TICKET.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnTMS_TICKET;
        }
        public ReturnResponseUpdate Insert_TMS_TICKET(string Ten_NV, string Phan_User, string Phan_Tinh, string Nhom_Tinh, string Ghi_Chu)
        {

            ReturnResponseUpdate R_update = new ReturnResponseUpdate();
            try
            {

                OracleCommand myCommand = new OracleCommand("TMS_TICKET.Insert_DM_Nhan_Vien_CMS", Helper.OraDCDevOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_Ten_NV", OracleDbType.Varchar2).Value = Ten_NV;
                myCommand.Parameters.Add("v_Phan_User", OracleDbType.Varchar2).Value = Phan_User;
                myCommand.Parameters.Add("v_Phan_Tinh   ", OracleDbType.Varchar2).Value = Phan_Tinh;
                myCommand.Parameters.Add("v_Nhom_Tinh  ", OracleDbType.Int32).Value = Nhom_Tinh;
                myCommand.Parameters.Add("v_Ghi_Chu", OracleDbType.Varchar2).Value = Ghi_Chu;
                myCommand.ExecuteNonQuery();
                R_update.Code = "00";
                R_update.Message = "Thêm mới dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_update.Code = "01";
                R_update.Message = ex.ToString();

            }

            return R_update;
        }

        public ReturnResponseUpdate Update_TMS_TICKET(string ID, string Ten_NV, string Phan_User, string Phan_Tinh, string Nhom_Tinh, string Ghi_Chu, int isdelete)
        {

            ReturnResponseUpdate R_update = new ReturnResponseUpdate();
            try
            {

                OracleCommand myCommand = new OracleCommand("TMS_TICKET.Update_DM_Nhan_Vien_CMS", Helper.OraDCDevOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_id", OracleDbType.Varchar2).Value = ID;
                myCommand.Parameters.Add("v_Ten_NV", OracleDbType.NVarchar2).Value = Ten_NV;
                myCommand.Parameters.Add("v_Phan_User", OracleDbType.NVarchar2).Value = Phan_User;
                myCommand.Parameters.Add("v_Phan_Tinh", OracleDbType.NVarchar2).Value = Phan_Tinh;
                myCommand.Parameters.Add("v_Nhom_Tinh", OracleDbType.Int32).Value = Nhom_Tinh;
                myCommand.Parameters.Add("v_Ghi_Chu", OracleDbType.NVarchar2).Value = Ghi_Chu;
                myCommand.Parameters.Add("v_isdelete", OracleDbType.Int32).Value = isdelete;
                myCommand.ExecuteNonQuery();
                R_update.Code = "00";
                R_update.Message = "Cập nhật dữ liệu thành công  !";
            }
            catch (Exception ex)
            {
                R_update.Code = "01";
                R_update.Message = ex.ToString();

            }

            return R_update;
        }


        #region Import 
        public ReturnResponseUpdate ImportTickets(string inputXml, string sessionId)
        {
            ReturnResponseUpdate response = new ReturnResponseUpdate();

            // Sử dụng using để đảm bảo kết nối được đóng đúng cách
            using (var connection = new OracleConnection(Helper.OraDCDevConnectionString))
            {
                try
                {
                    connection.Open(); // Mở kết nối
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Gọi stored procedure để chèn vào TMS_TICKET
                            using (var command1 = new OracleCommand("TMS_TICKET.Insert_List_Ticket", connection))
                            {
                                command1.CommandType = CommandType.StoredProcedure;
                                command1.Parameters.Add("P_INPUT", OracleDbType.Clob).Value = inputXml;
                                command1.ExecuteNonQuery();
                            }

                            // Gọi stored procedure để cập nhật thông tin nhân viên
                            using (var command2 = new OracleCommand("TMS_TICKET.Update_List_Ticket", connection))
                            {
                                command2.CommandType = CommandType.StoredProcedure;
                                command2.Parameters.Add("v_IdSession", OracleDbType.Varchar2).Value = sessionId;
                                // Thêm các tham số khác nếu cần
                                command2.ExecuteNonQuery();
                            }

                            // Thêm lệnh khác nếu cần
                            using (var command3 = new OracleCommand("TMS_TICKET.Insert_Ticket", connection))
                            {
                                command3.CommandType = CommandType.StoredProcedure;
                                //command3.Parameters.Add("v_IdSession", OracleDbType.Varchar2).Value = sessionId;
                                // Thêm các tham số khác nếu cần
                                command3.ExecuteNonQuery();
                            }

                            transaction.Commit(); // Cam kết giao dịch
                            response.Code = "00";
                            response.Message = "File xử lý thành công!";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Nếu có lỗi, rollback
                            response.Code = "01";
                            response.Message = $"Lỗi khi thực thi lệnh: {ex.Message}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Code = "02";
                    response.Message = $"Lỗi kết nối đến cơ sở dữ liệu: {ex.Message}";
                }
            }

            return response;
        }

        #endregion

        public ReturnTMS_TICKET TMS_TICKET_DETAIL(int startdate, int enddate, int time, int user, int nhomtinh, int id)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnTMS_TICKET _ReturnTMS_TICKET = new ReturnTMS_TICKET();
            var test = Helper.OraDCDevOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.List_TICKET", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Time", OracleDbType.Int32).Value = time;
                    myCommand.Parameters.Add("v_user", OracleDbType.Int32).Value = user;
                    myCommand.Parameters.Add("v_nhomtinh", OracleDbType.Int32).Value = nhomtinh;
                    myCommand.Parameters.Add("v_id", OracleDbType.Int32).Value = id;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<LIST_TICKET>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new LIST_TICKET();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.TTK_CODE = dr["TTK_CODE"].ToString();
                            oTMS_TICKET.TTK_TYPE = dr["TTK_TYPE"].ToString();
                            oTMS_TICKET.PARCEL_ID = dr["PARCEL_ID"].ToString();
                            oTMS_TICKET.TTK_STATUS = dr["TTK_STATUS"].ToString();
                            oTMS_TICKET.TTK_EXPIRATION = dr["TTK_EXPIRATION"].ToString();
                            oTMS_TICKET.NEXT_ORG = dr["NEXT_ORG"].ToString();
                            oTMS_TICKET.NEXT_ORG_NAME = dr["NEXT_ORG_NAME"].ToString();
                            oTMS_TICKET.MANAGED_ORG = dr["MANAGED_ORG"].ToString();
                            oTMS_TICKET.MANAGED_ORG = dr["MANAGED_ORG"].ToString();
                            oTMS_TICKET.REF_ORG = dr["REF_ORG"].ToString();
                            oTMS_TICKET.REF_ORG_NAME = dr["REF_ORG_NAME"].ToString();
                            oTMS_TICKET.CREATED_DATE = dr["CREATED_DATE"].ToString();
                            oTMS_TICKET.CREATED_ORG = dr["CREATED_ORG"].ToString();
                            oTMS_TICKET.ACT_CONTENT = dr["ACT_CONTENT"].ToString();
                            oTMS_TICKET.PHAN_TINH = dr["PHAN_TINH"].ToString();
                            oTMS_TICKET.TEN_NV = dr["TEN_NV"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.ListTICKET = listTMS_TICKET;
                    }
                    else
                    {
                        _ReturnTMS_TICKET.Code = "01";
                        _ReturnTMS_TICKET.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnTMS_TICKET.Code = "99";
                _ReturnTMS_TICKET.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnTMS_TICKET;
        }
    }

}
