using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_Customer
    {
        public int STT { get; set; }
        public string Custommer { get; set; }
        public string STARTPROVINCE { get; set; }
        public string STARTPROVINCENAME { get; set; }
        public string ENDPROVINCE { get; set; }
        public string ENDPROVINCENAME { get; set; }
        public string TongSL { get; set; }
        public string PTC { get; set; }
        public string TLPTC { get; set; }
        public string KTC { get; set; }
        public string TLKTC { get; set; }
        public string PCH { get; set; }
        public string TLCH { get; set; }
        public string CCTT { get; set; }
        public string TLCCTT { get; set; }
        public string DCT { get; set; }
        public string TLDCT { get; set; }
        public string KDCT { get; set; }
        public string TLKDCT { get; set; }
        public int STATUSDATE { get; set; }
        public string DELIVERYDATE { get; set; }
    }
    public class KPI_Customer_Detail
    {
        public int STT { get; set; }
        public string ITEMCODE { get; set; }
        public string STATUSDATE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string STARTPROVINCENAME { get; set; }
        public string DELIVERYDATE { get; set; }
        public string ENDPOSTCODENAME { get; set; }
        public string CAUSECODE { get; set; }
        public string FREQOCCUR { get; set; }
    }

    public class KPI_Customer_DetailKDCT
    {
        public int STT { get; set; }
        public string ITEMCODE { get; set; }
        public string STATUSDATETIME { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string STARTPROVINCENAME { get; set; }
        public string DELIVERYDATE { get; set; }
        public string ENDPOSTCODENAME { get; set; }
        public string STATUS { get; set; }
        public string TIMELINE { get; set; }
        public string CRITERION { get; set; }
    }
    public class KPI_Customer_Excel

    {
        public int STT { get; set; }
        public string STARTPROVINCE { get; set; }
        public string STARTPROVINCENAME { get; set; }
        public string ENDPROVINCE { get; set; }
        public string ENDPROVINCENAME { get; set; }
        public string TongSL { get; set; }
        public string PTC { get; set; }
        public string TLPTC { get; set; }
        public string KTC { get; set; }
        public string TLKTC { get; set; }
        public string PCH { get; set; }
        public string TLCH { get; set; }
        public string CCTT { get; set; }
        public string TLCCTT { get; set; }
        public string DCT { get; set; }
        public string TLDCT { get; set; }
        public string KDCT { get; set; }
        public string TLKDCT { get; set; }
    }
    public class ReturnKPI_Customer
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public KPI_Customer KPI_CustomerReport { get; set; }
        public List<KPI_Customer> ListKPI_CustomerReport;
        public KPI_Customer_Detail KPI_Customer_DetailReport { get; set; }
        public List<KPI_Customer_Detail> ListKPI_Customer_DetailReport;
        public KPI_Customer_Excel KPI_Customer_EXReport { get; set; }
        public List<KPI_Customer_Excel> ListKPI_EXCustomerReport;
        public KPI_Customer_DetailKDCT KPI_Customer_DetailKDCT { get; set; }
        public List<KPI_Customer_DetailKDCT> ListKPI_Customer_DetailKDCT;


    }
}