using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;


namespace T41.Areas.Admin.Data
{
    public class TotalDataCustomerRepository
    {
        #region GETPROVINCE
        //Lấy mã bưu cục phát dưới DB Procedure transfer_management_ems.GetProvince_Ems
        public IEnumerable<GETPROVINCE> GETPROVINCE()
        {
            List<GETPROVINCE> listGetProvinceCode = null;
            GETPROVINCE oGetProvinceCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "management_customer.GetProvince_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetProvinceCode = new List<GETPROVINCE>();
                        while (dr.Read())
                        {
                            oGetProvinceCode = new GETPROVINCE();
                            //oGetProvinceCode.PROVINCECODE = int.Parse(dr["PROVINCECODE"].ToString());
                            oGetProvinceCode.PROVINCECODE = dr["PROVINCECODE"].ToString();
                            oGetProvinceCode.PROVINCENAME = dr["PROVINCENAME"].ToString();
                            listGetProvinceCode.Add(oGetProvinceCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETPROVINCE" + ex.Message);
                listGetProvinceCode = null;
            }

            return listGetProvinceCode;
        }
        #endregion

        #region GetAllPOSCODE
        //Lấy mã bưu cục phát dưới DB Procedure Detail_DeliveryPosCode_Ems , phần GETALLPOSCODE này đã được get; set; trong data model Qualty Delivery
        public IEnumerable<GETPOSCODE_TOTALDATA> GetPOSCODE()
        {
            List<GETPOSCODE_TOTALDATA> listGetPosCode = null;
            GETPOSCODE_TOTALDATA oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "management_customer.GetPosCode_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<GETPOSCODE_TOTALDATA>();
                        while (dr.Read())
                        {
                            oGetPosCode = new GETPOSCODE_TOTALDATA();
                            //oGetPosCode.POSCODE = int.Parse(dr["POSCODE"].ToString());
                            oGetPosCode.POSTCODE = dr["POSTCODE"].ToString();
                            oGetPosCode.POSNAME = dr["POSNAME"].ToString();
                            listGetPosCode.Add(oGetPosCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETCRPOSCODE" + ex.Message);
                listGetPosCode = null;
            }

            return listGetPosCode;
        }
        public string GetallProvinceTra()
        {
            string LISTTINH = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format(" select MA_TINH,TEN_TINH from  Bccp_Province order by ma_tinh");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        LISTTINH += "<option value='" + 0 + "'>" + "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTTINH += "<option value='" + dr["MA_TINH"].ToString() + "'>" + dr["MA_TINH"].ToString() + '-' + dr["TEN_TINH"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // LogAPI.LogToFile(LogFileType.EXCEPTION, "InternationalDetailRepository.GETMANC" + ex.Message);
            }

            return LISTTINH;
        }
        public string GetallProvinceNhan()
        {
            string LISTTINH = "";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format(" select MA_TINH,TEN_TINH from  Bccp_Province order by ma_tinh");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        // LISTTINH += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTTINH += "<option value='" + dr["MA_TINH"].ToString() + "'>" + dr["MA_TINH"].ToString() + '-' + dr["TEN_TINH"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // LogAPI.LogToFile(LogFileType.EXCEPTION, "InternationalDetailRepository.GETMANC" + ex.Message);
            }

            return LISTTINH;
        }
        #endregion

        //Phần chi tiết của bảng tổng hợp 
        #region TOTAL_DATA_CUSTOMER_DETAIL          
        public ReturnTotalDataCustomer TOTAL_DATA_CUSTOMER_DETAIL(string customercode, int startdate, int enddate, int startprovince, int endprovince, int isservice)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTotalDataCustomer _returnTotalDataCustomer = new ReturnTotalDataCustomer();

            List<TotalDataCustomerDetail> listTotalDataCustomer = null;
            TotalDataCustomerDetail oTotalDataCustomerDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("kpi_detail_delivery_Customer.Detail_Total_CLTT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = startprovince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = endprovince;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Int32).Value = isservice;                    
                    myCommand.Parameters.Add("v_Startdate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_Enddate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_CustomerCode", OracleDbType.Varchar2).Value = customercode;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();

                    

                    if (dr.HasRows)
                    {
                        listTotalDataCustomer = new List<TotalDataCustomerDetail>();
                        while (dr.Read())
                        {
                            oTotalDataCustomerDetail = new TotalDataCustomerDetail();
                         
                            oTotalDataCustomerDetail.STARTPROVINCENAME = dr["STARTPROVINCENAME"].ToString();
                            oTotalDataCustomerDetail.ENDPROVINCENAME = dr["ENDPROVINCENAME"].ToString();
                            oTotalDataCustomerDetail.C17TOTAL = dr["C17TOTAL"].ToString();
                            oTotalDataCustomerDetail.PTC = dr["PTC"].ToString();
                            oTotalDataCustomerDetail.TLPTC = dr["TLPTC"].ToString();
                            oTotalDataCustomerDetail.CH = dr["CH"].ToString();
                            oTotalDataCustomerDetail.TLCH = dr["TLCH"].ToString();
                            oTotalDataCustomerDetail.TOTALKTT = dr["TOTALKTT"].ToString();
                            oTotalDataCustomerDetail.TLKTT =dr["TLKTT"].ToString();
                            
                            listTotalDataCustomer.Add(oTotalDataCustomerDetail);

                        }
                        _returnTotalDataCustomer.Code = "00";
                        _returnTotalDataCustomer.Message = "Lấy dữ liệu thành công.";
                        _returnTotalDataCustomer.ListTotalDataCustomerReport = listTotalDataCustomer;
                    }
                    else
                    {
                        _returnTotalDataCustomer.Code = "01";
                        _returnTotalDataCustomer.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnTotalDataCustomer.Code = "99";
                _returnTotalDataCustomer.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnTotalDataCustomer;
        }



        #endregion

        
    }

}

