using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class Detail_TK_LD
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string Manv { get; set; }
        public string Postmanid { get; set; }
        public string PostmanName { get; set; }
        public string Total { get; set; }
        public string Reason_Code { get; set; }
        public string Reason_Name { get; set; }
    }
    public class Detail_TK_CT_LD
    {
        public int STT { get; set; }
        public string ItemCode { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostcodeName { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string POSTMANID { get; set; }
        public string PostmanName { get; set; }
        public string StatusDateTime { get; set; }

        public string SenderPhone { get; set; }
        public string ReceiverPhone { get; set; }

        public string Reason_Code { get; set; }
        public string Reason_Name { get; set; }
    }

    public class ResponseDetail_TK_LD
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<Detail_TK_LD> listDetail_TK_LDReport;
    }

    public class ResponseDetail_TK_CT_LD
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<Detail_TK_CT_LD> Data;
    }
}