using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class TKSLGD
    {
        public int STT { get; set; }
        public string ROLE { get; set; }
        public string POSCODE { get; set; }
        public string POSNAME { get; set; }
        public string SLTOTAL { get; set; }
        public string TLTOTAL { get; set; }
        public string SLTHUCONG { get; set; }
        public string TLTHUCONG { get; set; }
        public string SLEXCEL { get; set; }
        public string TLEXCEL { get; set; }
        public string SLONE { get; set; }
        public string TLONE { get; set; }
        public string SLVDDT { get; set; }
        public string TLVDDT { get; set; } 
        public string SLMCS { get; set; }
        public string TLMCS { get; set; }

    }
    public class ResponseTKSLGD
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<TKSLGD> listDetail_TKSLGDReport;
    }
}