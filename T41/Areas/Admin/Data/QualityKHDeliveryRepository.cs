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
    public class QualityKHDeliveryRepository
    {
        #region GetAllPOSCODE
        //Lấy mã bưu cục phát dưới DB Procedure Detail_DeliveryPosCode_Ems
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

        //Phần chi tiết của bảng tổng hợp sản lượng đi phát
        #region QUALITY_DETAIL          
        public ReturnQualityKH QUALITY_DELIVERY_DETAIL_PTC(int StartProvince,int EndProvince, int Isservice, int startdate, int enddate, string CustomerCode)
        {
            DataTable da = new DataTable();
            MetaDataKH _metadata1 = new MetaDataKH();
            Convertion common = new Convertion();
            ReturnQualityKH _returnQualityKH = new ReturnQualityKH();

            _metadata1.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityKH.MetaData1 = _metadata1;
            List<QualityDeliveryDetailKH> listQualityDeliveryKHDetail = null;
            QualityDeliveryDetailKH oQualityDeliveryDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("kpi_detail_delivery_Customer.Detail_PTC", Helper.OraDCOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                   
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Int32).Value = Isservice;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_CustomerCode", OracleDbType.NVarchar2).Value = CustomerCode;
                   
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityDeliveryKHDetail = new List<QualityDeliveryDetailKH>();
                        while (dr.Read())
                        {
                            oQualityDeliveryDetail = new QualityDeliveryDetailKH();
                            oQualityDeliveryDetail.STT = a++;
                            oQualityDeliveryDetail.StartProvince = dr["STARTPROVINCE"].ToString();                          
                            oQualityDeliveryDetail.EndProvince = dr["ENDPROVINCE"].ToString();
                            oQualityDeliveryDetail.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oQualityDeliveryDetail.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oQualityDeliveryDetail.C17total = Convert.ToInt32(dr["C17TOTAL"].ToString());
                            //oQualityDeliveryDetail.Total = Convert.ToInt32(dr["TOTAL"].ToString());

                            oQualityDeliveryDetail.J1 = Convert.ToInt32(dr["J1"].ToString());
                            oQualityDeliveryDetail.TLJ1 = Convert.ToDecimal(dr["TLJ1"].ToString());

                            oQualityDeliveryDetail.J2 = Convert.ToInt32(dr["J2"].ToString());
                            oQualityDeliveryDetail.TLJ2 = Convert.ToDecimal(dr["TLJ2"].ToString());

                            oQualityDeliveryDetail.J25 = Convert.ToInt32(dr["J25"].ToString());
                            oQualityDeliveryDetail.TLJ25 = Convert.ToDecimal(dr["TLJ25"].ToString());

                            oQualityDeliveryDetail.J3 = Convert.ToInt32(dr["J3"].ToString());
                            oQualityDeliveryDetail.TLJ3 = Convert.ToDecimal(dr["TLJ3"].ToString());

                            oQualityDeliveryDetail.J35 = Convert.ToInt32(dr["J35"].ToString());
                            oQualityDeliveryDetail.TLJ35 = Convert.ToDecimal(dr["TLJ35"].ToString());

                            oQualityDeliveryDetail.J4 = Convert.ToInt32(dr["J4"].ToString());
                            oQualityDeliveryDetail.TLJ4 = Convert.ToDecimal(dr["TLJ4"].ToString());

                            oQualityDeliveryDetail.J5 = Convert.ToInt32(dr["J5"].ToString());
                            oQualityDeliveryDetail.TLJ5 = Convert.ToDecimal(dr["TLJ5"].ToString());

                            oQualityDeliveryDetail.TotalKTT = Convert.ToInt32(dr["TotalKTT"].ToString());
                            oQualityDeliveryDetail.TLKTT = Convert.ToDecimal(dr["TLKTT"].ToString());

                            listQualityDeliveryKHDetail.Add(oQualityDeliveryDetail);
                            
                        }
                        _returnQualityKH.Code = "00";
                        _returnQualityKH.Message = "Lấy dữ liệu thành công.";
                        _returnQualityKH.ListQualityDeliveryKHReport = listQualityDeliveryKHDetail;
                    }
                    else
                    {
                        _returnQualityKH.Code = "01";
                        _returnQualityKH.Message = "Không có dữ liệu";
                        
                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityKH.Code = "99";
                _returnQualityKH.Message = "Lỗi xử lý dữ liệu";
                _returnQualityKH.ListQualityDeliveryKHReport = null;
            }
            return _returnQualityKH;
        }

        public ReturnQualityKH QUALITY_DELIVERY_DETAIL_PLD(int StartProvince, int EndProvince, int Isservice, int startdate, int enddate, string CustomerCode)
        {
            DataTable da = new DataTable();
            MetaDataKH _metadata1 = new MetaDataKH();
            Convertion common = new Convertion();
            ReturnQualityKH _returnQualityKH = new ReturnQualityKH();

            _metadata1.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityKH.MetaData1 = _metadata1;
            List<QualityDeliveryDetailKH> listQualityDeliveryKHDetail = null;
            QualityDeliveryDetailKH oQualityDeliveryDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_delivery_Customer.Detail_PLD", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    myCommand.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Int32).Value = Isservice;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_CustomerCode", OracleDbType.NVarchar2).Value = CustomerCode;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityDeliveryKHDetail = new List<QualityDeliveryDetailKH>();
                        while (dr.Read())
                        {
                            oQualityDeliveryDetail = new QualityDeliveryDetailKH();
                            oQualityDeliveryDetail.STT = a++;
                            oQualityDeliveryDetail.StartProvince = dr["STARTPROVINCE"].ToString();
                            oQualityDeliveryDetail.EndProvince = dr["ENDPROVINCE"].ToString();
                            oQualityDeliveryDetail.StartProvinceName = dr["STARTPROVINCENAME"].ToString();
                            oQualityDeliveryDetail.EndProvinceName = dr["ENDPROVINCENAME"].ToString();
                            oQualityDeliveryDetail.C17total = Convert.ToInt32(dr["C17TOTAL"].ToString());
                            //oQualityDeliveryDetail.Total = Convert.ToInt32(dr["TOTAL"].ToString());

                            oQualityDeliveryDetail.J1 = Convert.ToInt32(dr["J1"].ToString());
                            oQualityDeliveryDetail.TLJ1 = Convert.ToDecimal(dr["TLJ1"].ToString());

                            oQualityDeliveryDetail.J2 = Convert.ToInt32(dr["J2"].ToString());
                            oQualityDeliveryDetail.TLJ2 = Convert.ToDecimal(dr["TLJ2"].ToString());

                            oQualityDeliveryDetail.J25 = Convert.ToInt32(dr["J25"].ToString());
                            oQualityDeliveryDetail.TLJ25 = Convert.ToDecimal(dr["TLJ25"].ToString());

                            oQualityDeliveryDetail.J3 = Convert.ToInt32(dr["J3"].ToString());
                            oQualityDeliveryDetail.TLJ3 = Convert.ToDecimal(dr["TLJ3"].ToString());

                            oQualityDeliveryDetail.J35 = Convert.ToInt32(dr["J35"].ToString());
                            oQualityDeliveryDetail.TLJ35 = Convert.ToDecimal(dr["TLJ35"].ToString());

                            oQualityDeliveryDetail.J4 = Convert.ToInt32(dr["J4"].ToString());
                            oQualityDeliveryDetail.TLJ4 = Convert.ToDecimal(dr["TLJ4"].ToString());

                            oQualityDeliveryDetail.J5 = Convert.ToInt32(dr["J5"].ToString());
                            oQualityDeliveryDetail.TLJ5 = Convert.ToDecimal(dr["TLJ5"].ToString());

                            oQualityDeliveryDetail.TotalKTT = Convert.ToInt32(dr["TotalKTT"].ToString());
                            oQualityDeliveryDetail.TLKTT = Convert.ToDecimal(dr["TLKTT"].ToString());

                            listQualityDeliveryKHDetail.Add(oQualityDeliveryDetail);

                        }
                        _returnQualityKH.Code = "00";
                        _returnQualityKH.Message = "Lấy dữ liệu thành công.";
                        _returnQualityKH.ListQualityDeliveryKHReport = listQualityDeliveryKHDetail;
                    }
                    else
                    {
                        _returnQualityKH.Code = "01";
                        _returnQualityKH.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityKH.Code = "99";
                _returnQualityKH.Message = "Lỗi xử lý dữ liệu";
                _returnQualityKH.ListQualityDeliveryKHReport = null;
            }
            return _returnQualityKH;
        }

        #endregion

        //Phần chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
        #region QUALITY_DETAIL DETAIL      
        public ReturnQualityKH Quality_Delivery_Item_Detail_PTC(int StartProvince, int EndProvince, int Isservice, int startdate, int enddate, string CustomerCode)
        {
            DataTable da = new DataTable();
            MetaDataKH _metadata1 = new MetaDataKH();
            Convertion common = new Convertion();
            ReturnQualityKH _returnQuality = new ReturnQualityKH();

            _metadata1.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQuality.MetaData1 = _metadata1;
            List<QualityDeliverySuccessNotItemKH> listQualityDeliverySuccessitemDetail = null;
            QualityDeliverySuccessNotItemKH oQualityDeliverySuccessitemDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "kpi_detail_delivery_Customer.Detail_Item_Ems_KTT_PTC";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    cmd.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    cmd.Parameters.Add("v_Isservice", OracleDbType.Int32).Value = Isservice;
                    cmd.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    cmd.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    cmd.Parameters.Add("v_CustomerCode", OracleDbType.NVarchar2).Value = CustomerCode;
                    cmd.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(cmd);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityDeliverySuccessitemDetail = new List<QualityDeliverySuccessNotItemKH>();
                        while (dr.Read())
                        {
                            oQualityDeliverySuccessitemDetail = new QualityDeliverySuccessNotItemKH();
                            oQualityDeliverySuccessitemDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oQualityDeliverySuccessitemDetail.CustomerCode = dr["CUSTOMERCODE"].ToString();
                            oQualityDeliverySuccessitemDetail.StartPostcode = Convert.ToInt32(dr["STARTPOSTCODE"].ToString());
                            oQualityDeliverySuccessitemDetail.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oQualityDeliverySuccessitemDetail.EndPostcode = Convert.ToInt32(dr["ENDPOSTCODE"].ToString());
                            oQualityDeliverySuccessitemDetail.EndPostcodeName = dr["ENDPOSTCODENAME"].ToString();
                            oQualityDeliverySuccessitemDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oQualityDeliverySuccessitemDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oQualityDeliverySuccessitemDetail.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            listQualityDeliverySuccessitemDetail.Add(oQualityDeliverySuccessitemDetail);
                        }
                        _returnQuality.Code = "00";
                        _returnQuality.Message = "Lấy dữ liệu thành công.";
                        _returnQuality.ListQualityDeliverySuccessNotItemReport = listQualityDeliverySuccessitemDetail;
                    }
                    else
                    {
                        _returnQuality.Code = "01";
                        _returnQuality.Message = "Không có dữ liệu";
                        _returnQuality.Total = 0;
                        _returnQuality.ListQualityDeliverySuccessNotItemReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                _returnQuality.Code = "99";
                _returnQuality.Message = "Lỗi xử lý dữ liệu";
                _returnQuality.Total = 0;
                _returnQuality.ListQualityDeliverySuccessNotItemReport = null;
            }
            return _returnQuality;
        }
        public ReturnQualityKH Quality_Delivery_Item_Detail_PLD(int StartProvince, int EndProvince, int Isservice, int startdate, int enddate, string CustomerCode)
        {
            DataTable da = new DataTable();
            MetaDataKH _metadata1 = new MetaDataKH();
            Convertion common = new Convertion();
            ReturnQualityKH _returnQuality = new ReturnQualityKH();

            _metadata1.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQuality.MetaData1 = _metadata1;
            List<QualityDeliverySuccessNotItemKH> listQualityDeliverySuccessitemDetail = null;
            QualityDeliverySuccessNotItemKH oQualityDeliverySuccessitemDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "kpi_detail_delivery_Customer.Detail_Item_Ems_KTT_PLD";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_StartProvince", OracleDbType.Int32).Value = StartProvince;
                    cmd.Parameters.Add("v_EndProvince", OracleDbType.Int32).Value = EndProvince;
                    cmd.Parameters.Add("v_Isservice", OracleDbType.Int32).Value = Isservice;
                    cmd.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    cmd.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    cmd.Parameters.Add("v_CustomerCode", OracleDbType.NVarchar2).Value = CustomerCode;                   
                    cmd.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(cmd);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityDeliverySuccessitemDetail = new List<QualityDeliverySuccessNotItemKH>();
                        while (dr.Read())
                        {
                            oQualityDeliverySuccessitemDetail = new QualityDeliverySuccessNotItemKH();
                            oQualityDeliverySuccessitemDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oQualityDeliverySuccessitemDetail.CustomerCode = dr["CUSTOMERCODE"].ToString();
                            oQualityDeliverySuccessitemDetail.StartPostcode = Convert.ToInt32(dr["STARTPOSTCODE"].ToString());
                            oQualityDeliverySuccessitemDetail.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oQualityDeliverySuccessitemDetail.EndPostcode = Convert.ToInt32(dr["ENDPOSTCODE"].ToString());
                            oQualityDeliverySuccessitemDetail.EndPostcodeName = dr["ENDPOSTCODENAME"].ToString();
                            oQualityDeliverySuccessitemDetail.StatusDate = dr["STATUSDATE"].ToString();
                            oQualityDeliverySuccessitemDetail.StatusTime = dr["STATUSTIME"].ToString();
                            oQualityDeliverySuccessitemDetail.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            listQualityDeliverySuccessitemDetail.Add(oQualityDeliverySuccessitemDetail);
                        }
                        _returnQuality.Code = "00";
                        _returnQuality.Message = "Lấy dữ liệu thành công.";
                        _returnQuality.ListQualityDeliverySuccessNotItemReport = listQualityDeliverySuccessitemDetail;
                    }
                    else
                    {
                        _returnQuality.Code = "01";
                        _returnQuality.Message = "Không có dữ liệu";
                        _returnQuality.Total = 0;
                        _returnQuality.ListQualityDeliverySuccessNotItemReport = null;
                    }


                }
            }
            catch (Exception ex)
            {
                _returnQuality.Code = "99";
                _returnQuality.Message = "Lỗi xử lý dữ liệu";
                _returnQuality.Total = 0;
                _returnQuality.ListQualityDeliverySuccessNotItemReport = null;
            }
            return _returnQuality;
        }
        #endregion


    }

}

