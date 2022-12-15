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
    public class PostmanDeliveryRepository
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
      
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public IEnumerable<GETPOSTMAN> GetAllPOSTMAN(int routecode)
        {
            List<GETPOSTMAN> listPostman = null;
            GETPOSTMAN oGetPostman = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "kpi_detail_PostMan.Detail_Postman_Ems";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add(new OracleParameter("v_RouteCode", OracleDbType.Int32)).Value = routecode;
                    cm.Parameters.Add("v_ListPostBag", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listPostman = new List<GETPOSTMAN>();
                        while (dr.Read())
                        {
                            oGetPostman = new GETPOSTMAN();
                            oGetPostman.POSTMAN_ID = dr["Postman_ID"].ToString();
                            oGetPostman.POSTMAN_NAME = dr["Postman_Name"].ToString();
                            listPostman.Add(oGetPostman);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "kpi_detail_PostMan.Detail_Postman_Ems" + ex.Message);
                listPostman = null;
            }

            return listPostman;
        }
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
                    cm.CommandText = Helper.SchemaName + "kpi_detail_PostMan.Detail_DeliveryRoute_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_POCODE", OracleDbType.Int32)).Value = endpostcode;
                    cm.Parameters.Add("v_ListPostBag", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetRouteCode = new List<GETROUTECODE>();
                        while (dr.Read())
                        {
                            oGetRouteCode = new GETROUTECODE();
                            oGetRouteCode.POSCODE = int.Parse(dr["Code"].ToString());
                            oGetRouteCode.POSNAME = dr["Name"].ToString();
                            listGetRouteCode.Add(oGetRouteCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "PostmanDeliveryRepository.GetAllROUTECODE" + ex.Message);
                listGetRouteCode = null;
            }

            return listGetRouteCode;
        }

        #endregion

        //Phần chi tiết của bảng tổng hợp sản lượng đi phát
        #region Postman_DELIVERY_DETAIL          
        public ReturnPostman Postman_DELIVERY_DETAIL(int postmanid,int endpostcode,int routecode, int startdate, int enddate, int service)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnPostman _returnPostMan = new ReturnPostman();

            
            List<PostmanDeliveryDetail> listPostmanDeliveryDetail = null;
            PostmanDeliveryDetail oPostmanDeliveryDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_PTC", Helper.OraDCOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                                   
                    myCommand.Parameters.Add("v_EndPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_routecode", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_PostmanID", OracleDbType.Int32).Value = postmanid;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listPostmanDeliveryDetail = new List<PostmanDeliveryDetail>();
                        while (dr.Read())
                        {
                            oPostmanDeliveryDetail = new PostmanDeliveryDetail();
                            oPostmanDeliveryDetail.STT = a++;                          
                            oPostmanDeliveryDetail.BuuCuc = dr["STARTPOSTCODE"].ToString();
                            oPostmanDeliveryDetail.TenBuuCuc = dr["STARTPOSTCODENAME"].ToString();
                            oPostmanDeliveryDetail.TuyenPhat = dr["ROUTECODE"].ToString();
                            oPostmanDeliveryDetail.TenTuyenPhat = dr["ROUTENAME"].ToString();
                            oPostmanDeliveryDetail.IDBuuTa = dr["POSTMANID"].ToString();
                            oPostmanDeliveryDetail.TenBuuTa = dr["POSTMANNAME"].ToString();
                            oPostmanDeliveryDetail.DichVu =dr["SERVICETYPENUMBER"].ToString();
                            try
                            {
                                oPostmanDeliveryDetail.TongSL = Convert.ToInt32(dr["TONGSL"].ToString());
                            }catch
                            {

                                oPostmanDeliveryDetail.TongSL = 0;
                            }

                            try
                            {
                                oPostmanDeliveryDetail.SanLuongPTC = Convert.ToInt32(dr["SANLUONGPTC"].ToString());
                            }
                            catch
                            {

                                oPostmanDeliveryDetail.SanLuongPTC = 0;
                            }
                            //try
                            //{
                            //    oPostmanDeliveryDetail.TyLePTC = Convert.ToDecimal(dr["TYLEPTC"].ToString());
                            //}
                            //catch
                            //{

                            //    oPostmanDeliveryDetail.TyLePTC = 0;
                            //}
                            try
                            {
                                oPostmanDeliveryDetail.SanLuongPTC5H = Convert.ToInt32(dr["SANLUONGQD"].ToString());
                            }
                            catch
                            {

                                oPostmanDeliveryDetail.SanLuongPTC5H = 0;
                            }

                            try
                            {
                                oPostmanDeliveryDetail.TOTAL5KG = Convert.ToInt32(dr["TOTAL5KG"].ToString());
                            }
                            catch
                            {

                                oPostmanDeliveryDetail.TOTAL5KG = 0;
                            }

                            try
                            {
                                oPostmanDeliveryDetail.TyLeTrong5H = Convert.ToDecimal(dr["TYLEQD"].ToString());
                            }
                            catch
                            {

                                oPostmanDeliveryDetail.TyLeTrong5H = 0;
                            }
                            try
                            {
                                oPostmanDeliveryDetail.SanLuongPTCQUA5H = Convert.ToInt32(dr["SANLUONGKQD"].ToString());
                            }
                            catch
                            {

                                oPostmanDeliveryDetail.SanLuongPTCQUA5H = 0;
                            }

                            try
                            {
                                oPostmanDeliveryDetail.TOTALNOT5KG = Convert.ToInt32(dr["TOTALNOT5KG"].ToString());
                            }
                            catch
                            {

                                oPostmanDeliveryDetail.TOTALNOT5KG = 0;
                            }

                            try
                            {
                                oPostmanDeliveryDetail.TyLeQua5H = Convert.ToDecimal(dr["TYLEKQD"].ToString());
                            }
                            catch
                            {

                                oPostmanDeliveryDetail.TyLeQua5H = 0;
                            }


                            try
                            {
                                oPostmanDeliveryDetail.SanLuongKTT = Convert.ToInt32(dr["SANLUONGKTT"].ToString());
                            }
                            catch
                            {

                                oPostmanDeliveryDetail.SanLuongKTT = 0;
                            }

                          
                          



                            listPostmanDeliveryDetail.Add(oPostmanDeliveryDetail);
                            
                        }
                        _returnPostMan.Code = "00";
                        _returnPostMan.Message = "Lấy dữ liệu thành công.";
                        _returnPostMan.ListPostmanDeliveryReport = listPostmanDeliveryDetail;
                    }
                    else
                    {
                        _returnPostMan.Code = "01";
                        _returnPostMan.Message = "Không có dữ liệu";
                        
                    }


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "PostmanDeliveryRepository.POSTMAN_DELIVERY_DETAIL" + ex.Message);
                _returnPostMan.Code = "99";
                _returnPostMan.Message = "Lỗi xử lý dữ liệu";
                _returnPostMan.ListPostmanDeliveryReport = null;
            }
            return _returnPostMan;
        }
        public int ConvertToInt(object str)
        {
            try
            {
                return Convert.ToInt32(str.ToString());
            }
            catch
            {
                return 0;
            }
        }


        #endregion

        //Phần chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
        #region KpiBD13_Delivery_Success6H_Detail          
        public ReturnPostman Postman_Delivery_Success5H_Detail(int startpostcode,int routecode,int PostmanID, string service, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnPostman _returnPostman = new ReturnPostman();

            
            List<PostmanItemDeliverySuccess5HDetail> listPostmanDeliverySuccess5HDetail = null;
            PostmanItemDeliverySuccess5HDetail oKpiPostmanSuccess5HDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.                          
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_Item_Ems_PTC_QQD", Helper.OraDCOracleConnection);
                        //xử lý tham số truyền vào data table
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.CommandTimeout = 20000;
                        OracleDataAdapter mAdapter = new OracleDataAdapter();
                        myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = startpostcode;
                        myCommand.Parameters.Add("v_RouteCode", OracleDbType.Int32).Value = routecode;
                        myCommand.Parameters.Add("v_PostmanID", OracleDbType.Int32).Value = PostmanID;                       
                        myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                        myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                       myCommand.Parameters.Add("v_Isservice", OracleDbType.Varchar2).Value = service;
                       myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                        mAdapter = new OracleDataAdapter(myCommand);
                        mAdapter.Fill(da);
                        DataTableReader dr = da.CreateDataReader();               
                    if (dr.HasRows)
                    {
                        listPostmanDeliverySuccess5HDetail = new List<PostmanItemDeliverySuccess5HDetail>();
                        while (dr.Read())
                        {
                            oKpiPostmanSuccess5HDetail = new PostmanItemDeliverySuccess5HDetail();
                            oKpiPostmanSuccess5HDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oKpiPostmanSuccess5HDetail.StartPostcode = dr["STARTPOSTCODE"].ToString();
                            oKpiPostmanSuccess5HDetail.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oKpiPostmanSuccess5HDetail.RouteCode = dr["ROUTECODE"].ToString();
                            oKpiPostmanSuccess5HDetail.RouteCodeName = dr["ROUTECODENAME"].ToString();
                            oKpiPostmanSuccess5HDetail.PostmanID = dr["POSTMANID"].ToString();
                            oKpiPostmanSuccess5HDetail.PostManName = dr["POSTMANNAME"].ToString();
                            oKpiPostmanSuccess5HDetail.C17StatusDate = dr["C17STATUSDATE"].ToString();
                            oKpiPostmanSuccess5HDetail.C17StatusTime = dr["C17STATUSTIME"].ToString();
                            oKpiPostmanSuccess5HDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oKpiPostmanSuccess5HDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oKpiPostmanSuccess5HDetail.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();                          
                            oKpiPostmanSuccess5HDetail.Timeinterval = dr["TIMEINTERVAL"].ToString();
                          
                            listPostmanDeliverySuccess5HDetail.Add(oKpiPostmanSuccess5HDetail);
                        }
                        _returnPostman.Code = "00";
                        _returnPostman.Message = "Lấy dữ liệu thành công.";
                        _returnPostman.ListPostmanDeliverySuccess5HReport = listPostmanDeliverySuccess5HDetail;
                    }
                    else
                    {
                        _returnPostman.Code = "01";
                        _returnPostman.Message = "Không có dữ liệu";
                        _returnPostman.Total = 0;
                        _returnPostman.ListPostmanDeliverySuccess5HReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "PostmanDeliveryRepository.Detail_Item_Ems_PTC_QQD" + ex.Message);
                _returnPostman.Code = "99";
                _returnPostman.Message = "Lỗi xử lý dữ liệu";
                _returnPostman.Total = 0;
                _returnPostman.ListPostmanDeliverySuccess5HReport = null;
            }
            return _returnPostman;
        }
        #endregion

        #region Not PostmanIDKPI
        public ReturnPostman NotPostmanIdKPIDetail(int RouteCode, int PostmanID, string servicetypenumber, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnPostman _returnPostman = new ReturnPostman();


            List<NotPostmanKPIDetail> listNotPostmanKPIDetaill = null;
            NotPostmanKPIDetail oNotPostmanKPIDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.                          
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_NotPostmanID", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_RouteCode", OracleDbType.Int32).Value = RouteCode;
                    myCommand.Parameters.Add("v_PostmanID", OracleDbType.Int32).Value = PostmanID;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Varchar2).Value = servicetypenumber;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listNotPostmanKPIDetaill = new List<NotPostmanKPIDetail>();
                        while (dr.Read())
                        {
                            oNotPostmanKPIDetail = new NotPostmanKPIDetail();
                            oNotPostmanKPIDetail.STT = a++;
                            oNotPostmanKPIDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oNotPostmanKPIDetail.RouteCode = dr["ROUTECODE"].ToString();
                            oNotPostmanKPIDetail.RouteCodeName = dr["ROUTECODENAME"].ToString();
                            oNotPostmanKPIDetail.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oNotPostmanKPIDetail.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oNotPostmanKPIDetail.PostmanID = dr["POSTMANID"].ToString();
                            oNotPostmanKPIDetail.PostManName = dr["POSTMANNAME"].ToString();
                            oNotPostmanKPIDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oNotPostmanKPIDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oNotPostmanKPIDetail.servicetypenumber = dr["SERVICETYPENUMBER"].ToString();
                            listNotPostmanKPIDetaill.Add(oNotPostmanKPIDetail);
                        }
                        _returnPostman.Code = "00";
                        _returnPostman.Message = "Lấy dữ liệu thành công.";
                        _returnPostman.ListNotPostmanKPIDetailReport = listNotPostmanKPIDetaill;
                    }
                    else
                    {
                        _returnPostman.Code = "01";
                        _returnPostman.Message = "Không có dữ liệu";
                        _returnPostman.Total = 0;
                        _returnPostman.ListNotPostmanKPIDetailReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                _returnPostman.Code = "99";
                _returnPostman.Message = "Lỗi xử lý dữ liệu";
                _returnPostman.Total = 0;
                _returnPostman.ListNotPostmanKPIDetailReport = null;
            }
            return _returnPostman;
        }
        #endregion

        #region NotPODKPI
        public ReturnPostman NotPODKPIDetail(int RouteCode, int PostmanID, string servicetypenumber, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnPostman _returnPostman = new ReturnPostman();


            List<NotPODKPIDetail> listNotPODKPIDetaill = null;
            NotPODKPIDetail oNotPODKPIDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.                          
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_PostMan.Detail_NotPOD", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_RouteCode", OracleDbType.Int32).Value = RouteCode;
                    myCommand.Parameters.Add("v_PostmanID", OracleDbType.Int32).Value = PostmanID;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Varchar2).Value = servicetypenumber;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listNotPODKPIDetaill = new List<NotPODKPIDetail>();
                        while (dr.Read())
                        {
                            oNotPODKPIDetail = new NotPODKPIDetail();
                            oNotPODKPIDetail.STT = a++;
                            oNotPODKPIDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oNotPODKPIDetail.RouteCode = dr["ROUTECODE"].ToString();
                            oNotPODKPIDetail.RouteCodeName = dr["ROUTECODENAME"].ToString();
                            oNotPODKPIDetail.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oNotPODKPIDetail.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oNotPODKPIDetail.PostmanID = dr["POSTMANID"].ToString();
                            oNotPODKPIDetail.PostManName = dr["POSTMANNAME"].ToString();
                            oNotPODKPIDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oNotPODKPIDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oNotPODKPIDetail.servicetypenumber = dr["SERVICETYPENUMBER"].ToString();
                            listNotPODKPIDetaill.Add(oNotPODKPIDetail);
                        }
                        _returnPostman.Code = "00";
                        _returnPostman.Message = "Lấy dữ liệu thành công.";
                        _returnPostman.ListNotPODKPIDetailReport = listNotPODKPIDetaill;
                    }
                    else
                    {
                        _returnPostman.Code = "01";
                        _returnPostman.Message = "Không có dữ liệu";
                        _returnPostman.Total = 0;
                        _returnPostman.ListNotPODKPIDetailReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                _returnPostman.Code = "99";
                _returnPostman.Message = "Lỗi xử lý dữ liệu";
                _returnPostman.Total = 0;
                _returnPostman.ListNotPODKPIDetailReport = null;
            }
            return _returnPostman;
        }
        #endregion
    }

}

