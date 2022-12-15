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
    public class KPITransportRepository
    {

        #region GETLISTPROVINCE
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public string GetProvince()
        {
            List<GETPROVINCECODE> listProvince = null;
            GETPROVINCECODE oGetProvince = null;
            string LISTPROVINCE = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "KPI_DETAIL_Transport.Detail_Province_Ems";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        // LISTTINH += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTPROVINCE += "<option value='" + dr["MA_TINH"].ToString() + "'>" + dr["MA_TINH"].ToString() + '-' + dr["TEN_TINH"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION,  ex.Message);
                LISTPROVINCE = null;
            }

            return LISTPROVINCE;
        }

        #endregion
        //Phần TONG HOP
        #region KPITRANSPORT_DETAIL          
        public ReturnKPITransportTotal KPITRANSPORT_DETAIL(int Hub,int EndProvince, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPITransportTotal _metadataKpiTransportTotal = new MetaDataKPITransportTotal();
            Convertion common = new Convertion();
            ReturnKPITransportTotal _returnKpiTransportTotal = new ReturnKPITransportTotal();
            _metadataKpiTransportTotal.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            _returnKpiTransportTotal.MetaDataKPITransportTotal = _metadataKpiTransportTotal;
            List<KPITransportTotal> listKPITransportTotal = null;
            KPITransportTotal oKpiTransportTotalDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("KPI_DETAIL_Transport.Detail_Total_Transport", Helper.OraDCOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                  
                    myCommand.Parameters.Add("v_Hub", OracleDbType.Int32).Value = Hub;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPITransportTotal = new List<KPITransportTotal>();
                        while (dr.Read())
                        {
                            oKpiTransportTotalDetail = new KPITransportTotal();
                            oKpiTransportTotalDetail.STT = a++;
                            oKpiTransportTotalDetail.WorkCenter = dr["WORKCENTER"].ToString();
                            oKpiTransportTotalDetail.WorkCenterName = dr["WORKCENTERNAME"].ToString();
                            oKpiTransportTotalDetail.EndProvince = dr["ENDPROVINCE"].ToString();
                            oKpiTransportTotalDetail.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                           // oKpiTransportTotalDetail.IsService = dr["ISSERVICE"].ToString();
                            oKpiTransportTotalDetail.Total = dr["TOTAL"].ToString();
                            oKpiTransportTotalDetail.TLSuccess = dr["TLSUCCESS"].ToString();
                            oKpiTransportTotalDetail.IsFailStatus = dr["ISFAILSTATUS"].ToString();
                            oKpiTransportTotalDetail.TLIsFailStatus = dr["TLISFAILSTATUS"].ToString();
                            oKpiTransportTotalDetail.NotKPI = dr["NOTKPI"].ToString();
                            oKpiTransportTotalDetail.TLNotKPI = dr["TLNOTKPI"].ToString();
                            listKPITransportTotal.Add(oKpiTransportTotalDetail);
                        }
                        _returnKpiTransportTotal.Code = "00";
                        _returnKpiTransportTotal.Message = "Lấy dữ liệu thành công.";
                        _returnKpiTransportTotal.ListKpiTransportReport = listKPITransportTotal;
                    }
                    else
                    {
                        _returnKpiTransportTotal.Code = "01";
                        _returnKpiTransportTotal.Message = "Không có dữ liệu";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _returnKpiTransportTotal.Code = "99";
                _returnKpiTransportTotal.Message = "Lỗi xử lý dữ liệu";
               
            }
            return _returnKpiTransportTotal;
        }
        #endregion

        // PHAN KPI TRANSPORT NOT KPI
        # region 
        public ReturnKPITransportNotKPI KPITransportNotKPI(int Hub, int EndProvince, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPITransportTotal _metadataKpiTransportTotal = new MetaDataKPITransportTotal();
            Convertion common = new Convertion();
            ReturnKPITransportNotKPI _returnKpiTransportNotKpi = new ReturnKPITransportNotKPI();
            _metadataKpiTransportTotal.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            List<KPITransportNotKPI> listKPITransportNotKPI = null;
            KPITransportNotKPI oKPITransportNotKPIItem = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_Transport.Detail_Item_Transport_Notkpi", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Hub", OracleDbType.Int32).Value = Hub;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPITransportNotKPI = new List<KPITransportNotKPI>();
                        while (dr.Read())
                        {
                            oKPITransportNotKPIItem = new KPITransportNotKPI();
                            oKPITransportNotKPIItem.STT = a++;
                            oKPITransportNotKPIItem.ItemCode = dr["ITEMCODE"].ToString();
                            oKPITransportNotKPIItem.WorkCenter = dr["WORKCENTER"].ToString();
                            oKPITransportNotKPIItem.WorkCenterName = dr["WORKCENTERNAME"].ToString();
                            oKPITransportNotKPIItem.StartProvince = dr["STARTPROVINCE"].ToString();
                            oKPITransportNotKPIItem.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oKPITransportNotKPIItem.EndProvince = dr["ENDPROVINCE"].ToString();
                            oKPITransportNotKPIItem.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oKPITransportNotKPIItem.StatusDateTime = dr["STATUSDATETIME"].ToString();
                            oKPITransportNotKPIItem.IsService = dr["ISSERVICE"].ToString();
                            oKPITransportNotKPIItem.IsType = dr["ISTYPE"].ToString();
                            listKPITransportNotKPI.Add(oKPITransportNotKPIItem);
                        }
                        _returnKpiTransportNotKpi.Code = "00";
                        _returnKpiTransportNotKpi.Message = "Lấy dữ liệu thành công.";
                        _returnKpiTransportNotKpi.ListKpiTransportNotKpiReport = listKPITransportNotKPI;
                    }
                    else
                    {
                        _returnKpiTransportNotKpi.Code = "01";
                        _returnKpiTransportNotKpi.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnKpiTransportNotKpi.Code = "99";
                _returnKpiTransportNotKpi.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnKpiTransportNotKpi;
        }
        # endregion 

        //PHAN CHI TIET KPITRANSPORT FAIL
        # region 
        public ReturnKPITransportFail KPITransportFail(int Hub, int EndProvince, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPITransportTotal _metadataKpiTransportTotal = new MetaDataKPITransportTotal();
            Convertion common = new Convertion();
            ReturnKPITransportFail _returnkpiTransportFail = new ReturnKPITransportFail();
            _metadataKpiTransportTotal.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            List<KPITransportFail> listKPITransportFail = null;
            KPITransportFail oKPITransportFailItem = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_Transport.Detail_Item_Transport_Fail", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Hub", OracleDbType.Int32).Value = Hub;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPITransportFail = new List<KPITransportFail>();
                        while (dr.Read())
                        {
                            oKPITransportFailItem = new KPITransportFail();
                            oKPITransportFailItem.STT = a++;
                            oKPITransportFailItem.ItemCode = dr["ITEMCODE"].ToString();
                            oKPITransportFailItem.isService = dr["ISSERVICE"].ToString();
                            oKPITransportFailItem.WorkCenter = dr["WORKCENTER"].ToString();
                            oKPITransportFailItem.WorkCenterName = dr["WORKCENTERNAME"].ToString();
                            oKPITransportFailItem.A1FromPosCode = dr["A1FROMPOSCODE"].ToString();
                            oKPITransportFailItem.A1ToPosCode = dr["A1TOPOSCODE"].ToString();
                            oKPITransportFailItem.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oKPITransportFailItem.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oKPITransportFailItem.A1StatusDateTime = dr["A1STATUSDATETIME"].ToString();
                            oKPITransportFailItem.A2PosCode = dr["A2POSCODE"].ToString();
                            oKPITransportFailItem.A2StatusDateTime = dr["A2STATUSDATETIME"].ToString();
                            oKPITransportFailItem.A3StatusDateTime = dr["A3STATUSDATETIME"].ToString();
                            oKPITransportFailItem.TimeHub1 = dr["TIMEHUB1"].ToString();
                            oKPITransportFailItem.A4PosCode = dr["A4POSCODE"].ToString();
                            oKPITransportFailItem.A4StatusDateTime = dr["A4STATUSDATETIME"].ToString();
                            oKPITransportFailItem.A5StatusDateTime = dr["A5STATUSDATETIME"].ToString();
                            oKPITransportFailItem.TimeHub2 = dr["TIMEHUB2"].ToString();
                            oKPITransportFailItem.A6PosCode = dr["A6POSCODE"].ToString();
                            oKPITransportFailItem.A6StatusDateTime = dr["A6STATUSDATETIME"].ToString();
                            oKPITransportFailItem.TimeInterval = dr["TIMEINTERVAL"].ToString();
                            oKPITransportFailItem.Note = dr["NOTE"].ToString();
                            listKPITransportFail.Add(oKPITransportFailItem);
                        }
                        _returnkpiTransportFail.Code = "00";
                        _returnkpiTransportFail.Message = "Lấy dữ liệu thành công.";
                        _returnkpiTransportFail.ListKpiTransportFailReport = listKPITransportFail;
                    }
                    else
                    {
                        _returnkpiTransportFail.Code = "01";
                        _returnkpiTransportFail.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiTransportFail.Code = "99";
                _returnkpiTransportFail.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiTransportFail;
        }
        # endregion  
        #region THhub          
        public ReturnKPITransportTotal THHub(int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataKPITransportTotal _metadata1 = new MetaDataKPITransportTotal();
            Convertion common = new Convertion();
            ReturnKPITransportTotal _returnPostMan = new ReturnKPITransportTotal();


            List<THHubDetail> listTHHubDetail = null;
            THHubDetail oTHHubDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_Transport.Detail_KTVC_HUB", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();

                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTHHubDetail = new List<THHubDetail>();
                        while (dr.Read())
                        {
                            oTHHubDetail = new THHubDetail();
                            oTHHubDetail.STT = a++;
                            oTHHubDetail.WorkCenter = dr["WORKCENTER"].ToString();
                            oTHHubDetail.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oTHHubDetail.Total = dr["TOTAL"].ToString();
                            oTHHubDetail.SLSuccess = dr["SLSUCCESS"].ToString();
                            oTHHubDetail.TLSuccess = dr["TLSUCCESS"].ToString();
                            oTHHubDetail.SLFail = dr["SLFAIL"].ToString();
                            oTHHubDetail.TLFail = dr["TLFAIL"].ToString();
                            oTHHubDetail.Total0_16 = dr["TOTAL0_16"].ToString();
                            oTHHubDetail.SL0_16 = dr["SL0_16"].ToString();
                            oTHHubDetail.TL0_16 = dr["TL0_16"].ToString();
                            oTHHubDetail.SLFail0_16 = dr["SLFAIL0_16"].ToString();
                            oTHHubDetail.TLFail0_16 = dr["TLFAIL0_16"].ToString();
                            oTHHubDetail.Total16_24 = dr["TOTAL16_24"].ToString();
                            oTHHubDetail.SL16_24 = dr["SL16_24"].ToString();
                            oTHHubDetail.TL16_24 = dr["TL16_24"].ToString();
                            oTHHubDetail.SLFail16_24 = dr["SLFAIL16_24"].ToString();
                            oTHHubDetail.TLFail16_24 = dr["TLFAIL16_24"].ToString();
                            listTHHubDetail.Add(oTHHubDetail);

                        }
                        _returnPostMan.Code = "00";
                        _returnPostMan.Message = "Lấy dữ liệu thành công.";
                        _returnPostMan.ListTHHubDetailReport = listTHHubDetail;
                       
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

                _returnPostMan.Code = "99";
                _returnPostMan.Message = "Lỗi xử lý dữ liệu";
                _returnPostMan.ListTHHubDetailReport = null;
            }
            return _returnPostMan;
        }

        #endregion
    }

}

