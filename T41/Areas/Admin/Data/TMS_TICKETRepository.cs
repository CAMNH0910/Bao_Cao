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
        Convertion common = new Convertion();

        #region Check User
        public List<int> GetUserIdsByUserName(string username)
        {
            List<int> userIds = new List<int>();

            // Kết nối đến cơ sở dữ liệu Oracle
            using (var connection = new OracleConnection(Helper.OraDCDevConnectionString))
            {
                connection.Open();

                // Đảm bảo rằng tên stored procedure đúng
                using (var command = new OracleCommand("TMS_TICKET.CHECK_USER_ROLE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số đầu vào cho stored procedure
                    command.Parameters.Add(new OracleParameter("p_username", OracleDbType.Varchar2)).Value = username;

                    // Thêm tham số đầu ra là RefCursor
                    var outputParam = new OracleParameter("p_userids", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    // Thực thi stored procedure và lấy kết quả
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Lấy UserID từ kết quả trả về
                            if (reader["UserID"] != DBNull.Value)
                            {
                                userIds.Add(Convert.ToInt32(reader["UserID"])); // Chuyển đổi về kiểu int
                            }
                        }
                    }
                }
            }

            return userIds;
        }


        #endregion
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


        public IEnumerable<Get_Nhom_Tinh> Get_Nhom_Tinh(int id, string types)
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
                    cm.Parameters.Add(new OracleParameter("v_types", OracleDbType.Varchar2)).Value = types;
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

        public IEnumerable<Get_Nhan_Vien> Get_Nhan_Vien(string id)
        {
            List<Get_Nhan_Vien> listGetPosCode = null;
            Get_Nhan_Vien oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCDevOracleConnection;
                    cm.CommandText = Helper.SchemaName + "TMS_TICKET.Get_Nhan_Vien";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_id", OracleDbType.Varchar2)).Value = id;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<Get_Nhan_Vien>();
                        while (dr.Read())
                        {
                            oGetPosCode = new Get_Nhan_Vien();
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
        #endregion

        #region Danh mục nhân viên
        public ReturnTMS_TICKET TMS_TICKET_DETAIL(string tennv, int zone)
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
                    myCommand.Parameters.Add("v_zone", OracleDbType.Int32).Value = zone;
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
                            oTMS_TICKET.Id = dr["Id"].ToString();
                            oTMS_TICKET.MA_NV = dr["MA_NV"].ToString();
                            oTMS_TICKET.Ten_NV = dr["Ten_NV"].ToString();
                            oTMS_TICKET.Phan_User = dr["Phan_User"].ToString();
                            oTMS_TICKET.Tinh_Nhan = dr["Tinh_Nhan"].ToString();
                            oTMS_TICKET.Tinh_Tra = dr["Tinh_Tra"].ToString();
                            oTMS_TICKET.Khu_Vuc = dr["Khu_Vuc"].ToString();
                            oTMS_TICKET.Nhom_Tinh = dr["Nhom_Tinh"].ToString();
                            oTMS_TICKET.Ngay_Khoi_Tao = dr["Ngay_Khoi_Tao"].ToString();
                            oTMS_TICKET.Ngay_Ket_Thuc = dr["Ngay_Ket_Thuc"].ToString();
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

        public ReturnTMS_TICKET DM_Nhan_Vien(string tennv, int zone)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.DM_Nhan_Vien_CMS", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Ten_NV", OracleDbType.NVarchar2).Value = tennv;
                    myCommand.Parameters.Add("v_zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<DM_Nhan_Vien>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new DM_Nhan_Vien();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.Phan_User = dr["Phan_User"].ToString();
                            oTMS_TICKET.Khu_Vuc = dr["Khu_Vuc"].ToString();
                            oTMS_TICKET.MA_NV = dr["MA_NV"].ToString();
                            oTMS_TICKET.Ten_NV = dr["Ten_NV"].ToString();
                            oTMS_TICKET.Tinh_Nhan = dr["Tinh_Nhan"].ToString();
                            oTMS_TICKET.Tinh_Tra = dr["Tinh_Tra"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.ListDM_Nhan_Vien = listTMS_TICKET;
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

        public ReturnTMS_TICKET TMS_TICKET_DETAIL_SUA(int id)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.Lay_NV", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_id", OracleDbType.Int32).Value = id;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<TMS_TICKET_Sua>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new TMS_TICKET_Sua();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.Id = dr["Id"].ToString();
                            oTMS_TICKET.MA_NV = dr["MA_NV"].ToString();
                            oTMS_TICKET.Ten_NV = dr["Ten_NV"].ToString();
                            oTMS_TICKET.Phan_User = dr["Phan_User"].ToString();
                            oTMS_TICKET.Tinh_Nhan = dr["Tinh_Nhan"].ToString();
                            oTMS_TICKET.Tinh_Tra = dr["Tinh_Tra"].ToString();
                            oTMS_TICKET.Khu_Vuc = dr["Khu_Vuc"].ToString();
                            oTMS_TICKET.Nhom_Tinh = dr["Nhom_Tinh"].ToString();
                            oTMS_TICKET.Ngay_Khoi_Tao = dr["Ngay_Khoi_Tao"].ToString();
                            oTMS_TICKET.Ngay_Ket_Thuc = dr["Ngay_Ket_Thuc"].ToString();
                            oTMS_TICKET.Ghi_Chu = dr["Ghi_Chu"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.ListTMS_TICKET_Sua = listTMS_TICKET;
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

        public ReturnResponseUpdate Khoa(int id,string types)
        {

            ReturnResponseUpdate R_update = new ReturnResponseUpdate();
            try
            {

                OracleCommand myCommand = new OracleCommand("TMS_TICKET.Khoa_Nhan_Vien_CMS", Helper.OraDCDevOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_id", OracleDbType.Int32).Value = id;
                myCommand.Parameters.Add("v_types", OracleDbType.Varchar2).Value = types;
                myCommand.ExecuteNonQuery();
                R_update.Code = "00";
                R_update.Message = "Cập nhật dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_update.Code = "01";
                R_update.Message = ex.ToString();

            }

            return R_update;
        }
       
        public bool Add(TMS_TICKET_Sua model)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("TMS_TICKET.Insert_DM_Nhan_Vien_CMS", Helper.OraDCDevOracleConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_MA_NV", OracleDbType.Varchar2).Value = model.MA_NV;
                    cmd.Parameters.Add("v_Ten_NV", OracleDbType.Varchar2).Value = model.Ten_NV;
                    cmd.Parameters.Add("v_Phan_User", OracleDbType.Varchar2).Value = model.Phan_User;
                    cmd.Parameters.Add("v_Tinh_nhan", OracleDbType.Varchar2).Value = model.Tinh_Nhan;
                    cmd.Parameters.Add("v_Tinh_Tra", OracleDbType.Varchar2).Value = model.Tinh_Tra;
                    cmd.Parameters.Add("v_Tinh_Tra", OracleDbType.Varchar2).Value = model.Khu_Vuc;
                    cmd.Parameters.Add("v_Nhom_Tinh", OracleDbType.Int32).Value = model.Nhom_Tinh;
                    cmd.Parameters.Add("v_ngay_tao", OracleDbType.NVarchar2).Value = model.Ngay_Khoi_Tao;
                    cmd.Parameters.Add("v_ngay_kt", OracleDbType.NVarchar2).Value = model.Ngay_Ket_Thuc;
                    cmd.Parameters.Add("v_Ghi_Chu", OracleDbType.Varchar2).Value = model.Ghi_Chu;

                    cmd.ExecuteNonQuery();
                }

                return true; // Thêm thành công
            }
            catch
            {
                return false; // Thêm thất bại
            }
        }
        public bool Edit(TMS_TICKET_Sua model)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("TMS_TICKET.Update_DM_Nhan_Vien_CMS", Helper.OraDCDevOracleConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = model.Id;
                    cmd.Parameters.Add("v_MA_NV", OracleDbType.Varchar2).Value = model.MA_NV;
                    cmd.Parameters.Add("v_Ten_NV", OracleDbType.Varchar2).Value = model.Ten_NV;
                    cmd.Parameters.Add("v_Phan_User", OracleDbType.Varchar2).Value = model.Phan_User;
                    cmd.Parameters.Add("v_Tinh_nhan", OracleDbType.Varchar2).Value = model.Tinh_Nhan;
                    cmd.Parameters.Add("v_Tinh_Tra", OracleDbType.Varchar2).Value = model.Tinh_Tra;
                    cmd.Parameters.Add("v_Tinh_Tra", OracleDbType.Varchar2).Value = model.Khu_Vuc;
                    cmd.Parameters.Add("v_Nhom_Tinh", OracleDbType.Int32).Value = model.Nhom_Tinh;
                    cmd.Parameters.Add("v_ngay_tao", OracleDbType.NVarchar2).Value = model.Ngay_Khoi_Tao;
                    cmd.Parameters.Add("v_ngay_kt", OracleDbType.NVarchar2).Value = model.Ngay_Ket_Thuc;
                    cmd.Parameters.Add("v_Ghi_Chu", OracleDbType.Varchar2).Value = model.Ghi_Chu;

                    cmd.ExecuteNonQuery();
                }

                return true; // Thêm thành công
            }
            catch
            {
                return false; // Thêm thất bại
            }
        }
        #endregion

        #region Danh mục lượt rà soát
        public ReturnTMS_TICKET DM_Ra_Soat(int startdate,string user)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.ListDM_Ra_Soat", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_THOI_GIAN", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_user", OracleDbType.Varchar2).Value = user;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<DM_Ra_Soat>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new DM_Ra_Soat();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.LUOT = dr["LUOT"].ToString();
                            oTMS_TICKET.THOI_GIAN = dr["THOI_GIAN"].ToString();
                            oTMS_TICKET.GIO = dr["GIO"].ToString();
                            oTMS_TICKET.USERS = dr["USERS"].ToString();
                            oTMS_TICKET.USERS_IMPORT = dr["USERS_IMPORT"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.ListDM_Ra_Soat = listTMS_TICKET;
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
        #endregion
        #region Quản lý phân công hỗ trợ
        #region Quản lý xin nghỉ
        public ReturnTMS_TICKET QL_PhanCong_HT(int startdate, int enddate, string seach)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.List_PC_HoTro", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_user", OracleDbType.Varchar2).Value = seach;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<QL_PhanCong_HT>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new QL_PhanCong_HT();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.Id = dr["Id"].ToString();
                            oTMS_TICKET.NGAY = dr["NGAY"].ToString();
                            oTMS_TICKET.HINH_THUC = dr["HINH_THUC"].ToString();
                            oTMS_TICKET.CA_LAM = dr["CA_LAM"].ToString();
                            oTMS_TICKET.NOI_DUNG = dr["NOI_DUNG"].ToString();
                            oTMS_TICKET.PHE_DUYET = dr["PHE_DUYET"].ToString();
                            oTMS_TICKET.NGUOI_TAO = dr["NGUOI_TAO"].ToString();
                            oTMS_TICKET.DATECREATE = dr["DATECREATE"].ToString();
                            oTMS_TICKET.NGUOI_DUYET = dr["NGUOI_DUYET"].ToString();
                            oTMS_TICKET.DATEUPDATE = dr["DATEUPDATE"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.ListQL_PhanCong_HT = listTMS_TICKET;
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
        public bool Duyet(string ids, string users)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("TMS_TICKET.Duyet", Helper.OraDCDevOracleConnection))
                {
                    // Đặt kiểu lệnh là stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào câu lệnh SQL, để truyền giá trị vào stored procedure
                    cmd.Parameters.Add("v_ids", OracleDbType.Varchar2).Value = ids;
                    cmd.Parameters.Add("v_users", OracleDbType.Varchar2).Value = users;

                    // Thực thi câu lệnh
                    cmd.ExecuteNonQuery();
                }

                return true; // Thành công
            }
            catch (Exception ex)
            {
                // Ghi log hoặc thông báo lỗi (nếu cần)
                return false; // Thất bại
            }
        }
        public bool Xin_Nghi(PhanCong_Xin_Nghi model,string NGUOI_TAO)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("TMS_TICKET.Add_Xin_Nghi", Helper.OraDCDevOracleConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_NGAY", OracleDbType.Varchar2).Value = model.NGAY;
                    cmd.Parameters.Add("v_HINH_THUC", OracleDbType.Varchar2).Value = model.HINH_THUC;
                    cmd.Parameters.Add("v_CA_LAM", OracleDbType.Varchar2).Value = model.CA_LAM;
                    cmd.Parameters.Add("v_NOI_DUNG", OracleDbType.Varchar2).Value = model.NOI_DUNG;
                    cmd.Parameters.Add("v_NGUOI_TAO", OracleDbType.Varchar2).Value = NGUOI_TAO;
                    cmd.ExecuteNonQuery();
                }

                return true; // Thêm thành công
            }
            catch
            {
                return false; // Thêm thất bại
            }
        }
        #endregion
        #region Tổng hợp xin nghỉ
        public ReturnTMS_TICKET THPhanCong_HT(int startdate, int enddate, string seach)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.List_HoTro", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_user", OracleDbType.Varchar2).Value = seach;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<THPhanCong_HT>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new THPhanCong_HT();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.Id = dr["Id"].ToString();
                            oTMS_TICKET.NGUOI_TAO = dr["NGUOI_TAO"].ToString();
                            oTMS_TICKET.So_Lan = dr["So_Lan"].ToString();
                            oTMS_TICKET.PHE_DUYET = dr["PHE_DUYET"].ToString();
                            oTMS_TICKET.NGUOI_DUYET = dr["NGUOI_DUYET"].ToString();
                            oTMS_TICKET.DATEUPDATE = dr["DATEUPDATE"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.ListTHPhanCong_HT = listTMS_TICKET;
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
        public bool Phan_Cong(string soHS, int newEmployeeId)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("TMS_TICKET.Phan_Cong", Helper.OraDCDevOracleConnection))
                {
                    // Đặt kiểu lệnh là stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào câu lệnh SQL, để truyền giá trị vào stored procedure
                    cmd.Parameters.Add("v_ids", OracleDbType.Varchar2).Value = soHS;
                    cmd.Parameters.Add("v_users", OracleDbType.Int32).Value = newEmployeeId;

                    // Thực thi câu lệnh
                    cmd.ExecuteNonQuery();
                }

                return true; // Thành công
            }
            catch (Exception ex)
            {
                // Ghi log hoặc thông báo lỗi (nếu cần)
                return false; // Thất bại
            }
        }
        #endregion
        #region Phân công tạm thời
        public ReturnTMS_TICKET PhanCong_HT(int startdate, int enddate, string seach, string mabg)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.List_Phan_Cong", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_user", OracleDbType.Varchar2).Value = seach;
                    myCommand.Parameters.Add("v_mabg", OracleDbType.Varchar2).Value = mabg;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<PhanCong_HT>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new PhanCong_HT();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.SO_HS = dr["SO_HS"].ToString();
                            oTMS_TICKET.MA_BG = dr["MA_BG"].ToString();
                            oTMS_TICKET.ID_NV = dr["ID_NV"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.ListPhanCong_HT = listTMS_TICKET;
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
        #endregion
        #endregion
        #region Import 
        //Import dữ liệu
        public ReturnResponseUpdate ImportTickets(string inputXml, string sessionId, string user,string luot)
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
                            using (var command3 = new OracleCommand("TMS_TICKET.Check_List_Ticket", connection))
                            {
                                command3.CommandType = CommandType.StoredProcedure;
                                command3.Parameters.Add("v_IdSession", OracleDbType.Varchar2).Value = sessionId;

                                // Thêm các tham số khác nếu cần
                                command3.ExecuteNonQuery();
                            }

                            using (var command4 = new OracleCommand("TMS_TICKET.TICKET_History", connection))
                            {
                                command4.CommandType = CommandType.StoredProcedure;
                                command4.Parameters.Add("v_IdSession", OracleDbType.Varchar2).Value = sessionId;

                                // Thêm các tham số khác nếu cần
                                command4.ExecuteNonQuery();
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

        public ReturnResponseUpdate SaceTickets(int startdate, string user)
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
                            // Thực thi lệnh gọi Stored Procedure Insert_Ticket
                            using (var command4 = new OracleCommand("TMS_TICKET.Insert_Ticket", connection))
                            {
                                command4.CommandType = CommandType.StoredProcedure;

                                // Sử dụng kiểu dữ liệu phù hợp với startdate
                                command4.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                                command4.Parameters.Add("v_Users", OracleDbType.Varchar2).Value = user;
                                // Thêm các tham số khác nếu cần
                                command4.ExecuteNonQuery();
                            }

                            transaction.Commit(); // Cam kết giao dịch
                            response.Code = "00";
                            response.Message = "Ghi dữ liệu thành công!";
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
        //Update nhân viên theo hồ sơ
        public bool UpdateTicketTrangThai(string itemcode, string Item, string Ly_Do, string Ly_Do_Khac, string types)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("TMS_TICKET.Update_Trang_Thai", Helper.OraDCDevOracleConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_itemcode", OracleDbType.Varchar2).Value = itemcode;
                    //cmd.Parameters.Add("v_trangthai", OracleDbType.Int32).Value = trangthai;
                    cmd.Parameters.Add("v_Item", OracleDbType.Varchar2).Value = Item;
                    cmd.Parameters.Add("v_lydo", OracleDbType.Varchar2).Value = Ly_Do;
                    cmd.Parameters.Add("v_lydokhac", OracleDbType.Varchar2).Value = Ly_Do_Khac;
                    cmd.Parameters.Add("v_type", OracleDbType.Varchar2).Value = types;
                    cmd.ExecuteNonQuery();
                }

                return true; // Thêm thành công
            }
            catch
            {
                return false; // Thêm thất bại
            }
        }

        //Lấy nhân viên theo hồ sơ
        public ReturnTMS_TICKET GetTicketDetails(string So_HS,string types)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.Lay_Itemcode", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_itemcode", OracleDbType.Varchar2).Value = So_HS;
                    myCommand.Parameters.Add("v_type", OracleDbType.Varchar2).Value = types;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<Lay_Itemcode>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new Lay_Itemcode();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.So_HS = dr["So_HS"].ToString();
                            oTMS_TICKET.Ma_BG = dr["Ma_BG"].ToString();
                            oTMS_TICKET.Trang_Thai = dr["Trang_Thai"].ToString();
                            oTMS_TICKET.ID_NV = dr["ID_NV"].ToString();
                            oTMS_TICKET.Ten_NV = dr["Ten_NV"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.ListLay_Itemcode = listTMS_TICKET;
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

        #region Báo cáo dành cho người quản lý

        //Báo cáo chi tiết
        public ReturnTMS_TICKET TICKET_DETAIL(int startdate, int Id,string luot,string types)
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
                    myCommand.Parameters.Add("v_id", OracleDbType.Int32).Value = Id;
                    myCommand.Parameters.Add("v_luot", OracleDbType.Varchar2).Value = luot;
                    myCommand.Parameters.Add("v_types", OracleDbType.Varchar2).Value = types;
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
                            oTMS_TICKET.So_HS = dr["So_HS"].ToString();
                            oTMS_TICKET.Ma_BG = dr["Ma_BG"].ToString();
                            oTMS_TICKET.Ngay_Tao = dr["Ngay_Tao"].ToString();
                            oTMS_TICKET.Trang_Thai = dr["Trang_Thai"].ToString();
                            oTMS_TICKET.Ma_DV_Chu_Tri = dr["Ma_DV_Chu_Tri"].ToString();
                            oTMS_TICKET.Ten_DV_Chu_Tri = dr["Ten_DV_Chu_Tri"].ToString();
                            oTMS_TICKET.Ngay_Het_han = dr["Ngay_Het_han"].ToString();
                            oTMS_TICKET.Tinh_Nhan = dr["Tinh_Nhan"].ToString();
                            oTMS_TICKET.Tinh_Tra = dr["Tinh_Tra"].ToString();
                            oTMS_TICKET.TEN_NV = dr["TEN_NV"].ToString();
                            oTMS_TICKET.STATUS = dr["STATUS"].ToString();
                            oTMS_TICKET.STATUS_HS = dr["STATUS_HS"].ToString();
                            oTMS_TICKET.Luot = dr["Luot"].ToString();
                            oTMS_TICKET.USERS = dr["USERS"].ToString();
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
        //Báo cáo tổng hợp
        public ReturnTMS_TICKET TMS_TICKET_DETAIL_TH(int startdate, int luot, string user)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.List_TICKET_TH", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_luot", OracleDbType.Int32).Value = luot;
                    myCommand.Parameters.Add("v_user", OracleDbType.Varchar2).Value = user;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<LIST_TICKET_TH>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new LIST_TICKET_TH();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.Khu_Vuc = dr["Khu_Vuc"].ToString();
                            oTMS_TICKET.Nhom_Tinh = dr["Nhom_Tinh"].ToString();
                            oTMS_TICKET.Id = dr["Id"].ToString();
                            oTMS_TICKET.Ten_NV = dr["Ten_NV"].ToString();
                            oTMS_TICKET.Tong_So = dr["Tong_So"].ToString();
                            oTMS_TICKET.HS_Moi = dr["HS_Moi"].ToString();
                            oTMS_TICKET.HS_Cu = dr["HS_Cu"].ToString();
                            oTMS_TICKET.DXL = dr["DXL"].ToString();
                            oTMS_TICKET.DCKQ = dr["DCKQ"].ToString();
                            oTMS_TICKET.Dong = dr["Dong"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.List_TICKET_TH = listTMS_TICKET;
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

        #endregion
        #endregion


        #region Báo cáo dành cho người dùng
        public ReturnTMS_TICKET TICKET_DETAIL_ND(int startdate, int enddate, string seach, int trangthai, string mabg)
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
                    OracleCommand myCommand = new OracleCommand("TMS_TICKET.List_TICKET_ND", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Seach", OracleDbType.Varchar2).Value = seach;
                    myCommand.Parameters.Add("v_trangthai", OracleDbType.Int32).Value = trangthai;
                    myCommand.Parameters.Add("v_mabg", OracleDbType.Varchar2).Value = mabg;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listTMS_TICKET = new List<LIST_TICKET_ND>();
                        while (dr.Read())
                        {

                            var oTMS_TICKET = new LIST_TICKET_ND();
                            oTMS_TICKET.STT = a++;
                            oTMS_TICKET.So_HS = dr["So_HS"].ToString();
                            oTMS_TICKET.Ma_BG = dr["Ma_BG"].ToString();
                            oTMS_TICKET.Ngay_Tao = dr["Ngay_Tao"].ToString();
                            oTMS_TICKET.Trang_Thai = dr["Trang_Thai"].ToString();
                            oTMS_TICKET.Ma_DV_Chu_Tri = dr["Ma_DV_Chu_Tri"].ToString();
                            oTMS_TICKET.Ngay_Het_han = dr["Ngay_Het_han"].ToString();
                            oTMS_TICKET.TEN_NV = dr["TEN_NV"].ToString();
                            oTMS_TICKET.UPDATE_TT = dr["UPDATE_TT"].ToString();
                            oTMS_TICKET.UPDATE_DV = dr["UPDATE_DV"].ToString();
                            oTMS_TICKET.STATUS = dr["STATUS"].ToString();
                            oTMS_TICKET.STATUS_HS = dr["STATUS_HS"].ToString();
                            listTMS_TICKET.Add(oTMS_TICKET);
                        }
                        _ReturnTMS_TICKET.Code = "00";
                        _ReturnTMS_TICKET.Message = "Lấy dữ liệu thành công.";
                        _ReturnTMS_TICKET.List_TICKET_ND = listTMS_TICKET;
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
        #endregion
    }

}
