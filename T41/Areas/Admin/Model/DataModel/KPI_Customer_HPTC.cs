using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_Customer_HPTC
    {

        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string StartPostCodeName { get; set; }
        public string STARTPROVINCEName { get; set; }
        public string status { get; set; }
        public string TRACEDATE { get; set; }
    }

    public class ReturnKPI_Customer_HPTC
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }

        public KPI_Customer_HPTC KPI_Customer_HPTCReport { get; set; }
        public List<KPI_Customer_HPTC> ListKPI_Customer_HPTCReport;


    }
}