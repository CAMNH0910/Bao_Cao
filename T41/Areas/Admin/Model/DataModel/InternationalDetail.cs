using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    
    //Phần lấy dữ liệu chi tiết
    public class INTERNATIONAL_DETAIL
    {
        public int STT { get; set; }
        
        public string MAE1 { get; set; }
        public string EDI_CODE { get; set; }
        public string D_OE { get; set; }
        public string EMD { get; set; }
        public string EMD_TRAN { get; set; }
        public string DELI { get; set; }
        public string EME { get; set; }
        public string EME_TRAN { get; set; }
         
        public string EDB { get; set; }
        public string EDB_TRAN { get; set; }
        public string EDC { get; set; }
        public string EDC_TRAN { get; set; }
        public string EMF { get; set; }
        public string EMF_TRAN { get; set; }

      
        public string EMH { get; set; }
        
        public string EMH_TRAN { get; set; }
        public string EMI { get; set; }
        public string EMI_TRAN { get; set; }
        

    }
    
    public class ReturnInternationalDetail
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
        
        //Detail
        public INTERNATIONAL_DETAIL International_Detail_Report { get; set; }
        public List<INTERNATIONAL_DETAIL> ListInternational_Detail_Report;

        

    }
}