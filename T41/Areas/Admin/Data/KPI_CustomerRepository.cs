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
    public class KPI_CustomerRepository
    {
        #region GETLISTBUUCUC
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems

        public string GetProvinceAcceptEms()
        {
            List<ProvinceEms> listProvince = null;
            ProvinceEms oGetProvince = null;
            string LISTPROVINCE = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "KPI_Customer_delivery_Total.Detail_Province_Ems";
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
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetProvinceEms" + ex.Message);
                LISTPROVINCE = null;
            }

            return LISTPROVINCE;
        }
        public string GetProvinceEms()
        {
            List<ProvinceEms> listProvince = null;
            ProvinceEms oGetProvince = null;
            string LISTPROVINCE = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "KPI_Customer_delivery_Total.Detail_Province_Ems";
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
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetProvinceEms" + ex.Message);
                LISTPROVINCE = null;
            }

            return LISTPROVINCE;
        }
        #endregion
        #region KPI_Customer          
        public ReturnKPI_Customer KPI_Customer_DETAIL(int StartProvince, int EndProvince, int StartDate, int EndDate, int IsService, string Customer)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Customer _ReturnKPI_Customer = new ReturnKPI_Customer();
            List<KPI_Customer> listKPI_Customer = null;
            KPI_Customer oKPI_Customer = null;
            int a = 1;
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Customer_delivery_Total.KPI_CUSTOMER_Total", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_Customer", OracleDbType.Varchar2).Value = Customer;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Customer = new List<KPI_Customer>();
                        while (dr.Read())
                        {
                            oKPI_Customer = new KPI_Customer();
                            oKPI_Customer.STT = a++;
                            oKPI_Customer.STARTPROVINCE = dr["STARTPROVINCE"].ToString();
                            oKPI_Customer.STARTPROVINCENAME = dr["STARTPROVINCENAME"].ToString();
                            oKPI_Customer.ENDPROVINCE = dr["ENDPROVINCE"].ToString();
                            oKPI_Customer.ENDPROVINCENAME = dr["ENDPROVINCENAME"].ToString();
                            oKPI_Customer.TongSL = dr["TongSL"].ToString();
                            oKPI_Customer.PTC = dr["PTC"].ToString();
                            oKPI_Customer.TLPTC = dr["TLPTC"].ToString();
                            oKPI_Customer.KTC = dr["KTC"].ToString();
                            oKPI_Customer.TLKTC = dr["TLKTC"].ToString();
                            oKPI_Customer.PCH = dr["PCH"].ToString();
                            oKPI_Customer.TLCH = dr["TLCH"].ToString();
                            oKPI_Customer.CCTT = dr["CCTT"].ToString();
                            oKPI_Customer.TLCCTT = dr["TLCCTT"].ToString();
                            oKPI_Customer.DCT = dr["DCT"].ToString();
                            oKPI_Customer.TLDCT = dr["TLDCT"].ToString();
                            oKPI_Customer.KDCT = dr["KDCT"].ToString();
                            oKPI_Customer.TLKDCT = dr["TLKDCT"].ToString();
                            listKPI_Customer.Add(oKPI_Customer);
                        }
                        _ReturnKPI_Customer.Code = "00";
                        _ReturnKPI_Customer.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Customer.ListKPI_CustomerReport = listKPI_Customer;
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
        public ReturnKPI_Customer LIST_KPI_Customer_FAIL(int StartProvince, int EndProvince, int StartDate, int EndDate, int IsService, string Customer)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Customer returnKPI_Customer = new ReturnKPI_Customer();

            List<KPI_Customer_Detail> listKPI_Customer_Detail = null;
            KPI_Customer_Detail oKPI_Customer_Detail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Customer_delivery_Total.KPI_CUSTOMER_TotalCT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_Customer", OracleDbType.Varchar2).Value = Customer;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Customer_Detail = new List<KPI_Customer_Detail>();
                        while (dr.Read())
                        {
                            oKPI_Customer_Detail = new KPI_Customer_Detail();
                            oKPI_Customer_Detail.STT = a++;
                            oKPI_Customer_Detail.ITEMCODE = dr["ITEMCODE"].ToString();
                            oKPI_Customer_Detail.STATUSDATE = dr["STATUSDATE"].ToString();
                            oKPI_Customer_Detail.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oKPI_Customer_Detail.STARTPROVINCENAME = dr["STARTPROVINCENAME"].ToString();
                            oKPI_Customer_Detail.DELIVERYDATE = dr["DELIVERYDATE"].ToString();
                            oKPI_Customer_Detail.ENDPOSTCODENAME = dr["ENDPOSTCODENAME"].ToString();
                            oKPI_Customer_Detail.CAUSECODE = dr["CAUSECODE"].ToString();
                            oKPI_Customer_Detail.FREQOCCUR = dr["FREQOCCUR"].ToString();

                            listKPI_Customer_Detail.Add(oKPI_Customer_Detail);
                        }
                        returnKPI_Customer.Code = "00";
                        returnKPI_Customer.Message = "Lấy dữ liệu thành công.";
                        returnKPI_Customer.ListKPI_Customer_DetailReport = listKPI_Customer_Detail;
                    }
                    else
                    {
                        returnKPI_Customer.Code = "01";
                        returnKPI_Customer.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                returnKPI_Customer.Code = "99";
                returnKPI_Customer.Message = "Lỗi xử lý dữ liệu";

            }
            return returnKPI_Customer;
        }
        public ReturnKPI_Customer LIST_KPI_Customer_KDCT(int StartProvince, int EndProvince, int StartDate, int EndDate, int IsService, string Customer)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Customer returnKPI_CustomerKDCT = new ReturnKPI_Customer();

            List<KPI_Customer_DetailKDCT> listKPI_Customer_Detail = null;
            KPI_Customer_DetailKDCT oKPI_Customer_Detail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Customer_delivery_Total.KPI_CUSTOMER_TotalKDCT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_Customer", OracleDbType.Varchar2).Value = Customer;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Customer_Detail = new List<KPI_Customer_DetailKDCT>();
                        while (dr.Read())
                        {
                            oKPI_Customer_Detail = new KPI_Customer_DetailKDCT();
                            oKPI_Customer_Detail.STT = a++;
                            oKPI_Customer_Detail.ITEMCODE = dr["ITEMCODE"].ToString();
                            oKPI_Customer_Detail.STATUSDATETIME = dr["STATUSDATETIME"].ToString();
                            oKPI_Customer_Detail.STARTPOSTCODENAME = dr["STARTPOSTCODENAME"].ToString();
                            oKPI_Customer_Detail.STARTPROVINCENAME = dr["STARTPROVINCENAME"].ToString();
                            oKPI_Customer_Detail.DELIVERYDATE = dr["DELIVERYDATE"].ToString();
                            oKPI_Customer_Detail.ENDPOSTCODENAME = dr["ENDPOSTCODENAME"].ToString();
                            oKPI_Customer_Detail.STATUS = dr["STATUS"].ToString();
                            oKPI_Customer_Detail.TIMELINE = dr["TIMELINE"].ToString();
                            oKPI_Customer_Detail.CRITERION = dr["CRITERION"].ToString();

                            listKPI_Customer_Detail.Add(oKPI_Customer_Detail);
                        }
                        returnKPI_CustomerKDCT.Code = "00";
                        returnKPI_CustomerKDCT.Message = "Lấy dữ liệu thành công.";
                        returnKPI_CustomerKDCT.ListKPI_Customer_DetailKDCT = listKPI_Customer_Detail;
                    }
                    else
                    {
                        returnKPI_CustomerKDCT.Code = "01";
                        returnKPI_CustomerKDCT.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                returnKPI_CustomerKDCT.Code = "99";
                returnKPI_CustomerKDCT.Message = "Lỗi xử lý dữ liệu";

            }
            return returnKPI_CustomerKDCT;
        }
        public ReturnKPI_Customer KPI_Customer_EXCEL_Total(int StartProvince, int EndProvince, int StartDate, int EndDate, int IsService, string Customer)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Customer kPI_CustomerEX = new ReturnKPI_Customer();
            List<KPI_Customer_Excel> listKPI_Customer = null;
            KPI_Customer_Excel oKPI_Customer = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Customer_delivery_Total.KPI_CUSTOMER_Total", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_Customer", OracleDbType.Varchar2).Value = Customer;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Customer = new List<KPI_Customer_Excel>();
                        while (dr.Read())
                        {
                            oKPI_Customer = new KPI_Customer_Excel();
                            oKPI_Customer.STT = a++;
                            oKPI_Customer.STARTPROVINCE = dr["STARTPROVINCE"].ToString();
                            oKPI_Customer.STARTPROVINCENAME = dr["STARTPROVINCENAME"].ToString();
                            oKPI_Customer.ENDPROVINCE = dr["ENDPROVINCE"].ToString();
                            oKPI_Customer.ENDPROVINCENAME = dr["ENDPROVINCENAME"].ToString();
                            oKPI_Customer.TongSL = dr["TongSL"].ToString();
                            oKPI_Customer.PTC = dr["PTC"].ToString();
                            oKPI_Customer.TLPTC = dr["TLPTC"].ToString();
                            oKPI_Customer.KTC = dr["KTC"].ToString();
                            oKPI_Customer.TLKTC = dr["TLKTC"].ToString();
                            oKPI_Customer.PCH = dr["PCH"].ToString();
                            oKPI_Customer.TLCH = dr["TLCH"].ToString();
                            oKPI_Customer.CCTT = dr["CCTT"].ToString();
                            oKPI_Customer.TLCCTT = dr["TLCCTT"].ToString();
                            oKPI_Customer.DCT = dr["DCT"].ToString();
                            oKPI_Customer.TLDCT = dr["TLDCT"].ToString();
                            oKPI_Customer.KDCT = dr["KDCT"].ToString();
                            oKPI_Customer.TLKDCT = dr["TLKDCT"].ToString();
                            listKPI_Customer.Add(oKPI_Customer);
                        }
                        kPI_CustomerEX.Code = "00";
                        kPI_CustomerEX.Message = "Lấy dữ liệu thành công.";
                        kPI_CustomerEX.ListKPI_EXCustomerReport = listKPI_Customer;
                    }
                    else
                    {
                        kPI_CustomerEX.Code = "01";
                        kPI_CustomerEX.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                kPI_CustomerEX.Code = "99";
                kPI_CustomerEX.Message = "Lỗi xử lý dữ liệu";

            }
            return kPI_CustomerEX;
        }
        #endregion
    }
}