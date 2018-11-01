using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_ExpressRoad
    {
        public int zone { get; set; }

    }
    
    //Dữ liệu lấy ra của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class ExpressRoadDetail
    {
        public int ID { get; set; }
        public String AMND_DATE { get; set; }
        public String READ { get; set; }
        public String USER_NAME { get; set; }
        public String CONTENT { get; set; }

        //CONVERT JSON CONTENT --> OBJECT
        public string E_CODE { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string STATUS { get; set; }
        public string NOTE { get; set; }
        public string CITY { get; set; }
        public string WEIGHT { get; set; }
        public string COLLECT { get; set; }
        public DateTime DELIVERY_DATE { get; set; }
        public string POST_CODE { get; set; }

    }



    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnExpressRoad
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

        public ExpressRoadDetail ExpressRoadReport { get; set; }
        public List<ExpressRoadDetail> ListExpressRoadReport;
        public List<ListTracking> ListTracking { get; set; }

    }

    public class ListTracking
    {
        public string ORDER_CODE { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PRODUCT_DESCRIPTION { get; set; }
        public int PRODUCT_QUANTITY { get; set; }
        public int PRODUCT_VALUE { get; set; }
        public int STORE_ID { get; set; }
        public int TOTAL_AMOUNT { get; set; }
        public int COD { get; set; }
        public int WEIGHT { get; set; }
        public string TO_COUNTRY { get; set; }
        public string RECEIVER_NAME { get; set; }
        public string RECEIVER_ADDRESS { get; set; }
        public string RECEIVER_PHONE { get; set; }
        public string RECEIVER_PROVINCE_ID { get; set; }
        public string RECEIVER_DISTRICT_ID { get; set; }
        public int RECEIVER_WARD_ID { get; set; }
        public object SENDER_CODE { get; set; }
        public string SENDER_NAME { get; set; }
        public string SENDER_ADDRESS { get; set; }
        public string SENDER_PHONE { get; set; }
        public string SENDER_PROVINCE_ID { get; set; }
        public string SENDER_DISTRICT_ID { get; set; }
        public string STATUS { get; set; }
        public int SERVICE_TYPE { get; set; }
    }

    public class RootObject
    {
        public List<ListTracking> ListTracking { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class EMSCODE
    {
        public string E_CODE { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string STATUS { get; set; }
        public string NOTE { get; set; }
        public string CITY { get; set; }
        public string WEIGHT { get; set; }
        public string COLLECT { get; set; }
        public DateTime DELIVERY_DATE { get; set; }
        public string POST_CODE { get; set; }
    }

   




}