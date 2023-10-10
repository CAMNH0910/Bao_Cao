using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_Delivery_Wage
    {
        public int STT { get; set; }
        public string ZONE { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string ServiceTypeName { get; set; }
        public string Total { get; set; }
        public string SL_DG { get; set; }
        public string KXD { get; set; }
        public string TotalPTC { get; set; }
        public string TotalPKTC { get; set; }
        public string TC { get; set; }
        public string PTC6H { get; set; }
        public string PTC72H { get; set; }
       
        public string ServiceTypeNumber { get; set; }
    }
    public class KPI_Detail_Wage
    {
        public int STT { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string ROUTECODE { get; set; }
        public string ROUTECODENAME { get; set; }
        public string POSTMAN_HRM { get; set; }
        public string POSTMAN_ID { get; set; }
        public string POSTMANName { get; set; }
        public string ServiceTypeName { get; set; }
        public string TotalM { get; set; }
        public string TotalCT { get; set; }
        public string Total { get; set; }
        public string TotalPL { get; set; }
        public string SLD2 { get; set; }
        public string KLD2 { get; set; }
        public string SLT2 { get; set; }
        public string KLT2 { get; set; }
        public string TotalSL { get; set; }
        public string TotalKL { get; set; }
        public string TotalKTC { get; set; }
        public string TotalAll { get; set; }
        public string PTC6H { get; set; }
        public string PTC72H { get; set; }
        public string ServiceTypeNumber { get; set; }
        public string ZONE { get; set; }
    }


    public class Detail_CTTH
    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string ROUTECODE  { get; set; }
        public string ROUTECODENAME { get; set; }
        public string POSTMAN_ID { get; set; }
        public string POSTMANName { get; set; }
        public string STATUSDATETIME { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeNumber { get; set; }

    }


    public class Detail_CT
    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string ROUTECODE { get; set; }
        public string ROUTECODENAME { get; set; }
        public string POSTMAN_ID { get; set; }
        public string POSTMANName { get; set; }
        public string STATUSDATETIME { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeNumber { get; set; }

    }

    public class Detail_CTCT
    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string ROUTECODE { get; set; }
        public string ROUTECODENAME { get; set; }
        public string POSTMAN_ID { get; set; }
        public string POSTMANName { get; set; }
        public string STATUSDATETIME { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeNumber { get; set; }
       

    }

    public class Detail_KGD
    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string STATUSDATETIME { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeNumber { get; set; }

    }


    public class Detail_KG
    {
        public int STT { get; set; }
        public string ZONE { get; set; }
        public string STARTPOSTCODE { get; set; }
        public string STARTPOSTCODENAME { get; set; }
        public string ServiceTypeName { get; set; }
        public string Total { get; set; }
        public string N_5KG { get; set; }

        public string KL_N_5KG { get; set; }
        public string L_5KG { get; set; }
        public string KL_L_5KG { get; set; }
        public string PTC6H { get; set; }
        public string PTC72H { get; set; }

    }
    public class ReturnKPI_Wage
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

        public KPI_Delivery_Wage KPI_Delivery_Wage { get; set; }
        public List<KPI_Delivery_Wage> ListDetaiKPI_Delivery_Wage;

        public KPI_Detail_Wage KPI_Detail_Wage { get; set; }
        public List<KPI_Detail_Wage> ListDetailKPI_Detail_Wage;

        public Detail_CTTH Detail_CTTH { get; set; }
        public List<Detail_CTTH> ListDetail_CTTH;

        public Detail_CT Detail_CT { get; set; }
        public List<Detail_CT> ListDetail_CT;

        public Detail_CTCT Detail_CTCT { get; set; }
        public List<Detail_CTCT> ListDetail_CTCT;

        public Detail_KGD Detail_KGD { get; set; }
        public List<Detail_KGD> ListDetail_KGD;

        public Detail_KG Detail_KG { get; set; }
        public List<Detail_KG> ListDetail_KG;
    }

}