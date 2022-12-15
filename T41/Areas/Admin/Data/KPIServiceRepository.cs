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
    public class KPIServiceRepository
    {

        #region GETLISTPROVINCE
        //Lấy mã tuyến phát dưới DB Procedure Detail_Province_Ems
        public string GetProvince()
        {
            List<GetStartProvince> listProvince = null;
            GetStartProvince oGetProvince = null;
            string LISTProvince = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "kpi_detail_delivery_Service.Detail_Province_Ems";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        // LISTTINH += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTProvince += "<option value='" + dr["MA_TINH"].ToString() + "'>" + dr["MA_TINH"].ToString() + '-' + dr["TEN_TINH"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                LISTProvince = null;
            }

            return LISTProvince;
        }

        #endregion
        //Phần TONG HOP
        #region KPISERVICE_DETAIL          
        public ReturnKPIService KPIService_DETAIL(int StartProvince, int EndProvince, int isService, int isCod, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPIService _metadatakpiService = new MetaDataKPIService();
            Convertion common = new Convertion();
            ReturnKPIService _returnkpiService = new ReturnKPIService();
            _metadatakpiService.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            _returnkpiService.MetaDataKPIService = _metadatakpiService;
            List<KPIServiceDetail> listKPIServiceDetail = null;
            KPIServiceDetail oProvinceDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("kpi_detail_delivery_Service.Detail_Total_Service", Helper.OraDCOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                  
                    myCommand.Parameters.Add("v_StartProvice", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Int32).Value = isService;
                    myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = isCod;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIServiceDetail = new List<KPIServiceDetail>();
                        while (dr.Read())
                        {
                            oProvinceDetail = new KPIServiceDetail();
                            oProvinceDetail.STT = a++;
                            oProvinceDetail.StartProvince = dr["STARTPROVINCE"].ToString();
                            oProvinceDetail.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oProvinceDetail.EndProvince = dr["ENDPROVINCE"].ToString();
                            oProvinceDetail.EndProvinceName = dr["EndProvinceName"].ToString();
                            oProvinceDetail.Total = dr["TOTAL"].ToString();
                            oProvinceDetail.J0 = dr["J0"].ToString();
                            oProvinceDetail.TLJ0 = dr["TLJ0"].ToString();
                            oProvinceDetail.J05 = dr["J05"].ToString();
                            oProvinceDetail.TLJ05 = dr["TLJ05"].ToString();
                            oProvinceDetail.J1 = dr["J1"].ToString();
                            oProvinceDetail.TLJ1 = dr["TLJ1"].ToString();
                            oProvinceDetail.J15 = dr["J15"].ToString();
                            oProvinceDetail.TLJ15 = dr["TLJ15"].ToString();
                            oProvinceDetail.J2 = dr["J2"].ToString();
                            oProvinceDetail.TLJ2 = dr["TLJ2"].ToString();
                            oProvinceDetail.J25 = dr["J25"].ToString();
                            oProvinceDetail.TLJ25 = dr["TLJ25"].ToString();
                            oProvinceDetail.J3 = dr["J3"].ToString();
                            oProvinceDetail.TLJ3 = dr["TLJ3"].ToString();
                            oProvinceDetail.J35 = dr["J35"].ToString();
                            oProvinceDetail.TLJ35 = dr["TLJ35"].ToString();
                            oProvinceDetail.J4 = dr["J4"].ToString();
                            oProvinceDetail.TLJ4 = dr["TLJ4"].ToString();
                            oProvinceDetail.J5 = dr["J5"].ToString();
                            oProvinceDetail.TLJ5 = dr["TLJ5"].ToString();                          
                            listKPIServiceDetail.Add(oProvinceDetail);
                        }
                        _returnkpiService.Code = "00";
                        _returnkpiService.Message = "Lấy dữ liệu thành công.";
                        _returnkpiService.ListKPIServiceReport = listKPIServiceDetail;
                    }
                    else
                    {
                        _returnkpiService.Code = "01";
                        _returnkpiService.Message = "Không có dữ liệu";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiService.Code = "99";
                _returnkpiService.Message = "Lỗi xử lý dữ liệu";
               
            }
            return _returnkpiService;
        }
        #endregion

        //Phần CHI TIET ITEM CỦA MỘT TỈNH
        # region KPISERVICE_ITEM_DETAIL
        public ReturnKPIServiceItem KPIServiceItem_DETAIL(int StartProvince, int EndProvince, int isService, int isCod, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            MetaDataKPIService _metadatakpiService = new MetaDataKPIService();
            Convertion common = new Convertion();
            ReturnKPIServiceItem _returnkpiServiceItem = new ReturnKPIServiceItem();
            _metadatakpiService.from_to_date = "Từ ngày " + common.Convert_Date(StartDate);
            _returnkpiServiceItem.MetaDataKPIService = _metadatakpiService;
            List<KPIServiceItemDetail> listKPIServiceItemDetail = null;
            KPIServiceItemDetail oProvinceItemDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_delivery_Service.Detail_Item_Ems_Service", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvice", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Int32).Value = isService;
                    myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = isCod;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPIServiceItemDetail = new List<KPIServiceItemDetail>();
                        while (dr.Read())
                        {
                            oProvinceItemDetail = new KPIServiceItemDetail();
                            oProvinceItemDetail.STT = a++;
                            oProvinceItemDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oProvinceItemDetail.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oProvinceItemDetail.StartDistrict = dr["STARTDISTRICT"].ToString();
                            oProvinceItemDetail.StartProvince = dr["STARTPROVINCE"].ToString();
                            oProvinceItemDetail.EndPostCode = dr["ENDPOSTCODE"].ToString();
                            oProvinceItemDetail.EndDistrict = dr["ENDDISTRICT"].ToString();
                            oProvinceItemDetail.EndProvince = dr["ENDPROVINCE"].ToString();
                            oProvinceItemDetail.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            oProvinceItemDetail.IsCod = dr["ISCOD"].ToString();
                            oProvinceItemDetail.A1StatusDateTime = dr["A1STATUSDATETIME"].ToString();
                            oProvinceItemDetail.A2StatusDateTime = dr["A2STATUSDATETIME"].ToString();
                            oProvinceItemDetail.A3StatusDateTime = dr["A3STATUSDATETIME"].ToString();
                            oProvinceItemDetail.A4StatusDateTime = dr["A4STATUSDATETIME"].ToString();
                            oProvinceItemDetail.A5StatusDateTime = dr["A5STATUSDATETIME"].ToString();
                            oProvinceItemDetail.A6StatusDateTime = dr["A6STATUSDATETIME"].ToString();
                            oProvinceItemDetail.A7StatusDateTime = dr["A7STATUSDATETIME"].ToString();
                            oProvinceItemDetail.J = dr["J"].ToString();

                            listKPIServiceItemDetail.Add(oProvinceItemDetail);
                        }
                        _returnkpiServiceItem.Code = "00";
                        _returnkpiServiceItem.Message = "Lấy dữ liệu thành công.";
                        _returnkpiServiceItem.ListKPIServiceItemReport = listKPIServiceItemDetail;
                    }
                    else
                    {
                        _returnkpiServiceItem.Code = "01";
                        _returnkpiServiceItem.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnkpiServiceItem.Code = "99";
                _returnkpiServiceItem.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnkpiServiceItem;
        }
        # endregion
    }

}

