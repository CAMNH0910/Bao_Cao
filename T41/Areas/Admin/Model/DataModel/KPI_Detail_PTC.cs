using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class KPI_Detail_PTC
    {

        public int STT { get; set; }
        public string zone { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public int SLD { get; set; }
        public int PTC { get; set; }
        public string TL_PTC { get; set; }
        public int PTC_LD { get; set; }
        public string TLPTC_LD { get; set; }
        public int PTC_TN_1 { get; set; }
        public int PTC_TN_2 { get; set; }
        public int Tong_PTC_TN { get; set; }
        public string TLTong_PTC_TN { get; set; }
        public int KDQD { get; set; }
        public int PKTC { get; set; }
        
    }
    public class KPI_Detail_PTC_CT
    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STATUSDATETIME { get; set; }
        public string C19 { get; set; }
        public string TIMEINTERVAL { get; set; }
    }
    public class KPI_Detail_PTC_BT
    {

        public int STT { get; set; }
        public string zone { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string ROUTECODENAME { get; set; }
        public string ROUTECODE { get; set; }
        public string POSTMAN_HRM { get; set; }
        public string POSTMAN_ID { get; set; }
        public string POSTMANName { get; set; }
        public string ServiceTypeName { get; set; }
        public string SLD_TC_1 { get; set; }
        public string SL_PTC_1 { get; set; }
        public string TL_PTC_1 { get; set; }
        public string SLD_TC_2 { get; set; }
        public string SL_PTC_2 { get; set; }
        public string TL_PTC_2 { get; set; }
        public string SLD_TC_3 { get; set; }
        public string SL_PTC_3 { get; set; }
        public string TL_PTC_3 { get; set; }
    }


    public class ReturnKPI_Detail_PTC
    {
        public string Code { get; set; }

        public string Message { get; set; }
        public int Total { get; set; }

        public KPI_Detail_PTC KPI_Detail_PTC { get; set; }
        public List<KPI_Detail_PTC> ListKPI_Detail_PTC;
        
        public KPI_Detail_PTC_CT KPI_Detail_PTC_CT { get; set; }
        public List<KPI_Detail_PTC_CT> ListKPI_Detail_PTC_CT;
        public KPI_Detail_PTC_BT KPI_Detail_PTC_BT { get; set; }
        public List<KPI_Detail_PTC_BT> ListKPI_Detail_PTC_BT;

    }
}