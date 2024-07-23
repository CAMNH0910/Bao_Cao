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
using System.Web.Services.Description;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;

namespace T41.Areas.Admin.Data
{
    public class KPI_Thu_GomRepository
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
        public ReturnKPI_Thu_Gom KPI_Thu_Gom(int StartDate, int EndDate, int zone, int endpostcode)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Thu_Gom _ReturnKPI_Thu_Gom = new ReturnKPI_Thu_Gom();
            var test = Helper.OraPNSOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("REPORT_COLLECT_PKG.REPORT_KPI_Thu_Gom", Helper.OraPNSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("P_PO_CODE", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add(new OracleParameter("P_OUT_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listKPI_Thu_Gom = new List<KPI_Thu_Gom>();
                        while (dr.Read())
                        {

                            var oKPI_Thu_Gom = new KPI_Thu_Gom();
                            oKPI_Thu_Gom.STT = a++;
                            oKPI_Thu_Gom.PO_CODE = dr["PO_CODE"].ToString();
                            //oKPI_Thu_Gom.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oKPI_Thu_Gom.PO_NAME = dr["PO_NAME"].ToString();
                            oKPI_Thu_Gom.ROUTE_CODE = dr["ROUTE_CODE"].ToString();
                            oKPI_Thu_Gom.ROUTE_NAME = dr["ROUTE_NAME"].ToString();
                            oKPI_Thu_Gom.Id_Postman = dr["Id_Postman"].ToString();
                            oKPI_Thu_Gom.PostMan_Name = dr["PostMan_Name"].ToString();
                            oKPI_Thu_Gom.Total = dr["Total"].ToString();
                            oKPI_Thu_Gom.ARRIVED_WEIGHT = dr["ARRIVED_WEIGHT"].ToString();

                            listKPI_Thu_Gom.Add(oKPI_Thu_Gom);
                        }
                        _ReturnKPI_Thu_Gom.Code = "00";
                        _ReturnKPI_Thu_Gom.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Thu_Gom.ListKPI_Thu_Gom = listKPI_Thu_Gom;
                    }
                    else
                    {
                        _ReturnKPI_Thu_Gom.Code = "01";
                        _ReturnKPI_Thu_Gom.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Thu_Gom.Code = "99";
                _ReturnKPI_Thu_Gom.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Thu_Gom;
        }
    }
}