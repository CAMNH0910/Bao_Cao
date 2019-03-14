using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Data
{
    public class ChannelManagement_BP_Detail
    {
        public int STT { get; set; }
        public String AMND_DATE { get; set; }
        
        public String CONTACT_NAME { get; set; }
        public String CONTACT_ADDRESS { get; set; }
        public String CONTACT_DISTRICT { get; set; }
        public String CONTACT_PROVINCE { get; set; }
        public String CONTACT_PHONE_WORK { get; set; }
        public String CUSTOMER_CODE { get; set; }
        public String GENERAL_EMAIL { get; set; }
        
       
    }

    public class ReturnChannelManagement
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

        
        public ChannelManagement_BP_Detail ChannelManagement_BP_Report { get; set; }
        public List<ChannelManagement_BP_Detail> ListChannelManagement_BP_Report;
        
    }

}