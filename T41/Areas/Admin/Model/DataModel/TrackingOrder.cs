using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Theo Dõi Đơn Hàng
    public class PARAMETER_TrackingOrder
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string customer { get; set; }
    }


    //Dữ liệu lấy ra của phần Header Theo Dõi Đơn Hàng
    public class HeaderTrackingOrderDetail
    {

        public String CustomerCode { get; set; }
        public String CustomerName { get; set; }
        public String TotalItem { get; set; }
        public String TotalSuccess { get; set; }
        public String TotalCharge_E1 { get; set; }
        public String TotalAmount { get; set; }
        

    }

    //Dữ liệu lấy ra của phần List Theo Dõi Đơn Hàng
    public class TrackingOrderDetail
    {

        public String CustomerCode { get; set; }
        public String ItemCode { get; set; }
        public String ItemCodePartner { get; set; }
        public String SenderDate { get; set; }
        public String ReceivePhone { get; set; }
        public String ReceiveAddress { get; set; }
        public String ToProvince { get; set; }
        public String Weight { get; set; }
        public String Charge_E1 { get; set; }
        public String TotalAmount { get; set; }
        public String DeliveryName { get; set; }
        public String DeliveryDate { get; set; }
        public String DeliveryTime { get; set; }
        public String State { get; set; }
        public String Note { get; set; }


    }


    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnTrackingOrder
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        public HeaderTrackingOrderDetail HeaderTrackingOrderReport { get; set; }
        public List<HeaderTrackingOrderDetail> ListHeaderTrackingOrderReport;

        public TrackingOrderDetail TrackingOrderReport { get; set; }
        public List<TrackingOrderDetail> ListTrackingOrderReport;

        public MetaData MetaData { get; set; }


    }

    
}