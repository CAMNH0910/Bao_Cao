using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{


    public class GetPosCodeGD
    {
        public int PosCode { get; set; }

        public string PosName { get; set; }

    }

    public class GetPosCodePickup
    {
        public string PosCode { get; set; }

        public string PosName { get; set; }

    }

    public class GetRouteCodePickup
    {
        public string RouteCode { get; set; }

        public string RouteName { get; set; }

    }

    public class PARAMETERKPIPickup
    {
        public string Zone { get; set; }
        public string PosCode { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }

    }

    public class KPIPickupTotalExcel
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string Total { get; set; }
        public string TimeintervalUpTo6 { get; set; }
        public string TLTimeintervalUpTo6 { get; set; }
    }

        //Lấy chi tiết tổng hợp
    public class KPIPickupTotal
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string Total { get; set; }
        public string TimeintervalUpTo6 { get; set; }
        public string TLTimeintervalUpTo6 { get; set; }
        public string TimeintervalUpTo10 { get; set; }
        public string TLTimeintervalUpTo10 { get; set; }
        public string TimeintervalExceed10 { get; set; }
        public string TLTimeintervalExceed10 { get; set; }
        public string TimeintervalUpTo3GD { get; set; }
        public string TimeintervalUpTo3VC { get; set; }
    }

    public class KPIPickupSuccess
    {
        public int STT { get; set; }
        public string PO_NAME { get; set; }
        public string ROUTE_NAME { get; set; }
        public string TOTAL_QUANTITY { get; set; }
        public string COLLECT_SUCCESS_QUANTITY { get; set; }
        public string ARRIVED_WEIGHT { get; set; }
        public string COLLECT_SUCCESS_PERCENT { get; set; }
        public string COLLECT_NOT_SUCCESS_QUANTITY { get; set; }
        public string COLLECT_NOT_SUCCESS_QUANTITY_0 { get; set; }
        public string COLLECT_NOT_SUCCESS_QUANTITY_1 { get; set; }
        public string COLLECT_NOT_SUCCESS_QUANTITY_2 { get; set; }
        public string COLLECT_NOT_SUCCESS_QUANTITY_3 { get; set; }
        public string COLLECT_NOT_SUCCESS_QUANTITY_4 { get; set; }
        public string COLLECT_NOT_SUCCESS_QUANTITY_5 { get; set; }

    }

    public class KPIPickupSuccessDQD
    {
        public int STT { get; set; }
        public string PO_NAME { get; set; }
        public string ROUTE_NAME { get; set; }
        public string TOTAL_QUANTITY { get; set; }
        public string ARRIVED_QUANTITY { get; set; }
        public string ARRIVED_WEIGHT { get; set; }
        public string ARRIVED_PERCENT { get; set; }
        public string BD10_QUANTITY { get; set; }
        public string BD10_WEIGHT { get; set; }
        public string BD10_PERCENT { get; set; }
    }

    public class ReturnKPIPickup
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPIPickupTotal KPIPickupReport { get; set; }
        public List<KPIPickupTotal> ListKPIPickupReport;
        public MetaDataKPIPickup MetaDataKPIPickup { get; set; }

    }

    public class ReturnKPIPickupSuccess
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPIPickupSuccess KPIPickupSuccessReport { get; set; }
        public List<KPIPickupSuccess> ListKPIPickupSuccessReport;
    }

    public class ReturnKPIPickupSuccessDQD
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPIPickupSuccessDQD KPIPickupSuccessDQDReport { get; set; }
        public List<KPIPickupSuccessDQD> ListKPIPickupSuccessDQDReport;
    }

    public class ReturnDetailPickup
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        public string Value { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        public List<DetailPickup> listDetailPickup_Report { get; set; }

       // public List<DetailPickup> List_DetailPickup_Report

        public MetaData MetaData { get; set; }


    }

    public class DetailPickup
    {
        public int STT { get; set; }
        public string CREATED_DATE { get; set; }
        public string PO_PICKUP_CODE { get; set; }
        public string ORDER_CODE { get; set; }
        public string ROUTE_CODE { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string QUANTITY { get; set; }
        public string WEIGH { get; set; }
        public string GIO_DIEU_TIN { get; set; }
        public string REQUEST_PICKUP_TIME { get; set; }
        public string GIO_NHAN_TIN { get; set; }
        public string GIO_XAC_NHAN_TIN { get; set; }
        public string GIO_THU_GOM { get; set; }
        public string GIO_XAC_NHAN_DEN { get; set; }
        public string GIO_LAY_HANG_KTC { get; set; }
        public string EDIT_ARRIVED_QUANTITY { get; set; }
        public string EDIT_ARRIVED_WEIGHT { get; set; }
        public string COLLECT_REASON { get; set; }
        public string NGUOI_TAO { get; set; }
        public string NGUOI_DIEU { get; set; }
        public string NGUOI_GOM { get; set; }


    }

    public class KPIPickupItemExcel
    {
        public int STT { get; set; }
        public string ItemCode { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string EndProvince { get; set; }
        public string EndProvinceName { get; set; }
        public string A1StatusDate { get; set; }
        public string A1StatusTime { get; set; }
        public string A2StatusDate { get; set; }
        public string A2StatusTime { get; set; }
    }
    

        //Lấy ra chi tiết
    public class KPIPickupItem
    {
        public int STT { get; set; }
        public string ItemCode { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string EndProvince { get; set; }
        public string EndProvinceName { get; set; }
        public string A1StatusDate { get; set; }
        public string A1StatusTime { get; set; }
        public string PosCode { get; set; }
        public string PosName { get; set; }
        public string A2StatusDate { get; set; }
        public string A2StatusTime { get; set; }
        public string A3StatusDate { get; set; }
        public string A3StatusTime { get; set; }
        public string TimeintervalGD { get; set; }
        public string TimeintervalVC { get; set; }
        public string Timeinterval { get; set; }
    
    }
    public class ReturnKPIPickupItem
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPIPickupItem KPIPickupItemReport { get; set; }
        public List<KPIPickupItem> ListKPIPickupItemReport;
       
    }

    public class MetaDataKPIPickup
    {
        public string from_to_date { get; set; }

    }

    public class TotalKPIPickUpV2
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string Total { get; set; }
        public string Timeinterval { get; set; }
        public string TLTimeinterval { get; set; }
        public string TotalTC { get; set; }
        public string TLTotalTC { get; set; }
    }
    public class ReturnKPITotalPickUpV2
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<TotalKPIPickUpV2> ListTotalKPIPickUpV2 { get; set; }
    }
    public class DetailKPIPickUpV2
    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string MailRouteName { get; set; }
        public string A1StatusDate { get; set; }
        public string A1StatusTime { get; set; }
        public string A2StatusDate { get; set; }
        public string A2StatusTime { get; set; }
        public string TimeExpected { get; set; }
    }
    public class ReturnDetailPickUpV2
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<DetailKPIPickUpV2> ListDetailPickUpV2 { get; set; }
    }

}