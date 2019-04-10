using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Data
{
    public class DealFalse_Detail
    {
        public int STT { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string account_Name { get; set; }
        public string COUNTID { get; set; }
        
       
    }

    public class AccountFalse_Detail
    {
        public int STT { get; set; }
        public String REFERENT_ACCOUNT { get; set; }
        public String COUNTID { get; set; }


    }


    public class ReturnDealAndAccountFalse
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

        
        public DealFalse_Detail DealFalse_Report { get; set; }
        public List<DealFalse_Detail> ListDealFalse_Report;


        public AccountFalse_Detail AccountFalse_Report { get; set; }
        public List<AccountFalse_Detail> ListAccountFalse_Report;

    }

}