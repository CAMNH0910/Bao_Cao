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
    public class KPI_Delivery_WageRepository
    {
        public ReturnKPI_Wage KPI_Delivery_Wage_DETAIL(int startdate, int enddate, int zone, int endpostcode, int service)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnKPI_Wage = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Total", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listKPI_Delivery_Wage = new List<KPI_Delivery_Wage>();
                        while (dr.Read())
                        {

                            var oKPI_Delivery_Wage = new KPI_Delivery_Wage();
                            oKPI_Delivery_Wage.STT = a++;
                            oKPI_Delivery_Wage.ZONE = dr["ZONE"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oKPI_Delivery_Wage.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oKPI_Delivery_Wage.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oKPI_Delivery_Wage.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            oKPI_Delivery_Wage.ServiceTypeNumber = dr["ServiceTypeNumber"].ToString();
                            oKPI_Delivery_Wage.Total = dr["Total"].ToString();
                            oKPI_Delivery_Wage.SL_DG = dr["SL_DG"].ToString();
                            oKPI_Delivery_Wage.KXD = dr["KXD"].ToString();
                            oKPI_Delivery_Wage.TotalPTC = dr["TotalPTC"].ToString();
                            oKPI_Delivery_Wage.TotalPKTC = dr["TotalPKTC"].ToString();
                            oKPI_Delivery_Wage.TC = dr["TC"].ToString();
                            oKPI_Delivery_Wage.PTC6H = dr["PTC6H"].ToString();
                            oKPI_Delivery_Wage.PTC72H = dr["PTC72H"].ToString();
                            
                            listKPI_Delivery_Wage.Add(oKPI_Delivery_Wage);
                        }
                        _ReturnKPI_Wage.Code = "00";
                        _ReturnKPI_Wage.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Wage.ListDetaiKPI_Delivery_Wage = listKPI_Delivery_Wage;
                    }
                    else
                    {
                        _ReturnKPI_Wage.Code = "01";
                        _ReturnKPI_Wage.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Wage.Code = "99";
                _ReturnKPI_Wage.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Wage;
        }

        public ReturnKPI_Wage KPI_Delivery_CTTH(int startdate, int enddate, int zone, int endpostcode, int service)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnDetail_CTTH = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Detail_TH", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_CTTH = new List<Detail_CTTH>();
                        while (dr.Read())
                        {

                            var oDetail_CTTH = new Detail_CTTH();
                            oDetail_CTTH.STT = a++;
                            oDetail_CTTH.Itemcode = dr["Itemcode"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oDetail_CTTH.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oDetail_CTTH.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_CTTH.ROUTECODE = dr["ROUTECODE"].ToString();
                            oDetail_CTTH.ROUTECODENAME = dr["ROUTECODENAME"].ToString();
                            oDetail_CTTH.POSTMAN_ID = dr["POSTMAN_ID"].ToString();
                            oDetail_CTTH.POSTMANName = dr["POSTMANName"].ToString();
                            oDetail_CTTH.STATUSDATETIME = dr["STATUSDATETIME"].ToString();
                            oDetail_CTTH.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            listDetail_CTTH.Add(oDetail_CTTH);
                        }
                        _ReturnDetail_CTTH.Code = "00";
                        _ReturnDetail_CTTH.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetail_CTTH.ListDetail_CTTH = listDetail_CTTH;
                    }
                    else
                    {
                        _ReturnDetail_CTTH.Code = "01";
                        _ReturnDetail_CTTH.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_CTTH.Code = "99";
                _ReturnDetail_CTTH.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDetail_CTTH;
        }

        public ReturnKPI_Wage KPI_Delivery_KGD(int startdate, int enddate, int zone, int endpostcode, int service)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnDetail_KGD = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Detail_KGD", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_KGD = new List<Detail_KGD>();
                        while (dr.Read())
                        {

                            var oDetail_KGD = new Detail_KGD();
                            oDetail_KGD.STT = a++;
                            oDetail_KGD.Itemcode = dr["Itemcode"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oDetail_KGD.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oDetail_KGD.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_KGD.STATUSDATETIME = dr["STATUSDATETIME"].ToString();
                            oDetail_KGD.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            listDetail_KGD.Add(oDetail_KGD);
                        }
                        _ReturnDetail_KGD.Code = "00";
                        _ReturnDetail_KGD.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetail_KGD.ListDetail_KGD = listDetail_KGD;
                    }
                    else
                    {
                        _ReturnDetail_KGD.Code = "01";
                        _ReturnDetail_KGD.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_KGD.Code = "99";
                _ReturnDetail_KGD.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDetail_KGD;
        }



        public ReturnKPI_Wage KPI_Detail_Wage_DETAIL(int startdate, int enddate, int zone, int endpostcode, int service, int routercode, int postman, int date)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnKPI_Wage = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Detail", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_Routercode", OracleDbType.Int32).Value = routercode;
                    myCommand.Parameters.Add("v_Postman", OracleDbType.Int32).Value = postman;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date; 

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);


                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_TKSLP = new List<KPI_Detail_Wage>();
                        while (dr.Read())
                        {

                            var oKPI_Delivery_Wage = new KPI_Detail_Wage();
                            oKPI_Delivery_Wage.STT = a++;
                            oKPI_Delivery_Wage.ZONE = dr["ZONE"].ToString();
                            oKPI_Delivery_Wage.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oKPI_Delivery_Wage.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oKPI_Delivery_Wage.ROUTECODE = dr["ROUTECODE"].ToString();
                            oKPI_Delivery_Wage.ROUTECODENAME = dr["ROUTECODENAME"].ToString();
                            oKPI_Delivery_Wage.POSTMAN_HRM = dr["POSTMAN_HRM"].ToString();
                            oKPI_Delivery_Wage.POSTMAN_ID = dr["POSTMAN_ID"].ToString();
                            oKPI_Delivery_Wage.POSTMANName = dr["POSTMANName"].ToString();
                            oKPI_Delivery_Wage.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            oKPI_Delivery_Wage.ServiceTypeNumber = dr["ServiceTypeNumber"].ToString();
                            oKPI_Delivery_Wage.TotalM = dr["TotalM"].ToString();
                            oKPI_Delivery_Wage.TotalCT = dr["TotalCT"].ToString();
                            oKPI_Delivery_Wage.Total = dr["Total"].ToString();
                            oKPI_Delivery_Wage.TotalPL = dr["TotalPL"].ToString();
                            oKPI_Delivery_Wage.SLD2 = dr["SLD2"].ToString();
                            oKPI_Delivery_Wage.KLD2 = dr["KLD2"].ToString();
                            oKPI_Delivery_Wage.SLT2 = dr["SLT2"].ToString();
                            oKPI_Delivery_Wage.KLT2 = dr["KLT2"].ToString();
                            oKPI_Delivery_Wage.TotalSL = dr["TotalSL"].ToString();
                            oKPI_Delivery_Wage.TotalKL = dr["TotalKL"].ToString();
                            oKPI_Delivery_Wage.TotalKTC = dr["TotalKTC"].ToString();
                            oKPI_Delivery_Wage.TotalAll = dr["TotalAll"].ToString();
                            oKPI_Delivery_Wage.PTC6H = dr["PTC6H"].ToString();
                            oKPI_Delivery_Wage.PTC72H = dr["PTC72H"].ToString();
                            listDetail_TKSLP.Add(oKPI_Delivery_Wage);

                        }
                        _ReturnKPI_Wage.Code = "00";
                        _ReturnKPI_Wage.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Wage.ListDetailKPI_Detail_Wage = listDetail_TKSLP;
                    }
                    else
                    {
                        _ReturnKPI_Wage.Code = "01";
                        _ReturnKPI_Wage.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Wage.Code = "99";
                _ReturnKPI_Wage.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Wage;
        }

        public ReturnKPI_Wage KPI_Delivery_CT(int startdate, int enddate, int zone, int endpostcode, int service, int routercode, int postman)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnDetail_CT = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Detail_CT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_Routercode", OracleDbType.Int32).Value = routercode;
                    myCommand.Parameters.Add("v_Postman", OracleDbType.Int32).Value = postman;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_CT = new List<Detail_CT>();
                        while (dr.Read())
                        {

                            var oDetail_CT = new Detail_CT();
                            oDetail_CT.STT = a++;
                            oDetail_CT.Itemcode = dr["Itemcode"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oDetail_CT.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oDetail_CT.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_CT.ROUTECODE = dr["ROUTECODE"].ToString();
                            oDetail_CT.ROUTECODENAME = dr["ROUTECODENAME"].ToString();
                            oDetail_CT.POSTMAN_ID = dr["POSTMAN_ID"].ToString();
                            oDetail_CT.POSTMANName = dr["POSTMANName"].ToString();
                            oDetail_CT.STATUSDATETIME = dr["STATUSDATETIME"].ToString();
                            oDetail_CT.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            listDetail_CT.Add(oDetail_CT);
                        }
                        _ReturnDetail_CT.Code = "00";
                        _ReturnDetail_CT.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetail_CT.ListDetail_CT = listDetail_CT;
                    }
                    else
                    {
                        _ReturnDetail_CT.Code = "01";
                        _ReturnDetail_CT.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_CT.Code = "99";
                _ReturnDetail_CT.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDetail_CT;
        }

        public ReturnKPI_Wage KPI_Delivery_CTCT(int startdate, int enddate, int zone, int endpostcode, int service, int routercode, int postman)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnDetail_CTCT = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Detail_CTCT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_Routercode", OracleDbType.Int32).Value = routercode;
                    myCommand.Parameters.Add("v_Postman", OracleDbType.Int32).Value = postman;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_CTCT = new List<Detail_CTCT>();
                        while (dr.Read())
                        {

                            var oDetail_CTCT = new Detail_CTCT();
                            oDetail_CTCT.STT = a++;
                            oDetail_CTCT.Itemcode = dr["Itemcode"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oDetail_CTCT.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oDetail_CTCT.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_CTCT.ROUTECODE = dr["ROUTECODE"].ToString();
                            oDetail_CTCT.ROUTECODENAME = dr["ROUTECODENAME"].ToString();
                            oDetail_CTCT.POSTMAN_ID = dr["POSTMAN_ID"].ToString();
                            oDetail_CTCT.POSTMANName = dr["POSTMANName"].ToString();
                            oDetail_CTCT.STATUSDATETIME = dr["STATUSDATETIME"].ToString();
                            oDetail_CTCT.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            listDetail_CTCT.Add(oDetail_CTCT);
                        }
                        _ReturnDetail_CTCT.Code = "00";
                        _ReturnDetail_CTCT.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetail_CTCT.ListDetail_CTCT = listDetail_CTCT;
                    }
                    else
                    {
                        _ReturnDetail_CTCT.Code = "01";
                        _ReturnDetail_CTCT.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_CTCT.Code = "99";
                _ReturnDetail_CTCT.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDetail_CTCT;
        }



        public ReturnKPI_Wage KPI_Delivery_KG(int startdate, int enddate, int zone, int endpostcode,int service)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnDetail_KG = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Total_SL", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                   myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_KG = new List<Detail_KG>();
                        while (dr.Read())
                        {
                            var oDetail_KG = new Detail_KG();
                            oDetail_KG.STT = a++;
                            oDetail_KG.ZONE = dr["ZONE"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oDetail_KG.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oDetail_KG.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_KG.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            oDetail_KG.Total = dr["Total"].ToString();
                            oDetail_KG.N_5KG = dr["N_5KG"].ToString();
                            oDetail_KG.KL_N_5KG = dr["KL_N_5KG"].ToString();
                            oDetail_KG.L_5KG = dr["L_5KG"].ToString();
                            oDetail_KG.KL_L_5KG = dr["KL_L_5KG"].ToString();
                            oDetail_KG.PTC6H = dr["PTC6H"].ToString();
                            oDetail_KG.PTC72H = dr["PTC72H"].ToString();
                            listDetail_KG.Add(oDetail_KG);
                        }
                        _ReturnDetail_KG.Code = "00";
                        _ReturnDetail_KG.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetail_KG.ListDetail_KG = listDetail_KG;
                    }
                    else
                    {
                        _ReturnDetail_KG.Code = "01";
                        _ReturnDetail_KG.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_KG.Code = "99";
                _ReturnDetail_KG.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDetail_KG;
        }


        public ReturnKPI_Wage KPI_Detail_Lai_Xe(int startdate, int enddate, int zone, int endpostcode, int service, int routercode)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnKPI_Wage = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Detail_lai_xe", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_Routercode", OracleDbType.Int32).Value = routercode;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);


                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_TKSLP = new List<KPI_Detail_Lai_Xe>();
                        while (dr.Read())
                        {

                            var oKPI_Delivery_Wage = new KPI_Detail_Lai_Xe();
                            oKPI_Delivery_Wage.STT = a++;
                            oKPI_Delivery_Wage.ZONE = dr["ZONE"].ToString();
                            oKPI_Delivery_Wage.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oKPI_Delivery_Wage.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oKPI_Delivery_Wage.ROUTECODE = dr["ROUTECODE"].ToString();
                            oKPI_Delivery_Wage.ROUTECODENAME = dr["ROUTECODENAME"].ToString();
                            oKPI_Delivery_Wage.POSTMAN_HRM = dr["POSTMAN_HRM"].ToString();
                            oKPI_Delivery_Wage.POSTMAN_ID = dr["POSTMAN_ID"].ToString();
                            oKPI_Delivery_Wage.POSTMANName = dr["POSTMANName"].ToString();
                            oKPI_Delivery_Wage.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            oKPI_Delivery_Wage.ServiceTypeNumber = dr["ServiceTypeNumber"].ToString();
                            oKPI_Delivery_Wage.TotalM = dr["TotalM"].ToString();
                            oKPI_Delivery_Wage.TotalCT = dr["TotalCT"].ToString();
                            oKPI_Delivery_Wage.Total = dr["Total"].ToString();
                            oKPI_Delivery_Wage.TotalPL = dr["TotalPL"].ToString();
                            oKPI_Delivery_Wage.SLD2 = dr["SLD2"].ToString();
                            oKPI_Delivery_Wage.KLD2 = dr["KLD2"].ToString();
                            oKPI_Delivery_Wage.SLT2 = dr["SLT2"].ToString();
                            oKPI_Delivery_Wage.KLT2 = dr["KLT2"].ToString();
                            oKPI_Delivery_Wage.TotalSL = dr["TotalSL"].ToString();
                            oKPI_Delivery_Wage.TotalKL = dr["TotalKL"].ToString();
                            oKPI_Delivery_Wage.TotalKTC = dr["TotalKTC"].ToString();
                            oKPI_Delivery_Wage.TotalAll = dr["TotalAll"].ToString();
                            oKPI_Delivery_Wage.PTC6H = dr["PTC6H"].ToString();
                            oKPI_Delivery_Wage.PTC72H = dr["PTC72H"].ToString();
                            listDetail_TKSLP.Add(oKPI_Delivery_Wage);

                        }
                        _ReturnKPI_Wage.Code = "00";
                        _ReturnKPI_Wage.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Wage.ListKPI_Detail_Lai_Xe = listDetail_TKSLP;
                    }
                    else
                    {
                        _ReturnKPI_Wage.Code = "01";
                        _ReturnKPI_Wage.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Wage.Code = "99";
                _ReturnKPI_Wage.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Wage;
        }

        public ReturnKPI_Wage KPI_Delivery_CT_LX(int startdate, int enddate, int zone, int endpostcode, int service, int routercode, int postman)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnDetail_CT = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Detail_CT_LX", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_Routercode", OracleDbType.Int32).Value = routercode;
                    myCommand.Parameters.Add("v_Postman", OracleDbType.Int32).Value = postman;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_CT = new List<Detail_CT_LX>();
                        while (dr.Read())
                        {

                            var oDetail_CT = new Detail_CT_LX();
                            oDetail_CT.STT = a++;
                            oDetail_CT.Itemcode = dr["Itemcode"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oDetail_CT.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oDetail_CT.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_CT.ROUTECODE = dr["ROUTECODE"].ToString();
                            oDetail_CT.ROUTECODENAME = dr["ROUTECODENAME"].ToString();
                            oDetail_CT.POSTMAN_ID = dr["POSTMAN_ID"].ToString();
                            oDetail_CT.POSTMANName = dr["POSTMANName"].ToString();
                            oDetail_CT.STATUSDATETIME = dr["STATUSDATETIME"].ToString();
                            oDetail_CT.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            listDetail_CT.Add(oDetail_CT);
                        }
                        _ReturnDetail_CT.Code = "00";
                        _ReturnDetail_CT.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetail_CT.ListDetail_CT_LX = listDetail_CT;
                    }
                    else
                    {
                        _ReturnDetail_CT.Code = "01";
                        _ReturnDetail_CT.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_CT.Code = "99";
                _ReturnDetail_CT.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDetail_CT;
        }

        public ReturnKPI_Wage KPI_Delivery_CTCT_LX(int startdate, int enddate, int zone, int endpostcode, int service, int routercode, int postman)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Wage _ReturnDetail_CTCT = new ReturnKPI_Wage();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Delivery_PostMan_New.Get_Detail_CTCT_LX", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = endpostcode;
                    myCommand.Parameters.Add("v_Service", OracleDbType.Int32).Value = service;
                    myCommand.Parameters.Add("v_Routercode", OracleDbType.Int32).Value = routercode;
                    myCommand.Parameters.Add("v_Postman", OracleDbType.Int32).Value = postman;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listDetail_CTCT = new List<Detail_CTCT_LX>();
                        while (dr.Read())
                        {

                            var oDetail_CTCT = new Detail_CTCT_LX();
                            oDetail_CTCT.STT = a++;
                            oDetail_CTCT.Itemcode = dr["Itemcode"].ToString();
                            //oKPI_Delivery_Wage.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oDetail_CTCT.STARTPOSTCODE = dr["STARTPOSTCODE"].ToString();
                            oDetail_CTCT.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_CTCT.ROUTECODE = dr["ROUTECODE"].ToString();
                            oDetail_CTCT.ROUTECODENAME = dr["ROUTECODENAME"].ToString();
                            oDetail_CTCT.POSTMAN_ID = dr["POSTMAN_ID"].ToString();
                            oDetail_CTCT.POSTMANName = dr["POSTMANName"].ToString();
                            oDetail_CTCT.STATUSDATETIME = dr["STATUSDATETIME"].ToString();
                            oDetail_CTCT.ServiceTypeName = dr["ServiceTypeName"].ToString();
                            listDetail_CTCT.Add(oDetail_CTCT);
                        }
                        _ReturnDetail_CTCT.Code = "00";
                        _ReturnDetail_CTCT.Message = "Lấy dữ liệu thành công.";
                        _ReturnDetail_CTCT.ListDetail_CTCT_LX = listDetail_CTCT;
                    }
                    else
                    {
                        _ReturnDetail_CTCT.Code = "01";
                        _ReturnDetail_CTCT.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnDetail_CTCT.Code = "99";
                _ReturnDetail_CTCT.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnDetail_CTCT;
        }
    }
}