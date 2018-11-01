using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Dữ liệu lấy ra từ bảng Journey_log
    public class JourneyDetail
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
    public class ReturnJourney
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Total { get; set; }

        public JourneyDetail JourneyDetailReport { get; set; }
        public List<JourneyDetail> ListJourneyReport;

    }

    public class CONTENT
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