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
    public class KPIPickupRepository
    {


        #region GetAllPOSCODE
        //Lấy mã bưu cục phát dưới DB Procedure Detail_DeliveryPosCode_Ems
        public IEnumerable<GetPosCodeGD> GetAllPOSCODE(int zone)
        {
            List<GetPosCodeGD> listGetPosCode = null;
            GetPosCodeGD oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "KPI_DETAIL_PICKUP.Detail_PosCode_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_Zone", OracleDbType.Int32)).Value = zone;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<GetPosCodeGD>();
                        while (dr.Read())
                        {
                            oGetPosCode = new GetPosCodeGD();
                            oGetPosCode.PosCode = int.Parse(dr["POSCODE"].ToString());
                            oGetPosCode.PosName = dr["POSNAME"].ToString();
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
        public IEnumerable<GetPosCodeGD> GetAllPOSCODELOG()
        {
            List<GetPosCodeGD> listGetPosCode = null;
            GetPosCodeGD oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "KPI_DETAIL_PICKUP.Detail_PosCode_Ems_Log";
                    cm.CommandType = CommandType.StoredProcedure;                  
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<GetPosCodeGD>();
                        while (dr.Read())
                        {
                            oGetPosCode = new GetPosCodeGD();
                            oGetPosCode.PosCode = int.Parse(dr["POSCODE"].ToString());
                            oGetPosCode.PosName = dr["POSNAME"].ToString();
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
        public IEnumerable<GetPosCodePickup> GetAllPOSCODEPickup(string unit)
        {
            List<GetPosCodePickup> listGetPosCode = null;
            GetPosCodePickup oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraPNSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "DIC_PKG.POS_GET";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("P_POCODE", OracleDbType.Varchar2)).Value = null;
                    cm.Parameters.Add(new OracleParameter("P_UNIT_CODE", OracleDbType.Varchar2)).Value = unit;
                    cm.Parameters.Add(new OracleParameter("P_PROVINCE_CODE", OracleDbType.Int32)).Value = null;
                    cm.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, null, ParameterDirection.Output);
                    cm.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<GetPosCodePickup>();
                        while (dr.Read())
                        {
                            oGetPosCode = new GetPosCodePickup();
                            oGetPosCode.PosCode = dr["PO_CODE"].ToString();
                            oGetPosCode.PosName = dr["POSNAME"].ToString();
                            listGetPosCode.Add(oGetPosCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllPOSCODEPNS" + ex.Message);
                listGetPosCode = null;
            }

            return listGetPosCode;
        }

        public IEnumerable<GetRouteCodePickup> GetAllROUTECODEPickup(string Poscode)
        {
            List<GetRouteCodePickup> listGetPosCode = null;
            GetRouteCodePickup oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraPNSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "DIC_PKG.ROUTE_GET";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("P_POCODE", OracleDbType.Varchar2)).Value = Poscode;
                    cm.Parameters.Add(new OracleParameter("P_ROUTECODE", OracleDbType.Varchar2)).Value = null;
                    cm.Parameters.Add(new OracleParameter("P_DISTRICTID", OracleDbType.Int32)).Value = null;
                    cm.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Int32)).Value = null;
                    cm.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, null, ParameterDirection.Output);
                    cm.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<GetRouteCodePickup>();
                        while (dr.Read())
                        {
                            oGetPosCode = new GetRouteCodePickup();
                            oGetPosCode.RouteCode = dr["CODE"].ToString();
                            oGetPosCode.RouteName = dr["NAME"].ToString();
                            listGetPosCode.Add(oGetPosCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllPOSCODEPNS" + ex.Message);
                listGetPosCode = null;
            }

            return listGetPosCode;
        }
        #endregion
        //Phần TONG HOP
        #region KPIPICKUP        
        public ReturnKPIPickup KPIPickup(int Zone, int PosCode, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPIPickup _metadatakpiPickup = new MetaDataKPIPickup();
            Convertion common = new Convertion();
            ReturnKPIPickup _returnkpiPickup = new ReturnKPIPickup();
            _metadatakpiPickup.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            _returnkpiPickup.MetaDataKPIPickup = _metadatakpiPickup;
            List<KPIPickupTotal> listKPIPickupTotal = null;
            KPIPickupTotal oPickupDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_PICKUP.Detail_Total_PickUp", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = Zone;
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.Int32).Value = PosCode;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIPickupTotal = new List<KPIPickupTotal>();
                        while (dr.Read())
                        {
                            oPickupDetail = new KPIPickupTotal();
                            oPickupDetail.STT = a++;
                            oPickupDetail.Total = dr["TOTAL"].ToString();
                            oPickupDetail.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oPickupDetail.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oPickupDetail.TimeintervalUpTo6 = dr["TIMEINTERVALUPTO6"].ToString();
                            oPickupDetail.TLTimeintervalUpTo6 = dr["TLTIMEINTERVALUPTO6"].ToString();
                            oPickupDetail.TimeintervalUpTo10 = dr["TIMEINTERVALUPTO10"].ToString();
                            oPickupDetail.TLTimeintervalUpTo10 = dr["TLTIMEINTERVALUPTO10"].ToString();
                            oPickupDetail.TimeintervalExceed10 = dr["TIMEINTERVALEXCEED10"].ToString();
                            oPickupDetail.TLTimeintervalExceed10 = dr["TLTIMEINTERVALEXCEED10"].ToString();
                            oPickupDetail.TimeintervalUpTo3GD = dr["TIMEINTERVALUPTO3GD"].ToString();
                            oPickupDetail.TimeintervalUpTo3VC = dr["TIMEINTERVALUPTO3VC"].ToString();
                            listKPIPickupTotal.Add(oPickupDetail);
                        }
                        _returnkpiPickup.Code = "00";
                        _returnkpiPickup.Message = "Lấy dữ liệu thành công.";
                        _returnkpiPickup.ListKPIPickupReport = listKPIPickupTotal;
                    }
                    else
                    {
                        _returnkpiPickup.Code = "01";
                        _returnkpiPickup.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiPickup.Code = "99";
                _returnkpiPickup.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiPickup;
        }
        public ReturnKPIPickup KPIPickupLOG(int PosCode, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPIPickup _metadatakpiPickup = new MetaDataKPIPickup();
            Convertion common = new Convertion();
            ReturnKPIPickup _returnkpiPickup = new ReturnKPIPickup();
            _metadatakpiPickup.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            _returnkpiPickup.MetaDataKPIPickup = _metadatakpiPickup;
            List<KPIPickupTotal> listKPIPickupTotal = null;
            KPIPickupTotal oPickupDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_PICKUP.Detail_Total_PickUp_LOG", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                   
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.Int32).Value = PosCode;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIPickupTotal = new List<KPIPickupTotal>();
                        while (dr.Read())
                        {
                            oPickupDetail = new KPIPickupTotal();
                            oPickupDetail.STT = a++;
                            oPickupDetail.Total = dr["TOTAL"].ToString();
                            oPickupDetail.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oPickupDetail.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oPickupDetail.TimeintervalUpTo6 = dr["TIMEINTERVAL"].ToString();
                            oPickupDetail.TLTimeintervalUpTo6 = dr["TLTIMEINTERVAL"].ToString();
                        
                            listKPIPickupTotal.Add(oPickupDetail);
                        }
                        _returnkpiPickup.Code = "00";
                        _returnkpiPickup.Message = "Lấy dữ liệu thành công.";
                        _returnkpiPickup.ListKPIPickupReport = listKPIPickupTotal;
                    }
                    else
                    {
                        _returnkpiPickup.Code = "01";
                        _returnkpiPickup.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiPickup.Code = "99";
                _returnkpiPickup.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiPickup;
        }
        public ReturnKPIPickupSuccess KPIPickupSuccess(int StartDate, int EndDate, string district, string poscode, int routecode)
        {
            DataTable da = new DataTable();
            MetaDataKPIPickup _metadatakpiPickup = new MetaDataKPIPickup();
            Convertion common = new Convertion();
            ReturnKPIPickupSuccess _returnkpiPickup = new ReturnKPIPickupSuccess();
            List<KPIPickupSuccess> listKPIPickupTotal = null;
            KPIPickupSuccess oPickupDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("REPORT_COLLECT_PKG.REPORT_COLLECT_003", Helper.OraPNSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_FROM_DATE", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("P_TO_DATE", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("P_PO_DISTRICT_CODE", OracleDbType.Varchar2).Value = district;
                    myCommand.Parameters.Add("P_PO_CODE", OracleDbType.Varchar2).Value = (poscode == "0") ? null : poscode;
                    myCommand.Parameters.Add("P_ROUTE_ID", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("P_DATE_TYPE", OracleDbType.Int32).Value = 0;
                    myCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, null, ParameterDirection.Output);
                    myCommand.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIPickupTotal = new List<KPIPickupSuccess>();
                        while (dr.Read())
                        {
                            oPickupDetail = new KPIPickupSuccess();
                            oPickupDetail.STT = a++;
                            oPickupDetail.PO_NAME = dr["PO_CODE"].ToString() + " - " + dr["PO_NAME"].ToString();
                            oPickupDetail.ROUTE_NAME = dr["ROUTE_CODE"].ToString() + " - " + dr["ROUTE_NAME"].ToString();
                            oPickupDetail.TOTAL_QUANTITY = dr["TOTAL_QUANTITY"].ToString();
                            oPickupDetail.COLLECT_SUCCESS_QUANTITY = dr["COLLECT_SUCCESS_QUANTITY"].ToString();
                            oPickupDetail.ARRIVED_WEIGHT = dr["ARRIVED_WEIGHT"].ToString();
                            oPickupDetail.COLLECT_SUCCESS_PERCENT = dr["COLLECT_SUCCESS_PERCENT"].ToString();
                            oPickupDetail.COLLECT_NOT_SUCCESS_QUANTITY = dr["COLLECT_NOT_SUCCESS_QUANTITY"].ToString();
                            oPickupDetail.COLLECT_NOT_SUCCESS_QUANTITY_0 = dr["COLLECT_NOT_SUCCESS_QUANTITY_0"].ToString();
                            oPickupDetail.COLLECT_NOT_SUCCESS_QUANTITY_1 = dr["COLLECT_NOT_SUCCESS_QUANTITY_1"].ToString();
                            oPickupDetail.COLLECT_NOT_SUCCESS_QUANTITY_2 = dr["COLLECT_NOT_SUCCESS_QUANTITY_2"].ToString();
                            oPickupDetail.COLLECT_NOT_SUCCESS_QUANTITY_3 = dr["COLLECT_NOT_SUCCESS_QUANTITY_3"].ToString();
                            oPickupDetail.COLLECT_NOT_SUCCESS_QUANTITY_4 = dr["COLLECT_NOT_SUCCESS_QUANTITY_4"].ToString();
                            oPickupDetail.COLLECT_NOT_SUCCESS_QUANTITY_5 = dr["COLLECT_NOT_SUCCESS_QUANTITY_5"].ToString();
                            listKPIPickupTotal.Add(oPickupDetail);
                        }
                        _returnkpiPickup.Code = "00";
                        _returnkpiPickup.Message = "Lấy dữ liệu thành công.";
                        _returnkpiPickup.ListKPIPickupSuccessReport = listKPIPickupTotal;
                    }
                    else
                    {
                        _returnkpiPickup.Code = "01";
                        _returnkpiPickup.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiPickup.Code = "99";
                _returnkpiPickup.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiPickup;
        }

        public ReturnKPIPickupSuccessDQD KPIPickupSuccessDQD(int StartDate, int EndDate, string district, string poscode, int routecode)
        {
            DataTable da = new DataTable();
            MetaDataKPIPickup _metadatakpiPickup = new MetaDataKPIPickup();
            Convertion common = new Convertion();
            ReturnKPIPickupSuccessDQD _returnkpiPickup = new ReturnKPIPickupSuccessDQD();
            List<KPIPickupSuccessDQD> listKPIPickupTotal = null;
            KPIPickupSuccessDQD oPickupDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("REPORT_COLLECT_PKG.REPORT_COLLECT_004", Helper.OraPNSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_FROM_DATE", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("P_TO_DATE", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("P_PO_DISTRICT_CODE", OracleDbType.Varchar2).Value = district;
                    myCommand.Parameters.Add("P_PO_CODE", OracleDbType.Varchar2).Value = (poscode == "0") ? null : poscode;
                    myCommand.Parameters.Add("P_ROUTE_ID", OracleDbType.Int32).Value = routecode;
                    myCommand.Parameters.Add("P_DATE_TYPE", OracleDbType.Int32).Value = 1;
                    myCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, null, ParameterDirection.Output);
                    myCommand.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIPickupTotal = new List<KPIPickupSuccessDQD>();
                        while (dr.Read())
                        {
                            oPickupDetail = new KPIPickupSuccessDQD();
                            oPickupDetail.STT = a++;
                            oPickupDetail.PO_NAME = dr["PO_CODE"].ToString() + " - " + dr["PO_NAME"].ToString();
                            oPickupDetail.ROUTE_NAME = dr["ROUTE_CODE"].ToString() + " - " + dr["ROUTE_NAME"].ToString();
                            oPickupDetail.TOTAL_QUANTITY = dr["TOTAL_QUANTITY"].ToString();
                            oPickupDetail.ARRIVED_QUANTITY = dr["ARRIVED_QUANTITY"].ToString();
                            oPickupDetail.ARRIVED_WEIGHT = dr["ARRIVED_WEIGHT"].ToString();
                            oPickupDetail.ARRIVED_PERCENT = dr["ARRIVED_PERCENT"].ToString();
                            oPickupDetail.BD10_QUANTITY = dr["BD10_QUANTITY"].ToString();
                            oPickupDetail.BD10_WEIGHT = dr["BD10_WEIGHT"].ToString();
                            oPickupDetail.BD10_PERCENT = dr["BD10_PERCENT"].ToString();
                            listKPIPickupTotal.Add(oPickupDetail);
                        }
                        _returnkpiPickup.Code = "00";
                        _returnkpiPickup.Message = "Lấy dữ liệu thành công.";
                        _returnkpiPickup.ListKPIPickupSuccessDQDReport = listKPIPickupTotal;
                    }
                    else
                    {
                        _returnkpiPickup.Code = "01";
                        _returnkpiPickup.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiPickup.Code = "99";
                _returnkpiPickup.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiPickup;
        }
        public ReturnKPITotalPickUpV2 KPIPickupTotalV2(int Zone, int PosCode, int StartDate, int EndDate)
        {
            ReturnKPITotalPickUpV2 _returnkpiPickup = new ReturnKPITotalPickUpV2();
            DataTable da = new DataTable();
            List<TotalKPIPickUpV2> listKPIPickupTotal = null;
            TotalKPIPickUpV2 oPickupDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_PICKUP.Detail_Total_PickUp_V2", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = Zone;
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.Int32).Value = PosCode;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIPickupTotal = new List<TotalKPIPickUpV2>();
                        while (dr.Read())
                        {
                            oPickupDetail = new TotalKPIPickUpV2();
                            oPickupDetail.STT = a++;
                            oPickupDetail.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oPickupDetail.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oPickupDetail.Total = dr["TOTAL"].ToString();
                            oPickupDetail.Timeinterval = dr["TIMEINTERVAL"].ToString();
                            oPickupDetail.TLTimeinterval = dr["TLTIMEINTERVAL"].ToString();
                            oPickupDetail.TotalTC = dr["TOTALTC"].ToString();
                            oPickupDetail.TLTotalTC = dr["TLTOTALTC"].ToString();

                            listKPIPickupTotal.Add(oPickupDetail);
                        }
                        _returnkpiPickup.Code = "00";
                        _returnkpiPickup.Message = "Lấy dữ liệu thành công.";
                        _returnkpiPickup.ListTotalKPIPickUpV2 = listKPIPickupTotal;
                    }
                    else
                    {
                        _returnkpiPickup.Code = "01";
                        _returnkpiPickup.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiPickup.Code = "99";
                _returnkpiPickup.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiPickup;
        }
        #endregion

        //Phần CHI TIET
        # region 
        public ReturnKPIPickupItem KPIPickupItem(int PostCode, int Timeinterval, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPIPickup _metadatakpiPickup = new MetaDataKPIPickup();
            Convertion common = new Convertion();
            ReturnKPIPickupItem _returnkpiPickupItem = new ReturnKPIPickupItem();
            _metadatakpiPickup.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            List<KPIPickupItem> listKPIPickupItem = null;
            KPIPickupItem oKPIPickupItem = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_PICKUP.Detail_Item_Ems_PickUp", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.Int32).Value = PostCode;
                    myCommand.Parameters.Add("v_Timeinterval", OracleDbType.Int32).Value = Timeinterval;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIPickupItem = new List<KPIPickupItem>();
                        while (dr.Read())
                        {
                            oKPIPickupItem = new KPIPickupItem();
                            oKPIPickupItem.STT = a++;
                            oKPIPickupItem.ItemCode = dr["ITEMCODE"].ToString();
                            oKPIPickupItem.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oKPIPickupItem.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oKPIPickupItem.EndProvince = dr["ENDPROVINCE"].ToString();
                            oKPIPickupItem.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oKPIPickupItem.A1StatusDate = dr["A1STATUSDATE"].ToString();
                            oKPIPickupItem.A1StatusTime = dr["A1STATUSTIME"].ToString();
                            oKPIPickupItem.PosCode = dr["POSCODE"].ToString();
                            oKPIPickupItem.PosName = dr["POSNAME"].ToString();
                            oKPIPickupItem.A2StatusDate = dr["A2STATUSDATE"].ToString();
                            oKPIPickupItem.A2StatusTime = dr["A2STATUSTIME"].ToString();
                            oKPIPickupItem.A3StatusDate = dr["A3STATUSDATE"].ToString();
                            oKPIPickupItem.A3StatusTime = dr["A3STATUSTIME"].ToString();
                            oKPIPickupItem.TimeintervalGD = dr["TIMEINTERVALGD"].ToString();
                            oKPIPickupItem.TimeintervalVC = dr["TIMEINTERVALVC"].ToString();
                            oKPIPickupItem.Timeinterval = dr["TIMEINTERVAL"].ToString();
                            listKPIPickupItem.Add(oKPIPickupItem);
                        }
                        _returnkpiPickupItem.Code = "00";
                        _returnkpiPickupItem.Message = "Lấy dữ liệu thành công.";
                        _returnkpiPickupItem.ListKPIPickupItemReport = listKPIPickupItem;
                    }
                    else
                    {
                        _returnkpiPickupItem.Code = "01";
                        _returnkpiPickupItem.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiPickupItem.Code = "99";
                _returnkpiPickupItem.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiPickupItem;
        }
        public ReturnKPIPickupItem KPIPickupItemLog(int PostCode, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPIPickup _metadatakpiPickup = new MetaDataKPIPickup();
            Convertion common = new Convertion();
            ReturnKPIPickupItem _returnkpiPickupItem = new ReturnKPIPickupItem();
            _metadatakpiPickup.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            List<KPIPickupItem> listKPIPickupItem = null;
            KPIPickupItem oKPIPickupItem = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_PICKUP.Detail_Item_Ems_PickUp_Log", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.Int32).Value = PostCode;                   
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIPickupItem = new List<KPIPickupItem>();
                        while (dr.Read())
                        {
                            oKPIPickupItem = new KPIPickupItem();
                            oKPIPickupItem.STT = a++;
                            oKPIPickupItem.ItemCode = dr["ITEMCODE"].ToString();
                            oKPIPickupItem.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oKPIPickupItem.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oKPIPickupItem.EndProvince = dr["ENDPROVINCE"].ToString();
                            oKPIPickupItem.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oKPIPickupItem.A1StatusDate = dr["A1STATUSDATE"].ToString();
                            oKPIPickupItem.A1StatusTime = dr["A1STATUSTIME"].ToString();                         
                            oKPIPickupItem.A2StatusDate = dr["A2STATUSDATE"].ToString();
                            oKPIPickupItem.A2StatusTime = dr["A2STATUSTIME"].ToString();                          
                            listKPIPickupItem.Add(oKPIPickupItem);
                        }
                        _returnkpiPickupItem.Code = "00";
                        _returnkpiPickupItem.Message = "Lấy dữ liệu thành công.";
                        _returnkpiPickupItem.ListKPIPickupItemReport = listKPIPickupItem;
                    }
                    else
                    {
                        _returnkpiPickupItem.Code = "01";
                        _returnkpiPickupItem.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiPickupItem.Code = "99";
                _returnkpiPickupItem.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiPickupItem;
        }
        public ReturnDetailPickUpV2 DetailKPIPickUpV2 (int PostCode, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            ReturnDetailPickUpV2 _returnDetailKpiPickupItem = new ReturnDetailPickUpV2();
            List<DetailKPIPickUpV2> listKPIPickupItem = null;
            DetailKPIPickUpV2 oKPIPickupItem = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_PICKUP.Detail_Item_Ems_PickUp_V2", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.Int32).Value = PostCode;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIPickupItem = new List<DetailKPIPickUpV2>();
                        while (dr.Read())
                        {
                            oKPIPickupItem = new DetailKPIPickUpV2();
                            oKPIPickupItem.STT = a++;
                            oKPIPickupItem.Itemcode = dr["ITEMCODE"].ToString();
                            oKPIPickupItem.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oKPIPickupItem.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oKPIPickupItem.MailRouteName = dr["MAILROUTENAME"].ToString();
                            oKPIPickupItem.A1StatusDate = dr["A1STATUSDATE"].ToString();
                            oKPIPickupItem.A1StatusTime = dr["A1STATUSTIME"].ToString();
                            oKPIPickupItem.A2StatusDate = dr["A2STATUSDATE"].ToString();
                            oKPIPickupItem.A2StatusTime = dr["A2STATUSTIME"].ToString();
                            oKPIPickupItem.TimeExpected = dr["TIMEEXPECTED"].ToString();
                            listKPIPickupItem.Add(oKPIPickupItem);
                        }
                        _returnDetailKpiPickupItem.Code = "00";
                        _returnDetailKpiPickupItem.Message = "Lấy dữ liệu thành công.";
                        _returnDetailKpiPickupItem.ListDetailPickUpV2 = listKPIPickupItem;
                    }
                    else
                    {
                        _returnDetailKpiPickupItem.Code = "01";
                        _returnDetailKpiPickupItem.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnDetailKpiPickupItem.Code = "99";
                _returnDetailKpiPickupItem.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnDetailKpiPickupItem;
        }
        #endregion

        public ReturnDetailPickup DETAIL_PICKUP(string fromdate, string todate, string fromtime, string totime, string poscode, string unit, int routeid, string status,int pageindex,int pagesize,int datatype,int collectquality)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnDetailPickup _ReturnDetailPickup = new ReturnDetailPickup();
            int a = 0;
            List<DetailPickup> listDetailPickup = null;
            DetailPickup oDetailPickup = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("REPORT_PKG.ORDER_PNS01_V2", Helper.OraPNSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_FROM_CREATE_DATE", OracleDbType.Int32).Value = common.DateToInt(fromdate);
                    myCommand.Parameters.Add("P_TO_CREATE_DATE", OracleDbType.Int32).Value = common.DateToInt(todate);
                    myCommand.Parameters.Add("P_FROM_TIME", OracleDbType.Int32).Value = (fromtime != "") ?common.ConvertTimeToInt(fromtime) : 0;
                    myCommand.Parameters.Add("P_TO_TIME", OracleDbType.Int32).Value = (totime != "") ? common.ConvertTimeToInt(totime) : 0;
                    myCommand.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.Varchar2).Value = null;
                    myCommand.Parameters.Add("P_PO_CODE", OracleDbType.Varchar2).Value = null;
                    myCommand.Parameters.Add("P_PO_PICKUP_CODE", OracleDbType.Varchar2).Value = unit;
                    myCommand.Parameters.Add("P_ROUTE_ID", OracleDbType.Int32).Value = routeid;
                    myCommand.Parameters.Add("P_STATUS", OracleDbType.Varchar2).Value = status;
                    myCommand.Parameters.Add("P_PAGE_INDEX", OracleDbType.Int32).Value = pageindex;
                    myCommand.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = pagesize;
                    myCommand.Parameters.Add("P_DATE_TYPE", OracleDbType.Int32).Value = datatype;
                    myCommand.Parameters.Add("P_COLLECT_QUALITY", OracleDbType.Int32).Value = collectquality;
                    myCommand.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add(new OracleParameter("P_OUT_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDetailPickup = new List<DetailPickup>();
                        while (dr.Read())
                        {
                            oDetailPickup = new DetailPickup();
                            oDetailPickup.STT = a++;
                            oDetailPickup.CREATED_DATE = dr["CREATED_DATE"].ToString();
                            oDetailPickup.PO_PICKUP_CODE = dr["PO_PICKUP_CODE"].ToString();
                            oDetailPickup.ORDER_CODE = dr["ORDER_CODE"].ToString();
                            oDetailPickup.ROUTE_CODE = dr["CODE"].ToString() +"-"+ dr["NAME"].ToString();
                            oDetailPickup.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            oDetailPickup.CUSTOMER_NAME = dr["CUSTOMER_NAME"].ToString();
                            oDetailPickup.ADDRESS = dr["PROVINCE_NAME"].ToString() + "-" + dr["DISTRICT_NAME"].ToString() + "-" + dr["WARD_NAME"].ToString() + "-" + dr["PICKUP_STREET"].ToString();
                            oDetailPickup.QUANTITY = dr["QUANTITY"].ToString();
                            oDetailPickup.WEIGH = dr["WEIGH"].ToString();
                            oDetailPickup.GIO_DIEU_TIN = dr["GIO_DIEU_TIN"].ToString();
                            oDetailPickup.REQUEST_PICKUP_TIME = dr["REQUEST_PICKUP_TIME"].ToString();
                            oDetailPickup.GIO_NHAN_TIN = dr["GIO_NHAN_TIN"].ToString();
                            oDetailPickup.GIO_XAC_NHAN_TIN = dr["GIO_XAC_NHAN_TIN"].ToString();
                            oDetailPickup.GIO_XAC_NHAN_DEN = dr["GIO_XAC_NHAN_DEN"].ToString();
                            oDetailPickup.GIO_LAY_HANG_KTC = dr["GIO_LAY_HANG_KTC"].ToString();
                            oDetailPickup.EDIT_ARRIVED_QUANTITY = dr["EDIT_ARRIVED_QUANTITY"].ToString();
                            oDetailPickup.EDIT_ARRIVED_WEIGHT = dr["EDIT_ARRIVED_WEIGHT"].ToString();
                            oDetailPickup.COLLECT_REASON = dr["COLLECT_REASON"].ToString();
                            oDetailPickup.NGUOI_TAO = dr["NGUOI_NHAN"].ToString();
                            oDetailPickup.NGUOI_DIEU = dr["NGUOI_DIEU"].ToString();
                            oDetailPickup.NGUOI_GOM = dr["NGUOI_GOM"].ToString();
                            listDetailPickup.Add(oDetailPickup);

                        }
                        _ReturnDetailPickup.Code = "00";
                        _ReturnDetailPickup.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetailPickup.Total = Convert.ToInt32(myCommand.Parameters["P_TOTAL"].Value.ToString());
                        _ReturnDetailPickup.listDetailPickup_Report = listDetailPickup;
                    }
                    else
                    {
                        _ReturnDetailPickup.Code = "01";
                        _ReturnDetailPickup.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnDetailPickup.Code = "99";
                _ReturnDetailPickup.Message = "Lỗi xử lý dữ liệu";
            }
            return _ReturnDetailPickup;
        }
    }

}

