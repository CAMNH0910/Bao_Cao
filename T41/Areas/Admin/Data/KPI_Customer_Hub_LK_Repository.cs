using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;

namespace T41.Areas.Admin.Data
{
    public class KPI_Customer_Hub_LK_Repository
    {
        public IEnumerable<GETROUTECODENBP> GetAllROUTECODE(int endpostcode)
        {
            List<GETROUTECODENBP> listGetRouteCode = null;
            GETROUTECODENBP oGetRouteCode = null;

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
                        listGetRouteCode = new List<GETROUTECODENBP>();
                        while (dr.Read())
                        {
                            oGetRouteCode = new GETROUTECODENBP();
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
        public ReturnKPI_Customer_Hub_LK KPI_Customer_Hub_LK(int StartDate, int EndDate, int EndPostCode, int CaKT, int IsService, int Routecode, int zone)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Customer_Hub_LK _ReturnKPI_Customer = new ReturnKPI_Customer_Hub_LK();
            List<KPI_Customer_Hub_LK> listKPI_Customer = null;
            KPI_Customer_Hub_LK oKPI_Customer = null;
            int a = 1;
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Customer_Hub_LK_New.KPI_Customer_Hub_LK_New_List", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_EndPostCode", OracleDbType.Int32).Value = EndPostCode;
                    myCommand.Parameters.Add("v_CaKT", OracleDbType.Int32).Value = CaKT;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_Routecode", OracleDbType.Int32).Value = Routecode;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Customer = new List<KPI_Customer_Hub_LK>();
                        while (dr.Read())
                        {
                            oKPI_Customer = new KPI_Customer_Hub_LK();
                            oKPI_Customer.STT = a++;
                            oKPI_Customer.ITEMCODE = dr["ITEMCODE"].ToString();
                            oKPI_Customer.CUSTOMERCODE = dr["CUSTOMERCODE"].ToString();
                            oKPI_Customer.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oKPI_Customer.STATUS = dr["STATUS"].ToString();
                            oKPI_Customer.DELIVERYDATE = dr["DELIVERYDATE"].ToString();
                            oKPI_Customer.ENDPOSTCODENAME = dr["ENDPOSTCODENAME"].ToString();
                            oKPI_Customer.ROUTECODE = dr["ROUTECODE"].ToString();
                            oKPI_Customer.AMND_DATE = dr["AMND_DATE"].ToString();
                            oKPI_Customer.Status_LK = dr["Status_LK"].ToString();
                            listKPI_Customer.Add(oKPI_Customer);
                        }
                        _ReturnKPI_Customer.Code = "00";
                        _ReturnKPI_Customer.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Customer.ListKPI_Customer_Hub_LKReport = listKPI_Customer;
                    }
                    else
                    {
                        _ReturnKPI_Customer.Code = "01";
                        _ReturnKPI_Customer.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Customer.Code = "99";
                _ReturnKPI_Customer.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Customer;
        }

        public ReturnKPI_Customer_Hub_LK EXKPI_Customer_Hub_LK(int StartDate, int EndDate, int EndPostCode, int CaKT, int IsService, int Routecode, int zone)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Customer_Hub_LK _ReturnKPI_Customer = new ReturnKPI_Customer_Hub_LK();
            List<EXKPI_Customer_Hub_LK> listKPI_Customer = null;
            EXKPI_Customer_Hub_LK oKPI_Customer = null;
            int a = 1;
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Customer_Hub_LK_New.KPI_Customer_Hub_LK_New_List", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();

                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_EndPostCode", OracleDbType.Int32).Value = EndPostCode;
                    myCommand.Parameters.Add("v_CaKT", OracleDbType.Int32).Value = CaKT;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_Routecode", OracleDbType.Int32).Value = Routecode;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;



                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Customer = new List<EXKPI_Customer_Hub_LK>();
                        while (dr.Read())
                        {
                            oKPI_Customer = new EXKPI_Customer_Hub_LK();
                            oKPI_Customer.STT = a++;
                            oKPI_Customer.ITEMCODE = dr["ITEMCODE"].ToString();
                            oKPI_Customer.CUSTOMERCODE = dr["CUSTOMERCODE"].ToString();
                            oKPI_Customer.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oKPI_Customer.STATUS = dr["STATUS"].ToString();
                            oKPI_Customer.DELIVERYDATE = dr["DELIVERYDATE"].ToString();
                            oKPI_Customer.ENDPOSTCODENAME = dr["ENDPOSTCODENAME"].ToString();
                            oKPI_Customer.ROUTECODE = dr["ROUTECODE"].ToString();
                            oKPI_Customer.AMND_DATE = dr["AMND_DATE"].ToString();
                            oKPI_Customer.Status_LK = dr["Status_LK"].ToString();
                            listKPI_Customer.Add(oKPI_Customer);
                        }
                        _ReturnKPI_Customer.Code = "00";
                        _ReturnKPI_Customer.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Customer.ListKPI_EXCustomerReport = listKPI_Customer;
                    }
                    else
                    {
                        _ReturnKPI_Customer.Code = "01";
                        _ReturnKPI_Customer.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Customer.Code = "99";
                _ReturnKPI_Customer.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Customer;
        }
    }
}