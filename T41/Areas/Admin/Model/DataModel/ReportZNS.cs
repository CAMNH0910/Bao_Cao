using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Theo Dõi Đơn Hàng
    public class PARAMETER_ReportZNS
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string MAKH { get; set; }
        public int MADONVI { get; set; }
        public int LOAITIN  { get; set; }
        public int TRANGTHAI { get; set; }
    }

    public class ZNSTotal
    {
        public int STT { get; set; }
        public string AcceptDate { get; set; }
        public string TotalSend { get; set; }
        public string SuccessMessagers { get; set; }
        public string FailMessagers { get; set; }
        public string ServiceFeeNotVAT { get; set; }
        public string Note { get; set; }
    }


    public class ZNSDetail
    {
        public int STT { get; set; }
        public string AcceptDate { get; set; }
        public string E1Code { get; set; }
        public string PosCode { get; set; }
        public string CustomerCode { get; set; }
        public string Phone { get; set; }
        public string StatusDate { get; set; }
        public string StatusDelivery { get; set; }
        public string SendDate { get; set; }
        public string TypeMessage { get; set; }
        public string TotalSend { get; set; }
        public string SuccessMessagers { get; set; }
        public string FailMessagers { get; set; }
        public string ServiceFeeNotVAT { get; set; }
        public string Note { get; set; }
    }

    public class ReturnZNS
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public ZNSDetail ZNSDetail { get; set; }
        public List<ZNSDetail> ListZNSDetailReport;
        public ZNSTotal ZNSTotal { get; set; }
        public List<ZNSTotal> ListZNSTotalReport;
        public MetaData MetaData { get; set; }

    }

}