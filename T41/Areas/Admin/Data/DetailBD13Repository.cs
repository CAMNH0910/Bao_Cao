using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class DetailBD13Repository
    {
        //Phần Lấy Dữ Liệu Delivery PostCode
        #region GetALLDeliveryPostCode

        public IEnumerable<DeliveryPostCode> GetAllDeliveryPostCode()
        {
            List<DeliveryPostCode> listDeliveryPostCode = null;
            DeliveryPostCode oDeliveryPostCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "EMS_E1_BD13_DI.Detail_DELIVERY_POST_CODE";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("P_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listDeliveryPostCode = new List<DeliveryPostCode>();
                        while (dr.Read())
                        {
                            oDeliveryPostCode = new DeliveryPostCode();
                            oDeliveryPostCode.POST_CODE = int.Parse(dr["DELIVERY_POST_CODE"].ToString());
                            oDeliveryPostCode.POST_CODE_NAME = dr["POST_CODE_NAME"].ToString();
                            listDeliveryPostCode.Add(oDeliveryPostCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllDeliveryPostCode" + ex.Message);
                listDeliveryPostCode = null;
            }

            return listDeliveryPostCode;
        }
        #endregion


        //Phần Lấy Dữ Liệu GetDeliveryRouteCodeById
        #region GetDeliveryRouteCodeById
        public IEnumerable<DeliveryPostCode> GetDeliveryRouteCodeByDeliveryCode(int delivery_post_code)
        {
            List<DeliveryPostCode> listGetRouteCode = null;
            DeliveryPostCode oGetRouteCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "EMS_E1_BD13_DI.Detail_DELIVERY_ROUTE_CODE";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("P_DELIVERY_POST_CODE", OracleDbType.Int32)).Value = delivery_post_code;
                    cm.Parameters.Add("P_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetRouteCode = new List<DeliveryPostCode>();
                        while (dr.Read())
                        {
                            oGetRouteCode = new DeliveryPostCode();
                            oGetRouteCode.POST_CODE = int.Parse(dr["POST_CODE"].ToString());
                            oGetRouteCode.POST_CODE_NAME = dr["POST_CODE_NAME"].ToString();
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
        #endregion


        // Phần lấy dữ liệu từ bảng e1_bd13_di
        #region BD13_DI_DETAIL          
        public ReturnBD13 BD13_DI_DETAIL(int page_index, int page_size, int mabc_kt, int mabc, int ngay, int cakt, int chthu, int tuiso)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnBD13 _returnBD13 = new ReturnBD13();

            List<BD13_DI_Detail> listBD13Detail = null;
            BD13_DI_Detail oBD13Detail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("EMS_E1_BD13_DI.Detail_E1_BD13_DI", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_PAGE_INDEX", OracleDbType.Int32).Value = page_index;
                    myCommand.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = page_size;
                    myCommand.Parameters.Add("P_MABC_KT", OracleDbType.Int32).Value = mabc_kt;
                    myCommand.Parameters.Add("P_MABC", OracleDbType.Int32).Value = mabc;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = ngay;
                    myCommand.Parameters.Add("P_CAKT", OracleDbType.Int32).Value = cakt;
                    myCommand.Parameters.Add("P_CHTHU", OracleDbType.Int32).Value = chthu;
                    myCommand.Parameters.Add("P_TUISO", OracleDbType.Int32).Value = tuiso;
                    myCommand.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add(new OracleParameter("P_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listBD13Detail = new List<BD13_DI_Detail>();
                        while (dr.Read())
                        {
                            oBD13Detail = new BD13_DI_Detail();
                            oBD13Detail.MAE1 = dr["MAE1"].ToString();
                            oBD13Detail.MABCTRA = dr["MABCTRA"].ToString();
                            oBD13Detail.MABCNHAN = dr["MABCNHAN"].ToString();
                            oBD13Detail.KHOILUONG = dr["KHOILUONG"].ToString();
                            oBD13Detail.CUOCCS = dr["CUOCCS"].ToString();
                            oBD13Detail.CUOCDV = dr["CUOCDV"].ToString();
                            oBD13Detail.TRANGTHAI = dr["TRANGTHAI"].ToString();
                            oBD13Detail.DIACHI = dr["DIACHI"].ToString();
                            oBD13Detail.MABC = dr["MABC"].ToString();
                            oBD13Detail.CHTHU = dr["CHTHU"].ToString();
                            oBD13Detail.TUISO = dr["TUISO"].ToString();
                            oBD13Detail.NGAY = dr["NGAY"].ToString();
                            oBD13Detail.MABC_KT = dr["MABC_KT"].ToString();
                            oBD13Detail.DATE_LOG = dr["DATE_LOG"].ToString();
                            listBD13Detail.Add(oBD13Detail);

                        }
                        _returnBD13.Code = "00";
                        _returnBD13.Message = "Lấy dữ liệu thành công.";
                        _returnBD13.Total = Convert.ToInt32(myCommand.Parameters["P_TOTAL"].Value.ToString());
                        _returnBD13.ListBD13Report = listBD13Detail;
                    }
                    else
                    {
                        _returnBD13.Code = "01";
                        _returnBD13.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnBD13.Code = "99";
                _returnBD13.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnBD13;
        }



        #endregion


        

        
    }



}

