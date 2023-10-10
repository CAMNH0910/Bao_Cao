using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_SL_Du_Bao
    {
        public int STT { get; set; }
        public string Province { get; set; }
        public string ProvinceName { get; set; }
        public string TStui { get; set; }
        public string TStui_KTlai { get; set; }
        public string TStui_QG { get; set; }
        public string TStui_DaDen { get; set; }
        public string TStui_DaKT { get; set; }
        public string TSTui_QG_DaDen { get; set; }
        public string TStui_QG_DaGiao { get; set; }
        public string TStui_DiDuong { get; set; }
        public string TSbp { get; set; }
        public string TSbp_KTlai { get; set; }
        public string TSbp_QG { get; set; }
        public string TSbp_DaDen { get; set; }
        public string TSbp_DaKT { get; set; }
        public string TSbp_QG_DaKT { get; set; }
        public string TSbp_QG_DaGiao { get; set; }
        public string TSbp_DiDuong { get; set; }
        public string Tkl { get; set; }
        public string Tkl_KTlai { get; set; }
        public string Tkl_QG { get; set; }
        public string Tkl_DaDen { get; set; }
        public string Tkl_DaKT { get; set; }
        public string Tkl_QG_DaDen { get; set; }
        public string Tkl_QG_DaGiao { get; set; }
        public string Tkl_DiDuong { get; set; }
        public string DATE_LOG { get; set; }
    }

    public class KPI_SL_Du_BaoTime
    {
        public String DATE_LOG { get; set; }

    }
        public class ReturnKPI_SL_Du_Bao
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

        public KPI_SL_Du_Bao KPI_SL_Du_Bao { get; set; }
        public List<KPI_SL_Du_Bao> ListKPI_SL_Du_Bao;

        public KPI_SL_Du_BaoTime KPI_SL_Du_BaoTime { get; set; }
        public List<KPI_SL_Du_BaoTime> ListKPI_SL_Du_BaoTime;



        public MetaData MetaData { get; set; }

    }
}