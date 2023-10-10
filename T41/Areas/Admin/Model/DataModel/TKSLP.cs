using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class Detail_TKSLP
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string Manv { get; set; }
        public string Postmanid { get; set; }
        public string PostmanName { get; set; }
        public string ServiceType { get; set; }
        public string SLD { get; set; }

        public string SLPTC { get; set; }
        public string SLPTC72H { get; set; }
        public string SLDQD2KG { get; set; }
        public string KLDQD2KG { get; set; }
        public string SLDQDT2KG { get; set; }
        public string KLDQDT2KG { get; set; }
        public string SLDQD { get; set; }
        public string KLDQD { get; set; }
        public string SLKTC2KG { get; set; }
        public string KLKTC2KG { get; set; }
        public string SLKTCT2KG { get; set; }
        public string KLKTCT2KG { get; set; }
        public string SLKTC { get; set; }
        public string KLKTC { get; set; }
        public string SLCHUAPHAT { get; set; }
    }

    public class ResponseDetail_TKSLP
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<Detail_TKSLP> listDetail_TKSLPReport;
    }
}