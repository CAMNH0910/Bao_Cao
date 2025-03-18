using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class Chap_Nhan_TH
    {
        public int STT { get; set; }
        public string MA_BC_CN { get; set; }
        public string TIME_GROUP { get; set; }
        public int SL { get; set; }
        public int KPI_RESULT { get; set; }
        public string TL { get; set; }
        public int KDQD { get; set; }
        public string TL_KDQD { get; set; }
    }
    public class Chap_Nhan_CT
    {
        public int STT { get; set; }
        public string ITEMCODE { get; set; }
        public string MA_BC_CN { get; set; }
        public string THOI_GIAN_CN { get; set; }
        public string TGD_100916 { get; set; }
        public string TGD_101000 { get; set; }
        public string MA_BC_DEN { get; set; }
        public string Danh_Gia { get; set; }
    }
    public class ReturnKhai_Thac_BDHN
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

        public Chap_Nhan_TH Chap_Nhan_TH { get; set; }
        public List<Chap_Nhan_TH> ListChap_Nhan_TH;

        public Chap_Nhan_CT Chap_Nhan_CT { get; set; }
        public List<Chap_Nhan_CT> ListChap_Nhan_CT;
    }
}