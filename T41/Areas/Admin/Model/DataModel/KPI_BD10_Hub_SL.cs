using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_BD10_Hub_SL
    {

        public int STT { get; set; }
        public string Duong_Thu { get; set; }
        public string Ten_Duong_Thu { get; set; }
        public string BD10 { get; set; }
        public string BCGui { get; set; }
        public string BCNhan { get; set; }
        public string Ngay { get; set; }
        public string SL { get; set; }
        public string KL { get; set; }
    }
    public class KPI_BD10_Hub_SLEX
    {

        public int STT { get; set; }
        public string Duong_Thu { get; set; }
        public string Ten_Duong_Thu { get; set; }
        public string BD10 { get; set; }
        public string BCGui { get; set; }
        public string BCNhan { get; set; }
        public string Ngay { get; set; }
        public string SL { get; set; }
        public string KL { get; set; }
    }


    public class ReturnKPI_BD10_Hub_SL
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

        public KPI_BD10_Hub_SL KPI_BD10_Hub_SL { get; set; }
        public List<KPI_BD10_Hub_SL> ListKPI_BD10_Hub_SL;

        public KPI_BD10_Hub_SLEX KPI_BD10_Hub_SLEX { get; set; }
        public List<KPI_BD10_Hub_SLEX> ListKPI_BD10_Hub_SLEX;



        public MetaData MetaData { get; set; }

    }
}