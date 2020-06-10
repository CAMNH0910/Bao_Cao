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
    public class TrackingOrderRepository
    {

        //Phần chi tiết của bảng tổng hợp 
       
        #region TRACKING_ORDER_DETAIL          
        public ReturnTrackingOrder TRACKING_ORDER_DETAIL(int startdate, int enddate, string customercode, int type)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTrackingOrder _returnTrackingOrder = new ReturnTrackingOrder();
            List<TrackingOrderDetail> listTrackingOrderDetail = null;
            TrackingOrderDetail oTrackingOrderDetailDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "management_customer.GetListItemCustomer";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add(new OracleParameter("v_Customer", OracleDbType.Varchar2)).Value = customercode;
                    cm.Parameters.Add(new OracleParameter("v_Startdate", OracleDbType.Int32)).Value = startdate;
                    cm.Parameters.Add(new OracleParameter("v_Enddate", OracleDbType.Int32)).Value = enddate;
                    cm.Parameters.Add(new OracleParameter("v_type", OracleDbType.Int32)).Value = type;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            listTrackingOrderDetail = new List<TrackingOrderDetail>();
                            while (dr.Read())
                            {
                                oTrackingOrderDetailDetail = new TrackingOrderDetail();
                                oTrackingOrderDetailDetail.STT = a++;
                                oTrackingOrderDetailDetail.CustomerCode = dr["CUSTOMERCODE"].ToString();
                                oTrackingOrderDetailDetail.ItemCode = dr["ITEMCODE"].ToString();
                                oTrackingOrderDetailDetail.ItemCodePartner = dr["ITEMCODEPARTNER"].ToString();
                                oTrackingOrderDetailDetail.SenderDate = dr["SENDERDATE"].ToString();
                                oTrackingOrderDetailDetail.ReceivePhone = dr["RECEIVEPHONE"].ToString();
                                oTrackingOrderDetailDetail.ReceiveAddress = dr["RECEIVEADDRESS"].ToString();
                                oTrackingOrderDetailDetail.ToProvince = dr["TOPROVINCE"].ToString();
                                oTrackingOrderDetailDetail.Weight = dr["WEIGHT"].ToString();
                                oTrackingOrderDetailDetail.Charge_E1 = dr["CHARGE_E1"].ToString();
                                oTrackingOrderDetailDetail.TotalAmount = dr["TOTALAMOUNT"].ToString();
                                oTrackingOrderDetailDetail.DeliveryName = dr["DELIVERYNAME"].ToString();
                                oTrackingOrderDetailDetail.DeliveryDate = dr["DELIVERYDATE"].ToString();
                                oTrackingOrderDetailDetail.DeliveryTime = dr["DELIVERYTIME"].ToString();
                                oTrackingOrderDetailDetail.State = dr["STATE"].ToString();
                                oTrackingOrderDetailDetail.Note = dr["NOTE"].ToString();
                                listTrackingOrderDetail.Add(oTrackingOrderDetailDetail);

                            }
                            _returnTrackingOrder.Code = "00";
                            _returnTrackingOrder.Message = "Lấy dữ liệu thành công.";
                            _returnTrackingOrder.ListTrackingOrderReport = listTrackingOrderDetail;
                        }
                        else
                        {
                            _returnTrackingOrder.Code = "01";
                            _returnTrackingOrder.Message = "Không có dữ liệu";

                        }
                    }    
                    


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "TrackingOrderRepository.TRACKING_ORDER_DETAIL" + ex.Message);
                _returnTrackingOrder.Code = "99";
                _returnTrackingOrder.Message = "Lỗi xử lý dữ liệu";
                
            }
            return _returnTrackingOrder;
        }



        #endregion

        //Phần Header của bảng tổng hợp 

        #region HEADER_TRACKING_ORDER_DETAIL          
        public ReturnTrackingOrder HEADER_TRACKING_ORDER_DETAIL(string startdate, string enddate, string customercode)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTrackingOrder _returnTrackingOrder = new ReturnTrackingOrder();
            List<HeaderTrackingOrderDetail> listHeaderTrackingOrderDetail = null;
            HeaderTrackingOrderDetail oHeaderTrackingOrderDetailDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("management_customer.GetHeaderCustomer", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_Customer", OracleDbType.Varchar2).Value = customercode;
                    myCommand.Parameters.Add("v_Startdate", OracleDbType.Int32).Value = common.DateToInt(startdate);
                    myCommand.Parameters.Add("v_Enddate", OracleDbType.Int32).Value = common.DateToInt(enddate);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();
                    
                    if (dr.HasRows)
                    {
                        listHeaderTrackingOrderDetail = new List<HeaderTrackingOrderDetail>();
                        while (dr.Read())
                        {
                            oHeaderTrackingOrderDetailDetail = new HeaderTrackingOrderDetail();
                            oHeaderTrackingOrderDetailDetail.CustomerCode = dr["CUSTOMERCODE"].ToString();
                            oHeaderTrackingOrderDetailDetail.CustomerName = dr["CUSTOMERNAME"].ToString();
                            oHeaderTrackingOrderDetailDetail.TotalItem = dr["TOTALITEM"].ToString();
                            oHeaderTrackingOrderDetailDetail.TotalSuccess = dr["TOTALSUCCESS"].ToString();
                            oHeaderTrackingOrderDetailDetail.TotalCharge_E1 = dr["TOTALCHARGE_E1"].ToString();
                            oHeaderTrackingOrderDetailDetail.TotalAmount = dr["TOTALAMOUNT"].ToString();
                            listHeaderTrackingOrderDetail.Add(oHeaderTrackingOrderDetailDetail);

                        }
                        _returnTrackingOrder.Code = "00";
                        _returnTrackingOrder.Message = "Lấy dữ liệu thành công.";
                        _returnTrackingOrder.ListHeaderTrackingOrderReport = listHeaderTrackingOrderDetail;
                    }
                    else
                    {
                        
                        _returnTrackingOrder.Code = "01";
                        _returnTrackingOrder.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "TrackingOrderRepository.HEADER_TRACKING_ORDER_DETAIL" + ex.Message);
                _returnTrackingOrder.Code = "99";
                _returnTrackingOrder.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnTrackingOrder;
        }



        #endregion
    }

}

