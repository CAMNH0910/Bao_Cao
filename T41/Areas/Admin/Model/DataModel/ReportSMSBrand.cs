using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Theo Dõi Đơn Hàng
    public class PARAMETER_ReportSMSBrand
    {
        public string fromdate { get; set; }
        public string todate { get; set; }

    }


    public class SMSBrandDetail
    {
        public int STT { get; set; }
        public string BrandName { get; set; }
        public string Network { get; set; }
        public string Status { get; set; }
        public string EMSPro1 { get; set; }
        public string EMSTest1 { get; set; }
        public string Total1 { get; set; }
        public string EMSPro2 { get; set; }
        public string EMSTest2 { get; set; }
        public string Total2 { get; set; }
    }

    public class ReturnSMSBrandDetail
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


        public SMSBrandDetail SMSBrandDetailReport { get; set; }
        public List<SMSBrandDetail> ListSMSBrandDetailReport;

        public MetaData MetaData { get; set; }

    }

}