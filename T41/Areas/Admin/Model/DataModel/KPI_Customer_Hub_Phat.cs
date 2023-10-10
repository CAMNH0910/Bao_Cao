using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_Customer_Hub_Phat
    {
        public int STT { get; set; }
        public string ITEMCODE { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string STATUSDATETIME { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string COD { get; set; }
        public string COD_PP { get; set; }
        public string STATUS_BCCP { get; set; }
        public string DELIVERYDATE { get; set; }
        public string STATUSPP { get; set; }
        public string DELIVERY_DATEPP { get; set; }
        public string ENDPOSTCODENAME { get; set; }
        public string ROUTECODE { get; set; }
    }


    public class EXKPI_Customer_Hub_Phat
    {
        public int STT { get; set; }
        public string ITEMCODE { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string STATUSDATETIME { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string COD { get; set; }
        public string COD_PP { get; set; }
        public string STATUS_BCCP { get; set; }
        public string DELIVERYDATE { get; set; }
        public string STATUSPP { get; set; }
        public string DELIVERY_DATEPP { get; set; }
        public string ENDPOSTCODENAME { get; set; }
        public string ROUTECODE { get; set; }
    }
    public class ReturnKPI_Customer_Hub_Phat
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public KPI_Customer_Hub_Phat KPI_Customer_Hub_Phat_Report { get; set; }
        public List<KPI_Customer_Hub_Phat> ListKPI_Customer_Hub_PhatReport;

        public EXKPI_Customer_Hub_Phat KPI_Customer_EXReport { get; set; }
        public List<EXKPI_Customer_Hub_Phat> ListKPI_EXCustomerReport;


    }
}