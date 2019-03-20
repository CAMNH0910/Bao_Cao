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
    public class CheckDealTransferRepository
    {
        Convertion common = new Convertion();

        #region GETPOSCODE_GIAO_DICH
        //Lấy mã bưu cục giao dịch dưới DB
        public String GETPOSCODE_GIAO_DICH(int id_don_vi)
        {
            //string LISTPOSTCODE_GD = "<option value=\"0\">Tất Cả</option>";
            string LISTPOSTCODE_GD = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraEVComDevOracleConnection;
                    cm.CommandText = Helper.SchemaName + "pk_telepost.V2_BC_GIAO_DICH_DS";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("v_Loai", OracleDbType.Int32).Value = 0;
                    cm.Parameters.Add("v_Mien", OracleDbType.Int32).Value = id_don_vi;
                    cm.Parameters.Add("v_cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTPOSTCODE_GD += "<option value='" + dr["MA_BC"].ToString() + "'>" + dr["MA_BC"].ToString() + '-' + dr["TEN_BC"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CheckDealTransferRepository.LISTPOSTCODE_GD" + ex.Message);
            }

            return LISTPOSTCODE_GD;
        }
        #endregion


        // Phần lấy dữ liệu CHECK_DEAL_TRANSFER_DETAIL
        #region CHECK_DEAL_TRANSFER_DETAIL          
        public ReturnCheckDealTransfer CHECK_DEAL_TRANSFER_DETAIL(int fromdate , int todate, int ma_bc_khai_thac)
        {
            int STT = 1;
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnCheckDealTransfer _ReturnCheckDealTransfer = new ReturnCheckDealTransfer();
            List<CheckDealTransfer_Detail> listCheckDealTransferDetail = null;
            CheckDealTransfer_Detail oCheckDealTransferDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("pk_telepost.V2_C_THU_DI_DOI_SOAT_NOTIFY_BC", Helper.OraEVComDevOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_TU_NGAY", OracleDbType.Int32).Value = fromdate;
                    myCommand.Parameters.Add("v_DEN_NGAY", OracleDbType.Int32).Value = todate;
                    myCommand.Parameters.Add("v_MA_BC_KHAI_THAC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add(new OracleParameter("v_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listCheckDealTransferDetail = new List<CheckDealTransfer_Detail>();
                        while (dr.Read())
                        {
                            oCheckDealTransferDetail = new CheckDealTransfer_Detail();
                            oCheckDealTransferDetail.STT = STT++;
                            oCheckDealTransferDetail.ID_CHUYEN_THU =  dr["ID_CHUYEN_THU"].ToString();
                            oCheckDealTransferDetail.MA_BC_KHAI_THAC =  dr["MA_BC_KHAI_THAC"].ToString();
                            oCheckDealTransferDetail.SO_CHUYEN_THU =  dr["SO_CHUYEN_THU"].ToString();
                            oCheckDealTransferDetail.NGAY_DONG =  dr["NGAY_DONG"].ToString();
                            oCheckDealTransferDetail.GIO_DONG =  dr["GIO_DONG"].ToString();
                            oCheckDealTransferDetail.TONG_SO_TUI =  dr["TONG_SO_TUI"].ToString();
                            oCheckDealTransferDetail.TONG_SO_BP = dr["TONG_SO_BP"].ToString() == "" ? 0 : Convert.ToInt32(dr["TONG_SO_BP"].ToString());

                            oCheckDealTransferDetail.TONG_KL =  dr["TONG_KL"].ToString();
                            oCheckDealTransferDetail.TONG_KLBP =  dr["TONG_KLBP"].ToString();
                            oCheckDealTransferDetail.TONG_CUOC_COD =  dr["TONG_CUOC_COD"].ToString();
                            oCheckDealTransferDetail.TONG_CUOC_DV =  dr["TONG_CUOC_DV"].ToString();
                            oCheckDealTransferDetail.TONG_CUOC =  dr["TONG_CUOC"].ToString();
                            oCheckDealTransferDetail.HH_PHAT_HANH =  dr["HH_PHAT_HANH"].ToString();
                            oCheckDealTransferDetail.HH_PHAT_TRA =  dr["HH_PHAT_TRA"].ToString();

                            oCheckDealTransferDetail.NGAY_HE_THONG = dr["NGAY_HE_THONG"].ToString();
                            
                            oCheckDealTransferDetail.IP_MAY_CHU = dr["IP_MAY_CHU"].ToString();
                            oCheckDealTransferDetail.MAILTRIP_KEY =  dr["MAILTRIP_KEY"].ToString();
                            oCheckDealTransferDetail.TONG_SO_BP_DOI_SOAT = dr["TONG_SO_BP_DOI_SOAT"].ToString() == "" ? 0 : Convert.ToInt32(dr["TONG_SO_BP_DOI_SOAT"].ToString());

                            listCheckDealTransferDetail.Add(oCheckDealTransferDetail);

                        }
                        _ReturnCheckDealTransfer.Code = "00";
                        _ReturnCheckDealTransfer.Message = "Lấy dữ liệu thành công.";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_Report = listCheckDealTransferDetail;
                    }
                    else
                    {
                        _ReturnCheckDealTransfer.Code = "01";
                        _ReturnCheckDealTransfer.Message = "Không có dữ liệu";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_Report = listCheckDealTransferDetail;
                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnCheckDealTransfer.Code = "99";
                _ReturnCheckDealTransfer.Message = "Lỗi xử lý dữ liệu";
                _ReturnCheckDealTransfer.Total = 0;
                _ReturnCheckDealTransfer.ListCheckDealTransfer_Report = listCheckDealTransferDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CheckDealTransferRepository.CHECK_DEAL_TRANSFER_DETAIL" + ex.Message);
            }
            return _ReturnCheckDealTransfer;
        }

        #endregion


        //Export Excel
        // Phần lấy dữ liệu CHECK_DEAL_TRANSFER_EXCEL_DETAIL
        #region CHECK_DEAL_TRANSFER_EXCEL_DETAIL          
        public ReturnCheckDealTransfer CHECK_DEAL_TRANSFER_EXCEL_DETAIL(int fromdate, int todate, int ma_bc_khai_thac)
        {
            int STT = 1;
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnCheckDealTransfer _ReturnCheckDealTransfer = new ReturnCheckDealTransfer();
            List<CheckDealTransfer_Excel_Detail> listCheckDealTransferDetail = null;
            CheckDealTransfer_Excel_Detail oCheckDealTransferDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("pk_telepost.V2_C_THU_DI_DOI_SOAT_NOTIFY_BC", Helper.OraEVComDevOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_TU_NGAY", OracleDbType.Int32).Value = fromdate;
                    myCommand.Parameters.Add("v_DEN_NGAY", OracleDbType.Int32).Value = todate;
                    myCommand.Parameters.Add("v_MA_BC_KHAI_THAC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add(new OracleParameter("v_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listCheckDealTransferDetail = new List<CheckDealTransfer_Excel_Detail>();
                        while (dr.Read())
                        {
                            oCheckDealTransferDetail = new CheckDealTransfer_Excel_Detail();
                            oCheckDealTransferDetail.STT = STT++;
                            oCheckDealTransferDetail.ID_CHUYEN_THU = dr["ID_CHUYEN_THU"].ToString();
                            oCheckDealTransferDetail.MA_BC_KHAI_THAC = dr["MA_BC_KHAI_THAC"].ToString();
                            oCheckDealTransferDetail.SO_CHUYEN_THU = dr["SO_CHUYEN_THU"].ToString();
                            oCheckDealTransferDetail.NGAY_DONG = dr["NGAY_DONG"].ToString();
                            oCheckDealTransferDetail.GIO_DONG = dr["GIO_DONG"].ToString();
                            oCheckDealTransferDetail.TONG_SO_TUI = dr["TONG_SO_TUI"].ToString();
                            
                            oCheckDealTransferDetail.TONG_KL = dr["TONG_KL"].ToString();
                            oCheckDealTransferDetail.TONG_KLBP = dr["TONG_KLBP"].ToString();
                            oCheckDealTransferDetail.TONG_CUOC_COD = dr["TONG_CUOC_COD"].ToString();
                            oCheckDealTransferDetail.TONG_CUOC_DV = dr["TONG_CUOC_DV"].ToString();
                            oCheckDealTransferDetail.TONG_CUOC = dr["TONG_CUOC"].ToString();
                            oCheckDealTransferDetail.HH_PHAT_HANH = dr["HH_PHAT_HANH"].ToString();
                            oCheckDealTransferDetail.HH_PHAT_TRA = dr["HH_PHAT_TRA"].ToString();

                            
                            oCheckDealTransferDetail.IP_MAY_CHU = dr["IP_MAY_CHU"].ToString();
                            oCheckDealTransferDetail.TONG_SO_BP = dr["TONG_SO_BP"].ToString() == "" ? 0 : Convert.ToInt32(dr["TONG_SO_BP"].ToString());
                            oCheckDealTransferDetail.TONG_SO_BP_DOI_SOAT = dr["TONG_SO_BP_DOI_SOAT"].ToString() == "" ? 0 : Convert.ToInt32(dr["TONG_SO_BP_DOI_SOAT"].ToString());


                            listCheckDealTransferDetail.Add(oCheckDealTransferDetail);

                        }
                        _ReturnCheckDealTransfer.Code = "00";
                        _ReturnCheckDealTransfer.Message = "Lấy dữ liệu thành công.";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_Excel_Report = listCheckDealTransferDetail;
                    }
                    else
                    {
                        _ReturnCheckDealTransfer.Code = "01";
                        _ReturnCheckDealTransfer.Message = "Không có dữ liệu";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_Excel_Report = listCheckDealTransferDetail;
                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnCheckDealTransfer.Code = "99";
                _ReturnCheckDealTransfer.Message = "Lỗi xử lý dữ liệu";
                _ReturnCheckDealTransfer.Total = 0;
                _ReturnCheckDealTransfer.ListCheckDealTransfer_Excel_Report = listCheckDealTransferDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CheckDealTransferRepository.CHECK_DEAL_TRANSFER_DETAIL" + ex.Message);
            }
            return _ReturnCheckDealTransfer;
        }

        #endregion

        // Phần lấy dữ liệu CHECK_DEAL_TRANSFER_MODAL_DETAIL
        #region CHECK_DEAL_TRANSFER_MODAL_DETAIL          
        public ReturnCheckDealTransfer CHECK_DEAL_TRANSFER_MODAL_DETAIL(string id_chuyen_thu, int? ma_bc_khai_thac, Int64? mailtrip_key)
        {
            int STT = 1;
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnCheckDealTransfer _ReturnCheckDealTransfer = new ReturnCheckDealTransfer();
            List<CheckDealTransfer_Detail_Modal> listCheckDealTransferDetail = null;
            CheckDealTransfer_Detail_Modal oCheckDealTransferDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("pk_telepost.V2_E2_DI_BY_DK_DS", Helper.OraEVComDevOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_ID_CHUYEN_THU", OracleDbType.Varchar2).Value = id_chuyen_thu;
                    myCommand.Parameters.Add("v_MA_BC_KHAI_THAC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add("v_MAILTRIP_KEY", OracleDbType.Int32).Value = mailtrip_key;
                    myCommand.Parameters.Add(new OracleParameter("v_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    //myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listCheckDealTransferDetail = new List<CheckDealTransfer_Detail_Modal>();
                        while (dr.Read())
                        {
                            oCheckDealTransferDetail = new CheckDealTransfer_Detail_Modal();
                            oCheckDealTransferDetail.STT = STT++;
                            oCheckDealTransferDetail.ID_DUONG_THU = dr["ID_DUONG_THU"].ToString();
                            oCheckDealTransferDetail.ID_CHUYEN_THU = dr["ID_CHUYEN_THU"].ToString();
                            oCheckDealTransferDetail.ID_E2 = dr["ID_E2"].ToString();
                            oCheckDealTransferDetail.ID_CA = dr["ID_CA"].ToString();
                            oCheckDealTransferDetail.MA_BC_KHAI_THAC = dr["MA_BC_KHAI_THAC"].ToString();
                            oCheckDealTransferDetail.TUI_SO = dr["TUI_SO"].ToString();
                            oCheckDealTransferDetail.TONG_SO_BP = dr["TONG_SO_BP"].ToString();
                            oCheckDealTransferDetail.MAILTRIP_KEY = dr["MAILTRIP_KEY"].ToString();


                            listCheckDealTransferDetail.Add(oCheckDealTransferDetail);

                        }
                        _ReturnCheckDealTransfer.Code = "00";
                        _ReturnCheckDealTransfer.Message = "Lấy dữ liệu thành công.";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_Modal_Report = listCheckDealTransferDetail;
                    }
                    else
                    {
                        _ReturnCheckDealTransfer.Code = "01";
                        _ReturnCheckDealTransfer.Message = "Không có dữ liệu";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_Modal_Report = listCheckDealTransferDetail;
                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnCheckDealTransfer.Code = "99";
                _ReturnCheckDealTransfer.Message = "Lỗi xử lý dữ liệu";
                _ReturnCheckDealTransfer.Total = 0;
                _ReturnCheckDealTransfer.ListCheckDealTransfer_Modal_Report = listCheckDealTransferDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CheckDealTransferRepository.CHECK_DEAL_TRANSFER_MODAL_DETAIL" + ex.Message);
            }
            return _ReturnCheckDealTransfer;
        }

        #endregion

        // Phần lấy dữ liệu CHECK_DEAL_TRANSFER_BY_ID_E2_DETAIL
        #region CHECK_DEAL_TRANSFER_BY_ID_E2_DETAIL          
        public ReturnCheckDealTransfer CHECK_DEAL_TRANSFER_BY_ID_E2_DETAIL(string id_e2, int? ma_bc_khai_thac, Int64? mailtrip_key)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnCheckDealTransfer _ReturnCheckDealTransfer = new ReturnCheckDealTransfer();
            List<CheckDealTransfer_Detail_NewTab> listCheckDealTransferDetail = null;
            //CheckDealTransfer_Detail_NewTab oCheckDealTransferDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("pk_telepost.V2_E1_DI_BY_ID_E2_DS", Helper.OraEVComDevOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_ID_E2", OracleDbType.Varchar2).Value = id_e2;
                    myCommand.Parameters.Add("v_MA_BC_KHAI_THAC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add("v_MAILTRIP_KEY", OracleDbType.Int32).Value = mailtrip_key;
                    myCommand.Parameters.Add(new OracleParameter("v_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    listCheckDealTransferDetail = ConvertListToDataTable.DataTableToList<CheckDealTransfer_Detail_NewTab>(da);
                    if (listCheckDealTransferDetail != null && listCheckDealTransferDetail.Count != 0)
                    {
                        _ReturnCheckDealTransfer.Code = "00";
                        _ReturnCheckDealTransfer.Message = "Lấy dữ liệu thành công.";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_NewTab_Report = listCheckDealTransferDetail;

                    }
                    
                    else
                    {
                        _ReturnCheckDealTransfer.Code = "01";
                        _ReturnCheckDealTransfer.Message = "Không có dữ liệu";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_NewTab_Report = listCheckDealTransferDetail;
                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnCheckDealTransfer.Code = "99";
                _ReturnCheckDealTransfer.Message = "Lỗi xử lý dữ liệu";
                _ReturnCheckDealTransfer.Total = 0;
                _ReturnCheckDealTransfer.ListCheckDealTransfer_NewTab_Report = listCheckDealTransferDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CheckDealTransferRepository.CHECK_DEAL_TRANSFER_BY_ID_E2_DETAIL" + ex.Message);
            }
            return _ReturnCheckDealTransfer;
        }

        #endregion

        // Phần lấy dữ liệu CHECK_DEAL_TRANSFER_BY_ID_CT_DETAIL
        #region CHECK_DEAL_TRANSFER_BY_ID_CT_DETAIL          
        public ReturnCheckDealTransfer CHECK_DEAL_TRANSFER_BY_ID_CT_DETAIL(string id_chuyen_thu, int? ma_bc_khai_thac, Int64? mailtrip_key)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnCheckDealTransfer _ReturnCheckDealTransfer = new ReturnCheckDealTransfer();
            List<CheckDealTransfer_Detail_NewTab> listCheckDealTransferDetail = null;
            //CheckDealTransfer_Detail_NewTab oCheckDealTransferDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    OracleCommand myCommand = new OracleCommand("pk_telepost.V2_E1_DI_BY_ID_DK_DS", Helper.OraEVComDevOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_ID_CHUYEN_THU", OracleDbType.Varchar2).Value = id_chuyen_thu;
                    myCommand.Parameters.Add("v_MA_BC_KHAI_THAC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add("v_MAILTRIP_KEY", OracleDbType.Int32).Value = mailtrip_key;
                    myCommand.Parameters.Add(new OracleParameter("v_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    listCheckDealTransferDetail = ConvertListToDataTable.DataTableToList<CheckDealTransfer_Detail_NewTab>(da);
                    if (listCheckDealTransferDetail != null && listCheckDealTransferDetail.Count != 0)
                    {
                        _ReturnCheckDealTransfer.Code = "00";
                        _ReturnCheckDealTransfer.Message = "Lấy dữ liệu thành công.";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_NewTab_Report = listCheckDealTransferDetail;

                    }

                    else
                    {
                        _ReturnCheckDealTransfer.Code = "01";
                        _ReturnCheckDealTransfer.Message = "Không có dữ liệu";
                        _ReturnCheckDealTransfer.Total = listCheckDealTransferDetail.Count();
                        _ReturnCheckDealTransfer.ListCheckDealTransfer_NewTab_Report = listCheckDealTransferDetail;
                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnCheckDealTransfer.Code = "99";
                _ReturnCheckDealTransfer.Message = "Lỗi xử lý dữ liệu";
                _ReturnCheckDealTransfer.Total = 0;
                _ReturnCheckDealTransfer.ListCheckDealTransfer_NewTab_Report = listCheckDealTransferDetail;
                LogAPI.LogToFile(LogFileType.EXCEPTION, "CheckDealTransferRepository.CHECK_DEAL_TRANSFER_BY_ID_CT_DETAIL" + ex.Message);
            }
            return _ReturnCheckDealTransfer;
        }

        #endregion
    }



}

