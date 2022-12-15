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
    public class KPIHTRepository
    {

        #region GETLISTBUUCUC
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public string GetProvince()
        {
            List<PROVINCE> listProvince = null;
            PROVINCE oGetProvince = null;
            string LISTPROVINCE = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "KPI_DETAIL_HT.Detail_Province_Ems";
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
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetProvince" + ex.Message);
                LISTPROVINCE = null;
            }

            return LISTPROVINCE;
        }

        #endregion
        //Phần TONG HOP
        #region KPI_TOTAL_HT          
        public ResponseKPI_Total_HT KPI_TOTAL_HT(int StartProvince,int EndProvince,int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataAffair _metadataAffair = new MetaDataAffair();
            Convertion common = new Convertion();
            ResponseKPI_Total_HT _returnKPI_Total_HT = new ResponseKPI_Total_HT();

            List<KPI_Total_HT> listKPI_Total_HT = null;
            KPI_Total_HT oKPI_Total_HT = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("KPI_DETAIL_HT.Detail_Total_HT", Helper.OraDCOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                  
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Total_HT = new List<KPI_Total_HT>();
                        while (dr.Read())
                        {
                            oKPI_Total_HT = new KPI_Total_HT();
                            oKPI_Total_HT.STT = a++;
                            oKPI_Total_HT.StartProvince = dr["STARTPROVINCE"].ToString();
                            oKPI_Total_HT.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oKPI_Total_HT.Endprovince = dr["ENDPROVINCE"].ToString();
                            oKPI_Total_HT.EndprovinceName = dr["ENDPROVINCENAME"].ToString();
                            oKPI_Total_HT.Target = dr["TARGET"].ToString();
                            oKPI_Total_HT.Total = dr["TOTAL"].ToString();
                            oKPI_Total_HT.SLSuccess = dr["SLSUCCESS"].ToString();
                            oKPI_Total_HT.TLSuccess = dr["TLSUCCESS"].ToString();
                            oKPI_Total_HT.IsFailStatus = dr["ISFAILSTATUS"].ToString();
                            oKPI_Total_HT.TLIsFailStatus = dr["TLISFAILSTATUS"].ToString();
                            oKPI_Total_HT.AcceptFrom_Cutoff = dr["ACCEPTFROM_CUTOFF"].ToString();
                            oKPI_Total_HT.AcceptTo_Cutoff = dr["ACCEPTTO_CUTOFF"].ToString();
                            oKPI_Total_HT.Delivery_Cutoff = dr["DELIVERY_CUTOFF"].ToString();
                            listKPI_Total_HT.Add(oKPI_Total_HT);
                        }
                        _returnKPI_Total_HT.Code = "00";
                        _returnKPI_Total_HT.Message = "Lấy dữ liệu thành công.";
                        _returnKPI_Total_HT.listKPI_Total_HTReport = listKPI_Total_HT;
                    }
                    else
                    {
                        _returnKPI_Total_HT.Code = "01";
                        _returnKPI_Total_HT.Message = "Không có dữ liệu";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _returnKPI_Total_HT.Code = "99";
                _returnKPI_Total_HT.Message = "Lỗi xử lý dữ liệu";
               
            }
            return _returnKPI_Total_HT;
        }
        #endregion

        public ResponseKPI_Detail_Fail_Hub KPI_ITEM_HUB_FAIL(int StartProvince, int EndProvince,int AcceptFromCutOff,int AcceptToCutOff,int DeliveryCutOff , int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            ResponseKPI_Detail_Fail_Hub _returnKPI_Detail_Fail_Hub = new ResponseKPI_Detail_Fail_Hub();

            List<KPI_Detail_Fail_Hub> listKPI_Detail_Fail_Hub = null;
            KPI_Detail_Fail_Hub oKPI_Detail_Fail_Hub = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_HT.Detail_Item_HUB_Fail", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_acceptFrom_cutoff", OracleDbType.Int32).Value = AcceptFromCutOff;
                    myCommand.Parameters.Add("v_acceptto_cutoff", OracleDbType.Int32).Value = AcceptToCutOff;
                    myCommand.Parameters.Add("v_Delivery_Cutoff", OracleDbType.Int32).Value = DeliveryCutOff;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Detail_Fail_Hub = new List<KPI_Detail_Fail_Hub>();
                        while (dr.Read())
                        {
                            oKPI_Detail_Fail_Hub = new KPI_Detail_Fail_Hub();
                            oKPI_Detail_Fail_Hub.STT = a++;
                            oKPI_Detail_Fail_Hub.Itemcode = dr["ITEMCODE"].ToString();
                            oKPI_Detail_Fail_Hub.A1PosCode = dr["A1POSCODE"].ToString();
                            oKPI_Detail_Fail_Hub.A1PosCodeName = dr["A1POSCODENAME"].ToString();
                            oKPI_Detail_Fail_Hub.A1StatusDatetime = dr["A1STATUSDATETIME"].ToString();
                            oKPI_Detail_Fail_Hub.A2PosCode = dr["A2POSCODE"].ToString();
                            oKPI_Detail_Fail_Hub.A2PosCodeName = dr["A2POSCODENAME"].ToString();
                            oKPI_Detail_Fail_Hub.A2StatusDateTime = dr["A2STATUSDATETIME"].ToString();
                            oKPI_Detail_Fail_Hub.StartProvince = dr["STARTPROVINCE"].ToString();
                            oKPI_Detail_Fail_Hub.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oKPI_Detail_Fail_Hub.Endprovince = dr["ENDPROVINCE"].ToString();
                            oKPI_Detail_Fail_Hub.EndprovinceName = dr["ENDPROVINCENAME"].ToString();
                            oKPI_Detail_Fail_Hub.Target = dr["TARGET"].ToString();
                            oKPI_Detail_Fail_Hub.IsFailstatus = dr["ISFAILSTATUS"].ToString();
                            listKPI_Detail_Fail_Hub.Add(oKPI_Detail_Fail_Hub);
                        }
                        _returnKPI_Detail_Fail_Hub.Code = "00";
                        _returnKPI_Detail_Fail_Hub.Message = "Lấy dữ liệu thành công.";
                        _returnKPI_Detail_Fail_Hub.listKPI_Detail_Fail_HubReport = listKPI_Detail_Fail_Hub;
                    }
                    else
                    {
                        _returnKPI_Detail_Fail_Hub.Code = "01";
                        _returnKPI_Detail_Fail_Hub.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnKPI_Detail_Fail_Hub.Code = "99";
                _returnKPI_Detail_Fail_Hub.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnKPI_Detail_Fail_Hub;
        }
    }

}

