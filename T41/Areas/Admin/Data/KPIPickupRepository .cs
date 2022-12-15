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
        # endregion
    }

}

