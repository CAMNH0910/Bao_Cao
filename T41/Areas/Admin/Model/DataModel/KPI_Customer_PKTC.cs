using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_Customer_PKTC

    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string StartPostCodeName { get; set; }
        public string STARTPROVINCEName { get; set; }
        public string ISSERVICE { get; set; }
        public string CAUSECODE { get; set; }
        public string DELIVERYDATE { get; set; }
    }

    public class ReturnKPI_Customer_PKTC
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }

        public KPI_Customer_PKTC KPI_Customer_PKTCReport { get; set; }
        public List<KPI_Customer_PKTC> ListKPI_Customer_PKTCReport;


    }
}