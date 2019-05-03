using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{

    public class CallHistory
    {
        public int ID { get; set; }
        public string PO_CODE { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string FULL_NAME { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string LADING_CODE { get; set; }
        public string CALLER { get; set; }
        public string START_TIME { get; set; }
        public string TALK_TIME { get; set; }
        public string PATH { get; set; }

    }

    //Phần Return api cho trường hợp thiếu User and PW
    public class ReturnCallHistory_Error
    {
        public string Code { get; set; }

        public string Message { get; set; }



    }

    //Phần Return api cho trường hợp đúng
    public class ReturnCallHistory
    {
        public string Code { get; set; }

        public string Message { get; set; }


        public CallHistory CallHistoryReport { get; set; }
        public List<CallHistory> List_Call_History_Report;


    }
}