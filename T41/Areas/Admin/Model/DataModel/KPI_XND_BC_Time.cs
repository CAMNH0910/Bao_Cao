using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
     public class KPI_XND_BC_Time
    {
        public int STT { get; set; }
        public string ZONE { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string Total { get; set; }
        public string XeMay_HT { get; set; }
        public string OTo_HT { get; set; }
        public string XeMay_QT { get; set; }
        public string OTo_QT { get; set; }
        public string XeMay_VISA { get; set; }
        public string OTo_VISA { get; set; }
        public string XeMay_DG { get; set; }
        public string OTo_DG { get; set; }
        public string XeMay_COD { get; set; }
        public string OTo_COD { get; set; }
        public string XeMay_KCOD { get; set; }
        public string OTo_KCOD { get; set; }
        public string XeMay_DAILY { get; set; }
        public string OTo_DAILY { get; set; }
        public string XeMay_TTC { get; set; }
        public string OTo_TTC { get; set; }
        public string XeMay_TTB { get; set; }
        public string OTo_TTB { get; set; }
        public string XeMay_TTV { get; set; }
        public string OTo_TTV { get; set; }
        public string XeMay_HH { get; set; }
        public string OTo_HH { get; set; }
        public string XeMay_TL { get; set; }
        public string OTo_TL { get; set; }
    }
    public class ReturnKPI_XND_BC_Time
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }

        public KPI_XND_BC_Time KPI_XND_BC_Time { get; set; }
        public List<KPI_XND_BC_Time> ListKPI_XND_BC_Time;


    }
}

