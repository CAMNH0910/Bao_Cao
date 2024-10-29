using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;
using System.Security.Policy;


namespace T41.Areas.Admin.Data
{
    public class QualityDeliveryRepository
    {
        #region GetAllPOSCODE
        //Lấy mã bưu cục phát dưới DB Procedure Detail_DeliveryPosCode_Ems
        public IEnumerable<GETPOSCODE> GetAllPOSCODE(int zone)
        {
            List<GETPOSCODE> listGetPosCode = null;
            GETPOSCODE oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "kpi_detail_delivery.Detail_DeliveryPosCode_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_Zone", OracleDbType.Int32)).Value = zone;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<GETPOSCODE>();
                        while (dr.Read())
                        {
                            oGetPosCode = new GETPOSCODE();
                            oGetPosCode.POSCODE = int.Parse(dr["POSCODE"].ToString());
                            oGetPosCode.POSNAME = dr["POSNAME"].ToString();
                            listGetPosCode.Add(oGetPosCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllPOSCODE" + ex.Message);
                listGetPosCode = null;
            }

            return listGetPosCode;
        }
        #endregion

        #region GetAllROUTECODE
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public IEnumerable<GETROUTECODE> GetAllROUTECODE(int endpostcode)
        {
            List<GETROUTECODE> listGetRouteCode = null;
            GETROUTECODE oGetRouteCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "kpi_detail_delivery.Detail_DeliveryRoute_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_EndPostCode", OracleDbType.Int32)).Value = endpostcode;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetRouteCode = new List<GETROUTECODE>();
                        while (dr.Read())
                        {
                            oGetRouteCode = new GETROUTECODE();
                            oGetRouteCode.POSCODE = int.Parse(dr["POSCODE"].ToString());
                            oGetRouteCode.POSNAME = dr["POSNAME"].ToString();
                            listGetRouteCode.Add(oGetRouteCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllROUTECODE" + ex.Message);
                listGetRouteCode = null;
            }

            return listGetRouteCode;
        }

        #endregion

        //Phần chi tiết của bảng tổng hợp sản lượng đi phát
        #region QUALITY_DETAIL          
        public ReturnQuality QUALITY_DELIVERY_DETAIL(int zone, int endpostcode, int routecode, int startdate, int enddate, int service)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnQuality _returnQuality = new ReturnQuality();

            _metadata1.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQuality.MetaData1 = _metadata1;
            List<QualityDeliveryDetail> listQualityDeliveryDetail = null;
            QualityDeliveryDetail oQualityDeliveryDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_delivery.Detail_area_Ems", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_EndPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_routecode", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityDeliveryDetail = new List<QualityDeliveryDetail>();
                        while (dr.Read())
                        {
                            oQualityDeliveryDetail = new QualityDeliveryDetail();
                            oQualityDeliveryDetail.STT = a++;
                            oQualityDeliveryDetail.KhuVuc = dr["KHUVUC"].ToString();
                            //oQualityDeliveryDetail.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oQualityDeliveryDetail.BuuCuc = dr["BUUCUC"].ToString();
                            oQualityDeliveryDetail.TenBuuCuc = dr["TENBUUCUC"].ToString();
                            oQualityDeliveryDetail.TongSL = Convert.ToInt32(dr["TONGSL"].ToString());
                            oQualityDeliveryDetail.SanLuongPTC = Convert.ToInt32(dr["SANLUONGPTC"].ToString());
                            oQualityDeliveryDetail.TylePTC = Convert.ToDecimal(dr["TYLEPTC"].ToString());

                            oQualityDeliveryDetail.SanLuongPTC72H = Convert.ToInt32(dr["TC72"].ToString());
                            oQualityDeliveryDetail.TylePTC72H = Convert.ToDecimal(dr["TYLEPTC72"].ToString());
                            oQualityDeliveryDetail.SanLuongPTC48H = Convert.ToInt32(dr["TC48"].ToString());
                            oQualityDeliveryDetail.TylePTC48H = Convert.ToDecimal(dr["TYLEPTC48"].ToString());
                            oQualityDeliveryDetail.TC24H = Convert.ToInt32(dr["TC24H"].ToString());
                            oQualityDeliveryDetail.TYLEPTC24 = Convert.ToDecimal(dr["TYLEPTC24"].ToString());

                            oQualityDeliveryDetail.SanLuongKTT = Convert.ToInt32(dr["SANLUONGKTT"].ToString());
                            oQualityDeliveryDetail.SanLuongPTC6H = Convert.ToInt32(dr["SANLUONGPTC6H"].ToString());
                            oQualityDeliveryDetail.SanLuongPTCQUA6H = Convert.ToInt32(dr["SANLUONGPTCQUA6H"].ToString());
                            oQualityDeliveryDetail.TyLeTrong6H = Convert.ToDecimal(dr["TYLETRONG6H"].ToString());
                            oQualityDeliveryDetail.TyLeQua6H = Convert.ToDecimal(dr["TYLEQUA6H"].ToString());
                            oQualityDeliveryDetail.TCKXD = Convert.ToInt32(dr["TCKXD"].ToString());
                            listQualityDeliveryDetail.Add(oQualityDeliveryDetail);

                        } 
                        _returnQuality.Code = "00";
                        _returnQuality.Message = "Lấy dữ liệu thành công.";
                        _returnQuality.ListQualityDeliveryReport = listQualityDeliveryDetail;
                    }
                    else
                    {
                        _returnQuality.Code = "01";
                        _returnQuality.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQuality.Code = "99";
                _returnQuality.Message = "Lỗi xử lý dữ liệu";
                _returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnQuality;
        }



        #endregion

        //Phần chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
        #region QUALITY_DETAIL          
        public ReturnQuality Quality_Delivery_Success6H_Detail(int zone,int endpostcode, int routecode, int startdate, int enddate, int service, int type)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnQuality _returnQuality = new ReturnQuality();

            _metadata1.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQuality.MetaData1 = _metadata1;
            List<QualityDeliverySuccess6HDetail> listQualityDeliverySuccess6HDetail = null;
            QualityDeliverySuccess6HDetail oQualityDeliverySuccess6HDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "kpi_detail_delivery.Detail_Item_Ems";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Zone", OracleDbType.Int32)).Value = zone;
                    cmd.Parameters.Add(new OracleParameter("v_EndPostCode", OracleDbType.Int32)).Value = endpostcode;
                    cmd.Parameters.Add(new OracleParameter("v_routecode", OracleDbType.Int32)).Value = routecode;
                    cmd.Parameters.Add(new OracleParameter("v_Service", OracleDbType.Int32)).Value = service;
                    cmd.Parameters.Add(new OracleParameter("v_StartDate", OracleDbType.Int32)).Value = startdate;
                    cmd.Parameters.Add(new OracleParameter("v_EndDate", OracleDbType.Int32)).Value = enddate;
                    cmd.Parameters.Add(new OracleParameter("v_type", OracleDbType.Int32)).Value = type;
                    cmd.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (dr.HasRows)
                    {
                        listQualityDeliverySuccess6HDetail = new List<QualityDeliverySuccess6HDetail>();
                        while (dr.Read())
                        {
                            oQualityDeliverySuccess6HDetail = new QualityDeliverySuccess6HDetail();
                            oQualityDeliverySuccess6HDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oQualityDeliverySuccess6HDetail.EndPostCode = Convert.ToInt32(dr["ENDPOSTCODE"].ToString());
                            oQualityDeliverySuccess6HDetail.RouteCode = Convert.ToInt32(dr["ROUTECODE"].ToString());
                            oQualityDeliverySuccess6HDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oQualityDeliverySuccess6HDetail.C17StatusDate = dr["C17STATUSDATE"].ToString();
                            oQualityDeliverySuccess6HDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oQualityDeliverySuccess6HDetail.C17StatusTime = dr["C17STATUSTIME"].ToString();
                            oQualityDeliverySuccess6HDetail.TimeInterval = dr["TIMEINTERVAL"].ToString();
                            listQualityDeliverySuccess6HDetail.Add(oQualityDeliverySuccess6HDetail);
                        }
                        _returnQuality.Code = "00";
                        _returnQuality.Message = "Lấy dữ liệu thành công.";
                        _returnQuality.ListQualityDeliverySuccess6HReport = listQualityDeliverySuccess6HDetail;
                    }
                    else
                    {
                        _returnQuality.Code = "01";
                        _returnQuality.Message = "Không có dữ liệu";
                        _returnQuality.Total = 0;
                        _returnQuality.ListQualityDeliverySuccess6HReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                _returnQuality.Code = "99";
                _returnQuality.Message = "Lỗi xử lý dữ liệu";
                _returnQuality.Total = 0;
                _returnQuality.ListQualityDeliverySuccess6HReport = null;
            }
            return _returnQuality;
        }
        #endregion


        //Phần chi tiết của từng bưu gửi theo số lượng phát thành công không có thông tin
        #region QUALITY_DETAIL          
        public ReturnQuality Quality_Delivery_NoInformation_Detail(int zone,int endpostcode, int routecode, int startdate, int enddate, int service, int type)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnQuality _returnQuality = new ReturnQuality();
            List<QualityDeliverySuccess6HDetail> listQualityDeliverySuccess6HDetail = null;
            QualityDeliverySuccess6HDetail oQualityDeliverySuccess6HDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "kpi_detail_delivery.Detail_Item_Ems_KTT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Zone", OracleDbType.Int32)).Value = zone;
                    cmd.Parameters.Add(new OracleParameter("v_EndPostCode", OracleDbType.Int32)).Value = endpostcode;
                    cmd.Parameters.Add(new OracleParameter("v_routecode", OracleDbType.Int32)).Value = routecode;
                    cmd.Parameters.Add(new OracleParameter("v_Service", OracleDbType.Int32)).Value = service;
                    cmd.Parameters.Add(new OracleParameter("v_StartDate", OracleDbType.Int32)).Value = startdate;
                    cmd.Parameters.Add(new OracleParameter("v_EndDate", OracleDbType.Int32)).Value = enddate;
                    cmd.Parameters.Add(new OracleParameter("v_type", OracleDbType.Int32)).Value = type;
                    cmd.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (dr.HasRows)
                    {
                        listQualityDeliverySuccess6HDetail = new List<QualityDeliverySuccess6HDetail>();
                        while (dr.Read())
                        {
                            oQualityDeliverySuccess6HDetail = new QualityDeliverySuccess6HDetail();
                            oQualityDeliverySuccess6HDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oQualityDeliverySuccess6HDetail.EndPostCode = Convert.ToInt32(dr["ENDPOSTCODE"].ToString());
                            oQualityDeliverySuccess6HDetail.RouteCode = Convert.ToInt32(dr["ROUTECODE"].ToString());
                            oQualityDeliverySuccess6HDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oQualityDeliverySuccess6HDetail.C17StatusDate = dr["C17STATUSDATE"].ToString();
                            oQualityDeliverySuccess6HDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oQualityDeliverySuccess6HDetail.C17StatusTime = dr["C17STATUSTIME"].ToString();
                            oQualityDeliverySuccess6HDetail.TimeInterval = dr["TIMEINTERVAL"].ToString();
                            listQualityDeliverySuccess6HDetail.Add(oQualityDeliverySuccess6HDetail);
                        }
                        _returnQuality.Code = "00";
                        _returnQuality.Message = "Lấy dữ liệu thành công.";
                        _returnQuality.ListQualityDeliverySuccess6HReport = listQualityDeliverySuccess6HDetail;
                    }
                    else
                    {
                        _returnQuality.Code = "01";
                        _returnQuality.Message = "Không có dữ liệu";
                        _returnQuality.Total = 0;
                        _returnQuality.ListQualityDeliverySuccess6HReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                _returnQuality.Code = "99";
                _returnQuality.Message = "Lỗi xử lý dữ liệu";
                _returnQuality.Total = 0;
                _returnQuality.ListQualityDeliverySuccess6HReport = null;
            }
            return _returnQuality;
        }
        #endregion
    }

}

