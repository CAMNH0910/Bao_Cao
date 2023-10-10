using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_BD10_Hub_Time
    {

        public int STT { get; set; }
        public string Duong_Thu { get; set; }
        public string Ma_DT { get; set; }
        public string BD10 { get; set; }
        public string Ma_BG { get; set; }
        public string BC_Den { get; set; }
        public string Ngay_Gio_Den { get; set; }
    }

    public class KPI_BD10_Hub_TimeEX
    {

        public int STT { get; set; }
        public string Duong_Thu { get; set; }
        public string Ma_DT { get; set; }
        public string BD10 { get; set; }
        public string Ma_BG { get; set; }
        public string BC_Den { get; set; }
        public string Ngay_Gio_Den { get; set; }
    }


    public class ReturnKPI_BD10_Hub_Time
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

        public KPI_BD10_Hub_Time KPI_BD10_Hub_Time { get; set; }
        public List<KPI_BD10_Hub_Time> ListKPI_BD10_Hub_Time;

        public KPI_BD10_Hub_TimeEX KPI_BD10_Hub_TimeEX { get; set; }
        public List<KPI_BD10_Hub_TimeEX> ListKPI_BD10_Hub_TimeEX;



        public MetaData MetaData { get; set; }

    }
}