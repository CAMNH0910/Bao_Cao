using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_BD10_Hub
    {
        public int STT { get; set; }
        public string Ngay_Di { get; set; }
        public string Duong_Thu { get; set; }
        public string Ma_DT { get; set; }
        public string BD10 { get; set; }
        public string Ma_BG { get; set; }
        public string BC_Den { get; set; }
        public string Ngay_Gio_Den { get; set; }
    }
    public class KPI_BD10_Hub_EX
    {
        public int STT { get; set; }
        public string Ngay_Di { get; set; }
        public string Duong_Thu { get; set; }
        public string Ma_DT { get; set; }
        public string BD10 { get; set; }
        public string Ma_BG { get; set; }
        public string BC_Den { get; set; }
        public string Ngay_Gio_Den { get; set; }
    }

    public class ReturnKPI_BD10_Hub
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

        public KPI_BD10_Hub KPI_BD10_Hub { get; set; }
        public List<KPI_BD10_Hub> ListKPI_BD10_Hub;

        public KPI_BD10_Hub_EX KPI_BD10_HubEX { get; set; }
        public List<KPI_BD10_Hub_EX> ListKPI_BD10_HubEX;



        public MetaData MetaData { get; set; }

    }
}