using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class Tms_Kpi_HT
    {
        public int STT { get; set; }
        public string HUB { get; set; }
        public string TINH_PHAT { get; set; }
        public string HUYEN_PHAT { get; set; }
        public string TEN_HUYEN { get; set; }
        public string ID_HANH_TRINH { get; set; }
        public string TEN_HANH_TRINH { get; set; }
        public string THOI_GIAN_DI { get; set; }
        public string Trang_Thai { get; set; }
    }

    public class ReturnTms_Kpi_HT
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

        public Tms_Kpi_HT TTms_Kpi_HTlReport { get; set; }
        public List<Tms_Kpi_HT> List_Tms_Kpi_HT;


        public MetaData MetaData { get; set; }

    }
}