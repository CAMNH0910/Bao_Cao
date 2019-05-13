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
    public class KpiBD13DeliveryRepository
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
                    cm.CommandText = Helper.SchemaName + "Kpi_Detail_Delivery_BD13.Detail_DeliveryPosCode_Ems";
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
                LogAPI.LogToFile(LogFileType.EXCEPTION, "KpiBD13DeliveryRepository.GetAllPOSCODE" + ex.Message);
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
                    cm.CommandText = Helper.SchemaName + "Kpi_Detail_Delivery_BD13.Detail_DeliveryRoute_Ems";
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
                LogAPI.LogToFile(LogFileType.EXCEPTION, "KpiBD13DeliveryRepository.GetAllROUTECODE" + ex.Message);
                listGetRouteCode = null;
            }

            return listGetRouteCode;
        }

        #endregion

        //Phần chi tiết của bảng tổng hợp sản lượng đi phát
        #region KpiBD13_DELIVERY_DETAIL          
        public ReturnKpiBD13 KpiBD13_DELIVERY_DETAIL(int zone,int endpostcode,int routecode, int startdate, int enddate, int service)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKpiBD13 _returnKpiBD13 = new ReturnKpiBD13();

            
            List<KpiBD13DeliveryDetail> listKpiBD13DeliveryDetail = null;
            KpiBD13DeliveryDetail oKpiBD13DeliveryDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("Kpi_Detail_Delivery_BD13.Detail_area_Ems", Helper.OraDCOracleConnection);
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
                        listKpiBD13DeliveryDetail = new List<KpiBD13DeliveryDetail>();
                        while (dr.Read())
                        {
                            oKpiBD13DeliveryDetail = new KpiBD13DeliveryDetail();
                            oKpiBD13DeliveryDetail.STT = a++;
                            oKpiBD13DeliveryDetail.KhuVuc = dr["KHUVUC"].ToString();
                            //oKpiBD13DeliveryDetail.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oKpiBD13DeliveryDetail.BuuCuc = dr["BUUCUC"].ToString();
                            oKpiBD13DeliveryDetail.TenBuuCuc = dr["TENBUUCUC"].ToString();
                            oKpiBD13DeliveryDetail.TongSL = Convert.ToInt32(dr["TONGSL"].ToString());
                            oKpiBD13DeliveryDetail.SanLuongPTC = Convert.ToInt32(dr["SANLUONGPTC"].ToString());
                            oKpiBD13DeliveryDetail.SanLuongKTT = Convert.ToInt32(dr["SANLUONGKTT"].ToString());
                            oKpiBD13DeliveryDetail.SanLuongPTC6H = Convert.ToInt32(dr["SANLUONGPTC6H"].ToString());
                            oKpiBD13DeliveryDetail.SanLuongPTCQUA6H = Convert.ToInt32(dr["SANLUONGPTCQUA6H"].ToString());
                            oKpiBD13DeliveryDetail.TyLeTrong6H = Convert.ToDecimal(dr["TYLETRONG6H"].ToString());
                            oKpiBD13DeliveryDetail.TyLeQua6H = Convert.ToDecimal(dr["TYLEQUA6H"].ToString());
                            oKpiBD13DeliveryDetail.TCKXD = Convert.ToInt32(dr["TCKXD"].ToString());
                            listKpiBD13DeliveryDetail.Add(oKpiBD13DeliveryDetail);
                            
                        }
                        _returnKpiBD13.Code = "00";
                        _returnKpiBD13.Message = "Lấy dữ liệu thành công.";
                        _returnKpiBD13.ListKpiBD13DeliveryReport = listKpiBD13DeliveryDetail;
                    }
                    else
                    {
                        _returnKpiBD13.Code = "01";
                        _returnKpiBD13.Message = "Không có dữ liệu";
                        
                    }


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "KpiBD13DeliveryRepository.KpiBD13_DELIVERY_DETAIL" + ex.Message);
                _returnKpiBD13.Code = "99";
                _returnKpiBD13.Message = "Lỗi xử lý dữ liệu";
                _returnKpiBD13.ListKpiBD13DeliveryReport = null;
            }
            return _returnKpiBD13;
        }



        #endregion

        //Phần chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
        #region KpiBD13_Delivery_Success6H_Detail          
        public ReturnKpiBD13 KpiBD13_Delivery_Success6H_Detail(int endpostcode,int routecode, int startdate, int enddate, int service, int type)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKpiBD13 _returnKpiBD13 = new ReturnKpiBD13();

            
            List<KpiBD13DeliverySuccess6HDetail> listKpiBD13DeliverySuccess6HDetail = null;
            KpiBD13DeliverySuccess6HDetail oKpiBD13DeliverySuccess6HDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "Kpi_Detail_Delivery_BD13.Detail_Item_Ems";
                    cmd.CommandType = CommandType.StoredProcedure;
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
                        listKpiBD13DeliverySuccess6HDetail = new List<KpiBD13DeliverySuccess6HDetail>();
                        while (dr.Read())
                        {
                            oKpiBD13DeliverySuccess6HDetail = new KpiBD13DeliverySuccess6HDetail();
                            oKpiBD13DeliverySuccess6HDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.EndPostCode = Convert.ToInt32(dr["ENDPOSTCODE"].ToString());
                            oKpiBD13DeliverySuccess6HDetail.RouteCode = Convert.ToInt32(dr["ROUTECODE"].ToString());
                            oKpiBD13DeliverySuccess6HDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.C17StatusDate = dr["C17STATUSDATE"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.C17StatusTime = dr["C17STATUSTIME"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.TimeInterval = dr["TIMEINTERVAL"].ToString();
                            listKpiBD13DeliverySuccess6HDetail.Add(oKpiBD13DeliverySuccess6HDetail);
                        }
                        _returnKpiBD13.Code = "00";
                        _returnKpiBD13.Message = "Lấy dữ liệu thành công.";
                        _returnKpiBD13.ListKpiBD13DeliverySuccess6HReport = listKpiBD13DeliverySuccess6HDetail;
                    }
                    else
                    {
                        _returnKpiBD13.Code = "01";
                        _returnKpiBD13.Message = "Không có dữ liệu";
                        _returnKpiBD13.Total = 0;
                        _returnKpiBD13.ListKpiBD13DeliverySuccess6HReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "KpiBD13DeliveryRepository.KpiBD13_Delivery_Success6H_Detail" + ex.Message);
                _returnKpiBD13.Code = "99";
                _returnKpiBD13.Message = "Lỗi xử lý dữ liệu";
                _returnKpiBD13.Total = 0;
                _returnKpiBD13.ListKpiBD13DeliverySuccess6HReport = null;
            }
            return _returnKpiBD13;
        }
        #endregion


        //Phần chi tiết của từng bưu gửi theo số lượng phát thành công không có thông tin
        #region KpiBD13_Delivery_NoInformation_Detail          
        public ReturnKpiBD13 KpiBD13_Delivery_NoInformation_Detail(int endpostcode, int routecode, int startdate, int enddate, int service, int type)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKpiBD13 _returnKpiBD13 = new ReturnKpiBD13();
            List<KpiBD13DeliverySuccess6HDetail> listKpiBD13DeliverySuccess6HDetail = null;
            KpiBD13DeliverySuccess6HDetail oKpiBD13DeliverySuccess6HDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "Kpi_Detail_Delivery_BD13.Detail_Item_Ems_KTT";
                    cmd.CommandType = CommandType.StoredProcedure;
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
                        listKpiBD13DeliverySuccess6HDetail = new List<KpiBD13DeliverySuccess6HDetail>();
                        while (dr.Read())
                        {
                            oKpiBD13DeliverySuccess6HDetail = new KpiBD13DeliverySuccess6HDetail();
                            oKpiBD13DeliverySuccess6HDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.EndPostCode = Convert.ToInt32(dr["ENDPOSTCODE"].ToString());
                            oKpiBD13DeliverySuccess6HDetail.RouteCode = Convert.ToInt32(dr["ROUTECODE"].ToString());
                            oKpiBD13DeliverySuccess6HDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.C17StatusDate = dr["C17STATUSDATE"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.C17StatusTime = dr["C17STATUSTIME"].ToString();
                            oKpiBD13DeliverySuccess6HDetail.TimeInterval = dr["TIMEINTERVAL"].ToString();
                            listKpiBD13DeliverySuccess6HDetail.Add(oKpiBD13DeliverySuccess6HDetail);
                        }
                        _returnKpiBD13.Code = "00";
                        _returnKpiBD13.Message = "Lấy dữ liệu thành công.";
                        _returnKpiBD13.ListKpiBD13DeliverySuccess6HReport = listKpiBD13DeliverySuccess6HDetail;
                    }
                    else
                    {
                        _returnKpiBD13.Code = "01";
                        _returnKpiBD13.Message = "Không có dữ liệu";
                        _returnKpiBD13.Total = 0;
                        _returnKpiBD13.ListKpiBD13DeliverySuccess6HReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "KpiBD13DeliveryRepository.KpiBD13_Delivery_NoInformation_Detail" + ex.Message);
                _returnKpiBD13.Code = "99";
                _returnKpiBD13.Message = "Lỗi xử lý dữ liệu";
                _returnKpiBD13.Total = 0;
                _returnKpiBD13.ListKpiBD13DeliverySuccess6HReport = null;
            }
            return _returnKpiBD13;
        }
        #endregion
    }

}

