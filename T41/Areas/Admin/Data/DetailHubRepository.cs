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
    public class DetailHubRepository
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
                    cm.CommandText = Helper.SchemaName + "KPI_DETAIL_HUB.Detail_Province_Ems";
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
                    cm.CommandText = Helper.SchemaName + "KPI_DETAIL_HUB.Detail_Province_Ems";
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
        //Phần TONG HOP
        #region TOTAL_HUB_DETAIL          
        public ReturnDetailHub TOTAL_HUB_DETAIL(int StartProvince,int EndProvince, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataDetailHub _metadataDetailHub = new MetaDataDetailHub();
            Convertion common = new Convertion();
            ReturnDetailHub _returnDetailHub = new ReturnDetailHub();
            List<TotalHub> listTotalHub = null;
            TotalHub oTotalHub = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("KPI_DETAIL_HUB.Detail_Total_HUB", Helper.OraDCOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                  
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                   // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTotalHub = new List<TotalHub>();
                        while (dr.Read())
                        {
                            oTotalHub = new TotalHub();
                            oTotalHub.STT = a++;
                            oTotalHub.StartProvince = dr["STARTPROVINCE"].ToString();
                            oTotalHub.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oTotalHub.IsStartDistrict = dr["ISSTARTDISTRICT"].ToString();
                            oTotalHub.EndProvince = dr["ENDPROVINCE"].ToString();
                            oTotalHub.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oTotalHub.IsEndDistrict = dr["ISENDDISTRICT"].ToString();
                            oTotalHub.IsSDistrict = dr["ISSDISTRICT"].ToString();
                            oTotalHub.IsEDistrict = dr["ISEDISTRICT"].ToString();
                            oTotalHub.IsserviceNumber = dr["ISSERVICENUMBER"].ToString();
                            oTotalHub.Isservice = dr["ISSERVICE"].ToString();
                            oTotalHub.Targets = dr["TARGETS"].ToString();
                            oTotalHub.Total = dr["TOTAL"].ToString();
                            oTotalHub.SLSuccess = dr["SLSUCCESS"].ToString();
                            oTotalHub.TLSuccess = dr["TLSUCCESS"].ToString();
                            oTotalHub.IsFailStatus = dr["ISFAILSTATUS"].ToString();
                            oTotalHub.TLIsFailStatus = dr["TLISFAILSTATUS"].ToString();
                            listTotalHub.Add(oTotalHub);
                        }
                        _returnDetailHub.Code = "00";
                        _returnDetailHub.Message = "Lấy dữ liệu thành công.";
                        _returnDetailHub.ListTotalHubReport = listTotalHub;
                    }
                    else
                    {
                        _returnDetailHub.Code = "01";
                        _returnDetailHub.Message = "Không có dữ liệu";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _returnDetailHub.Code = "99";
                _returnDetailHub.Message = "Lỗi xử lý dữ liệu";
               
            }
            return _returnDetailHub;
        }

        public ReturnDetailHub TOTAL_HUB_DETAIL_EXCEL(int StartProvince, int EndProvince, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataDetailHub _metadataDetailHub = new MetaDataDetailHub();
            Convertion common = new Convertion();
            ReturnDetailHub _returnDetailHub = new ReturnDetailHub();
            List<TotalHub_Excel> listTotalHub = null;
            TotalHub_Excel oTotalHub = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_HUB.Detail_Total_HUB", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTotalHub = new List<TotalHub_Excel>();
                        while (dr.Read())
                        {
                            oTotalHub = new TotalHub_Excel();
                            oTotalHub.STT = a++;
                            oTotalHub.StartProvince = dr["STARTPROVINCE"].ToString();
                            oTotalHub.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oTotalHub.IsStartDistrict = dr["ISSTARTDISTRICT"].ToString();
                            oTotalHub.EndProvince = dr["ENDPROVINCE"].ToString();
                            oTotalHub.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oTotalHub.IsEndDistrict = dr["ISENDDISTRICT"].ToString();
                            oTotalHub.Isservice = dr["ISSERVICE"].ToString();
                            oTotalHub.Targets = dr["TARGETS"].ToString();
                            oTotalHub.Total = dr["TOTAL"].ToString();
                            oTotalHub.SLSuccess = dr["SLSUCCESS"].ToString();
                            oTotalHub.TLSuccess = dr["TLSUCCESS"].ToString();
                            oTotalHub.IsFailStatus = dr["ISFAILSTATUS"].ToString();
                            oTotalHub.TLIsFailStatus = dr["TLISFAILSTATUS"].ToString();
                            listTotalHub.Add(oTotalHub);
                        }
                        _returnDetailHub.Code = "00";
                        _returnDetailHub.Message = "Lấy dữ liệu thành công.";
                        _returnDetailHub.ListTotalHub_ExcelReport = listTotalHub;
                    }
                    else
                    {
                        _returnDetailHub.Code = "01";
                        _returnDetailHub.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnDetailHub.Code = "99";
                _returnDetailHub.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnDetailHub;
        }
        #endregion

        public ReturnDetailHub DETAIL_HUB_FAIL(int StartProvince, int EndProvince,int StartDistrict,int enddistrict, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataDetailHub _metadataDetailHub = new MetaDataDetailHub();
            Convertion common = new Convertion();
            ReturnDetailHub _returnDetailHub = new ReturnDetailHub();
            
            List<DetailHubFail> listDetailHubFail = null;
            DetailHubFail oDetailHubFail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_DETAIL_HUB.Detail_Item_HUB_Fail", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_IsStartDistrict", OracleDbType.Int32).Value = StartDistrict;
                    myCommand.Parameters.Add("v_IsEndtDistrict", OracleDbType.Int32).Value = enddistrict;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;                  
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDetailHubFail = new List<DetailHubFail>();
                        while (dr.Read())
                        {
                            oDetailHubFail = new DetailHubFail();
                            oDetailHubFail.STT = a++;
                            oDetailHubFail.Itemcode = dr["ITEMCODE"].ToString();
                            oDetailHubFail.Servicetypechar = dr["SERVICETYPECHAR"].ToString();
                            oDetailHubFail.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetailHubFail.StartPostCodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetailHubFail.StartDistrict = dr["STARTDISTRICT"].ToString();
                            oDetailHubFail.StartDistrictName = dr["STARTDISTRICTNAME"].ToString();
                            oDetailHubFail.StartProvince = dr["STARTPROVINCE"].ToString();
                            oDetailHubFail.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oDetailHubFail.A1statusdatetime = dr["A1STATUSDATETIME"].ToString();
                            oDetailHubFail.EndPosCode = dr["ENDPOSCODE"].ToString();
                            oDetailHubFail.EndPosCodeName = dr["ENDPOSCODENAME"].ToString();
                            oDetailHubFail.EndDistrict = dr["ENDDISTRICT"].ToString();
                            oDetailHubFail.EndDistrictName = dr["ENDDISTRICTNAME"].ToString();
                            oDetailHubFail.EndProvince = dr["ENDPROVINCE"].ToString();
                            oDetailHubFail.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oDetailHubFail.A2statusdatetime = dr["A2STATUSDATETIME"].ToString();
                            oDetailHubFail.Timeinterval = dr["TIMEINTERVAL"].ToString();
                            oDetailHubFail.J = dr["J"].ToString();
                            oDetailHubFail.IsFailstatus = dr["ISFAILSTATUS"].ToString();
                            listDetailHubFail.Add(oDetailHubFail);
                        }
                        _returnDetailHub.Code = "00";
                        _returnDetailHub.Message = "Lấy dữ liệu thành công.";
                        _returnDetailHub.ListDetailHubFailReport = listDetailHubFail;
                    }
                    else
                    {
                        _returnDetailHub.Code = "01";
                        _returnDetailHub.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnDetailHub.Code = "99";
                _returnDetailHub.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnDetailHub;
        }
    }

}

