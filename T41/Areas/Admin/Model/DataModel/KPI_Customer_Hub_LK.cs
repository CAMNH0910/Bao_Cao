using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_Customer_Hub_LK
    {
        public int STT { get; set; }
        public string ITEMCODE { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string STATUS { get; set; }
        public string DELIVERYDATE { get; set; }
        public string ENDPOSTCODENAME { get; set; }
        public string ROUTECODE { get; set; }
        public string AMND_DATE { get; set; }
        public string Status_LK { get; set; }
    }


    public class EXKPI_Customer_Hub_LK
    {
        public int STT { get; set; }
        public string ITEMCODE { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string STATUS { get; set; }
        public string DELIVERYDATE { get; set; }
        public string ENDPOSTCODENAME { get; set; }
        public string ROUTECODE { get; set; }
        public string AMND_DATE { get; set; }
        public string Status_LK { get; set; }
    }
    public class ReturnKPI_Customer_Hub_LK
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public KPI_Customer_Hub_LK KPI_Customer_Hub_LK_Report { get; set; }
        public List<KPI_Customer_Hub_LK> ListKPI_Customer_Hub_LKReport;

        public EXKPI_Customer_Hub_LK KPI_Customer_EXReport { get; set; }
        public List<EXKPI_Customer_Hub_LK> ListKPI_EXCustomerReport;


    }
}