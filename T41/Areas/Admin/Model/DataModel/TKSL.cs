using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class Detail_TKSL
    {
        public string StartPostCode { get; set; }
        public string StartPostName { get; set; }
        public string EndPostCode { get; set; }
        public string EndPostName { get; set; }
        public string Tong_SL { get; set; }
        public string TS_SMP { get; set; }
        public string TL_SMP { get; set; }
        public string TS_NOTSMP { get; set; }
        public string TL_NOTSMP { get; set; }
    }

    public class Detail_CT_TKSL
    {
        public string ItemCode { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostName { get; set; }
        public string EndPostCode { get; set; }
        public string EndPostName { get; set; }
        public string TG { get; set; }
    }

    public class ResponseDetail_TKSL
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<Detail_TKSL> listDetail_TKSLReport;
    }

    public class ResponseDetail_CT_TKSL
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<Detail_CT_TKSL> listDetail_CT_TKSLReport;
    }
}