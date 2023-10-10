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
    public class TransferManagementRepository
    {
        #region GETPROVINCE
        //Lấy mã bưu cục phát dưới DB Procedure transfer_management_ems.GetProvince_Ems
        public String GETPROVINCE()
        {
            string LISTPROVINCE = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "transfer_management_ems.GetProvince_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTPROVINCE += "<option value='" + dr["PROVINCECODE"].ToString() + "'>" + dr["PROVINCECODE"].ToString() + '-' + dr["PROVINCENAME"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETPROVINCE" + ex.Message);
            }

            return LISTPROVINCE;
        }
        #endregion

        #region GetAllPOSCODE
        //Lấy mã bưu cục phát dưới DB Procedure Detail_DeliveryPosCode_Ems , phần GETALLPOSCODE này đã được get; set; trong data model Qualty Delivery
        public string GetCRPOSCODE(int province)
        {
            string LISTPOSCODE = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "transfer_management_ems.GetPosCode_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_Province", OracleDbType.Int32)).Value = province;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTPOSCODE += "<option value='" + dr["POSCODE"].ToString() + "'>" + dr["POSCODE"].ToString() + '-' + dr["POSNAME"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETCRPOSCODE" + ex.Message);
            }

            return LISTPOSCODE;
        }
        #endregion

        //Phần chi tiết của bảng tổng hợp 
        #region TRANSFER_MANAGEMENT_DETAIL          
        public ReturnTransferManagement TRANSFER_MANAGEMENT_DETAIL(int fromprovince, int toprovince, int fromposcode, int toposcode, string fromdate, string todate, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement _returnTransferManagement = new ReturnTransferManagement();

            //_metadata.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            //_returnTransferManagement.MetaData = _metadata;
            List<TransferManagementDetail> listTransferManagementDetail = null;
            TransferManagementDetail oTransferManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Mailtrip_Ems", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromProvince", OracleDbType.Int32).Value = fromprovince;
                    myCommand.Parameters.Add("v_ToProvince", OracleDbType.Int32).Value = toprovince;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_FromDate", OracleDbType.Int32).Value = common.DateToInt(fromdate);
                    myCommand.Parameters.Add("v_ToDate", OracleDbType.Int32).Value = common.DateToInt(todate);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();



                    if (dr.HasRows)
                    {
                        listTransferManagementDetail = new List<TransferManagementDetail>();
                        while (dr.Read())
                        {
                            oTransferManagementDetail = new TransferManagementDetail();
                            oTransferManagementDetail.Typecomunication = typecomunication;
                            oTransferManagementDetail.FromPosCode = Convert.ToInt32(dr["FROMPOSCODE"].ToString());
                            oTransferManagementDetail.FromPosName = dr["FROMPOSNAME"].ToString();
                            oTransferManagementDetail.ToPosCode = Convert.ToInt32(dr["TOPOSCODE"].ToString());
                            oTransferManagementDetail.ToPosName = dr["TOPOSNAME"].ToString();
                            oTransferManagementDetail.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagementDetail.TotalMailTrip = Convert.ToInt32(dr["TOTALMAILTRIP"].ToString());
                            oTransferManagementDetail.TotalPostBag = Convert.ToInt32(dr["TOTALPOSTBAG"].ToString());
                            oTransferManagementDetail.TotalItem = Convert.ToInt32(dr["TOTALITEM"].ToString());
                            listTransferManagementDetail.Add(oTransferManagementDetail);

                        }
                        _returnTransferManagement.Code = "00";
                        _returnTransferManagement.Message = "Lấy dữ liệu thành công.";
                        _returnTransferManagement.ListTransferManagementReport = listTransferManagementDetail;
                    }
                    else
                    {
                        _returnTransferManagement.Code = "01";
                        _returnTransferManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnTransferManagement.Code = "99";
                _returnTransferManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnTransferManagement;
        }



        #endregion

        //Phần chi tiết của bảng chi tiết theo số chuyến thư hoặc số túi 
        #region TRANSFER_MANAGEMENT_CT_TS_DETAIL          
        public ReturnTransferManagement TRANSFER_MANAGEMENT_CT_TS_DETAIL(int fromposcode, int toposcode, int date, int type, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement _returnTransferManagement = new ReturnTransferManagement();


            List<TransferManagement_CTTS_Detail> listTransferManagement_CTTS_Detail = null;
            TransferManagement_CTTS_Detail oTransferManagement_CTTS_Detail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;

                    //string ngay = date;
                    cmd.CommandText = Helper.SchemaName + "transfer_management_ems.Detail_PostBag_Ems";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_typecomunication", OracleDbType.Int32)).Value = typecomunication;
                    cmd.Parameters.Add(new OracleParameter("v_FromPosCode", OracleDbType.Int32)).Value = fromposcode;
                    cmd.Parameters.Add(new OracleParameter("v_ToPosCode", OracleDbType.Int32)).Value = toposcode;
                    cmd.Parameters.Add(new OracleParameter("v_Date", OracleDbType.Int32)).Value = date;
                    cmd.Parameters.Add(new OracleParameter("v_type", OracleDbType.Int32)).Value = type;
                    cmd.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listTransferManagement_CTTS_Detail = new List<TransferManagement_CTTS_Detail>();
                        while (dr.Read())
                        {
                            oTransferManagement_CTTS_Detail = new TransferManagement_CTTS_Detail();
                            oTransferManagement_CTTS_Detail.Typecomunication = typecomunication;
                            oTransferManagement_CTTS_Detail.FromPosCode = Convert.ToInt32(dr["FROMPOSCODE"].ToString());
                            oTransferManagement_CTTS_Detail.FromPosName = dr["FROMPOSNAME"].ToString();
                            oTransferManagement_CTTS_Detail.ToPosCode = Convert.ToInt32(dr["TOPOSCODE"].ToString());
                            oTransferManagement_CTTS_Detail.ToPosName = dr["TOPOSNAME"].ToString();
                            oTransferManagement_CTTS_Detail.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagement_CTTS_Detail.TotalMailTrip = Convert.ToInt32(dr["CHTHU"].ToString());
                            oTransferManagement_CTTS_Detail.TotalPostBag = Convert.ToInt32(dr["TUISO"].ToString());
                            oTransferManagement_CTTS_Detail.TotalItem = Convert.ToInt32(dr["COUNT(MAE1)"].ToString());
                            listTransferManagement_CTTS_Detail.Add(oTransferManagement_CTTS_Detail);
                        }
                        _returnTransferManagement.Code = "00";
                        _returnTransferManagement.Message = "Lấy dữ liệu thành công.";
                        _returnTransferManagement.ListTransferManagement_CTTS_Report = listTransferManagement_CTTS_Detail;
                    }
                    else
                    {
                        _returnTransferManagement.Code = "01";
                        _returnTransferManagement.Message = "Không có dữ liệu";
                        _returnTransferManagement.Total = 0;
                        _returnTransferManagement.ListTransferManagement_CTTS_Report = null;
                    }

                }
            }
            catch (Exception ex)
            {
                _returnTransferManagement.Code = "99";
                _returnTransferManagement.Message = "Lỗi xử lý dữ liệu";
                _returnTransferManagement.Total = 0;
                _returnTransferManagement.ListTransferManagement_CTTS_Report = null;
            }
            return _returnTransferManagement;
        }



        #endregion

        //Phần chi tiết của số túi theo số chuyến thư 
        #region TRANSFER_MANAGEMENT_SOTUI_DETAIL          
        public ReturnTransferManagement TRANSFER_MANAGEMENT_SOTUI_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement _returnTransferManagement = new ReturnTransferManagement();


            List<TransferManagement_SOTUI_Detail> listTransferManagement_SOTUI_Detail = null;
            TransferManagement_SOTUI_Detail oTransferManagement_SOTUI_Detail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;

                    //string ngay = date;
                    cmd.CommandText = Helper.SchemaName + "transfer_management_ems.Detail_PostBag_Mailtrip";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_typecomunication", OracleDbType.Int32)).Value = typecomunication;
                    cmd.Parameters.Add(new OracleParameter("v_FromPosCode", OracleDbType.Int32)).Value = fromposcode;
                    cmd.Parameters.Add(new OracleParameter("v_ToPosCode", OracleDbType.Int32)).Value = toposcode;
                    cmd.Parameters.Add(new OracleParameter("v_Date", OracleDbType.Int32)).Value = date;
                    cmd.Parameters.Add(new OracleParameter("v_mailtrip", OracleDbType.Int32)).Value = mailtrip;
                    cmd.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listTransferManagement_SOTUI_Detail = new List<TransferManagement_SOTUI_Detail>();
                        while (dr.Read())
                        {
                            oTransferManagement_SOTUI_Detail = new TransferManagement_SOTUI_Detail();
                            oTransferManagement_SOTUI_Detail.FromPosCode = Convert.ToInt32(dr["FROMPOSCODE"].ToString());
                            oTransferManagement_SOTUI_Detail.FromPosName = dr["FROMPOSNAME"].ToString();
                            oTransferManagement_SOTUI_Detail.ToPosCode = Convert.ToInt32(dr["TOPOSCODE"].ToString());
                            oTransferManagement_SOTUI_Detail.ToPosName = dr["TOPOSNAME"].ToString();
                            oTransferManagement_SOTUI_Detail.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagement_SOTUI_Detail.ChThu = Convert.ToInt32(dr["CHTHU"].ToString());
                            oTransferManagement_SOTUI_Detail.TuiSo = Convert.ToInt32(dr["TUISO"].ToString());
                            oTransferManagement_SOTUI_Detail.CountMaE1 = Convert.ToInt32(dr["COUNT(MAE1)"].ToString());
                            listTransferManagement_SOTUI_Detail.Add(oTransferManagement_SOTUI_Detail);
                        }
                        _returnTransferManagement.Code = "00";
                        _returnTransferManagement.Message = "Lấy dữ liệu thành công.";
                        _returnTransferManagement.ListTransferManagement_SOTUI_Report = listTransferManagement_SOTUI_Detail;
                    }
                    else
                    {
                        _returnTransferManagement.Code = "01";
                        _returnTransferManagement.Message = "Không có dữ liệu";
                        _returnTransferManagement.Total = 0;
                        _returnTransferManagement.ListTransferManagement_SOTUI_Report = null;
                    }

                }
            }
            catch (Exception ex)
            {
                _returnTransferManagement.Code = "99";
                _returnTransferManagement.Message = "Lỗi xử lý dữ liệu";
                _returnTransferManagement.Total = 0;
                _returnTransferManagement.ListTransferManagement_SOTUI_Report = null;
            }
            return _returnTransferManagement;
        }



        #endregion

        //Phần chi tiết của bảng in bản kê E2 lấy theo chuyến thư túi số
        #region TRANSFER_MANAGEMENT_E2_DETAIL          
        public ReturnTransferManagement TRANSFER_MANAGEMENT_E2_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement _returnTransferManagement = new ReturnTransferManagement();

            List<TransferManagement_E2_Detail> listTransferManagement_E2_Detail = null;
            TransferManagement_E2_Detail oTransferManagement_E2_Detail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Item_Ems", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = postbag;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTransferManagement_E2_Detail = new List<TransferManagement_E2_Detail>();
                        while (dr.Read())
                        {
                            oTransferManagement_E2_Detail = new TransferManagement_E2_Detail();
                            oTransferManagement_E2_Detail.MaE1 = dr["MAE1"].ToString();
                            oTransferManagement_E2_Detail.MaBCTra = Convert.ToInt32(dr["MABCTRA"].ToString());
                            oTransferManagement_E2_Detail.KhoiLuong = dr["KHOILUONG"].ToString();
                            oTransferManagement_E2_Detail.CuocCS = dr["CUOCCS"].ToString();
                            oTransferManagement_E2_Detail.Cuoc_DV = dr["CUOC_DICH_VU"].ToString();
                            oTransferManagement_E2_Detail.Tong_Cuoc = dr["TONG_CUOC"].ToString();
                            oTransferManagement_E2_Detail.TrangThai = dr["TRANGTHAI"].ToString();
                            listTransferManagement_E2_Detail.Add(oTransferManagement_E2_Detail);

                        }
                        _returnTransferManagement.Code = "00";
                        _returnTransferManagement.Message = "Lấy dữ liệu thành công.";
                        _returnTransferManagement.ListTransferManagement_E2_Report = listTransferManagement_E2_Detail;
                    }
                    else
                    {
                        _returnTransferManagement.Code = "01";
                        _returnTransferManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnTransferManagement.Code = "99";
                _returnTransferManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnTransferManagement;
        }



        #endregion

        //Phần chi tiết của chuyến thư túi số phần in bản kê E2 lấy theo chuyến thư túi số
        #region TRANSFER_MANAGEMENT_CTTS_E2_DETAIL          
        public ReturnTransferManagement TRANSFER_MANAGEMENT_CTTS_E2_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement _returnTransferManagement = new ReturnTransferManagement();

            List<TransferManagement_CTTS_E2_Detail> listTransferManagement_CTTS_E2_Detail = null;
            TransferManagement_CTTS_E2_Detail oTransferManagement_CTTS_E2_Detail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Info_Ems", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = postbag;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTransferManagement_CTTS_E2_Detail = new List<TransferManagement_CTTS_E2_Detail>();
                        while (dr.Read())
                        {
                            oTransferManagement_CTTS_E2_Detail = new TransferManagement_CTTS_E2_Detail();
                            oTransferManagement_CTTS_E2_Detail.FromPos = dr["FROMPOS"].ToString();
                            oTransferManagement_CTTS_E2_Detail.ToPos = dr["TOPOS"].ToString();
                            oTransferManagement_CTTS_E2_Detail.MailTrip = Convert.ToInt32(dr["MAILTRIP"].ToString());
                            oTransferManagement_CTTS_E2_Detail.PostBag = Convert.ToInt32(dr["POSTBAG"].ToString());
                            oTransferManagement_CTTS_E2_Detail.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagement_CTTS_E2_Detail.PickUpTime = dr["PICKUPTIME"].ToString();
                            oTransferManagement_CTTS_E2_Detail.TotalWeight = Convert.ToDecimal(dr["TOTALWEIGHT"].ToString());
                            listTransferManagement_CTTS_E2_Detail.Add(oTransferManagement_CTTS_E2_Detail);
                        }
                        _returnTransferManagement.Code = "00";
                        _returnTransferManagement.Message = "Lấy dữ liệu thành công.";
                        _returnTransferManagement.ListTransferManagement_CTTS_E2_Report = listTransferManagement_CTTS_E2_Detail;
                    }
                    else
                    {
                        _returnTransferManagement.Code = "01";
                        _returnTransferManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnTransferManagement.Code = "99";
                _returnTransferManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnTransferManagement;
        }



        #endregion

        //Phần chi tiết của bảng in bản kê E2 lấy theo mã cổ túi
        #region TRANSFER_MANAGEMENT_MCT_E2_DETAIL          
        public ReturnTransferManagement TRANSFER_MANAGEMENT_MCT_E2_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            //int parafromposcode = Convert.ToInt32(fromposcode.ToString().Substring(0, 6));
            //int paratoposcode = Convert.ToInt32(fromposcode.ToString().Substring(7, 6));
            //int paradate = Convert.ToInt32(fromposcode.ToString().Substring(13, 8));
            //int paramailtrip = Convert.ToInt32(fromposcode.ToString().Substring(21, 3));
            //int parapostbag = Convert.ToInt32(fromposcode.ToString().Substring(24, 3));
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement _returnTransferManagement = new ReturnTransferManagement();

            List<TransferManagement_MCT_E2_Detail> listTransferManagement_MCT_E2_Detail = null;
            TransferManagement_MCT_E2_Detail oTransferManagement_MCT_E2_Detail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Item_Ems", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = postbag;
                    //myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = parafromposcode;
                    //myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = paratoposcode;
                    //myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = paradate;
                    //myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = paramailtrip;
                    //myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = parapostbag;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTransferManagement_MCT_E2_Detail = new List<TransferManagement_MCT_E2_Detail>();
                        while (dr.Read())
                        {
                            oTransferManagement_MCT_E2_Detail = new TransferManagement_MCT_E2_Detail();
                            oTransferManagement_MCT_E2_Detail.MaE1 = dr["MAE1"].ToString();
                            oTransferManagement_MCT_E2_Detail.MaBCTra = Convert.ToInt32(dr["MABCTRA"].ToString());
                            oTransferManagement_MCT_E2_Detail.KhoiLuong = dr["KHOILUONG"].ToString();
                            oTransferManagement_MCT_E2_Detail.CuocCS = dr["CUOCCS"].ToString();
                            oTransferManagement_MCT_E2_Detail.Cuoc_DV = dr["CUOC_DICH_VU"].ToString();
                            oTransferManagement_MCT_E2_Detail.Tong_Cuoc = dr["TONG_CUOC"].ToString();
                            oTransferManagement_MCT_E2_Detail.TrangThai = dr["TRANGTHAI"].ToString();
                            listTransferManagement_MCT_E2_Detail.Add(oTransferManagement_MCT_E2_Detail);

                        }
                        _returnTransferManagement.Code = "00";
                        _returnTransferManagement.Message = "Lấy dữ liệu thành công.";
                        _returnTransferManagement.ListTransferManagement_MCT_E2_Report = listTransferManagement_MCT_E2_Detail;
                    }
                    else
                    {
                        _returnTransferManagement.Code = "01";
                        _returnTransferManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnTransferManagement.Code = "99";
                _returnTransferManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnTransferManagement;
        }



        #endregion

        //Phần chi tiết của chuyến thư túi số phần in bản kê E2 lấy theo mã cổ túi
        #region TRANSFER_MANAGEMENT_CTTS_MCT_E2_DETAIL          
        public ReturnTransferManagement TRANSFER_MANAGEMENT_CTTS_MCT_E2_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement _returnTransferManagement = new ReturnTransferManagement();

            List<TransferManagement_CTTS_MCT_E2_Detail> listTransferManagement_CTTS_MCT_E2_Detail = null;
            TransferManagement_CTTS_MCT_E2_Detail oTransferManagement_CTTS_MCT_E2_Detail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Info_Ems", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = postbag;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTransferManagement_CTTS_MCT_E2_Detail = new List<TransferManagement_CTTS_MCT_E2_Detail>();
                        while (dr.Read())
                        {
                            oTransferManagement_CTTS_MCT_E2_Detail = new TransferManagement_CTTS_MCT_E2_Detail();
                            oTransferManagement_CTTS_MCT_E2_Detail.FromPos = dr["FROMPOS"].ToString();
                            oTransferManagement_CTTS_MCT_E2_Detail.ToPos = dr["TOPOS"].ToString();
                            oTransferManagement_CTTS_MCT_E2_Detail.MailTrip = Convert.ToInt32(dr["MAILTRIP"].ToString());
                            oTransferManagement_CTTS_MCT_E2_Detail.PostBag = Convert.ToInt32(dr["POSTBAG"].ToString());
                            oTransferManagement_CTTS_MCT_E2_Detail.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagement_CTTS_MCT_E2_Detail.PickUpTime = dr["PICKUPTIME"].ToString();
                            oTransferManagement_CTTS_MCT_E2_Detail.TotalWeight = Convert.ToDecimal(dr["TOTALWEIGHT"].ToString());
                            listTransferManagement_CTTS_MCT_E2_Detail.Add(oTransferManagement_CTTS_MCT_E2_Detail);
                        }
                        _returnTransferManagement.Code = "00";
                        _returnTransferManagement.Message = "Lấy dữ liệu thành công.";
                        _returnTransferManagement.ListTransferManagement_CTTS_MCT_E2_Report = listTransferManagement_CTTS_MCT_E2_Detail;
                    }
                    else
                    {
                        _returnTransferManagement.Code = "01";
                        _returnTransferManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnTransferManagement.Code = "99";
                _returnTransferManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnTransferManagement;
        }
        #endregion

        //Truyền lại dữ liệu từ Center về BCP
        public ReturnTransmited TransmitDataCentertoBCP(int fromposcode, int toposcode, int date, int mailtrip, int posbag)
        {
            ReturnTransmited res = new ReturnTransmited();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.UPDATE_CENTER_TO_BCP", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    myCommand.Parameters.Add("v_Mabc_kt", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_Mabc", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Ngay", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Chthu", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Tuiso", OracleDbType.Int32).Value = posbag;
                    myCommand.ExecuteNonQuery();
                    res.Code = "00";
                    res.Message = "Success";

                }
            }
            catch (Exception ex)
            {
                res.Code = "99";
                res.Message = "Failed";
            }
            return res;
        }

        public ReturnTransmited TransmitDataHubtoCenter(int fromposcode, int toposcode, int date, int mailtrip, int posbag)
        {
            ReturnTransmited res = new ReturnTransmited();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand();
                    if (fromposcode == 100916 || fromposcode == 101000)
                    {
                        myCommand = new OracleCommand("UPDATE_HUB_TO_CENTER_HN", Helper.OraDCOracleConnection);
                    }
                    else if (fromposcode == 700916 || fromposcode == 701000)
                    {
                        myCommand = new OracleCommand("UPDATE_HUB_TO_CENTER_HCM", Helper.OraDCOracleConnection);
                    }
                    else if (fromposcode == 550915 || fromposcode == 550100)
                    {
                        myCommand = new OracleCommand("UPDATE_HUB_TO_CENTER_DN", Helper.OraDCOracleConnection);
                    }
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    myCommand.Parameters.Add("v_Mabc_kt", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_Mabc", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Ngay", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Chthu", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Tuiso", OracleDbType.Int32).Value = posbag;
                    myCommand.ExecuteNonQuery();
                    res.Code = "00";
                    res.Message = "Success";

                }
            }
            catch (Exception ex)
            {
                res.Code = "99";
                res.Message = "Failed";
            }
            return res;
        }
        public ReturnTransmited TransmitDataCentertoBCCP(int fromposcode, int toposcode, int date, int mailtrip)
        {
            ReturnTransmited res = new ReturnTransmited();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.UPDATE_CENTER_TO_BCCP", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    myCommand.Parameters.Add("v_Mabc_kt", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_Mabc", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Ngay", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Chthu", OracleDbType.Int32).Value = mailtrip;
                    myCommand.ExecuteNonQuery();
                    res.Code = "00";
                    res.Message = "Success";

                }
            }
            catch (Exception ex)
            {
                res.Code = "99";
                res.Message = "Failed";
            }
            return res;
        }
    }  
}

