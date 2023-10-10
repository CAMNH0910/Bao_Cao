using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_Delivery_Time
    {
        public int STT { get; set; }
        public string Ma_Tinh { get; set; }
        public string Ten_Tinh { get; set; }
        public string XND { get; set; }
        public string XNP { get; set; }
        public string PTC { get; set; }
        public string Ngay { get; set; }
    }
    public class KPI_Delivery_Time_EX
    {
        public int STT { get; set; }
        public string Ma_Tinh { get; set; }
        public string Ten_Tinh { get; set; }
        public string XND { get; set; }
        public string XNP { get; set; }
        public string PTC { get; set; }
        public string Ngay { get; set; }
    }
    public class ReturnKPI_Delivery_Time
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

        public KPI_Delivery_Time KPI_Delivery_Time { get; set; }
        public List<KPI_Delivery_Time> ListKPI_Delivery_Time;

        public KPI_Delivery_Time_EX KPI_Delivery_Time_EX { get; set; }
        public List<KPI_Delivery_Time_EX> ListKPI_Delivery_Time_EX;



        public MetaData MetaData { get; set; }

    }
}